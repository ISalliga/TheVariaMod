using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class Agateclaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Agateclaw");
			Tooltip.SetDefault("Hitting enemies has a 20% chance to increase your critical strike chance by 12% for five seconds");
		}
		public override void SetDefaults()
		{
			item.damage = 13;
			item.melee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 4;
			item.useAnimation = 4;
			item.width = 50;
			item.height = 38;
			item.value = 8000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (Main.rand.Next(1, 21) == 1)
            {
                player.AddBuff(mod.BuffType("TaxonBoost"), 300);
            }
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 95);
			recipe.AddIngredient(null, "CrystalliaBar", 9);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
