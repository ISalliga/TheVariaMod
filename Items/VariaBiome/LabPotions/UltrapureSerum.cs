using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class UltrapureSerum : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultrapure Serum"); 
            Tooltip.SetDefault("Cleanses corrupt, crimson and hallow tiles in a medium radius around you... as well as combats a certain plague \n2 minute duration"); 
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
            player.AddBuff(mod.BuffType("PurificationSerum3"), 60 * 60 * 2);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AnomalousChunk"), 1);
            recipe.AddIngredient(mod.ItemType("GreaterPurificationSerum"), 2);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}