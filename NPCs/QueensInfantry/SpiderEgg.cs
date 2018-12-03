using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.QueensInfantry
{
    public class SpiderEgg : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spider Egg");
		}
        public override void SetDefaults()
        {
            npc.alpha = 255;
			npc.scale = 1f;
            npc.width = 88;
            npc.height = 28;
            npc.damage = Main.expertMode ? 20 : 33;
            npc.defense = 3;
            npc.lifeMax = 9;
            npc.value = Item.buyPrice(0, 0, 2, 50);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            if (npc.alpha > 0) npc.alpha -= 25;
            if (!NPC.AnyNPCs(mod.NPCType("SpiderQueen"))) npc.active = false;
        }

        public override void NPCLoot()
        {
            if (NPC.CountNPCS(mod.NPCType("SpiderQueen")) == 0)
            {
                npc.active = false;
            }
            switch (Main.rand.Next(1, 4))
            {
                case 1:
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RocketSpider"));
                        break;
                    }
                case 2:
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WingedSpider"));
                        break;
                    }
                case 3:
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("JumpingSpider"));
                        break;
                    }
            }
        }
    }
}