using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
	public class DarklightEssence : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darklight Essence");
			Tooltip.SetDefault("The Fallen Angel dropped this. It emanates a gloomy aura on one side, and a happy one on the other.");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 350;
			item.rare = 2;
			item.maxStack = 99;
		}
	}
}
