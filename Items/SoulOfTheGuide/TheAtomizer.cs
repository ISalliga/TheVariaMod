using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class TheAtomizer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Atomizer");
			Tooltip.SetDefault("Fires an atomic laser");
		}
		public override void SetDefaults()
		{
			item.damage = 39;
			item.noMelee = true;
			item.ranged = true;
			item.width = 58;
			item.height = 22;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("AtomizerProj");
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 20;
			item.useStyle = 5;
			item.knockBack = 2;
			item.rare = 4;
			item.autoReuse = false;
		}
		
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X += speedX * 4f;
            position.Y += speedY * 4f;
            int projectile1 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AtomizerProj"), damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(mod.ItemType("SoulShard"), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

