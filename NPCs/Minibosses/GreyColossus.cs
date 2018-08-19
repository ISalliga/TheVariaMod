using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.Minibosses
{

    public class GreyColossus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grey Colossus");
            //NPCID.Sets.TrailCacheLength[npc.type] = 8;
            //NPCID.Sets.TrailingMode[npc.type] = 1;
            Main.npcFrameCount[npc.type] = 2; //the number of frames your npc has
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = Main.expertMode ? 25000 / 2 : 17500;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 50 : 75;
            npc.defense = 2;
            npc.knockBackResist = 0.2f;
            npc.width = 88;
            npc.height = 104;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = false;
            Main.npcFrameCount[npc.type] = 2;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.noGravity = true;
        }
        public override void NPCLoot()
        {
            //loot goes here
        }


        //change these variables to quickly tweak parts of the AI
        int stunTime = 60 * 4; // stun time in frames. 60 frames in 1 second
        int freezeAfterStun = 30; // frames until the npc stops moving in its stun
        float flySpeed = 5f; // how fast the npc will fly at the player
        float bounceSpeed = 15f; // how fast the npc is knocked back from the wall
        float teleportDistance = 500; // distance the npc will teleport from the player
        int teleportTries = 100; // number of teleport attampts before the npc rage quits

        //these variables are used for logic in the AI
        int stunTimer;
        float flyDirection;
        int bounceCooldown;
        bool ragequit = false;
        public override void AI()
        {
            Player player = Main.player[npc.target]; // set the variable player to the npc's target
            npc.TargetClosest(true);

            if ((npc.collideX || npc.collideY) && bounceCooldown <= 0) // npc hit a tile
            {
                bounceCooldown = 3;
                if (stunTimer <= 0)
                {

                    stunTimer = stunTime;




                }
                if (npc.collideX)
                {
                    npc.velocity.X *= -1; //makes the npc bounce
                }
                if (npc.collideY)
                {
                    npc.velocity.Y *= -1; //makes the npc bounce
                }
                npc.velocity = npc.velocity.SafeNormalize(-Vector2.UnitY) * bounceSpeed; //alters the nps's speed based on bounceSpeed
            }
            if (stunTimer > 0) //is the boss stunned?
            {
                stunTimer--;
                npc.velocity.X = npc.velocity.X * 10 / 11;
                npc.velocity.Y = npc.velocity.Y * 10 / 11;
                if (stunTimer == 2) //near end of stun event
                {
                    for (int c = 0; c < teleportTries; c++)
                    {
                        ragequit = true;
                        if (Main.netMode != 1) // don't run on client
                        {
                            //Use the npc.ai[0] variable as it's easy to sync in multiplayer
                            //server sync whenever randomizing so the client and server won't disagree
                            npc.ai[0] = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI); // sets the npc.ai[0] variable to a random radian angle
                            npc.netUpdate = true; // update the client's npc.ai[0] variable  to be equal to the server's
                        }
                        Vector2 teleTo = new Vector2(player.Center.X + (float)Math.Cos(npc.ai[0]) * teleportDistance, player.Center.Y + (float)Math.Sin(npc.ai[0]) * teleportDistance);
                        if (Collision.CanHit(new Vector2(teleTo.X - npc.width / 2, teleTo.Y - npc.height / 2), npc.width, npc.height, player.position, player.width, player.height))// checks if there are no tiles between player and potential teleport spot
                        {
                            npc.Center = teleTo; //teleport npc

                            ragequit = false;
                            break; //end for loop

                        }
                    }
                    if (ragequit)
                    {
                        npc.Center = player.Center;
                    }
                }



                npc.dontTakeDamage = false;
            }
            else
            {
                npc.dontTakeDamage = true;
                flyDirection = (player.Center - npc.Center).ToRotation(); //finds the difference in vectors between the player and the npc then converts it to an angle in radians
                //Set the npc's velocity
                npc.velocity.X = (float)Math.Cos(flyDirection) * flySpeed;
                npc.velocity.Y = (float)Math.Sin(flyDirection) * flySpeed;
                // Sine and Cosine are used to create a ratio between the X and Y velocities when multiplied by the fly Speed it will make the npc move in the desired vector velocity
                // Math.Sin and Math.Cos return as doubles the (float) converts them to floats
            }
            bounceCooldown--;
        }
        public override void FindFrame(int frameHeight)
        {
            if (stunTimer <= 0)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else
            {
                npc.frame.Y = 1 * frameHeight;
            }
        }


    }
}