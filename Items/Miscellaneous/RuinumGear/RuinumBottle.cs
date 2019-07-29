using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumBottle : ModItem
	{
		private Player player;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruinum Bottle");
			Tooltip.SetDefault("Leaves a trail of aqueous fumes when thrown");
		}
		public override void SetDefaults()
		{
			item.consumable = false;
			item.width = 42;
			item.height = 54;
			item.thrown = true;
			item.value = 50000;
			item.damage = 15;
			item.useStyle = 1;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 1);
			item.noUseGraphic = true;
			item.rare = 5;
			item.useTime = 20;
			item.shoot = mod.ProjectileType("RuinumBottleProj");
			item.shootSpeed = 11;
			item.useAnimation = 20;
			item.autoReuse = true;
			item.maxStack = 1;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RuinumBar", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}