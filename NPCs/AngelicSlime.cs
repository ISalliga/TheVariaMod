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

namespace Varia.NPCs
{
    public class AngelicSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angelic Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[2];
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 45 : 90;
            npc.aiStyle = 1;
            npc.damage = Main.expertMode ? 9 : 15;
            npc.defense = 0;
            animationType = NPCID.BlueSlime;
            aiType = NPCID.BlueSlime;
            npc.knockBackResist = 0f;
            npc.width = 32;
            npc.height = 34;
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
            return !Main.LocalPlayer.ZoneCrimson && !Main.LocalPlayer.ZoneCorrupt && !Main.LocalPlayer.ZoneUnderworldHeight ? 0.2f : 0.0f;
        }
        public override void AI()
        {
            if (npc.collideY) Lighting.AddLight(npc.position, new Vector3(0.2f, 0.2f, 0.3f));
            else if (npc.velocity.Y > 0) npc.velocity.Y -= 0.12f;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WhiteGel"), Main.rand.Next(4, 10));
        }
    }
}