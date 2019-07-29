using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class PinkGel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pink Gel");
		}

		public override void SetDefaults()
		{
			projectile.damage = 1;
			projectile.thrown = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.alpha = 0;
			projectile.timeLeft = 300;
			projectile.aiStyle = 14;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (projectile.velocity.X > 0)
				projectile.rotation += 1f;
			if (projectile.velocity.X < 0)
				projectile.rotation -= 1f;
		}
	    public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
            Player owner = Main.player[projectile.owner];
            n.AddBuff(137, 180);
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 48);
			projectile.velocity = new Vector2(0, 0);

			if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f) {
				projectile.velocity.X = oldVelocity.X * -0.9f;
			}
			if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f) {
				projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}
	}
}
