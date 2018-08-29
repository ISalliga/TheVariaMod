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

namespace Varia.Items.FallenAngel.TechHunk
{
    public class HunkOfUnidentifiedTechnology : ModNPC
    {
		int shootTime = 0;
		int riseTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunk of Unidentified Technology");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 300;
            npc.damage = 10;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 66;
            npc.height = 56;
            npc.aiStyle = 14;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
            {
                return SpawnCondition.OverworldNight.Chance * 0.03f;
            }
            else return 0.0f;
        }
        public override void NPCLoot()
        {
            switch(Main.rand.Next(1, 5))
            {
                case 1:
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TrydaniumPhaserapier"), 1);
                    break;
                case 2:
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheEnergizer"), 1);
                    break;
                case 3:
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TrydanReactorCore"), 1);
                    break;
                case 4:
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnergyFusillade"), 1);
                    break;
            }
            VariaWorld.hunkCount += 1;
        }
    }
}