using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Varia;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Varia.NPCs
{
	[AutoloadHead]
	public class Ninja : ModNPC
	{
        int storyArc = 0;

		public override string Texture
		{
			get
			{
				return "Varia/NPCs/Ninja";
			}
		}

		public override bool Autoload(ref string name)
        {
			name = "Ninja";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ninja");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 50;
            npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
			animationType = NPCID.Merchant;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 1);
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
            if (NPC.downedSlimeKing) return true;
            else return false;
		}

		public override string TownNPCName()
		{
            if (Main.rand.Next(50) == 0) return "Rorbert";
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Gary";
                case 1:
                    return "Claus";
                default:
                    return "Roy";
            }
        }

		public override string GetChat()
		{
            /*if (VariaWorld.downedAngel && storyArc == 0)
            {
                storyArc++;
                return "I have a confession... I- I'm not what you guys think I am. I'm a Varian, sent by our king to guard you from an invasion. I wasn't told anything more, and the dishonor of being trapped in a slime when you first saw me... I just didn't want to say. I'm sorry.";
            }*/
			int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
			if (partyGirl >= 0 && Main.rand.Next(5) == 0)
			{
				return "I've got nothing against " + Main.npc[partyGirl].GivenName + ", but can you please tell her to stop decorating my house with streamers and rainbows?";
			}
			switch (Main.rand.Next(4))
			{
				case 0:
					return "Oh hey, " + Main.LocalPlayer.name + ", how's your adventuring been?";
                case 1:
                    return "What's your favorite color? Mine are blue and green. ... You look surprised.";
                case 2:
                    return "Why are so many people asking me about some video game called Fortnite?";
                default:
					return "What is this edge people talk about me having? The only edge I have is a katana.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
            shop.item[nextSlot].SetDefaults(ItemID.Katana);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaHood);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaShirt);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.NinjaPants);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.ThrowingKnife);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.InvisibilityPotion);
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.RodofDiscord);
                nextSlot++;
            }
        }

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 5;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.Shuriken;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}