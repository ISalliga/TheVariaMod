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
	public class GlistenynMaskedHood : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Glistenyn Masked Hood");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 5;
			item.defense = 15;
		}

		public override bool IsArmorSet(Item head,  Item body,  Item legs)
		{
            return body.type == mod.ItemType("GlistenynArmor") && legs.type == mod.ItemType("GlistenynGreaves");
        }
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "While using magic damage, every hit has a chance to spawn sparkles";
			player.GetModPlayer<VariaPlayer>().glistenynSetBonusMagic = true;
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