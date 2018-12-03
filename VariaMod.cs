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
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameInput;

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
            if (!Main.dedServ)
            {
                Filters.Scene["Varia:Cavity"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(.50f, 0.20f, 0f).UseOpacity(0.3f), EffectPriority.VeryHigh);
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
                bossList.Call("AddBossWithInfo", "Fallen Angel", 7.5f, (Func<bool>)(() => VariaWorld.downedAngel), string.Format("Destroy three Hunks of Unidentified Technology, found in the sky after killing any Mechanical Boss"));

                bossList.Call("AddBossWithInfo", "Nice Guy", 10.2f, (Func<bool>)(() => VariaWorld.downedOptime), string.Format("Use a [i:{0}]", ItemType("NiceMask")));

                bossList.Call("AddBossWithInfo", "The Anomaly", 11.2f, (Func<bool>)(() => VariaWorld.downedAnomaly), string.Format("-Us|+e \a N-&UL*)L"));

                bossList.Call("AddBossWithInfo", "Spider Queen", 2.5f, (Func<bool>)(() => VariaWorld.downedSpoderQueen), string.Format("Use a [i:{0}]", ItemType("BugAttractant")));

                bossList.Call("AddBossWithInfo", "Soul of the Guide", 0.5f, (Func<bool>)(() => VariaWorld.downedSotG), string.Format("Use a [i:{0}]", ItemType("SoulActivator")));

                //bossList.Call("AddBossWithInfo", "Rainmaker", 8.2f, (Func<bool>)(() => VariaWorld.downedRainmaker), string.Format("Use a [i:{0}]", ItemType("CallersCloud")));
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

                if (Main.LocalPlayer.GetModPlayer<VariaPlayer>().zoneBreeze)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingBreeze");
                    priority = MusicPriority.Environment;
                }
            }
        }
    }
}
