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

namespace Varia.Items.FallenAngel.TechHunk
{
	public class LanceSpark : ModProjectile
	{
        int cloneTime = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phaserapier Afterimage");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255,  255,  255,  projectile.alpha);
        }
        public override void SetDefaults()	
		{
            projectile.alpha = 255;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 17;
			projectile.width = 44;
            projectile.tileCollide = false;
			projectile.height = 6;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.alpha = 50;
		}
		public override void AI()
		{
            cloneTime++;
            projectile.alpha -= 40;
            if (cloneTime == 2 && projectile.ai[1] < 6)
            {
                if (projectile.ai[0] == -1)
                {
                    int spark = Projectile.NewProjectile(projectile.Center.X - 36,  projectile.Center.Y,  0,  0,  mod.ProjectileType("LanceSpark"),  projectile.damage - 2,  0f,  Main.myPlayer,  0f,  0f);
                    Main.projectile[spark].ai[1] = projectile.ai[1] + 1;
                    Main.projectile[spark].ai[0] = projectile.ai[0];
                }
                else
                {
                    int spark = Projectile.NewProjectile(projectile.Center.X + 36,  projectile.Center.Y,  0,  0,  mod.ProjectileType("LanceSpark"),  projectile.damage - 2,  0f,  Main.myPlayer,  0f,  0f);
                    Main.projectile[spark].ai[1] = projectile.ai[1] + 1;
                    Main.projectile[spark].ai[0] = projectile.ai[0];
                }
            }
        }
	}
}