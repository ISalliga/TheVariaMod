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
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.StarShower
{
    public class GalacticRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
			projectile.alpha = 255;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 1200;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galactic Rain");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0; 
        }
        
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 226, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 226, 0f, 1.7f, 0, new Color(255, 255, 255), 1f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
            }
            projectile.velocity.Y += 0.07f;
        }
    }
}