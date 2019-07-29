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

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class BarrageShard : ModProjectile
    {
        int bounce = 3;
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 20;
			projectile.magic = true;
			projectile.damage = 17;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.scale = bounce/3;
            projectile.timeLeft = 9999;
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Barrage Shard");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y / 2;
            }
            bounce--;
            if (bounce == 0)
            {
                projectile.Kill();
            }
            return false;
        }
		public override void AI()
		{
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.X = projectile.velocity.X * 15 / 16;
            projectile.velocity.Y += 1.1f;
        }
    }
}