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
using Terraria.Graphics.Shaders;

namespace Varia.Items.Cavity.Cacitian
{
    public class CavitousSmog : ModProjectile
    {
        int FrameCountMeter = 0;
		int timeAlive = 0;
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 30;
		    projectile.damage = 4;
            projectile.scale = 1;
			projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 4;
            projectile.timeLeft = 100;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cavitous Smog");
            Main.projFrames[projectile.type] = 5;
        }
		public override void AI()
		{
            timeAlive++;
            if (timeAlive < 20)
			{
				projectile.alpha -= 15;
			}
			else
			{
				projectile.alpha += 5;
			}

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
            projectile.velocity.X = (projectile.velocity.X * 15) / 16;
            projectile.velocity.Y = (projectile.velocity.Y * 15) / 16;
            projectile.scale += .015f;
        }
    }
}