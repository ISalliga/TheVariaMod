using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.Miscellaneous
{
	public class BlazenBlaster : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazen Blaster");
        }
        public override void SetDefaults()
		{
			item.damage = 10;
			item.noMelee = true;
			item.ranged = true;
			item.autoReuse = false;
			item.rare = 2;
			item.width = 28;
			item.height = 30;
			item.useStyle = 5;
            item.useTime = 20;
			item.UseSound = SoundID.Item11;
            item.shootSpeed = 6f;
			item.useAnimation = 20;                         
			item.shoot = 282;
			item.value = 2700;
			item.useAmmo = AmmoID.Bullet;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-10, 11) * 0.045f, speedY + Main.rand.Next(-10, 11) * 0.045f, type, 10, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 6);
            recipe.AddIngredient(ItemID.Torch, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
