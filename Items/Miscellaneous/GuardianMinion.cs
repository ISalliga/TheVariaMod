using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class GuardianMinion : ModProjectile
	{
        int FrameCountMeter = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Personal Guardian");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabySkeletronHead);
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 28;
            projectile.height = 32;
			aiType = ProjectileID.BabySkeletronHead;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
        }

		public override bool PreAI()
		{
            Player player = Main.player[projectile.owner];
			return true;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.rotation = 0;
            return false;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);
            if (player.dead)
            {
                modPlayer.guardianMinion = false;
            }
            if (modPlayer.guardianMinion)
            {
                projectile.timeLeft = 2;
            }

            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
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
            if (magnitude > 16f)
            {
                vector *= 16f / magnitude;
            }
        }
    }
}