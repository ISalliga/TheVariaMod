using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
	public class Arachnophobia : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnophobia");
			Tooltip.SetDefault("Fires venom bubbles");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.noMelee = true;
			item.magic = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 15;
			item.useAnimation = 15;
			item.shoot = mod.ProjectileType("VenomBolt");
			item.shootSpeed = 10;
			item.useStyle = 1;
			item.mana = 9;
			item.knockBack = 2;
			item.rare = 7;
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.useTurn = false;
        }		
	}
}

