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
    public class HisRemnant : ModNPC
    {
        int FrameCountMeter = 0;
		int shootTime = 0;
        NPC parent;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("His Remnant");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[8];
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = Main.expertMode ? 100 : 150;
            npc.aiStyle = 0;
            npc.damage = Main.expertMode ? 25 : 42;
            npc.defense = Main.expertMode ? 2 : 3;
            npc.width = 64;
            npc.height = 106;
            npc.noGravity = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
        }
        public override void AI()
        {
            parent = Main.npc[(int)npc.ai[0]];
            int dashTime = 70;
            if (parent.life < parent.lifeMax * 0.35f)
            {
                dashTime = 60;
            }
            if (parent.life < parent.lifeMax * 0.15f)
            {
                dashTime = 40;
            }
            if (!parent.active)
            {
                npc.active = false;
            }
            Player player = Main.player[npc.target];
            npc.ai[1] += 1f;
            if (npc.ai[1] > dashTime)
            {
                Vector2 tPos;
                tPos.X = player.Center.X + Main.rand.Next(-20, 21);
                tPos.Y = player.Center.Y + Main.rand.Next(-20, 21);
                npc.velocity = npc.DirectionTo(tPos) * 26;
                npc.ai[1] = 0;
            }
            else
            {
                npc.velocity.X = npc.velocity.X * 30 / 31;
                npc.velocity.Y = npc.velocity.Y * 30 / 31;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.rotation = Main.rand.NextFloat(-0.1f, 0.1f);
        }
    }
}