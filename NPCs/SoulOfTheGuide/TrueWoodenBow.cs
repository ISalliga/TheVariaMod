using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Varia;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.SoulOfTheGuide
{
    public class TrueWoodenBow : ModNPC
    {
        int shootTime = 0;
        bool start = true;
        int despawn = 0;
        NPC parent;
        
        int dashTimer = 50;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Wooden Bow");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.scale = 1f;
            npc.dontTakeDamage = true;
            npc.lifeMax = Main.expertMode ? 100 : 150;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = 9999;
            npc.width = 62;
            npc.height = 62;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override bool PreAI()
        {
            //int angelCount = NPC.CountNPCS(mod.NPCType("FallenAngel"));
            parent = Main.npc[(int)npc.ai[0]];
            if (!parent.active)
            {
                npc.active = false;
            }
            if (start)
            {

                start = false;
            }
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];


            if (Main.expertMode)
            {
                if (parent.active)
                {
                    if (parent.life > parent.lifeMax * 0.4f)
                    {
                        npc.position = new Vector2(parent.position.X - 7, parent.position.Y - 7);
                    }
                    else
                    {
                        if (Main.player[npc.target].dead)
                        {
                            npc.velocity.Y -= despawn;
                            despawn++;
                            if (despawn > 40)
                            {
                                npc.active = false;
                            }
                        }
                        else
                        {
                            dashTimer++;
                            if (dashTimer > 70)
                            {
                                npc.velocity = npc.DirectionTo(player.Center) * (Main.hardMode ? 7f : 4f);
                                dashTimer = 0;
                            }
                        }
                        
                    }
                }
                else
                {
                    npc.active = false;
                }
            }
            else
            {
                if (parent.active)
                {
                    npc.position = new Vector2(parent.position.X - 7, parent.position.Y - 7);
                }
                else
                {
                    npc.active = false;
                }
            }
            Vector2 playerPos = player.Center;
            npc.rotation = npc.AngleTo(playerPos);
            shootTime++;
            if (shootTime >= 70)
            {
                float Speed = 10f;
                if (Main.hardMode) Speed = 14f;

                int damage;

                if (!Main.hardMode) damage = Main.expertMode ? 6 : 4;
                else damage = Main.expertMode ? 25 : 17;

                if (Main.netMode != 1)
                {
                    if (Main.rand.Next(1, 3) != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation) * Speed)), (float)((Math.Sin(npc.rotation) * Speed)), mod.ProjectileType("SoulboundArrow"), damage, 0f, Main.myPlayer);
                    }
                }
                shootTime = 0;
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 1;
        }
    }
}