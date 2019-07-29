using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
	public class RuinumBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruinum Bar");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 0;
			item.rare = 2;
			item.maxStack = 99;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RuinumOre"),  3);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
