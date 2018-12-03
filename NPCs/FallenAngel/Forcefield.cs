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

namespace Varia.NPCs.FallenAngel
{
    public class Forcefield : ModNPC
    {
        NPC parent;
        int timeAlive = 0;
        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.scale = 3f;
            npc.width = 96;
            npc.height = 96;
            npc.alpha = 255;
            npc.lifeMax = Main.expertMode ? 100 : 150;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = 99999;
            npc.noGravity = true;
            Main.npcFrameCount[npc.type] = 4;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forcefield");
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(125, 200, 255, npc.alpha);
        }
        public override bool PreAI()
        {
            //int angelCount = NPC.CountNPCS(mod.NPCType("FallenAngel"));
            parent = Main.npc[(int)npc.ai[0]]; // set parent equal to the identy of the npc that spawned it. 
            if(!parent.active)
            {
                npc.life = 0;
                npc.checkDead();
                
            }
            npc.position.X = parent.Center.X - npc.width / 2;
            npc.position.Y = parent.Center.Y - npc.height / 2;
            timeAlive++;
            npc.life = npc.lifeMax;
            if (timeAlive <= 60)
            {
                npc.dontTakeDamage = true;
            }
            if (timeAlive > 60)
            {
                npc.dontTakeDamage = false;
                npc.reflectingProjectiles = true;
                npc.alpha = 0;
            }
            if (timeAlive > 390)
            {
                npc.scale -= 0.3f;
            }
            if (timeAlive > 400)
            {
                npc.active = false;
            }
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 4) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % Main.npcFrameCount[npc.type] * frameHeight;
                npc.frameCounter = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < 3; k++)
            {
                Texture2D flicker = mod.GetTexture("NPCs/FallenAngel/Forcefield");
                lightColor = new Color(0, 15, 25, npc.alpha);
                Vector2 drawPos = new Vector2(npc.position.X + Main.rand.Next(-15, 16), npc.position.Y + Main.rand.Next(-15, 16));
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(flicker, drawPos, new Rectangle(npc.frame.X, npc.frame.Y, npc.frame.X + npc.width, npc.frame.Y + npc.height), color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}