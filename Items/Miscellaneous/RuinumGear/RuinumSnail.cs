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
    public class RuinumSnail : ModNPC
    {
        int phase = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruinum Snail");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 130 : 205;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 30 : 39;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 42;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit2;
            Main.npcFrameCount[npc.type] = 7;
            npc.DeathSound = SoundID.NPCDeath1;
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
            if (phase < 1)
            {
                if (npc.frameCounter >= 5) // ticks per frame
                {
                    npc.frame.Y = (npc.frame.Y / frameHeight + 1) % 4 * frameHeight;
                    npc.frameCounter = 0;
                }
            }
            else
            {
                if (npc.frameCounter >= 4) // ticks per frame
                {
                    npc.frame.Y = ((npc.frame.Y / frameHeight + 1) % 3) + 4 * frameHeight;
                    npc.frameCounter = 0;
                }
            }
        }
        public override void AI()
        {
            if (phase < 1)
            {
                int snailThing = 0;
                BaseAI.AISnail(npc, ref npc.ai, ref snailThing);
            }
            else
            {
                npc.HitSound = SoundID.NPCHit1;
                npc.defense = 0;
                npc.noGravity = true;
                BaseAI.AIFish(npc, ref npc.ai, true, true, false, 7, 4);
            }

            if (npc.life <= npc.lifeMax * 0.67f)
            {
                if (phase < 1)
                {
                    BaseAI.DropItem(npc, mod.ItemType("RuinumOre"), Main.rand.Next(10, 21), 250, 1f);
                }
                phase = 2;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (phase >= 1)
            {
                damage += Main.rand.Next(14, 19);
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (phase >= 1)
            {
                damage += Main.rand.Next(14, 19);
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Gel, Main.rand.Next(1, 5));
        }
    }
}