using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class BottledBlaze : ModItem
	{
		private Player player;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bottled Blaze");
			Tooltip.SetDefault("Explodes into a flame cloud");
		}
		public override void SetDefaults()
		{
			item.consumable = false;
			item.width = 42;
			item.height = 54;
			item.thrown = true;
			item.damage = 8;
			item.useStyle = 1;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 1);
			item.noUseGraphic = true;
			item.rare = 2;
			item.useTime = 27;
			item.shoot = mod.ProjectileType("BottledBlazeProj");
			item.shootSpeed = 11;
			item.useAnimation = 27;
			item.autoReuse = true;
			item.maxStack = 1;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 4);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}