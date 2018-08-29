using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
	public class GelatineStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gelatine Staff");
			Tooltip.SetDefault("Conjures a pulsating gelatinous blob");
		}
        public override void SetDefaults()
        {
            item.damage = 17;
            item.noMelee = true;
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 33;
            item.useAnimation = 33;
            item.shoot = mod.ProjectileType("PulsatingBlob");
            item.shootSpeed = 10;
            item.useStyle = 1;
            item.mana = 9;
            item.knockBack = 2;
            item.rare = 5;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.useTurn = false;
        }
	}
}

