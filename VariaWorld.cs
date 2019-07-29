using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.DataStructures;
using Varia.NPCs.QueensInfantry;

namespace Varia
{
    public class VariaWorld : ModWorld
    {
        public static bool spaceHasShimmered = false;
        public static bool ninja = false;
        public static bool jelliumSpawned = false;

        public static int cavityTiles = 0;
        public static int breezeTiles = 0;
        public static int breezeTiles2 = 0;

        public static int dropBoost = 0;

        public static bool downedAngel = false;
        public static bool downedOptime = false;
        public static bool downedRainmaker = false;
        public static bool downedAnomaly = false;
        public static bool downedSpoderQueen = false;
        public static bool downedSotG = false;
        public static bool downedCore = false;

        public static bool storyMode = false;

        public static int hitsTakenNice = 0;

        public static int hunkCount = 0;

        private static bool dayTimeLast = false;
        public static bool dayTimeSwitched = false;
        public static bool starShower = false;

        public static int purityTiles = 0;
        public static int oldWorldTiles = 0;

        public override void Initialize()
        {
            hunkCount = 0;
            downedAngel = false;
            downedOptime = false;
            storyMode = false;
            downedRainmaker = false;
            downedSpoderQueen = false;
            downedAnomaly = false;
            downedSotG = false;
            downedCore = false;
            starShower = false;
            dropBoost = 0;
        }

        public override void PostUpdate()
        {
            if (hunkCount > 2)
            {
                NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2), Main.maxTilesX / 2, Main.maxTilesY / 2), mod.NPCType("FallenAngel"));
                hunkCount = 0;
            }
            if (NPC.AnyNPCs(mod.NPCType("Optime")) == false && NPC.AnyNPCs(mod.NPCType("NiceGuy")) == false)
            {
                hitsTakenNice = 0;
            }

            dropBoost = 0;

            if (Main.dayTime)
            {
                starShower = false;
            }
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedAngel) downed.Add("downedAngel");
            if (downedOptime) downed.Add("downedRainmaker");
            if (downedRainmaker) downed.Add("downedOptime");
            if (downedSpoderQueen) downed.Add("downedSpoderInvasion");
            if (downedSotG) downed.Add("downedSotG");
            if (downedAnomaly) downed.Add("downedAnomaly");
            if (downedCore) downed.Add("downedCore");

            return new TagCompound
			{
                {"downed", downed}
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedAngel = downed.Contains("downedAngel");
            downedOptime = downed.Contains("downedOptime");
            downedRainmaker = downed.Contains("downedRainmaker");
            downedSpoderQueen = downed.Contains("downedSpoderInvasion");
            downedSotG = downed.Contains("downedSotG");
            downedAnomaly = downed.Contains("downedAnomaly");
            downedCore = downed.Contains("downedCore");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedAngel = flags[0];
                downedOptime = flags[1];
                downedRainmaker = flags[2];
                downedSpoderQueen = flags[3];
                downedSotG = flags[4];
                downedAnomaly = flags[5];
                downedCore = flags[6];
            }
            else
            {
                ErrorLogger.Log("Varia: Unknown loadVersion: " + loadVersion);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedAngel;
            flags[1] = downedOptime;
            flags[2] = downedRainmaker;
            flags[3] = downedSpoderQueen;
            flags[4] = downedSotG;
            flags[5] = downedAnomaly;
            flags[6] = downedCore;
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedAngel = flags[0];
            downedOptime = flags[1];
            downedAngel = flags[2];
            downedSpoderQueen = flags[3];
            downedSotG = flags[4];
            downedAnomaly = flags[5];
            downedCore = flags[6];
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            cavityTiles = tileCounts[mod.TileType("Holestone")] + tileCounts[mod.TileType("ToothySpike")];
            breezeTiles = tileCounts[mod.TileType("ForgottenCloud")];
            breezeTiles2 = tileCounts[mod.TileType("StarplateBrick")];
            oldWorldTiles = tileCounts[mod.TileType("WornBrick")];
        }

        public override void ResetNearbyTileEffects()
        {
            cavityTiles = 0;
            breezeTiles = 0;
            breezeTiles2 = 0;
        }
    }
}