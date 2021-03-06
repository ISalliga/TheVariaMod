using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity
{
	public class MutatedBlob : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutated Blob");
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
