using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
	public class PurificationSerum2 : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Purification Serum (Tier 2)");
			Description.SetDefault("You feel at ease with yourself. (Corrupt, crimson, and hallow tiles around you will be converted to Purity.)");
		}
		public override void Update(Player player, ref int buffIndex)
		{
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().purificationSerum = 2;
        }
	}
}