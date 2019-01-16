using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class TheBlackHole : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 20;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0f;
            item.value = 40000;
            item.shoot = 3;
            item.rare = 3;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item38;
            item.autoReuse = true;
            item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int proj1 = Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("VacuumShot"), damage, 0.3f, player.whoAmI, 0, 0);
            int proj2 = Projectile.NewProjectile(position, new Vector2(speedX + Main.rand.Next(-5, 6), speedY + Main.rand.Next(-3, 4)), mod.ProjectileType("VacuumShot"), damage, 0.3f, player.whoAmI, 0, 0);
            int proj3 = Projectile.NewProjectile(position, new Vector2(speedX + Main.rand.Next(-5, 6), speedY + Main.rand.Next(-3, 4)), mod.ProjectileType("VacuumShot"), damage, 0.3f, player.whoAmI, 0, 0);
            Main.projectile[proj1].scale = 2f;
            Main.projectile[proj2].scale = 2f;
            Main.projectile[proj3].scale = 2f;
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-11, -4);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Black Hole");
            Tooltip.SetDefault("Instead of knocking enemies back, this weapon pulls them toward you \nRapidly fires bursts of three");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(null, "TheWhirlwind", 1);
            recipe1.AddIngredient(3456, 20);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
}