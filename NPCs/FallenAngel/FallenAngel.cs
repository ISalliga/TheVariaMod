using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.FallenAngel
{
    [AutoloadBossHead]
    public class FallenAngel : ModNPC
    {
        Vector2 tPos;
        bool vc1 = false;
        bool vc2 = false;
        bool vc3 = false;
        int despawn = 0;
        int turretTime = 0;
        int stationaryTurretTime = 0;
        int chargedOrbTime = 0;
        int forcefieldTime = 0;
        bool forcefield = false;

        bool clonesSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Angel");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 7500 : 10000;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = Main.expertMode ? 2 : 2;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FallenAngel");
            npc.width = 152;
            npc.height = 114;
            npc.noGravity = true;
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
                int numOfWeapons = Main.rand.Next(1, 3);
                int weaponPoolCount = 2;
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
                            dropName = "VarianWings";
                            break;
                        case 1:
                            dropName = "GuardiansValor";
                            break;
                    }
                    if (dropName != "none")
                    {
                        Item.NewItem(npc.getRect(), mod.ItemType(dropName));
                    }
                }

                Item.NewItem(npc.getRect(), mod.ItemType("DarklightEssence"), Main.rand.Next(19, 27));
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
                BaseAI.AISpaceOctopus(npc, ref npc.ai, 0.25f, 19f, 275);
                if (npc.rotation > 0.8f) npc.rotation = 0.8f;
                if (npc.rotation < -0.8f) npc.rotation = -0.8f;
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

            turretTime++;
            if (turretTime == 1 )
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrbitingTurret"), 0, npc.whoAmI);
            }
            if (turretTime == 61 )
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrbitingTurret"), 0, npc.whoAmI);
            } 
            if (turretTime == 121)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrbitingTurret"), 0, npc.whoAmI);
            }
            if (!clonesSpawned)
            {
                stationaryTurretTime++;
            }
            if (stationaryTurretTime == 720)
            {
                NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-1, 2) * 50, (int)npc.Center.Y + Main.rand.Next(-1, 2) * 50, mod.NPCType("UnholyTurret"), 0, npc.whoAmI);
                stationaryTurretTime = 0;
            }

            chargedOrbTime++;
            if (chargedOrbTime == 65)
            {
                if (Main.rand.Next(1, 4) == 1 && Main.netMode !=1)
                {
                    if (!clonesSpawned)
                    {
                        float Speed = 10f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                        int damage = Main.expertMode ? 25 : 42;
                        int type = mod.ProjectileType("ChargedOrb");
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            int damage = Main.expertMode ? 25 : 42;
                            int type = mod.ProjectileType("ChargedOrb");
                            int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11), type, damage, 0f, Main.myPlayer);
                        }
                    }
                }
                chargedOrbTime = 0;
            }

            if (!forcefield)
            {
                forcefieldTime++;
            }

            if (forcefieldTime >= 400)
            {
                if (Main.rand.Next(1, 6) == 1 && Main.netMode !=1)
                {
                    forcefield = true;
                    NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-1, 2) * 50, (int)npc.Center.Y + Main.rand.Next(-1, 2) * 50, mod.NPCType("Forcefield") , 0, npc.whoAmI); //at the end the npc.whoAmI tells the orcefield's npc.ai[0] to equal the npc's nuumber in the Main.npc[] array
                    
                }
                forcefieldTime = 0;
            }

            //int forcefieldCount = NPC.CountNPCS(mod.NPCType("Forcefield"));
            for(int i=0; i<200; i++)
            {
                if(Main.npc[i].type == mod.NPCType("Forcefield") && (int)Main.npc[i].ai[0] == npc.whoAmI && Main.npc[i].active) //checks for a forcefield npc and if it's ai[0] is equal to npc.whoAmI meaning its parent
                {
                    hasPersonalForcefield = true;
                }

            }
            if (hasPersonalForcefield)
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
                forcefield = false;
            }
            hasPersonalForcefield = false;
            
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