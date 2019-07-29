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
	[AutoloadEquip(EquipType.Legs)]
	public class RuinumGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+15% maximum move speed, +30% underwater");
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
            player.ignoreWater = true;
            if (!player.wet)
            {
                player.maxRunSpeed *= 1.15f;
            }
            else
            {
                player.maxRunSpeed *= 1.3f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RuinumBar", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}