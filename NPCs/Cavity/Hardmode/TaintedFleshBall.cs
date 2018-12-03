using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.NPCs.Cavity.Hardmode
{
	public class TaintedFleshBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tainted Flesh Ball");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.hostile = true;
			projectile.timeLeft = 200;
			projectile.aiStyle = 0;
			projectile.width = 22;
			projectile.height = 22;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.knockBack = 0;
		}
	}
}