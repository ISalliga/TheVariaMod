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

namespace Varia
{
    public class VariaWorld : ModWorld
    {
		public static int cavityTiles = 0;
		public static bool downedAngel = false;
        public static bool downedOptime = false;
		public static bool storyMode = false;

        public static int hunkCount = 0;
		
		public override void Initialize()
		{
            hunkCount = 0;
            downedAngel = false;
            downedOptime = false;
            storyMode = false;
		}

        public override void PostUpdate()
        {
            if (hunkCount > 2)
            {
                NPC.SpawnOnPlayer(1, mod.NPCType("FallenAngel"));
                hunkCount = 0;
            }
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedAngel) downed.Add("downedAngel");
            if (downedOptime) downed.Add("downedOptime");
            if (storyMode) downed.Add("storyMode");

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
            storyMode = downed.Contains("storyMode");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedAngel = flags[0];
                downedOptime = flags[1];
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
            writer.Write(flags);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedAngel = flags[0];
            downedOptime = flags[1];
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            cavityTiles = tileCounts[mod.TileType("Holestone")];
        }

        public override void ResetNearbyTileEffects()
        {
            cavityTiles = 0;
        }
    }
}