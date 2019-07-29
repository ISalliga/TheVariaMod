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

namespace Varia.Items.Cavity.Cacitian
{
	[AutoloadEquip(EquipType.Head)]
	public class CacitianHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Cacitian Helmet");
            Tooltip.SetDefault("Gives you night vision");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 7;
		}

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.NightOwl, 8);
        }

        public override bool IsArmorSet(Item head,  Item body,  Item legs)
		{
			return body.type == mod.ItemType("CacitianChestplate") && legs.type == mod.ItemType("CacitianLeggings");
		}
		
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You leave clouds of cavitous smog where you move";
			player.GetModPlayer<VariaPlayer>().cacitianSetBonus = true;
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MutatedBlob", 9);
            recipe.AddIngredient(null, "CacitianBar", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}