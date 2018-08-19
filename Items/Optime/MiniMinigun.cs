using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class MiniMinigun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 25;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 20; 
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4.25f;
            item.value = 40000;
            item.rare = 3;
			item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.shoot = 282;
            item.autoReuse = true;
            item.shootSpeed = 7f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Minigun");
			Tooltip.SetDefault("'Basically Megashark 2'");
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PureConcentratedDarkness", 13);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}