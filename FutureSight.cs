using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Varia
{
	public class FutureSight : ModDust
	{
        int timer = 0;

        public override void OnSpawn(Dust dust)
		{
			dust.velocity.Y = 0;
			dust.velocity.X *= 0;
            dust.alpha = 0;
			dust.scale = 0.7f;
            dust.frame = new Rectangle(0, 0, 10, 10);
        }

		public override bool MidUpdate(Dust dust)
		{
            timer++;
            if (timer > 1) dust.active = false;
            return true;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
		}
	}
}