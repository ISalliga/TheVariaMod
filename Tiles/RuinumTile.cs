using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class RuinumTile : ModTile
	{
		public override void SetDefaults()
		{
            soundType = 21;
            minPick = 45;
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			drop = mod.ItemType("RuinumOre");
			AddMapEntry(new Color(102, 201, 152));
		}
	}
}