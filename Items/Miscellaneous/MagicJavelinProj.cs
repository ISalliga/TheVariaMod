using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using Varia;
using System.IO;

namespace Varia.Items.Miscellaneous
{
    public class MagicJavelinProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Javelin");
        }

        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 5;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, false, 20, 0.995f, 0.18f);
            projectile.rotation = Methods.RotationTo(projectile.Center, projectile.Center + projectile.velocity) - MathHelper.ToRadians(45f);
            if (Main.rand.NextFloat() < 0.1710526f)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(projectile.position, 52, 52, 57, 0f, 0f, 0, new Color(255, 255, 255), 0.7894737f)];
                dust.noGravity = true;
            }

        }

        public override void Kill(int timeLeft)
        {
            Vector2 startPos = projectile.Center - ((projectile.DirectionTo(projectile.Center + projectile.velocity) * 1) * 26);
            for (int i = 0; i < 52 / 4; i++)
            {
                Vector2 pos = startPos + ((projectile.DirectionTo(projectile.Center + projectile.velocity) * 1) * (i * 4));
                for (int o = 0; o < 3; o++)
                {
                    int dust = Dust.NewDust(pos, 0, 0, 57, 0, 0, Scale: 2f);
                    Main.dust[dust].noGravity = true;
                }
            }
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 10), projectile.Center);
        }
    }
}