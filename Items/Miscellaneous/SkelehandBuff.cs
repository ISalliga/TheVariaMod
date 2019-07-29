using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class SkelehandBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Skelehands");
            Description.SetDefault("Hands? More like... stands.");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player,  ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("Skelehand")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).Skelehands = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).Skelehands)
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