using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
	public class MegaCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mega Cannon");
			Tooltip.SetDefault("Fires out one large laser orb that explodes into more laser orbs, which explode into EVEN MORE LASERS!");
		}
		public override void SetDefaults()
		{
			item.damage = 75;
			item.noMelee = true;
			item.magic = true;
			item.width = 20;
			item.mana = 23;
			item.height = 20;
			item.useTime = 70;
			item.useAnimation = 70;
			item.shoot = mod.ProjectileType("MegaArchorbFriendly");
			item.shootSpeed = 4;
			item.useStyle = 5;
			item.knockBack = 2;
			item.rare = 5;
			item.UseSound = SoundID.Item43;
			item.autoReuse = false;
			item.useTurn = true;
        }		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarklightEssence", 18);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

