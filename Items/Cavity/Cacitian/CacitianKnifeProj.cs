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

namespace Varia.Items.Cavity.Cacitian
{
	public class CacitianKnifeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacitian Poison Knife");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 2000;
			projectile.width = 20;
			projectile.height = 20;
			projectile.ignoreWater = true;
			projectile.penetrate = 3;
			projectile.alpha = 50;
		}
		public override void AI()
		{
			projectile.rotation += 0.4f;
			projectile.velocity.Y += 0.45f;
            projectile.velocity.X = projectile.velocity.X * 50 / 51;

        }
        public override void OnHitNPC(NPC target,  int damage,  float knockback,  bool crit)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42,  5),  projectile.Center);
            target.AddBuff(BuffID.Poisoned,  120);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42,  5),  projectile.Center);
        }
	}
}