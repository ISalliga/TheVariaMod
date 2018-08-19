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

namespace Varia.Items.Cavity.Cacitian
{
    public class ToxicPuddle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 30;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.timeLeft = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxic Puddle");
        }
        public override void AI()
        {
            projectile.alpha += 1;
        }
    }
}