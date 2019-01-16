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
	public class TaxonDamageBoost : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Taxon Damage Boost");
			Description.SetDefault("Knock 'em dead! (10% increased damage)");
		}
		public override void Update(Player player,  ref int buffIndex)
		{
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.magicDamage *= 1.1f;
			player.thrownDamage *= 1.1f;
			player.minionDamage *= 1.1f;
		}
	}
}