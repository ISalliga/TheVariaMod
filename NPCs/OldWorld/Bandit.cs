using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.Graphics.Shaders;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.OldWorld
{
    public class Bandit : ModNPC
    {
        Player player;
        int knifeTimer = 0;
        bool knives = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solix Bandit");
        }

        public override bool CheckActive()
        {
            if (Vector2.Distance(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(player.Center.X, player.Center.Y)) <= Main.screenWidth) return false;
            else return true;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 120 : 180;
            npc.aiStyle = 3;
            npc.damage = Main.expertMode ? 6 : 9;
            npc.defense = 5;
            npc.noGravity = false;
            npc.knockBackResist = 0f;
            Main.npcFrameCount[npc.type] = 6;
            npc.width = 36;
            npc.height = 44;
            npc.HitSound = SoundID.NPCHit48;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 2, 75);
            npc.lavaImmune = true;
        }

        public override void AI()
        {
            npc.TargetClosest();
            knifeTimer++;
            player = Main.player[npc.target];
            if (knifeTimer >= 250)
            {
                knives = true;
                npc.velocity.X = 0;
                if (knifeTimer == 290)
                {
                    //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                    int projectile = Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center) * Main.rand.Next(14, 18), mod.ProjectileType("BanditKnife"), Main.expertMode ? 12 : 18, 0.4f);
                    Main.projectile[projectile].hostile = true;
                    Main.projectile[projectile].friendly = false;
                }
                if (knifeTimer == 287) npc.frame.Y = npc.height * 3;
                if (knifeTimer == 290) npc.frame.Y = npc.height * 4;
                if (knifeTimer == 293) npc.frame.Y = npc.height * 5;
            }
            else
            {
                knives = false;
                npc.spriteDirection = -npc.direction;
                npc.frameCounter++;
                if (npc.frameCounter >= 10) // ticks per frame
                {
                    if (npc.frame.Y < npc.height) npc.frame.Y += npc.height;
                    else npc.frame.Y = 0;
                    npc.frameCounter = 0;
                }
                if (!npc.collideY)
                {
                    npc.frame.Y = npc.height;
                }
            }
            if (knifeTimer > 330)
            {
                knifeTimer = 0;
            }
            if (knifeTimer == 250)
            {
                npc.frame.Y = npc.height * 2;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return Main.LocalPlayer.GetModPlayer<VariaPlayer>().ZoneOldWorld && Main.dayTime ? 0.14f : 0f;
        }

        public override void NPCLoot()
        {
            int rand = Main.rand.Next(40, 91);
            Item.NewItem(npc.position, mod.ItemType("BanditKnife"), rand);
            int rand2 = Main.rand.Next(4, 10);
            Item.NewItem(npc.position, mod.ItemType("OldWorldAlloy"), rand2);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            /*for (int i = 0; i < 10; i++)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(npc.position, npc.width, npc.height, 213, 0f, 0f, 0, new Color(0, 217, 255), 5f)];
                dust.noGravity = true;
                dust.fadeIn = 3f;
            }*/
        }
    }
}