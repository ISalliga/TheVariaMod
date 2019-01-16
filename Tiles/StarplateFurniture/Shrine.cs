using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Tiles.StarplateFurniture
{
    public class Shrine : ModTile
    {
        public override void SetDefaults()
        {
            minPick = 30;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(2, 2);
            //TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(150, 150, 150));
            animationFrameHeight = 52;
            disableSmartCursor = true;
            //adjTiles = new int[] { TileID.LunarMonolith };
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            switch (Main.rand.Next(1, 4))
            {
                case 1:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("SpelunkersArtifact"));
                    break;
                case 2:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("ConjurersArtifact"));
                    break;
                case 3:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("AlchemistsArtifact"));
                    break;
            }
            switch (Main.rand.Next(1, 4))
            {
                case 1:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("SpelunkersArtifact"));
                    break;
                case 2:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("ConjurersArtifact"));
                    break;
                case 3:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("AlchemistsArtifact"));
                    break;
            }
            switch (Main.rand.Next(1, 4))
            {
                case 1:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("SpelunkersArtifact"));
                    break;
                case 2:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("ConjurersArtifact"));
                    break;
                case 3:
                    Item.NewItem(new Rectangle(i * 16, j * 16, 1, 1), mod.ItemType("AlchemistsArtifact"));
                    break;
            }
        }
    }
}