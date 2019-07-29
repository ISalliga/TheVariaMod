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
	[AutoloadEquip(EquipType.Head)]
	public class PineHat : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Pine Hat");
            Tooltip.SetDefault("'Once worn by a legendary detective... -ish'");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 5000;
			item.rare = 2;
            item.vanity = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddIngredient(ItemID.FallenStar, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}