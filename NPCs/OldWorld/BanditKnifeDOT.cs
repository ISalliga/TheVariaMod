using Terraria;
using Terraria.ModLoader;
using Varia;

namespace Varia.NPCs.OldWorld
{
	public class BanditKnifeDOT : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Open-Wound Bleeding");
			Description.SetDefault("UNTIL I TOOK A KNIFE... TO THE KNEE (You are losing life)");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<VariaGlobalNPC>().banditKnife = true;
		}
	}
}
