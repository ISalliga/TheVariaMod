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

namespace Varia.NPCs.Cavity
{
    public class MutantSlimeFleshwave : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 8;
		    projectile.damage = 4;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 20;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fleshwave");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void AI()
		{
			projectile.spriteDirection = projectile.direction;
		}
    }
}