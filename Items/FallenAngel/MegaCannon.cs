using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.FallenAngel
{
    public class MegaCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a large concentrated laser");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
            item.noMelee = true;
            item.magic = true;
            item.channel = true; //Channel so that you can held the weapon [Important]
            item.mana = 5;
            item.rare = 5;
            item.width = 28;
            item.height = 30;
            item.useTime = 20;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 14f;
            item.useAnimation = 20;
            item.shoot = mod.ProjectileType("MegaCannonProj");
            item.value = Item.sellPrice(silver: 3);
        }

        public override bool UseItem(Player player)
        {
            player.statMana -= 1;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarklightEssence", 19);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
