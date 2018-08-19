using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class MesmerBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mesmer Beam");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 2000;
            projectile.width = 14;
            projectile.height = 28;
            projectile.ignoreWater = true;
            projectile.alpha = 50;
        }
        public override void AI()
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Terraria.Dust.NewDustPerfect(position, 2, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1.907895f);
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-7, 8), Main.rand.Next(-7, 8), mod.ProjectileType("TheDarknessInside"), 7, 0, Main.myPlayer, 0f, 0f);
        }
    }
}