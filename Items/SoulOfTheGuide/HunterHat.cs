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

namespace Varia.Items.SoulOfTheGuide
{
	[AutoloadEquip(EquipType.Head)]
	public class HunterHat : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Hunter Hat");
            Tooltip.SetDefault("10% increased ranged damage \n40% increased damage with wooden arrows \n10% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
            item.expert = true;
			item.value = 10000;
			item.rare = 2;
			item.defense = 1;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetModPlayer<VariaPlayer>().hunterHat = true;
            player.rangedDamage *= 1.1f;
        }
	}
}