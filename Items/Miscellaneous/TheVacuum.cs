using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class TheVacuum : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 20; 
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0f;
            item.value = 40000;
            item.rare = 3;
            item.shoot = 3;
            item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("VacuumShot"), damage, 0.3f, player.whoAmI, 0, 0);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Vacuum");
			Tooltip.SetDefault("Instead of knocking enemies back, this weapon pulls them toward you");
        }
        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cloud, 25);
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.Cloud, 25);
            recipe1.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
}