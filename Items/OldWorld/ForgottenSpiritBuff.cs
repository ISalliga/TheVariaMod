using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class ForgottenSpiritBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Forgotten Spirit");
            Description.SetDefault("The past still haunts them... and by 'them' what is meant is those unlucky enough to come across you.");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player,  ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("ForgottenSpirit")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).ForgottenSpirit = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).ForgottenSpirit)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}