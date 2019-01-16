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

namespace Varia.Items.Cavity.Cacitian
{
	public class CacitianWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacitian Wand");
			Tooltip.SetDefault("Creates lingering clouds of Cavitous Smog");
		}
		public override void SetDefaults()
		{
            item.magic = true;
			item.damage = 20;
            item.mana = 8;
			item.noMelee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 25;
			item.useAnimation = 25;
			item.width = 46;
			item.height = 52;
			item.value = 2700;
            item.crit = 4;
			item.rare = 3;
			item.UseSound = SoundID.Item8;
			item.maxStack = 1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("CavitousSmog");
			item.shootSpeed = 15;
		}
        public override bool Shoot(Player player,  ref Microsoft.Xna.Framework.Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            
            Projectile.NewProjectile(position.X + Main.rand.Next(-20,  20),  position.Y + Main.rand.Next(-20,  20),  speedX + Main.rand.Next(-10,  11) * 0.6f,  speedY + Main.rand.Next(-10,  11) * 0.045f,  type,  damage,  knockBack,  player.whoAmI,  0.0f,  0.5f + (float)Main.rand.NextDouble() * 0.9f);
            Projectile.NewProjectile(position.X + Main.rand.Next(-20,  20),  position.Y + Main.rand.Next(-20,  20),  speedX + Main.rand.Next(-15,  16) * 0.6f,  speedY + Main.rand.Next(-15,  16) * 0.045f,  type,  damage,  knockBack,  player.whoAmI,  0.0f,  0.5f + (float)Main.rand.NextDouble() * 0.9f);
            Projectile.NewProjectile(position.X + Main.rand.Next(-20,  20),  position.Y + Main.rand.Next(-20,  20),  speedX + Main.rand.Next(-20,  21) * 0.6f,  speedY + Main.rand.Next(-20,  21) * 0.045f,  type,  damage,  knockBack,  player.whoAmI,  0.0f,  0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "CacitianBar",  6);
            recipe.AddIngredient(null,  "MutatedBlob",  9);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
