using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Varia.Items.QueensInfantry
{
    public class QueensJewel : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.alpha = 0;
            item.height = 12;
            item.maxStack = 1;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 5;
            item.expert = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Queen's Jewel");
            Tooltip.SetDefault("10% extra damage, melee speed, movement speed, and knockback\nGives the player the Feral Bite debuff");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(148, 8);
            player.kbGlove = true;
            player.moveSpeed += 0.1f;
            player.lifeRegen += 3;
            player.magicDamage += 0.10f;
            player.meleeDamage += 0.10f;
            player.rangedDamage += 0.10f;
            player.minionDamage += 0.10f;
			player.thrownDamage += 0.10f;
        }
    }
}