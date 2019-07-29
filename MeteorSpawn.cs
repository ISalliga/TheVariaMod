using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Varia
{
    public class MeteorSpawn : ModWorld
    {
        public override void Initialize()
        {

        }
        public static void YeetMeteoriteIntoExistance()
        {
            int centerX = Main.maxTilesX / 2;
            int X = new Random().Next(300, Main.maxTilesX - 300);
            while (Math.Abs(X - centerX) < 160)
            {
                X = new Random().Next(300, Main.maxTilesX - 300);
            }
            int Y = 125;
            int diameter = new Random().Next(8, 17); //8-16 circle meteor

            while (!Main.tile[X, Y].nactive() | !Main.tileSolid[Main.tile[X, Y].type] | !InNumericalCriterionRange(Main.tile[X, Y].type, 0, 3))
            {
                Y++;
            }

            for (int i = X - diameter; i <= X + diameter; i++)
            {
                for (int j = Y - diameter; j <= Y + diameter; j++)
                {
                    if (Vector2.Distance(new Vector2(X, Y), new Vector2(i, j)) <= diameter) //nifty little trick
                    {
                        Main.tile[i, j].type = 37;
                    }
                }
            }
        }
        public static bool InNumericalCriterionRange(int number, int min, int max)
        {
            return (number >= min & number <= max);
        }
        public override void PostUpdate()
        {
            if (new Random().Next(600000) == 0 & VariaWorld.starShower & WorldGen.shadowOrbSmashed) //Change "true" to your internal variable name for rain. 1/10,000 chance per second (10000 * 60)
            {
                YeetMeteoriteIntoExistance();
                Main.NewText("Admist the chaos, a small meteorite falls out of the sky!", 255, 25, 25, false);
            }
        }
    }
}