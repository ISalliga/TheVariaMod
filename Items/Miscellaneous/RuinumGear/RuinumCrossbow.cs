using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 17;
            item.ranged = true;
            item.width = 26;
            item.height = 40;
            item.crit = 10; 
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 5;
            item.noMelee = true;
			item.UseSound = SoundID.Item5;
            item.knockBack = 4.25f;
            item.value = 2250;
            item.rare = 3;
			item.useAmmo = AmmoID.Arrow;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 19f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruinum Crossbow");
            Tooltip.SetDefault("Fires a high-velocity arrow");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,  "RuinumBar",  7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}