using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Achievements;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Items.Optime
{
    class NegativeEnergyField : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 150;
            projectile.height = 150;
            projectile.friendly = true;
            projectile.penetrate = 10;
            projectile.tileCollide = false;
            projectile.timeLeft = 200;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.X = 0;
            target.velocity.Y = 0;
        }

        public override void AI()
        {
            for (int i = 0; i < projectile.width / 8; i++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, 0f, 0f, 100, new Color(255, 255, 255, 150), 1f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 2f;
                Main.dust[num624].shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);
            }
        }
    }
}
