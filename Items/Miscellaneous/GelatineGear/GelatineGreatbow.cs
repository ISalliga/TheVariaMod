using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class GelatineGreatbow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 19;
            item.ranged = true;
            item.width = 58;
            item.height = 28;
            item.crit = 20; 
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 4.25f;
            item.value = 40000;
            item.rare = 3;
			item.useAmmo = AmmoID.Arrow;
			item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HighVelocityBlob");
            item.shootSpeed = 20f;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X - 5, position.Y, speedX, speedY, mod.ProjectileType("HighVelocityBlob"), damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gelatine Greatbow");
			Tooltip.SetDefault("Fires a high-velocity, bouncing blob of gelatin");
        }
    }
}