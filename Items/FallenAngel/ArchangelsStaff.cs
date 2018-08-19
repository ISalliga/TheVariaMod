using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
	public class ArchangelsStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arch-Angel's Staff");
			Tooltip.SetDefault("Fires two laser orbs which follow the cursor then explode");
		}
		public override void SetDefaults()
		{
			item.damage = 47;
			item.noMelee = true;
			item.magic = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 18;
			item.useAnimation = 33;
			item.shoot = mod.ProjectileType("MiniArchorbFriendly");
			item.shootSpeed = 8;
			item.useStyle = 1;
			item.mana = 9;
			item.knockBack = 2;
			item.rare = 5;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.useTurn = false;
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

