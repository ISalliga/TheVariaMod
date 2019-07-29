using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class TheTongue : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Tongue");
			Tooltip.SetDefault("Fires a retracting tongue");
		}
		public override void SetDefaults()
		{
			item.damage = 92;
			item.ranged = true;
            item.noMelee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("Tongue");
			item.shootSpeed = 80;
			item.rare = 5;
            item.UseSound = SoundID.Item34;
			item.autoReuse = true;
			item.useTurn = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -5);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "JelliumCrystal", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
