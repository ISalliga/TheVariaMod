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
	public class PreservusProj : ModProjectile
	{
        bool hasCollided = false;

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Preservus");
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
			projectile.penetrate = 3;
		}
		public override void AI()
		{
            if (!hasCollided) BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 15, 0.99f, 0.35f, 20);
            else BaseAI.AIBoomerang(projectile, ref projectile.ai, default(Vector2), -1, -1, true, 100, 60, 0.4f, 0.4f, false);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!hasCollided)
            {
                projectile.velocity.Y = -15;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            }
            hasCollided = true;
            return false;
        }
    }
}