using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class CrystalSkinPotion : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Skin Potion"); 
            Tooltip.SetDefault("Boosts your melee critical strike chance by 15% and boosts your melee damage by 15%. \nReduces your damage reduction by 10% and reduces your defense by 20%. \n3 minute duration"); 
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
            player.AddBuff(mod.BuffType("CrystalSkin"), 60 * 60 * 3);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrystalShard, 1);
			recipe.AddIngredient(ItemID.Blinkroot, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(mod.TileType("LabStation"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}