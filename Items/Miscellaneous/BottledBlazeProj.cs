using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class BottledBlazeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bottled Blaze");     
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
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 12, 0.98f, 0.6f, 24);
		}
		public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 107), projectile.Center);
            int splosion = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, 2, 0f, projectile.owner);
            Main.projectile[splosion].timeLeft = 40;
        }
	}
}