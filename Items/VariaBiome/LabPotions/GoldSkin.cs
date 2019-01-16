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
	public class GoldSkin : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Gold Skin");
			Description.SetDefault("You gain more gold from enemies based on how many times you've hit them");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().goldSkin = true;
        }
	}
}