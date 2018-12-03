using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Varia.Tiles
{
    public class ForgottenGrass : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("ForgottenGrass")] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            //Main.tileLighted[Type] = true;
            AddMapEntry(new Color(142, 117, 181));
            drop = ItemID.DirtBlock;
        }
    }
}
