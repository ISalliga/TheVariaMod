using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.Miscellaneous
{
	public class UnstableMagmaLauncher : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable Magma Launcher");
			Tooltip.SetDefault("Shots fly around everywhere with no predictable trajectory \nUses gel as ammo");
        }
        public override void SetDefaults()
		{
			item.damage = 29;
			item.noMelee = true;
			item.ranged = true;
			item.autoReuse = true;
			item.rare = 5;
			item.width = 28;
			item.height = 30;
			item.useStyle = 5;
            item.useTime = 7;
			item.UseSound = SoundID.Item11;
            item.shootSpeed = 6f;
			item.useAnimation = 7;                         
			item.shoot = ProjectileID.Bullet;
			item.value = 2700;
			item.useAmmo = AmmoID.Gel;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1.9f, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("UnstableMagma");
            Vector2 offset = new Vector2(speedX, speedY) * 6.5f;
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
            {
                position += offset;
            }
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(position - new Vector2(6, 6), 12, 12, 174, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                dust.noLight = true;
            }
            return true;
        }
        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddIngredient(ItemID.Feather, 15);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
