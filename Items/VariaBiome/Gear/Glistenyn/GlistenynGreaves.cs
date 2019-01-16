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
	[AutoloadEquip(EquipType.Legs)]
	public class GlistenynGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Glistenyn Greaves");
			Tooltip.SetDefault("12% increased magic and melee damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 5;
			item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.12f;
			player.magicDamage *= 1.12f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "GlistenynBar",  15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}