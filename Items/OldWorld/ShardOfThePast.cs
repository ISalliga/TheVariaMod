using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class ShardOfThePast : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shard of the Past");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 24;
			item.value = 0;
			item.rare = 2;
			item.maxStack = 250;
		}
	}
}
