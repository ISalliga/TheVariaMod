using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class AtomizerProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 240;
            projectile.ranged = true;
        }

        public override void AI()
        {
            for (int num134 = 0; num134 < 7; num134++)
			{
				float x = projectile.position.X - projectile.velocity.X / 10f * (float)num134;
				float y = projectile.position.Y - projectile.velocity.Y / 10f * (float)num134;
				int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 27, 0f, 0f, 0, Color.Blue, 2f);
				Main.dust[num135].alpha = projectile.alpha;
				Main.dust[num135].position.X = x;
				Main.dust[num135].position.Y = y;
				Main.dust[num135].velocity *= 0f;
				Main.dust[num135].noGravity = true;
			}
        }
		
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
		
        public override void Kill(int timeLeft)
        {
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("AtomExplosion"), 27, 0, Main.myPlayer, 0f, 0f);
        }
    }
}