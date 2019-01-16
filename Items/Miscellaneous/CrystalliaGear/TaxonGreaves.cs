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
	[AutoloadEquip(EquipType.Legs)]
	public class TaxonGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("7% increased critical strike chance \n20% increased critical strike chance for three seconds after being damaged");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeCrit += 7;
			player.rangedCrit += 7;
			player.magicCrit += 7;
			player.thrownCrit += 7;
			player.GetModPlayer<VariaPlayer>().taxonGreaves = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(502,  10);
			recipe.AddIngredient(3,  50);
			recipe.AddIngredient(null,  "CrystalliaBar",  10);
			recipe.AddTile(134);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}