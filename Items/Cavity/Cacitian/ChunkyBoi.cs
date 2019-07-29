using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Cavity.Cacitian
{
	public class ChunkyBoi : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("One Chunky Boye");
			Main.projFrames[projectile.type] = 1;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Raven);
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage = 18;
            projectile.width = 20;
            projectile.height = 28;
			aiType = ProjectileID.Raven;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			return true;
		}
        
        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);
            if (player.dead)
            {
                modPlayer.hunkOChunk = false;
            }
            if (modPlayer.hunkOChunk)
            {
                projectile.timeLeft = 2;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.velocity.X = -projectile.velocity.X * 2;
            projectile.velocity.Y = -projectile.velocity.Y * 2;
        }
    }
}