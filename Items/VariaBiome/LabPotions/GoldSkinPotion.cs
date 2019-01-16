using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class GoldSkinPotion : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Skin Potion"); 
            Tooltip.SetDefault("Every hit increases the enemy's gold drop by 5% \nEffect is nullfified on bosses \n4 minute duration"); 
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
            player.AddBuff(mod.BuffType("GoldSkin"), 60 * 60 * 4);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldCoin, 1);
            recipe.AddIngredient(ItemID.Obsidian, 1);
            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}