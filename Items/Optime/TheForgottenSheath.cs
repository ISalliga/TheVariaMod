using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class TheForgottenSheath : ModItem
    {
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Forgotten Sheath");
            Tooltip.SetDefault("After use, the next six melee attacks you perform will deal one and a half times more damage \n" + "Does not stack, and has an 18-attack cooldown after your 6 buffed attacks run out");
        }
        public override void SetDefaults()
        {
            item.consumable = false;
            item.width = 42;
            item.height = 54;
            item.melee = true;
            item.noMelee = true;
            item.value = 50000;
            item.damage = 80;
            item.useStyle = 4;
            item.UseSound = SoundID.Item1;
            item.noUseGraphic = true;
            item.rare = 5;
            item.useTime = 50;
            item.useAnimation = 50;
            item.autoReuse = true;
            item.maxStack = 1;
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.LocalPlayer.GetModPlayer<VariaPlayer>().forgottenSheath < 1) return true;
            else return false;
        }
        public override bool UseItem(Player player)
        {
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().forgottenSheath = 24;
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PureConcentratedDarkness", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}