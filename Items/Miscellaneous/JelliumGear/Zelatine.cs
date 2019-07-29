using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class Zelatine : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zelatine");
			Tooltip.SetDefault("Fires a single, slow-moving crystal");
		}
		public override void SetDefaults()
		{
			item.damage = 340;
			item.melee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 55;
			item.useAnimation = 60;
			item.useStyle = 1;
			item.knockBack = 5f;
			item.shoot = mod.ProjectileType("ZelatineCrystal");
			item.shootSpeed = 6;
			item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("ZelatineCrystal"), 220, knockBack, player.whoAmI, 0.0f, 0.0f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "JelliumCrystal", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
