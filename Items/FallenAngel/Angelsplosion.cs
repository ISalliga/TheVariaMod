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

namespace Varia.Items.FallenAngel
{
	public class Angelsplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AngelSPLOSION");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.melee = true;
			projectile.timeLeft = 30;
			projectile.aiStyle = -1;
			projectile.width = 50;
			projectile.height = 50;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}
		public override void AI()
		{
            if (projectile.timeLeft > 15)
            {
                projectile.position.X -= 5;
                projectile.position.Y -= 5;
                projectile.width += 10;
                projectile.height += 10;
            }
			for (int i = 0; i < projectile.width / 20; i++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 156, Main.rand.Next(-projectile.width / 27, projectile.width / 27), Main.rand.Next(-projectile.height / 30, projectile.height / 30), 100, new Color(0, 67, 255), 1.6f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 2f;
			}
        }
    }
}