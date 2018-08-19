using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.Resizers
{
    public class SmallThing : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
            projectile.ranged = true;
        }

        public override void AI()
        {
            for (int num134 = 0; num134 < 10; num134++)
			{
				float x = projectile.position.X - projectile.velocity.X / 10f * (float)num134;
				float y = projectile.position.Y - projectile.velocity.Y / 10f * (float)num134;
				int num135 = Dust.NewDust(new Vector2(x, y), 1, 1, 15, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num135].alpha = projectile.alpha;
				Main.dust[num135].position.X = x;
				Main.dust[num135].position.Y = y;
				Main.dust[num135].velocity *= 0f;
				Main.dust[num135].noGravity = true;
			}
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.scale *= 0.5f;
        }
    }
}