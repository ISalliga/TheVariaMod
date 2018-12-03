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
    public class TheAnomalyRage : ModNPC
    {
		int dashTime = 0;
		int dashPhase = 1;
		int dashesDone = 0;
		int shootTime = 0;
		int npcTime = 0;
		int despawn = 0;
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
			npc.scale = 1f;
            npc.width = 120;
            npc.height = 160;
            npc.damage = 105;
            npc.defense = 2;
            npc.lifeMax = 20000;
			npc.aiStyle = 14;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 12, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Anomaly2");
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
            Main.npcFrameCount[npc.type] = 9;
        }
		public virtual void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.defense = 3;
			npc.lifeMax = 50000;
		}
		
        public override void AI()
        {
			npc.alpha -= 5;
			dashTime++;
			Player player = Main.player[npc.target];
			if (dashPhase == 1)
			{
				if (dashTime >= 60)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 20;
					dashTime = 0;
					dashesDone++;
					if (dashesDone > 8)
					{
						dashPhase = 2;
						dashTime = 0;
						dashesDone = 0;
					}
				}
			}
			if (dashPhase == 2)
			{
				if (dashTime == 40)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 0;
					int swich1 = Main.rand.Next(1,4);
					if (swich1 == 1)
					{
						npc.position.X = player.Center.X - 60;
					}
					else if (swich1 == 2)
					{
						npc.position.X = player.Center.X - 310;
					}
					else
					{
						npc.position.X = player.Center.X + 190;
					}
					
					int swich2 = Main.rand.Next(1,4);
					if (swich2 == 1)
					{
						if (swich1 != 1)
						{
							npc.position.Y = player.Center.Y - 60;
						}
						else
						{
							if (Main.rand.Next(1, 3) == 2)
							{
								npc.position.Y = player.Center.Y - 330;
							}
							else
							{
								npc.position.Y = player.Center.Y + 170;
							}
						}
					}
					else if (swich2 == 2)
					{
						npc.position.Y = player.Center.Y - 330;
					}
					else
					{
						npc.position.Y = player.Center.Y + 170;
					}
				}
				if (dashTime > 40 && dashTime < 80)
				{
					npc.velocity = npc.DirectionTo(player.Center) * -3;
				}
				if (dashTime == 80)
				{
					npc.velocity = npc.DirectionTo(player.Center) * 16;
					dashesDone++;
				}
				if (dashTime == 150)
				{
					dashTime = 0;
					if (dashesDone > 6)
					{
						dashPhase = 1;
						dashTime = 0;
						dashesDone = 0;
					}
				}
			}
			if (npc.life < npc.lifeMax * 0.7)
			{
				shootTime++;
				if (shootTime >= 40)
				{
					int bolt = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AnomalousBolt"));
					Main.npc[bolt].damage = 20;
					shootTime = 0;
				}
				if (shootTime == 20 & npc.life < npc.lifeMax * 0.35)
				{
					int bolt = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AnomalousBolt"));
					Main.npc[bolt].damage = 20;
				}
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
			NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("TheAnomalyGrief"));
			Main.NewText("........", 0, 155, 255);
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