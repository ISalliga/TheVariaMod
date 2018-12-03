using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.QueensInfantry
{
    public class VenomousWaste : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 30;
		    projectile.damage = 4;
            projectile.scale = 1;
			projectile.alpha = 255;
            projectile.tileCollide = true;
            projectile.hostile = true;
            projectile.penetrate = 9999;
            projectile.timeLeft = 120;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Venomous Waste");
        }
        public override void AI()
		{
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.alpha -= 25;
            projectile.velocity.Y += 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool isCollidingDownwards = false;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                if (projectile.velocity.Y <= 0) projectile.velocity.Y = -oldVelocity.Y / 2;
                else
                {
                    isCollidingDownwards = true;
                }
            }
            return isCollidingDownwards;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X - 5, projectile.Center.Y, 0, 0, mod.ProjectileType("VenomousPuddle"), 10, 0, projectile.owner, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
        }
    }
}