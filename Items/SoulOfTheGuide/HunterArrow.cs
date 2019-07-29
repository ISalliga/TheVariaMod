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
using BaseMod;

namespace Varia.Items.SoulOfTheGuide
{
	public class HunterArrow : ModProjectile
	{
        bool still = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunter Arrow");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 350;
			projectile.width = 30;
			projectile.height = 30;
			projectile.ignoreWater = true;
			projectile.penetrate = 3;
		}
		public override void AI()
		{
            if (!still)
            {
                projectile.velocity.Y += 0.1f;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            }
            else
            {
                projectile.velocity = Vector2.Zero;
                projectile.alpha += 4;
                if (projectile.alpha >= 255)
                {
                    projectile.active = false;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            still = true;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D trail = mod.GetTexture("Items/SoulOfTheGuide/HunterArrow");
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                lightColor = new Color(k * 10, k * 25, k * 25);
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(trail, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            if (still)
            {
                BaseDrawing.DrawAura(this, trail, 116, projectile, 1f);
            }
            return true;
        }
	}
}