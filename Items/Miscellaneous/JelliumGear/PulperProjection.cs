using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;
using Varia;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear

{
    public class PulperProjection : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulper Projection");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }

        public override void SetDefaults()
		{
            projectile.damage = 40;
			projectile.friendly = true;
			projectile.timeLeft = 90;
            projectile.alpha = 0;
			projectile.width = 22;
            projectile.melee = true;
            projectile.height = 40;
            projectile.scale = 1.4f;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}

        public override void AI()
        {
            projectile.velocity.X *= 0.96f;
            projectile.velocity.Y *= 0.96f;
            projectile.rotation += 0.004f * projectile.timeLeft;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                float Speed = 8f;
                int damage = 10;
                int type = mod.ProjectileType("JellyBeanSmol");
                float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(projectile.Center.Y - 10, projectile.Center.X - 10));
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                Main.projectile[proj].timeLeft = 10;
                Main.projectile[proj].tileCollide = false;
                Main.projectile[proj].friendly = true;
                Main.projectile[proj].hostile = false;
            }
        }
    }
}