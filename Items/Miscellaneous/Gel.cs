using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class Gel : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gel");
		}

		public override void SetDefaults()
		{
			projectile.damage = 1;
			projectile.thrown = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.alpha = 0;
			projectile.timeLeft = 900;
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
		public override bool OnTileCollide(Vector2 velocityChange)
		{
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 48);
			projectile.velocity = new Vector2(0, 0);
			return false;
		}
	}
}
