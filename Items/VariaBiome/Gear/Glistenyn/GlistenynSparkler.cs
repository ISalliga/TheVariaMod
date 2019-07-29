using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
	public class GlistenynSparkler : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glistenyn Sparkler");
            Item.staff[item.type] = true;
            Tooltip.SetDefault("Creates a cloud of sparkles at the mouse cursor");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.noMelee = true;
			item.magic = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("GlistenynSparkle");
			item.shootSpeed = 8;
			item.useStyle = 5;
			item.mana = 9;
			item.knockBack = 2;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 10; i++)
            {
                Projectile.NewProjectile(Main.MouseWorld, new Vector2(Main.rand.Next(-9, 10), Main.rand.Next(-9, 10)), type, damage, 0.5f, Main.myPlayer);
            }
            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GlistenynBar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

