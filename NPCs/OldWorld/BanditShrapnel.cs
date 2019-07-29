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
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.OldWorld
{
    public class BanditShrapnel : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 30;
            projectile.height = 30;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 10;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bandit's Shrapnel");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 6, 0f, 0f, 0, new Color(75, 155, 255), 2.1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int numDust = 0; numDust < 8; numDust++)
            {
                {
                    Dust dust;
                    Vector2 position = projectile.position;
                    dust = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, 6, 0f, 0f, 0, new Color(75, 155, 255), 2.9f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                }
            }
        }
    }
}