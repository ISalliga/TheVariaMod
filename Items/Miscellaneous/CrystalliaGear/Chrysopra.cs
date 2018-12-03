using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class Chrysopra : ModItem
    {
		int arrowsShot;
		int arrowType;
        public override void SetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 10; 
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4.25f;
            item.value = 40000;
            item.rare = 3;
			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item38;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 15f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chrysopra");
			Tooltip.SetDefault("Shoots a barrage of crystal shards");
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int index = 0; index < 6; ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-60, 61) * 0.045f;
                float SpeedY = speedY + (float)Main.rand.Next(-60, 61) * 0.045f;
                int projectile1 = Projectile.NewProjectile(position.X, position.Y, SpeedX * 1.4f, SpeedY * 1.4f, 94, damage - 10, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            }
			return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 85);
			recipe.AddIngredient(null, "CrystalliaBar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}