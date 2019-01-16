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
    [AutoloadEquip(EquipType.Shoes)]
    public class TerraTreads : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0,  1,  0,  0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Treads");
            Tooltip.SetDefault("Allows flight, super fast running, near-static acceleration, swimming, and extra mobility on ice \n'GOTTA GO FAST'");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            player.rocketBoots = 2;
            player.accFlipper = true;
            player.iceSkate = true;
            player.accRunSpeed += 5f;
            player.runAcceleration = 1.3f;
            player.runSlowdown = 1f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
            recipe.AddIngredient(ItemID.Flipper, 1);
            recipe.AddIngredient(mod.ItemType("SlimyBarricade"), 1);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddIngredient(ItemID.TurtleShell, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}