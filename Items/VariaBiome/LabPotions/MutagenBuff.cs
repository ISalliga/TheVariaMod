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
	public class MutagenBuff : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mutagen");
			Description.SetDefault("You have a chance to change enemy stats on hit");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().mutagen = true;
        }
	}
}