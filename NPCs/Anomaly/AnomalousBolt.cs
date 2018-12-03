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
    public class AnomalousBolt : ModNPC
	{
		Player player;
		bool starting = true;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anomalous Bolt");
		}
        public override void SetDefaults()
        {
			npc.alpha = 0;
            npc.width = 10;
            npc.height = 10;
            npc.damage = 50;
			npc.value = 0;
            npc.defense = 1;
            npc.lifeMax = 50;
            npc.noGravity = true;
			npc.noTileCollide = true;
        }
		public override void AI()
		{
			if (npc.collideX || npc.collideY)
			{
				npc.active = false;
			}
			if (starting == true)
			{
				player = Main.player[npc.target];
				starting = false;
			}
			if (NPC.CountNPCS(mod.NPCType("TheAnomaly")) == 0 && NPC.CountNPCS(mod.NPCType("TheAnomalyRage")) == 0)
			{
				npc.active = false;
			}
			npc.velocity = npc.DirectionTo(player.Center) * 6;
		}
		public virtual void OnHitPlayer(Player target, int damage, bool crit)
		{
			npc.active = false;
		}
    }
}