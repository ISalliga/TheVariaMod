using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class JellomatProj : ModProjectile
    {
        int bounce = 6;
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 20;
			projectile.magic = true;
			projectile.damage = 17;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 9999;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jellomat");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
                projectile.velocity.Y += Main.rand.Next(-9, 10);
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
                projectile.velocity.X += Main.rand.Next(-3, 4);
            }
            bounce--;
            if (bounce == 0)
            {
                projectile.Kill();
            }
            return false;
        }
		public override void AI()
		{
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 10, 0.97f, 0.35f, 16);
        }
    }
}