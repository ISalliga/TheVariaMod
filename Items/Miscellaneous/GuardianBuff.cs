using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class GuardianBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Guardian");
            Description.SetDefault("The guardian will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("GuardianMinion")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).guardianMinion = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).guardianMinion)
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