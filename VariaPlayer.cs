using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;

namespace Varia
{
	public class VariaPlayer : ModPlayer
	{
        //Accessories
        public bool infinityCloudEquipped = false;
        public int forgottenSheath = 0;

        //Biomes
        public bool zoneCavity = false;

        //Armor sets
        public bool taxonGreaves = false;
        public bool taxonSetBonus = false;
        public bool taxonSetBonus2 = false;

        //Other
        public bool freeFall = false;
        public bool hisTopHat = false;

        public override void ResetEffects()
        {
            infinityCloudEquipped = false;
            freeFall = false;
            hisTopHat = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (taxonGreaves)
            {
                player.AddBuff(mod.BuffType("TaxonBoost"), 180);
            }
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (hisTopHat)
            {
                if (player.statLife > player.statLifeMax / 2 && damage >= player.statLife)
                {
                    damage = player.statLifeMax * 2 / 3;
                }
            }
            return true;
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (taxonSetBonus == true)
            {
                if (crit == true)
                {
                    player.AddBuff(mod.BuffType("TaxonDamageBoost"), 240);
                }
            }
            if (taxonSetBonus2 == true)
            {
                if (crit == true)
                {
                    player.AddBuff(mod.BuffType("TaxonDefenseBoost"), 240);
                }
            }
            if (forgottenSheath > 0)
            {
                damage = damage / 2 * 3;
                forgottenSheath--;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (taxonSetBonus == true)
            {
                if (crit == true)
                {
                    player.AddBuff(mod.BuffType("TaxonDamageBoost"), 240);
                }
            }
            if (taxonSetBonus2 == true)
            {
                if (crit == true)
                {
                    player.AddBuff(mod.BuffType("TaxonDefenseBoost"), 240);
                }
            }
            if (forgottenSheath > 0 && proj.melee)
            {
                if (forgottenSheath > 18)
                {
                    damage = damage / 2 * 3;
                }
                else
                {

                }
                forgottenSheath--;
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (taxonGreaves)
            {
                player.maxRunSpeed *= (int)1.3f;
            }
        }

        public override void UpdateBiomes()
        {
            zoneCavity = (VariaWorld.cavityTiles > 40);
        }
		
		public override bool CustomBiomesMatch(Player other)
        {
            VariaPlayer otherPlayer = other.GetModPlayer<VariaPlayer>(mod);
            return zoneCavity == otherPlayer.zoneCavity;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            VariaPlayer otherPlayer = other.GetModPlayer<VariaPlayer>(mod);
            otherPlayer.zoneCavity = zoneCavity;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = zoneCavity;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            zoneCavity = flags[0];
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (infinityCloudEquipped && PlayerInput.Triggers.JustPressed.Jump)
            {
                if (player.statMana > 15)
                {
                    if (!Main.tile[(int)player.Center.X / 16, (int)(player.Center.Y / 16) + 2].active() || !Main.tileSolid[Main.tile[(int)player.Center.X / 16, (int)(player.Center.Y / 16) + 2].type])
                    {
                        player.velocity.Y = -7;
                        player.statMana -= 15;
                        Main.PlaySound(16, player.Center);
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust;
                            Vector2 position;
                            position.X = player.Center.X - 30;
                            position.Y = player.Center.Y + player.height / 2;
                            dust = Main.dust[Terraria.Dust.NewDust(position, 60, 10, 91, 0f, 6.315789f, 0, new Color(255, 255, 255), 1.578947f)];
                            dust.noGravity = true;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(51, Main.LocalPlayer);
                            dust.fadeIn = 1.263158f;
                            Dust dust2;
                            Vector2 position2;
                            position2.X = player.Center.X + (i * 2);
                            position2.Y = player.Center.Y + player.height / 2 + 7;
                            dust2 = Terraria.Dust.NewDustPerfect(position2, 212, new Vector2(i, 0f), 0, new Color(255, 255, 255), 1.578947f);
                            dust2.noGravity = true;
                            dust2.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                            Dust dust3;
                            Vector2 position3;
                            position3.X = player.Center.X - (i * 2);
                            position3.Y = player.Center.Y + player.height / 2 + 7;
                            dust3 = Terraria.Dust.NewDustPerfect(position3, 212, new Vector2(-i, 0f), 0, new Color(0, 217, 255), 1.578947f);
                            dust3.noGravity = true;
                            dust3.shader = GameShaders.Armor.GetSecondaryShader(88, Main.LocalPlayer);
                        }
                    }
                    else { }
                }
            }
        }
        public override void PreUpdate()
        {
            if (freeFall)
            {
                if (player.velocity.Y < 100)
                {
                    player.velocity.Y += 2;
                }
                if (Main.tile[(int)(player.Center.X / 16 + 1), (int)(player.Center.Y / 16 + 3)].active() && Main.tile[(int)(player.Center.X / 16 + 1), (int)(player.Center.Y / 16 + 4)].type != 19)
                {
                    player.position.Y = 12;
                }
            }
        }
    }
}