using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.World.Generation;
using Terraria.Utilities;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Varia;

namespace Varia.Items.StoryModeOnly
{
    public class StoryBook : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 32;
            item.rare = -12;
			item.useStyle = 4;
			item.useTime = 20;
			item.useAnimation = 20;
			item.consumable = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Story Book");
			Tooltip.SetDefault("Enables Story Mode if no bosses have been killed \n" + "Story Mode is overall a more immersive experience, but you need to fight every boss in order, meaning it doesn't work well with other large mods. \n" + "There are cutscenes for some bosses, and a few new characters who will help you on your quest. \n" + "Do not get the wrong idea - bosses don't receive buffs, and this mode does not make anything more difficult. \n" + "This is not undoable! \n" + "It will unlock new content, but you will be bound to defeat all the bosses in order. Really think about this!");
        }
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine t in tooltips)
			{
				if (t.mod == "Terraria" && t.Name == "Tooltip0")
				{
					t.overrideColor = new Color(155, 97, 174);
				}
				if (t.mod == "Terraria" && t.Name == "Tooltip4")
				{
					t.overrideColor = new Color(255, 25, 25);
				}
			}
		}
		public override bool UseItem(Player player)
		{
            if (!NPC.downedSlimeKing && !NPC.downedBoss1 && !NPC.downedBoss2 && !NPC.downedBoss3 && !NPC.downedQueenBee && !Main.hardMode)
            {
                Main.NewText("Story Mode is active.", 155, 97, 174);
                VariaWorld.storyMode = true;
                for (int k = 0; k < Main.maxTilesX; k++)
                {
                    for (int l = 0; l < Main.maxTilesY; l++)
                    {
                        if (Main.tile[k, l].type == TileID.Hive)
                        {
                            if (Main.tile[k, l - 1].type == TileID.Larva)
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType("ProtectiveHoney");
                            }
                        }
                    }
                }
            }
            else
            {
                Main.NewText("You can't activate Story Mode if you have already killed a boss.", 155, 97, 174);
            }
			return true;
        }
    }
}