using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace Varia.Items.VariaBiome.Gear.Glistenyn
{
	public class GlistenynBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glistenyn Blade");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.value = 0;
			item.rare = 5;
            item.scale = 1.6f;
			item.maxStack = 1;
            item.damage = 52;
            item.melee = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.useTime = 20;
            item.useAnimation = 20;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(1, 3) == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 offset = new Vector2(Main.rand.Next(-9, 10), Main.rand.Next(-9, 10));
                    int proj = Projectile.NewProjectile(target.Center + (offset * 3), offset, mod.ProjectileType("GlistenynSparkle"), 6, 0.5f, Main.myPlayer);

                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlistenynBar", 14);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
