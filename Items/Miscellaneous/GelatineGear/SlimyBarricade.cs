using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class SlimyBarricade : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.defense = 4;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 4;
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slimy Barricade");
            Tooltip.SetDefault("Makes acceleration static");
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.runAcceleration = 1.3f;
			player.runSlowdown = 1f;
		}
    }
}