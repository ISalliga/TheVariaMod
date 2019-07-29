using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumWheelProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.aiStyle = 3;
            projectile.penetrate = -1;
            projectile.timeLeft = 9999999;
            projectile.melee = true;
        }
    }
}