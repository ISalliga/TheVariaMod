using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
	public class PureConcentratedDarkness : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pure, Concentrated Darkness");
			Tooltip.SetDefault("'Eating it would pulverize your immune system as well as turn you into an eldritch horror. \nAlso gluten-free and a great source of Vitamin C.'");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 350;
			item.rare = 5;
			item.maxStack = 99;
		}
	}
}
