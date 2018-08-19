using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class CrystalArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
			projectile.damage = 30;
            projectile.height = 14;
            projectile.friendly = true;
			projectile.aiStyle = 1;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
		
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity.Y += 0.35f;
		}
		
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 5; i++)
			{
			float Speed = 8f;
			int damage = 30;
			int type = mod.ProjectileType("ArrowShard");
			float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(projectile.Center.Y - 12, projectile.Center.X - 12));
			int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			Main.projectile[proj].timeLeft = 300;
			Main.projectile[proj].tileCollide = false; 
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 110);
			for (int num623 = 0; num623 < 70; num623++)
			{
				if (Main.rand.NextFloat() < 1f)
				{
					Dust dust;
					Vector2 position = projectile.Center;
					dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 15, 0f, 0f, 218, new Color(255,0,201), 1.447368f)];
					dust.noGravity = true;
				}
			}
		}
    }
}