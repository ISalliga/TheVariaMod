using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class SaintsGildedArchstaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saint's Gilded Archstaff");
			Tooltip.SetDefault("Creates pulsing shockwaves of light near the mouse cursor");
            Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.noMelee = true;
			item.magic = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("SaintsProj");
			item.shootSpeed = 0;
			item.useStyle = 5;
			item.mana = 10;
			item.knockBack = 2;
			item.UseSound = SoundID.Item20;
			item.rare = 4;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 mouseToPlayer;
			mouseToPlayer.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Vector2 mouseToPlayer2;
			mouseToPlayer2.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer2.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Vector2 mouseToPlayer3;
			mouseToPlayer3.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer3.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Vector2 mouseToPlayer4;
			mouseToPlayer4.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer4.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Vector2 mouseToPlayer5;
			mouseToPlayer5.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer5.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Vector2 mouseToPlayer6;
			mouseToPlayer6.X = Main.MouseWorld.X + Main.rand.Next(-100, 100);
			mouseToPlayer6.Y = Main.MouseWorld.Y + Main.rand.Next(-65, 65);
			
			Projectile.NewProjectile(mouseToPlayer, mouseToPlayer, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(mouseToPlayer2, mouseToPlayer2, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(mouseToPlayer3, mouseToPlayer3, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(mouseToPlayer4, mouseToPlayer, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(mouseToPlayer5, mouseToPlayer2, type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(mouseToPlayer6, mouseToPlayer3, type, damage, knockBack, player.whoAmI);
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 20);
			recipe.AddIngredient(ItemID.HallowedBar, 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

