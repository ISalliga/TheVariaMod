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

namespace Varia.Items.Anomaly
{
	public class AnomalousChunk : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anomalous Chunk");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.value = 0;
			item.rare = 2;
			item.maxStack = 250;
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 3));
		}
	}
}
