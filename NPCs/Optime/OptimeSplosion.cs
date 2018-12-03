using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.Optime
{
    class OptimeSplosion : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.hostile = true;
            projectile.penetrate = 10;
            projectile.tileCollide = false;
            projectile.timeLeft = 24;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 6;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 0, projectile.alpha);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = projectile.damage * 3 / 2;
        }

        public override void AI()
        {
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
