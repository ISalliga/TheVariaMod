using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class SanguineSpike : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sanguine Spike");
            Tooltip.SetDefault("Has a chance to steal life from enemies");
        }
        public override void SetDefaults()
        {
            item.damage = 15;
            item.magic = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 16;
            item.mana = 10;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4.5f;
            item.value = 250000;
            item.rare = 7;
            item.UseSound = SoundID.Item84;
            item.autoReuse = true;
            item.shootSpeed = 3f;
            item.shoot = mod.ProjectileType("SpikeProj");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 12);
            recipe.AddIngredient(ItemID.Pumpkin, 23);
            recipe.AddIngredient(ItemID.Bone, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}