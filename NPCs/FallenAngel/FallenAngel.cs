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

namespace Varia.NPCs.FallenAngel
{
    [AutoloadBossHead]
    public class FallenAngel : ModNPC
    {
        Vector2 tPos;
        int despawn = 0;

        int attackTime = 0;
        int attackInterval = 170;

        int gravityTime = 1000;
        
        int turretRingTime = 1000;

        int deathRayTime = 1000;

        int dartTrapTime = 1000;

        bool phase2Yet = false;

        bool vulnerable = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Angel");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 7500 : 10000;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = Main.expertMode ? 2 : 2;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FallenAngel");
            npc.width = 152;
            npc.height = 114;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            Main.npcFrameCount[npc.type] = 7;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax*2*bossLifeScale); // more health in expert for more players
        }
        //Main.netMode !=1 prevents things from happening on the client, over 99% of multiplayer specific bugs are from client server desync
        //Server dessyncs are cause by rng as the server and client can roll different numbers
        // Projectile.NewProjectile() when run on both server and client will cause two projectiles to generate
        NPC forcefieldNPC;
        bool hasPersonalForcefield;
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DarklightEssence"), Main.rand.Next(26, 34)); // darklight essence.... hmmmmmm
                }
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AngelBag"), 1);
            }
            potionType = ItemID.GreaterHealingPotion;
            VariaWorld.downedAngel = true;
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!Main.player[npc.target].dead)
            {
                despawn = 0;
                tPos.X = player.Center.X;
                tPos.Y = player.Center.Y - 70;
                npc.velocity.X += (npc.DirectionTo(tPos).X * Vector2.Distance(npc.Center, tPos) / 1000);
                npc.velocity.Y += (npc.DirectionTo(tPos).Y * Vector2.Distance(npc.Center, tPos) / 1000);
            }
            else
            {
                npc.velocity.Y -= despawn;
                despawn++;
                if (despawn > 40)
                {
                    npc.active = false;
                }
            }

            //if (!vulnerable)
            {
                attackTime++;
                if (attackTime >= attackInterval)
                {
                    switch (Main.rand.Next(1, 5))
                    {
                        case 1:
                            {
                                gravityTime = 0;
                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("TurretCenter"), 0, npc.whoAmI);
                                break;
                            }
                        case 2:
                            {
                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("TurretCenter"), 0, npc.whoAmI);
                                break;
                            }
                        case 3:
                            {
                                deathRayTime = 0;
                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("TurretCenter"), 0, npc.whoAmI);
                                break;
                            }
                        case 4:
                            {
                                dartTrapTime = 0;
                                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("TurretCenter"), 0, npc.whoAmI);
                                break;
                            }
                    }
                    attackInterval = Main.rand.Next(160, 181);
                    //attackTime = 0;
                }
            }

            //Main.NewText(NPC.CountNPCS(mod.NPCType("OrbitingTurret")));
            /*
            
            if (Main.netMode == 1)
            {
                Main.NewText("client: " + npc.velocity);
            }


            if (Main.netMode == 2) // Server
            {
                NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("Server: " + npc.velocity), Color.White);
            }
            */
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.frameCounter >= 3) // ticks per frame
			{
				npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
				npc.frameCounter = 0;
			}
        }
    }
}