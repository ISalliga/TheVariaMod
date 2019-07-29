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
    public class StellarCore : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Core");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 40 : 60;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 33 : 45;
            npc.defense = 8;
            npc.knockBackResist = 0f;
            npc.width = 50;
            npc.height = 42;
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

        public static Texture2D glowTex = null;

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/StarShower/StellarCore_GM");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Blue, Color.White, Color.SkyBlue, Color.Blue));
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Blue, Color.White, Color.SkyBlue, Color.Blue));
        }
        public override void AI()
        {
            foreach (Player player in Main.player)
            {
                if (player.active && Vector2.Distance(npc.Center, player.Center) < 360)
                {
                    player.AddBuff(mod.BuffType("Slowmo"), 8);
                }
            }
            npc.TargetClosest();
            npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 0.5f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
        }
    }
}