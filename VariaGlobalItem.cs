using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Varia;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;

namespace Varia
{
	public class VariaGlobalItem : GlobalItem
	{
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type == ItemID.Goggles)
            {
                item.accessory = true;
            }
        }

        public override void HoldItem(Item item, Player player)
        {
            if (item.type == ItemID.Goggles)
            {
                item.accessory = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.Goggles)
            {
                TooltipLine goggleTooltip = new TooltipLine(mod, "Goggles", "Allows you to see further towards the horizon when equipped \nCan be used in an accessory slot, or the helm slot");
                tooltips.Add(goggleTooltip);
            }
            if (item.type == ItemID.BrainOfConfusion)
            {
                tooltips.RemoveAt(2);
                TooltipLine bocTooltip = new TooltipLine(mod, "BoC", "Upon being struck, all enemies within a small radius of you will be confused \nYou gain more damage based on how many enemies are confused");
                tooltips.Insert(3, bocTooltip);
            }
            if (item.type == ItemID.SniperScope)
            {
                TooltipLine sniperTooltip = new TooltipLine(mod, "SniperScope", "Ranged projectiles deal more damage the farther away from you they are");
                tooltips.Insert(3, sniperTooltip);
            }
            if (item.type == ItemID.LifeCrystal)
            {
                TooltipLine lifeCrystalTooltip = new TooltipLine(mod, "LifeCrystal", "If you are at maximum capacity, this item instead restores half your max life");
                tooltips.Add(lifeCrystalTooltip);
            }
            if (item.type == ItemID.LifeFruit)
            {
                TooltipLine lifeFruitTooltip = new TooltipLine(mod, "LifeFruit", "If you are at maximum capacity, this item instead restores your health fully");
                tooltips.Add(lifeFruitTooltip);
            }
            if (item.type == ItemID.ManaCrystal)
            {
                TooltipLine manaCrystalTooltip = new TooltipLine(mod, "ManaCrystal", "If you are at maximum capacity, this item instead gives you infinite mana for thirty seconds \nThis works at the cost of not being able to use health potions");
                tooltips.Add(manaCrystalTooltip);
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.LifeCrystal || item.type == ItemID.LifeFruit || item.type == ItemID.ManaCrystal)
            {
                if (!player.HasBuff(BuffID.PotionSickness))
                {
                    if (item.type == ItemID.LifeCrystal)
                    {
                        player.statLife += player.statLifeMax / 2;
                        item.consumable = true;
                        player.AddBuff(BuffID.PotionSickness, 60 * 90);
                    }
                    if (item.type == ItemID.LifeFruit)
                    {
                        player.statLife = player.statLifeMax;
                        item.consumable = true;
                        player.AddBuff(BuffID.PotionSickness, 60 * 100);
                    }
                    if (item.type == ItemID.ManaCrystal)
                    {
                        player.AddBuff(mod.BuffType("ManaInvincibility"), 60 * 30);
                        item.consumable = true;
                        player.AddBuff(BuffID.PotionSickness, 60 * 30);
                    }
                    item.stack -= 1;
                    return true;
                }
                else return false;
            }
            return base.CanUseItem(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.Goggles)
            {
                player.GetModPlayer<VariaPlayer>().Goggles = true;
                item.accessory = true;
            }
            if (item.type == ItemID.BrainOfConfusion)
            {
                player.GetModPlayer<VariaPlayer>().BoC2 = true;
                item.accessory = true;
            }
            if (item.type == ItemID.SniperScope)
            {
                player.GetModPlayer<VariaPlayer>().SniperScope = true;
                item.accessory = true;
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ItemID.Goggles)
            {
                player.GetModPlayer<VariaPlayer>().Goggles = true;
                item.accessory = true;
            }
        }
    }
}