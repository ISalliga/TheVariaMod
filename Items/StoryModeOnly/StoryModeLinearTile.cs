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
	public class StoryModeLinearTile : GlobalTile
	{
		public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
		{
			if (VariaWorld.storyMode)
			{
				if (type == 231)
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
        public virtual bool CanExplode(int i, int j, int type)
        {
            if (VariaWorld.storyMode)
            {
                if (type == 231)
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