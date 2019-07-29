using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class FightNFlightProj : ModProjectile
    {
		int dustBurstTime = 0;
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 30;
            projectile.ranged = true;
        }

        public override void AI()
        {
            for (int num134 = 0; num134 < 5; num134++)
			{
				if (Main.rand.NextFloat() < 1f)
				{
					Dust dust;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = projectile.Center;
					dust = Terraria.Dust.NewDustPerfect(position, 16, new Vector2(0f, 0f), 0, new Color(255,255,255), 1.6f);
					dust.noGravity = true;
				}

			}
			dustBurstTime++;
			
			if (dustBurstTime >= 10)
			{
				int num20 = 36;
				for (int i = 0; i < num20; i++)
				{
					Vector2 spinningpoint = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f;
					spinningpoint = spinningpoint.RotatedBy((double)((float)(i - (num20 / 2 - 1)) * 6.28318548f / (float)num20), default(Vector2)) + projectile.Center;
					Vector2 vector = spinningpoint - projectile.Center;
					int num21 = Dust.NewDust(spinningpoint + vector, 0, 0, 16, vector.X * 2f, vector.Y * 2f, 0, new Color(255,255, 255), 1.6f);
					Main.dust[num21].noGravity = true;
					Main.dust[num21].noLight = true;
					Main.dust[num21].velocity = Vector2.Normalize(vector) * 3f;
				}
				dustBurstTime = 0;

                projectile.velocity.X += Main.rand.Next(-4, 5);
                projectile.velocity.Y += Main.rand.Next(-4, 5);
            }
        }
    }
}