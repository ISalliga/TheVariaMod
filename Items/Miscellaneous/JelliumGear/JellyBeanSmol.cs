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

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class JellyBeanSmol : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 20;
            projectile.magic = true;
            projectile.damage = 17;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 10;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smol Jellybean");
        }
        public override void AI()
        {
            if (projectile.timeLeft < 5) projectile.alpha += 50;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}