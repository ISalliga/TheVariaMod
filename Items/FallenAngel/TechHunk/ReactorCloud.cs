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

namespace Varia.Items.FallenAngel.TechHunk
{
	public class ReactorCloud : ModProjectile
	{
		int slowTime = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reactor Cloud");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.timeLeft = 120;
			projectile.width = 75;
            projectile.tileCollide = false;
			projectile.height = 75;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.alpha = 50;
		}
        public override void AI()
        {
            for (int dusterino = 0; dusterino <= 3; dusterino++)
            {
                Dust dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 1, 1, 226, Main.rand.Next(-6, 7), Main.rand.Next(-6, 7), 0, new Color(255, 255, 255), 0.5f)];
                dust.noGravity = true;
            }

            projectile.velocity.X *= 0.89f;
            projectile.velocity.Y *= 0.89f;
        }
	}
}