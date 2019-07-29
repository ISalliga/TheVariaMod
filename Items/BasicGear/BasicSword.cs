using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.BasicGear
{
	public class BasicSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Basic Sword");
			Tooltip.SetDefault("The standard sword given to Terrarians.");  //The (English) text shown below your weapon's name
		}

		public override void SetDefaults()
		{
			item.damage = 6;           //The damage of your weapon
			item.melee = true;          //Is your weapon a melee weapon?
			item.width = 40;            //Weapon's texture's width
			item.height = 40;           //Weapon's texture's height
			item.useTime = 22;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 22;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 6;         //The force of knockback of the weapon. Maximum is 20
			item.rare = 0;              //The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
		}
	}
}
