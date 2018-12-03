using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class SoulActivator : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Activator"); 
            Tooltip.SetDefault("Unleashes the Guide's soul energy"); 
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20; 
            item.maxStack = 20; 
            item.value = 0; 
            item.rare = 7; 
            item.useAnimation = 30; 
            item.useTime = 30; 
            item.useStyle = 4; 
            item.consumable = false; 
        }
		public override bool CanUseItem(Player player)
		{
			int soulCount = NPC.CountNPCS(mod.NPCType("SoulOfTheGuide"));
			if (soulCount >= 1)
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
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SoulOfTheGuide"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 3);
            recipe.AddIngredient(ItemID.Lens, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
			
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(ItemID.LeadBar, 3);
            recipe1.AddIngredient(ItemID.Lens, 1);
			recipe1.AddTile(TileID.Anvils);
			recipe1.SetResult(this, 1);
			recipe1.AddRecipe();
		}
    }
}