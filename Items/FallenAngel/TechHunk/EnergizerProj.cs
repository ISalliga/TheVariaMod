using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using BaseMod;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel.TechHunk
{
	public class EnergizerProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energizer");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()	
		{
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.timeLeft = 2000;
			projectile.width = 24;
			projectile.height = 30;
			projectile.ignoreWater = true;
			projectile.alpha = 50;
		}
		public override void AI()
		{
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 13, 0.92f, 0.38f, 18);
        }
		public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2,  94),  projectile.Center);
			Projectile.NewProjectile(projectile.Center.X,  projectile.Center.Y,  Main.rand.Next(-2,  3),  Main.rand.Next(-2,  3),  mod.ProjectileType("EnergizerCloud"),  20,  0f,  Main.myPlayer,  0f,  0f);
        }
	}
}