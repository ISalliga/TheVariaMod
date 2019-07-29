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
using BaseMod;

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
		}
		public override void AI()
		{
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 15, 0.99f, 0.35f, 14);
        }
        public override void OnHitNPC(NPC target,  int damage,  float knockback,  bool crit)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42,  5),  projectile.Center);
            target.AddBuff(BuffID.Poisoned,  120);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42,  5),  projectile.Center);
            if (Main.rand.NextBool(3)) Item.NewItem(projectile.getRect(), mod.ItemType("CacitianKnife"));
        }
	}
}