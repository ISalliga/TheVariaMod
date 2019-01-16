using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class Mutagen : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutagen"); 
            Tooltip.SetDefault("Gives you a chance to change enemy stats on hit \n4 minute duration"); 
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
            player.AddBuff(mod.BuffType("MutagenBuff"), 60 * 60 * 4);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MutatedBlob"), 1);
            recipe.AddIngredient(mod.ItemType("CacitianOre"), 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}