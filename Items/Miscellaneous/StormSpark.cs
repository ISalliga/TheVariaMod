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

namespace Varia.Items.Miscellaneous
{
    public class StormSpark : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 30;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Spark");
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.08f;
            for (int i = 0; i < 3; i++)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Main.LocalPlayer.Center;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 8, 8, 228, 0f, 0f, 0, new Color(255, 255, 255), (float)projectile.timeLeft / 30f + 0.3f)];
                    dust.noGravity = true;
                }

            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position - new Vector2(5, 5);
                dust = Main.dust[Terraria.Dust.NewDust(position, 8, 8, 228, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6), 0, new Color(255, 255, 255), 2.25f)];
                dust.noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position - new Vector2(5, 5);
                dust = Main.dust[Terraria.Dust.NewDust(position, 8, 8, 228, Main.rand.Next(-5, 6), Main.rand.Next(-5, 6), 0, new Color(255, 255, 255), 2.25f)];
                dust.noGravity = true;
            }
            return true;
        }
    }
}