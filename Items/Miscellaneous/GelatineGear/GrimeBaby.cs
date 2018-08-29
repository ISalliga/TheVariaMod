using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
	public class GrimeBaby : ModProjectile
	{
        int FrameCountMeter = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grime Baby");
			Main.projFrames[projectile.type] = 6;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabySlime);
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 22;
            projectile.height = 20;
			aiType = ProjectileID.BabySlime;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			return true;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);
            if (player.dead)
            {
                modPlayer.grimeBaby = false;
            }
            if (modPlayer.grimeBaby)
            {
                projectile.timeLeft = 2;
            }
        }
	}
}