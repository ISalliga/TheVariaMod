using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.Optime
{
    public class Optime : ModNPC
    {
        int despawn = 0;
        int framerino = 0;

        int attackTimer = 0;
        int spread1timer = 150;
        int spread2timer = 111;
        int rapidTimer = 151;
        int counterTimer = 151;

        bool moveOffsetRight = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Optime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[4];
        }

        public override void SetDefaults()
        {
            npc.width = 78;
            npc.height = 104;
            npc.damage = 999999;
            npc.defense = 8;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Optime");
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.aiStyle = -1;
            npc.lifeMax = Main.expertMode ? 3000 : 5000;

        }
        int useJitterMethod = 1; //set this to 1 for method 1 set it to 2 for method 2
        //variables used for both methods
        Vector2 moveTo;
        float flyDirection;
        //settings for both methods
        float heightAbovePlayer = 350;
        //variables used for jitter method 1

        //settings for jitter method 1
        float acceleration = 2f; // acceleration rate
        float maxSpeed = 18f; //max speed


        //variables used for jitter method 2
        float currentFlyDirection;
        //settings for jitter method 2
        float speed = 24; //how fast it moves
        float rotationSpeed = 20; //rotation speed in degrees per frame
        public override void AI()
        {
            if (NPC.CountNPCS(mod.NPCType("OrbitingMask")) == 0) npc.dontTakeDamage = false;
            else npc.dontTakeDamage = true;

            //look at stuff.cs for information on the SlowRotation and PolarVector
            Player player = Main.player[npc.target];
            if (Main.player[npc.target].dead)
            {
                npc.position.Y += 5;
                despawn++;
                if (despawn > 50)
                {
                    npc.active = false;
                }
            }

            npc.TargetClosest(true);
            moveTo = new Vector2(player.Center.X, player.Center.Y - heightAbovePlayer);

            flyDirection = (moveTo - npc.Center).ToRotation();

            if (useJitterMethod == 1)//jitter method 1 Acceleration
            {

                npc.velocity += Methods.PolarVector(acceleration, flyDirection);  //increase the npc's velocity toward flyDirection
                                                                                  //check iif the npc is moving to fast and slow it down if it is
                if (npc.velocity.Length() > maxSpeed)
                {
                    npc.velocity = npc.velocity.SafeNormalize(-Vector2.UnitY) * maxSpeed;
                }
            }

            if (useJitterMethod == 2) //jitter method 2 slowed direction change
            {


                currentFlyDirection = Methods.SlowRotation(currentFlyDirection, flyDirection, rotationSpeed); // takes the angle curretFlyDirection and moves it toward the flyDirection angle by rotationSpeed degrees
                npc.velocity = Methods.PolarVector(speed, currentFlyDirection); //move the npc in the desired direction and speed

            }

            attackTimer++;
            if (attackTimer == 105)
            {
                switch (Main.rand.Next(1, 5))
                {
                    case 1:
                        {
                            spread1timer = 0;
                            break;
                        }
                    case 2:
                        {
                            spread2timer = 0;
                            break;
                        }
                    case 3:
                        {
                            rapidTimer = 0;
                            break;
                        }
                    case 4:
                        {
                            counterTimer = 0;
                            break;
                        }
                }
                attackTimer = 0;
            }
            spread1timer++;
            if (spread1timer == 30 || spread1timer == 60 || spread1timer == 90 || spread1timer == 120 || spread1timer == 150)
            {
                for (int i = 0; i < 10; i++)
                {
                    float Speed = 12f;
                    int type = mod.ProjectileType("Happifier");
                    float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(npc.Center.Y - 10, npc.Center.X - 10));
                    int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, 999999, 0f, 0);
                    Main.projectile[proj].tileCollide = false;
                }
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 72), npc.Center);
            }
            spread2timer++;
            if (spread2timer == 30 || spread2timer == 60 || spread2timer == 90 || spread2timer == 120 || spread2timer == 150)
            {
                for (int i = 0; i < 6; i++)
                {
                    float Speed = 15;
                    int type = mod.ProjectileType("Happifier");
                    int proj = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1920, 1920), player.Center.Y - 1080, 0, -speed, type, 999999, 0f, 0);
                    int proj2 = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1920, 1920), player.Center.Y + 1080, 0, -speed, type, 999999, 0f, 0);
                    Main.projectile[proj].tileCollide = false;
                    Main.projectile[proj2].tileCollide = false;
                }
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 42), npc.Center);
            }
            rapidTimer++;
            if (rapidTimer == 10 || rapidTimer == 20 || rapidTimer == 30 || rapidTimer == 40 || rapidTimer == 70 || rapidTimer == 80 || rapidTimer == 90 || rapidTimer == 100)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 traj = player.Center;
                    traj.X += Main.rand.Next(-20, 20);
                    traj.Y += Main.rand.Next(-20, 20);
                    float Speed = 12f;
                    int type = mod.ProjectileType("Happifier");
                    int proj = Projectile.NewProjectile(npc.Center, npc.DirectionTo(traj) * 15, type, 999999, 0f, 0);
                    Main.projectile[proj].tileCollide = false;
                }
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 72), npc.Center);
            }
            counterTimer++;
            if (counterTimer == 5 || counterTimer == 10 || counterTimer == 15 || counterTimer == 20 || counterTimer == 25)
            {
                Projectile.NewProjectile(new Vector2(player.Center.X - 1920, player.Center.Y), npc.DirectionTo(new Vector2(npc.Center.X + 20, npc.Center.Y)) * 20, mod.ProjectileType("Happifier"), 999999, 0.4f);
                Projectile.NewProjectile(new Vector2(player.Center.X + 1920, player.Center.Y), npc.DirectionTo(new Vector2(npc.Center.X - 20, npc.Center.Y)) * 20, mod.ProjectileType("Happifier"), 999999, 0.4f);
                Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y - 1080), npc.DirectionTo(new Vector2(npc.Center.X, npc.Center.Y + 20)) * 20, mod.ProjectileType("Happifier"), 999999, 0.4f);
                Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y + 1080), npc.DirectionTo(new Vector2(npc.Center.X, npc.Center.Y - 20)) * 20, mod.ProjectileType("Happifier"), 999999, 0.4f);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Height = 104;
            npc.spriteDirection = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 4) // ticks per frame
            {
                npc.frame.Y += 104;
                framerino += 1;
                npc.frameCounter = 0;
            }
            if (framerino >= 4)
            {
                npc.frame.Y = 0;
                framerino = 0;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PureConcentratedDarkness"), Main.rand.Next(25, 31)); // darklight essence.... hmmmmmm
                }
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OptimeBag"), 1);
            }
            potionType = ItemID.GreaterHealingPotion;
            VariaWorld.downedOptime = true;
        }
    }
}