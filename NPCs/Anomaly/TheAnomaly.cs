using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Varia.NPCs.Anomaly;
using Varia;
using Terraria.ModLoader;

namespace Varia.NPCs.Anomaly
{
	[AutoloadBossHead]
    public class TheAnomaly : ModNPC
    {
		int tpPhase = 0;
		int wormholeCount;
		int despawn = 0;
		int portalCount = 0;
		int tpTime = 0;
		bool starting = true;
		int npcTime = 0;
		int tpTimeIntv = 0;
		int tpTime2 = 0;
		bool phase2Started = false;
		int timeAlive = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Anomaly");
		}
        public override bool CheckActive()
        {
            return false;
        }
        public override void SetDefaults()
        {
			npc.scale = 1.5f;
            npc.width = 120;
            npc.height = 160;
            npc.damage = 105;
            npc.defense = 2;
            npc.lifeMax = 18000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Anomaly1");
            npc.noGravity = true;
			npc.noTileCollide = true; 
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Venom] = true;
			npc.buffImmune[BuffID.ShadowFlame] = true;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.buffImmune[BuffID.Daybreak] = true;
			npc.lavaImmune = true;
            Main.npcFrameCount[npc.type] = 14;
        }
		public virtual void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.defense = 3;
			npc.lifeMax = 50000;
		}
		
        public override void AI()
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            wormholeCount = NPC.CountNPCS(mod.NPCType("AnomalousWormhole"));
			Player player = Main.player[npc.target];
			if (starting == true)
			{
				
				NPC.NewNPC((int)player.Center.X + -800, (int)player.Center.Y - 400, mod.NPCType("AnomalousWormhole"));
				NPC.NewNPC((int)player.Center.X + -500, (int)player.Center.Y - 475, mod.NPCType("AnomalousWormhole"));
				NPC.NewNPC((int)player.Center.X + -200, (int)player.Center.Y - 550, mod.NPCType("AnomalousWormhole"));
				NPC.NewNPC((int)player.Center.X + 200, (int)player.Center.Y - 550, mod.NPCType("AnomalousWormhole"));
				NPC.NewNPC((int)player.Center.X + 500, (int)player.Center.Y - 475, mod.NPCType("AnomalousWormhole"));
				NPC.NewNPC((int)player.Center.X + 800, (int)player.Center.Y - 400, mod.NPCType("AnomalousWormhole"));
				npc.dontTakeDamage = true;
				Main.NewText("Hello, friend! Nice to meet you! What's your name?", 0, 255, 60);
				starting = false;
			}
			tpTime++;
			if (wormholeCount <= 3 && wormholeCount > 1)
			{
				tpTimeIntv++;
				if (tpTimeIntv == 2)
				{
					tpTime++;
					tpTimeIntv = 0;
				}
			}
			if (tpTime == 60 && wormholeCount > 1)
			{
				Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 10, mod.ProjectileType("GlitchProj"), 40, 0f, 0, 0f, 0f);
			}
			if (tpTime == 90 && wormholeCount > 1)
			{
				Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 10, mod.ProjectileType("GlitchProj"), 40, 0f, 0, 0f, 0f);
			}
			if (tpTime == 120 && wormholeCount > 1)
			{
				Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 10, mod.ProjectileType("GlitchProj"), 40, 0f, 0, 0f, 0f);
			}
			if (tpTime == 150 && wormholeCount > 1)
			{
				Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 10, mod.ProjectileType("GlitchProj"), 40, 0f, 0, 0f, 0f);
			}
			if (tpTime >= 300)
			{
				portalCount = 0;
				if (wormholeCount != 1)
				{
					foreach (NPC portal in Main.npc)
					{
						if (portal.type == mod.NPCType("AnomalousWormhole"))
						{
							if (Main.rand.Next(1,6) == 1 || portalCount == 5)
							{
								npc.position.X = portal.position.X - 60;
								npc.position.Y = portal.position.Y - 80;
							}
							else
							{
								portalCount++;
							}
						}
					}
				}
				tpTime = 0;
			}
			if (wormholeCount <= 5 && wormholeCount != 1)
			{
				npcTime++;
				if (npcTime > 500)
				{
					int swich = Main.rand.Next(1,4);
					if (swich == 1)
					{
						if (NPC.CountNPCS(mod.NPCType("AnomalyMinion1")) <= 4)
						{
							NPC.NewNPC((int)npc.Center.X + 15, (int)npc.Center.Y, mod.NPCType("AnomalyMinion1"));
							NPC.NewNPC((int)npc.Center.X - 15, (int)npc.Center.Y, mod.NPCType("AnomalyMinion1"));
						}
					}
					if (swich == 2)
					{
						if (NPC.CountNPCS(mod.NPCType("AnomalyMinion2")) <= 6)
						{
							NPC.NewNPC((int)npc.Center.X + 50, (int)npc.Center.Y, mod.NPCType("AnomalyMinion2"));
							NPC.NewNPC((int)npc.Center.X - 25, (int)npc.Center.Y - 25, mod.NPCType("AnomalyMinion2"));
							NPC.NewNPC((int)npc.Center.X - 25, (int)npc.Center.Y + 25, mod.NPCType("AnomalyMinion2"));
						}
					}
					if (swich == 3)
					{
						if (NPC.CountNPCS(mod.NPCType("AnomalyMinion3")) <= 3)
						{
							NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AnomalyMinion3"));
						}
					}
					npcTime = 0;
				}
			}
			
			if (wormholeCount == 1)
			{
				if (phase2Started == false)
				{
					npc.position.X = player.position.X;
					npc.position.Y = player.position.Y - 200;
					npc.dontTakeDamage = false;
					phase2Started = true;
				}
				tpTime2++;
				if (tpTime2 == 51 && tpPhase < 4)
				{
					NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AnomalousBolt"));
				}
				if (tpTime2 > 100)
				{
					if (tpPhase < 4)
					{
						NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AnomalousBolt"));
						int distance;
						if (Main.rand.Next(1, 3) == 1)
						{
							distance = 250;
						}
						else
						{
							distance = -250;
						}
						if (Main.rand.Next(1, 3) == 1)
						{
							NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - distance, mod.NPCType("AnomalousBolt"));
						}
						else
						{
							NPC.NewNPC((int)player.Center.X + distance, (int)player.Center.Y, mod.NPCType("AnomalousBolt"));
						}
						
					}
					Vector2 tpPos;
					if (Main.rand.Next(1, 3) == 1)
					{
						tpPos.X = player.Center.X - 300;
					}
					else
					{
						tpPos.X = player.Center.X + 300;
					}
					if (Main.rand.Next(1, 3) == 1)
					{
						tpPos.Y = player.Center.Y - 300;
					}
					else
					{
						tpPos.Y = player.Center.Y + 300;
					}
					tpPos.X -= 175;
					tpPos.Y -= 175;
					npc.position = tpPos;
					tpTime2 = 0;
					tpPhase++;
					if (tpPhase > 6)
					{	
						tpPhase = 0;
					}
				}
			}
			timeAlive++;
			if (timeAlive > 10 && wormholeCount == 0)
			{
				
			}
			
			if (Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                despawn++;
            }
            if (despawn > 30)
            {
                npc.active = false;
            }
        }
		public override bool CheckDead()
		{
			npc.active = false;
			NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("TheAnomalyRage"));
			Main.NewText("stop it stop it STOP IT STOP IT STOP IT STOP IT I HAVEN'T EVEN GOTTEN YOUR NAME YET! WHY ARE YOU DOING THIS TO ME?!", 255, 0, 0);
			return false;
		}
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.frameCounter >= 5)
			{
				npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
				npc.frameCounter = 0;
			}
        }
    }
}