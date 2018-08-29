using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Varia;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia
{
	class VariaMod : Mod
	{
		public VariaMod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

        public override void Load()
        {
            Main.tileCut[231] = false;
            if (!Main.dedServ)
            {
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Optime"), ItemType("OptimeMusicBox"), TileType("OptimeMusicBox"));
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/NiceGuy"), ItemType("NGMusicBox"), TileType("NGMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/CavityMusic"), ItemType("CavityMusicBox"), TileType("CavityMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/FallenAngel"), ItemType("FAMusicBox"), TileType("FAMusicBox"));
            }
        }
        public override void Unload()
        {
            Main.tileCut[231] = true;
        }

        public override void PostSetupContent()
		{
			Mod bossList = ModLoader.GetMod("BossChecklist");
			if (bossList != null)
			{
                bossList.Call("AddBossWithInfo", "Fallen Angel", 7.5f, (Func<bool>)(() => VariaWorld.downedAngel), string.Format("Destroy three Hunks of Unidentified Technology, found after killing any Mechanical Boss"));

                bossList.Call("AddBossWithInfo", "Nice Guy", 10.2f, (Func<bool>)(() => VariaWorld.downedAngel), string.Format("Use a [i:{0}]", ItemType("NiceMask")));
            }
		}

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (Main.LocalPlayer.GetModPlayer<VariaPlayer>().zoneCavity)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/CavityMusic");
                    priority = MusicPriority.BiomeHigh;
                }
            }
        }
    }
}
