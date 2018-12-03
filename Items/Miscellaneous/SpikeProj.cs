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
    class SpikeProj : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 66;
            projectile.height = 64;
            projectile.damage = 1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 27;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 9;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 0, projectile.alpha);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            FrameCountMeter++;
            if (FrameCountMeter >= 3)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 9)
                {
                    projectile.frame = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(1, 4) == 1) Main.player[Player.FindClosest(projectile.Center, 45, 45)].HealEffect(Main.rand.Next(1, 3), false);
        }
    }
}
