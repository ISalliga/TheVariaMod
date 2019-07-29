using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    //imported from my tAPI mod because I'm lazy
    public class RuinumShark : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruinum Shark Staff");
            Tooltip.SetDefault("Summons a small shark to fight for you");
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
            item.value = Item.buyPrice(0,  3,  0,  0);
            item.rare = 3;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("SharkMinion");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("RuinumSharkBuff"); //The buff added to player after used the item
            item.buffTime = 3600;               //The duration of the buff,  here is 60 seconds
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RuinumBar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player,  ref Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
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
