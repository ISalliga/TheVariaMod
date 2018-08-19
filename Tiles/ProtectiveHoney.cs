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

namespace Varia.Tiles
{
	public class ProtectiveHoney : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			minPick = 65;
			Main.tileMergeDirt[Type] = true;
			drop = ItemID.CrispyHoneyBlock;
			AddMapEntry(new Color(100, 50, 26));
		}
	}
}