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

namespace Varia.Items.Miscellaneous
{
    public class IronSoleStomp : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 30;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.penetrate = 7;
            projectile.timeLeft = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Soles");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[(int)projectile.ai[0]].velocity.Y = -8f;
            Main.player[(int)projectile.ai[0]].noFallDmg = true;
            Main.player[(int)projectile.ai[0]].GetModPlayer<VariaPlayer>().IronSolesCooldown = 0;
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 56), projectile.Center);
            Main.player[(int)projectile.ai[0]].fallStart = (int)Main.player[(int)projectile.ai[0]].position.Y;
        }
    }
}