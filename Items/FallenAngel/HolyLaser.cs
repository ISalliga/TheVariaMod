﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class HolyLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HolyLaser");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 900;
            projectile.alpha = 60;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, projectile.alpha);
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.alpha > 70)
            {
                projectile.alpha -= 15;
                if (projectile.alpha < 70)
                {
                    projectile.alpha = 70;
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 400f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].immortal && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (10 * projectile.velocity + move) / 11f;
                AdjustMagnitude(ref projectile.velocity);
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 16f)
            {
                vector *= 16f / magnitude;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 drawPosition;
            Texture2D texture = Main.projectileTexture[projectile.type];

            for (int t = 0; t < 66; t++)
            {
                float directionOffset = (float)Math.PI / 2;
                drawPosition = projectile.Center + (new Vector2((float)Math.Cos(projectile.rotation + directionOffset), (float)Math.Sin(projectile.rotation + directionOffset)) * t);
                spriteBatch.Draw(texture, new Vector2(drawPosition.X - Main.screenPosition.X, drawPosition.Y - Main.screenPosition.Y),
                            new Rectangle(0, t, 6, 1), Color.Lerp(new Color(255, 255, 255, 255), new Color(0, 0, 0, 0), (float)t / 66f), projectile.rotation,
                            new Vector2(3, .5f), 1f, SpriteEffects.None, 0f);
            }
            drawPosition = projectile.Center;
            return false;
        }
    }
}

