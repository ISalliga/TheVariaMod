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
    public class ScopeOfTheOmniscientHorizon : ModItem
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
            DisplayName.SetDefault("Scope of the Omniscient Horizon");
            Tooltip.SetDefault("Allows you to see further towards the horizon when equipped (Press a hotkey to toggle) \nIncreases view range (Right Click to zoom out) \nHold UP or DOWN to look up or down \nPress a special hotkey to shift the camera focus to the nearest enemy to the cursor \nYou can see all enemies, danger, and treasure \nRanged projectiles deal more damage the farther away from the player they are \n+15% ranged damage \n+10% ranged critical strike chance");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            player.GetModPlayer<VariaPlayer>().Goggles = true;
            player.scope = true;
            player.GetModPlayer<VariaPlayer>().SniperScope = true;
            player.GetModPlayer<VariaPlayer>().PocketTelescope = true;
            player.AddBuff(BuffID.Spelunker, 10);
            player.AddBuff(BuffID.Dangersense, 10);
            player.AddBuff(BuffID.Hunter, 10);
            player.rangedDamage *= 1.15f;
            player.rangedCrit += 10;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AquaMonocle"));
            recipe.AddIngredient(mod.ItemType("PocketTelescope"));
            recipe.AddIngredient(ItemID.SniperScope);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}