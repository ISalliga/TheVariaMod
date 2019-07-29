using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class WornBrickWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = false;
            AddMapEntry(new Color(54, 17, 6));
        }
	}
}