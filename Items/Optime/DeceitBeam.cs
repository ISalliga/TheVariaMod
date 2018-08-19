using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Varia.Items.Optime
{
    class DeceitBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            projectile.rotation += 0.3f;
        }
    }
}
