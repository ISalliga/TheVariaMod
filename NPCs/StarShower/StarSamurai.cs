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
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.StarShower
{
    public class StarSamurai : ModNPC
    {
        int frameNumber = 0;
        int teleporterTimer = -120;
        int slashFrameTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellar Samurai");
            Main.npcFrameCount[npc.type] = 9;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 130;
            npc.defense = 1;
            npc.knockBackResist = 0f;
            npc.width = 44;
            npc.height = 54;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.value = Item.buyPrice(0, 0, 10, 10);
            npc.npcSlots = 1f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath51;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 420;
            npc.defense = 0;
        }
        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            Vector2 moveTo = player.Center + new Vector2(-player.direction * 80, -npc.height / 2);
            teleporterTimer++;
            if (teleporterTimer == 70) frameNumber = 1;
            if (teleporterTimer == 80) frameNumber = 2;
            if (teleporterTimer == 90) frameNumber = 3;
            if (teleporterTimer == 100)
            {
                frameNumber = 4;
                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 226, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
                }
                npc.position = moveTo;
                for (int i = 0; i < 10; i++)
                {
                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = npc.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 226, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(50, Main.LocalPlayer);
                }
            }

            if (teleporterTimer >= 100 && teleporterTimer < 220)
                npc.velocity = npc.DirectionTo(moveTo) * npc.Distance(moveTo) / 6;

            if (teleporterTimer == 220)
            {
                npc.velocity = Vector2.Zero;
                frameNumber = 5;
            }

            if (teleporterTimer == 230) frameNumber = 6;

            if (teleporterTimer == 240)
            {
                frameNumber = 7;
                npc.velocity = npc.DirectionTo(player.Center) * 24;
                npc.damage = 14;
            }

            if (teleporterTimer >= 250)
            {
                npc.velocity *= 0.79f;
                frameNumber = 8;
            }

            if (teleporterTimer == 300)
            {
                teleporterTimer = 0;
                npc.damage = 0;
                frameNumber = 0;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.X > 0) npc.spriteDirection = -1;
            else npc.spriteDirection = 1;
            npc.frame.Y = frameHeight * frameNumber;
        }
    }
}
