using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity
{
	public class Holestone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holestone Block");
		}
		public override void SetDefaults()
		{
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = 0;
			item.useStyle = 1;
			item.rare = 0;
			item.useTime = 10;
			item.useAnimation = 15;
			item.autoReuse = true;
			item.createTile = mod.TileType("Holestone");
			item.maxStack = 999;
		}
	}
}
