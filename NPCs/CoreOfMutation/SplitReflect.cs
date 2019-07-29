using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
	public class SplitReflect : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reflected Splitball");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 90;
            projectile.alpha = 255;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
       	{
			Dust dust;
			dust = Terraria.Dust.NewDustPerfect(projectile.Center, 6, new Vector2(0f, 0f), 0, default(Color), 2f);
            dust.noGravity = true;
        }
    }
}