using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.VariaBiome.ShrineArtifacts
{
    public class ConjurersArtifact : ModItem
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
          DisplayName.SetDefault("Conjurer Artifact of the Shrine");
          Tooltip.SetDefault("Grants you a random pick from a selection of mana-related items. Amounts vary.");
        }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void RightClick(Player player)
        {
            switch (Main.rand.Next(1, 16))
            {
                case 1:
                    player.QuickSpawnItem(ItemID.ManaCrystal, Main.rand.Next(1, 3));
                    break;
                case 2:
                    player.QuickSpawnItem(ItemID.Diamond, Main.rand.Next(15, 31));
                    break;
                case 3:
                    player.QuickSpawnItem(ItemID.Amethyst, Main.rand.Next(15, 31));
                    break;
                case 4:
                    player.QuickSpawnItem(ItemID.Ruby, Main.rand.Next(15, 31));
                    break;
                case 5:
                    player.QuickSpawnItem(ItemID.Sapphire, Main.rand.Next(15, 31));
                    break;
                case 6:
                    player.QuickSpawnItem(ItemID.Emerald, Main.rand.Next(15, 31));
                    break;
                case 7:
                    player.QuickSpawnItem(ItemID.Topaz, Main.rand.Next(15, 31));
                    break;
                case 8:
                    player.QuickSpawnItem(ItemID.FallenStar, Main.rand.Next(20, 29));
                    break;
                case 9:
                    player.QuickSpawnItem(ItemID.WizardHat, 1);
                    Main.NewText("Legendary Loot! (Wizard Hat)");
                    break;
                case 10:
                    player.QuickSpawnItem(ItemID.ManaFlower, 1);
                    Main.NewText("Legendary Loot! (Mana Flower)");
                    break;
                case 11:
                    player.QuickSpawnItem(ItemID.ManaPotion, Main.rand.Next(19, 37));
                    break;
                case 12:
                    player.QuickSpawnItem(mod.ItemType("CacitianOre"), Main.rand.Next(30, 54));
                    break;
                case 13:
                    player.QuickSpawnItem(ItemID.MagicMirror, 1);
                    Main.NewText("Legendary Loot! (Magic Mirror)");
                    break;
                case 14:
                    player.QuickSpawnItem(ItemID.MagicHat, 1);
                    Main.NewText("Legendary Loot! (Magic Hat)");
                    break;
                case 15:
                    player.QuickSpawnItem(ItemID.Amber, Main.rand.Next(15, 31));
                    break;
            }
        }
    }
}
