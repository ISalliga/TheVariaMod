using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class SoulHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.value = 300;
            item.rare = 8;
			item.maxStack = 50;
			item.consumable = true;
			item.useStyle = 4;
			item.useTime = 35;
			item.useAnimation = 35;
        }
		public override bool UseItem(Player player)
		{
			Main.PlaySound(SoundID.MaxMana, player.position, 0);
			player.AddBuff(mod.BuffType("EnhancedSoul"), 10);
			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "SoulShard", 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}