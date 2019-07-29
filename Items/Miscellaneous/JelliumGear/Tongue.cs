using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class Tongue : ModProjectile
    {
        Vector2 mountedCenter;
        int timer = 0;
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.ai[1] = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Varia/Items/Miscellaneous/JelliumGear/Tongue_Tongue");
            mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 position = projectile.Center;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);

                }
            }

            return true;
        }
        public override void AI()
        {
            timer++;
            if (timer > 20)
            {
                projectile.velocity = projectile.DirectionTo(mountedCenter) * 18;
                if (projectile.Distance(mountedCenter) < 20)
                {
                    projectile.active = false;
                }
            }
            else
            {
                projectile.velocity.X *= 0.85f;
                projectile.velocity.Y *= 0.85f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
        }
    }
}