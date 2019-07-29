using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
	public class WarningBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warning Beam");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 90;
            projectile.alpha = 255;
			projectile.aiStyle = 0;
            projectile.friendly = false;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
       	{
			Dust dust;
			dust = Dust.NewDustPerfect(projectile.Center, 114, new Vector2(0f, 0f), 0, default(Color), 1.5f);
            dust.noGravity = true;
        }
	}
}