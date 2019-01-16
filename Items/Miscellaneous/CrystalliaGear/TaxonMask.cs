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

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
	[AutoloadEquip(EquipType.Head)]
	public class TaxonMask : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Taxon Mask");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 27;
		}

		public override bool IsArmorSet(Item head,  Item body,  Item legs)
		{
			return body.type == mod.ItemType("TaxonChestguard") && legs.type == mod.ItemType("TaxonGreaves");
		}
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Upon critically hitting an enemy,  your damage reduction is boosted by 8% for four seconds";
			player.GetModPlayer<VariaPlayer>().taxonSetBonus2 = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(502,  15);
			recipe.AddIngredient(3,  75);
			recipe.AddIngredient(null,  "CrystalliaBar",  15);
			recipe.AddTile(134);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}