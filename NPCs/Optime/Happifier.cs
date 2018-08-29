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

namespace Varia.NPCs.Optime
{
    public class Happifier : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.scale = 1f;
            projectile.width = 22;
            projectile.height = 10;
            projectile.alpha = 0;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.tileCollide = false;
            aiType = ProjectileID.Bullet;
            projectile.penetrate = 5;
            projectile.timeLeft = 1200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Happifier");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 4;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 255, 155, projectile.alpha);
        }
        public override void AI()
        {
            if (projectile.aiStyle == 1)
            {
                projectile.velocity.Y += 1.7f;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
            FrameCountMeter++;
            if (FrameCountMeter >= 5)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}