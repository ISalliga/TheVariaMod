using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide

{
    public class TheOmniscient: ModProjectile
    {

        public override void SetDefaults()
        {

            projectile.width = 66; //Set the hitbox width
            projectile.height = 62;   //Set the hitbox heinght
            projectile.hostile = false;    //tells the game if is hostile or not.
            projectile.friendly = false;   //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = true;    //Tells the game whether or not projectile will be affected by water
            Main.projFrames[projectile.type] = 1;  //this is where you add how many frames u'r projectile has to make the animation
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed  -1 is infinity
            projectile.tileCollide = true; //Tells the game whether or not it can collide with tiles/ terrain
            projectile.sentry = true; //tells the game that this is a sentry
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, projectile.Center, 62);
        }

        public override void AI()
        {
            //---------------------------------------------------This make this projectile1 shot another projectile2 to a target if is in between the distance and this projectile1 ------------------------------------------------------------------------


            //Getting the npc to fire at
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                //Getting the shooting trajectory
                float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + (float)target.height * 0.5f - projectile.Center.Y;
                float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                //If the distance between the projectile and the live target is active
                if (distance < 520f && !target.friendly && target.active)  //distance < 520 this is the projectile1 distance from the target if the tarhet is in that range the this projectile1 will shot the projectile2
                {
                    if (projectile.ai[0] > 40f)
                    {
                        //Dividing the factor of 2f which is the desired velocity by distance
                        distance = 1.6f / distance;

                        //Multiplying the shoot trajectory with distance times a multiplier if you so choose to
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;
                        shootToX /= 3;
                        int damage = 25;  //this is the projectile's damage       
                        for (int w = 0; w < 3; w++)
                        {
                            int num57 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX * 1.4f + Main.rand.Next(-2, 3), shootToY * 1.4f + Main.rand.Next(-2, 3), ProjectileID.DeathLaser, damage, 0, Main.myPlayer, 0f, 0f);
                            Main.projectile[num57].friendly = true;
                            Main.projectile[num57].hostile = false;
                            Main.projectile[num57].penetrate = 1;
                        }
                        for (int e = 0; e < 6; e++)
                        {
                            int num59 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -shootToX + Main.rand.Next(-2, 3), -shootToY + Main.rand.Next(-2, 3), ProjectileID.EyeFire, damage, 0, Main.myPlayer, 0f, 0f);
                            Main.projectile[num59].friendly = true;
                            Main.projectile[num59].hostile = false;
                            Main.projectile[num59].penetrate = 1;
                        }
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 24); 
                        projectile.ai[0] = 0f;

                        projectile.rotation = projectile.AngleTo(new Vector2(shootToX, shootToY));
                    }
                }
            }
            projectile.ai[0] += 1f;
        }
    }
}