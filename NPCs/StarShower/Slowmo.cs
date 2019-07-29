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

namespace Varia.NPCs.StarShower
{
	public class Slowmo : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Slow-Mo");
			Description.SetDefault("It's like the Matrix, but with... actually, yeah, it's exactly like the Matrix.");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.runAcceleration /= 3;
            player.runSlowdown /= 3;
            player.gravity /= 3;
            player.jump /= 3;
        }
	}
}