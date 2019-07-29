using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    //[AutoloadEquip(EquipType.Shoes)]
    public class AquaMonocle : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0,  3,  50,  0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqua Monocle");
            Tooltip.SetDefault("You can see all enemies, danger, and treasure while underwater \nThis effect lingers for 45 seconds after leaving the water");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            if (player.wet)
            {
                player.AddBuff(BuffID.Spelunker, 60 * 45);
                player.AddBuff(BuffID.Dangersense, 60 * 45);
                player.AddBuff(BuffID.Hunter, 60 * 45);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Lens, 1);
            recipe.AddIngredient(mod.ItemType("BlessedAqua"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}