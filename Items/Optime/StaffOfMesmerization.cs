using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.Optime
{
	public class StaffOfMesmerization : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff of Mesmerization");
			Tooltip.SetDefault("Fires normal projectiles \nUpon enemy hits minions spawn");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.summon = true;
            item.mana = 8;
			item.width = 68;
			item.height = 68;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("MesmerBeam");
			item.shootSpeed = 19;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PureConcentratedDarkness", 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
