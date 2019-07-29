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
	[AutoloadEquip(EquipType.Body)]
	public class RuinumBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ruinum Breastplate");
			Tooltip.SetDefault("10% increased ranged damage, +20% when underwater \n+4% ranged critical strike chance, +9% when underwater");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 4;
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

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "RuinumBar",  8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}