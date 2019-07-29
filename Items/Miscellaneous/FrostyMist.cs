using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.Items.Miscellaneous
{
	public class FrostyMist : ModProjectile
	{
        int FrameCountMeter = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frosty Mist");
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
            projectile.width = 30;
            projectile.height = 30;
			aiType = ProjectileID.Raven;
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
            projectile.rotation = 0;
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = Main.rand.Next(4, 8);
        }

        public override void AI()
		{
            for (int i = 0; i < 3; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 197, 0f, 0f, 0, new Color(0, 67, 255), 1.710526f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(25, Main.LocalPlayer);
            }

            Player player = Main.player[projectile.owner];
            VariaPlayer modPlayer = player.GetModPlayer<VariaPlayer>(mod);
            if (player.dead)
            {
                modPlayer.frostyMist = false;
            }
            if (modPlayer.frostyMist)
            {
                projectile.timeLeft = 2;
            }
        }
	}
}