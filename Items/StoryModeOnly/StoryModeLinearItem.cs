using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.StoryModeOnly
{
	public class StoryModeLinearItem : GlobalItem
	{
        public override void PostUpdate(Item item)
        {
            if (item.type == ItemID.GuideVoodooDoll && item.lavaWet)
            {
                int variable = Player.FindClosest(item.position, item.width, item.height);
                Player player = Main.player[Player.FindClosest(item.position, item.width, item.height)];
                Item.NewItem((int)item.position.X, (int)item.position.Y, player.width, player.height, ItemID.GuideVoodooDoll, 1, false, item.prefix);
                item.active = false;
                item.type = 0;
                //item.name = ""; 
                item.stack = 0;
                Main.NewText("The doll vaporizes in the lava, but when you look at your inventory you realize it's still there", 155, 97, 174);
            }
        }
        public override bool CanUseItem(Item item, Player player)
		{
			if (VariaWorld.storyMode)
			{
				if (item.type == ItemID.SuspiciousLookingEye)
				{
					if (!NPC.downedSlimeKing)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else if (item.type == 70 || item.type == 1331)
				{
					if (!NPC.downedBoss1)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else if (item.type == 1133)
				{
					if (!NPC.downedBoss2)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}
	}
}