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

namespace Varia.NPCs.FallenAngel
{
    public class ChargedOrb : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.scale = 1f;
            projectile.width = 96;
            projectile.height = 96;
            projectile.alpha = 0;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 1200;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unholy Beam");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 4;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(125, 200, 255, projectile.alpha);
        }
        public override void AI()
        {
            projectile.alpha += 1;
            if (projectile.alpha >= 127)
            {
                projectile.scale -= 0.0752941176f;
                projectile.alpha += 16;
            }
            if (projectile.alpha >= 255)
            {
                projectile.active = false;
            }
            if (projectile.scale < 1f)
            {
                projectile.scale += 0.003f;
            }
            if (projectile.scale == 0)
            {
                projectile.active = false;
            }
            projectile.velocity.Y += 0.4f;
            FrameCountMeter++;
            if (FrameCountMeter >= 4)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X) projectile.velocity.X = -oldVelocity.X + 2f;
                if (projectile.velocity.Y != oldVelocity.Y) projectile.velocity.Y = -oldVelocity.Y + 2f;
                if (projectile.velocity.Y > 0) projectile.velocity.Y = 0;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Texture2D trail = mod.GetTexture("NPCs/FallenAngel/ChargedOrb");
                lightColor = new Color(255 - (k * 10), 255 - (k * 25), 255 - (k * 30), projectile.alpha);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(trail, drawPos, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}