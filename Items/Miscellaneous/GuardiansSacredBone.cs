using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class GuardiansSacredBone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guardian's Sacred Bone");
            Tooltip.SetDefault("Summons your own personal Guardian to fight for you");
        }

        public override void SetDefaults()
        {
            item.damage = 19;
            item.summon = true;
            item.mana = 10;
            item.width = 40;
            item.height = 40;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("GuardianMinion");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("GuardianBuff"); //The buff added to player after used the item
            item.buffTime = 3600;               //The duration of the buff, here is 60 seconds
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 15);
            recipe.AddIngredient(ItemID.Pumpkin, 20);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
    }
}
