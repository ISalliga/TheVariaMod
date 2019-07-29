using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace Varia.Tiles
{
	public class WornBrick : ModTile
	{
		public override void SetDefaults()
		{
            soundType = 21;
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			drop = mod.ItemType("WornBrick");
			AddMapEntry(new Color(114, 81, 56));
            Main.tileBlockLight[Type] = true;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = new Vector2(i * 16, j * 16);
            dust = Main.dust[Terraria.Dust.NewDust(position, 16, 16, 1, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.shader = GameShaders.Armor.GetSecondaryShader(10, Main.LocalPlayer);
            return true;
        }
    }
}