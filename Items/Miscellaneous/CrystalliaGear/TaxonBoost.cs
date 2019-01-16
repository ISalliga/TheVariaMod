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

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
	public class TaxonBoost : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Taxon Crit Boost");
			Description.SetDefault("When you hit hard,  you hit REALLY hard.");
		}
		public override void Update(Player player,  ref int buffIndex)
		{
			player.meleeCrit += 12;
			player.rangedCrit += 12;
			player.magicCrit += 12;
			player.thrownCrit += 12;
		}
	}
}