using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
	public class Xanthia : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Xanthia");
			Tooltip.SetDefault("Sprays an absolute barrage of crystal shards");
		}
		public override void SetDefaults()
		{
			item.damage = 23;
			item.noMelee = true;
			item.magic = true;
			item.width = 40;
			item.height = 48;
			item.useTime = 5;
			item.useAnimation = 10;
			item.shoot = 90;
			item.shootSpeed = 20;
			item.useStyle = 1;
			item.mana = 6;
			item.knockBack = 2;
			item.rare = 7;
			item.UseSound = SoundID.Item42;
			item.autoReuse = true;
			item.useTurn = false;
        }		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 70);
			recipe.AddIngredient(null, "CrystalliaBar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
			Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
			Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
			Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-10, 10), speedY + Main.rand.Next(-10, 10), 90, damage - Main.rand.Next(1, 27), knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
		}
	}
}

