using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld.ChestLoot
{
	public class SolarShank : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Shank");
            Tooltip.SetDefault("Functions as a spear \nSets enemies on fire");
        }
        public override void SetDefaults()
		{
			item.damage = 11;
			item.useStyle = 5;
			item.useAnimation = 30;
			item.useTime = 30;
			item.shootSpeed = 5.7f;
			item.knockBack = 6.5f;
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.rare = 3;
			item.value = Item.sellPrice(silver: 10);

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("SolarShankProj");
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1; 
		}
    }
}
