using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Varia;
using Terraria.World.Generation;

namespace Varia
{
    class OldWorldGen : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            ErrorLogger.ClearLogs();
            int treesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));
            if (treesIndex != -1)
            {
                tasks.Insert(treesIndex, new PassLegacy("Adding the Old World", delegate (GenerationProgress progress)
                {
                    GenOldWorld(); //places the Old World
                }));
            }
        }

        public void GenOldWorld()
        {
            int[] itemPool = new int[8] { mod.ItemType("SolarShank"), mod.ItemType("IronSoles"), mod.ItemType("CopterHat"), mod.ItemType("LunarLance"), 0, 0, 0, 0 };
            for (int i = 0; i < Main.maxTilesX / 500; i++)
            {
                int itemType = itemPool[Main.rand.Next(8)];
                bool chest = true;
                if (itemType == 0) chest = false;
                int x = Main.maxTilesX / 2 + Main.rand.Next(-Main.maxTilesX / 4, Main.maxTilesX / 4);
                int y = (int)Main.worldSurface - 160;
                while (!Main.tile[x, y + 8].active() && !Main.tile[x + 20, y + 8].active()) y++;
                Point placeCheck = new Point(x + 10, y - 5);
                if (!AreaContains(placeCheck, 30, mod.TileType<Tiles.WornBrick>()) && !AreaContains(placeCheck, 30, TileID.LivingWood) && !AreaContains(placeCheck, 30, TileID.Cloud) && Math.Abs(x - Main.spawnTileX) > 60 && Math.Abs(y - Main.worldSurface) > 40)
                {
                    if (x > 50 && x < Main.maxTilesX - 50 && x > 80 && y < Main.maxTilesY - 80)
                    {
                        Ruin(new Point(x, y), chest, itemType);
                    }
                    else i--;
                }
                else i--;
            }
        }

        public void Ruin(Point position, bool chest, int itemType) //Creates a new Old World ruin
        {
            //Point position is aligned to the bottom-left of the ruin.
            int roomWidth = Main.rand.Next(14, 21);
            int roomHeight = Main.rand.Next(7, 10);
            int numChest = Main.rand.Next(2) == 0 ? 0 : 1;
            for (int x = 0; x <= 1; x++)
            {
                for (int y = 0; y < Main.rand.Next(3, 6); y++)
                {
                    bool chest2 = numChest == x && y == 1 && chest;
                    bool booksOrPaintings = true;
                    if (Main.rand.Next(5) == 0) booksOrPaintings = false;
                    RuinRoom(new Point(position.X + roomWidth * x, position.Y - roomHeight * y), roomWidth, roomHeight, mod.TileType<Tiles.WornBrick>(), mod.WallType<Tiles.WornBrickWall>(), chest2, y == 0, Main.rand.Next(2) == 0, itemType, y < 1, y >= 1 && booksOrPaintings, y >= 1 && !booksOrPaintings);
                }
            }
            for (int y = 1; y < 6; y++)
            {
                for (int x = 0; x <= roomWidth * 2; x++)
                {
                    Point pos = new Point(position.X + x, position.Y + roomHeight + y);
                    WorldGen.PlaceTile(pos.X, pos.Y, mod.TileType<Tiles.WornBrick>(), forced: true);
                    WorldGen.SlopeTile(pos.X, pos.Y);
                }
            }
            for (int y = 6; y < 20; y++)
            {
                int xLR = roomWidth;
                xLR *= 1 - y / 20;
                for (int x = roomWidth - xLR; x <= roomWidth + xLR; x++)
                {
                    Point pos = new Point(position.X + x, position.Y + roomHeight + y);
                    WorldGen.PlaceTile(pos.X, pos.Y, AreaContains(pos, 2, TileID.SnowBlock) ? TileID.SnowBlock : AreaContains(pos, 2, TileID.Sand) ? TileID.Sand : TileID.Dirt, forced: true);
                    WorldGen.SlopeTile(pos.X, pos.Y);
                }
            }
        }

        public void RuinRoom(Point position, int width, int height, int tileType, int wallType, bool chest, bool doors, bool appliance, int itemType, bool banners = false, bool books = false, bool painting = false) //Creates a room for the ruins
        {
            //Point posiion is aligned to the top-left of the room.
            int x = position.X;
            int y = position.Y;
            int roomWidthPlat = width / 2;
            for (int i = x; i <= x + width; i++)
            {
                for (int j = y; j <= y + height; j++)
                {
                    if (Main.rand.Next(1, 7) < 6) WorldGen.PlaceTile(i, j, tileType);
                    else WorldGen.KillTile(i, j);
                }
            }
            for (int i = x + 1; i < x + width; i++)
            {
                for (int j = y + 1; j < y + height; j++)
                {
                    WorldGen.KillTile(i, j);
                    if (Main.rand.Next(1, 5) < 4) Main.tile[i, j].wall = (ushort)wallType;
                }
            }
            if (doors)
            {
                for (int i = 1; i <= 3; i++)
                {
                    WorldGen.KillTile(x, y + height - i);
                    WorldGen.KillTile(x + width, y + height - i);
                }
                WorldGen.PlaceTile(x, y + height, tileType);
                WorldGen.PlaceTile(x, y + height - 4, tileType);
                WorldGen.PlaceTile(x + width, y + height, tileType);
                WorldGen.PlaceTile(x + width, y + height - 4, tileType);
                WorldGen.PlaceDoor(x, y + height - 2, TileID.ClosedDoor);
                WorldGen.PlaceDoor(x + width, y + height - 2, TileID.ClosedDoor);
            }
            if (appliance)
            {
                for (int e = 0; e < Main.rand.Next(1, 3); e++)
                {
                    int applianceType = TileID.WorkBenches;
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            break;
                        case 1:
                            applianceType = TileID.Anvils;
                            break;
                        case 2:
                            applianceType = TileID.Loom;
                            break;
                        case 3:
                            applianceType = TileID.Sawmill;
                            break;
                        case 4:
                            applianceType = TileID.Bookcases;
                            break;
                    }
                    int i = x + Main.rand.Next(2, width - 2);
                    WorldGen.PlaceObject(i, y + height - 1, applianceType);
                }
            }
            if (chest)
            {
                int x2 = 0;
                switch (Main.rand.Next(2))
                {
                    case 0:
                        {

                            WorldGen.PlaceTile(x + 2, y + height, tileType, forced: true);
                            WorldGen.PlaceTile(x + 3, y + height, tileType, forced: true);
                            x2 = 2;
                            break;
                        }
                    case 1:
                        {
                            WorldGen.PlaceTile(x + width - 2, y + height, tileType, forced: true);
                            WorldGen.PlaceTile(x + width - 3, y + height, tileType, forced: true);
                            x2 = width - 3;
                            break;
                        }
                }
                Chest theChest = Main.chest[WorldGen.PlaceChest(x + x2, y + height - 1, notNearOtherChests: true)];
                theChest.item[0].SetDefaults(itemType, false);
                for (int item = 1; item <= 6; item++)
                {
                    int[] otherItemType = new int[] { 0, 0 };
                    switch (Main.rand.Next(14))
                    {
                        case 1:
                            {
                                otherItemType[0] = ItemID.IronBar;
                                otherItemType[1] = Main.rand.Next(5, 11);
                                break;
                            }
                        case 2:
                            {
                                otherItemType[0] = ItemID.LeadBar;
                                otherItemType[1] = Main.rand.Next(5, 11);
                                break;
                            }
                        case 3:
                            {
                                otherItemType[0] = ItemID.CopperBar;
                                otherItemType[1] = Main.rand.Next(8, 17);
                                break;
                            }
                        case 4:
                            {
                                otherItemType[0] = ItemID.TinBar;
                                otherItemType[1] = Main.rand.Next(8, 17);
                                break;
                            }
                        case 5:
                            {
                                otherItemType[0] = mod.ItemType("WornBrick");
                                otherItemType[1] = Main.rand.Next(150, 241);
                                break;
                            }
                        case 6:
                            {
                                otherItemType[0] = ItemID.SilverBar;
                                otherItemType[1] = Main.rand.Next(5, 11);
                                break;
                            }
                        case 7:
                            {
                                otherItemType[0] = ItemID.TungstenBar;
                                otherItemType[1] = Main.rand.Next(5, 11);
                                break;
                            }
                        case 8:
                            {
                                otherItemType[0] = ItemID.Daybloom;
                                otherItemType[1] = Main.rand.Next(8, 17);
                                break;
                            }
                        case 9:
                            {
                                otherItemType[0] = ItemID.Blinkroot;
                                otherItemType[1] = Main.rand.Next(8, 17);
                                break;
                            }
                        case 10:
                            {
                                otherItemType[0] = ItemID.Wood;
                                otherItemType[1] = Main.rand.Next(150, 241);
                                break;
                            }
                        case 11:
                            {
                                otherItemType[0] = ItemID.Acorn;
                                otherItemType[1] = Main.rand.Next(10, 17);
                                break;
                            }
                        case 12:
                            {
                                otherItemType[0] = ItemID.RecallPotion;
                                otherItemType[1] = Main.rand.Next(3, 6);
                                break;
                            }
                        case 13:
                            {
                                otherItemType[0] = ItemID.IronskinPotion;
                                otherItemType[1] = Main.rand.Next(3, 6);
                                break;
                            }
                        default:
                            {
                                otherItemType[0] = ItemID.Ale;
                                otherItemType[1] = Main.rand.Next(10, 20);
                                break;
                            }
                    }
                    theChest.item[item].SetDefaults(otherItemType[0], false);
                    theChest.item[item].stack = otherItemType[1];
                }
            }

            if (banners)
            {
                int banner1X = position.X + 2;
                int banner2X = position.X + width - 2;
                WorldGen.PlaceTile(banner1X, y, tileType);
                WorldGen.PlaceObject(banner1X, y + 1, mod.TileType("OldWorldBanner"));
                WorldGen.PlaceTile(banner2X, y, tileType);
                WorldGen.PlaceObject(banner2X, y + 1, mod.TileType("OldWorldBanner"));
            }

            if (books)
            {
                switch(Main.rand.Next(2))
                {
                    case 1:
                        {
                            for (int i = x + 1; i <= x + 5; i++)
                            {
                                int bookType = TileID.Books;
                                WorldGen.PlaceTile(i, y + height / 2, TileID.Platforms, forced: true);
                                if (Main.rand.Next(75) != 1) WorldGen.PlaceTile(i, y + height / 2 - 1, bookType);
                                else WorldGen.PlaceObject(i, y + height / 2 - 1, mod.TileType("BookOfBoulders"));
                            }
                            break;
                        }
                    default:
                        {
                            for (int i = x + width - 1; i >= x + width - 5; i--)
                            {
                                int bookType = TileID.Books;
                                WorldGen.PlaceTile(i, y + height / 2, TileID.Platforms, forced: true);
                                if (Main.rand.Next(50) != 1) WorldGen.PlaceTile(i, y + height / 2 - 1, bookType);
                                else WorldGen.PlaceObject(i, y + height / 2 - 1, mod.TileType("BookOfBoulders"));
                            }
                            break;
                        }
                }
            }

            if (painting)
            {
                switch (Main.rand.Next(4))
                {
                    case 1:
                        {
                            int iStart = x + width / 2 + Main.rand.Next(-2, 3);
                            int jStart = y + height / 2 - Main.rand.Next(-1, 2);
                            for (int i = iStart; i <= iStart + 1; i++)
                            {
                                for (int j = jStart; j <= jStart + 1; j++)
                                {
                                    Main.tile[i, j].wall = (ushort)wallType;
                                }
                            }
                            WorldGen.PlaceObject(iStart + 1, jStart + 1, (ushort)mod.TileType("ToolsOfTheTrade"));
                            break;
                        }
                    case 2:
                        {
                            int iStart = x + width / 2 + Main.rand.Next(-2, 3);
                            int jStart = y + height / 2 - Main.rand.Next(0, 3);
                            for (int i = iStart - 1; i <= iStart + 1; i++)
                            {
                                for (int j = jStart - 1; j <= jStart + 1; j++)
                                {
                                    Main.tile[i, j].wall = (ushort)wallType;
                                }
                            }
                            WorldGen.PlaceObject(iStart, jStart, (ushort)mod.TileType("TheGrandOpening"));
                            break;
                        }
                    case 3:
                        {
                            int iStart = x + width / 2 + Main.rand.Next(-2, 3);
                            int jStart = y + height / 2 - Main.rand.Next(0, 3);
                            for (int i = iStart - 1; i <= iStart + 1; i++)
                            {
                                for (int j = jStart - 1; j <= jStart; j++)
                                {
                                    Main.tile[i, j].wall = (ushort)wallType;
                                }
                            }
                            WorldGen.PlaceObject(iStart, jStart, (ushort)mod.TileType("AGiftFromAbove"));
                            break;
                        }
                    default:
                        {

                            break;
                        }
                }
            }

            for (int i = x + roomWidthPlat - 2; i <= x + roomWidthPlat + 2; i++)
            {
                WorldGen.PlaceTile(i, y, TileID.Platforms, forced: true);
            }
        }


        public bool AreaContains(Point position, int size, int type) //Checks if a tile of a specific type is within a radius
        {
            for (int xPos = position.X - size; xPos <= position.X + size; xPos++)
            {
                for (int yPos = position.Y - size; yPos <= position.Y + size; yPos++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(xPos, yPos)) <= size && Main.tile[xPos, yPos].type == type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AreaContainsAny(Point position, int size) //Same but if there is any active tile
        {
            for (int xPos = position.X - size; xPos <= position.X + size; xPos++)
            {
                for (int yPos = position.Y - size; yPos <= position.Y + size; yPos++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(xPos, yPos)) <= size && Main.tile[xPos, yPos].active() == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool FlatContainsAny(Point position, int size) //Same but if there is any active tile
        {
            for (int x = position.X; x < position.X + size; x++)
            {
                if (Main.tile[x, position.Y].active()) return true;
            }
            return false;
        }


        public bool FlatContains(Point position, int size, int type) //Same but if there is any active tile
        {
            for (int x = position.X; x < position.X + size; x++)
            {
                if (Main.tile[x, position.Y].type == type) return true;
            }
            return false;
        }

        public bool IsInBounds(int x, int y) //Checks for out-of-bounds tiles
        {
            if (x > 0 && x < Main.maxTilesX && y > 0 && y < Main.maxTilesY) return true;
            else return false;
        }

        public void BlockLining(double x, double y, int repeats, int tileType, bool random, int max, int min = 3)
        {
            for (double i = x; i < x + repeats; i++)
            {
                if (random)
                {
                    for (double k = y; k < y + Main.rand.Next(min, max); k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
                else
                {
                    for (double k = y; k < y + max; k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
            }
        }
    }
}
