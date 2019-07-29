using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.NPCs.CoreOfMutation
{
    public class CoreOfMutation : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Core of Mutation");
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 80;
			npc.damage = 12;
			npc.defense = 0;
			npc.lifeMax = Main.expertMode ? 2000 : 3000;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.boss = true;
			npc.value = 60f;
			npc.knockBackResist = 0f;
			npc.noGravity = true;
			npc.noTileCollide = true;
            npc.aiStyle = 0;
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CoreOfMutation");
		}
		private float MovementCounter
		{
			get
			{
				return npc.ai[0];
			}
			set
			{
				npc.ai[0] = value;
			}
		}
		private float AttackChoice
		{
			get
			{
				return npc.ai[1];
			}
			set
			{
				npc.ai[1] = value;
			}
		}
        private float Spin
		{
			get
			{
				return npc.ai[2];
			}
			set
			{
				npc.ai[2] = value;
			}
		}
        private float MonolithCounter
		{
			get
			{
				return npc.ai[3];
			}
			set
			{
				npc.ai[3] = value;
			}
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.75f);
			npc.defense = 2;
		}
        int monolith = 0;
        int spinrand = 0;
        float randx = 0;
        float randy = 0;
        Vector2 pcent = new Vector2(0, 0);
        Vector2 moveTo = new Vector2(0, 0);
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (MonolithCounter > 600)
            {
                if (!NPC.AnyNPCs(mod.NPCType("MutationMonolith"))) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 50, mod.NPCType("MutationMonolith"));
                if (Main.expertMode && Main.rand.Next(3) == 0)
                {
                    if (NPC.CountNPCS(mod.NPCType("CoreSentinel")) < 4) NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("CoreSentinel"));
                }
                MonolithCounter = 0;
            }
            if (MovementCounter == 1)
            {
                randx = player.Center.X + Main.rand.Next(-500, 500);
                randy = player.Center.Y + Main.rand.Next(-500, -100);
            }
            if (MovementCounter < 40)
            {
                moveTo = new Vector2(randx, randy);
            }
            if (MovementCounter == 40)
            {
                randx = player.Center.X + Main.rand.Next(-100, 100);
                randy = player.Center.Y + Main.rand.Next(-250, -200);
            }
            if (MovementCounter > 40 && MovementCounter < 80)
            {  
                moveTo = new Vector2(randx, randy);
            }
            if (MovementCounter == 80)
            {  
                AttackChoice = Main.rand.Next(5);
            }
            if (MovementCounter > 80)
            {
                if (AttackChoice == 0) // Concentrated Beam
                {
                    if (MovementCounter > 80 && MovementCounter < 160)
                    {
                        pcent = player.Center;
                    } 
                    if (MovementCounter > 80 && MovementCounter < 240)
                    {
                        Vector2 heading = pcent - npc.Center;
			            heading.Normalize();
			            heading *= new Vector2(30, 30).Length();
                        float speedX = heading.X;
			            float speedY = heading.Y;
		               	Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speedX, speedY, mod.ProjectileType("WarningBeam"), 0, 0);
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 28);
                    }
                    if (MovementCounter == 240)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 124);
                    }
                    if (MovementCounter > 240)
                    {
                        Vector2 heading = pcent - npc.Center;
			            heading.Normalize();
			            heading *= new Vector2(20, 20).Length();
                        float speedX = heading.X;
			            float speedY = heading.Y;
		               	Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-30, 30), npc.Center.Y + Main.rand.Next(-30, 30), speedX, speedY, mod.ProjectileType("Splitter"), Main.expertMode ? 35 : 48, 0);
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 75);
                    }
			        if (MovementCounter == 300)
                    {
                        MovementCounter = 0;
                    }
                }
                else // Uneven Spread
                {
                    if (MovementCounter == 82)
                    {
                        spinrand = Main.rand.Next(360);
                        Spin = spinrand;
                        for (int k = 0; k < 16; k++)
                        {
                            Spin += 15; // Rotation Variable
                            float rotation = MathHelper.ToRadians(Spin); // Set the actual rotation counts
                            Vector2 perturbedSpeed = new Vector2(180, 180).RotatedBy(MathHelper.Lerp(-rotation, rotation, 16)) * .2f; // Adjust the rotation on the projectile
                            Projectile.NewProjectile(npc.Center, perturbedSpeed, mod.ProjectileType("WarningBeam"), 30, 0);
                        }
                    }
                    if (MovementCounter == 190 && !Main.expertMode || MovementCounter == 160 && Main.expertMode)
                    {
                        Spin = spinrand;
                        MovementCounter = 0;
                        for (int k = 0; k < 16; k++)
                        {
                            Spin += 15; // Rotation Variable
                            float rotation = MathHelper.ToRadians(Spin); // Set the actual rotation counts
                            Vector2 perturbedSpeed = new Vector2(80, 80).RotatedBy(MathHelper.Lerp(-rotation, rotation, 16)) * .2f; // Adjust the rotation on the projectile
                            Projectile.NewProjectile(npc.Center, perturbedSpeed, 438, Main.expertMode ? 13 : 20, 0);
                        }
                    }
                }     
            }
            // Move time.
			float speed = 30f;
			Vector2 move = moveTo - npc.Center;
			float magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
			if(magnitude > speed)
			{
		    	move *= speed / magnitude;
		    }
		    float turnResistance = 1f;
		    move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
		    magnitude = (float)Math.Sqrt(move.X * move.X + move.Y * move.Y);
		    if(magnitude > speed)
		    {
		    	move *= speed / magnitude;
		    }
		    npc.velocity = move;

            // Increase counters + check for the monolith heal
            MovementCounter += 1;
            if (Main.expertMode)
                MovementCounter += 1; // Does speeding through phases faster make something harder? Probably.
            if (NPC.AnyNPCs(mod.NPCType("MutationMonolith")))
            {
                monolith += 1;
                if (monolith > 180)
                {
                    if (npc.life < npc.lifeMax - 50) npc.life += 50;
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), new Color(0f, 255f, 0f), "50", false, false);
                    monolith = 0;
                }
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6, 0f, 0f, 50, default(Color), 1f);
            }
            MonolithCounter += 1;
            npc.netUpdate = true; // For a *tiny* bit of multiplayer compatibility. 
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if(npc.life < 1)
            {
                for(int k = 0; k < 500; k++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 27, 0f, 0f, 50, default(Color), 2.5f);
                    Main.dust[dust].velocity *= 100f;
                    Main.dust[dust].noGravity = true;
                }
                npc.netUpdate = true;
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                int numOfWeapons = 2;
                int weaponPoolCount = 7;
                int[] weaponLoot = new int[numOfWeapons];
                for (int n = 0; n < numOfWeapons; n++)
                {
                    weaponLoot[n] = Main.rand.Next(weaponPoolCount - n);
                    for (int j = 0; j < n; j++)
                    {
                        if (weaponLoot[n] >= weaponLoot[j])
                        {
                            weaponLoot[n]++;
                        }
                        Array.Sort(weaponLoot);
                    }
                }
                for (int i = 0; i < weaponLoot.Length; i++)
                {
                    string dropName = "none";
                    switch (weaponLoot[i])
                    {
                        case 0:
                            dropName = "CacitianRevolver";
                            break;
                        case 1:
                            dropName = "CacitianClaws";
                            break;
                        case 2:
                            dropName = "CacitianSaber";
                            break;
                        case 3:
                            dropName = "ChunkStaff";
                            break;
                        case 4:
                            dropName = "CacitianScepter";
                            break;
                        case 5:
                            dropName = "CacitianBow";
                            break;
                        case 6:
                            dropName = "CacitianWand";
                            break;
                    }
                    if (dropName != "none")
                    {
                        Item.NewItem(npc.getRect(), mod.ItemType(dropName));
                    }
                }
                Item.NewItem(npc.getRect(), mod.ItemType("CacitianOre"), Main.rand.Next(63, 87));
            }
            else Item.NewItem(npc.getRect(), mod.ItemType("CoreBag"));

            if (!VariaWorld.downedCore)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (VariaPlayer.alignmentValue < 50) VariaPlayer.alignmentValue += 1;
                }
            }

            VariaWorld.downedCore = true;
        }
    }
}