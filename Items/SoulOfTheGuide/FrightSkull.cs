using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class FrightSkull : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fright Skull");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.magic = true;
			projectile.timeLeft = 51;
			projectile.aiStyle = -1;
			projectile.width = 80;
			projectile.height = 102;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}
		public override void AI()
		{
			projectile.alpha += 5;
		}
	}
}