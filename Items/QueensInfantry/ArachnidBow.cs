using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.QueensInfantry
{
	public class ArachnidBow : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arachnid Bow");
        }
        public override void SetDefaults()
		{
			item.damage = 15;
			item.noMelee = true;
			item.ranged = true;
			item.rare = 3;
			item.width = 28;
			item.height = 30;
			item.useStyle = 5;
            item.useTime = 21;
            item.shootSpeed = 10f;				//Speed is not important here
			item.useAnimation = 21;
            item.shoot = ProjectileID.WoodenArrowFriendly;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SpiderFlesh", 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
