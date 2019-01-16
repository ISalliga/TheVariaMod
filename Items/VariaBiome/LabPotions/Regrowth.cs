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
	public class Regrowth : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Regrowth");
			Description.SetDefault("You are regrowing");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.statLife < player.statLifeMax) player.statLife += 1;
        }
	}
}