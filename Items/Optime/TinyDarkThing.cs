using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime

{
    public class TinyDarkThing : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tiny Dark Thing");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
            projectile.damage = 40;
			projectile.friendly = true;
			projectile.timeLeft = 100;
			projectile.width = 6;
			projectile.height = 6;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}

        public override void AI()
        {
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Terraria.Dust.NewDustPerfect(position, 2, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.907895f);
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);
            }
        }
    }
}