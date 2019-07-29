using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class WhiteGel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Gel");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 10;
			item.rare = 2;
			item.maxStack = 999;
		}
	}
}
