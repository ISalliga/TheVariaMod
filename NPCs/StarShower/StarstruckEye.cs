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
using BaseMod;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.StarShower
{
    public class StarstruckEye : ModNPC
    {
        float flyDirection;
        float currentFlyDirection;
        Vector2 moveTo;
        float speed = 8;
        float heightAbovePlayer = 0;
        float rotationSpeed = 1.5f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starstruck Eye");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 90;
            npc.aiStyle = 2;
            npc.damage = 40;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 58;
            npc.height = 34;
            npc.npcSlots = 0.5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.scale = 1f;
            Main.npcFrameCount[npc.type] = 5;
            npc.noTileCollide = true;
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
            if (npc.collideX) npc.velocity.X = -npc.velocity.X;
            if (npc.collideY) npc.velocity.Y = -npc.velocity.Y;
            Player player = Main.player[npc.target];
            moveTo = new Vector2(player.Center.X, player.Center.Y - heightAbovePlayer);
            flyDirection = (moveTo - npc.Center).ToRotation();
            currentFlyDirection = Methods.SlowRotation(currentFlyDirection, flyDirection, rotationSpeed); // takes the angle curretFlyDirection and moves it toward the flyDirection angle by rotationSpeed degrees
            npc.velocity = Methods.PolarVector(speed, currentFlyDirection); //move the npc in the desired direction and speed
            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 4) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }
        public static Texture2D glowTex = null;

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/StarShower/StarstruckEye_GM");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.MediumPurple, Color.Yellow, Color.White, Color.MediumPurple));
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.MediumPurple, Color.Yellow, Color.White, Color.MediumPurple));
        }
    }
}