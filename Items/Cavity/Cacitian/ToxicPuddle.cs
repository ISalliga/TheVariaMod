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

namespace Varia.Items.Cavity.Cacitian
{
    public class ToxicPuddle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 30;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.timeLeft = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Puddle");
        }
        public override void AI()
        {
            projectile.alpha += 1;
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X, position.Y + 30), 38, 8, 33, 0f, -5.631579f, 0, new Color(255, 226, 0), 1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(53, Main.LocalPlayer);
            }

            if (Main.rand.NextFloat() < 0.5f)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X, position.Y + 30), 38, 8, 36, 0f, -2.631579f, 0, new Color(255, 226, 0), 0.3947368f)];
                dust.shader = GameShaders.Armor.GetSecondaryShader(53, Main.LocalPlayer);
            }
        }
    }
}