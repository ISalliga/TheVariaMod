using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class GelatineBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gelatine Blade");
			Tooltip.SetDefault("Fires one or two low-range bouncing blobs of flaming gel");
		}
		public override void SetDefaults()
		{
			item.damage = 28;
			item.melee = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 16;
			item.useAnimation = 16;
			item.width = 52;
			item.height = 52;
			item.value = 3000;
            item.rare = 2;
            item.shoot = mod.ProjectileType("FlamingBlob");
            item.shootSpeed = 16;
			item.UseSound = SoundID.Item1;
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, (speedX * 2) + Main.rand.Next(-2, 3), speedY * 2f - Main.rand.Next(25, 28), type, damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            return false;
        }
    }
}
