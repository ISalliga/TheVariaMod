using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
    public class CacitianBow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 3;
            item.ranged = true;
            item.width = 26;
            item.height = 40;
            item.crit = 10; 
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
			item.UseSound = SoundID.Item5;
            item.knockBack = 4.25f;
            item.value = 2250;
            item.rare = 3;
			item.useAmmo = 40;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 15f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cacitian Longbow");
			Tooltip.SetDefault("Shoots three arrows at once");
        }
        public override bool Shoot(Player player,  ref Microsoft.Xna.Framework.Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            for (int i = 0; i <= 2; i++)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-20,  21) * 0.045f;
                float SpeedY = speedY + (float)Main.rand.Next(-20,  21) * 0.045f;
                int projectile1 = Projectile.NewProjectile(position.X,  position.Y,  SpeedX,  SpeedY,  type,  damage,  knockBack,  player.whoAmI,  0.0f,  0.5f + (float)Main.rand.NextDouble() * 0.9f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,  "CacitianBar",  5);
            recipe.AddIngredient(null,  "MutatedBlob",  7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}