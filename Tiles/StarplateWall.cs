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
            AddMapEntry(new Color(142, 117, 191));
        }
	}
}