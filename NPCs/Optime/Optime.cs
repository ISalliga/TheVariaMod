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

namespace Varia.NPCs.Optime
{
    [AutoloadBossHead]
    public class Optime : ModNPC
    {
        int despawn = 0;
        int framerino = 0;

        bool phase2Entered;

        int portal1Timer = 500;
        int portal2Timer = 500;
        bool shockwave = false;

        int attackTimer = 0;

        bool moveOffsetRight = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Optime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[4];
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.width = 78;
            npc.height = 104;
            npc.damage = Main.expertMode ? 35 : 48;
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
            npc.lifeMax = Main.expertMode ? 6900 : 10000;

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
            if (npc.life < npc.lifeMax * 0.5f)
            {
                if (!phase2Entered)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("HisRemnant"), 0, npc.whoAmI);
                    phase2Entered = true;
                }
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!shockwave)
            {
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
                if (npc.ai[2] < 1)
                {
                    attackTimer++;
                }
                if (attackTimer == 111)
                {
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            {
                                portal1Timer = 0;
                                break;
                            }
                        case 2:
                            {
                                portal2Timer = 0;
                                break;
                            }
                        case 3:
                            {
                                shockwave = true;
                                break;
                            }
                    }
                    attackTimer = 0;
                }
                portal1Timer++;
                if (portal1Timer == 20 || portal1Timer == 40 || portal1Timer == 60 || portal1Timer == 80 || portal1Timer == 100 || portal1Timer == 120 || portal1Timer == 140)
                {
                    int damage = Main.expertMode ? 35 : 48;
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-150, 151), npc.Center.Y + Main.rand.Next(-150, 151), 0f, 0f, mod.ProjectileType("OptimePortal"), damage, 0f, Main.myPlayer);
                }
                portal2Timer++;
                if (portal2Timer == 25 || portal2Timer == 50 || portal2Timer == 75 || portal2Timer == 100 || portal2Timer == 125 || portal2Timer == 150 || portal2Timer == 175)
                {
                    {
                        int damage = 0;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(player.Center.X + Main.rand.Next(-50, 50), player.Center.Y + Main.rand.Next(-50, 50), 0f, 0f, mod.ProjectileType("OptimeRift"), damage, 0f, Main.myPlayer);
                        }
                    }
                }
            }
            else
            {
                if (npc.ai[2] == 0)
                {
                    npc.velocity = npc.DirectionFrom(player.Center) * 13;
                }
                npc.ai[1] += 1f;
                if (npc.ai[1] > 60)
                {
                    Vector2 tPos;
                    tPos.X = player.Center.X + Main.rand.Next(-20, 21);
                    tPos.Y = player.Center.Y + Main.rand.Next(-20, 21);
                    npc.velocity = npc.DirectionTo(tPos) * 26;
                    npc.ai[1] = 0;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 20 / 21;
                    npc.velocity.Y = npc.velocity.Y * 20 / 21;
                }
                npc.ai[2]++;
                {
                    if (npc.ai[2] % 2 == 0)
                    {
                        int damage = Main.expertMode ? 30 : 45;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 201), npc.Center.Y + Main.rand.Next(-200, 201), 0f, 0f, mod.ProjectileType("OptimeSplosion"), damage, 0f, Main.myPlayer);
                        }
                    }
                    if (npc.ai[2] > 160)
                    {
                        npc.ai[2] = 0;
                        shockwave = false;
                    }
                }
            }
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y -= 2f;
                despawn++;
                if (despawn > 50)
                {
                    npc.active = false;
                }
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
                int numOfWeapons = 2;
                int weaponPoolCount = 3;
                int[] weaponLoot = new int[numOfWeapons];
                for (int n = 0; n < numOfWeapons; n++)
                {
                    weaponLoot[n] = Main.rand.Next(weaponPoolCount - n);
                    for (int j = 0; j < n; j++)
                    {
                        if (weaponLoot[n] >= weaponLoot[j])
                        {
                            weaponLoot[n]++;
                        }
                        Array.Sort(weaponLoot);
                    }
                }
                for (int i = 0; i < weaponLoot.Length; i++)
                {
                    string dropName = "none";
                    switch (weaponLoot[i])
                    {
                        case 0:
                            dropName = "MrNicey";
                            break;
                        case 1:
                            dropName = "HappyPills";
                            break;
                        case 2:
                            dropName = "GoldenDollar";
                            break;
                    }
                    if (dropName != "none")
                    {
                        Item.NewItem(npc.getRect(), mod.ItemType(dropName));
                    }
                }

                Item.NewItem(npc.getRect(), mod.ItemType("PureConcentratedDarkness"), Main.rand.Next(20, 35));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OptimeBag"), 1);
            }
            potionType = ItemID.GreaterHealingPotion;
            VariaWorld.downedOptime = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Texture2D OptimeThing = mod.GetTexture("NPCs/Optime/Optime_Trail");
                    lightColor = new Color(k * 50, 0, 0);
                    Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    drawPos.Y += 33;
                    Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                    spriteBatch.Draw(OptimeThing, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
    }
}