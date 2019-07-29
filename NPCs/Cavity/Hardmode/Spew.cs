using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Varia;

namespace Varia.NPCs.Cavity.Hardmode
{
	public class Spew : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spew");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.hostile = true;
			projectile.timeLeft = 15;
			projectile.aiStyle = 0;
			projectile.width = 22;
			projectile.height = 22;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.knockBack = 0;
		}

        public override void AI()
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X, position.Y), 0, 0, 33, 0f, -5.631579f, 0, new Color(255, 226, 0), 1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(53, Main.LocalPlayer);
            }

            if (Main.rand.NextFloat() < 0.5f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X, position.Y), 0, 0, 36, 0f, -2.631579f, 0, new Color(255, 226, 0), 0.3947368f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(53, Main.LocalPlayer);
            }

            projectile.velocity.Y++;
        }
    }
}