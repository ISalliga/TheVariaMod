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

namespace Varia.Items.Optime
{
	public class OptimesGrasp : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Optime's Grasp");
			Tooltip.SetDefault("Generates a negative energy field that immobilizes enemies");
		}
		public override void SetDefaults()
		{
            item.magic = true;
			item.damage = 15;
            item.mana = 20;
			item.noMelee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 35;
			item.useAnimation = 35;
			item.width = 46;
			item.height = 52;
			item.value = 3100;
            item.crit = 4;
			item.rare = 3;
			item.UseSound = SoundID.Item8;
			item.maxStack = 1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("NegativeEnergyField");
			item.shootSpeed = 15;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, type, damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PureConcentratedDarkness", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
