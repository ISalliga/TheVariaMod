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

namespace Varia.Items.OldWorld
{
    [AutoloadEquip(EquipType.Head)]
    public class SolixBandana : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solix Bandana");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 5000;
            item.rare = 2;
            item.vanity = true;
        }
    }
}