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

namespace Varia.Items.FallenAngel.TechHunk
{
    public class TrydanReactorCore : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trydan Reactor Core");
            Tooltip.SetDefault("You emit spark clouds when you're hit");
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<VariaPlayer>().trydanCore = true;
		}
    }
}