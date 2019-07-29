using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia.Tiles
{
	public class CacitianOre : ModTile
	{
		public override void SetDefaults()
		{
            soundType = 21;
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			drop = mod.ItemType("CacitianOre");
			AddMapEntry(new Color(176, 91, 44));
		}
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (!VariaWorld.downedCore)
            {
                return false;
            }
            return true;
        }
    }
}