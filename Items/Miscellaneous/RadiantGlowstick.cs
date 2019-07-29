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
	public class RadiantGlowstick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Glowstick");
            Tooltip.SetDefault("Emits more light than normal glowsticks");
		}
        public override void HoldItem(Player player)
        {
            Lighting.AddLight(player.position, new Vector3(0.8f, 0.8f, 0.12f));
        }
        public override void SetDefaults()
		{
            item.noMelee = true;
            item.noUseGraphic = true;
			item.thrown = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 47;
			item.useAnimation = 47;
			item.width = 30;
			item.height = 30;
			item.value = 350;
            item.shoot = mod.ProjectileType("RadiantGlowstickProj");
            item.shootSpeed = 9f;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 999;
            item.consumable = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WhiteGel", 1);
            recipe.AddIngredient(ItemID.Glowstick, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();
        }
    }
}
