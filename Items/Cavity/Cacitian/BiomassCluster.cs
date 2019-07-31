using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
    public class BiomassCluster : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0,  1,  0,  0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Biomass Cluster");
            Tooltip.SetDefault("'It pulsates in my hand. What even are those red things? Eyes? Gross.' \nPermanent Hunter, Spelunker and Dangersense buffs \nGrants you primitive future sight");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            player.GetModPlayer<VariaPlayer>().biomassCluster = true;
            player.AddBuff(BuffID.Hunter, 8);
            player.AddBuff(BuffID.Spelunker, 8);
            player.AddBuff(BuffID.Dangersense, 8);
        }
    }
}