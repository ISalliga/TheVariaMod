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
    public class TheAnomalyGrief : ModNPC
    {
		int dialogue = 0;
		int mercyTime = 0;
		int life;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Anomaly");
		}
        public override void SetDefaults()
        {
			npc.scale = 1.5f;
            npc.width = 120;
            npc.height = 160;
            npc.damage = 105;
            npc.defense = 2;
            npc.lifeMax = 25000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Anomaly3");
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
			if (npc.life <= npc.lifeMax * 0.9)
			{
				if (dialogue == 0)
				{
					if (VariaWorld.downedAnomaly == true)
					{
						Main.NewText("I've done this before, haven't I?", 0, 155, 255);
						dialogue++;
					}
					else
					{
						Main.NewText("I see now.", 0, 155, 255);
						dialogue++;
					}
				}
			}
			if (npc.life <= npc.lifeMax * 0.8)
			{
				if (dialogue == 1)
				{
					Main.NewText("All I've been doing the whole time was wreaking havoc.", 0, 155, 255);
					dialogue++;
				}
			}
			if (npc.life <= npc.lifeMax * 0.7)
			{
				if (dialogue == 2)
				{
					Main.NewText("But please, forgive me.", 0, 155, 255);
					dialogue++;
				}
			}
			if (npc.life <= npc.lifeMax * 0.6)
			{
				if (dialogue == 3)
				{
					Main.NewText("I don't have the mental capacity to do this anymore, now that I've snapped out of my past delusions... Please...", 0, 155, 255);
					dialogue++;
				}
			}
			if (npc.life <= npc.lifeMax * 0.5)
			{
				if (dialogue == 4)
				{
					Main.NewText("Please, even though I'm such a terrible...", 0, 155, 255);
					dialogue++;
				}
			}
			if (npc.life <= npc.lifeMax * 0.4)
			{
				if (dialogue == 5)
				{
					Main.NewText("Please don't kill me! PLEASE! I'm sorry... I'll do anything!", 0, 155, 255);
					dialogue++;
				}
				mercyTime++;
				npc.defense = 1;
			}
			if (npc.life <= npc.lifeMax * 0.2)
			{
				if (dialogue == 6)
				{
					Main.NewText("I despise you, and I always will. I won't let you spare me anymore. I won't give you that satisfaction.", 0, 155, 255);
					dialogue++;
				}
			}
			if (mercyTime > 500)
			{
				npc.dontTakeDamage = true;
			}
			if (mercyTime == 500)
			{
				Main.NewText("I can tell you still have forgiveness in your heart, but sadly...", 0, 155, 255);
			}
			if (mercyTime == 700)
			{
				Main.NewText("I cannot keep myself like this for any longer.", 0, 155, 255);
			}
			if (mercyTime == 900)
			{
				Main.NewText("Thank you, though, for helping me achieve this state.", 0, 255, 155);
			}
			if (mercyTime == 1100)
			{
				Main.NewText("I'll... leave you alone now...", 0, 255, 55);
			}
			if (mercyTime == 1300)
			{
				Main.NewText("But first, please take this as a token of my appreciation.", 0, 255, 0);
			}
			if (mercyTime == 1400)
			{
				if (!Main.expertMode)
				{
					{
						switch(Main.rand.Next(14))
						{
							case 1:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RoyalGel, 1);
							break;
							case 2:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.EoCShield, 1);
							break;
							case 3:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.WormScarf, 1);
							break;
							case 4:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Soulbinder"), 1);
							break;
							case 5:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BrainOfConfusion, 1);
							break;
							case 6:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BoneGlove, 1);
							break;
							case 7:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3333, 1);
							break;
							case 8:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlimyBarricade"), 1);
							break;
							case 9:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.DemonHeart, 1);
							break;
							case 10:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3353, 1);
							break;
							case 11:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AngelHeart"), 1);
							break;
							case 12:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SporeSac, 1);
							break;
							case 13:
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJewel"), 1);
							break;
						}
					}
				}
				else
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AnomalyBag"), 1);
				}
				VariaWorld.downedAnomaly = true;
				npc.active = false;
			}
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
		{
			mercyTime = 0;
		}
		public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                {
					switch(Main.rand.Next(14))
					{
						case 1:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RoyalGel, 1);
						break;
						case 2:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.EoCShield, 1);
						break;
						case 3:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.WormScarf, 1);
						break;
						case 4:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Soulbinder"), 1);
						break;
						case 5:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BrainOfConfusion, 1);
						break;
						case 6:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BoneGlove, 1);
						break;
						case 7:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3333, 1);
						break;
						case 8:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlimyBarricade"), 1);
						break;
						case 9:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.DemonHeart, 1);
						break;
						case 10:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 3353, 1);
						break;
						case 11:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AngelHeart"), 1);
						break;
						case 12:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SporeSac, 1);
						break;
						case 13:
						Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueensJewel"), 1);
						break;
					}
                }
            }
            else
            {
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AnomalyBag"), 1);
            }
            potionType = ItemID.GreaterHealingPotion;
            VariaWorld.downedAnomaly = true;
        }
		public override bool CheckDead()
		{
			Main.NewText("Hahaha...", 0, 155, 255);
			Main.NewText("I knew it. You heartless monster.", 255, 0, 0);
			return true;
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