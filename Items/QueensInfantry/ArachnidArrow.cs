using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
	public class ArachnidArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnid Arrow");
            Tooltip.SetDefault("Poisons enemies");
		}
		public override void SetDefaults()
		{
            item.damage = 6;
            item.consumable = true;
			item.width = 30;
			item.height = 24;
			item.value = 450;
			item.rare = 2;
			item.maxStack = 999;
            item.ammo = AmmoID.Arrow;
            item.shoot = mod.ProjectileType("ArachnidArrowProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SpiderFlesh", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
