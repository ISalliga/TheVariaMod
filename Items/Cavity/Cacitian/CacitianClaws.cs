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
	public class CacitianClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacitian Claws");
		}
		public override void SetDefaults()
		{
			item.damage = 17;
			item.melee = true;
            item.scale = 1.8f;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 7;
			item.useAnimation = 7;
			item.width = 46;
			item.height = 52;
			item.value = 1350;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "CacitianBar",  3);
            recipe.AddIngredient(null,  "MutatedBlob",  5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
