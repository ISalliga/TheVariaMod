using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Varia.NPCs.OldWorld
{
	public class LunixCaster : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunix Caster");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 36;
			npc.height = 28;
			npc.damage = 4;
			npc.defense = 0;
			npc.lifeMax = 90;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 300f;
			npc.knockBackResist = 0.5f;
            aiType = NPCID.FireImp;
        }
        public override void FindFrame(int frameHeight) // Animation.
        {
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
        public override void NPCLoot()
		{
			//
		}
        private float Teleport
        {
            get
            {
                return npc.ai[0];
            }
            set
            {
                npc.ai[0] = value;
            }
        }
        private float Activate
        {
            get
            {
                return npc.ai[1];
            }
            set
            {
                npc.ai[1] = value;
            }
        }
        public override void AI()
        {
            // Vanilla code scares me, but at least it works.
            npc.frameCounter = 0;
            Player player = Main.player[npc.target];
            Teleport += 1;
            if (Activate == 1 || Teleport > 80)
            {
                npc.frameCounter = 1;
            }
            if (Teleport == 80 && Activate == 0)
            {
                Vector2 Aim = player.Center - npc.Center;
                Aim.Normalize();
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Aim.X * 8, Aim.Y * 8, mod.ProjectileType("LunixProj"), npc.damage, 1f, Main.myPlayer);
            }
            if (Teleport >= 90)
            {
                if (Activate == 0)
                {
                    Vector2 Aim = player.Center - npc.Center;
                    Aim.Normalize();
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Aim.X * 8, Aim.Y * 8, mod.ProjectileType("LunixProj"), npc.damage, 1f, Main.myPlayer);
                    Activate += 1;
                }
                else if (Activate == 1)
                {
                    Activate -= 1;
                    int PlayerX = (int)player.position.X / 16;
                    int PlayerY = (int)player.position.Y / 16;
                    int EnemyX = (int)npc.position.X / 16;
                    int EnemyY = (int)npc.position.Y / 16;
                    int Random = 20;
                    int Check = 0;
                    bool WillNotCollide = false;
                    if ((double)Math.Abs(npc.position.X - player.position.X) + (double)Math.Abs(npc.position.Y - player.position.Y) > 2000.0)
                    {
                        Check = 100;
                        WillNotCollide = true;
                    }
                    int TileCheck1 = Main.rand.Next(PlayerX - Random, PlayerX + Random);
                    int TileCheck2 = Main.rand.Next(PlayerY - Random, PlayerY + Random);
                    while (!WillNotCollide && Check < 100)
                    {
                        ++Check;

                        for (TileCheck2 = Main.rand.Next(PlayerY - Random, PlayerY + Random); TileCheck2 < PlayerY + Random; ++TileCheck2)
                        {
                            if ((TileCheck2 < PlayerY - 4 || TileCheck2 > PlayerY + 4 || (TileCheck1 < PlayerX - 4 || TileCheck1 > PlayerX + 4)) && (TileCheck2 < EnemyY - 1 || TileCheck2 > EnemyY + 1 || (TileCheck1 < EnemyX - 1 || TileCheck1 > EnemyX + 1)) && Main.tile[TileCheck1, TileCheck2].nactive())
                            {
                                bool flag2 = true;
                                if (Main.tile[TileCheck1, TileCheck2 - 1].lava())
                                    flag2 = false;
                                if (flag2 && Main.tileSolid[(int)Main.tile[TileCheck1, TileCheck2].type] && !Collision.SolidTiles(TileCheck1 - 1, TileCheck1 + 1, TileCheck2 - 4, TileCheck2 - 1))
                                {
                                    WillNotCollide = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (WillNotCollide)
                    {
                        npc.position.X = (float)((double)TileCheck1 * 16.0f - (double)(npc.width / 2) + 8.0);
                        npc.position.Y = TileCheck2 * 16f - (float)npc.height;
                        for (int q = 0; q < 20; q++)
                        {
                            int dust = Dust.NewDust(npc.position, npc.width, npc.height, 113, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 0, default(Color), 3f);
                            Main.dust[dust].velocity *= 3f;
                            Main.dust[dust].noGravity = true;
                        }
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 15);
                    }
                }
                Teleport = 0;
            }
        }
    }
}
