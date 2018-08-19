using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.FallenAngel
{
	public class DarklightBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darklight Blade");
			Tooltip.SetDefault("Harnesses the power of dark and light to create a beam of two blades which splits into lesser beams upon hitting a tile or enemy");
		}
		public override void SetDefaults()
		{
			item.damage = 57;
			item.melee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("DarklightSwordBeam");
			item.shootSpeed = 10;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarklightEssence", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
