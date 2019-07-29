using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class SaintsProj : ModProjectile
    {
		int FrameCountMeter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saintsplosion");
			Main.projFrames[projectile.type] = 5;
		}
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = 200;
            projectile.timeLeft = 15;
            projectile.ranged = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 175, 145, projectile.alpha);
        }

        public override void AI()
        {
			FrameCountMeter++;
			if (FrameCountMeter >= 3)
			{
				projectile.frame++;
				FrameCountMeter = 0;
			}
			projectile.velocity.X = 0;
			projectile.velocity.Y = 0;
        }
    }
}