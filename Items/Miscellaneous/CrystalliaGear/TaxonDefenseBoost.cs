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
	public class TaxonDefenseBoost : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Taxon Defense Boost");
			Description.SetDefault("I'M INVINCIBLE! (8% increased damage reduction)");
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.endurance += 0.08f;
		}
	}
}