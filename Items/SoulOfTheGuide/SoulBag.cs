using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.SoulOfTheGuide
{
    public class SoulBag : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 9;
            bossBagNPC = mod.NPCType("SoulOfTheGuide");
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
            player.QuickSpawnItem(mod.ItemType("SoulShard"), Main.rand.Next(5, 9));
            player.QuickSpawnItem(mod.ItemType("Soulbinder"));
        }
    }
}
