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
    public class ForgottenGrass : ModTile
    {
        int varianGrassDrawOffset = 0;
        int varianGrassDrawOffDir = 1;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.Grass[Type] = true;
            AddMapEntry(new Color(96, 75, 129));
            Main.tileBrick[Type] = true;
            drop = ItemID.DirtBlock;
            TileID.Sets.ChecksForMerge[Type] = true;
            minPick = 9999;
            soundType = 6;
            soundStyle = 6;
        }

        public override bool CanExplode(int i, int j)
        {
            return true;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = TileID.Dirt;
                WorldGen.SquareTileFrame(i, j, true);
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            WorldGen.SpreadGrass(i, j, TileID.Dirt, mod.TileType("ForgottenGrass"), true, Main.tile[i, j].color());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.142f * 4f;
            g = 0.117f * 4f;
            b = 0.181f * 4f;
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Texture2D texture = mod.GetTexture("Tiles/ForgottenGrass_Shine");
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 pos = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + new Vector2(8, 8);
            for (int b = 0; b < 3; b++)
            {
                int length = 10;
                float rotation = -(float)Math.PI / 2 + ((float)Math.Sin(SharedShine.shineCounter + b * (2 * (float)Math.PI) / 3 + ((i % 7) * 2 * (float)Math.PI) / 7) * (float)Math.PI / 4);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                for (int d = 0; d < length; d++)
                {
                    spriteBatch.Draw(texture,
                        pos + new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * d * 2, //position
                        new Rectangle(0, 0, 2, 2), //source Rectangle
                        Color.Lerp(new Color(255, 255, 255, 255), new Color(0, 0, 0, 0), (float)d / (float)length), //transparency
                        rotation, //rotation
                        new Vector2(1, 1), //origin
                        1f, //scale
                        SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen) zero = Vector2.Zero;
            if (varianGrassDrawOffDir == 1)
            {
                if (varianGrassDrawOffset >= 9) varianGrassDrawOffDir = -1;
                varianGrassDrawOffset++;
            }
            if (varianGrassDrawOffDir == -1)
            {
                if (varianGrassDrawOffset <= 4) varianGrassDrawOffDir = 1;
                varianGrassDrawOffset--;
            }
            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/ForgottenGrass_GM"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
    public class SharedShine : ModWorld
    {
        public static float shineAngle;
        public static float shineCounter;
        public static float shineLength;
        public override void PreUpdate()
        {
            shineCounter += (float)Math.PI / 240;
            shineAngle = -(float)Math.PI / 2 + ((float)Math.Sin(shineCounter) * (float)Math.PI / 4);
            shineLength = Math.Abs((float)Math.Sin(shineCounter * 1.5f));
        }
    }
}