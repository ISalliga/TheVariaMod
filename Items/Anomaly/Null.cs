using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Anomaly
{
    public class Null : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("null"); 
            Tooltip.SetDefault("Calls upon a particularly fatal glitch in Agherium's code"); 
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20; 
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
            int anomalyCount = NPC.CountNPCS(mod.NPCType("TheAnomaly"));
            int anomalyCount2 = NPC.CountNPCS(mod.NPCType("TheAnomalyRage"));
            int anomalyCount3 = NPC.CountNPCS(mod.NPCType("TheAnomalyGrief"));
            if (anomalyCount >= 1 && anomalyCount2 >= 1 && anomalyCount3 >= 1)
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
           NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TheAnomaly"));
           Main.PlaySound(SoundID.Roar, player.position, 0);
           return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "AnomalousChunk", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}