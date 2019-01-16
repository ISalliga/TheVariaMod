using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.FallenAngel
{
    public class HolyMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Holy Machine");
            Tooltip.SetDefault("Rapidly fires homing lasers");
        }
        public override void SetDefaults()
        {
            item.damage = 28;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;                            //Channel so that you can held the weapon [Important]
            item.rare = 7;
            item.width = 28;
            item.height = 30;
            item.useStyle = 5;
            item.useTime = 12;
            item.shootSpeed = 6f;               //Speed is not important here
            item.useAnimation = 12;
            item.shoot = 282;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, mod.ProjectileType("HolyLaser"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarklightEssence", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
