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
    public class MagicBoulder : ModProjectile
    {
        bool hasLanded = false;
        public override void SetDefaults()
        {
            projectile.alpha = 255;
            projectile.width = 38;
            projectile.height = 30;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Boulder");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            float rot = projectile.velocity.Y + projectile.velocity.X * 0.000000000005f;
            if (rot >= 0.15f) rot = 0.15f;
            projectile.rotation += rot;
            if (projectile.alpha <= 0) projectile.velocity.Y += 0.3f;
            if (projectile.alpha > 0) projectile.alpha -= 50;
            else projectile.alpha = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.position, 1);
            for (int numDust = 0; numDust < 25; numDust++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 30, 30, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.052632f)];
            }
        }
    }
}