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
    public class RainGuillotines : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rain Guillotines");
			Tooltip.SetDefault("Rains guillotines down from the heavens!");
		}
		public override void SetDefaults()
		{
			item.damage = 82;
            item.melee = true;
			item.noMelee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 8;
			item.useAnimation = 8;
			item.width = 52;
            item.noUseGraphic = true;
			item.height = 52;
			item.value = 3000;
            item.rare = 7;
            item.shoot = mod.ProjectileType("RainGuillotineProj");
            item.shootSpeed = 16;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AnomalousChunk", 15);
            recipe.AddIngredient(ItemID.ChainGuillotines, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(new Vector2(Main.MouseWorld.X + Main.rand.Next(-75, 76), position.Y - Main.rand.Next(Main.screenHeight / 2 + 50, Main.screenHeight / 2 + 100)), new Vector2(speedX / 5 + (Main.rand.Next(-40, 41) * 0.1f), 19), mod.ProjectileType("RainGuillotineProj"), item.damage, 0.3f, player.whoAmI, 0, 0);
            return false;
        }
    }
}
