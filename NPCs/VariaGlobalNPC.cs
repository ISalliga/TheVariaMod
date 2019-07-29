using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using System.IO;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Varia;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace Varia.NPCs
{
    public class VariaGlobalNPC : GlobalNPC
    { 
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool banditKnife = false;

        public override void ResetEffects(NPC npc)
        {
            banditKnife = false;
        }

        public override bool Autoload(ref string name)
        {
            return true;
        }

        public override void SetDefaults(NPC npc)
        {
            if (VariaWorld.dropBoost > 0)
            {
                npc.value *= (1.5f * VariaWorld.dropBoost);
            }

            npc.buffImmune[mod.BuffType("BanditKnifeDOT")] = npc.buffImmune[BuffID.BoneJavelin];
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (banditKnife)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int banditKnifeCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == mod.ProjectileType("BanditKnife") && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        banditKnifeCount++;
                    }
                }
                npc.lifeRegen -= banditKnifeCount * 2 * 3;
                if (damage < banditKnifeCount * 3)
                {
                    damage = banditKnifeCount * 3;
                }
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.TaxCollector && VariaWorld.dropBoost > 0)
            {
                npc.life -= 50;
            }
            if (npc.type == NPCID.Guide)
            {
                if (NPC.AnyNPCs(mod.NPCType("SoulOfTheGuide")))
                {
                    if (npc.alpha <= 255) npc.alpha += 15;
                    else npc.alpha = 255;
                    npc.velocity = Vector2.Zero;
                }
                else
                {
                    if (npc.alpha >= 0) npc.alpha -= 15;
                    else npc.alpha = 0;
                }
            }
        }

        public override void TownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay)
        {
            if (npc.type == NPCID.Guide && NPC.AnyNPCs(mod.NPCType("SoulOfTheGuide")))
            {
                projType = 0;
            }
        }

        public override void DrawTownAttackGun(NPC npc, ref float scale, ref int item, ref int closeness)
        {
            if (npc.type == NPCID.Guide && NPC.AnyNPCs(mod.NPCType("SoulOfTheGuide"))) item = 0;
        }

        public override void DrawTownAttackSwing(NPC npc, ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            if (npc.type == NPCID.Guide && NPC.AnyNPCs(mod.NPCType("SoulOfTheGuide"))) item = mod.GetTexture("Items/Miscellaneous/BounceDamage");
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (npc.type == NPCID.Guide && NPC.AnyNPCs(mod.NPCType("SoulOfTheGuide"))) return false;
            return true;
        }

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Tim)
            {
                int rand = Main.rand.Next(4);
                if (rand < 3)
                {
                    Item.NewItem(npc.position, mod.ItemType("ChaoticBliss"));
                }
            }

            if (npc.type == NPCID.RuneWizard)
            {
                int rand = Main.rand.Next(4);
                if (rand < 3)
                {
                    Item.NewItem(npc.position, mod.ItemType("RunicBliss"));
                }
            }

            if (npc.type == NPCID.KingSlime && !VariaWorld.ninja)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Ninja"));
                VariaWorld.ninja = true;
            }

            if (npc.type == NPCID.EyeofCthulhu && !VariaWorld.spaceHasShimmered)
            {
                Main.NewText("Space is shimmering...maybe you should explore it.", 190, 136, 204);
                VariaWorld.spaceHasShimmered = true;
            }

            if (npc.type == 439 && Main.rand.Next(1, 3) == 1)
            {
                switch(Main.rand.Next(0, 2))
                {
                case 0:
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Smallerizer"), 1);
                        break;
                    }
                case 1:
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Bigifier"), 1);
                        break;
                    }
                }
            }
        }
    }
}