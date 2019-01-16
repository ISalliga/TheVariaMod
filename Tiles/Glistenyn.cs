using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Varia.Tiles
{
	public class Glistenyn : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            soundType = 21;
            minPick = 185;
            drop = mod.ItemType("GlistenynOre");
            AddMapEntry(new Color(142, 117, 181));
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Item.NewItem(new Vector2(i * 16 + 8, j * 16 + 8), mod.ItemType("GlistenynOre"));
                Main.tile[i, j].type = (ushort)mod.TileType("ForgottenCloudRegen");
                WorldGen.SquareTileFrame(i, j, true);
            }
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.142f * 2f;
            g = 0.117f * 2f;
            b = 0.181f * 2f;
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
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/Glistenyn_GM"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}