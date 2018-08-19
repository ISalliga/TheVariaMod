using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
	public class StormFromHell : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm from Hell");
			Tooltip.SetDefault("'Rains death from below! Don't ask me how that works.'");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.summon = true;
            item.mana = 8;
			item.width = 68;
			item.height = 68;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("TinyDarkThing");
			item.shootSpeed = 19;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            Projectile.NewProjectile(new Vector2(position.X + Main.rand.Next(-950, 950), position.Y + Main.rand.Next(1200, 1500)), new Vector2(speedX / 12, -19), mod.ProjectileType("TinyDarkThing"), 23, 0.3f, player.whoAmI, 0, 0);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PureConcentratedDarkness", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
