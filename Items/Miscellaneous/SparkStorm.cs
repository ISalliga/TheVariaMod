using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class SparkStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark Storm");
            Tooltip.SetDefault("Casts a volley of sparks");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 18;
            item.magic = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 4;
            item.mana = 7;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = 30000;
            item.rare = 4;
            item.UseSound = SoundID.Item8;
            item.autoReuse = false;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("StormSpark");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WandofSparking, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.AshBlock, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 offset = new Vector2(speedX, speedY) * 3.75f;
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
            {
                position += offset;
            }
            int rand1 = Main.rand.Next(-10, 11) / 5;
            int rand2 = Main.rand.Next(-10, 11) / 5;
            speedX += rand1;
            speedY += rand2;
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}