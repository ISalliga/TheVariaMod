using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.EverlastingBreeze
{
	public class StarplateBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starplate Brick");
		}
		public override void SetDefaults()
		{
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = 150;
			item.useStyle = 1;
			item.rare = 0;
			item.useTime = 10;
			item.useAnimation = 15;
			item.autoReuse = true;
			item.createTile = mod.TileType("StarplateBrick");
			item.maxStack = 999;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GalaxianMirror", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(null, "StarplateWall", 4);
            recipe1.AddTile(TileID.WorkBenches);
            recipe1.SetResult(this, 1);
            recipe1.AddRecipe();
        }
    }
}
