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

namespace Varia.NPCs
{
    public class AnomalousCaster : ModNPC
    {
		int shootTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anomalous Caster");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 450;
            npc.aiStyle = -1;
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
		public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AnomalousChunk"), Main.rand.Next(4, 9));
        }
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
			if (NPC.downedGolemBoss == true)
			{
				return SpawnCondition.OverworldNight.Chance * 0.01f;
			}
			else
			{
				return SpawnCondition.OverworldDay.Chance * 0f;
			}
        }
		public override void AI()
		{
			Player player = Main.player[npc.target];
			npc.velocity = npc.DirectionTo(player.Center) * 2;
			shootTime++;
			if (shootTime == 100)
			{
				npc.position.X = npc.Center.X + (Main.rand.Next(-150, 151));
				npc.position.Y = npc.Center.Y + (Main.rand.Next(-150, 151));
			}
			if (shootTime == 130)
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 10f, 0f, mod.ProjectileType("GlitchProj"), 30, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -10f, 0f, mod.ProjectileType("GlitchProj"), 30, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 10f, mod.ProjectileType("GlitchProj"), 30, 0f, 0, 0f, 0f);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, -10f, mod.ProjectileType("GlitchProj"), 30, 0f, 0, 0f, 0f);
				shootTime = 0;
			}
		}
    }
}