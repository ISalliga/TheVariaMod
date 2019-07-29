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

namespace Varia.NPCs.Cavity
{
    public class FleshyBat : ModNPC
    {
		int riseTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fleshy Bat");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 40 : 80;
            npc.aiStyle = 14;
            npc.damage = Main.expertMode ? 8 : 12;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 56;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
			Main.npcFrameCount[npc.type] = 4;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.cavityTiles > 75 ? 15f : 0f;
        }
		public override void AI()
		{
			Player player = Main.player[npc.target];
			if (player.Center.Y <= npc.Center.Y)
			{
				riseTime++;
			}
			else
			{
				riseTime = 89;
			}
			
			if (riseTime >= 100)
			{
				npc.velocity.Y -= 0.8f;
			}
			else
			{
				npc.velocity.Y += 0.06f;
			}
			
			if (riseTime == 109)
			{
				riseTime = 0;
			}
			
			if (npc.velocity.Y > 7)
			{
				npc.velocity.Y = 7;
			}
			if (npc.velocity.Y < -5)
			{
				npc.velocity.Y = -5;
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
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(0, 3));
        }
    }
}