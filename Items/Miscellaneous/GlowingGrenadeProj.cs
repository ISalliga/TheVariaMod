using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class GlowingGrenadeProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Grenade");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 20;
			projectile.timeLeft = 300;
			projectile.alpha = 0;
			projectile.aiStyle = 14;
			projectile.hostile = false;
            projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 2;
		}
		public override void AI()
       	{
            Player player = Main.player[projectile.owner];
            if (projectile.timeLeft == 10 || projectile.penetrate == 1)
            {
			   	projectile.width = 100;
				projectile.height = 100;
				projectile.timeLeft = 9;
				projectile.alpha = 255;
				projectile.penetrate = -1;
				projectile.tileCollide = false;
				projectile.friendly = false;
				projectile.hostile = true;
            }
			if (projectile.timeLeft == 9)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);			
				projectile.position += new Vector2(-50, -50);
			}
			if (projectile.timeLeft < 10)
				projectile.velocity = new Vector2(0, 0);
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 63, 0f, 0f, 50, default(Color), 1f);
			    Main.dust[dust].noGravity = true;
			}
            Lighting.AddLight(projectile.Center, 0.4f, 0.4f, 0.6f);
        }
    }
}