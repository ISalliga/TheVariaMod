using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.VariaBiome.ShrineArtifacts
{
    public class AlchemistsArtifact : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 5;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 4;
			item.value = Item.buyPrice(0, 2, 0, 0);
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Alchemist Artifact of the Shrine");
          Tooltip.SetDefault("Grants you a random pick from a selection of potions or herbs. Amounts vary.");
        }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void RightClick(Player player)
        {
            switch (Main.rand.Next(1, 18))
            {
                case 1:
                    player.QuickSpawnItem(ItemID.LesserHealingPotion, 30);
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.HealingPotion, Main.rand.Next(9, 15));
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.ThornsPotion, Main.rand.Next(2, 6));
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.IronskinPotion, Main.rand.Next(2, 6));
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.RegenerationPotion, Main.rand.Next(2, 6));
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.ManaRegenerationPotion, Main.rand.Next(2, 6));
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.RecallPotion, Main.rand.Next(5, 10));
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.BottledWater, 20);
                    break;
                case 9:
                    player.QuickSpawnItem(mod.ItemType("SpectrumPotion"), Main.rand.Next(1, 4));
                    Main.NewText("Legendary Loot! (Spectrum Potion)");
                    break;
                case 10:
                    player.QuickSpawnItem(mod.ItemType("Mutagen"), Main.rand.Next(1, 4));
                    Main.NewText("Legendary Loot! (Mutagen)");
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.Daybloom, Main.rand.Next(19, 37));
                    break;
                case 12:
                    player.QuickSpawnItem(ItemID.Moonglow, Main.rand.Next(19, 37));
                    break;
                case 13:
                    player.QuickSpawnItem(mod.ItemType("FieryWrathPotion"), Main.rand.Next(1, 4));
                    Main.NewText("Legendary Loot! (Fiery Wrath Potion)");
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.Waterleaf, Main.rand.Next(19, 37));
                    break;
                case 15:
                    player.QuickSpawnItem(ItemID.Deathweed, Main.rand.Next(19, 37));
                    break;
                case 16:
                    player.QuickSpawnItem(ItemID.Blinkroot, Main.rand.Next(19, 37));
                    break;
                case 17:
                    player.QuickSpawnItem(ItemID.Fireblossom, Main.rand.Next(19, 37));
                    break;

            }
        }
    }
}
