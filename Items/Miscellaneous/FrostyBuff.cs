using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class FrostyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Frosty Mist");
            Description.SetDefault("The frosty mist will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player,  ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("FrostyMist")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).frostyMist = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).frostyMist)
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