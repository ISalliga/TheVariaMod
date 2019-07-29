using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    //[AutoloadEquip(EquipType.Shoes)]
    public class PocketTelescope : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0,  1,  0,  0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pocket Telescope");
            Tooltip.SetDefault("Allows you to see further towards the horizon when equipped (Press a hotkey to toggle) \nHold UP or DOWN to look up or down \nIncreases view range (Right Click to zoom out) \n+5% ranged damage");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            player.scope = true;
            player.GetModPlayer<VariaPlayer>().Goggles = true;
            player.GetModPlayer<VariaPlayer>().PocketTelescope = true;
            player.rangedDamage *= 1.05f;
        }
    }
}