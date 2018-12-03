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

namespace Varia.NPCs.Cavity.Hardmode
{
    public class TaintedAmalgam : ModNPC
    {
		int shootTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tainted Amalgam");
            NPCID.Sets.TrailCacheLength[npc.type] = 1;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 110 : 150;
            npc.aiStyle = 14;
            npc.damage = Main.expertMode ? 45 : 67;
            npc.defense = 2;
            npc.knockBackResist = 0f;
            npc.width = 44;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 5, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath4;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return VariaWorld.cavityTiles > 75 ? 8f : 0f;
            }
            else return 0.0f;
        }
        public override void AI()
		{
			Player player = Main.player[npc.target];
            shootTime++;
            if (shootTime > 80)
            {
                Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), npc.DirectionTo(player.Center) * 13, mod.ProjectileType("TaintedFleshBall"), Main.expertMode ? 30 : 45, 0f);
                shootTime = 0;
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(1, 5));
        }
    }
}