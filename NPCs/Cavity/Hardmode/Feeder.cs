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

namespace Varia.NPCs.Cavity.Hardmode
{
    public class Feeder : ModNPC
    {
        bool trail = false;
		int dashTime = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feeder");
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = Main.expertMode ? 133 : 200;
            npc.aiStyle = 14;
            npc.damage = Main.expertMode ? 36 : 60;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 56;
            npc.height = 36;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.npcSlots = 0.5f;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath17;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
            {
                return VariaWorld.cavityTiles > 75 ? 8f : 0f;
            }
            else return 0.0f;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
        }
        public override void AI()
		{
			Player player = Main.player[npc.target];
            dashTime++;
            if (dashTime > 60 && dashTime < 90) npc.velocity = npc.oldVelocity * 3 / 4;
            if (dashTime == 90) npc.velocity = npc.DirectionFrom(player.Center) * 2;
            if (dashTime == 90) trail = false;
            if (dashTime > 90 && dashTime < 100) npc.velocity = npc.oldVelocity * 2 / 3;
            if (dashTime == 100) npc.velocity = npc.DirectionTo(player.Center) * 12;
            if (dashTime == 100) trail = true;
            if (dashTime == 130) dashTime = 0;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutatedBlob"), Main.rand.Next(3, 8));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (trail)
            {
                Vector2 drawOrigin = npc.position;
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Texture2D OptimeThing = mod.GetTexture("NPCs/Cavity/Hardmode/Feeder");
                    lightColor = new Color(k * 25, k * 15, 0);
                    Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    drawPos.X -= 12;
                    drawPos.Y -= 21;
                    Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                    if (npc.direction == -1) spriteBatch.Draw(OptimeThing, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                    else spriteBatch.Draw(OptimeThing, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            return true;
        }
    }
}