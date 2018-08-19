using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class HappyBomb : ModItem
    {
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Happy Bomb");
            Tooltip.SetDefault("Throws a floating bomb that destroys tiles");
        }
        public override void SetDefaults()
        {
            item.consumable = false;
            item.width = 42;
            item.height = 54;
            item.thrown = true;
            item.value = 50000;
            item.damage = 80;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.noUseGraphic = true;
            item.rare = 5;
            item.useTime = 50;
            item.shoot = mod.ProjectileType("HappyBombProj");
            item.shootSpeed = 12;
            item.useAnimation = 50;
            item.autoReuse = true;
            item.maxStack = 1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PureConcentratedDarkness", 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}