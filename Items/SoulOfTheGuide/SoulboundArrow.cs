using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class SoulboundArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soulbound Arrow");
            Tooltip.SetDefault("Stays in place upon hitting tiles, dealing damage to any enemies who run into it");
		}
		public override void SetDefaults()
		{
            item.damage = 9;
            item.consumable = true;
			item.width = 30;
			item.height = 24;
			item.value = 450;
			item.rare = 2;
			item.maxStack = 999;
            item.ammo = AmmoID.Arrow;
            item.shoot = mod.ProjectileType("HunterArrow");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SoulShard", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 150);
            recipe.AddRecipe();
        }
    }
}
