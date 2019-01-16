using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.Miscellaneous
{
    class ThrownStake : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.penetrate = 10;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y,  (double)projectile.velocity.X);
            projectile.velocity.X = projectile.velocity.X * 30/31;
            projectile.velocity.Y += 0.6f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch,  Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f,  projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Texture2D trail = mod.GetTexture("Items/Miscellaneous/ThrownStake");
                lightColor = new Color(k * 25,  15,  15);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f,  projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(trail,  drawPos,  null,  color,  projectile.rotation,  drawOrigin,  projectile.scale,  SpriteEffects.None,  0f);
            }
            return true;
        }
    }
}
