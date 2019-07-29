using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class MagicJavelin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Javelin");
            Tooltip.SetDefault("Unlimited javelins!");
        }

        public override void SetDefaults()
        {
            item.damage = 32;
            item.magic = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 15;
            item.mana = 7;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 3.5f;
            item.value = 30000;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.shootSpeed = 11f;
            item.shoot = mod.ProjectileType("MagicJavelinProj");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 offset = new Vector2(speedX, speedY) * 1.2f;
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
            {
                position += offset;
            }
            return true;
        }
    }
}