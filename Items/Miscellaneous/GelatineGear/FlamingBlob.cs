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

namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class FlamingBlob : ModProjectile
    {
        int bounce = 2;
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 28;
			projectile.melee = true;
			projectile.damage = 17;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 9999;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(187, 89, 36, 50);
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flaming Blob");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 6;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int num621 = 0; num621 < 40; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 70; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y / 2;
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
            projectile.velocity.X = projectile.velocity.X * 15 / 16;
            projectile.velocity.Y += 2.1f;

            FrameCountMeter++;
            if (FrameCountMeter >= 4)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
        }
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
		}
    }
}