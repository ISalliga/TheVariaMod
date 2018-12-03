using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace varia.Items.FallenAngel.TechHunk
{
    public class EnergyFusillade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 18;
			item.scale = 1.25f;
            item.ranged = true;
            item.width = 56;
            item.height = 26;
            item.crit = 10; 
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0f;
            item.value = 6000;
            item.rare = 3;
			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item91;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 15f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Fusillade");
			Tooltip.SetDefault("'It's like the Minishark, but with FRICKIN' LASER BEAMS ATTACHED TO ITS HEAD.'");
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int index = 0; index < Main.rand.Next(1, 3); ++index)
            {
                float SpeedX = speedX + (float)Main.rand.Next(-60, 61) * 0.045f;
                float SpeedY = speedY + (float)Main.rand.Next(-60, 61) * 0.045f;
                int projectile1 = Projectile.NewProjectile(position.X, position.Y, SpeedX * 1.4f, SpeedY * 1.4f, 88, damage - Main.rand.Next(3), knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
				Main.projectile[projectile1].penetrate = 1;
            }
			for (int despacito = 2; despacito <= 22; despacito++)
			{
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num117 = 0.3f;
				int num118 = 1;
				Vector2 vector7 = new Vector2(speedX, speedY);
				vector7.Normalize();
				vector7 *= 80f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
				for (int num119 = 0; num119 < num118; num119++)
				{
					float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
					Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
					if (!flag11)
					{
						value9 -= vector7;
					}
					Vector2 pos1;
					pos1.X = vector2.X + value9.X - 4;
					pos1.Y = vector2.Y + value9.Y - 4;
					int decre =  Main.rand.Next(4,9);
					Dust dust = Main.dust[Terraria.Dust.NewDust(pos1, 1, 1, 111, speedX /  decre, speedY / decre, 0, new Color(255,255,255), 0.5f)];
				}
			}
			return false;
        }
    }
}