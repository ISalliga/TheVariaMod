using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity
{
	public class PlaguedStabber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plagued Stabber");
			Tooltip.SetDefault("Stabbing an enemy with this thing inflicts poison for an immense amount of time");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.useStyle = 3;
			item.knockBack = 3;
			item.useTime = 17;
			item.useAnimation = 17;
			item.width = 70;
			item.height = 70;
			item.value = 8000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public virtual void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 3000);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CacitianBar", 3);
            recipe.AddIngredient(287, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
