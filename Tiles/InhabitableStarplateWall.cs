using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class StarplateWall : ModWall
	{
		public override void SetDefaults()
		{
			Main.wallHouse[Type] = true;
            drop = mod.ItemType("StarplateWall");
            AddMapEntry(new Color(102, 87, 151));
        }
	}
}