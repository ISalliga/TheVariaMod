using Terraria;
using Terraria.ModLoader;
using Varia;

namespace Varia.Items.Miscellaneous
{
	public class ManaInvincibility : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mana Invincibility");
			Description.SetDefault("You are incapable of running out of mana");
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.statMana = player.statManaMax;
		}
	}
}
