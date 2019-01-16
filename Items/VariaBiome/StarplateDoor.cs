using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome
{
    public class StarplateDoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starplate Door");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("StarplateDoorClosed");
        }
    }
}