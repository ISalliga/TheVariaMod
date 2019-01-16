using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome
{
	public class StarplateWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starplate Wall");
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
			item.createWall = mod.WallType("StarplateWall");
			item.maxStack = 999;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,  "StarplateBrick",  1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this,  4);
            recipe.AddRecipe();
        }
    }
}
