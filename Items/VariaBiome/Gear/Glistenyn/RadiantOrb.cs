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
using Terraria.Graphics.Shaders;

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
    public class RadiantOrb : ModProjectile
    {
        int bounce = 4;
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 28;
			projectile.magic = true;
			projectile.damage = 17;
            projectile.friendly = true;
            projectile.penetrate = 4;
            projectile.timeLeft = 120;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(254, 254, 254, 50);
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Orb");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void AI()
		{
            projectile.velocity.X = projectile.velocity.X * 15 / 16;
            projectile.velocity.Y += 1.05f;
            
            for (int i = 0; i < projectile.velocity.Y; i++)
            {
                if((Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16 + i)].active() && Main.tileSolid[Main.tile[(int)projectile.Center.X / 16, (int)(projectile.Center.Y / 16) + i].type]))
                {
                    projectile.velocity.Y -= 2.1f;
                }
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Items/VariaBiome/Gear/Glistenyn/RadiantOrb");

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, projectile, auraPercent, 1f, 0f, 0f, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Pink * 6f, Color.Lavender * 6f, Color.SkyBlue * 6f, Color.Pink * 6f));
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile, new Color(254, 254, 254, 50));
            return false;
        }
        public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int i = 0; i < 16; i++)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = projectile.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 181, 0f, 0f, 191, new Color(255, 255, 255), 1.447368f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(89, Main.LocalPlayer);
                }
            }
        }
    }
}