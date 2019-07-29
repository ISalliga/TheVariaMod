using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Varia.Items.BasicGear
{
	public class BasicHook : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Basic Hook");
			Tooltip.SetDefault("For adventuring.");
		}
		public override void SetDefaults()
		{
			item.width = 54;
			item.height = 16;
			item.useTime = 10;
			item.useAnimation = 10;
			item.shoot = mod.ProjectileType("BasicHookProj");
			item.autoReuse = false;
			item.knockBack = 2;
			item.shootSpeed = 10;
			item.useStyle = 1;
            item.noUseGraphic = true;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
		}
	}
}
