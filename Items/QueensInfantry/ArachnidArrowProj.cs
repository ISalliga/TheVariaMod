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

namespace Varia.Items.QueensInfantry
{
	public class ArachnidArrowProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnid Arrow");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 2000;
			projectile.width = 14;
			projectile.height = 14;
			projectile.ignoreWater = true;
			projectile.penetrate = 3;
		}
		public override void AI()
		{
            BaseAI.AIArrow(projectile, ref projectile.ai, 32, 0.15f, 12);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void OnHitNPC(NPC target,  int damage,  float knockback,  bool crit)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42,  5),  projectile.Center);
            if (Main.rand.NextBool(3)) target.AddBuff(BuffID.Poisoned,  120);
        }

        public override void Kill(int timeLeft)
        {
            if (Main.rand.NextBool(3)) Item.NewItem(projectile.getRect(), mod.ItemType("ArachnidArrow"));
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 5), projectile.Center);
        }
	}
}