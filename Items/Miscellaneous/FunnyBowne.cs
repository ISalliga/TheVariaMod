using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class FunnyBowne : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Funny Bowne");
			Tooltip.SetDefault("May fire bone arrows occasionally instead of your chosen ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 32;
			item.ranged = true;
			item.width = 30;
			item.height = 46;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.shoot = 1;
			item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Arrow;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, -3);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (Main.rand.Next(4) == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 117, damage, 0, player.whoAmI);
                return false;
			}
			else
            {
                return true;
            }
		}
	}
}