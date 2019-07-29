using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Optime
{
    public class OptimeBag : ModItem
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

        public override int BossBagNPC => mod.NPCType("Optime");

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
            int numOfWeapons = 2;
            int weaponPoolCount = 3;
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
                        dropName = "MrNicey";
                        break;
                    case 1:
                        dropName = "HappyPills";
                        break;
                    case 2:
                        dropName = "GoldenDollar";
                        break;
                }
                if (dropName != "none")
                {
                    player.QuickSpawnItem(mod.ItemType(dropName));
                }
            }

            player.QuickSpawnItem(mod.ItemType("PureConcentratedDarkness"), Main.rand.Next(20, 35));

            player.QuickSpawnItem(mod.ItemType("HisTopHat"));
        }
    }
}
