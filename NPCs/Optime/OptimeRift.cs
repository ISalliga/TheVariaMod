using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.Optime
{
    public class OptimeRift : ModProjectile
    {
        int FrameCountMeter = 0;
        public override void SetDefaults()
        {
            projectile.scale = 1f;
            projectile.width = 40;
            projectile.height = 40;
            projectile.alpha = 0;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 5;
            projectile.timeLeft = 60;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horrific Rift");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 4;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(155, 0, 255, projectile.alpha);
        }
        public override void AI()
        {
            FrameCountMeter++;
            if (FrameCountMeter >= 5)
            {
                projectile.frame++;
                FrameCountMeter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 9, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int b = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 9, 0, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int c = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -9, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int d = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -9, 0, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int e = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6, 6, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int f = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6, 6, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int g = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -6, -6, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            int AYCH = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 6, -6, ProjectileID.ShadowFlame, Main.expertMode ? 30 : 22, 0f, Main.myPlayer, 0f, 0f);
            Main.projectile[a].friendly = false;
            Main.projectile[a].hostile = true;
            Main.projectile[b].friendly = false;
            Main.projectile[b].hostile = true;
            Main.projectile[c].friendly = false;
            Main.projectile[c].hostile = true;
            Main.projectile[d].friendly = false;
            Main.projectile[d].hostile = true;
            Main.projectile[e].friendly = false;
            Main.projectile[e].hostile = true;
            Main.projectile[f].friendly = false;
            Main.projectile[f].hostile = true;
            Main.projectile[g].friendly = false;
            Main.projectile[g].hostile = true;
            Main.projectile[AYCH].friendly = false;
            Main.projectile[AYCH].hostile = true;
        }
    }
}