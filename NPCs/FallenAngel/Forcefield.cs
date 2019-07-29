using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using BaseMod;
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
            npc.scale = 0f;
            npc.width = 160;
            npc.height = 160;
            npc.alpha = 255;
            npc.lifeMax = Main.expertMode ? 100 : 150;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = 99999;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forcefield");
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
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
            npc.position.X = parent.Center.X;
            npc.position.Y = parent.Center.Y + 114;
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
                npc.alpha -= 10;
            }
            if (timeAlive > 390) npc.scale -= 0.3f;
            else npc.scale += 0.3f;
            if (timeAlive > 400)
            {
                npc.active = false;
            }
            
            if (npc.scale >= 1.5f) npc.scale = 1.5f;
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
    }
}