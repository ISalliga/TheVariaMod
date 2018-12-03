using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous 
{
    public class DeathwishBagnagProj : ModProjectile
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwish Bagnakhs");
        }

        public override void SetDefaults()
        {
            projectile.width = 68;    
            projectile.height = 60;      
            projectile.friendly = true;    
            projectile.penetrate = -1;  
            projectile.tileCollide = false;
            projectile.ignoreWater = true;  
            projectile.melee = true; 
            projectile.scale = 2f;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }
            projectile.Center = player.MountedCenter;
            projectile.spriteDirection = player.direction;
            projectile.rotation += 1.3f * player.direction;
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) 
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

    }
}