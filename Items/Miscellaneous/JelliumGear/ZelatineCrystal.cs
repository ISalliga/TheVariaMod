using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Shaders;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear

{
    public class ZelatineCrystal : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zelatine Crystal");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }
        public override void SetDefaults()
		{
            projectile.damage = 40;
			projectile.friendly = true;
			projectile.timeLeft = 90;
            projectile.alpha = 255;
            projectile.melee = true;
            projectile.width = 22;
			projectile.height = 40;
            projectile.scale = 1.4f;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}

        public override void AI()
        {
            projectile.velocity.X *= 1.02f;
            projectile.velocity.Y *= 1.02f;
            if (projectile.timeLeft > 85) projectile.alpha -= 50;
            else if (projectile.timeLeft < 80) projectile.alpha += 3;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}