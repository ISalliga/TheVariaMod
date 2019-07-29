using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
	public class RuinumSharkBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ruinum Shark");
            Description.SetDefault("THE B OY HE HERE HE SM OL AAAAAAA EHBNS J GFJK");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player,  ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("SharkMinion")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).sharkMinion = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).sharkMinion)
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