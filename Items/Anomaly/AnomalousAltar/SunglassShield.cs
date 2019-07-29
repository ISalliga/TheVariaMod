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

namespace Varia.Items.Anomaly.AnomalousAltar
{
    [AutoloadEquip(EquipType.Shield)]
    public class SunglassShield : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 42;
			item.rare = 2;
            item.value = Item.sellPrice(0,  2,  50,  0);
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sunglass Shield");
            Tooltip.SetDefault("Negates knockback and allows you to dash \nReflects the heat of fire, giving you heat resistance and setting other enemies on fire instead");
        }
		public override void UpdateAccessory(Player player,  bool hideVisual)
		{
            if (player.HasBuff(BuffID.OnFire))
            {
                foreach (NPC target in Main.npc)
                {
                    if (Vector2.Distance(target.Center, player.Center) <= 100) target.AddBuff(BuffID.OnFire, 240);
                }
                player.buffImmune[BuffID.OnFire] = true;
            }
            player.noKnockback = true;
            player.dash = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EoCShield, 1);
            recipe.AddIngredient(ItemID.Sunglasses, 1);
            recipe.AddIngredient(mod.ItemType("AnomalousChunk"), 15);
            recipe.AddTile(mod.TileType("AnomalousAltar"));
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}