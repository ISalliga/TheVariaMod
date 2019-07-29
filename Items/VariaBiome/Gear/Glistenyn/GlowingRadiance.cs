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

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
    public class GlowingRadiance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Radiance");
			Tooltip.SetDefault("Conjures a glowing orb of light that hovers above the ground");
		}
		public override void SetDefaults()
		{
			item.damage = 58;
			item.magic = true;
            item.mana = 5;
            item.noMelee = true;
            item.useStyle = 5;
            item.knockBack = 3;
			item.useTime = 16;
			item.useAnimation = 16;
			item.width = 52;
			item.height = 52;
			item.value = 3000;
            item.rare = 5;
            item.shoot = mod.ProjectileType("RadiantOrb");
            item.shootSpeed = 16;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, (speedX * 2) + Main.rand.Next(-2, 3), speedY * 2f, type, damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddIngredient(null, "GlistenynBar", 9);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}