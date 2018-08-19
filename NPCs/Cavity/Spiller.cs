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
            npc.aiStyle = 22;
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
            npc.DeathSound = SoundID.NPCDeath23;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.cavityTiles > 75 ? 10f : 0f;
        }
        public override void AI()
        {
			shootTime++;
			if (shootTime > 12)
			{
				Projectile.NewProjectile(npc.position.X + (33 + Main.rand.Next(0, 7)), npc.position.Y + 35, 0, 13, mod.ProjectileType("SpillerRain"), Main.expertMode ? 19 : 30, 0f, 0);
				shootTime = 0;
			}
			
			if (!Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 8].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 7].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 6].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 5].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 4].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 3].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 2].active() && !Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 1].active())
			{
				npc.velocity.Y = npc.velocity.Y / 2; 
			}
			
			if (!Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 10].active())
			{
				npc.velocity.Y += 0.2f;
			}
			
			if (Main.tile[(int)npc.Center.X / 16, ((int)npc.Center.Y / 16) + 8].active())
			{
				npc.velocity.Y -= 0.2f;
			}
		}
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(0, 3));
        }
    }
}