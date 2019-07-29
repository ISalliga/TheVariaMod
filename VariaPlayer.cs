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
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using Varia;
using Microsoft.Xna.Framework.Input;

namespace Varia
{
    public class VariaPlayer : ModPlayer
    {
        //Varia X Stuff
        public bool SpringheelBoots = false;
        public bool IronSoles = false;
        public int IronSolesCooldown = 0;
        public bool CopterHat = false;

        public int direction2 = 0;
        public bool BoC2 = false;
        public int BoCDamageCount = 0;
        public bool Goggles = false;
        public int ScreenOffsetGoggles = 0;
        public float direction2Y = 0f;
        public float ScreenOffsetTelescope = 0f;
        public bool PocketTelescope = false;
        public bool SniperScope = false;

        public bool ForgottenSpirit = false;
        public bool Skelehands = false;
        public bool LunarSigil = false;

        public bool ZoneOldWorld = false;

        //Accessories
        public bool infinityCloudEquipped = false;
        public int forgottenSheath = 0;
        public bool trydanCore = false;
        public bool bitcrusher = false;
        public bool goldDollarEquipped = false;

        //Summons
        public bool rorPet = false;
        public bool soulBoi = false;
        public bool hunkOChunk = false;
        public bool grimeBaby = false;
        public bool guardianMinion = false;
        public bool sharkMinion = false;
        public bool frostyMist = false;

        //Biomes
        public bool zoneCavity = false;
        public bool zoneBreeze = false;
        public bool nearWindow = false;

        //Armor sets
        public bool taxonGreaves = false;
        public bool taxonSetBonus = false;
        public bool taxonSetBonus2 = false;
        public bool ruinumSetBonus = false;
        public bool cacitianSetBonus = false;
        public int smogTime = 0;
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
        public bool hunterHat = false;
        public bool starShowerShdr = false;
        public bool isCollidingTooth = false;
        public int toothSpikeTimer = 0;

        public static int alignmentValue = 0;
        public bool hostAttack = false;

        public int InfectionTimer = 0;
        public int Infection = 0;
        public int InfectionCap = 3000;
        public int InfectionTimerInc = 3;
        public bool SeeInfectionBar = false;

        public bool host = false;
        
        Vector2 oldPos = Vector2.Zero;

        public override void ResetEffects()
        {
            trydanCore = false;
            taxonGreaves = false;
            taxonSetBonus = false;
            taxonSetBonus2 = false;
            ruinumSetBonus = false;
            infinityCloudEquipped = false;
            hisTopHat = false;
            rorPet = false;
            soulBoi = false;
            hunkOChunk = false;
            frostyMist = false;
            grimeBaby = false;
            sharkMinion = false;
            guardianMinion = false;
            cacitianSetBonus = false;
            glistenynSetBonusMelee = false;
            glistenynSetBonusMagic = false;
            electricStunChance = false;
            mutagen = false;
            fieryWrath = false;
            regrowthAura = false;
            spectrumPotion = false;
            purificationSerum = 0;
            goldSkin = false;
            bitcrusher = false;
            goldDollarEquipped = false;
            isCollidingTooth = false;
            hunterHat = false;
            InfectionTimerInc = 3;
            hostAttack = false;
            SeeInfectionBar = false;
            if (!Main.mapFullscreen) nearWindow = false;

            SpringheelBoots = false;
            IronSoles = false;
            CopterHat = false;
            ForgottenSpirit = false;
            Skelehands = false;
            LunarSigil = false;
            if (!Goggles)
            {
                ScreenOffsetGoggles *= 9 / 10;
                direction2 = 0;
            }
            Goggles = false;
            if (!Goggles) direction2Y = 0f;
            PocketTelescope = false;
            SniperScope = false;
            BoC2 = false;
            BoCDamageCount = 0;
        }

        public override void ModifyScreenPosition()
        {
            if (Goggles)
            {
                if (player.velocity.X > 0) direction2 = 1;
                if (player.velocity.X < 0) direction2 = -1;
                ScreenOffsetGoggles += ((direction2 * 150) - ScreenOffsetGoggles) / 20;
            }
            Main.screenPosition.X += ScreenOffsetGoggles;

            if (PocketTelescope)
            {
                if (Main.keyState.IsKeyDown(Keys.W)) direction2Y = -80;
                else if (Main.keyState.IsKeyDown(Keys.S)) direction2Y = 80;
                else direction2Y = 0;
            }

            ScreenOffsetTelescope += ((direction2Y) - ScreenOffsetTelescope) / 20;

            Main.screenPosition.Y += ScreenOffsetTelescope;
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

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            foreach (NPC target in Main.npc)
            {
                if (target.Distance(player.Center) <= 175 && BoC2) target.AddBuff(BuffID.Confused, 300);
            }
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            foreach (NPC target in Main.npc)
            {
                if (target.Distance(player.Center) <= 175 && BoC2) target.AddBuff(BuffID.Confused, 300);
            }
        }

        public override void SetupStartInventory(IList<Item> items)
        {
            items.Clear();
            Item item1 = new Item();
            item1.SetDefaults(mod.ItemType("BasicSword"));
            item1.stack = 1;
            items.Add(item1);
            Item item2 = new Item();
            item2.SetDefaults(mod.ItemType("BasicPickaxe"));
            item2.stack = 1;
            items.Add(item2);
            Item item3 = new Item();
            item3.SetDefaults(mod.ItemType("BasicAxe"));
            item3.stack = 1;
            items.Add(item3);
            Item item4 = new Item();
            item4.SetDefaults(mod.ItemType("BasicHammer"));
            item4.stack = 1;
            items.Add(item4);
            Item item5 = new Item();
            item5.SetDefaults(mod.ItemType("BasicHook"));
            item5.stack = 1;
            items.Add(item5);
            Item item6 = new Item();
            item6.SetDefaults(mod.ItemType("BasicBoots"));
            item6.stack = 1;
            items.Add(item6);
            Item item7 = new Item();
            item7.SetDefaults(ItemID.LesserHealingPotion);
            item7.stack = 15;
            items.Add(item7);
            Item item8 = new Item();
            item8.SetDefaults(ItemID.LesserManaPotion);
            item8.stack = 40;
            items.Add(item8);

            player.statManaMax = 60;
        }
        
        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (taxonGreaves) player.AddBuff(mod.BuffType("TaxonBoost"), 180);
            if (trydanCore)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("ReactorCloud"), 30, 0f, Main.myPlayer, 0f, 0f);
            }
            if (bitcrusher && Main.rand.NextBool(1, 3))
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("BitcrusherShockwave"), 45, 0f, Main.myPlayer, 0f, 0f);
                Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Miscellaneous/Bitcrusher"));
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
            damage += 5 * BoCDamageCount;

            if (hostAttack && Main.rand.Next(4) == 0)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        {
                            target.AddBuff(BuffID.OnFire, 120);
                            break;
                        }
                    case 1:
                        {
                            target.AddBuff(BuffID.ShadowFlame, 120);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(BuffID.Frostburn, 120);
                            break;
                        }
                    case 3:
                        {
                            target.AddBuff(BuffID.CursedInferno, 120);
                            break;
                        }
                }
            }

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
            damage += 5 * BoCDamageCount;

            if (proj.ranged)
            {
                int damageAddition = (int)proj.Distance(player.Center) * 5 / 100;
                damage += damageAddition;
            }

            if (hostAttack && Main.rand.Next(4) == 0)
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        {
                            target.AddBuff(BuffID.OnFire, 120);
                            break;
                        }
                    case 1:
                        {
                            target.AddBuff(BuffID.ShadowFlame, 120);
                            break;
                        }
                    case 2:
                        {
                            target.AddBuff(BuffID.Frostburn, 120);
                            break;
                        }
                    case 3:
                        {
                            target.AddBuff(BuffID.CursedInferno, 120);
                            break;
                        }
                }
            }

            if (proj.type == ProjectileID.WoodenArrowFriendly && hunterHat)
            {
                float newDMG = (float)damage * 1.4f;
                damage = (int)newDMG;
            }

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
            ZoneOldWorld = (VariaWorld.oldWorldTiles > 40);
            zoneCavity = (VariaWorld.cavityTiles > 60);
            zoneBreeze = (VariaWorld.breezeTiles > 60 || VariaWorld.breezeTiles2 > 30);
            starShowerShdr = VariaWorld.starShower;
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
            otherPlayer.ZoneOldWorld = ZoneOldWorld;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = zoneCavity;
            flags[1] = zoneBreeze;
            flags[2] = ZoneOldWorld;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            zoneCavity = flags[0];
            zoneBreeze = flags[1];
            zoneBreeze = flags[2];
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (BoC2)
                foreach (NPC npc in Main.npc)
                    if (npc.HasBuff(BuffID.Confused)) BoCDamageCount++;

            if (SpringheelBoots)
            {
                if (Math.Abs(player.velocity.X) < 20) player.jumpSpeedBoost += Math.Abs((int)player.velocity.X) / 2;
                else player.jumpSpeedBoost += 10f;
            }

            if (CopterHat)
            {
                if (Main.keyState.IsKeyDown(Keys.Space))
                {
                    player.maxFallSpeed = 4;
                    if (player.wingTimeMax > 0) player.maxFallSpeed = 2;
                    player.noFallDmg = true;
                    player.fallStart = (int)player.position.Y;
                }
            }

            if (IronSoles)
            {
                IronSolesCooldown++;
                if (player.velocity.Y != 0)
                {
                    if (IronSolesCooldown >= 10) Projectile.NewProjectile(new Vector2(player.Center.X, player.position.Y + player.height + (player.velocity.Y / 2)), Vector2.Zero, mod.ProjectileType("IronSoleStomp"), 17, 0f, Main.myPlayer, player.whoAmI);
                }
            }
            else IronSolesCooldown = 0;

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

            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Main.tile[(int)(player.position.X / 16 + i), (int)(player.position.Y / 16 + j)].type == (ushort)mod.TileType("ToothySpike"))
                    {
                        isCollidingTooth = true;
                        break;
                    }
                }
            }

            if (Infection > InfectionCap * 0.3f)
            {
                player.statDefense /= 3;
            }

            if (Infection > InfectionCap * 0.5f)
            {
                player.AddBuff(BuffID.Poisoned, 8);
            }

            if (Infection > InfectionCap * 0.7f)
            {
                player.AddBuff(BuffID.Darkness, 8);
            }

            if (Infection > InfectionCap * 0.9f)
            {
                player.AddBuff(BuffID.Slow, 8);
                if (Main.rand.Next(120) == 0) player.AddBuff(BuffID.Slow, 60);
            }

            if (isCollidingTooth) toothSpikeTimer--;
            else toothSpikeTimer = 0;

            if (toothSpikeTimer == -1)
            {
                player.Hurt(new PlayerDeathReason(), 29, 0);
                toothSpikeTimer = 39;
            }

            if (cacitianSetBonus && player.velocity != Vector2.Zero)
            {
                smogTime++;
                if (smogTime > 15)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("CavitousSmog"), 18, 3f, Main.myPlayer);
                    smogTime = 0;
                }
            }

            if (VariaWorld.starShower && !Main.dayTime)
            {
                if (Main.rand.Next(1, 31) <= 4)
                {
                    {
                        Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                        vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                        vector2_1.Y -= (float)(100);
                        float num12 = Main.rand.Next(-30, 30);
                        float num13 = 100;
                        if ((double)num13 < 0.0) num13 *= -1f;
                        if ((double)num13 < 20.0) num13 = 20f;
                        float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                        float num15 = 10 / num14;
                        float num16 = num12 * num15;
                        float num17 = num13 * num15;
                        float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile X position speed and randomnes
                        float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile Y position speed and randomnes
                        int proj = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1001), player.Center.Y + Main.rand.Next(-1200, -901), SpeedX, SpeedY, ProjectileID.FallingStar, 3, 3, Main.myPlayer, 0.0f, 1);
                        Main.projectile[proj].friendly = false;
                        Main.projectile[proj].hostile = false;
                    }
                }
            }

            if (ruinumSetBonus)
            {
                player.breath = player.breathMax;
            }

            if (Main.mapFullscreen) //Teleportation code by jopojelly. I of course take no credit
            {
                if (nearWindow)
                {
                    if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P))
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
            if (Infection >= 1)
            {
                SeeInfectionBar = true;
            }
            InfectionTimer++;
            if (zoneCavity && !zoneBreeze)
            {
                if (alignmentValue != -50 && alignmentValue != 50)
                {
                    if (InfectionTimer >= InfectionTimerInc && Infection < InfectionCap) Infection += 1;
                }
            }
            else
            {
                if (!host)
                {
                    if (Infection > 0) Infection -= 2;
                    else Infection = 0;
                }
                else if (InfectionTimer >= 6 && Infection > 0) Infection -= 1;
            }
            if (Infection >= InfectionCap)
            {
                player.KillMe(new PlayerDeathReason(), 2346572347905679060, 0, false);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, mod.NPCType("Host"));
                host = true;
            }

            if (!NPC.AnyNPCs(mod.NPCType("Host"))) host = false;
        }

        public override void PreUpdate()
        {
            if (goldDollarEquipped)
            {
                VariaWorld.dropBoost += 1;
            }

            if (purificationSerum == 1)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass };
                Methods.AreaConvert(player.Center, 40, tilesToConvert, 2, 1, DustID.Grass);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone };
                Methods.AreaConvert(player.Center, 40, tilesToConvert2, TileID.Stone, 1, DustID.Grass);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand };
                Methods.AreaConvert(player.Center, 40, tilesToConvert3, TileID.Sand, 1, DustID.Grass);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce };
                Methods.AreaConvert(player.Center, 40, tilesToConvert4, TileID.IceBlock, 1, DustID.Grass);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand };
                Methods.AreaConvert(player.Center, 40, tilesToConvert5, TileID.HardenedSand, 1, DustID.Grass);
            }
            if (purificationSerum == 2)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass, TileID.HallowedGrass };
                Methods.AreaConvert(player.Center, 90, tilesToConvert, 2, 1, DustID.Grass);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone };
                Methods.AreaConvert(player.Center, 100, tilesToConvert2, TileID.Stone, 1, DustID.Grass);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand };
                Methods.AreaConvert(player.Center, 90, tilesToConvert3, TileID.Sand, 1, DustID.Grass);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce };
                Methods.AreaConvert(player.Center, 90, tilesToConvert4, TileID.IceBlock, 1, DustID.GoldFlame);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand };
                Methods.AreaConvert(player.Center, 90, tilesToConvert5, TileID.HardenedSand, 1, DustID.Grass);
            }
            if (purificationSerum == 3)
            {
                List<int> tilesToConvert = new List<int> { TileID.CorruptGrass, TileID.FleshGrass, TileID.HallowedGrass };
                Methods.AreaConvert(player.Center, 130, tilesToConvert, 2, 1, DustID.Grass);
                List<int> tilesToConvert2 = new List<int> { TileID.Ebonstone, TileID.Crimstone, TileID.Pearlstone };
                Methods.AreaConvert(player.Center, 130, tilesToConvert2, TileID.Stone, 1, DustID.Grass);
                List<int> tilesToConvert3 = new List<int> { TileID.Ebonsand, TileID.Crimsand, TileID.Pearlsand };
                Methods.AreaConvert(player.Center, 130, tilesToConvert3, TileID.Sand, 1, DustID.Grass);
                List<int> tilesToConvert4 = new List<int> { TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce };
                Methods.AreaConvert(player.Center, 130, tilesToConvert4, TileID.IceBlock, 1, DustID.Grass);
                List<int> tilesToConvert5 = new List<int> { TileID.CorruptHardenedSand, TileID.CrimsonHardenedSand, TileID.HallowHardenedSand };
                Methods.AreaConvert(player.Center, 130, tilesToConvert5, TileID.HardenedSand, 1, DustID.Grass);
                if (alignmentValue > -50)
                {
                    List<int> DISINFECT = new List<int> { mod.TileType("Holestone") };
                    Methods.AreaConvert(player.Center, 130, DISINFECT, TileID.Stone, 1, DustID.GoldFlame);
                    if (Infection > 0) Infection -= 2;
                }
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

        public override void UpdateBiomeVisuals()
        {
            //player.ManageSpecialBiomeVisuals("FilterBloodMoon", host);
        }
    }
}