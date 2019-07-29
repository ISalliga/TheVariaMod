using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class HappyPills : ModItem
    {
        private Player player;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Happy Pills");
            Tooltip.SetDefault("Throws a pill that makes enemies go insane");
        }
        public override void SetDefaults()
        {
            item.consumable = false;
            item.width = 42;
            item.height = 54;
            item.thrown = true;
            item.value = 50000;
            item.damage = 80;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.noUseGraphic = true;
            item.rare = 5;
            item.useTime = 50;
            item.shoot = mod.ProjectileType("HappyPillsProj");
            item.shootSpeed = 10;
            item.useAnimation = 50;
            item.autoReuse = true;
            item.maxStack = 1;
        }
    }
}