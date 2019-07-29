using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class BlazenBreaker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazen Breaker");
            Tooltip.SetDefault("Has a chance to set enemies on fire");
        }
        public override void SetDefaults()
        {
            item.damage = 14;
            item.melee = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.useTime = 24;
            item.useAnimation = 24;
            item.width = 46;
            item.height = 42;
            item.value = 8000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.maxStack = 1;
            item.autoReuse = false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.NextBool(4)) target.AddBuff(BuffID.OnFire, 180);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 7);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
