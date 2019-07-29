using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
    public class VenomFangShortRange : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Fang");
        }
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 50;
            projectile.timeLeft = 255;
            projectile.scale = 0.8f;
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.alpha += 9;
            if (projectile.alpha >= 255) projectile.Kill();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 0, 255, projectile.alpha);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 5), projectile.Center);
            target.AddBuff(BuffID.Venom, 60);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 5), projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = projectile.Center;
                    dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 27, 0f, 0f, 0, new Color(255, 0, 201), 1f)];
                    dust.noGravity = true;
                }
            }
        }
    }
}