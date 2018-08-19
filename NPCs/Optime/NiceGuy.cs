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

namespace Varia.NPCs.Optime
{
    [AutoloadBossHead]
    public class NiceGuy : ModNPC
    {
        int despawn = 0;

        int maskCount = 0;
        int attackTime = 0;
        int maskTime = 0;
        int boltTime = 0;
        bool maskSpam = false;
        bool bolts = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nice Guy");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 3150 : 6000;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 90;
            npc.defense = 0;
            npc.knockBackResist = 0.2f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/NiceGuy");
            npc.width = 64;
            npc.height = 102;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 11, 0, 0);
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            Main.npcFrameCount[npc.type] = 4;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Boss/NiceGuy_Hit");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax*2*bossLifeScale); // more health in expert for more players
        }
        //Main.netMode !=1 prevents things from happening on the client, over 99% of multiplayer specific bugs are from client server desync
        //Server dessyncs are cause by rng as the server and client can roll different numbers
        // Projectile.NewProjectile() when run on both server and client will cause two projectiles to generate
        /*public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PureConcentradedEdge"), Main.rand.Next(26, 34)); // darklight essence.... hmmmmmm
                }
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OptimeBag"), 1);
            }
            potionType = ItemID.GreaterHealingPotion;
            VariaWorld.downedAngel = true;
        }*/
        public override void AI()
        {
            maskCount = NPC.CountNPCS(mod.NPCType("OrbitingMask"));

            Player player = Main.player[npc.target];

            if(Main.player[npc.target].dead)
            {
                npc.position.Y += 5;
                despawn++;
                if (despawn > 50)
                {
                    npc.active = false;
                }
            }

            attackTime++;
            if (attackTime == 300)
            {
                switch(Main.rand.Next(1, 4))
                {
                    case 1:
                        {
                            if (maskCount == 0)
                            {
                                maskSpam = true;
                            }
                            break;
                        }
                    case 2:
                        {
                            bolts = true;
                            break;
                        }
                    case 3:
                        {
                            Projectile.NewProjectile(new Vector2(player.Center.X - 400, player.Center.Y), npc.DirectionTo(new Vector2(npc.Center.X + 20, npc.Center.Y)) * 20, mod.ProjectileType("Happifier"), Main.expertMode ? 40 : 70, 0.4f);
                            Projectile.NewProjectile(new Vector2(player.Center.X + 400, player.Center.Y), npc.DirectionTo(new Vector2(npc.Center.X - 20, npc.Center.Y)) * 20, mod.ProjectileType("Happifier"), Main.expertMode ? 40 : 70, 0.4f);
                            Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y - 400), npc.DirectionTo(new Vector2(npc.Center.X, npc.Center.Y + 20)) * 20, mod.ProjectileType("Happifier"), Main.expertMode ? 40 : 70, 0.4f);
                            Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y + 400), npc.DirectionTo(new Vector2(npc.Center.X, npc.Center.Y - 20)) * 20, mod.ProjectileType("Happifier"), Main.expertMode ? 40 : 70, 0.4f);
                            break;
                        }
                }
                attackTime = 0;
            }
            if (maskSpam)
            {
                maskTime++;
                if (maskTime == 1 || maskTime == 12 || maskTime == 24 || maskTime == 36 || maskTime == 48 || maskTime == 60)
                {
                    int mask = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrbitingMask"), 0, npc.whoAmI);
                    Main.npc[mask].velocity.X = Main.rand.Next(-10, 10);
                    Main.npc[mask].velocity.Y = Main.rand.Next(-10, 10);
                }
            }

            if (bolts)
            {
                boltTime++;
                if (boltTime == 1 || boltTime == 12 || boltTime == 24 || boltTime == 36 || boltTime == 48)
                {
                    Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center) * 20, mod.ProjectileType("Happifier"), Main.expertMode ? 40 : 70, 0.4f);
                    Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 72), npc.Center);
                }
                if (boltTime == 48)
                {
                    boltTime = 0;
                    bolts = false;
                }
            }
        }
        public override bool CheckDead()
        {
            npc.active = false;
            int optime = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Optime"));
            Main.npc[optime].velocity.Y = -25;
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 0;
            npc.rotation = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 10) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }
    }
}