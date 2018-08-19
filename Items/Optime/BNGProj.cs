using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.Optime
{
    class BNGProj : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.penetrate = 10;
            projectile.tileCollide = false;

            // 5 second fuse.
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = projectile.damage * 3 / 2;
        }

        public override void AI()
        {
            projectile.velocity.X = projectile.velocity.X * 20 / 21;
            projectile.velocity.Y = projectile.velocity.Y * 30 / 31 - 0.005f;

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
    }
}
