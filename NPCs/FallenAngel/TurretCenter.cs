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
    public class TurretCenter : ModNPC
    {
        Player player;
        NPC parent;
        int turretTime = 0;
        public override void SetDefaults()
        {
            npc.alpha = 255;
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
            DisplayName.SetDefault("Centerpiece");
        }
        public override bool PreAI()
        {
            player = Main.player[npc.target];
            npc.TargetClosest();
            //int angelCount = NPC.CountNPCS(mod.NPCType("FallenAngel"));
            parent = Main.npc[(int)npc.ai[0]]; // set parent equal to the identy of the npc that spawned it. 
            if(!parent.active)
            {
                npc.life = 0;
                npc.checkDead();
            }
            turretTime++;
            if (turretTime == 20 || turretTime == 40 || turretTime == 60 || turretTime == 80)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("OrbitingTurret"), 0, npc.whoAmI);
            }
            if (turretTime >= 100)
            {
                Vector2 tPos;
                tPos.X = player.Center.X;
                tPos.Y = player.Center.Y - 70;
                npc.velocity.X = (npc.DirectionTo(tPos).X * Vector2.Distance(npc.Center, tPos) / 50);
                npc.velocity.Y = (npc.DirectionTo(tPos).Y * Vector2.Distance(npc.Center, tPos) / 50);
            }
            else
            {
                npc.position.X = parent.position.X;
                npc.position.Y = parent.position.Y;
            }
            return false;
        }
    }
}