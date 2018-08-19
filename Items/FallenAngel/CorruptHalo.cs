using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class CorruptHalo : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.TheEyeOfCthulhu);
            item.damage = 44;
            item.height = 13;
            item.value = 40000;
            item.rare = 5;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 5;
            item.useTime = 5;
            item.shoot = mod.ProjectileType("CorruptHaloProj");
            item.shootSpeed = 18f;
            item.UseSound = SoundID.Item1;
            ItemID.Sets.Yoyo[item.type] = true;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Halo");
            Tooltip.SetDefault("Fires mini corruptors at nearby enemies");
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarklightEssence", 13);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
