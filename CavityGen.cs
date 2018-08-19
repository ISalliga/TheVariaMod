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
using Terraria.World.Generation;

namespace Varia
{
    class CavityGen : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            ErrorLogger.ClearLogs();
            tasks.Add(new PassLegacy("Making the world rotten", delegate (GenerationProgress progress)
            {
                for (int i = 0; i < Main.maxTilesX / 250; ++i) //Repeats ~16 times on a small world (i.e. 16 biomes/world)
                {
                    Point pos = new Point(WorldGen.genRand.Next(70, Main.maxTilesX - 70), WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200)); //Position of the biome
                    if (!AreaContains(pos, 30, TileID.BlueDungeonBrick) && !AreaContains(pos, 30, TileID.GreenDungeonBrick) && !AreaContains(pos, 30, TileID.PinkDungeonBrick) && !AreaContains(pos, 30, TileID.LihzahrdBrick)) //checks for unwanted blocks
                        GenCavity(pos, WorldGen.genRand.Next(3)); //places biome
                    else
                        i--; //Repeat until valid placement
                }
            }));
        }

        public void GenCavity(Point pos, int size) //Gen code for the biome
        {
            int[] types = new int[] { mod.TileType("Holestone"), TileID.Stone, mod.WallType("HolestoneWall"), mod.TileType("CacitianOre") }; //types[0] is the main "custom" block type, the one that is used for the inlining and the 'spikes'. types[1] is the outline. types[2] is the wallID. types[3] is the ore.
            int reps = WorldGen.genRand.Next(4, 8); //Repeats (how many holes there are)
            int sizeCircle = WorldGen.genRand.Next(6, 12); //Size of the holes
            int[] randSiz = new int[] { -10, 10 }; //Random offset
            if (size == 1) //Size 2
            {
                reps = WorldGen.genRand.Next(8, 12);
                sizeCircle = WorldGen.genRand.Next(12, 18);
                randSiz = new int[] { -20, 21 };
            }
            else if (size == 2) //Size 3
            {
                reps = WorldGen.genRand.Next(12, 18);
                sizeCircle = WorldGen.genRand.Next(18, 26);
                randSiz = new int[] { -30, 31 };
            }
            List<Point> centres = new List<Point>(); //List of centres
            for (int i = 0; i < reps; ++i) //Places actual holes/outlines
            {
                Point placePos = new Point(pos.X + WorldGen.genRand.Next(randSiz[0], randSiz[1]), pos.Y + WorldGen.genRand.Next(randSiz[0], randSiz[1]));
                SmoothRunner(placePos, sizeCircle + 12, types[1], types[2]);
                SmoothRunner(placePos, sizeCircle + 6, types[0], types[2]);
                SmoothTunnel(placePos, sizeCircle - 1);
                SmoothWallRunner(pos, sizeCircle, types[2]);
                centres.Add(placePos);
            }
            //Places 'spikes'
            for (int i = 0; i < reps - 2; ++i)
            {
                Vector2 dir = new Vector2(1f); //Direction of the spike
                dir = dir.RotatedBy(WorldGen.genRand.Next(0, 314) * 0.1f);
                Vector2 initPos = centres[WorldGen.genRand.Next(0, centres.Count)].ToVector2(); //The initial start 'centre'
                Vector2 nPos = initPos; //Place position
                bool hasTouched = false; //If the spike has touched a tile
                float siz = 1; //Size of the spike as segments
                int timer = -7; //Offset for the loop
                int totalReps = 0; //Total repeats
                while (!hasTouched && timer < 4) //Makes spikes
                {
                    if (Main.tile[(int)nPos.X, (int)nPos.Y].active())
                        hasTouched = true;
                    DirectSmoothRunner(nPos.ToPoint(), (int)(totalReps < 9 ? siz : siz * 2), types[0], types[2]); //Places spike
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
                Vector2 placePos = new Vector2(pos.X + WorldGen.genRand.Next((int)(randSiz[0] * 1.5f), (int)(randSiz[1] * 1.5f)), pos.Y + WorldGen.genRand.Next(randSiz[0], randSiz[1]));
                while (!Main.tile[(int)placePos.X, (int)placePos.Y].active()) //Makes spikes
                {
                    placePos = new Vector2(pos.X + WorldGen.genRand.Next(randSiz[0], randSiz[1]), pos.Y + WorldGen.genRand.Next(randSiz[0], randSiz[1]));
                }
                WorldGen.TileRunner((int)placePos.X, (int)placePos.Y, WorldGen.genRand.Next(4, 8), 5, types[3], true, 0, 0, false, true);
            }
        }

        public void DirectSmoothRunner(Point position, int size, int type, int wallID) //Forces placement of blocks in a cirlce
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size)
                    {
                        WorldGen.PlaceTile(x, y, type);
                        WorldGen.PlaceWall(x, y, wallID);
                    }
                }
            }
        }

        public void SmoothRunner(Point position, int size, int type, int wallID) //Overrides blocks in a circle
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size)
                    {
                        Main.tile[x, y].type = (ushort)type;
                        Main.tile[x, y].wall = (ushort)wallID;
                    }
                }
            }
        }

        public void SmoothTunnel(Point pos, int size) //Tunnels in a circle shape
        {
            for (int x = pos.X - size; x <= pos.X + size; x++)
            {
                for (int y = pos.Y - size; y <= pos.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(pos.X, pos.Y), new Vector2(x, y)) <= size)
                    {
                        WorldGen.KillTile(x, y);
                    }
                }
            }
        }

        public void SmoothWallRunner(Point position, int size, int wallID) //Walls...but in a circle
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size)
                    {
                        Main.tile[x, y].wall = (ushort)wallID;
                    }
                }
            }
        }

        public bool AreaContains(Point position, int size, int type) //Checks if a tile of a specific type is within a radius
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size && Main.tile[x, y].type == type)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AreaContains(Point position, int size) //Same but if there is any active tile
        {
            for (int x = position.X - size; x <= position.X + size; x++)
            {
                for (int y = position.Y - size; y <= position.Y + size; y++)
                {
                    if (Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(x, y)) <= size && Main.tile[x, y].active() == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DebugPlacement(int x, int y, string debug = "Placed: ") //Debug
        {
            WorldGen.PlaceTile(x, y, TileID.Meteorite);
            ErrorLogger.Log(debug + " " + x + " " + y);
        }
    }
}
