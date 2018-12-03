using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
    public class QueenBag : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 9;
            bossBagNPC = mod.NPCType("SpiderQueen");
            item.expert = true;      
			item.value = Item.buyPrice(0, 0, 0, 0);
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Treasure Bag");
      Tooltip.SetDefault("Right click to open");
    }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void OpenBossBag(Player player)
        {
            switch (Main.rand.Next(1, 5))
            {
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Arachnophobia"), 1);
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("VenomPiercer"), 1);
                            break;
                        case 2:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("RainforestsBane"), 1);
                            break;
                        case 3:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("QueensJaw"), 1);
                            break;
                    }
                    break;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("VenomPiercer"), 1);
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Arachnophobia"), 1);
                            break;
                        case 2:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("RainforestsBane"), 1);
                            break;
                        case 3:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("QueensJaw"), 1);
                            break;
                    }
                    break;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("RainforestsBane"), 1);
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("VenomPiercer"), 1);
                            break;
                        case 2:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Arachnophobia"), 1);
                            break;
                        case 3:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("QueensJaw"), 1);
                            break;
                    }
                    break;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("QueensJaw"), 1);
                    switch (Main.rand.Next(1, 4))
                    {
                        case 1:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("VenomPiercer"), 1);
                            break;
                        case 2:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Arachnophobia"), 1);
                            break;
                        case 3:
                            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("RainforestsBane"), 1);
                            break;
                    }
                    break;
            }
            player.QuickSpawnItem(mod.ItemType("QueensJewel"));
        }
    }
}
