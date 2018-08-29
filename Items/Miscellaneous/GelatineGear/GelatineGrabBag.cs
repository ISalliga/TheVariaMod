using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class GelatineGrabBag : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 9;     
			item.value = Item.buyPrice(0, 1, 0, 0);
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Gelatine Grab Bag");
      Tooltip.SetDefault("Right click to open \nCan at a 1 in 20 chance contain a special item with very useful effects");
    }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void RightClick(Player player)
        {
			switch(Main.rand.Next(1, 6))
			{
				case 1:
				player.QuickSpawnItem(mod.ItemType("GelatineBlade"), 1);
				break;
				case 2:
				player.QuickSpawnItem(mod.ItemType("GelatineStaff"), 1);
				break;
				case 3:
				player.QuickSpawnItem(mod.ItemType("GelatineScepter"), 1);
				break;
				case 4:
				player.QuickSpawnItem(mod.ItemType("GelatineGreatbow"), 1);
				break;
				case 5:
				player.QuickSpawnItem(mod.ItemType("GelatineBlob"), 1);
                break;
			}
            if (Main.rand.Next(1, 21) == 1)
            {
                player.QuickSpawnItem(mod.ItemType("SlimyBarricade"), 1);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkGel, 7);
            recipe.AddIngredient(ItemID.Gel, 70);
            recipe.AddTile(TileID.Solidifier);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
