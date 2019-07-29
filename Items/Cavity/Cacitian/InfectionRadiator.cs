using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
    public class InfectionRadiator : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infection Radiator"); 
            Tooltip.SetDefault("Summons the Core of Mutation when used underground"); 
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
            int coreCount = NPC.CountNPCS(mod.NPCType("CoreOfMutation"));
            if (coreCount >= 1 || !player.ZoneRockLayerHeight)
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
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("CoreOfMutation"));
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MutatedBlob"), 10);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();

            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("MutatedBlob"), 10);
            recipe2.AddIngredient(ItemID.LeadBar, 5);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }
    }
}