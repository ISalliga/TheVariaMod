using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Varia;

namespace Varia.Items.FallenAngel
{
    public class TheInfinityCloud : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 32;
            item.value = Item.sellPrice(0, 15, 0, 0);
            item.rare = -12;
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Infinity Cloud");
            Tooltip.SetDefault("Grants you infinite jumps at the cost of mana \n" + "Expert");
        }   
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<VariaPlayer>().infinityCloudEquipped = true;
        }
    }
}