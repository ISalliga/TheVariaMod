using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class MegaArchorb : ModProjectile
    {
		public int scaleTimer1 = 0;
		public int scaleTimer2 = 0;
		public int scalePhase = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Archorb");
        }
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
			projectile.tileCollide = false;
			projectile.alpha = 50;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 10; i++)
			{
			float Speed = 8f;
			int damage = 80;
			int type = mod.ProjectileType("MiniArchorb");
			float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(projectile.Center.Y - 10, projectile.Center.X - 10));
			int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			Main.projectile[proj].timeLeft = 45;
			Main.projectile[proj].tileCollide = false;                    
			}
        }
    }
}