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
            AddMapEntry(new Color(221, 147, 30));
        }
	}
}