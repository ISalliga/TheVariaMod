using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Varia.Items.SoulOfTheGuide
{
	public class SoulBoi : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Boi");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
            ProjectileID.Sets.LightPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
            projectile.tileCollide = false;
            projectile.width = 18;
            projectile.height = 24;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);
			if (player.dead)
			{
				modPlayer.soulBoi = false;
			}
			if (modPlayer.soulBoi)
			{
				projectile.timeLeft = 2;
			}

            projectile.velocity = projectile.DirectionTo(Main.MouseWorld) * (projectile.Distance(Main.MouseWorld) / 8);

            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            Lighting.AddLight(projectile.position, new Vector3(100, 100, 175) / 200);
        }
	}
}