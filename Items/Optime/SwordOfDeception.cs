using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.Optime
{
	public class SwordOfDeception : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword of Deception");
			Tooltip.SetDefault("Creates two beams at the cost of mana");
		}
		public override void SetDefaults()
		{
			item.damage = 90;
			item.melee = true;
            item.mana = 10;
			item.width = 68;
			item.height = 68;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("DeceitBeam");
			item.shootSpeed = 15;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX + Main.rand.Next(-3, 3), speedY + Main.rand.Next(-3, 3), mod.ProjectileType("DeceitBeam"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PureConcentratedDarkness", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
