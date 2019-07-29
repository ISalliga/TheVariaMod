using Terraria;
using Terraria.ModLoader;
using Varia;

namespace Varia.Items.Miscellaneous
{
	public class Slowed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Slowed");
			Description.SetDefault("You are slowed");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            if (!npc.boss)
            {
                if (npc.velocity.X > 3f) npc.velocity.X -= 1f;
                if (npc.velocity.X < -3f) npc.velocity.X += 1f;
                if (npc.velocity.Y > 3f && npc.noGravity) npc.velocity.Y -= 1f;
                if (npc.velocity.Y < -3f) npc.velocity.Y += 1f;
            }
        }
	}
}
