using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class AmianthusProj : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.aiStyle = 3;
            projectile.penetrate = 100;
            projectile.timeLeft = 360;
            projectile.thrown = true;
        }
		public override void AI()
		{
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Crystals))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Copper))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Tin))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Iron))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Lead))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Silver))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Tungsten))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Gold))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Platinum))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Demonite))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Crimtane))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Hellstone))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Cobalt))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Palladium))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Mythril))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
			if (Main.tile[(int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16)].type == (TileID.Orichalcum))
			{
				Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 70), projectile.Center);
				WorldGen.KillTile((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
			}
		}
    }
}