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
using BaseMod;
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.OldWorld
{
    public class BanditBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 14;
            projectile.height = 24;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bandit Bomb");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            BaseAI.AIThrownWeapon(projectile, ref projectile.ai, true, 10, 0.99f, 0.28f, 15f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int numDust = 0; numDust < 24; numDust++)
            {
                {
                    Dust dust;
                    Vector2 position = projectile.position - new Vector2(10, 10);
                    dust = Main.dust[Dust.NewDust(position, projectile.width + 20, projectile.height + 20, 6, 0f, 0f, 0, new Color(75, 155, 255), 2.2f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                }
            }
            
                int num20 = 36;
                for (int i = 0; i < num20; i++)
                {
                    Vector2 pos = projectile.Center;
                    Vector2 spinningpoint = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f * 0.5f;
                    spinningpoint = spinningpoint.RotatedBy((double)((float)(i - (num20 / 2 - 1)) * 6.28318548f / (float)num20), default(Vector2)) + pos;
                    Vector2 vector = spinningpoint - pos;
                    int num21 = Dust.NewDust(spinningpoint + vector, 0, 0, 6, vector.X * 2, vector.Y * 2, 0, new Color(75, 155, 255), 2.5f);
                    Main.dust[num21].noGravity = true;
                    Main.dust[num21].noLight = true;
                    Main.dust[num21].shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                }

            if (projectile.hostile)
            {
                int rand = Main.rand.Next(8, 13);
                for (int i = 0; i < rand; i++)
                {
                    int xSpeed = Main.rand.Next(-12, 13);
                    int ySpeed = Main.rand.Next(-7, 8);
                    int proj = Projectile.NewProjectile(projectile.Center, new Vector2(xSpeed, ySpeed), mod.ProjectileType("BanditShrapnel"), Main.expertMode ? 25 : 33, 0f, projectile.owner);
                    Main.projectile[proj].friendly = projectile.friendly;
                    Main.projectile[proj].hostile = projectile.hostile;
                }
            }
            else
            {
                int rand = Main.rand.Next(4, 9);
                for (int i = 0; i < rand; i++)
                {
                    int xSpeed = Main.rand.Next(-12, 13);
                    int ySpeed = Main.rand.Next(-7, 8);
                    int proj = Projectile.NewProjectile(projectile.Center, new Vector2(xSpeed, ySpeed), mod.ProjectileType("BanditShrapnel"), 6, 0f, projectile.owner);
                    Main.projectile[proj].friendly = projectile.friendly;
                    Main.projectile[proj].hostile = projectile.hostile;
                }
            }
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 14), projectile.Center);
        }
    }
}