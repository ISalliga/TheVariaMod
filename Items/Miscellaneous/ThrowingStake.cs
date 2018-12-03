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

namespace Varia.Items.Miscellaneous
{
    public class ThrowingStake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Throwing Stake");
			Tooltip.SetDefault("'This is pretty self-explanatory.'");
		}
		public override void SetDefaults()
		{
			item.damage = 19;
            item.thrown = true;
			item.noMelee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 14;
			item.useAnimation = 14;
			item.width = 52;
            item.noUseGraphic = true;
			item.height = 52;
			item.value = 3000;
            item.rare = 7;
            item.shoot = mod.ProjectileType("ThrownStake");
            item.shootSpeed = 40;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.AddIngredient(ItemID.Pumpkin, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
