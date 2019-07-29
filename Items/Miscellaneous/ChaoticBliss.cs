using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class ChaoticBliss : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaotic Bliss");
            Tooltip.SetDefault("Casts a chaotic orb");
        }

        public override void SetDefaults()
        {
            item.damage = 29;
            item.magic = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 22;
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
            item.shoot = mod.ProjectileType("ChaoticOrb");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(position, 26, 26, 27, speedX + Main.rand.Next(-5, 6) / 2, speedY + Main.rand.Next(-5, 6) / 2, 0, new Color(255, 255, 255), 1.842105f)];
                dust.noGravity = true;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}