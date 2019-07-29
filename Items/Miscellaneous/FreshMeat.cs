using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class FreshMeat : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fresh Meat");
            Tooltip.SetDefault("Functions as a spear \nSlows enemies \n'Stab your enemies, then point and laugh at their death'");
        }
        public override void SetDefaults()
		{
			item.damage = 16;
			item.useStyle = 5;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 5.7f;
			item.knockBack = 6.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.rare = 3;
			item.value = Item.sellPrice(silver: 10);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("FreshMeatProj");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] <= 1; 
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ZombieArm, 1);
            recipe.AddIngredient(ItemID.ShadowScale, 9);
            recipe.AddIngredient(ItemID.DemoniteBar, 13);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.ZombieArm, 1);
            recipe1.AddIngredient(ItemID.TissueSample, 9);
            recipe1.AddIngredient(ItemID.CrimtaneBar, 13);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
}
