using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumSteeringWheel : ModItem
	{
		private Player player;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ruinum Steering Wheel");
			Tooltip.SetDefault("Operates like a boomerang");
		}
		public override void SetDefaults()
		{
			item.consumable = false;
			item.width = 42;
			item.height = 54;
			item.melee = true;
            item.noMelee = true;
			item.value = 50000;
			item.damage = 19;
			item.useStyle = 1;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 71);
			item.noUseGraphic = true;
			item.rare = 5;
			item.useTime = 20;
			item.shoot = mod.ProjectileType("RuinumWheelProj");
			item.shootSpeed = 12;
			item.useAnimation = 20;
			item.autoReuse = true;
			item.maxStack = 1;
		}
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "RuinumBar", 6);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool CanUseItem(Player player)
        {
            int boomerangsActive = 0;
            bool use = true;
            for (int m = 0; m < 1000; m++)
            {
                if (Main.projectile[m].active && Main.projectile[m].owner == Main.myPlayer && Main.projectile[m].type == item.shoot)
                {
                    boomerangsActive++;
                }
                if (boomerangsActive >= 3)
                {
                    use = false;
                    break;
                }
            }
            return use;
        }
    }
}