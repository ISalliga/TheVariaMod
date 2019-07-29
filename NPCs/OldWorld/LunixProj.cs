using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.OldWorld
{
	public class LunixProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunix Bolt");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
            projectile.alpha = 255;
			projectile.timeLeft = 600;
			projectile.aiStyle = 0;
            projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
        }
		public override void AI()
       	{
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 29, new Color(0, 0, 255), 1.5f);
            Main.dust[dust].noGravity = true;
        }
        public override bool OnTileCollide(Vector2 velocityChange)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
            for (int q = 0; q < 20; q++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color), 3f);
                Main.dust[dust].velocity *= 2f;
                Main.dust[dust].noGravity = true;
            }
            return true;
        }
    }
}