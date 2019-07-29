using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace Varia.Items.Miscellaneous.JelliumGear
{
	public class JelliumCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jellium Crystal");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.value = 0;
			item.rare = 5;
			item.maxStack = 250;
		}
    }
}
