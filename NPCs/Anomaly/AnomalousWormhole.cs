using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Varia.NPCs.Anomaly;
using Terraria.ModLoader;

namespace Varia.NPCs.Anomaly
{
    public class AnomalousWormhole : ModNPC
	{
		int despawn = 0;
		int countOthers;
		bool starting = true;
		int shootTime = 0;
		Vector2 startingPos;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anomalous Wormhole");
		}
        public override void SetDefaults()
        {
			npc.alpha = 255;
            npc.width = 60;
            npc.height = 60;
            npc.damage = 25;
            npc.defense = 50;
            npc.lifeMax = 4500;
            npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Venom] = true;
			npc.buffImmune[BuffID.ShadowFlame] = true;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.buffImmune[BuffID.Daybreak] = true;
        }
		public override bool CheckActive()
		{
			return false;
		}
		public override void AI()
		{
			npc.alpha -= 30;
			if (starting == true)
			{
				countOthers = 6;
				startingPos = npc.position;
				starting = false;
			}
			npc.position = startingPos;
			Player player = Main.player[npc.target];
			if (Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                despawn++;
            }
            if (despawn > 30)
            {
                npc.active = false;
            }
			if (countOthers != NPC.CountNPCS(mod.NPCType("AnomalousWormhole")))
			{
				npc.life = npc.lifeMax;
				countOthers--;
			}
			if (NPC.CountNPCS(mod.NPCType("AnomalousWormhole")) <= 3 && NPC.CountNPCS(mod.NPCType("AnomalousWormhole")) != 1)
			{
				shootTime++;
				if (shootTime >= 70)
				{
					if (Main.rand.Next(1,3) == 1)
					{
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 10f, 0f, mod.ProjectileType("GlitchProj"), 20, 0f, 0, 0f, 0f);
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -10f, 0f, mod.ProjectileType("GlitchProj"), 20, 0f, 0, 0f, 0f);
					}
					else
					{
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 10f, mod.ProjectileType("GlitchProj"), 20, 0f, 0, 0f, 0f);
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, -10f, mod.ProjectileType("GlitchProj"), 20, 0f, 0, 0f, 0f);
					}
					shootTime = 0;
				}
			}
			if (NPC.CountNPCS(mod.NPCType("TheAnomaly")) <= 0)
			{
				npc.active = false;
			}
			if (countOthers == 1)
			{
				npc.damage = 0;
				player.position.X = npc.Center.X - 17;
				player.position.Y = npc.Center.Y - 46;
				npc.dontTakeDamage = true;
			}
		}
    }
}