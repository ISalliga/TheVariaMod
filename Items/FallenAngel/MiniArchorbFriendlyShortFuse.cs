using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class MiniArchorbFriendlyShortFuse : ModProjectile
    {
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
			projectile.tileCollide = false;
            projectile.timeLeft = 40;
			projectile.alpha = 50;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
			{
			float Speed = 8f;
			int damage = 80;
			int type = mod.ProjectileType("UnholyTurretBeam");
			float rotation = ((((float)Math.PI / 5) * i) + (float)Math.Atan2(projectile.Center.Y - 10, projectile.Center.X - 10));
			int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			Main.projectile[proj].timeLeft = 40;
			Main.projectile[proj].tileCollide = false; 
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			}
        }
    }
}