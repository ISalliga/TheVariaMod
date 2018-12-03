using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.Miscellaneous
{
    class VacuumShot : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.tileCollide = true;

            // 5 second fuse.
            projectile.timeLeft = 300;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, 0, Main.DiscoB, projectile.alpha);
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
            FrameCountMeter++;
            if (FrameCountMeter >= 4)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.X += (projectile.velocity.X * -1) * 3 / 7;
            target.velocity.Y += (projectile.velocity.Y * -1) * 3 / 7;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, projectile.width * (int)projectile.scale, projectile.height * (int)projectile.scale, 1, 0f, 0f, 0, new Color(0, 167, 255), 1f)];
                Dust dust2;
                dust2 = Main.dust[Terraria.Dust.NewDust(position, projectile.width * (int)projectile.scale, projectile.height * (int)projectile.scale, 1, 0f, 0f, 0, new Color(255, 0, 226), 1f)];
                dust.noGravity = true;
                dust2.noGravity = true;
            }
        }
    }
}
