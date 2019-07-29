using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Varia.Items.Miscellaneous
{
	public class AngelsSorrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angel's Sorrow");
			Tooltip.SetDefault("'They say that when it rains, \nit's the tears of an angel...' \nSwinging this sword causes rain droplets to appear above the cursor and fall down");
		}
		public override void SetDefaults()
		{
			item.damage = 51;
			item.melee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 5;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("RuinumFume");
			item.shootSpeed = 10;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = Main.rand.Next(-4, 5);
            speedY = 10;
            position = Main.MouseWorld + new Vector2(Main.rand.Next(-100, 101), -Main.screenHeight * 0.8f);
            damage = Main.rand.Next(10, 17);
            return true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteBar,  10);
            recipe.AddIngredient(mod.ItemType("BlessedAqua"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
