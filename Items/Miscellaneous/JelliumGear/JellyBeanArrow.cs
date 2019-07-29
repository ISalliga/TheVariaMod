using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class JellyBeanArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jelly Bean Arrow");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
			projectile.friendly = true;
            projectile.height = 32;
            projectile.aiStyle = 1;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 3000;
			projectile.alpha = 0;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 0, 223, projectile.alpha);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
			{
			float Speed = 8f;
			int damage = 10;
			int type = mod.ProjectileType("JellyBeanSmol");
			float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(projectile.Center.Y - 10,  projectile.Center.X - 10));
			int proj = Projectile.NewProjectile(projectile.Center.X,  projectile.Center.Y,  (float)((Math.Cos(rotation) * Speed) * -1),  (float)((Math.Sin(rotation) * Speed) * -1),  type,  damage,  0f,  0);
			Main.projectile[proj].timeLeft = 10;
			Main.projectile[proj].tileCollide = false; 
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			}
        }
    }
}