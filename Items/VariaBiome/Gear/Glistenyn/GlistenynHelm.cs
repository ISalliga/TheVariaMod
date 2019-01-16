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

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
	[AutoloadEquip(EquipType.Head)]
	public class GlistenynHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Glistenyn Helm");
            Tooltip.SetDefault("18% increased melee damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 5;
			item.defense = 20;
		}

		public override bool IsArmorSet(Item head,  Item body,  Item legs)
		{
			return body.type == mod.ItemType("GlistenynArmor") && legs.type == mod.ItemType("GlistenynGreaves");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.18f;
		}
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Dealing more true melee damage than your weapon's base damage will multiply your damage by 2, dealing less will divide it by 2. \nDealing the exact amount will multiply it by 4. \nCritical strikes are unchanged";
			player.GetModPlayer<VariaPlayer>().glistenynSetBonusMelee = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlistenynBar", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}