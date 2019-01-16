using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.LabPotions
{
    public class RegrowthAura : ModBuff
    {
        internal string texture;
        public bool canBeCleared = true;
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Regrowth Aura");
            Description.SetDefault("Upon taking damage, you are given a regeneration effect");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Main.LocalPlayer.GetModPlayer<VariaPlayer>().regrowthAura = true;
        }
    }
}