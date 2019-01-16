using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace Varia.Tiles
{
	public class ForgottenCloudRegen : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileMerge[mod.TileType("ForgottenCloud")][Type] = true;
            minPick = 9999;
            AddMapEntry(new Color(221, 147, 30));
        }
        public override void RandomUpdate(int i, int j)
        {
            Main.tile[i, j].type = (ushort)mod.TileType("Glistenyn");
            for (int x = 0; x < 25; x++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = new Vector2(i * 16, j * 16);
                dust = Main.dust[Terraria.Dust.NewDust(position, 16, 16, 185, 0f, 0f, 0, new Color(255, 255, 255), 2.236842f)];
                dust.noGravity = true;
                dust.noLight = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(80, Main.LocalPlayer);
            }
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
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/ForgottenCloud_GM"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}