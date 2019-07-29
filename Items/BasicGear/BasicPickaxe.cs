using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.BasicGear
{
	public class BasicPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The standard pickaxe given to Terrarians.");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 20;
			item.pick = 35;
			item.useStyle = 1;
            item.knockBack = 0.02f;
            item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
	}
}