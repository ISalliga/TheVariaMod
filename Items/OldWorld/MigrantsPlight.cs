using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
    //imported from my tAPI mod because I'm lazy
    public class MigrantsPlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Migrant's Plight");
            Tooltip.SetDefault("Summons a swift but inaccurate forgotten spirit to fight for you");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
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
            item.rare = 2;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("ForgottenSpirit");
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ShardOfThePast", 5);
            recipe.AddIngredient(null, "OldWorldAlloy", 10);
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
            speedX = Main.rand.Next(-10, 11);
            speedY = Main.rand.Next(-10, 11);
            if (player.altFunctionUse != 2) player.AddBuff(mod.BuffType("ForgottenSpiritBuff"), 10);
            position = Main.MouseWorld;
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
