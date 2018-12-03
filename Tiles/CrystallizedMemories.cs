using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class CrystallizedMemories : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            soundType = 21;
            drop = mod.ItemType("CrystallizedMemories");
            AddMapEntry(new Color(142, 117, 181));
        }
	}
}