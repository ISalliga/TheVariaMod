using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.SoulOfTheGuide
{
    class SoulFire : ModProjectile
    {
        Vector2 startPos;
        NPC parent;
        float dist = 1f;
        int startTimer = 0;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(125, 200, 255, projectile.alpha);
        }
        public override void SetDefaults()
        {
            startPos = projectile.Center;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.width = 8;
            projectile.height = 8;
            projectile.hostile = true;
            projectile.penetrate = 10;
            projectile.tileCollide = false;
            projectile.timeLeft = 235;
        }

        float ang;
        float speed = 4;
        Vector2 DirectionVelocity;
        public override void AI()
        {
            ang += (float)Math.PI / 30;
            projectile.velocity = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * speed;
            projectile.velocity += projectile.DirectionTo(Main.player[Player.FindClosest(projectile.position, 300, 300)].Center) * 4;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Texture2D trail = mod.GetTexture("NPCs/SoulOfTheGuide/SoulFire");
                lightColor = new Color(255 - (k * 10), 255 - (k * 25), 255 - (k * 30), projectile.alpha);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(trail, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
