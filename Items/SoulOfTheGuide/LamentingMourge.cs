using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
    public class LamentingMourge : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lamenting Mourge");
            Tooltip.SetDefault("Creates a Fright Skull upon enemy hits");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 48;
            item.rare = 3;
            item.value = 40000;
            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 7.5F;
            item.damage = 20;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("LamentingMourgeProj");
            item.shootSpeed = 34f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddIngredient(mod.ItemType("SoulShard"), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}