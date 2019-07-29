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

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class PectinatorProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pectinator");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 2000;
			projectile.width = 48;
			projectile.height = 48;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
		}
		public override void AI()
		{
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 15, 0.99f, 0.35f, 20);
        }
	}
}