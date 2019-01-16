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
    public class Spiller : ModNPC
    {
		int shootTime = 0;
		int riseTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiller");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 45 : 90;
            npc.aiStyle = 14;
            npc.damage = Main.expertMode ? 30 : 39;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 66;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit19;
            Main.npcFrameCount[npc.type] = 6;
            npc.DeathSound = SoundID.NPCDeath23;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.cavityTiles > 75 ? 10f : 0f;
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
        public override void AI()
        {
            shootTime++;
            if (shootTime > 17)
            {
                Projectile.NewProjectile(npc.position.X + (33 + Main.rand.Next(0, 7)), npc.position.Y + 35, 0, 13, mod.ProjectileType("SpillerRain"), Main.expertMode ? 19 : 30, 0f, 0);
                shootTime = 0;
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(0, 3));
        }
    }
}