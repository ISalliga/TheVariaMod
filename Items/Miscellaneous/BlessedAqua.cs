using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class BlessedAqua : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blessed Aqua");
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
