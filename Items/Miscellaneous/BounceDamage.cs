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

namespace Varia.Items.Miscellaneous
{
    public class BounceDamage : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.timeLeft = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bounce Damage");
        }
    }
}