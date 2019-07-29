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
    public class Jellomat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jellomat");
			Tooltip.SetDefault("Materializes a jellium crystal that bounces all over the place");
            Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.damage = 90;
			item.magic = true;
            item.mana = 8;
            item.noMelee = true;
            item.useStyle = 5;
            item.knockBack = 3;
			item.useTime = 20;
			item.useAnimation = 20;
			item.width = 52;
			item.height = 52;
			item.value = 3000;
            item.rare = 5;
            item.shoot = mod.ProjectileType("JellomatProj");
            item.shootSpeed = 10;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "JelliumCrystal", 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}