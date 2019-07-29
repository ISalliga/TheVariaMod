using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
	public class SpiderFlesh : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spider Flesh");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 450;
			item.rare = 2;
			item.maxStack = 250;
		}
	}
}
