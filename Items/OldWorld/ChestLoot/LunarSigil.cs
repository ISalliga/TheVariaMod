using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld.ChestLoot

{
    public class LunarSigil : ModProjectile
    {
        NPC target;

        float rot1 = 0;
        float rot2 = 0;

        float scale = 0;

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(122, 248, 178);
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.width = 30; //Set the hitbox width
            projectile.height = 30;   //Set the hitbox heinght
            projectile.hostile = false;    //tells the game if is hostile or not.
            projectile.friendly = false;   //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.ignoreWater = true;    //Tells the game whether or not projectile will be affected by water
            Main.projFrames[projectile.type] = 5;  //this is where you add how many frames u'r projectile has to make the animation
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed  -1 is infinity
            projectile.tileCollide = false; //Tells the game whether or not it can collide with tiles/ terrain
            projectile.sentry = true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, projectile.Center, 62);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D sigil = mod.GetTexture("Items/OldWorld/ChestLoot/LunarSigil2");
            Vector2 drawOrigin = new Vector2(sigil.Width * 0.5f, sigil.Height * 0.5f);
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(2f, 1f);
            spriteBatch.Draw(sigil, drawPos, new Rectangle(0, 0, sigil.Width, sigil.Height), new Color(122, 248, 178), rot1, drawOrigin, 1.4f * scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(sigil, drawPos, new Rectangle(0, 0, sigil.Width, sigil.Height), new Color(122, 248, 178), rot2, drawOrigin, 0.8f * scale, SpriteEffects.None, 0f);
        }

        public override void AI()
        {
            if (scale < 1f) scale += 0.1f;

            rot1 += 0.1f;
            rot2 -= 0.05f;

            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }

            bool isTargetingNPC = false;

            if (Methods.ClosestNPC(ref target, 900, projectile.Center))
            {
                isTargetingNPC = true;
            }

            if (isTargetingNPC)
            {
                projectile.ai[0]++;
                if (projectile.ai[0] >= 45)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(target.Center) * 14, ProjectileID.LaserMachinegunLaser, 16, 2f, projectile.owner);
                    projectile.ai[0] = 0;
                }
            }
        }
    }
}