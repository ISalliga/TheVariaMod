using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
	public class CacitianSaber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacitian Saber");
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 15;
			item.useAnimation = 15;
			item.width = 46;
			item.height = 52;
			item.value = 2700;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "CacitianBar",  6);
            recipe.AddIngredient(null,  "MutatedBlob",  10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
