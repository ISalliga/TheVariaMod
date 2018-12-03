using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.Cavity
{
	public class CollectiveConsciousness : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Collective Consciousness");
			Tooltip.SetDefault("Fires mind lasers instead of bullets");
        }
        public override void SetDefaults()
		{
			item.damage = 6;
			item.noMelee = true;
			item.ranged = true;
			item.autoReuse = true;
			item.rare = 3;
			item.width = 28;
			item.height = 30;
			item.useStyle = 5;
            item.useTime = 8;
			item.UseSound = SoundID.Item75;
            item.shootSpeed = 6f;
			item.useAnimation = 8;                         
			item.shoot = 282;
			item.value = 2700;
			item.useAmmo = AmmoID.Bullet;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MutatedBlob", 25);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
