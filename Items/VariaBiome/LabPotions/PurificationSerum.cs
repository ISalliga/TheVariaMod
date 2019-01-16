using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class PurificationSerum : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purification Serum"); 
            Tooltip.SetDefault("Cleanses corrupt and crimson tiles in a medium radius around you \n2 minute duration"); 
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42; 
            item.maxStack = 30; 
            item.value = 0; 
            item.rare = 11; 
            item.useAnimation = 10;
            item.useTime = 10; 
            item.useStyle = 2; 
            item.consumable = true; 
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("PurificationSerum1"), 60 * 60 * 2);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GrassSeeds, 1);
            recipe.AddIngredient(ItemID.Daybloom, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}