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
    public class StoneArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 14;
            projectile.aiStyle = 1;
            projectile.height = 34;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Arrow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = -(int)oldVelocity.X / 4 + Main.rand.Next(-3, 4);
                int y = -(int)oldVelocity.Y / 4 + Main.rand.Next(-3, 4);
                Projectile.NewProjectile(projectile.Center, new Vector2(x, y), mod.ProjectileType("Pebble"), 15, 0f, projectile.owner);
            }
            projectile.Kill();
            return true;
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