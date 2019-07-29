using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
	public class MutationMonolith : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutated Monolith");
        }

		public override void SetDefaults()
		{
			npc.width = 20;
			npc.height = 40;
			npc.damage = 0;
			npc.lifeMax = 100;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0f;
			npc.knockBackResist = 0f;
            npc.aiStyle = 0;
		}

        public override void AI()
        {
            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, -20f, 50, default(Color), 1.5f);
        }
    }
}