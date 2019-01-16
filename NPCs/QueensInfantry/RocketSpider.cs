using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.QueensInfantry
{
    public class RocketSpider : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket Spider");
        }
        int dashTime = 0;
        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.aiStyle = 14;
            npc.damage = 54;
            Main.npcFrameCount[npc.type] = 4;
            npc.defense = 3;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 66;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.scale = 1f;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            dashTime++;
            if (dashTime > 170)
            {
                npc.velocity += npc.DirectionTo(player.Center) * 1;
            }
            if (dashTime > 180)
            {
                dashTime = 0;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 40;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
			npc.frameCounter++;
			if (npc.frameCounter >= 4) // ticks per frame
			{
				npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
				npc.frameCounter = 0;
			}
        }
    }
}