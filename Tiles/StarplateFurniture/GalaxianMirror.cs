using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using BaseMod;
using Terraria.ModLoader;

namespace Varia.Tiles.StarplateFurniture
{
	public class GalaxianMirror : ModWall
	{
		public override void SetDefaults()
        {
            drop = mod.ItemType("GalaxianMirror");
            AddMapEntry(new Color(43, 43, 70));
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/StarplateFurniture/GalaxianMirror_Shine"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(0 * Main.rand.Next(0, 3), 0 * Main.rand.Next(0, 2), 16, 16), GalaxianMirrorShineEffect.shine1, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/StarplateFurniture/GalaxianMirror_Shine2"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(0 * Main.rand.Next(0, 3), 0 * Main.rand.Next(0, 2), 16, 16), GalaxianMirrorShineEffect.shine2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        
    }

    public class GalaxianMirrorShineEffect : ModWorld
    {
        public static float fade1 = 0;
        public static float fade2 = 0;
        public static Color shine1;
        public static Color shine2;
        public override void PreUpdate()
        {
            fade1 = (fade1 + 0.014f) % 2f;
            shine1 = Color.Lerp(new Color(55, 55, 55, 200), new Color(200, 200, 200, 55), Math.Abs(fade1 - 1f));
            fade2 = (fade2 + 0.02f) % 2f;
            shine2 = Color.Lerp(new Color(200, 200, 200, 55), new Color(55, 55, 55, 200), Math.Abs(fade2 - 1f));
        }
    }
}