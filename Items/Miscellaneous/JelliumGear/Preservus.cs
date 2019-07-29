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

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class Preservus : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Preservus");
            Tooltip.SetDefault("Returns to you after hitting the ground");
		}
		public override void SetDefaults()
		{
			item.damage = 86;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.thrown = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 18;
			item.useAnimation = 18;
			item.width = 30;
			item.height = 30;
			item.value = 350;
            item.shoot = mod.ProjectileType("PreservusProj");
            item.shootSpeed = 16;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
            item.consumable = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,  "JelliumCrystal",  6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
