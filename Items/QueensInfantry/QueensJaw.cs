using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
    public class QueensJaw : ModItem
    {
        public override void SetDefaults()
        {
            item.useTurn = true;
            item.damage = 12;
            item.melee = true;
            item.noMelee = false;
            item.width = 40;
            item.height = 11;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = 1;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 4, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("QueensJawProj"); //503;
            item.shootSpeed = 7f;
            item.noUseGraphic = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen's Jaw");
            Tooltip.SetDefault("Throws two boomerangs at once");
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 30f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 2f;
            double offsetAngle;
            int i;
            for (i = 0; i < 2; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                int ichor = Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
