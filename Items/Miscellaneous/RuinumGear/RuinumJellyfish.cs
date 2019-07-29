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
using BaseMod;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.RuinumGear
{
    public class RuinumJellyfish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruinum Jellyfish");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 50 : 75;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 30 : 39;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 26;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit25;
            Main.npcFrameCount[npc.type] = 5;
            npc.DeathSound = SoundID.NPCDeath28;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.LocalPlayer.wet)
            {
                return Main.LocalPlayer.ZoneBeach ? 0.1f : 0f;
            }
            else
                return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 5) // ticks per frame
            {
                npc.frame.Y = (npc.frame.Y / frameHeight + 1) % 5 * frameHeight;
                npc.frameCounter = 0;
            }
        }
        public override void AI()
        {
            float ebip = BaseUtility.MultiLerp(0.05f, new float[] { 50f, 150f, 50f });
            BaseAI.AISpaceOctopus(npc, ref npc.ai, 0.05f, 7f, ebip);
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage += Main.rand.Next(16, 21);
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 5));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RuinumOre"), Main.rand.Next(9, 21));
        }
    }
}