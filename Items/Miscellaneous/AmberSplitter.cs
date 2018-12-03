using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class AmberSplitter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amber Splitter");
            Tooltip.SetDefault("Reduces enemy contact damage by 25% upon hitting with a melee strike \nHitting enemies with the projectiles however will buff their contact damage by one");
        }
        public override void SetDefaults()
        {
            item.damage = 52;
            item.melee = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.useTime = 23;
            item.useAnimation = 23;
            item.width = 350;
            item.height = 350;
            item.value = 8000;
            item.shoot = mod.ProjectileType("AmberLaser");
            item.shootSpeed = 20;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.maxStack = 1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.damage = target.damage * 4 / 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Amber, 5);
            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.TitaniumBar, 12);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }
    }
}
