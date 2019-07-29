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
    public class ChaoticOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 355;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic Orb");
        }
        public override void AI()
        {
            projectile.rotation += 0.1f;
            for (int i = 0; i < 3; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 16, 16, 27, 0f, 0f, 0, new Color(255, 255, 255), 1.842105f)];
                dust.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position - new Vector2(5, 5);
                dust = Main.dust[Terraria.Dust.NewDust(position, 26, 26, 27, -projectile.velocity.X, -projectile.velocity.Y, 0, new Color(255, 255, 255), 1.842105f)];
                dust.noGravity = true;
            }
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 10), projectile.Center);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position - new Vector2(5, 5);
                dust = Main.dust[Terraria.Dust.NewDust(position, 26, 26, 27, -projectile.velocity.X, -projectile.velocity.Y, 0, new Color(255, 255, 255), 1.842105f)];
                dust.noGravity = true;
            }
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 10), projectile.Center);
        }
    }
}