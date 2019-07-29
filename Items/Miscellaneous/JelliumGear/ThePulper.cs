using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class ThePulper : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Pulper");
			Tooltip.SetDefault("Creates a projection of itself");
		}
		public override void SetDefaults()
		{
			item.damage = 75;
			item.melee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 5f;
			item.shoot = mod.ProjectileType("PulperProjection");
			item.shootSpeed = 12;
			item.rare = 5;
            item.UseSound = SoundID.Item45;
            item.autoReuse = false;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "JelliumCrystal", 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
