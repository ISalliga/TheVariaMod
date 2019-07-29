using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class JelliumCrystal : ModTile
	{
		public override void SetDefaults()
		{
            soundType = 21;
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			drop = mod.ItemType("JelliumCrystal");
			AddMapEntry(new Color(220, 120, 120));
		}
	}
}