using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
	public class ArachnidRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnid Rod");
            Tooltip.SetDefault("Barrages enemies with short-range venom fangs");
            Item.staff[item.type] = true;
        }
		public override void SetDefaults()
		{
			item.damage = 11;
			item.noMelee = true;
			item.magic = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 3;
			item.useAnimation = 15;
			item.shoot = mod.ProjectileType("VenomFangShortRange");
			item.shootSpeed = 10;
			item.useStyle = 5;
			item.mana = 7;
			item.knockBack = 2;
			item.rare = 3;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.useTurn = false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX += Main.rand.Next(-50, 51) / 10;
            speedY += Main.rand.Next(-50, 51) / 10;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SpiderFlesh", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

