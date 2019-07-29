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
	public class CacitianScepter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cacitian Scepter");
            Tooltip.SetDefault("Creates a droplet of toxic waste at your mouse cursor that forms a lingering puddle when it hits a tile");
            Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
            item.magic = true;
			item.damage = 30;
            item.mana = 9;
			item.noMelee = true;
			item.useStyle = 5;
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
			item.shoot = mod.ProjectileType("ToxicWaste");
			item.shootSpeed = 15;
		}
        public override bool Shoot(Player player,  ref Microsoft.Xna.Framework.Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X,  Main.MouseWorld.Y,  0,  0,  type,  damage,  knockBack,  player.whoAmI,  0.0f,  0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "CacitianBar",  7);
            recipe.AddIngredient(null,  "MutatedBlob",  9);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
