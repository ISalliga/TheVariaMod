using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.NPCs.QueensInfantry
{
    public class VenomousPuddle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 30;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.penetrate = 7;
            projectile.timeLeft = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venomous Puddle");
        }
        public override void AI()
        {
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 28), 47, 0, 1, 0f, -2.105263f, 0, new Color(184, 0, 255), 1f)];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(73, Main.LocalPlayer);
            }

            projectile.alpha += 1;

            if (projectile.alpha > 255)
            {
                projectile.active = false;
            }
        }
    }
}