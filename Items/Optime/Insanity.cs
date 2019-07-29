using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
	public class Insanity : ModBuff
	{
		internal string texture;
		public bool canBeCleared = false;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Insanity");
			Description.SetDefault("HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA");
		}
		public override void Update(NPC npc,  ref int buffIndex)
		{
            if (!npc.boss && Main.rand.NextBool(1, 181))
            {
                npc.velocity.Y -= Main.rand.Next(7, 15);
                if (npc.velocity.Y < -21)
                {
                    npc.velocity.Y = -21;
                }
                npc.velocity.X += Main.rand.Next(-5, 6);
            }
        }
	}
}