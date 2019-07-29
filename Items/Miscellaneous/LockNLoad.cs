using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Varia.Items.Miscellaneous
{
	public class LockNLoad : ModItem
	{
        int firingStage = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lock 'n' Load");
			Tooltip.SetDefault("Takes two clicks to fire, but deals a lot of damage");
        }
        public override void SetDefaults()
		{
			item.damage = 38;
			item.noMelee = true;
			item.ranged = true;
			item.autoReuse = false;
			item.rare = 3;
			item.width = 28;
			item.height = 30;
			item.useStyle = 5;
            item.useTime = 20;
			item.UseSound = SoundID.Item11;
            item.shootSpeed = 6f;
			item.useAnimation = 20;                         
			item.shoot = ProjectileID.Bullet;
			item.value = 2700;
			item.useAmmo = AmmoID.Bullet;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            firingStage++;
            if (firingStage > 2) firingStage = 1;
            if (firingStage == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position2 = new Vector2(player.Center.X - 20, player.position.Y + player.height);
                    dust = Main.dust[Terraria.Dust.NewDust(position2, 40, 5, 122, 0f, 0f, 0, new Color(255, 255, 255), 2.039474f)];
                    dust.noGravity = true;
                }
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 11), player.Center);
                return false;
            }
            else
            {
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 11), player.Center);
                return true;
            }
        }
        public override bool ConsumeAmmo(Player player)
        {
            return firingStage == 1 ? false : true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1.7f, 0);
        }
        public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol, 6);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddIngredient(ItemID.Lens, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.FlintlockPistol, 6);
            recipe1.AddIngredient(ItemID.IronBar, 10);
            recipe1.AddIngredient(ItemID.Lens, 4);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }
    }
}
