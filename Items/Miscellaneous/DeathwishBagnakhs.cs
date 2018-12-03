using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous   //where is located
{
    public class DeathwishBagnakhs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathwish Bagnakhs");
            Tooltip.SetDefault("'BAGNAGS'");
        }

        public override void SetDefaults()
        {

            item.damage = 96;    
            item.melee = true;     //This defines if it does Melee damage and if its effected by Melee increasing Armor/Accessories.
            item.width = 80;    //The size of the width of the hitbox in pixels.
            item.height = 80;    //The size of the height of the hitbox in pixels.

            item.useTime = 6;   //How fast the Weapon is used.
            item.useAnimation = 6;     //How long the Weapon is used for.
            item.channel = true;
            item.useStyle = 100;    //The way your Weapon will be used, 1 is the regular sword swing for example
            item.knockBack = 3f;    //The knockback stat of your Weapon.
            item.value = Item.buyPrice(0, 2, 0, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 10gold)
            item.rare = 7;                      
            item.shoot = mod.ProjectileType("DeathwishBagnagProj"); 
            item.noUseGraphic = true; 
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AnomalousChunk", 15);
            recipe.AddIngredient(ItemID.FetidBaghnakhs, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool UseItemFrame(Player player)     //this defines what frame the player use when this weapon is used
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }
    }
}