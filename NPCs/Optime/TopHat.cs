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
    public class TopHat : ModNPC
    {
        bool starting = true;
		int shootTime = 0;
        int timeAlive = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Top Hat");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 100 : 140;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 32 : 60;
            npc.defense = Main.expertMode ? 2 : 3;
            npc.width = 18;
            npc.height = 24;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
        }
        
        public override void AI()
        {
            if (!Main.npc[(int)npc.ai[0]].active)
            {
                npc.life = 0;
                npc.checkDead();
            }
            npc.velocity.X = npc.velocity.X * 25 / 26;
            npc.velocity.Y = npc.velocity.Y * 25 / 26;
            Player player = Main.player[npc.target];
            Vector2 playerPos;
            {
                playerPos.X = player.Center.X;
                playerPos.Y = player.Center.Y;
                if (starting)
                {
                    npc.rotation = Main.rand.Next(0, 361);
                    starting = false;
                }
            }
            shootTime++;
            /*
            if (shootTime >= 50)
            {
                float Speed = 18f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = Main.expertMode ? 25 : 42;
                int type = mod.ProjectileType("UnholyTurretBeam");
                float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                Main.projectile[num54].velocity.X += (float)Main.rand.Next(-20, 21) * 0.05f;
                Main.projectile[num54].velocity.Y += (float)Main.rand.Next(-20, 21) * 0.05f;
                Main.projectile[num54].netUpdate = true;
                shootTime = 0;
            }
            */
             if (shootTime >= 30)
            {
                float Speed = 18f;
               
                int damage = Main.expertMode ? 25 : 42;

                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation) * Speed)), (float)((Math.Sin(npc.rotation) * Speed)), mod.ProjectileType("Happifier"), damage, 0f, Main.myPlayer);
                }
               
                shootTime = 0;
            }


            timeAlive++;
            if (timeAlive > 1500)
            {
                npc.life = 0;
                npc.checkDead();
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 1;
        }
    }
}