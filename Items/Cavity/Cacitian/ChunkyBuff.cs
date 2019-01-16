using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
	public class ChunkyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Chunky Boye");
            Description.SetDefault("Who would win? Droves and droves of green slimes or... one chunky boye?");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player,  ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("ChunkyBoi")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).hunkOChunk = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).hunkOChunk)
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