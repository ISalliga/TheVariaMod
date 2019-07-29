using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.QueensInfantry
{
    public class SturdyVenom : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            AddMapEntry(new Color(200, 0, 255));
            Main.tileBlockLight[Type] = false;
        }
        public override void RandomUpdate(int i, int j)
        {
            WorldGen.KillTile(i, j);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (NPC.AnyNPCs(mod.NPCType("SpiderQueen")))
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (Main.tile[i + x, j].type == mod.TileType("SturdyVenom")) WorldGen.KillTile(i + x, j);
                }
            }
        }
    }
}