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

namespace Varia.Items.QueensInfantry
{
	public class ArachnidFang : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnid Fang");
            Tooltip.SetDefault("Makes enemies bleed");
		}
		public override void SetDefaults()
		{
			item.damage = 9;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.thrown = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 15;
			item.useAnimation = 15;
			item.width = 30;
			item.height = 30;
			item.value = 350;
            item.shoot = mod.ProjectileType("ArachnidFangProj");
            item.shootSpeed = 11;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 999;
            item.consumable = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "SpiderFlesh",  3);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this,  150);
			recipe.AddRecipe();
		}
	}
}
