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
    public class OrbitingMask : ModNPC
    {
        int frameThing = 0;
        int shootTime = 0;
        bool start = true;
        NPC parent;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orbiting Mask");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.scale = 1f;
            npc.lifeMax = Main.expertMode ? 15 : 10;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = 9999;
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


            //Factors for calculations
            double deg = (double)npc.ai[1] * 2; //The degrees, you can multiply npc.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 100; //Distance away from Optime

            /*Position the npc based on where the player is, the Sin/Cos of the angle times the /
    		/distance for the desired distance away from the player minus the npc's width   /
    		/and height divided by two so the center of the npc is at the right place.     */
            Vector2 traj = new Vector2(parent.Center.X - (int)(Math.Cos(rad) * dist), parent.Center.Y - (int)(Math.Sin(rad) * dist));
            npc.velocity = npc.DirectionTo(traj) * 5;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            npc.ai[1] += 2f;
            if (npc.scale < 1f)
            {
                npc.scale += 0.1f;
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 1;
        }
    }
}