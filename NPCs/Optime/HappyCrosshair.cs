using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.Optime
{
    public class HappyCrosshair : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
			projectile.damage = 0;
            projectile.height = 38;
            projectile.hostile = true;
			projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 255, 155, projectile.alpha);
        }
        public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 12, mod.ProjectileType("Happifier"), 20, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 12, 0, mod.ProjectileType("Happifier"), 20, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -12, mod.ProjectileType("Happifier"), 20, 0f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -12, 0, mod.ProjectileType("Happifier"), 20, 0f, Main.myPlayer, 0f, 0f);
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
			for (int num623 = 0; num623 < 70; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, Color.Blue, 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, Color.Blue, 2f);
				Main.dust[num624].velocity *= 2f;
			}
		}
    }
}