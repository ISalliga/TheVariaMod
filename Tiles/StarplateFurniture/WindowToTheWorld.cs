using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Varia.Tiles.StarplateFurniture
{
    public class WindowToTheWorld : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Window to the World");
            minPick = 9999999;
            AddMapEntry(new Color(255, 255, 255), name);
            disableSmartCursor = true;
        }
        public override void RightClick(int i, int j)
        {
            if (NPC.downedPlantBoss)
            {
                Main.mapFullscreen = true;
                Main.LocalPlayer.GetModPlayer<VariaPlayer>().nearWindow = true;
                Main.NewText("You may teleport anywhere you like. Press T to teleport.", new Color(200, 210, 255));
            }
            else
            {
                Main.NewText("You are not worthy. Come back when you are post-Plantera.", new Color(200, 200, 200));
            }
        }
    }
}