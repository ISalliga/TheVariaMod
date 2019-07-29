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
    [AutoloadBossHead]
    public class SoulOfTheGuide : ModNPC
    {
        bool bowSpawned = false;
        int vMax = 3;
        float vAccel = 0.35f;
        float tVel = 0;
        float vMag = 0;

        int despawn = 0;

        int timer = 0;
        int timerInc = 124;

        int attackTimer1 = 1000;
        int attackTimer2 = 1000;
        int attackTimer3 = 1000;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Guide");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            if (!Main.hardMode) npc.lifeMax = Main.expertMode ? 600 : 1700;
            else npc.lifeMax = Main.expertMode ? 3000 : 5000;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 6 : 9;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SoulOfTheGuide");
            npc.width = 48;
            npc.height = 52;
            npc.boss = true;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 3, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            Main.npcFrameCount[npc.type] = 4;
            npc.HitSound = SoundID.NPCHit36;
            npc.DeathSound = SoundID.NPCDeath39;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulShard"), Main.rand.Next(4, 7));
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulBag"), 1);
            }
            potionType = ItemID.LesserHealingPotion;
            VariaWorld.downedSotG = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax*2*bossLifeScale); // more health in expert for more players
        }
        public override void AI()
        {
            npc.TargetClosest();
            if (!bowSpawned)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("TrueWoodenBow"), 0, npc.whoAmI);
                bowSpawned = true;
            }
            Player player = Main.player[npc.target];
            float targetX = player.Center.X;
            float targetY = player.Center.Y;

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
                targetX = player.Center.X;
                targetY = player.Center.Y - 100;
                {
                    float dist = ((float)(Math.Sqrt((targetX - npc.Center.X) * (targetX - npc.Center.X) + (targetY - npc.Center.Y) * (targetY - npc.Center.Y))));
                    tVel = dist / 20;
                    if (vMag < vMax && vMag < tVel)
                    {
                        vMag += vAccel;
                    }
                    if (vMag > tVel)
                    {
                        vMag -= vAccel;
                    }

                    if (dist != 0)
                    {
                        Vector2 tPos;
                        tPos.X = targetX;
                        tPos.Y = targetY;
                        if (!Main.hardMode) npc.velocity = npc.DirectionTo(tPos) * (vMag - 0.6f);
                        else npc.velocity = npc.DirectionTo(tPos) * (vMag + 2.3f);
                    }
                }
            }

            timer++;

            if (timer > timerInc)
            {
                switch (Main.rand.Next(1, 4))
                {
                    case 1:
                        {
                            if (Main.hardMode) npc.velocity = npc.DirectionTo(player.Center) * 14;
                            attackTimer1 = 0;
                            break;
                        }
                    case 2:
                        {
                            attackTimer2 = 0;
                            break;
                        }
                    case 3:
                        {

                            break;
                        }
                }
                timer = 0;
                if (Main.hardMode) timerInc = Main.rand.Next(80, 96);
                else timerInc = Main.rand.Next(115, 136);
            }

            attackTimer2++;

            if (attackTimer2 < 20) npc.velocity = Vector2.Zero;

            if (attackTimer2 == 20)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 30, mod.NPCType("SoulFragment"));
                if (Main.hardMode)
                {
                    NPC.NewNPC((int)npc.Center.X - 50, (int)npc.Center.Y - 20, mod.NPCType("SoulFragment"));
                    NPC.NewNPC((int)npc.Center.X + 50, (int)npc.Center.Y - 20, mod.NPCType("SoulFragment"));
                }
            }

            attackTimer1++;

            if (!Main.hardMode)
            {
                if (attackTimer1 == 25 || attackTimer1 == 50)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("SoulFire"), Main.expertMode ? 7 : 10, 0.3f, Main.myPlayer);
                }
            }
            else
            {
                if (attackTimer1 == 10 || attackTimer1 == 20 || attackTimer1 == 30 || attackTimer1 == 40)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("SoulFire"), Main.expertMode ? 20 : 30, 0.3f, Main.myPlayer);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 5) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }
    }
}