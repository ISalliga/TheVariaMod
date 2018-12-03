using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.QueensInfantry
{
    public class QueensJawProj : ModProjectile
    {
        int slowed = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Queen's Jaw");
		}
        public override void SetDefaults()
        {
            projectile.width = 36; //30
            projectile.height = 36; //30
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 999999;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            aiType = 52;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			if(projectile.alpha == 0)
			{
				return Color.Purple;
			}
			return null;
		}
    }
}