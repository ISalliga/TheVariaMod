using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class AngelBag : ModItem
    {
        public override void SetDefaults()
        {

            item.maxStack = 99;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            item.rare = 9;
            item.expert = true;      
			item.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override int BossBagNPC => mod.NPCType("FallenAngel");

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Treasure Bag");
          Tooltip.SetDefault("Right click to open");
        }

        public override bool CanRightClick()
        {
            return true;
        }
 
        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("DarklightEssence"), Main.rand.Next(30, 43));
            player.QuickSpawnItem(mod.ItemType("TheInfinityCloud"));
            int numOfWeapons = 1;
            int weaponPoolCount = 2;
            int[] weaponLoot = new int[numOfWeapons];
            for (int n = 0; n < numOfWeapons; n++)
            {
                weaponLoot[n] = Main.rand.Next(weaponPoolCount - n);
                for (int j = 0; j < n; j++)
                {
                    if (weaponLoot[n] >= weaponLoot[j])
                    {
                        weaponLoot[n]++;
                    }
                    Array.Sort(weaponLoot);
                }
            }
            for (int i = 0; i < weaponLoot.Length; i++)
            {
                string dropName = "none";
                switch (weaponLoot[i])
                {
                    case 0:
                        dropName = "GuardiansValor";
                        break;
                    case 1:
                        dropName = "VarianWings";
                        break;
                    case 2:
                        dropName = "GuardiansValor";
                        break;
                    case 3:
                        dropName = "GuardiansValor";
                        break;
                }
                if (dropName != "none")
                {
                    Item.NewItem(player.getRect(), mod.ItemType(dropName));
                }
            }
        }
    }
}
