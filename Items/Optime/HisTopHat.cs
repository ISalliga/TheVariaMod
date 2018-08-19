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

namespace Varia.Items.Optime
{
	[AutoloadEquip(EquipType.Head)]
	public class HisTopHat : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("His Top Hat");
            Tooltip.SetDefault("When above half HP, fatal damage will take away half of your health instead");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
            item.expert = true;
			item.value = 10000;
			item.rare = 2;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetModPlayer<VariaPlayer>().hisTopHat = true;
        }
	}
}