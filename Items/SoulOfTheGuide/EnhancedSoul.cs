using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class EnhancedSoul : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Enhanced Soul");
			Description.SetDefault("You feel rejuvenated and your mood is infallibly great. (+50 max HP)");
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 += 50;
			player.buffTime[buffIndex] = 2;
			if (player.statLife < 1)
			{
				player.statLifeMax2 -= 50;
			}
		}
	}
}
