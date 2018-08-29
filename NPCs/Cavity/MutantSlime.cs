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
    public class MutantSlime : ModNPC
    {
		bool collided = false;
		int cooldown = 0;
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[2];
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 45 : 90;
            npc.aiStyle = 1;
            npc.damage = Main.expertMode ? 50 : 70;
            npc.defense = 0;
            animationType = NPCID.BlueSlime;
            aiType = NPCID.BlueSlime;
            npc.knockBackResist = 0f;
            npc.width = 44;
            npc.height = 30;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit19;
            npc.DeathSound = SoundID.NPCDeath23;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.cavityTiles > 75 ? 15f : 0f;
        }
        public override void AI()
        {
			cooldown++;
            if (npc.collideY && collided == false)
            {
				Projectile.NewProjectile(npc.Center.X + 18, npc.Center.Y + 10, 11, 0, mod.ProjectileType("MutantSlimeFleshwave"), Main.expertMode ? 10 : 18, 0f, 0);
				Projectile.NewProjectile(npc.Center.X - 18, npc.Center.Y + 10, -11, 0, mod.ProjectileType("MutantSlimeFleshwave"), Main.expertMode ? 10 : 18, 0f, 0);
				collided = true;
				cooldown = 0;
            }
			if (!npc.collideY && cooldown > 10)
            {
				collided = false;
            }
		}
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(1, 4));
        }
    }
}
//