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
	public class LightAfterimage : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light Afterimage");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.melee = true;
			projectile.timeLeft = 2000;
			projectile.aiStyle = 1;
			projectile.width = 14;
			projectile.height = 28;
			projectile.ignoreWater = true;
			projectile.maxPenetrate = 2;
			projectile.alpha = 50;
		}
		public override void AI()
		{
			projectile.velocity.Y += 0.35f;
		}
		public override void Kill(int timeLeft)
        {
			for (int num623 = 0; num623 < 35; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X,  projectile.position.Y),  projectile.width,  projectile.height,  20,  0f,  0f,  100,  Color.SkyBlue,  3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
			}
		}
	}
}