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
	public class Pectinator : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pectinator");
            Tooltip.SetDefault("Throws up to three blades at once");
		}
		public override void SetDefaults()
		{
			item.damage = 54;
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
            item.shoot = mod.ProjectileType("PectinatorProj");
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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i< Main.rand.Next(1, 4); i++)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-2, 3), speedY + Main.rand.Next(-2, 3), type, damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            }
            return false;
        }
    }
}
