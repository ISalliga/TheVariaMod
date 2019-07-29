using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
	public class Splitter : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutated Splitter");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.timeLeft = 90;
            projectile.alpha = 255;
			projectile.aiStyle = 0;
			projectile.hostile = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
       	{
			Dust dust;
			dust = Terraria.Dust.NewDustPerfect(projectile.Center, 6, new Vector2(0f, 0f), 0, default(Color), 5f);
            dust.noGravity = true;
        }
        public override bool OnTileCollide(Vector2 velocityChange)
        {
            float rotateby = MathHelper.ToRadians(360);
            Vector2 rotatedspeed = new Vector2(-projectile.velocity.X, -projectile.velocity.Y / 1.5f).RotatedBy(MathHelper.Lerp(-rotateby, rotateby, 1)) * .2f;
            if (Main.rand.Next(3) == 0)
            {
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, rotatedspeed.X, rotatedspeed.Y, mod.ProjectileType("SplitReflect"), 5, projectile.knockBack);
            }
            projectile.timeLeft = 0;
            return false;
        }
    }
}