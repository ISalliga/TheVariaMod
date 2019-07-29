using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class WornBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Worn Brick");
		}
		public override void SetDefaults()
		{
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = 150;
			item.useStyle = 1;
			item.rare = 0;
			item.useTime = 10;
			item.useAnimation = 15;
			item.autoReuse = true;
			item.createTile = mod.TileType("WornBrick");
			item.maxStack = 999;
		}
    }
}
