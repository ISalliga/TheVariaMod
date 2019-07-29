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

namespace Varia.Items.Anomaly.AnomalousAltar
{
	public class BitcrusherShockwave : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bitcrusher Shockwave");
            Main.projFrames[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.timeLeft = 30;
			projectile.aiStyle = -1;
			projectile.width = 50;
			projectile.height = 50;
            projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.knockBack = 0;
		}
		public override void AI()
		{
            if (projectile.timeLeft > 15)
            {
                projectile.position.X -= 6;
                projectile.position.Y -= 6;
                projectile.width += 12;
                projectile.height += 12;
            }
            for (int i = 0; i < projectile.width / 4; i++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 89, Main.rand.Next(-projectile.width / 18, projectile.width / 18), Main.rand.Next(-projectile.height / 20, projectile.height / 20), 100, new Color(59, 0, 255), (float)projectile.timeLeft / 16);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 2f;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            target.AddBuff(mod.BuffType("Stunned"), 90);
        }
    }
}