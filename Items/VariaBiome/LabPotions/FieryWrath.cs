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
	public class FieryWrath : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fiery Wrath");
			Description.SetDefault("+7% damage \nAll attacks inflict fire damage to foes");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.meleeDamage *= 1.07f;
            player.rangedDamage *= 1.07f;
            player.magicDamage *= 1.07f;
            player.thrownDamage *= 1.07f;
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().fieryWrath = true;
        }
	}
}