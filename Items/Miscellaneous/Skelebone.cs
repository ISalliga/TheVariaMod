using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class Skelebone : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skelebone");
		}

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 22;
            projectile.timeLeft = 600;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            aiType = ProjectileID.Bullet;
        }

        public override void AI() // Homes in.
        {
            AdjustMagnitude(ref projectile.velocity);
            Vector2 move = Vector2.Zero;
            float distance = 1000f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                AdjustMagnitude(ref projectile.velocity);
            }
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 20f / magnitude;
            }
        }
    }
}