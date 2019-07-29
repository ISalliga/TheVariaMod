using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.Miscellaneous
{
	public class Skelehand : ModProjectile
	{
        NPC target;
        int yOffset = 0;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skelehand");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
		{
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 22;
            projectile.height = 34;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			return true;
		}

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            bool isTargetingNPC = false;

            if (!player.HasBuff(mod.BuffType("SkelehandBuff"))) projectile.Kill();
            
            if (Methods.ClosestNPC(ref target, 500, player.Center))
            {
                if (projectile.ai[0] > 0 && target.Center.X > player.Center.X + 10) isTargetingNPC = true;
                if (projectile.ai[0] < 0 && target.Center.X < player.Center.X - 10) isTargetingNPC = true;
            }

            projectile.ai[1]++;

            if (projectile.ai[1] > 60)
            {
                if (!isTargetingNPC)
                {
                    yOffset = Main.rand.Next(-50, 51);
                    if (projectile.ai[0] < 0) projectile.ai[0] = Main.rand.Next(-100, -49);
                    if (projectile.ai[0] > 0) projectile.ai[0] = Main.rand.Next(50, 101);
                }
                else
                {
                    yOffset = (int)target.Center.Y - (int)player.Center.Y;
                    projectile.ai[0] = (int)target.Center.X - (int)player.Center.X;
                }
                projectile.ai[1] = 0;
            }

            projectile.rotation = 0;
            projectile.rotation += projectile.velocity.X * 0.01f;
            if (projectile.rotation > 0.5f) projectile.rotation = 0.5f;
            if (projectile.rotation < -0.5f) projectile.rotation = -0.5f;

            Vector2 flyTo;
            flyTo = Main.player[projectile.owner].Center + new Vector2(projectile.ai[0], yOffset);
            
            projectile.velocity = projectile.DirectionTo(flyTo) * projectile.Distance(flyTo) / 10;
            if (projectile.velocity.X >= 12) projectile.velocity.X = 12;
            if (projectile.velocity.X <= -12) projectile.velocity.X = -12;
            if (projectile.velocity.Y >= 12) projectile.velocity.Y = 12;
            if (projectile.velocity.Y <= -12) projectile.velocity.Y = -12;
            projectile.rotation = Methods.RotationTo(Main.player[projectile.owner].Center, projectile.Center);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X * .75f;
            projectile.velocity.Y = -projectile.velocity.Y * .75f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("Varia/Items/Misc/Skelehand_Chain");
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
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}