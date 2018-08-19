using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
	public class TheDarknessInside : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Darkness Inside");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.timeLeft = 200;
			projectile.aiStyle = 66;
			projectile.width = 47;
			projectile.height = 34;
			projectile.ignoreWater = true;
			projectile.penetrate = 999;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}
		
		public override void AI()
        {
			projectile.spriteDirection = projectile.direction;
            for (int i = 0; i < 45; i++)
            {
                NPC target = Main.npc[i];

                //Getting the shooting trajectory
                float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + (float)target.height * 0.5f - projectile.Center.Y;
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                //If the distance between the projectile and the live target is active
                if (distance < 520f && !target.friendly && target.active)  //distance < 520 this is the projectile1 distance from the target if the tarhet is in that range the this projectile1 will shot the projectile2
                {
                    if (projectile.ai[0] > 10f)//this make so the projectile1 shoot a projectile every 2 seconds(60 = 1 second so 120 = 2 seconds)
                    {
                        //Dividing the factor of 2f which is the desired velocity by distance
                        distance = 1.6f / distance;

                        //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;
                        int damage = 46;  //this is the projectile2 damage                  
                        int num57 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX, shootToY, mod.ProjectileType("TinyDarkThing"), damage, 0, Main.myPlayer, 0f, 0f);
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 24); 
                        projectile.ai[0] = 0f;
                    }
                }
            }
            projectile.ai[0] += 1f;
        }
	}
}