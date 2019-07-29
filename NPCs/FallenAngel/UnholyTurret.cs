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
using BaseMod;

namespace Varia.NPCs.FallenAngel
{
    public class UnholyTurret : ModNPC
    {
		int shootTime = 0;
        int timeAlive = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unholy Turret");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.scale = 0f;
            npc.lifeMax = Main.expertMode ? 80 : 120;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = Main.expertMode ? 2 : 3;
            npc.width = 62;
            npc.height = 62;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        
        public override void AI()
        {
            //int angelCount = NPC.CountNPCS(mod.NPCType("FallenAngel"));
            if (!Main.npc[(int)npc.ai[0]].active)
            {
                npc.life = 0;
                npc.checkDead();
            }
            npc.velocity.X = npc.velocity.X * 3 / 4;
            npc.velocity.Y = npc.velocity.Y * 3 / 4;
            Player player = Main.player[npc.target];
            Vector2 playerPos;
            {
                playerPos.X = player.Center.X;
                playerPos.Y = player.Center.Y;
                npc.rotation = npc.AngleTo(playerPos);
            }
            if (npc.scale < 1f)
            {
                npc.scale += 0.1f;
                npc.width = 62;
                npc.height = 62;
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
             if (shootTime >= 50)
            {
                float Speed = 18f;
               
                int damage = Main.expertMode ? 25 : 42;

                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation) * Speed)), (float)((Math.Sin(npc.rotation) * Speed)), mod.ProjectileType("UnholyTurretBeam"), damage, 0f, Main.myPlayer);
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

        public static Texture2D glowTex = null;

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override void PostDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/FallenAngel/UnholyTurret_GM");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Blue, Color.White, Color.SkyBlue, Color.Blue));
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Blue, Color.White, Color.SkyBlue, Color.Blue));
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 1;
        }
    }
}