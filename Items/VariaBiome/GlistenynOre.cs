using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome
{
	public class GlistenynOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glistenyn Ore");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.value = 150;
			item.rare = 5;
			item.useTime = 10;
			item.useAnimation = 15;
			item.autoReuse = true;
			item.maxStack = 999;
        }
    }
}
