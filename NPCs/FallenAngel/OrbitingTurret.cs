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

namespace Varia.NPCs.FallenAngel
{
    public class OrbitingTurret : ModNPC
    {
		int shootTime = 0;
        bool start = true;
        NPC parent;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orbiting Turret");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.scale = 0f;
            npc.lifeMax = Main.expertMode ? 100 : 150;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = Main.expertMode ? 2 : 3;
            npc.width = 40;
            npc.height = 40;
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
            npc.rotation = (Main.player[npc.target].Center - npc.Center).ToRotation();
            Player player = Main.player[npc.target];
            
                
            
            //Factors for calculations
            double deg = (double)npc.ai[1]; //The degrees, you can multiply npc.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 200; //Distance away from angel

            /*Position the npc based on where the player is, the Sin/Cos of the angle times the /
    		/distance for the desired distance away from the player minus the npc's width   /
    		/and height divided by two so the center of the npc is at the right place.     */
            npc.position.X = parent.Center.X - (int)(Math.Cos(rad) * dist) - npc.width / 2;
            npc.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * dist) - npc.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            npc.ai[1] += 3f;

            npc.velocity.X = npc.velocity.X * 3 / 4;
            npc.velocity.Y = npc.velocity.Y * 3 / 4;
            Vector2 playerPos;
            {
                playerPos.X = player.Center.X;
                playerPos.Y = player.Center.Y;
                npc.rotation = npc.AngleTo(playerPos);
            }
            if (npc.scale < 1f)
            {
                npc.scale += 0.1f;
            }
            shootTime++;
            if (shootTime >= 50)
            {
                float Speed = 18f;

                int damage = Main.expertMode ? 25 : 42;

                if (Main.netMode != 1)
                {
                    if (Main.rand.Next(1, 3) != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation) * Speed)), (float)((Math.Sin(npc.rotation) * Speed)), mod.ProjectileType("UnholyTurretBeam"), damage, 0f, Main.myPlayer);
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