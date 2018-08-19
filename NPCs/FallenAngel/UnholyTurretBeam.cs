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

namespace Varia.NPCs.FallenAngel
{
    public class UnholyTurretBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
			projectile.alpha = 255;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 1200;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Unholy Beam");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0; 
		}
		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(125, 200, 255, projectile.alpha);
        }
        
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			projectile.alpha -= 16;
		}
    }
}