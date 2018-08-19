using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class HolestoneWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
            AddMapEntry(new Color(44, 24, 16));
        }
	}
}