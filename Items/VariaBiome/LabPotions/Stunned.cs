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
	public class Stunned : ModBuff
	{
		internal string texture;
		public bool canBeCleared = false;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Stunned");
			Description.SetDefault("You canot move.");
		}
		public override void Update(NPC npc,  ref int buffIndex)
		{
            npc.velocity.X = 0;
            if (npc.noGravity) npc.velocity.Y = 0;
            Dust dust;
            Vector2 position = npc.position;
            dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.4605263f)];
            dust.noGravity = true;
            dust.fadeIn = 0.9473684f;
        }
	}
}