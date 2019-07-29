using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class ToothySpike : ModTile
	{
		public override void SetDefaults()
		{
            soundType = 21;
            Main.tileSolid[Type] = true;
			drop = mod.ItemType("ToothySpike");
			AddMapEntry(new Color(240, 255, 190));
            Main.tileBlockLight[Type] = true;
        }
    }
}