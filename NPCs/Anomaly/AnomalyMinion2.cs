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
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.Anomaly
{
    public class AnomalyMinion2 : ModNPC
    {
		int shootTime = 0;
		int anomalyCount;
		int wormholeCount;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anomaly's Minion");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 450;
            npc.aiStyle = 14;
            npc.damage = 40;
            npc.defense = 3;
            npc.knockBackResist = 0f;
            npc.width = 36;
            npc.height = 36;
            npc.npcSlots = 0.5f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.scale = 1.5f;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
		public override void AI()
		{
			anomalyCount = NPC.CountNPCS(mod.NPCType("TheAnomaly"));
			wormholeCount = NPC.CountNPCS(mod.NPCType("AnomalousWormhole"));
			if (anomalyCount < 1)
			{
				npc.active = false;
			}
			if (wormholeCount < 2)
			{
				npc.active = false;
			}
			Player player = Main.player[npc.target];
			shootTime++;
			if (shootTime > 50)
			{
				Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(player.Center) * 10, mod.ProjectileType("GlitchProj"), 20, 0f, 0, 0f, 0f);
				shootTime = 0;
			}
		}
    }
}