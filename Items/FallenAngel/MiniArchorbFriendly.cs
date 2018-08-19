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

namespace Varia.Items.FallenAngel
{
    public class MiniArchorbFriendly : ModProjectile
    {
		Vector2 mousePos;
		int jutsDone = 0;
		int jutTime = 0;
		int jutSpeed = 30;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Archorb");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
			projectile.friendly = true;
            projectile.height = 32; 
            projectile.penetrate = 1;
            projectile.timeLeft = 1000;
			projectile.tileCollide = false;
			projectile.alpha = 255;
        }
		public override void AI()
		{
			if (projectile.alpha > 50)
			{
				projectile.alpha -= 15;
			}
			else
			{
				projectile.alpha = 50;
			}
			jutTime++;
			if (jutTime == 39)
			{
				mousePos = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
				jutSpeed = 30;
				projectile.scale += 0.04f;
			}
			if (jutTime > 39)
			{
				Vector2 toTarget = new Vector2(mousePos.X - projectile.Center.X, mousePos.Y - projectile.Center.Y);
				toTarget.Normalize();
				projectile.velocity = toTarget * jutSpeed;
				jutSpeed -= 2;
				if (jutSpeed == 0)
				{
					jutTime = 0;
					jutsDone++;
				}
			}
			if (jutsDone == 3)
			{
				projectile.timeLeft = 0;
			}
		}
        public override void Kill(int timeLeft)
        {
			for (int num623 = 0; num623 < 25; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 20, 0f, 0f, 100, Color.Purple, 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
			}
		}
	}
}