using System;
using Microsoft.Xna.Framework;
using Varia;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.QueensInfantry
{
    public class BugAttractant : ModItem
    {
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.scale = 1;
			item.maxStack = 99;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item1;
            item.useStyle = 4;
            item.value = Item.buyPrice(0, 1, 0, 0);
			item.rare = 3;
			item.consumable = true;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bug Attractant");
			Tooltip.SetDefault("Summons the Spider Queen");
		}
        public override bool CanUseItem(Player player)
        {
            int queenCount = NPC.CountNPCS(mod.NPCType("SpiderQueen"));
            if (queenCount >= 1)
            {
                return false;
            }
            else
            {
                if (!Main.dayTime) return true;
                else return false;
            }
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, player.position, 0);
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SpiderQueen"));
            return true;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Cobweb, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
    }
}