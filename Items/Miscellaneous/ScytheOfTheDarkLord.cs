using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class ScytheOfTheDarkLord : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe of the Dark Lord");
            Tooltip.SetDefault("There's a disclaimer written in fine text on the handle, informing you that there is no dark lord.");
        }
        public override void SetDefaults()
        {
            item.damage = 101;
            item.melee = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.useTime = 30;
            item.useAnimation = 30;
            item.width = 74;
            item.height = 74;
            item.rare = 2;
            item.UseSound = SoundID.Item45;
            item.maxStack = 1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 15);
            recipe.AddIngredient(ItemID.Pumpkin, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
