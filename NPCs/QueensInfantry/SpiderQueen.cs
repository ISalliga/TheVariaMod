using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Varia;
using System.IO;
using System;

namespace Varia.NPCs.QueensInfantry
{
    [AutoloadBossHead]
    public class SpiderQueen : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spider Queen");
        }

        public override bool CheckActive()
        {
            if (!Main.player[npc.target].dead) return false;
            else return true;
        }

        public int[] customAI = new int[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
                writer.Write((short)customAI[4]);
                writer.Write((short)customAI[5]);
            }
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadInt16();
                customAI[1] = reader.ReadInt16();
                customAI[2] = reader.ReadInt16();
                customAI[3] = reader.ReadInt16();
                customAI[4] = reader.ReadInt16();
                customAI[5] = reader.ReadInt16();
            }
        }

        bool aboutToPlatform = false;

        public override void SetDefaults()
        {
            npc.width = 140;
            npc.height = 106;
            npc.damage = 30;
            npc.defense = 0;
            npc.boss = true;
            npc.HitSound = SoundID.NPCHit29;
            npc.DeathSound = SoundID.NPCDeath31;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = -1;
            npc.noGravity = false;
            npc.noTileCollide = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/QueensInfantry");
            npc.lifeMax = Main.expertMode ? 750 : 1500;
            npc.buffImmune[BuffID.Venom] = true;
            Main.npcFrameCount[npc.type] = 8;

            npc.ai[0] = 0; //Despawn

            npc.ai[1] = 0; //Platforms created before cooldown

            npc.ai[2] = 200; //Attack 1
            npc.ai[3] = 200; //Attack 2
            customAI[0] = 200;

            customAI[1] = 0; //Directional change timer
            customAI[2] = 40; //Directional change interval
            customAI[3] = 8; //Movement speed
            customAI[4] = 0; //Directional changes until attack
            customAI[5] = 0; //Workaround variable for the infamous freeze glitch
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 2 * bossLifeScale); // more health in expert for more players
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!Main.player[npc.target].dead)
            {
                npc.ai[0] = 0;
            }
            else
            {
                npc.velocity.Y += npc.ai[0];
                npc.ai[0]++;
                if (npc.ai[0] > 40)
                {
                    npc.active = false;
                }
            }

            if (customAI[4] >= 2)
            {
                switch (Main.rand.Next(1, 4))
                {
                    case 1:
                        {
                            npc.ai[2] = 0;
                            break;
                        }
                    case 2:
                        {
                            npc.ai[3] = 0;
                            break;
                        }
                    case 3:
                        {
                            if (npc.life <= npc.lifeMax * 0.4f) customAI[0] = 0;
                            else
                            {
                                switch (Main.rand.Next(1, 3))
                                {
                                    case 1:
                                        {
                                            npc.ai[2] = 0;
                                            break;
                                        }
                                    case 2:
                                        {
                                            npc.ai[3] = 0;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                }
                customAI[4] = 0;
            }

            npc.ai[2]++;

            if (npc.ai[2] == 2 || npc.ai[2] == 8)
            {
                for (int vT = 0; vT < 3; vT++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (npc.DirectionTo(player.Center).X * 5 * vT / 2) + Main.rand.Next(-8, 9), -29 + vT, mod.ProjectileType("VenomousWaste"), Main.expertMode ? 9 : 14, 0f, player.whoAmI);
                }
            }

            npc.ai[3]++;

            if (npc.ai[3] == 5)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SpiderEgg"));
            }

            customAI[0]++;

            if (customAI[0] == 5 || customAI[0] == 10 || customAI[0] == 15)
            {
                Projectile.NewProjectile(npc.Center, new Vector2((float)Math.Cos((player.Center - npc.Center).ToRotation()), (float)Math.Sin((player.Center - npc.Center).ToRotation())) * 10, ProjectileID.WebSpit, Main.expertMode ? 7 : 10, 0f, player.whoAmI);
            }

            //Movement!

            {
                for (int i = 0; i < 10; i++)
                {
                    if ((Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16 + i)].active() && Main.tileSolid[Main.tile[(int)npc.Center.X / 16, (int)(npc.Center.Y / 16) + i].type]))
                    {
                        customAI[1]++;
                        break;
                    }
                    if (npc.collideY)
                    {
                        customAI[1]++;
                        break;
                    }
                }

                if (aboutToPlatform && npc.velocity.Y > 3)
                {
                    for (int i = -5; i <= 5; i++)
                    {
                        if (!Main.tile[(int)(npc.Center.X / 16 + i), (int)(npc.position.Y / 16 + 10)].active())
                        {
                            WorldGen.PlaceTile((int)(npc.Center.X / 16 + i), (int)(npc.position.Y / 16 + 10), mod.TileType("SturdyVenom"));
                            aboutToPlatform = false;
                            customAI[1] = customAI[2];
                        }
                    }
                }

                if (customAI[1] >= customAI[2]) npc.velocity.X = 0;
                else npc.velocity.X = npc.direction * customAI[3];
                if (customAI[1] >= customAI[2] + 10)
                {
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            {
                                customAI[3] = Main.rand.Next(4, 8);
                                npc.ai[1] = 0;
                                npc.TargetClosest();
                                break;
                            }
                        case 2:
                            {
                                customAI[3] = Main.rand.Next(4, 8);
                                npc.TargetClosest();
                                npc.velocity.Y = -7;
                                npc.ai[1] = 0;
                                break;
                            }
                        case 3:
                            {
                                if (npc.ai[1] < 2)
                                {
                                    customAI[3] = Main.rand.Next(3, 6);
                                    npc.TargetClosest();
                                    npc.velocity.Y = -15;
                                    npc.ai[1]++;
                                    aboutToPlatform = true;
                                }
                                else
                                {
                                    customAI[3] = Main.rand.Next(4, 8);
                                    npc.TargetClosest();
                                    npc.velocity.Y = -7;
                                    npc.ai[1] = 0;
                                }
                                break;
                            }
                    }
                    customAI[4]++;
                    customAI[1] = 0;
                    customAI[2] = Main.rand.Next(30, 81);
                }
                if (npc.collideX)
                {
                    npc.velocity.Y = -2;
                }
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            /*for (int yourstyle = 0; yourstyle < Main.maxTilesX; yourstyle++) //The variable yourstyle is the X.
            {
                for (int isbad = 0; isbad < Main.maxTilesY; isbad++) //The variable isbad is the y.
                {
                    if (Main.tile[yourstyle, isbad].type == mod.TileType("SturdyVenom"))
                    {
                        WorldGen.KillTile(yourstyle, isbad);
                    }
                }
            }*/
            if (!Main.expertMode)
            {
                int numOfWeapons = 2;
                int weaponPoolCount = 4;
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
                            dropName = "RainforestsBane";
                            break;
                        case 1:
                            dropName = "QueensJaw";
                            break;
                        case 2:
                            dropName = "VenomPiercer";
                            break;
                        case 3:
                            dropName = "Arachnophobia";
                            break;
                    }
                    if (dropName != "none")
                    {
                        Item.NewItem(npc.getRect(), mod.ItemType(dropName));
                    }
                }
                Item.NewItem(npc.getRect(), mod.ItemType("SpiderFlesh"), Main.rand.Next(20, 31));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueenBag"), 1);
            }
            potionType = ItemID.LesserHealingPotion;
            VariaWorld.downedSpoderQueen = true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;

            if (!npc.collideY)
            {
                npc.frame.Y = frameHeight * 6;
            }
            else if (npc.velocity.X == 0)
            {
                npc.frame.Y = frameHeight * 7;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 2)
                {
                    npc.frame.Y = (npc.frame.Y / frameHeight + 1) % 6 * frameHeight;
                    npc.frameCounter = 0;
                }
            }
        }
    }
}
