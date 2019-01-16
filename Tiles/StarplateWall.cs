using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class InhabitableStarplateWall : ModWall
	{
		public override void SetDefaults()
		{
            drop = mod.ItemType("StarplateWall");
            AddMapEntry(new Color(102, 87, 151));
        }
	}
}