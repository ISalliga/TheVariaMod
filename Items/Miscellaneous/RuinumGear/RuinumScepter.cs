using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumScepter : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 22;
            item.magic = true;
            item.width = 26;
            item.height = 40;
            item.crit = 10; 
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 5;
            item.noMelee = true;
			item.UseSound = SoundID.Item21;
            item.knockBack = 4.25f;
            item.value = 2250;
            item.rare = 3;
            item.mana = 5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("RuinumFume");
            item.shootSpeed = 9f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruinum Scepter");
            Tooltip.SetDefault("Fires three bubbles of water");
            Item.staff[item.type] = true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X += speedX * 3;
            position.Y += speedY * 3;
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("RuinumFume"), damage, 0f, player.whoAmI);
                speedX *= 1.13f;
                speedY *= 1.13f;
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null,  "RuinumBar",  7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}