using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using BaseMod;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Varia.Items.Optime
{
    class HappyPillsProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Happy Pill");
        }

        public override void SetDefaults()
        {
            projectile.width = 15;
            projectile.height = 15;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, false, 12, 0.994f, 0.2f, 12);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Insanity"), 300);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}
