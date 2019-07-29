using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class ClawStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Claw Staff");
            Tooltip.SetDefault("Summons two skeletal claws fight for you \nThere can only be one pair \nCannot be used if there are not two minion slots available");
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
            item.shoot = mod.ProjectileType("Skelehand");
            item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = player.Center;
            player.AddBuff(mod.BuffType("SkelehandBuff"), 10);
            if (player.ownedProjectileCounts[item.shoot] < 2)
            {
                if (player.maxMinions - player.numMinions >= 2)
                {
                    Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("Skelehand"), item.damage, item.knockBack, player.whoAmI, -80);
                    Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("Skelehand"), item.damage, item.knockBack, player.whoAmI, 80);
                }
            }
            return false;
        }
    }
}
