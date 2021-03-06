using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.Optime
{
    [AutoloadBossHead]
    public class NiceGuy : ModNPC
    {
        public int[] customAI = new int[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
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
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nice Guy");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 12000 : 20000;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 90;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/NiceGuy");
            npc.width = 64;
            npc.height = 106;
            npc.alpha = 0;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 11, 0, 0);
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            Main.npcFrameCount[npc.type] = 6;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Boss/NiceGuy_Hit");

            npc.ai[0] = 0; //Despawn
            npc.ai[1] = 0; //Shoot Time
            npc.ai[2] = 165; //Shoot Interval
            npc.ai[3] = 400; //Orb Time
            customAI[0] = 400; //Teleportation Time
            customAI[1] = 400; //Crosshair Time
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax*bossLifeScale);
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
            npc.ai[1]++;
            if (npc.ai[1] >= npc.ai[2])
            {
                switch(Main.rand.Next(1, 5))
                {
                    case 1:
                        {
                            if (NPC.CountNPCS(mod.NPCType("TopHat")) < 3)
                            for (int i = 0; i < Main.rand.Next(3, 6); i++)
                            {
                                int hat = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("TopHat"), 0);
                                Main.npc[hat].velocity.X = Main.rand.Next(-20, 21);
                                Main.npc[hat].velocity.Y = Main.rand.Next(-20, 0);
                            }
                            break;
                        }
                    case 2:
                        {
                            customAI[0] = 39;
                            break;
                        }
                    case 3:
                        {
                            npc.ai[3] = 0;
                            break;
                        }
                    case 4:
                        {
                            customAI[1] = 0;
                            break;
                        }
                }
                npc.ai[1] = 0;
                npc.ai[2] = Main.rand.Next(190, 251);
            }
            npc.ai[3]++;
            if (npc.ai[3] == 50 || npc.ai[3] == 70 || npc.ai[3] == 100 || npc.ai[3] == 140 || npc.ai[3] == 190)
            {
                {
                    float Speed = 14f;
                    int damage = Main.expertMode ? 25 : 42;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center) * (Speed + Main.rand.Next(-5, 5)), mod.ProjectileType("HappyOrb"), damage, 0f, Main.myPlayer);
                    }
                }
            }
            customAI[1]++;
            if (customAI[1] == 50 || customAI[1] == 100 || customAI[1] == 150 || customAI[1] == 200 || customAI[1] == 250)
            {
                {
                    int damage = 0;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(player.Center.X + Main.rand.Next(-50, 50), player.Center.Y + Main.rand.Next(-50, 50), 0f, 0f, mod.ProjectileType("HappyCrosshair"), damage, 0f, Main.myPlayer);
                    }
                }
            }
            customAI[0]++;
            if (customAI[0] == 40 || customAI[0] == 80 || customAI[0] == 120 || customAI[0] == 160 || customAI[0] == 200)
            {
                switch (player.position.X > npc.position.X)
                {
                    case true:
                        {
                            for (int i = 0; i < 40; i++)
                            {
                                Dust dust;
                                Vector2 position = npc.position;
                                dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 218, 0f, 0f, 0, new Color(255, 255, 255), 3.815789f)];
                                dust.noGravity = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(61, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                            }
                            npc.position.X += npc.Distance(player.Center) * 0.4f;
                            for (int i = 0; i < 19; i++)
                            {
                                if (Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16) + 1].active() && Main.tileSolid[Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16) + 1].type]) npc.position.Y -= 16;
                            }
                            for (int i = 0; i < 40; i++)
                            {
                                Dust dust;
                                Vector2 position = npc.position;
                                dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 218, 0f, 0f, 0, new Color(255, 255, 255), 3.815789f)];
                                dust.noGravity = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(61, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                            }
                            break;
                        }
                    case false:
                        {
                            for (int i = 0; i < 40; i++)
                            {
                                Dust dust;
                                Vector2 position = npc.position;
                                dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 218, 0f, 0f, 0, new Color(255, 255, 255), 3.815789f)];
                                dust.noGravity = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(61, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                            }
                            npc.position.X -= npc.Distance(player.Center) * 0.4f;
                            for (int i = 0; i < 19; i++)
                            {
                                if (Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16) + 1].active() && Main.tileSolid[Main.tile[(int)(npc.Center.X / 16), (int)(npc.Center.Y / 16) + 1].type]) npc.position.Y -= 16;
                            }
                            for (int i = 0; i < 40; i++)
                            {
                                Dust dust;
                                Vector2 position = npc.position;
                                dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 218, 0f, 0f, 0, new Color(255, 255, 255), 3.815789f)];
                                dust.noGravity = true;
                                dust.shader = GameShaders.Armor.GetSecondaryShader(61, Main.LocalPlayer);
                                dust.fadeIn = 3f;
                            }
                            break;
                        }
                }
            }
        }
        public override bool CheckDead()
        {
            npc.active = false;
            int optime = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Optime"));
            Main.npc[optime].velocity.Y = -25;
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 0;
            npc.rotation = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 5) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }
    }
}