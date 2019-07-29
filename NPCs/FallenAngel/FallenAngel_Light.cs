using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.FallenAngel
{
    public class FallenAngel_Light : ModNPC
    {
        Vector2 tPos;
        int despawn = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angel Clone");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
			npc.alpha = 255;
			npc.dontTakeDamage = true;
            npc.lifeMax = Main.expertMode ? 15000 : 10000;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = Main.expertMode ? 1 : 1;
            npc.knockBackResist = 0.2f;
            npc.width = 152;
            npc.height = 84;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
			//Main.npcFrameCount[npc.type] = 5;
        }
        public override void AI()
        {
            //int angelCount = NPC.CountNPCS(mod.NPCType("FallenAngel"));
            if (!Main.npc[(int)npc.ai[0]].active)
            {
                npc.life = 0;
                npc.checkDead();
            }
            if (npc.alpha > 100)
			{
				npc.alpha -= 10;
			}
            Player player = Main.player[npc.target];
            if (!Main.player[npc.target].dead)
            {
                despawn = 0;
                tPos.X = player.Center.X;
                tPos.Y = player.Center.Y - 70;
                npc.velocity.X += (npc.DirectionTo(tPos).X * Vector2.Distance(npc.Center, tPos) / 600 / 2);
                npc.velocity.Y += (npc.DirectionTo(tPos).Y * Vector2.Distance(npc.Center, tPos) / 600 / 2 * 3);
            }
            else
            {
                npc.velocity.Y -= despawn;
                despawn++;
                if (despawn > 40)
                {
                    npc.active = false;
                }
            }
        }
	    public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.frameCounter >= 3) // ticks per frame
			{
				npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
				npc.frameCounter = 0;
			}
        }
    }
}