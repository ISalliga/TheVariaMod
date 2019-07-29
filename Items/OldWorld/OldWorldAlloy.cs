using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class OldWorldAlloy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old World Alloy");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 0;
			item.rare = 2;
			item.maxStack = 99;
		}
	}
}
