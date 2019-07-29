using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using BaseMod;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
	public class RuinumFume : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spectrum Flame");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.melee = true;
			projectile.timeLeft = 120;
			projectile.width = 28;
			projectile.height = 28;
            projectile.penetrate = 2;
			projectile.ignoreWater = true;
			projectile.alpha = 50;
		}
        public override void AI()
        {
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai);

            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center;
            dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 33, 0f, -5f, 0, new Color(33, 0, 255), 2.236842f)];
            dust.noGravity = true;
            dust.noLight = true;
        }
    }
}