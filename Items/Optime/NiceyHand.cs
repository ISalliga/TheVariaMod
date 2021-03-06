using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class NiceyHand : ModProjectile
    {
        Vector2 mountedCenter;
        int timer = 0;
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.minion = true;
            projectile.minionSlots = 0;
            projectile.ai[1] = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Varia/Items/Optime/NiceyArm");
            if (projectile.ai[1] < 1)
            {
                mountedCenter = projectile.Center;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                projectile.ai[1]++;
            }
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
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 ebip = mountedCenter;
                dust = Main.dust[Terraria.Dust.NewDust(ebip, 0, 0, 109, 0f, 0f, 0, new Color(0, 92, 255), 2.171053f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
            }

            return true;
        }
        public override void AI()
        {
            timer++;
            if (timer > 20)
            {
                projectile.velocity = projectile.DirectionTo(mountedCenter) * 19;
                if (projectile.Distance(mountedCenter) < 20)
                {
                    projectile.active = false;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust;
                Vector2 ebip = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(ebip, projectile.width, projectile.height, 109, 0f, 0f, 0, new Color(0, 92, 255), 2.171053f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
            }
            return true;
        }
    }
}