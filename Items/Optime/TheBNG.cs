using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class TheBNG : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 19;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 20;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4.25f;
            item.value = 40000;
            item.rare = 3;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item38;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BNGProj");
            item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("BNGProj"), 19, 0.3f, player.whoAmI, 0, 0);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -4);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The B.N.G.");
            Tooltip.SetDefault("For each time this weapon's shot pierces, it deals 1.5x more damage");
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