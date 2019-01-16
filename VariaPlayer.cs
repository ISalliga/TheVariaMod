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
using Varia;

namespace Varia
{
    public class VariaPlayer : ModPlayer
    {
        //Accessories
        public bool infinityCloudEquipped = false;
        public int forgottenSheath = 0;
        public bool trydanCore = false;

        //Summons
        public bool rorPet = false;
        public bool hunkOChunk = false;
        public bool grimeBaby = false;
        public bool guardianMinion = false;

        //Biomes
        public bool zoneCavity = false;
        public bool zoneBreeze = false;
        public bool nearWindow = false;

        //Armor sets
        public bool taxonGreaves = false;
        public bool taxonSetBonus = false;
        public bool taxonSetBonus2 = false;
        public bool glistenynSetBonusMelee = false;
        public bool glistenynSetBonusMagic = false;

        //Buffs
        public bool mutagen = false;
        public bool electricStunChance = false;
        public bool fieryWrath = false;
        public bool regrowthAura = false;
        public bool spectrumPotion = false;
        public int purificationSerum = 0;
        public bool goldSkin = false;

        //Other
        public bool freeFall = false;
        public bool hisTopHat = false;
        Vector2 oldPos = Vector2.Zero;

        public override void ResetEffects()
        {
            trydanCore = false;
            taxonGreaves = false;
            taxonSetBonus = false;
            taxonSetBonus2 = false;
            infinityCloudEquipped = false;
            hisTopHat = false;
            rorPet = false;
            hunkOChunk = false;
            grimeBaby = false;
            guardianMinion = false;
            glistenynSetBonusMelee = false;
            glistenynSetBonusMagic = false;
            electricStunChance = false;
            mutagen = false;
            fieryWrath = false;
            regrowthAura = false;
            spectrumPotion = false;
            purificationSerum = 0;
            goldSkin = false;
            if (!Main.mapFullscreen) nearWindow = false;
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (spectrumPotion)
            {
                r += Main.DiscoR;
                g += Main.DiscoG;
                b += Main.DiscoB;
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (taxonGreaves) player.AddBuff(mod.BuffType("TaxonBoost"), 180);
            if (trydanCore)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("ReactorCloud"), 30, 0f, Main.myPlayer, 0f, 0f);
            }
            if (regrowthAura && damage >= 50) player.AddBuff(mod.BuffType("Regrowth"), Main.rand.Next(20, 36));
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
            if (goldSkin && !target.boss)
            {
                target.value *= 1.05f;
            }

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
            if (glistenynSetBonusMelee && !crit)
            {
                if (damage > item.damage * player.meleeDamage) damage *= 2;
                if (damage < item.damage * player.meleeDamage) damage /= 2;
                if (damage == item.damage * player.meleeDamage) damage *= 4;
            }
            if (electricStunChance && !target.boss && Main.rand.NextBool(1, 5))
            {
                target.AddBuff(mod.BuffType("Stunned"), 35);
            }
            if (mutagen && Main.rand.NextBool(1, 5))
            {
                switch (Main.rand.Next(1, 6))
                {
                    case 1:
                        target.damage += Main.rand.Next(-5, 6);
                        break;
                    case 2:
                        target.defense += Main.rand.Next(-2, 3);
                        break;
                    case 3:
                        target.scale += Main.rand.NextFloat(-0.20f, 0.20f);
                        break;
                    case 4:
                        target.knockBackResist += Main.rand.NextFloat(-0.20f, 0.20f);
                        break;
                    case 5:
                        target.lifeMax += Main.rand.Next(-25, 26);
                        break;
                }
            }
            if (fieryWrath)
            {
                target.AddBuff(BuffID.OnFire, 100);
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (goldSkin && !target.boss)
            {
                target.value *= 1.05f;
            }

            if (taxonSetBonus && crit) player.AddBuff(mod.BuffType("TaxonDamageBoost"), 240);
            if (taxonSetBonus2 && crit) player.AddBuff(mod.BuffType("TaxonDefenseBoost"), 240);

            if (forgottenSheath > 0 && proj.melee)
            {
                if (forgottenSheath > 18)
                {
                    damage = damage / 2 * 3;
                }
                forgottenSheath--;
            }
            if (proj.magic && glistenynSetBonusMagic)
            {
                if (Main.rand.NextBool(1, 5))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Projectile.NewProjectile(Main.MouseWorld, new Vector2(Main.rand.Next(-9, 10), Main.rand.Next(-9, 10)), mod.ProjectileType("GlistenynSparkle"), damage / 10, 0.5f, Main.myPlayer);
                    }
                }
            }
            if (electricStunChance && !target.boss && Main.rand.NextBool(1, 5))
            {
                target.AddBuff(mod.BuffType("Stunned"), 35);
            }
            if (mutagen && Main.rand.NextBool(1, 5))
            {
                switch (Main.rand.Next(1, 6))
                {
                    case 1:
                        target.damage += Main.rand.Next(-5, 6);
                        break;
                    case 2:
                        target.defense += Main.rand.Next(-2, 3);
                        break;
                    case 3:
                        target.scale += Main.rand.NextFloat(-0.20f, 0.20f);
                        break;
                    case 4:
                        target.knockBackResist += Main.rand.NextFloat(-0.20f, 0.20f);
                        break;
                    case 5:
                        target.lifeMax += Main.rand.Next(-25, 26);
                        break;
                }
            }
            if (fieryWrath)
            {
                target.AddBuff(BuffID.OnFire, 100);
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
            zoneCavity = (VariaWorld.cavityTiles > 60);
            zoneBreeze = (VariaWorld.breezeTiles > 60 || VariaWorld.breezeTiles2 > 30);
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
            otherPlayer.zoneBreeze = zoneBreeze;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = zoneCavity;
            flags[1] = zoneBreeze;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            zoneCavity = flags[0];
            zoneBreeze = flags[1];
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (infinityCloudEquipped && PlayerInput.Triggers.JustPressed.Jump)
            {
                if (player.statMana > 15)
                {
                    player.noFallDmg = true;
                    player.noFallDmg = false;
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
        public override void PostUpdate()
        {
            if (Main.mapFullscreen) //Teleportation code by jopojelly. I of course take no credit
            {
                if (nearWindow)
                {
                    if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T))
                    {
                        int mapWidth = Main.maxTilesX * 16;
                        int mapHeight = Main.maxTilesY * 16;
                        Vector2 cursorPosition = new Vector2(Main.mouseX, Main.mouseY);

                        cursorPosition.X -= Main.screenWidth / 2;
                        cursorPosition.Y -= Main.screenHeight / 2;
                        Vector2 mapPosition = Main.mapFullscreenPos;
                        Vector2 cursorWorldPosition = mapPosition;

                        cursorPosition /= 16;
                        cursorPosition *= 16 / Main.mapFullscreenScale;
                        cursorWorldPosition += cursorPosition;
                        cursorWorldPosition *= 16;

                        if (cursorWorldPosition.X < 0) cursorWorldPosition.X = 0;
                        else if (cursorWorldPosition.X + player.width > mapWidth) cursorWorldPosition.X = mapWidth - player.width;
                        if (cursorWorldPosition.Y < 0) cursorWorldPosition.Y = 0;
                        else if (cursorWorldPosition.Y + player.height > mapHeight) cursorWorldPosition.Y = mapHeight - player.height;
                        player.Teleport(cursorWorldPosition);
                        player.velocity = Vector2.Zero;
                        player.fallStart = (int)(player.position.Y / 16f);
                        Main.mapFullscreen = false;
                    }
                }
            }
        }

        public override void PreUpdate()
        {
            if (purificationSerum == 1)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass };
                Methods.AreaConvert(player.Center, 80, tilesToConvert, TileID.Grass, 1, DustID.GoldFlame);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone };
                Methods.AreaConvert(player.Center, 80, tilesToConvert2, TileID.Stone, 1, DustID.GoldFlame);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand };
                Methods.AreaConvert(player.Center, 80, tilesToConvert3, TileID.Sand, 1, DustID.GoldFlame);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce };
                Methods.AreaConvert(player.Center, 80, tilesToConvert4, TileID.IceBlock, 1, DustID.GoldFlame);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand };
                Methods.AreaConvert(player.Center, 80, tilesToConvert5, TileID.HardenedSand, 1, DustID.GoldFlame);
            }
            if (purificationSerum == 2)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass, TileID.HallowedGrass };
                Methods.AreaConvert(player.Center, 100, tilesToConvert, TileID.Grass, 1, DustID.GoldFlame);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone };
                Methods.AreaConvert(player.Center, 100, tilesToConvert2, TileID.Stone, 1, DustID.GoldFlame);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand };
                Methods.AreaConvert(player.Center, 100, tilesToConvert3, TileID.Sand, 1, DustID.GoldFlame);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce };
                Methods.AreaConvert(player.Center, 100, tilesToConvert4, TileID.IceBlock, 1, DustID.GoldFlame);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand };
                Methods.AreaConvert(player.Center, 100, tilesToConvert5, TileID.HardenedSand, 1, DustID.GoldFlame);
            }
            if (purificationSerum == 3)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass, TileID.HallowedGrass };
                Methods.AreaConvert(player.Center, 130, tilesToConvert, TileID.Grass, 1, DustID.GoldFlame);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone, mod.TileType("Holestone") };
                Methods.AreaConvert(player.Center, 130, tilesToConvert2, TileID.Stone, 1, DustID.GoldFlame);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand };
                Methods.AreaConvert(player.Center, 130, tilesToConvert3, TileID.Sand, 1, DustID.GoldFlame);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce };
                Methods.AreaConvert(player.Center, 130, tilesToConvert4, TileID.IceBlock, 1, DustID.GoldFlame);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand };
                Methods.AreaConvert(player.Center, 130, tilesToConvert5, TileID.HardenedSand, 1, DustID.GoldFlame);
            }

            /*if (freeFall) //Rainmaker freefall code
            {
                if (player.velocity.Y < 100)
                {
                    player.velocity.Y += 2;
                }
                if (Main.tile[(int)(player.Center.X / 16 + 1), (int)(player.Center.Y / 16 + 3)].active() && Main.tile[(int)(player.Center.X / 16 + 1), (int)(player.Center.Y / 16 + 4)].type != 19)
                {
                    player.position.Y = 12;
                    if (Main.rand.NextFloat() < 1f)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Dust dust;
                            Vector2 position = player.Center;
                            dust = Main.dust[Terraria.Dust.NewDust(position, 1, 1, 16, (float)Main.rand.Next(-5, 6), 7.105264f, 0, new Color(255, 255, 255), 1f)];
                            dust.shader = GameShaders.Armor.GetSecondaryShader(33, Main.LocalPlayer);
                            dust.fadeIn = 0.9868421f;
                        }
                    }
                }
                player.canCarpet = false;
            }
            if (NPC.AnyNPCs(mod.NPCType("TheRainmaker"))) freeFall = true;
            else freeFall = false;*/
        }
    }
}