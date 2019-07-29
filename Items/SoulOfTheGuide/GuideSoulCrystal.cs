using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class GuideSoulCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guide Soul Crystal");
            Tooltip.SetDefault("Conjures a flaming spirit to light up your mouse cursor");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ShadowOrb);
            item.shoot = mod.ProjectileType("SoulBoi");
            item.buffType = mod.BuffType("SoulBuff");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SoulShard", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}