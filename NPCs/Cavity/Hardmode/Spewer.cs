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
using BaseMod;
using Terraria.ModLoader;

namespace Varia.NPCs.Cavity.Hardmode
{
    public class Spewer : ModNPC
    {
		int spewTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spewer");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 100 : 177;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 10 : 15;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 52;
            npc.height = 76;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            Main.npcFrameCount[npc.type] = 4;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath17;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            BaseAI.AIEater(npc, ref npc.ai, 0.022f, 42, 0.7f, false, true);
            npc.rotation = 0f;
            if (Main.hardMode)
            {
                return VariaWorld.cavityTiles > 75 ? 8f : 0f;
            }
            else return 0.0f;
        }
        public override void AI()
		{
			Player player = Main.player[npc.target];
            spewTime++;
            if (spewTime % 2 == 0)
            {
                int damage = Main.expertMode ? 25 : 42;
                int type = mod.ProjectileType("Spew");
                int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-15, 16), Main.rand.Next(-15, 16), type, damage, 0f, Main.myPlayer);
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(3, 8));
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 5) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }
    }
}