using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.SoulOfTheGuide
{
	public class FightNFlight : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fight'n'Flight");
			Tooltip.SetDefault("'Infused with the power to run like hell!'" + "\nGives you the Speedy buff upon hitting enemies with the blade");
		}
		public override void SetDefaults()
		{
			item.damage = 35;
			item.melee = true;
			item.width = 46;
			item.height = 50;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.shoot = mod.ProjectileType("FightNFlightProj");
			item.shootSpeed = 10f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(mod.ItemType("SoulShard"), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.AddBuff(BuffID.Swiftness, 720);
			target.AddBuff(BuffID.Slow, 720);
		}
	}
}
