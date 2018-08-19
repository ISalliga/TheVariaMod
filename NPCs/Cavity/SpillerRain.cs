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
    public class SpillerRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 2;
		    projectile.damage = 4;
            projectile.hostile = true;
            projectile.penetrate = 5;
            projectile.timeLeft = 300;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fleshy Rain");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
    }
}