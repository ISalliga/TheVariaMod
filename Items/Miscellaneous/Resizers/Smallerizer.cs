using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Varia.Items;

namespace Varia.Items.Miscellaneous.Resizers
{
    public class Smallerizer : ModItem
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smallerizer");
            Tooltip.SetDefault("Shrinks enemies and, as long as the enemy's defense isn't below five, cuts their defense down");
        }
        public override void SetDefaults()
        {
            item.damage = 1;
            item.ranged = true;
            item.width = 60;
            item.height = 40;
            item.useTime = 34;
            item.useAnimation = 34;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 114);
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 7f;
            item.value = 0;
            item.rare = 10;
            item.autoReuse = true;
            item.shootSpeed = 25f;
            item.shoot = mod.ProjectileType("SmallThing");
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(10, 0);
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 100f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SmallThing"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}