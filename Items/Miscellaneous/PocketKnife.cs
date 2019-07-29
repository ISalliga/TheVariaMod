using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class PocketKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pocket Knife");
			Tooltip.SetDefault("Right-click to whittle blocks");
		}
		public override void SetDefaults()
		{
			item.damage = 11;
			item.melee = true;
			item.useStyle = 3;
			item.knockBack = 3;
			item.useTime = 5;
			item.useAnimation = 15;
			item.width = 70;
			item.height = 70;
			item.value = 8000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = false;
			item.useTurn = true;
		}
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.hammer = 10;
                item.damage = 0;
            }
            else
            {
                item.damage = 11;
                item.hammer = 0;
            }
            return base.CanUseItem(player);
        }
    }
}
