using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace Varia.Items.SoulOfTheGuide
{
    public class LamentingMourgeProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        float rot = 0f;

        public override void AI()
        {
            BaseMod.BaseAI.AIFlail(projectile, ref projectile.ai, false, 200f);
            projectile.direction = projectile.spriteDirection = Main.player[projectile.owner].direction;
            if ((Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) / 2f > 0.52f)
            {
                rot += (float)Math.PI / 12f;
            }
            else { rot *= 0.8f; if (rot < (float)Math.PI / 18f) { rot = 0f; } }
            projectile.rotation += rot;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Varia/Items/SoulOfTheGuide/LamentingMourgeProj_Chain");
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            BaseMod.BaseAI.TileCollideFlail(projectile, ref value2);
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), Vector2.Zero, mod.ProjectileType("FrightSkull"), 63, 0f, projectile.owner);
        }
    }
}
