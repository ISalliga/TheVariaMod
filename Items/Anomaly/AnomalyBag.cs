using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.Anomaly
{
    public class AnomalyBag : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 9;
            bossBagNPC = mod.NPCType("TheAnomalyGrief");
            item.expert = true;      
			item.value = Item.buyPrice(0, 0, 0, 0);
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Treasure Bag");
      Tooltip.SetDefault("Right click to open");
    }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void OpenBossBag(Player player)
        {
			switch(Main.rand.Next(14))
			{
				case 1:
				player.QuickSpawnItem(ItemID.RoyalGel, 1);
				break;
				case 2:
				player.QuickSpawnItem(ItemID.EoCShield, 1);
				break;
				case 3:
				player.QuickSpawnItem(ItemID.WormScarf, 1);
				break;
				case 4:
				player.QuickSpawnItem(mod.ItemType("Soulbinder"), 1);
				break;
				case 5:
				player.QuickSpawnItem(ItemID.BrainOfConfusion, 1);
				break;
				case 6:
				player.QuickSpawnItem(ItemID.BoneGlove, 1);
				break;
				case 7:
				player.QuickSpawnItem(3333, 1);
				break;
				case 8:
				player.QuickSpawnItem(mod.ItemType("SlimyBarricade"), 1);
				break;
				case 9:
				player.QuickSpawnItem(ItemID.DemonHeart, 1);
				break;
				case 10:
				player.QuickSpawnItem(3353, 1);
				break;
				case 11:
				player.QuickSpawnItem(mod.ItemType("AngelHeart"), 1);
				break;
				case 12:
				player.QuickSpawnItem(ItemID.SporeSac, 1);
				break;
				case 13:
				player.QuickSpawnItem(mod.ItemType("QueensJewel"), 1);
				break;
			}
            player.QuickSpawnItem(mod.ItemType("AnomalousAltarItem"));
        }
    }
}
