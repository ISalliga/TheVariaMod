using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class AtomExplosion : ModProjectile
    {
		int FrameCountMeter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atom Explosion");
			Main.projFrames[projectile.type] = 5;
		}
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = 200;
            projectile.timeLeft = 20;
            projectile.ranged = true;
			projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(0, 0, 200, projectile.alpha);
        }

        public override void AI()
        {
			FrameCountMeter++;
			if (FrameCountMeter >= 4)
			{
				projectile.frame++;
				FrameCountMeter = 0;
				if (projectile.frame > 4)
				{
					projectile.frame = 0;
				}
			}
        }
    }
}