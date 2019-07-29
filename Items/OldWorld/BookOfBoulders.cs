using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
    public class BookOfBoulders : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Book of Boulders");
            Tooltip.SetDefault("Creates a boulder above the mouse cursor that falls down and breaks upon hitting an enemy or tile");
        }

        public override void SetDefaults()
        {
            item.damage = 54;
            item.magic = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 40;
            item.mana = 19;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4.5f;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shootSpeed = 1f;
            item.shoot = mod.ProjectileType("MagicBoulder");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = new Vector2(Main.MouseWorld.X + Main.rand.Next(-10, 11), Main.MouseWorld.Y - Main.rand.Next(40, 51));
            speedX = 0f;
            speedY = 0f;
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}