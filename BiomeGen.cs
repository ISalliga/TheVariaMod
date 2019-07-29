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
    class BiomeGen : ModWorld
    {

        public override void ModifyWorldGenTasks(List<GenPass> tasks,  ref float totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex - 1, new PassLegacy("Generating the Cavity", delegate (GenerationProgress progress)
                {
                    for (int i = 0; i < Main.maxTilesX / 500; ++i) //Repeats ~8 times on a small world (i.e. 8 biomes/world)
                          {
                        Point pos = new Point(WorldGen.genRand.Next(70, Main.maxTilesX - 70), WorldGen.genRand.Next((int)Main.worldSurface + 200, Main.maxTilesY - 300)); //Position of the biome
                              if (!AreaContains(pos, 50, TileID.BlueDungeonBrick) && !AreaContains(pos, 50, TileID.GreenDungeonBrick) && !AreaContains(pos, 50, TileID.PinkDungeonBrick) && !AreaContains(pos, 50, TileID.LihzahrdBrick) && !AreaContains(pos, 50, TileID.StoneSlab) && !AreaContains(pos, 20, TileID.Ash)) //checks for unwanted blocks
                                  GenCavity(pos, WorldGen.genRand.Next(3)); //places biome
                              else
                            i--; //Repeat until valid placement
                          }
                }));
            }
            /*ErrorLogger.ClearLogs(); //For when Extractinum is to be added
            tasks.Add(new PassLegacy("Cultivating Extractinum underground",  delegate (GenerationProgress progress)
            {
                for (int k = 0; k < ((int)((double)(Main.maxTilesX * Main.maxTilesY) * 18E-05) * 12 / 13); k++)
                {
                    int i = WorldGen.genRand.Next(10,  Main.maxTilesX - 10);
                    int j = WorldGen.genRand.Next((int)Main.worldSurface - 1,  Main.maxTilesY - 10);
                    Tile tile = Main.tile[i,  j];
                    if ((tile.type == 0 || tile.type == 1) && j > Main.worldSurface)
                    {
                        WorldGen.OreRunner(i,  j,  (double)WorldGen.genRand.Next(6,  7),  WorldGen.genRand.Next(6,  7),  (ushort)mod.TileType("ExtractinumTile")); //Generates Extractinum everywhere
                    }
                }
            }));

            */

            ErrorLogger.ClearLogs();
            tasks.Add(new PassLegacy("Adding the forgotten airborne wasteland", delegate (GenerationProgress progress)
            {
                GenBreeze(); //places the Everlasting Breeze
            }));

            ErrorLogger.ClearLogs();
            tasks.Add(new PassLegacy("Adding Ruinum", delegate (GenerationProgress progress)
            {
                for (int i = 0; i < 6; i++)
                {
                    AddRuinum(); //places Ruinum
                }
            }));
        }

        public void GenCavity(Point pos,  int size) //Gen code for the biome
        {
            int[] types = new int[] { mod.TileType("Holestone"),  TileID.Stone,  mod.WallType("HolestoneWall"),  mod.TileType("CacitianOre"), mod.TileType("ToothySpike") }; //types[0] is the main "custom" block type,  the one that is used for the inlining and the 'spikes'. types[1] is the outline. types[2] is the wallID. types[3] is the ore.
            int reps = WorldGen.genRand.Next(4,  8); //Repeats (how many holes there are)
            int sizeCircle = WorldGen.genRand.Next(6,  12); //Size of the holes
            int[] randSiz = new int[] { -10,  10 }; //Random offset
            if (size == 1) //Size 2
            {
                reps = WorldGen.genRand.Next(8,  12);
                sizeCircle = WorldGen.genRand.Next(12,  18);
                randSiz = new int[] { -20,  21 };
            }
            else if (size == 2) //Size 3
            {
                reps = WorldGen.genRand.Next(12,  18);
                sizeCircle = WorldGen.genRand.Next(18,  26);
                randSiz = new int[] { -30,  31 };
            }
            List<Point> centres = new List<Point>(); //List of centres
            for (int i = 0; i < reps; ++i) //Places actual holes/outlines
            {
                Point placePos = new Point(pos.X + WorldGen.genRand.Next(randSiz[0],  randSiz[1]),  pos.Y + WorldGen.genRand.Next(randSiz[0],  randSiz[1]));
                SmoothRunner(placePos,  sizeCircle + 12,  types[1],  types[2]);
                SmoothRunner(placePos,  sizeCircle + 6,  types[0],  types[2]);
                SmoothTunnel(placePos,  sizeCircle - 1);
                SmoothWallRunner(pos,  sizeCircle,  types[2]);
                centres.Add(placePos);
            }
            //Places 'spikes'
            for (int i = 0; i < reps - 2; ++i)
            {
                Vector2 dir = new Vector2(1f); //Direction of the spike
                dir = dir.RotatedBy(WorldGen.genRand.Next(0,  314) * 0.1f);
                Vector2 initPos = centres[WorldGen.genRand.Next(0,  centres.Count)].ToVector2(); //The initial start 'centre'
                Vector2 nPos = initPos; //Place position
                bool hasTouched = false; //If the spike has touched a tile
                float siz = 1; //Size of the spike as segments
                int timer = -7; //Offset for the loop
                int totalReps = 0; //Total repeats
                while (!hasTouched && timer < 4) //Makes spikes
                {
                    if (Main.tile[(int)nPos.X,  (int)nPos.Y].active())
                        hasTouched = true;
                    DirectSmoothRunner(nPos.ToPoint(),  (int)(totalReps < 9 ? siz : siz * 2),  siz > 4 ? types[0] : types[4],  types[2]); //Places spike
                    nPos += dir * siz; //Changes placement pos
                    siz += WorldGen.genRand.Next(2) + (WorldGen.genRand.NextFloat() - 0.5f); //Increases size
                    if (hasTouched)
                    timer++;
                    totalReps++;
                }
            }
            //Places ores
            for (int i = 0; i < (int)(reps / 1.5f); ++i)
            {
                Vector2 placePos = new Vector2(pos.X + WorldGen.genRand.Next((int)(randSiz[0] * 1.5f),  (int)(randSiz[1] * 1.5f)),  pos.Y + WorldGen.genRand.Next(randSiz[0],  randSiz[1]));
                while (!Main.tile[(int)placePos.X,  (int)placePos.Y].active()) //Makes spikes
                {
                    placePos = new Vector2(pos.X + WorldGen.genRand.Next(randSiz[0],  randSiz[1]),  pos.Y + WorldGen.genRand.Next(randSiz[0],  randSiz[1]));
                }
                WorldGen.TileRunner((int)placePos.X,  (int)placePos.Y,  WorldGen.genRand.Next(4,  8),  5,  types[3],  true,  0,  0,  false,  true);
            }
        }

        public void GenBreeze()
        {
            int currentPosL = 80;
            int currentPosR = 80;

            int startPos = Main.maxTilesX / 2 - 560;
            int endPos = Main.maxTilesX / 2 + 560;

            WindowToTheWorld(new Point(Main.maxTilesX / 2, 82));
            LabStation(new Point(Main.maxTilesX / 2, 118));

            int numOfStructuresL = 7;
            int structurePoolL = 7;
            int[] structuresL = new int[numOfStructuresL];
            for (int n = 0; n < numOfStructuresL; n++)
            {
                structuresL[n] = Main.rand.Next(structurePoolL - n);
                for (int j = 0; j < n; j++)
                {
                    if (structuresL[n] >= structuresL[j])
                    {
                        structuresL[n]++;
                    }
                }
            }
            for (int i = 0; i < structuresL.Length; i++)
            {
                int structureNum = 0;
                switch (structuresL[i])
                {
                    case 0:
                        structureNum = 1;
                        break;
                    case 1:
                        structureNum = 2;
                        break;
                    case 2:
                        structureNum = 3;
                        break;
                    case 3:
                        structureNum = 4;
                        break;
                    case 4:
                        structureNum = 5;
                        break;
                    case 5:
                        structureNum = 6;
                        break;
                    case 6:
                        structureNum = 7;
                        break;
                    case 7:
                        structureNum = 8;
                        break;
                }
                if (structureNum != 0)
                {
                    switch (structureNum)
                    {
                        case 1:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), 1);
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), Main.rand.Next(1, 4));
                                break;
                            }
                        case 2:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), Main.rand.Next(1, 4));
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), 2);
                                break;
                            }
                        case 3:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), 3);
                                break;
                            }
                        case 4:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 140), 4);
                                break;
                            }
                        case 5:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), Main.rand.Next(1, 3));
                                break;
                            }
                        case 6:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), Main.rand.Next(1, 3));
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), Main.rand.Next(1, 3));
                                break;
                            }
                        case 7:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 80), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 - currentPosL, 120), Main.rand.Next(1, 4));
                                break;
                            }
                    }
                    currentPosL += 80;
                }
            } //End left generation

            int numOfStructuresR = 7;
            int structurePoolR = 7;
            int[] structuresR = new int[numOfStructuresR];
            for (int n = 0; n < numOfStructuresR; n++)
            {
                structuresR[n] = Main.rand.Next(structurePoolR - n);
                for (int j = 0; j < n; j++)
                {
                    if (structuresR[n] >= structuresR[j])
                    {
                        structuresR[n]++;
                    }
                }
            }
            for (int i = 0; i < structuresR.Length; i++)
            {
                int structureNum = 0;
                switch (structuresR[i])
                {
                    case 0:
                        structureNum = 1;
                        break;
                    case 1:
                        structureNum = 2;
                        break;
                    case 2:
                        structureNum = 3;
                        break;
                    case 3:
                        structureNum = 4;
                        break;
                    case 4:
                        structureNum = 5;
                        break;
                    case 5:
                        structureNum = 6;
                        break;
                    case 6:
                        structureNum = 7;
                        break;
                    case 7:
                        structureNum = 8;
                        break;
                }
                if (structureNum != 0)
                {
                    switch (structureNum)
                    {
                        case 1:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), 1);
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), Main.rand.Next(1, 4));
                                break;
                            }
                        case 2:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), Main.rand.Next(1, 4));
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), 2);
                                break;
                            }
                        case 3:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), 3);
                                break;
                            }
                        case 4:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 100), 4);
                                break;
                            }
                        case 5:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), Main.rand.Next(1, 3));
                                break;
                            }
                        case 6:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), Main.rand.Next(1, 3));
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), Main.rand.Next(1, 3));
                                break;
                            }
                        case 7:
                            {
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 80), 3);
                                MiniIsland(new Point(Main.maxTilesX / 2 + currentPosR, 120), Main.rand.Next(1, 4));
                                break;
                            }
                    }
                    currentPosR += 80;
                }
            } //End right generation
        }
        public void DirectSmoothRunner(Point position,  int size,  int type,  int wallID)
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X,  position.Y),  new Vector2(x,  y)) <= size)
                    {
                        WorldGen.PlaceTile(x,  y,  type);
                        WorldGen.PlaceWall(x,  y,  wallID);
                    }
                }
            }
        }

        public void SmoothRunner(Point position,  int size,  int type,  int wallID) //Overrides blocks in a circle
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X,  position.Y),  new Vector2(x,  y)) <= size)
                    {
                        Main.tile[x,  y].type = (ushort)type;
                        Main.tile[x,  y].wall = (ushort)wallID;
                    }
                }
            }
        }

        public void SmoothTunnel(Point pos,  int size) //Tunnels in a circle shape
        {
            for (int x = pos.X - size; x <= pos.X + size; x++)
            {
                for (int y = pos.Y - size; y <= pos.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(pos.X,  pos.Y),  new Vector2(x,  y)) <= size)
                    {
                        WorldGen.KillTile(x,  y);
                    }
                }
            }
        }

        public int BlockLining(double x,  double y,  int repeats,  int tileType,  bool random,  int max,  int min = 3)
        {
            for (double i = x; i < x + repeats; i++)
            {
                if (random)
                {
                    for (double k = y; k < y + Main.rand.Next(min,  max); k++)
                    {
                        WorldGen.PlaceTile((int)i,  (int)k,  tileType);
                    }
                }
                else
                {
                    for (double k = y; k < y + max; k++)
                    {
                        WorldGen.PlaceTile((int)i,  (int)k,  tileType);
                    }
                }
            }
            return repeats;
        }

        public void MiniIsland(Point position,  int isType)
        {
            if (!AreaContains(position, 30, TileID.Cloud) && !AreaContains(position, 30, TileID.RainCloud))
            {
                int x = position.X;
                int y = position.Y;
                for (int i = 0; i < 27; i++)
                {
                    if (i == 0)
                    {
                        BlockLining(x, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 1)
                    {
                        BlockLining(x + 4, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 2)
                    {
                        BlockLining(x + 8, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 3)
                    {
                        BlockLining(x + 12, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 4)
                    {
                        BlockLining(x + 17, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 5)
                    {
                        BlockLining(x + 21, y + 1, 6, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 6)
                    {
                        BlockLining(x + 27, y + 2, 6, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 7)
                    {
                        BlockLining(x + 33, y + 1, 7, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 8)
                    {
                        BlockLining(x + 40, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 9)
                    {
                        BlockLining(x + 45, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 10)
                    {
                        BlockLining(x + 49, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 11)
                    {
                        BlockLining(x + 53, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 12)
                    {
                        BlockLining(x + 57, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 13)
                    {
                        BlockLining(x + 61, y - 5, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    if (i == 14 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 15 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 4, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 16 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 8, y, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 17 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 12, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 18 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 17, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 19 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 21, y + 3, 6, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 20 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 27, y + 4, 6, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 21 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 33, y + 3, 7, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 22 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 40, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 23 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 45, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 24 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 49, y, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 25 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 53, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 26 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 57, y - 2, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 27 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 61, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                }
                for (double i = x + 4; i < x + 57; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 4, mod.TileType("ForgottenGrass"));
                for (double i = x + 4; i < x + 57; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 3, TileID.Dirt);
                for (double i = x + 8; i < x + 53; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 2, TileID.Dirt);
                for (double i = x + 12; i < x + 49; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 1, TileID.Dirt);
                for (double i = x + 15; i < x + 45; i++)
                    WorldGen.PlaceTile((int)i, (int)y, TileID.Dirt);
                for (double i = x + 21; i < x + 40; i++)
                    WorldGen.PlaceTile((int)i, (int)y + 1, TileID.Dirt);
                switch (isType)
                {
                    case 1:
                        {
                            for (double i = x + 10; i < x + 51; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 5, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                            WorldGen.Place1x1(x + 13, y - 2, mod.TileType("StarplateTorch"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                            WorldGen.Place1x1(x + 47, y - 2, mod.TileType("StarplateTorch"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 12, mod.TileType("StarplateBrick"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 13, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 30, (int)i, mod.TileType("StarplateBrick"));
                            WorldGen.Place1x1(x + 29, y - 2, mod.TileType("StarplateTorch"));
                            WorldGen.Place1x1(x + 31, y - 2, mod.TileType("StarplateTorch"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.KillTile((int)i, (int)y - 13);
                            for (double i = x + 13; i < x + 48; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 15, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 16, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 17, mod.TileType("StarplateBrick"));
                            for (double i = x + 29; i < x + 32; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 18, mod.TileType("StarplateBrick"));
                            WorldGen.PlaceTile((int)x + 30, (int)y - 19, mod.TileType("StarplateBrick"));
                            for (double i = x + 29; i < x + 32; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 20, mod.TileType("StarplateBrick"));
                            WorldGen.PlaceTile((int)x + 30, (int)y - 21, mod.TileType("StarplateBrick"));
                            for (double i = x + 13; i < x + 48; i++)
                            {
                                for (double u = y - 6; u >= y - 13; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                            }
                            for (double i = x + 32; i < x + 47; i++)
                            {
                                for (double u = y - 7; u >= y - 10; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                            }
                            WorldGen.Place3x2((int)x + 21, (int)y - 6, (ushort)mod.TileType("StarplateTable"));
                            WorldGen.Place1x2((int)x + 25, (int)y - 6, (ushort)mod.TileType("StarplateChair"), 0);
                            WorldGen.PlaceObject((int)x + 12, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                            WorldGen.PlaceObject((int)x + 30, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                            WorldGen.PlaceObject((int)x + 48, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));

                            if (Main.rand.NextBool(1, 4)) Shrine(new Point(x + Main.rand.Next(-1, 2), y - 30 + Main.rand.Next(-1, 2)));
                            if (Main.rand.NextBool(1, 4)) Shrine(new Point(x + 57 + Main.rand.Next(-1, 2), y - 30 + Main.rand.Next(-1, 2)));

                            break;
                        }
                    case 2:
                        {
                            for (double i = x + 10; i < x + 51; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 5, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                            WorldGen.Place1x1(x + 13, y - 2, mod.TileType("StarplateTorch"));
                            WorldGen.Place1x1(x + 47, y - 2, mod.TileType("StarplateTorch"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 12, mod.TileType("StarplateBrick"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 13, mod.TileType("StarplateBrick"));
                            for (double i = x + 13; i < x + 48; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.KillTile((int)i, (int)y - 13);
                            for (double i = x + 13; i < x + 48; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 15, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 16, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 17, mod.TileType("StarplateBrick"));
                            for (double i = x + 29; i < x + 32; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 18, mod.TileType("StarplateBrick"));
                            WorldGen.PlaceTile((int)x + 30, (int)y - 19, mod.TileType("StarplateBrick"));
                            for (double i = x + 29; i < x + 32; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 20, mod.TileType("StarplateBrick"));
                            WorldGen.PlaceTile((int)x + 30, (int)y - 21, mod.TileType("StarplateBrick"));
                            WorldGen.Place1x1(x + 25, y - 2, mod.TileType("StarplateTorch"));
                            WorldGen.Place1x1(x + 35, y - 2, mod.TileType("StarplateTorch"));
                            for (double i = x + 13; i < x + 48; i++)
                            {
                                for (double u = y - 6; u >= y - 13; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                            }
                            for (double i = x + 14; i < x + 47; i++)
                            {
                                for (double u = y - 7; u >= y - 10; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                            }
                            WorldGen.PlaceObject((int)x + 12, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                            WorldGen.PlaceObject((int)x + 48, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));

                            if (Main.rand.NextBool(1, 4)) Shrine(new Point(x + Main.rand.Next(-1, 2), y - 30 + Main.rand.Next(-1, 2)));
                            if (Main.rand.NextBool(1, 4)) Shrine(new Point(x + 57 + Main.rand.Next(-1, 2), y - 30 + Main.rand.Next(-1, 2)));

                            break;
                        }
                    case 3:
                        {
                            for (int xd = 0; xd < 5; xd++)
                            {
                                int x2 = x + 30 + Main.rand.Next(-15, 16);
                                int y2 = y + Main.rand.Next(-5, 1);
                                SmoothRunner(new Point(x2, y2), Main.rand.Next(3, 6), mod.TileType("Holestone"), 0);
                                SmoothTunnel(new Point(x2 + Main.rand.Next(-1, 2), y2 - Main.rand.Next(3, 5)), Main.rand.Next(2, 5));
                            }
                            break;
                        }
                    case 4:
                        {
                            for (double i = x + 10; i < x + 51; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 5, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                            for (double i = y - 9; i > y - 12; i--)
                                WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 12, mod.TileType("StarplateBrick"));
                            for (double i = x + 12; i < x + 49; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 13, mod.TileType("StarplateBrick"));
                            for (double i = x + 13; i < x + 48; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.KillTile((int)i, (int)y - 13);
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.KillTile((int)i, (int)y - 14);
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.KillTile((int)i, (int)y - 12);
                            for (double i = x + 28; i < x + 33; i++)
                                WorldGen.PlaceTile((int)i, (int)y - 13, TileID.Platforms, false, false, -1, 14);
                            for (double i = x + 13; i < x + 48; i++)
                            {
                                for (double u = y - 6; u >= y - 13; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                            }
                            for (double i = x + 14; i < x + 47; i++)
                            {
                                for (double u = y - 7; u >= y - 10; u--)
                                    Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                            }
                            WorldGen.PlaceObject((int)x + 12, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                            WorldGen.PlaceObject((int)x + 48, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                            for (int l = 0; l < 5; l++)
                            {
                                for (double i = y - 9; i > y - 12 - (9 * l); i--)
                                    WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                                for (double i = y - 9; i > y - 12 - 9 * l; i--)
                                    WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                                for (double i = x + 12; i < x + 49; i++)
                                    WorldGen.PlaceTile((int)i, (int)y - 12 - (9 * l), mod.TileType("StarplateBrick"));
                                for (double i = x + 12; i < x + 49; i++)
                                    WorldGen.PlaceTile((int)i, (int)y - 13 - (9 * l), mod.TileType("StarplateBrick"));
                                for (double i = x + 13; i < x + 48; i++)
                                    WorldGen.PlaceTile((int)i, (int)y - 14 - (9 * l), mod.TileType("StarplateBrick"));
                                for (double i = x + 28; i < x + 33; i++)
                                    WorldGen.KillTile((int)i, (int)y - 13 - (9 * l));
                                for (double i = x + 28; i < x + 33; i++)
                                    WorldGen.KillTile((int)i, (int)y - 14 - (9 * l));
                                for (double i = x + 28; i < x + 33; i++)
                                    WorldGen.KillTile((int)i, (int)y - 12 - (9 * l));
                                for (double i = x + 28; i < x + 33; i++)
                                    WorldGen.PlaceTile((int)i, (int)y - 13 - (9 * l), TileID.Platforms, false, false, -1, 14);
                                if (l == 4)
                                {
                                    for (double i = x + 13; i < x + 48; i++)
                                    {
                                        for (double u = y - 4 - (9 * l); u >= y - 12 - (9 * l); u--)
                                            Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                                    }
                                }
                                else
                                {
                                    for (double i = x + 13; i < x + 48; i++)
                                    {
                                        for (double u = y - 6 - (9 * l); u >= y - 14 - (9 * l); u--)
                                            Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                                    }
                                }
                                for (double i = x + 14; i < x + 47; i++)
                                {
                                    for (double u = y - 7 - (9 * l); u >= y - 10 - (9 * l); u--)
                                        Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                                }
                            }
                            break;
                        }
                }
            }
        }

        public void Shrine(Point position)
        {
            int x = position.X;
            int y = position.Y;
            BlockLining(x - 7, y, 4, mod.TileType("ForgottenCloud"), true, 6);
            BlockLining(x - 3, y + 2, 4, mod.TileType("ForgottenCloud"), true, 6);
            BlockLining(x + 1, y + 4, 4, mod.TileType("ForgottenCloud"), true, 6);
            BlockLining(x + 4, y + 2, 4, mod.TileType("ForgottenCloud"), true, 6);
            BlockLining(x + 5, y, 4, mod.TileType("ForgottenCloud"), true, 6);
            for (double i = x - 5; i < x + 8; i++)
                WorldGen.PlaceTile((int)i, (int)y + 1, mod.TileType("ForgottenGrass"));
            for (double i = x - 5; i < x + 8; i++)
                WorldGen.PlaceTile((int)i, (int)y + 2, TileID.Dirt);
            for (double i = x - 5; i < x + 8; i++)
                WorldGen.PlaceTile((int)i, (int)y + 3, TileID.Dirt);
            WorldGen.PlaceObject(x + 1, y, mod.TileType("Shrine"));
        }

        public void LabStation(Point position)
        {
            if (!AreaContains(position, 30, TileID.Cloud) && !AreaContains(position, 30, TileID.RainCloud))
            {
                int x = position.X;
                int y = position.Y;
                for (int i = 0; i < 27; i++)
                {
                    if (i == 0)
                    {
                        BlockLining(x, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 1)
                    {
                        BlockLining(x + 4, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 2)
                    {
                        BlockLining(x + 8, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 3)
                    {
                        BlockLining(x + 12, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 4)
                    {
                        BlockLining(x + 17, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 5)
                    {
                        BlockLining(x + 21, y + 1, 6, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 6)
                    {
                        BlockLining(x + 27, y + 2, 6, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 7)
                    {
                        BlockLining(x + 33, y + 1, 7, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 8)
                    {
                        BlockLining(x + 40, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 9)
                    {
                        BlockLining(x + 45, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 10)
                    {
                        BlockLining(x + 49, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 11)
                    {
                        BlockLining(x + 53, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 12)
                    {
                        BlockLining(x + 57, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    else if (i == 13)
                    {
                        BlockLining(x + 61, y - 5, 4, mod.TileType("ForgottenCloud"), true, 6);
                    }
                    if (i == 14 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 15 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 4, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 16 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 8, y, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 17 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 12, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 18 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 17, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 19 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 21, y + 3, 6, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 20 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 27, y + 4, 6, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 21 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 33, y + 3, 7, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 22 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 40, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 23 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 45, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 24 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 49, y, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 25 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 53, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 26 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 57, y - 2, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                    else if (i == 27 && Main.rand.Next(1, 3) == 1)
                    {
                        BlockLining(x + 61, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                    }
                }
                for (double i = x + 4; i < x + 57; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 4, mod.TileType("ForgottenGrass"));
                for (double i = x + 4; i < x + 57; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 3, TileID.Dirt);
                for (double i = x + 8; i < x + 53; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 2, TileID.Dirt);
                for (double i = x + 12; i < x + 49; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 1, TileID.Dirt);
                for (double i = x + 15; i < x + 45; i++)
                    WorldGen.PlaceTile((int)i, (int)y, TileID.Dirt);
                for (double i = x + 21; i < x + 40; i++)
                    WorldGen.PlaceTile((int)i, (int)y + 1, TileID.Dirt);
                {
                    for (double i = x + 10; i < x + 51; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 5, mod.TileType("StarplateBrick"));
                    for (double i = y - 9; i > y - 12; i--)
                        WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                    WorldGen.Place1x1(x + 13, y - 2, mod.TileType("StarplateTorch"));
                    for (double i = y - 9; i > y - 12; i--)
                        WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                    WorldGen.Place1x1(x + 47, y - 2, mod.TileType("StarplateTorch"));
                    for (double i = x + 12; i < x + 49; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 12, mod.TileType("StarplateBrick"));
                    for (double i = x + 12; i < x + 49; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 13, mod.TileType("StarplateBrick"));
                    for (double i = y - 9; i > y - 12; i--)
                        WorldGen.PlaceTile((int)x + 30, (int)i, mod.TileType("StarplateBrick"));
                    WorldGen.Place1x1(x + 29, y - 2, mod.TileType("StarplateTorch"));
                    WorldGen.Place1x1(x + 31, y - 2, mod.TileType("StarplateTorch"));
                    for (double i = x + 28; i < x + 33; i++)
                        WorldGen.KillTile((int)i, (int)y - 13);
                    for (double i = x + 13; i < x + 48; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                    for (double i = x + 28; i < x + 33; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 15, mod.TileType("StarplateBrick"));
                    for (double i = x + 28; i < x + 33; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 16, mod.TileType("StarplateBrick"));
                    for (double i = x + 28; i < x + 33; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 17, mod.TileType("StarplateBrick"));
                    for (double i = x + 29; i < x + 32; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 18, mod.TileType("StarplateBrick"));
                    WorldGen.PlaceTile((int)x + 30, (int)y - 19, mod.TileType("StarplateBrick"));
                    for (double i = x + 29; i < x + 32; i++)
                        WorldGen.PlaceTile((int)i, (int)y - 20, mod.TileType("StarplateBrick"));
                    WorldGen.PlaceTile((int)x + 30, (int)y - 21, mod.TileType("StarplateBrick"));
                    for (double i = x + 13; i < x + 48; i++)
                    {
                        for (double u = y - 6; u >= y - 13; u--)
                            Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                    }
                    for (double i = x + 32; i < x + 47; i++)
                    {
                        for (double u = y - 7; u >= y - 10; u--)
                            Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                    }
                    WorldGen.PlaceObject((int)x + 21, (int)y - 6, (ushort)mod.TileType("LabStation"));
                    WorldGen.PlaceObject((int)x + 12, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                    WorldGen.PlaceObject((int)x + 30, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                    WorldGen.PlaceObject((int)x + 48, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                }
            }
        }

        public void WindowToTheWorld(Point position)
        {
            int x = position.X;
            int y = position.Y;
            for (int i = 0; i < 27; i++)
            {
                if (i == 0)
                {
                    BlockLining(x, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 1)
                {
                    BlockLining(x + 4, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 2)
                {
                    BlockLining(x + 8, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 3)
                {
                    BlockLining(x + 12, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 4)
                {
                    BlockLining(x + 17, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 5)
                {
                    BlockLining(x + 21, y + 1, 6, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 6)
                {
                    BlockLining(x + 27, y + 2, 6, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 7)
                {
                    BlockLining(x + 33, y + 1, 7, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 8)
                {
                    BlockLining(x + 40, y, 5, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 9)
                {
                    BlockLining(x + 45, y - 1, 5, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 10)
                {
                    BlockLining(x + 49, y - 2, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 11)
                {
                    BlockLining(x + 53, y - 3, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 12)
                {
                    BlockLining(x + 57, y - 4, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                else if (i == 13)
                {
                    BlockLining(x + 61, y - 5, 4, mod.TileType("ForgottenCloud"), true, 6);
                }
                if (i == 14 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 15 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 4, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 16 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 8, y, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 17 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 12, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 18 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 17, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 19 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 21, y + 3, 6, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 20 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 27, y + 4, 6, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 21 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 33, y + 3, 7, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 22 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 40, y + 2, 5, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 23 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 45, y + 1, 5, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 24 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 49, y, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 25 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 53, y - 1, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 26 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 57, y - 2, 4, mod.TileType("Glistenyn"), true, 6);
                }
                else if (i == 27 && Main.rand.Next(1, 3) == 1)
                {
                    BlockLining(x + 61, y - 3, 4, mod.TileType("Glistenyn"), true, 6);
                }
            }
            for (double i = x + 4; i < x + 57; i++)
                WorldGen.PlaceTile((int)i, (int)y - 4, mod.TileType("ForgottenGrass"));
            for (double i = x + 4; i < x + 57; i++)
                WorldGen.PlaceTile((int)i, (int)y - 3, TileID.Dirt);
            for (double i = x + 8; i < x + 53; i++)
                WorldGen.PlaceTile((int)i, (int)y - 2, TileID.Dirt);
            for (double i = x + 12; i < x + 49; i++)
                WorldGen.PlaceTile((int)i, (int)y - 1, TileID.Dirt);
            for (double i = x + 15; i < x + 45; i++)
                WorldGen.PlaceTile((int)i, (int)y, TileID.Dirt);
            for (double i = x + 21; i < x + 40; i++)
                WorldGen.PlaceTile((int)i, (int)y + 1, TileID.Dirt);
            {
                DirectSmoothRunner(new Point(x + 30, y - 14), 9, TileID.Glass, 0);
                SmoothTunnel(new Point(x + 30, y - 14), 7);
                SmoothWallRunner(new Point(x + 30, y - 14), 8, mod.WallType("GalaxianMirror"));

                for (int i = x + 13; i < x + 48; i++)
                {
                    for (int u = y - 5; u >= y - 14; u--)
                        WorldGen.KillTile(i, u);
                }

                for (double i = x + 10; i < x + 51; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 5, mod.TileType("StarplateBrick"));
                for (double i = y - 9; i > y - 12; i--)
                    WorldGen.PlaceTile((int)x + 12, (int)i, mod.TileType("StarplateBrick"));
                for (double i = y - 9; i > y - 12; i--)
                    WorldGen.PlaceTile((int)x + 48, (int)i, mod.TileType("StarplateBrick"));
                for (double i = x + 12; i < x + 49; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 12, mod.TileType("StarplateBrick"));
                for (double i = x + 12; i < x + 49; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 13, mod.TileType("StarplateBrick"));
                for (double i = x + 13; i < x + 48; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 14, mod.TileType("StarplateBrick"));
                for (double i = x + 28; i < x + 33; i++)
                    WorldGen.KillTile((int)i, (int)y - 13);
                for (double i = x + 28; i < x + 33; i++)
                    WorldGen.KillTile((int)i, (int)y - 14);
                for (double i = x + 28; i < x + 33; i++)
                    WorldGen.KillTile((int)i, (int)y - 12);
                for (double i = x + 28; i < x + 33; i++)
                    WorldGen.PlaceTile((int)i, (int)y - 13, TileID.Platforms, false, false, -1, 14);
                WorldGen.PlaceObject((int)x + 30, (int)y - 6, (ushort)mod.TileType("WindowToTheWorld"));
                for (double i = x + 13; i < x + 48; i++)
                {
                    for (double u = y - 6; u >= y - 13; u--)
                        Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("StarplateWall");
                }
                for (double i = x + 14; i < x + 47; i++)
                {
                    for (double u = y - 7; u >= y - 10; u--)
                        Main.tile[(int)i, (int)u].wall = (ushort)mod.WallType("GalaxianMirror");
                }
                WorldGen.PlaceObject((int)x + 12, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
                WorldGen.PlaceObject((int)x + 48, (int)y - 7, (ushort)mod.TileType("StarplateDoorClosed"));
            }
        }

            public void SmoothWallRunner(Point position,  int size,  int wallID) //Walls...but in a circle
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X,  position.Y),  new Vector2(x,  y)) <= size)
                    {
                        Main.tile[x,  y].wall = (ushort)wallID;
                    }
                }
            }
        }

        public bool AreaContains(Point position,  int size,  int type) //Checks if a tile of a specific type is within a radius
        {
            for (int xPos = position.X - size; xPos <= position.X + size; xPos++)
            {
                for (int yPos = position.Y - size; yPos <= position.Y + size; yPos++)
                {
                    if (Vector2.Distance(new Vector2(position.X,  position.Y),  new Vector2(xPos,  yPos)) <= size && Main.tile[xPos,  yPos].type == type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AreaContains(Point position,  int size) //Same but if there is any active tile
        {
            for (int xPos = position.X - size; xPos <= position.X + size; xPos++)
            {
                for (int yPos = position.Y - size; yPos <= position.Y + size; yPos++)
                {
                    if (Vector2.Distance(new Vector2(position.X,  position.Y),  new Vector2(xPos,  yPos)) <= size && Main.tile[xPos,  yPos].active() == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DebugPlacement(int x,  int y,  string debug = "Placed: ") //Debug
        {
            WorldGen.PlaceTile(x,  y,  TileID.Meteorite);
            ErrorLogger.Log(debug + " " + x + " " + y);
        }

        public void AddRuinum()
        {
            bool leftSideGenned = false;
            bool rightSideGenned = false;
            for (int i = 0; i < Main.rand.Next(20, 33); i++)
            {
                int LowX1 = 70;
                int HighX1 = 300;
                int LowX2 = (int)Main.maxTilesX - 300;
                int HighX2 = (int)Main.maxTilesX - 70;
                int LowY = (int)Main.worldSurface - 100;
                int HighY = (int)Main.worldSurface + 100;

                int X1 = WorldGen.genRand.Next(LowX1, HighX1);
                int X2 = WorldGen.genRand.Next(LowX2, HighX2);
                int Y = WorldGen.genRand.Next(LowY, HighY);

                int OreSpread = WorldGen.genRand.Next(5, 9);
                int OreFrequency = WorldGen.genRand.Next(5, 9);

                if (!leftSideGenned)
                {
                    if (Main.tile[X1, Y - 1].type == 53)
                    {
                        while (Main.tile[X1, Y - 1].active())
                        {
                            Y -= 1;
                        }
                        WorldGen.OreRunner(X1, Y, OreSpread, OreFrequency, (ushort)mod.TileType("RuinumTile"));
                        leftSideGenned = true;
                    }
                }

                if (!rightSideGenned)
                {
                    if (Main.tile[X2, Y - 1].type == 53)
                    {
                        while (Main.tile[X2, Y - 1].active())
                        {
                            Y -= 1;
                        }
                        WorldGen.OreRunner(X2, Y, OreSpread, OreFrequency, (ushort)mod.TileType("RuinumTile"));
                        rightSideGenned = true;
                    }
                }

                if (leftSideGenned && rightSideGenned)
                {
                    i++;
                    leftSideGenned = false;
                    rightSideGenned = false;
                }
            }
        }
    }
}
