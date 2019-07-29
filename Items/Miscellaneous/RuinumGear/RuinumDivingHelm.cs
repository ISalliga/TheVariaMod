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

namespace Varia.Items.Miscellaneous.RuinumGear
{
	[AutoloadEquip(EquipType.Head)]
	public class RuinumDivingHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ruinum Diving Mask");
            Tooltip.SetDefault("+11% ranged damage, +23% when underwater");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 3;
		}

		public override bool IsArmorSet(Item head,  Item body,  Item legs)
		{
			return body.type == mod.ItemType("RuinumBreastplate") && legs.type == mod.ItemType("RuinumGreaves");
		}

        public override void UpdateEquip(Player player)
        {
            if (!player.wet)
            {
                player.rangedDamage *= 1.1f;
                player.rangedCrit += 4;
            }
            else
            {
                player.rangedDamage *= 1.2f;
                player.rangedCrit += 9;
            }
        }
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You will not drown underwater";
			player.GetModPlayer<VariaPlayer>().ruinumSetBonus = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RuinumBar", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}