using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class ForgottenSpirit : ModProjectile
	{
        NPC target;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forgotten Old World Spirit");
			Main.projFrames[projectile.type] = 7;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
		{
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 22;
            projectile.height = 34;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			return true;
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(122, 248, 178);
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 7)
                {
                    projectile.frame = 0;
                }
            }

            Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);

            if (player.dead)
            {
                modPlayer.ForgottenSpirit = false;
            }
            if (modPlayer.ForgottenSpirit)
            {
                projectile.timeLeft = 2;
            }

            projectile.rotation = 0;
            projectile.rotation += projectile.velocity.X * 0.01f;
            if (projectile.rotation > 0.5f) projectile.rotation = 0.5f;
            if (projectile.rotation < -0.5f) projectile.rotation = -0.5f;

            projectile.ai[1]++;

            bool isTargetingNPC = false;

            if (Methods.ClosestNPC(ref target, 900, projectile.Center))
            {
                isTargetingNPC = true;
            }

            Vector2 flyTo;

            if (!isTargetingNPC)
            {
                flyTo = Main.player[projectile.owner].Center - new Vector2(0, 50 + 35 * (projectile.minionPos));
            }
            else
            {
                flyTo = target.Center;
            }

            projectile.velocity += projectile.DirectionTo(flyTo) * projectile.Distance(flyTo) / 350;
            if (projectile.velocity.X >= 14) projectile.velocity.X = 14;
            if (projectile.velocity.X <= -14) projectile.velocity.X = -14;
            if (projectile.velocity.Y >= 14) projectile.velocity.Y = 14;
            if (projectile.velocity.Y <= -14) projectile.velocity.Y = -14;

            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 206, 0f, 2.105263f, 0, new Color(255, 255, 255), 0.6578947f)];
            }

            if (projectile.Distance(flyTo) > 5000 && !isTargetingNPC) projectile.position = flyTo;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X * 0.75f;
            projectile.velocity.Y = -projectile.velocity.Y * 0.75f;
        }
    }
}