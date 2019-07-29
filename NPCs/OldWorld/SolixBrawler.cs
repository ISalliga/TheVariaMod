using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.Graphics.Shaders;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.OldWorld
{
    public class SolixBrawler : ModNPC
    {
        Player player;
        int punchTimer = 0;
        bool punches = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solix Brawler");
        }

        public override bool CheckActive()
        {
            if (Vector2.Distance(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(player.Center.X, player.Center.Y)) <= Main.screenWidth) return false;
            else return true;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 90 : 135;
            npc.aiStyle = 3;
            npc.damage = Main.expertMode ? 10 : 15;
            npc.defense = 5;
            npc.noGravity = false;
            npc.knockBackResist = 0f;
            Main.npcFrameCount[npc.type] = 7;
            npc.width = 36;
            npc.height = 48;
            npc.HitSound = SoundID.NPCHit48;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 2, 75);
            npc.lavaImmune = true;
        }

        public override void AI()
        {
            player = Main.player[npc.target];
            npc.TargetClosest();
            if (!punches)
            {
                if (npc.frameCounter >= 7) // ticks per frame
                {
                    if (npc.frame.Y < npc.height * 2) npc.frame.Y += npc.height;
                    else npc.frame.Y = 0;
                    npc.frameCounter = 0;
                }
            }
            else
            {

            }
            if (!npc.collideY)
            {
                npc.frame.Y = npc.height;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return Main.LocalPlayer.GetModPlayer<VariaPlayer>().ZoneOldWorld && Main.dayTime ? 0.14f : 0f;
        }

        public override void NPCLoot()
        {
            int rand2 = Main.rand.Next(4, 10);
            Item.NewItem(npc.position, mod.ItemType("OldWorldAlloy"), rand2);
        }
    }
}