using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using System.Collections.Generic;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.NPCs.StarShower
{
    public class Spark : ModProjectile
    {
        bool setRotation = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spark");
        }
        public override void SetDefaults()
        {
            projectile.width = 18; //Change me
            projectile.height = 18; //Change me
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.timeLeft = 61;
        }
        public override void AI()
        {
            if (!setRotation)
            {
                projectile.rotation = (float)new Random().NextDouble() * ((float)Math.PI * 2) - (float)Math.PI; // range of -pi to pi
                setRotation = true;
            }
            if (!projectile.tileCollide)
            {
                projectile.velocity.Y += 9.8f / 60f; //gravity
            }
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
            if (projectile.timeLeft == 1)
            {
                for (int i = 0; i < 24; i++)
                {
                    float angle = i * 2 * (float)Math.PI / 24f;
                    int dust = Dust.NewDust(projectile.position, 3, 3, 57);
                    Main.dust[dust].velocity.X = 2 * (float)Math.Cos(angle);
                    Main.dust[dust].velocity.Y = 2 * (float)Math.Sin(angle);
                }
                projectile.active = false;
            }
        }
    }
    public class StarBuck : ModNPC
    {
        int frameCounter = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Buck"); //DONT Change me
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 600;
            npc.damage = 30;
            npc.defense = 6;
            npc.knockBackResist = 0.04f;
            npc.width = 80;
            npc.height = 84;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 20, 48);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 720;
            npc.damage = 40;
            npc.defense = 9;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return VariaWorld.starShower ? 0.25f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frameCounter;
        }
        public override void AI()
        {
            npc.localAI[0]++; //timer
            npc.frameCounter++;
            int speedValue = (int)Math.Ceiling(20 / (npc.velocity.Length()));
            int speedClamped = (int)MathHelper.Clamp(speedValue, 2, 9); //faster the npc is, the quicker the frames are
            if (npc.frameCounter % speedClamped == 0)
            {
                frameCounter++;
                if (frameCounter >= Main.npcFrameCount[npc.type])
                    frameCounter = 0;
            }
            if (Math.Abs(npc.velocity.X) >= 8f & npc.localAI[0] % 4 == 0)
            {
                Projectile.NewProjectile(npc.Center.X, npc.position.Y + npc.height, 0, 0, mod.ProjectileType("Spark"), 10, 3);
            }
            int num = 30;
            int num2 = 10;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
            {
                flag2 = true;
                npc.ai[3] += 1f;
            }

            num2 = 4;
            bool flag4 = npc.velocity.Y == 0f;
            for (int i = 0; i < 200; i++)
            {
                if (i != npc.whoAmI && Main.npc[i].active && Main.npc[i].type == npc.type && Math.Abs(npc.position.X - Main.npc[i].position.X) + Math.Abs(npc.position.Y - Main.npc[i].position.Y) < (float)npc.width)
                {
                    if (npc.position.X < Main.npc[i].position.X)
                    {
                        npc.velocity.X = npc.velocity.X - 0.05f;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X + 0.05f;
                    }
                    if (npc.position.Y < Main.npc[i].position.Y)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.05f;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.05f;
                    }
                }
            }
            if (flag4)
            {
                npc.velocity.Y = 0f;
            }
            if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num) | flag2)
            {
                npc.ai[3] += 1f;
                flag3 = true;
            }
            else if (npc.ai[3] > 0f)
            {
                npc.ai[3] -= 1f;
            }
            if (npc.ai[3] > (float)(num * num2))
            {
                npc.ai[3] = 0f;
            }
            if (npc.justHit)
            {
                npc.ai[3] = 0f;
            }
            if (npc.ai[3] == (float)num)
            {
                npc.netUpdate = true;
            }
            npc.spriteDirection = Math.Sign(npc.velocity.X) == 1 ? 1 : -1;
            Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num3 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X;
            float num4 = Main.player[npc.target].position.Y - vector.Y;
            float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
            if (num5 < 200f && !flag3)
            {
                npc.ai[3] = 0f;
            }
            if (npc.ai[3] < (float)num)
            {
                if ((npc.type == 329 || npc.type == 315) && !Main.pumpkinMoon)
                {
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                }
            }
            else
            {
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.ai[0] += 1f;
                        if (npc.ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.ai[0] = 0f;
                        }
                    }
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                npc.directionY = -1;
                if (npc.direction == 0)
                {
                    npc.direction = 1;
                }
            }
            float maxVelocityX = 10f;
            float acceleration = 0.07f;
            if (!flag && (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0)))
            {
                if (npc.velocity.X > 0f && npc.direction < 0)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
                if (npc.velocity.X < 0f && npc.direction > 0)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
                if (npc.direction > 0 && npc.velocity.X < 3f)
                {
                    npc.velocity.X = npc.velocity.X + 0.1f;
                }
                if (npc.direction < 0 && npc.velocity.X > -3f)
                {
                    npc.velocity.X = npc.velocity.X - 0.1f;
                }

                if (npc.velocity.X < -maxVelocityX || npc.velocity.X > maxVelocityX)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity *= 0.8f;
                    }
                }
                else if (npc.velocity.X < maxVelocityX && npc.direction == 1)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X > maxVelocityX)
                    {
                        npc.velocity.X = maxVelocityX;
                    }
                }
                else if (npc.velocity.X > -maxVelocityX && npc.direction == -1)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X < -maxVelocityX)
                    {
                        npc.velocity.X = -maxVelocityX;
                    }
                }
            }
            if (npc.velocity.Y >= 0f)
            {
                int num10 = 0;
                if (npc.velocity.X < 0f)
                {
                    num10 = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    num10 = 1;
                }
                Vector2 position = npc.position;
                position.X += npc.velocity.X;
                int tileX = (int)((position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num10)) / 16f);
                int tileY = (int)((position.Y + (float)npc.height - 1f) / 16f);
                if (Main.tile[tileX, tileY] == null)
                {
                    Main.tile[tileX, tileY] = new Tile();
                }
                if (Main.tile[tileX, tileY - 1] == null)
                {
                    Main.tile[tileX, tileY - 1] = new Tile();
                }
                if (Main.tile[tileX, tileY - 2] == null)
                {
                    Main.tile[tileX, tileY - 2] = new Tile();
                }
                if (Main.tile[tileX, tileY - 3] == null)
                {
                    Main.tile[tileX, tileY - 3] = new Tile();
                }
                if (Main.tile[tileX, tileY + 1] == null)
                {
                    Main.tile[tileX, tileY + 1] = new Tile();
                }
                if ((float)(tileX * 16) < position.X + (float)npc.width && (float)(tileX * 16 + 16) > position.X && ((Main.tile[tileX, tileY].nactive() && !Main.tile[tileX, tileY].topSlope() && !Main.tile[tileX, tileY - 1].topSlope() && Main.tileSolid[(int)Main.tile[tileX, tileY].type] && !Main.tileSolidTop[(int)Main.tile[tileX, tileY].type]) || (Main.tile[tileX, tileY - 1].halfBrick() && Main.tile[tileX, tileY - 1].nactive())) && (!Main.tile[tileX, tileY - 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 1].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 1].type] || (Main.tile[tileX, tileY - 1].halfBrick() && (!Main.tile[tileX, tileY - 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 4].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 4].type]))) && (!Main.tile[tileX, tileY - 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 2].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 2].type]) && (!Main.tile[tileX, tileY - 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY - 3].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY - 3].type]) && (!Main.tile[tileX - num10, tileY - 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX - num10, tileY - 3].type]))
                {
                    float num13 = (float)(tileY * 16);
                    if (Main.tile[tileX, tileY].halfBrick())
                    {
                        num13 += 8f;
                    }
                    if (Main.tile[tileX, tileY - 1].halfBrick())
                    {
                        num13 -= 8f;
                    }
                    if (num13 < position.Y + (float)npc.height)
                    {
                        float num14 = position.Y + (float)npc.height - num13;
                        if ((double)num14 <= 16.1)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num13;
                            npc.position.Y = num13 - (float)npc.height;
                            if (num14 < 9f)
                            {
                                npc.stepSpeed = 1f;
                            }
                            else
                            {
                                npc.stepSpeed = 2f;
                            }
                        }
                    }
                }
            }
            if (npc.velocity.Y == 0f)
            {
                int tileX = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 2) * npc.direction) + npc.velocity.X * 5f) / 16f);
                int tileY = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
                if (Main.tile[tileX, tileY] == null)
                {
                    Main.tile[tileX, tileY] = new Tile();
                }
                if (Main.tile[tileX, tileY - 1] == null)
                {
                    Main.tile[tileX, tileY - 1] = new Tile();
                }
                if (Main.tile[tileX, tileY - 2] == null)
                {
                    Main.tile[tileX, tileY - 2] = new Tile();
                }
                if (Main.tile[tileX, tileY - 3] == null)
                {
                    Main.tile[tileX, tileY - 3] = new Tile();
                }
                if (Main.tile[tileX, tileY + 1] == null)
                {
                    Main.tile[tileX, tileY + 1] = new Tile();
                }
                if (Main.tile[tileX + npc.direction, tileY - 1] == null)
                {
                    Main.tile[tileX + npc.direction, tileY - 1] = new Tile();
                }
                if (Main.tile[tileX + npc.direction, tileY + 1] == null)
                {
                    Main.tile[tileX + npc.direction, tileY + 1] = new Tile();
                }
                if (Main.tile[tileX - npc.direction, tileY + 1] == null)
                {
                    Main.tile[tileX - npc.direction, tileY + 1] = new Tile();
                }
                int dir = npc.spriteDirection;
                if (npc.type == 423 || npc.type == 410 || npc.type == 546)
                {
                    dir *= -1;
                }
                if ((npc.velocity.X < 0f && dir == -1) || (npc.velocity.X > 0f && dir == 1))
                {
                    bool flag6 = npc.type == 410 || npc.type == 423;
                    float num18 = 3f;
                    if (Main.tile[tileX, tileY - 2].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 2].type])
                    {
                        if (Main.tile[tileX, tileY - 3].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 3].type])
                        {
                            npc.velocity.Y = -8.5f;
                            npc.netUpdate = true;
                        }
                        else
                        {
                            npc.velocity.Y = -7.5f;
                            npc.netUpdate = true;
                        }
                    }
                    else if (Main.tile[tileX, tileY - 1].nactive() && !Main.tile[tileX, tileY - 1].topSlope() && Main.tileSolid[(int)Main.tile[tileX, tileY - 1].type])
                    {
                        npc.velocity.Y = -7f;
                        npc.netUpdate = true;
                    }
                    else if (npc.position.Y + (float)npc.height - (float)(tileY * 16) > 20f && Main.tile[tileX, tileY].nactive() && !Main.tile[tileX, tileY].topSlope() && Main.tileSolid[(int)Main.tile[tileX, tileY].type])
                    {
                        npc.velocity.Y = -6f;
                        npc.netUpdate = true;
                    }
                    else if ((npc.directionY < 0 || Math.Abs(npc.velocity.X) > num18) && (!flag6 || !Main.tile[tileX, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 1].type]) && (!Main.tile[tileX, tileY + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 2].type]) && (!Main.tile[tileX + npc.direction, tileY + 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX + npc.direction, tileY + 3].type]))
                    {
                        npc.velocity.Y = -8f;
                        npc.netUpdate = true;
                    }
                }
            }
        }
        public static Texture2D glowTex = null;

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/StarShower/StarBuck_GM");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.MediumPurple, Color.Yellow, Color.White, Color.MediumPurple));
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.MediumPurple, Color.Yellow, Color.White, Color.MediumPurple));
        }
    }
}