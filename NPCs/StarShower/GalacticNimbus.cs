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
using BaseMod;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.StarShower
{
    public class GalacticNimbus : ModNPC
    {
        int proj = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Nimbus");
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.Yellow;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 40 : 60;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 33 : 45;
            npc.defense = 8;
            npc.knockBackResist = 0f;
            npc.width = 46;
            npc.height = 38;
            Main.npcFrameCount[npc.type] = 5;
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
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.starShower ? 0.25f : 0f;
        }
        
        public override void AI()
        {
            BaseAI.AISpaceOctopus(npc, ref npc.ai, 0.15f, 10f, 180);
            proj++;
            if (proj >= 10)
            {
                Projectile.NewProjectile(npc.Center.X + (Main.rand.Next(7, 8)), npc.position.Y + 35, 0, 13, mod.ProjectileType("GalacticRain"), Main.expertMode ? 8 : 12, 0f, 0);
                proj = 0;
            }
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