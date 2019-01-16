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
	public class CrystalSkin : ModBuff
	{
		internal string texture;
		public bool canBeCleared = true;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Crystal Skin");
			Description.SetDefault("+12% melee damage \n+8% melee critical strike chance \n-10% damage reduction \n-20% defense");
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance = player.endurance * 9 / 10;
            player.statDefense = player.statDefense * 8 / 10;
            player.meleeDamage = player.meleeDamage * 112 / 100;
            player.meleeCrit = player.meleeCrit * 108 / 100;
        }
	}
}