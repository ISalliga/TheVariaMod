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

namespace Varia.Items.OldWorld
{
    public class Pebble : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 12;
            projectile.aiStyle = 1;
            projectile.height = 10;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 220;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pebble");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.91f;
            projectile.velocity.Y += 0.15f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity.X = 0;
            projectile.velocity.Y = 0;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.position, 1);
            for (int numDust = 0; numDust < 5; numDust++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.052632f)];
            }
        }
    }
}