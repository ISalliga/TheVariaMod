using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
    public class CoreSentinel : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core Sentinel");
        }
        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 40;
            npc.damage = 12;
            npc.defense = 0;
            npc.lifeMax = 75;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = 60f;
            npc.knockBackResist = 0.5f;
            npc.noGravity = true;
        }
        private float MovementCounter
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
        float randx = 0;
        float randy = 0;
        Vector2 pcent = new Vector2(0, 0);
        Vector2 moveTo = new Vector2(0, 0);
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (MovementCounter == 1)
            {
                randx = player.Center.X + Main.rand.Next(-300, 300);
                randy = player.Center.Y + Main.rand.Next(-500, -50);
            }
            if (MovementCounter < 40)
            {
                moveTo = new Vector2(randx, randy);
            }
            if (MovementCounter == 60)
            {
                randx = player.Center.X + Main.rand.Next(-100, 100);
                randy = player.Center.Y + Main.rand.Next(-150, -100);
            }
            if (MovementCounter > 60 && MovementCounter < 100)
            {
                moveTo = new Vector2(randx, randy);
            }
            if (MovementCounter == 100)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(0, 8), 438, Main.expertMode ? 10 : 15, 0);
                MovementCounter = 0;
            }

            float speed = 45f;
            Vector2 move = moveTo - npc.Center;
            float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 1f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;

            MovementCounter += 1;
            Lighting.AddLight(npc.Center, 0.5f, 0.25f, 0f);
        }
    }
}