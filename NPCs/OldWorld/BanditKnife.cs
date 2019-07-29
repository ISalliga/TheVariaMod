using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.OldWorld
{
    public class BanditKnife : ModProjectile
    {
        // Added these 2 constant to showcase how you could make AI code cleaner by doing this
        // Change this number if you want to alter how long the javelin can travel at a constant speed
        private const float maxTicks = 20f;

        // Change this number if you want to alter how the alpha changes
        private const int alphaReduction = 25;

        public override void SetDefaults()
        {
            projectile.alpha = 0;
            projectile.width = 14;
            projectile.height = 24;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bandit Knife");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void AI()
        {
            if (projectile.hostile)
            {
                BaseAI.AIThrownWeapon(projectile, ref projectile.ai, false, 20, 0.95f, 0.33f, 15f);
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                // If ai0 is 0f, run this code. This is the 'movement' code for the javelin as long as it isn't sticking to a target
                if (!isStickingToTarget)
                {
                    targetWhoAmI += 1f;
                    // For a little while, the javelin will travel with the same speed, but after this, the javelin drops velocity very quickly.
                    if (targetWhoAmI >= maxTicks)
                    {
                        // Change these multiplication factors to alter the javelin's movement change after reaching maxTicks
                        float velXmult = 0.95f; // x velocity factor, every AI update the x velocity will be 98% of the original speed
                        float velYmult = 0.33f; // y velocity factor, every AI update the y velocity will be be 0.35f bigger of the original speed, causing the javelin to drop to the ground
                        targetWhoAmI = maxTicks; // set ai1 to maxTicks continuously
                        projectile.velocity.X = projectile.velocity.X * velXmult;
                        projectile.velocity.Y = projectile.velocity.Y + velYmult;
                    }
                    // Make sure to set the rotation accordingly to the velocity, and add some to work around the sprite's rotation
                    projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
                }
                if (isStickingToTarget)
                {
                    // These 2 could probably be moved to the ModifyNPCHit hook, but in vanilla they are present in the AI
                    projectile.ignoreWater = true; // Make sure the projectile ignores water
                    projectile.tileCollide = false; // Make sure the projectile doesn't collide with tiles anymore
                    int aiFactor = 15; // Change this factor to change the 'lifetime' of this sticking javelin
                    bool killProj = false; // if true, kill projectile at the end
                    bool hitEffect = false; // if true, perform a hit effect
                    projectile.localAI[0] += 1f;
                    // Every 30 ticks, the javelin will perform a hit effect
                    hitEffect = projectile.localAI[0] % 30f == 0f;
                    int projTargetIndex = (int)targetWhoAmI;
                    if (projectile.localAI[0] >= (float)(60 * aiFactor)// If it's time for this javelin to die, kill it
                        || (projTargetIndex < 0 || projTargetIndex >= 200)) // If the index is past its limits, kill it
                    {
                        killProj = true;
                    }
                    else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage) // If the target is active and can take damage
                    {
                        // Set the projectile's position relative to the target's center
                        projectile.Center = Main.npc[projTargetIndex].Center - projectile.velocity * 2f;
                        projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
                        if (hitEffect) // Perform a hit effect here
                        {
                            Main.npc[projTargetIndex].HitEffect(0, 1.0);
                        }
                    }
                    else // Otherwise, kill the projectile
                    {
                        killProj = true;
                    }

                    if (killProj) // Kill the projectile
                    {
                        projectile.Kill();
                    }
                }
            }
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            // If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
            if (projectile.ai[0] == 1f) // or if(isStickingToTarget) since we made that helper method.
            {
                int npcIndex = (int)projectile.ai[1];
                if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                {
                    if (Main.npc[npcIndex].behindTiles)
                        drawCacheProjsBehindNPCsAndTiles.Add(index);
                    else
                        drawCacheProjsBehindNPCs.Add(index);
                    return;
                }
            }
        }
        public bool isStickingToTarget
        {
            get { return projectile.ai[0] == 1f; }
            set { projectile.ai[0] = value ? 1f : 0f; }
        }

        // WhoAmI of the current target
        public float targetWhoAmI
        {
            get { return projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // If you'd use the example above, you'd do: isStickingToTarget = 1f;
            // and: targetWhoAmI = (float)target.whoAmI;
            isStickingToTarget = true; // we are sticking to a target
            targetWhoAmI = (float)target.whoAmI; // Set the target whoAmI
            projectile.velocity =
                (target.Center - projectile.Center) *
                0.75f; // Change velocity based on delta center of targets (difference between entity centers)
            projectile.netUpdate = true; // netUpdate this javelin
            target.AddBuff(mod.BuffType("BanditKnifeDOT"), 900); // Adds the ExampleJavelin debuff for a very small DoT

            projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore

            // The following code handles the javelin sticking to the enemy hit.
            int maxStickingJavelins = 3; // This is the max. amount of javelins being able to attach
            Point[] stickingJavelins = new Point[maxStickingJavelins]; // The point array holding for sticking javelins
            int javelinIndex = 0; // The javelin index
            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != projectile.whoAmI // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == Main.myPlayer // Make sure the projectile's owner is the client's player
                    && currentProjectile.type == projectile.type // Make sure the projectile is of the same type as this javelin
                    && currentProjectile.ai[0] == 1f // Make sure ai0 state is set to 1f (set earlier in ModifyHitNPC)
                    && currentProjectile.ai[1] == (float)target.whoAmI
                ) // Make sure ai1 is set to the target whoAmI (set earlier in ModifyHitNPC)
                {
                    stickingJavelins[javelinIndex++] =
                        new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                    if (javelinIndex >= stickingJavelins.Length
                    ) // If the javelin's index is bigger than or equal to the point array's length, break
                    {
                        break;
                    }
                }
            }
            // Here we loop the other javelins if new javelin needs to take an older javelin's place.
            if (javelinIndex >= stickingJavelins.Length)
            {
                int oldJavelinIndex = 0;
                // Loop our point array
                for (int i = 1; i < stickingJavelins.Length; i++)
                {
                    // Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
                    if (stickingJavelins[i].Y < stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i; // Remember the index of the removed javelin
                    }
                }
                // Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
                Main.projectile[stickingJavelins[oldJavelinIndex].X].Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.position, 1);
            for (int numDust = 0; numDust < 5; numDust++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 30, 30, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.052632f)];
            }
            if (projectile.friendly && Main.rand.Next(3) == 0)
            {
                Item.NewItem(projectile.position, mod.ItemType("BanditKnife"));
            }
        }
    }
}