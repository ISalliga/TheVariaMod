using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.VariaBiome.ShrineArtifacts
{
    public class SpelunkersArtifact : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 5;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 4;
			item.value = Item.buyPrice(0, 2, 0, 0);
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Spelunker Artifact of the Shrine");
          Tooltip.SetDefault("Grants you a random pick from a selection of underground-related items. Amounts vary.");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            switch (Main.rand.Next(1, 17))
            {
                case 1:
                    player.QuickSpawnItem(ItemID.CopperOre, Main.rand.Next(56, 64));
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.TinOre, Main.rand.Next(56, 64));
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.LeadOre, Main.rand.Next(43, 54));
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.IronOre, Main.rand.Next(43, 54));
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.SilverOre, Main.rand.Next(30, 39));
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.TungstenOre, Main.rand.Next(30, 39));
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.GoldOre, Main.rand.Next(20, 29));
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.PlatinumOre, Main.rand.Next(20, 29));
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.MagicLantern, 1);
                    Main.NewText("Legendary Loot! (Magic Lantern)");
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.GPS, 1);
                    Main.NewText("Legendary Loot! (GPS)");
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.SpelunkerGlowstick, Main.rand.Next(19, 37));
                    break;
                case 12:
                    player.QuickSpawnItem(mod.ItemType("CacitianOre"), Main.rand.Next(30, 54));
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.MiningHelmet, 1);
                    Main.NewText("Legendary Loot! (Mining Helmet)");
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.Obsidian, Main.rand.Next(14, 23));
                    break;
                case 15:
                    player.QuickSpawnItem(ItemID.StickyBomb, Main.rand.Next(4, 8));
                    break;
                case 16:
                    player.QuickSpawnItem(ItemID.Bomb, Main.rand.Next(4, 8));
                    break;

            }
        }
    }
}
