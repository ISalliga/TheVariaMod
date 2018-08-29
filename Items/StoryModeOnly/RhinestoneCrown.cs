using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Varia;
using Terraria.ModLoader;

namespace Varia.Items.StoryModeOnly
{
    public class RhinestoneCrown : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 32;
            item.rare = -12;
			item.useStyle = 4;
			item.useTime = 20;
			item.maxStack = 20;
			item.useAnimation = 20;
			item.consumable = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhinestone Crown");
			Tooltip.SetDefault("Summons King Slime, but much easier to get than a Slime Crown... with a catch");
        }
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine t in tooltips)
			{
				if (t.mod == "Terraria" && t.Name == "Tooltip0")
				{
					t.overrideColor = new Color(155, 97, 174);
				}
			}
		}
		public override bool UseItem(Player player)
		{
			int ks = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 800, 50);
			Main.PlaySound(SoundID.Roar, player.position, 0);
			if (Main.rand.Next(1, 6) == 3)
			{
				Main.NewText("It appears the King Slime has noticed your lack of a real gem, and has become angry...", 155, 20, 100);
				Main.npc[ks].damage *= 2;
				Main.npc[ks].lifeMax = Main.npc[ks].lifeMax / 2 * 3;
				Main.npc[ks].life = Main.npc[ks].lifeMax;
			}
			return true;
        }
		public override bool CanUseItem(Player player)
		{
			return NPC.CountNPCS(NPCID.KingSlime) == 0 ? true : false;
		}
    }
}