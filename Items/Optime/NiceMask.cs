using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class NiceMask : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nice Mask"); 
            Tooltip.SetDefault("Calls forth Nice Guy"); 
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42; 
            item.maxStack = 20; 
            item.value = 0; 
            item.rare = 11; 
            item.useAnimation = 30; 
            item.useTime = 30; 
            item.useStyle = 4; 
            item.consumable = true; 
        }
        public override bool CanUseItem(Player player)
        {
            int phaseoneCount = NPC.CountNPCS(mod.NPCType("NiceGuy"));
            int phasetwoCount = NPC.CountNPCS(mod.NPCType("Optime"));
            if (phaseoneCount >= 1 || phasetwoCount >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool UseItem(Player player)
        {
            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 400, mod.NPCType("NiceGuy"), 0, player.whoAmI);
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.ClayBlock, 100);
            recipe.AddIngredient(ItemID.Ectoplasm, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}