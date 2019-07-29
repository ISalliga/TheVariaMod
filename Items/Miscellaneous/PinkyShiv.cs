using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class PinkyShiv : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinky's Shiv");
			Tooltip.SetDefault("Enemies that bounce off this dagger into walls will take extra damage");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.useStyle = 3;
			item.knockBack = 17;
			item.useTime = 12;
			item.useAnimation = 12;
			item.width = 24;
			item.height = 24;
			item.value = 8000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Bounced"), 50);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            knockBack *= 5f;
        }

        public override void AddRecipes()
		{       
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkGel, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
