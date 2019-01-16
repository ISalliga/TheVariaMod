using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class RegrowthPotion : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regrowth Potion"); 
            Tooltip.SetDefault("Gives you an aura that works to counteract damage from hits of 50+ damage \n4 minute duration"); 
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
            player.AddBuff(mod.BuffType("RegrowthAura"), 60 * 60 * 4);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteOre, 1);
			recipe.AddIngredient(ItemID.Daybloom, 1);
            recipe.AddIngredient(ItemID.BottledHoney, 1);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}