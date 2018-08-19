using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using System.IO;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Varia;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace AgheriumMod.NPCs
{
    public class VariaGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
    }
}