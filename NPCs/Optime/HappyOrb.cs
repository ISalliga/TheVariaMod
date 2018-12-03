using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.Optime
{
    class HappyOrb : ModProjectile
    {
        int alphaTimer = 0;
        public override void SetDefaults()
        {
            projectile.width = 15;
            projectile.height = 15;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = -1;
            projectile.tileCollide = false;

            // 5 second fuse.
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            int num103 = Player.FindClosest(projectile.Center, 1, 1);
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 60)
            {
                Vector2 tPos;
                tPos.X = Main.player[num103].Center.X + Main.rand.Next(-20, 21);
                tPos.Y = Main.player[num103].Center.Y + Main.rand.Next(-20, 21);
                projectile.velocity = projectile.DirectionTo(tPos) * 20;
                projectile.ai[1] = 0;
            }
            else
            {
                projectile.velocity.X = projectile.velocity.X * 20 / 21;
                projectile.velocity.Y = projectile.velocity.Y * 20 / 21;
            }
        }
    }
}
