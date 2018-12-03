using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using Varia;
using System.IO;

namespace Varia.NPCs.QueensInfantry
{
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

        int platformsCreated = 0;

        int despawn = 0;
        int attackTimer = 0;
        int attack1Timer = 200;
        int attack2Timer = 200;
        int attack3Timer = 200;

        bool aboutToPlatform = false;
        int dirChangeTimer = 0;
        int dirChangeInterval = Main.rand.Next(20, 71);
        int move = Main.rand.Next(7, 10);
        int dirChanges = 0;

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
                despawn = 0;
            }
            else
            {
                npc.velocity.Y += despawn;
                despawn++;
                if (despawn > 40)
                {
                    npc.active = false;
                }
            }

            if (dirChanges >= 2)
            {
                switch (Main.rand.Next(1, 4))
                {
                    case 1:
                        {
                            attack1Timer = 0;
                            break;
                        }
                    case 2:
                        {
                            attack2Timer = 0;
                            break;
                        }
                    case 3:
                        {
                            if (npc.life <= npc.lifeMax * 0.4f) attack3Timer = 0;
                            else
                            {
                                switch(Main.rand.Next(1, 3))
                                {
                                    case 1:
                                        {
                                            attack1Timer = 0;
                                            break;
                                        }
                                    case 2:
                                        {
                                            attack2Timer = 0;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                }
                dirChanges = 0;
            }

            attack1Timer++;

            if (attack1Timer == 2 || attack1Timer == 8)
            {
                for (int vT = 0; vT < 3; vT++)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (npc.DirectionTo(player.Center).X * 5 * vT / 2) + Main.rand.Next(-8, 9), -29 + vT, mod.ProjectileType("VenomousWaste"), Main.expertMode ? 9 : 14, 0f, player.whoAmI);
                }
            }

            attack2Timer++;

            if (attack2Timer == 5)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("SpiderEgg"));
            }

            attack3Timer++;

            if (attack3Timer == 5 || attack3Timer == 10 || attack3Timer == 15)
            {
                Projectile.NewProjectile(npc.Center, new Vector2((float)Math.Cos((player.Center - npc.Center).ToRotation()), (float)Math.Sin((player.Center - npc.Center).ToRotation())) * 10, ProjectileID.WebSpit, Main.expertMode ? 7 : 10, 0f, player.whoAmI);
            }

            //Movement!

            {
                for (int i = 0; i < 10; i++)
                {
                    if ((Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16 + i)].active() && Main.tileSolid[Main.tile[(int)npc.Center.X / 16, (int)(npc.Center.Y / 16) + i].type]))
                    {
                        dirChangeTimer++;
                        break;
                    }
                    if (npc.collideY)
                    {
                        dirChangeTimer++;
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
                            dirChangeTimer = dirChangeInterval;
                        }
                    }
                }

                if (dirChangeTimer >= dirChangeInterval) npc.velocity.X = 0;
                else npc.velocity.X = npc.direction * move;
                if (dirChangeTimer >= dirChangeInterval + 10)
                {
                    switch(Main.rand.Next(1, 4))
                    {
                        case 1:
                            {
                                move = Main.rand.Next(4, 8);
                                platformsCreated = 0;
                                npc.TargetClosest();
                                break;
                            }
                        case 2:
                            {
                                move = Main.rand.Next(4, 8);
                                npc.TargetClosest();
                                npc.velocity.Y = -7;
                                platformsCreated = 0;
                                break;
                            }
                        case 3:
                            {
                                if (platformsCreated < 2)
                                {
                                    move = Main.rand.Next(3, 6);
                                    npc.TargetClosest();
                                    npc.velocity.Y = -15;
                                    platformsCreated++;
                                    aboutToPlatform = true;
                                }
                                else
                                {
                                    move = Main.rand.Next(4, 8);
                                    npc.TargetClosest();
                                    npc.velocity.Y = -7;
                                    platformsCreated = 0;
                                }
                                break;
                            }
                    }
                    dirChanges++;
                    dirChangeTimer = 0;
                    dirChangeInterval = Main.rand.Next(30, 81);
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
                {
                    switch (Main.rand.Next(1, 5))
                    {
                        case 1:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Arachnophobia"), 1);
                            switch (Main.rand.Next(1, 4))
                            {
                                case 1:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VenomPiercer"), 1);
                                    break;
                                case 2:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RainforestsBane"), 1);
                                    break;
                                case 3:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJaw"), 1);
                                    break;
                            }
                            break;
                        case 2:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VenomPiercer"), 1);
                            switch (Main.rand.Next(1, 4))
                            {
                                case 1:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Arachnophobia"), 1);
                                    break;
                                case 2:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RainforestsBane"), 1);
                                    break;
                                case 3:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJaw"), 1);
                                    break;
                            }
                            break;
                        case 3:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RainforestsBane"), 1);
                            switch (Main.rand.Next(1, 4))
                            {
                                case 1:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VenomPiercer"), 1);
                                    break;
                                case 2:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Arachnophobia"), 1);
                                    break;
                                case 3:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJaw"), 1);
                                    break;
                            }
                            break;
                        case 4:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJaw"), 1);
                            switch (Main.rand.Next(1, 4))
                            {
                                case 1:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VenomPiercer"), 1);
                                    break;
                                case 2:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Arachnophobia"), 1);
                                    break;
                                case 3:
                                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RainforestsBane"), 1);
                                    break;
                            }
                            break;
                    }
                }
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
