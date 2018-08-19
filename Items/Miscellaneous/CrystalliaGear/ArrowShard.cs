using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class ArrowShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
			projectile.damage = 34;
            projectile.height = 7;
			projectile.scale = 0.5f;
            projectile.friendly = true;
			projectile.aiStyle = 1;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
		
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity.Y += 0.25f;
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 118);
			for (int num623 = 0; num623 < 70; num623++)
			{
				if (Main.rand.NextFloat() < 1f)
				{
					Dust dust;
					Vector2 position = projectile.Center;
					dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 15, 0f, 0f, 218, new Color(255,0,201), 1.447368f)];
					dust.noGravity = true;
				}
			}
		}
    }
}