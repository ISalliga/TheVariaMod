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
    public class OptimePortal : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.scale = 1f;
            projectile.width = 40;
            projectile.height = 40;
            projectile.alpha = 0;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 5;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Portal of Terror");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 4;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 0, projectile.alpha);
        }
        public override void AI()
        {
            projectile.ai[1]++;
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
            if (projectile.ai[1] > 60)
            {
                int player = Player.FindClosest(projectile.Center, 1, 1);
                if (projectile.ai[1] % 10 == 0)
                {
                    projectile.velocity.X += projectile.DirectionTo(Main.player[player].Center).X * 2;
                    projectile.velocity.Y += projectile.DirectionTo(Main.player[player].Center).Y * 2;
                }
            }
        }
    }
}