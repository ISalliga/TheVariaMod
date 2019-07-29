using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
	public class RuinumOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruinum Ore");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 0;
			item.rare = 2;
            item.useTime = 15;
            item.useAnimation = 15;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
            item.useStyle = 1;
            item.createTile = mod.TileType("RuinumTile");
			item.maxStack = 250;
		}
	}
}
