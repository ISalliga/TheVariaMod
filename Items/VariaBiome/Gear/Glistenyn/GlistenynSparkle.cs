using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
    class GlistenynSparkle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glistenyn Sparkle");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.timeLeft = 12;
            projectile.scale = 0.5f;
        }
        public override void AI()
        {
            projectile.velocity.X = projectile.velocity.X * 20 / 21;
            projectile.velocity.Y = projectile.velocity.Y * 30 / 31 + 0.8f;

            Dust dust;
            Vector2 position = projectile.Center;
            dust = Terraria.Dust.NewDustDirect(position, 0, 0, 213, 0f, 0f, 0, new Color(255, 255, 255), 5f);
            dust.noGravity = true;
            dust.shader = GameShaders.Armor.GetSecondaryShader(16, Main.LocalPlayer);
        }
    }
}
