using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.Items.Miscellaneous
{
	public class FyrebyrdProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fyrebyrd Rocket");
            Main.projFrames[projectile.type] = 6;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.OrangeRed;
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 44;
            projectile.timeLeft = 600;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.tileCollide = true;
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

            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = Methods.RotationTo(projectile.Center, projectile.Center - projectile.velocity) + MathHelper.ToRadians(90f);
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 11f / magnitude;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int numDust = 0; numDust < 24; numDust++)
            {
                {
                    Dust dust;
                    Vector2 position = projectile.position - new Vector2(10, 10);
                    dust = Main.dust[Dust.NewDust(position, projectile.width + 20, projectile.height + 20, 127, 0f, 0f, 0, new Color(75, 155, 255), 2.2f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                }
            }

            foreach (NPC npc in Main.npc)
            {
                if (npc.Distance(projectile.Center) <= 20) npc.StrikeNPC(projectile.damage, 0f, 0);
            }

            int num20 = 36;
            for (int i = 0; i < num20; i++)
            {
                Vector2 pos = projectile.Center;
                Vector2 spinningpoint = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f;
                spinningpoint = spinningpoint.RotatedBy((double)((float)(i - (num20 / 2 - 1)) * 6.28318548f / (float)num20), default(Vector2)) + pos;
                Vector2 vector = spinningpoint - pos;
                int num21 = Dust.NewDust(spinningpoint + vector, 0, 0, 127, vector.X * 2, vector.Y * 2, 0, new Color(75, 155, 255), 2.5f);
                Main.dust[num21].noGravity = true;
                Main.dust[num21].noLight = true;
            }

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), projectile.Center);
        }
    }
}