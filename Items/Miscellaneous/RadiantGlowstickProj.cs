using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class RadiantGlowstickProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Glowstick");
		}

		public override void SetDefaults()
		{
			projectile.width = 15;
			projectile.height = 15;
			projectile.timeLeft = 3600;
			projectile.alpha = 0;
			projectile.aiStyle = 14;
			projectile.hostile = false;
            projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
		}
		public override void AI()
       	{
            Player player = Main.player[projectile.owner];
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 63, 0f, 0f, 50, default(Color), 1f);
			    Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 5f;
			}
            Lighting.AddLight(projectile.Center, new Vector3(0.12f, 0.12f, 0.18f));
        }
    }
}