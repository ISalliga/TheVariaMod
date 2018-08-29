using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
	public class GrimeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Grime Baby");
            Description.SetDefault("The grime baby will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("GrimeBaby")] > 0)
            {
                player.GetModPlayer<VariaPlayer>(mod).grimeBaby = true;
            }
            if (!player.GetModPlayer<VariaPlayer>(mod).grimeBaby)
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