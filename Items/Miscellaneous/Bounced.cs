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

namespace Varia.Items.Miscellaneous
{
	public class Bounced : ModBuff
	{
		internal string texture;
		public bool canBeCleared = false;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Bounced");
			Description.SetDefault("BONES ARE TO BE CRACKED BITCH");
		}
		public override void Update(NPC npc,  ref int buffIndex)
		{
            if (npc.collideX && npc.velocity.X != 0)
            {
                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("BounceDamage"), 25, 0f, Main.LocalPlayer.whoAmI);
            }
        }
	}
}