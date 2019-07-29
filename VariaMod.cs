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
using Terraria.UI;
using Terraria.GameContent.UI;
using System.Linq;
using Varia.UI;

namespace Varia
{
	class VariaMod : Mod
	{
        public static ModHotKey TwinSight;
        private InfectionBar BarUIStateInfection;
        internal UserInterface BarInterfaceInfection;
        internal static VariaMod instance;
        public VariaMod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }

        public override void Load()
        {
            TwinSight = RegisterHotKey("Twin Sight", "Z");
            Main.tileCut[231] = false;
            if (!Main.dedServ)
            {
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Optime"), ItemType("OptimeMusicBox"), TileType("OptimeMusicBox"));
                //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/NiceGuy"), ItemType("NGMusicBox"), TileType("NGMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/CavityMusic"), ItemType("CavityMusicBox"), TileType("CavityMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/FallenAngel"), ItemType("FAMusicBox"), TileType("FAMusicBox"));

                BarUIStateInfection = new InfectionBar();
                BarUIStateInfection.Activate();
                BarInterfaceInfection = new UserInterface();
                BarInterfaceInfection.SetState(BarUIStateInfection);
            }
            //Filters.Scene["StarShower"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(1.86f, 1.78f, 2.26f).UseOpacity(1f), EffectPriority.VeryHigh);
            Filters.Scene["Host"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(2.56f, 2f, 0.54f).UseOpacity(0.5f), EffectPriority.VeryHigh);

            instance = this;

            if (!Main.dedServ)
            {
                PremultiplyTexture(GetTexture("NPCs/FallenAngel/Forcefield"));
            }
        }

        public override void Unload()
        {
            Main.tileCut[231] = true;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (BarInterfaceInfection != null)
                BarInterfaceInfection.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "Infection UI",
                    delegate
                    {
                        if (BarInterfaceInfection != null)
                            BarUIStateInfection.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void PostSetupContent()
		{
			Mod bossList = ModLoader.GetMod("BossChecklist");
			if (bossList != null)
			{
                bossList.Call("AddBossWithInfo", "Fallen Angel", 7.5f, (Func<bool>)(() => VariaWorld.downedAngel), string.Format("Destroy three Hunks of Unidentified Technology, found in the Everlasting Breeze after killing any Mechanical Boss, or use a [i:{0}]", ItemType("UnholyBeacon")));

                bossList.Call("AddBossWithInfo", "Nice Guy", 10.2f, (Func<bool>)(() => VariaWorld.downedOptime), string.Format("Use a [i:" + ItemType<Items.Optime.NiceMask>() + "] and recieve the help you need..."));

                bossList.Call("AddBossWithInfo", "The Anomaly", 11.2f, (Func<bool>)(() => VariaWorld.downedAnomaly), string.Format("-Us|+e \a N-&UL*)L (", ItemType("Null"), ")"));

                bossList.Call("AddBossWithInfo", "Spider Queen", 2.5f, (Func<bool>)(() => VariaWorld.downedSpoderQueen), string.Format("At night, and preferably above ground, use a [i:{0}]", ItemType("BugAttractant")));

                bossList.Call("AddBossWithInfo", "Soul of the Guide", 0.5f, (Func<bool>)(() => VariaWorld.downedSotG), string.Format("Use a [i:{0}]", ItemType("SoulActivator")));

                bossList.Call("AddBossWithInfo", "Core of Mutation", 5.4f, (Func<bool>)(() => VariaWorld.downedCore), string.Format("Use a [i:" + ItemType<Items.Cavity.Cacitian.InfectionRadiator>() + "] underground"));
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

                if (Main.LocalPlayer.GetModPlayer<VariaPlayer>().ZoneOldWorld)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/OldWorld");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.GetModPlayer<VariaPlayer>().zoneBreeze)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/EverlastingBreeze");
                    priority = MusicPriority.Environment;
                }

                if (VariaWorld.starShower)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/StarShower");
                    priority = MusicPriority.Event;
                }

                if (NPC.AnyNPCs(NPCType("Host")))
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/YOU");
                    priority = MusicPriority.BossHigh;
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipeBullet = new ModRecipe(this);
            recipeBullet.AddIngredient(ItemID.IronBar, 1);
            recipeBullet.AddTile(TileID.Anvils);
            recipeBullet.SetResult(ItemID.MusketBall, 30);
            recipeBullet.AddRecipe();

            ModRecipe recipeNHelm = new ModRecipe(this);
            recipeNHelm.AddIngredient(ItemID.Silk, 10);
            recipeNHelm.AddIngredient(ItemID.IronBar, 8);
            recipeNHelm.AddIngredient(ItemID.Gel, 12);
            recipeNHelm.AddTile(TileID.Solidifier);
            recipeNHelm.SetResult(ItemID.NinjaHood, 1);
            recipeNHelm.AddRecipe();

            ModRecipe recipeNChest = new ModRecipe(this);
            recipeNChest.AddIngredient(ItemID.Silk, 13);
            recipeNChest.AddIngredient(ItemID.IronBar, 10);
            recipeNChest.AddIngredient(ItemID.Gel, 15);
            recipeNChest.AddTile(TileID.Solidifier);
            recipeNChest.SetResult(ItemID.NinjaShirt, 1);
            recipeNChest.AddRecipe();

            ModRecipe recipeNLegs = new ModRecipe(this);
            recipeNLegs.AddIngredient(ItemID.Silk, 9);
            recipeNLegs.AddIngredient(ItemID.IronBar, 8);
            recipeNLegs.AddIngredient(ItemID.Gel, 10);
            recipeNLegs.AddTile(TileID.Solidifier);
            recipeNLegs.SetResult(ItemID.NinjaPants, 1);
            recipeNLegs.AddRecipe();
        }
    }
}
