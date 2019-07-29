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
    public class MemorialShockwave : ModProjectile
    {
        int timer = 0;
        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 80;
            projectile.aiStyle = 1;
            projectile.height = 32;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 45;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.SkyBlue;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memorial Shockwave");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center - new Vector2(0, projectile.width / 2);
                if (Main.rand.Next(2) == 0)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(position, 0, 73, 92, 0f, 0f, 0, new Color(255, 255, 255), 1.776316f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(32, Main.LocalPlayer);
                }
            }
            timer++;
            projectile.alpha += 2;
            {
                projectile.velocity.Y += projectile.ai[1] / 10;
                projectile.velocity.X *= 1.04f;
                timer = 0;
            }
            if (projectile.velocity.Y > 5) projectile.velocity.Y = 5;
            if (projectile.velocity.Y < -5) projectile.velocity.Y = -5;
            if (projectile.timeLeft < 10)
            {
                projectile.velocity.X /= 0.915f;
                projectile.velocity.Y *= 0.915f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage *= 8/10;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Texture2D trail = mod.GetTexture("Items/OldWorld/MemorialShockwave");
                lightColor = new Color(255 - (k * 10), 255 - (k * 25), 255 - (k * 30), projectile.alpha);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(trail, drawPos, new Rectangle(0, 0, projectile.width, projectile.height), color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center - new Vector2(0, projectile.width / 2);
                dust = Main.dust[Terraria.Dust.NewDust(position, 0, 73, 92, 0f, 0f, 0, new Color(255, 255, 255), 1.776316f)];
                dust.noGravity = true;
                dust.noLight = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(32, Main.LocalPlayer);
            }

        }
    }
}