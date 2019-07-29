using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class UnstableMagma : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 30;
            projectile.ranged = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 155, 65);
        }

        public override void AI()
        {
            projectile.velocity.X += Main.rand.Next(-1, 2);
            projectile.velocity.Y += Main.rand.Next(-1, 2);
            if (Main.rand.NextFloat() < 0.3f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(projectile.position, 14, 14, 174, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                dust.noLight = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 7; i++)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(projectile.position, 14, 14, 174, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                dust.noLight = true;
            }
        }
    }
}