using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
    public class AmberLaser : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
			projectile.damage = 34;
            projectile.height = 7;
			projectile.scale = 0.5f;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(155, 100, 0, projectile.alpha);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.damage += 1;
        }

        public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
        
    }
}