using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class ForgottenCloud : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileMerge[mod.TileType("ForgottenCloudRegen")][Type] = true;
            AddMapEntry(new Color(221, 147, 30));
            minPick = 9999;
        }
    }
}