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
    public class StoryModeLinearNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            if (npc.type == 35 && !NPC.downedQueenBee)
            {
                npc.active = false;
                Main.NewText("You cannot summon Skeletron until you have defeated the Queen Bee.", 155, 97, 174);
            }
            if (npc.type == 36 && !NPC.downedQueenBee)
            {
                npc.active = false;
            }
            if (npc.type == 113 && !NPC.downedBoss3)
            {
                npc.active = false;
                Main.NewText("You cannot summon the Wall of Flesh until you have gained access to the Dungeon.", 155, 97, 174);
            }
            if (npc.type == 114 && !NPC.downedBoss3)
            {
                npc.active = false;
            }
        }
    }
}