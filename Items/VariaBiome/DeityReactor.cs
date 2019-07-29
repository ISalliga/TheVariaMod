using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Varia;

namespace Varia.Items.VariaBiome
{
    public class DeityReactor : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deity Reactor"); 
            Tooltip.SetDefault("Calls upon the power of an all-powerful goddess to bring a star shower to your location"); 
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42; 
            item.maxStack = 20; 
            item.value = 0; 
            item.rare = 11; 
            item.useAnimation = 30; 
            item.useTime = 30; 
            item.useStyle = 4; 
            item.consumable = true; 
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime || VariaWorld.starShower)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool UseItem(Player player)
        {
            VariaWorld.starShower = true;
            Main.NewText("A star shower is happening!", Color.MediumPurple);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.StoneBlock, 25);
            recipe.AddIngredient(null, "GlistenynOre", 10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}