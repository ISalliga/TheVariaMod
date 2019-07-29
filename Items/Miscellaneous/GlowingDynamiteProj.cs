using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace Varia.Items.Miscellaneous
{
	public class GlowingDynamiteProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Dynamite");
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 32;
			projectile.timeLeft = 300;
			projectile.alpha = 0;
			projectile.aiStyle = 14;
			projectile.hostile = true;
            projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 2;
		}
		public override void AI()
       	{
            Player player = Main.player[projectile.owner];
            if (projectile.timeLeft == 10 || projectile.penetrate == 1)
            {
				projectile.position += new Vector2(-50, -50);
				projectile.width = 250;
				projectile.height = 250;
				projectile.timeLeft = 9;
				projectile.alpha = 255;
				projectile.penetrate = -1;
				projectile.tileCollide = false;
				projectile.friendly = false;
				projectile.hostile = true;
            }
			if (projectile.timeLeft == 9)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);			
			}
			if (projectile.timeLeft < 10)
				projectile.velocity = new Vector2(0, 0);

			if (projectile.timeLeft == 3)
			{
				projectile.width = 12;
				projectile.height = 32;
				// I had done no prior testing with the dynamite. If the explosion is skewed, change this until it isn't.
				projectile.position += new Vector2(60, 60);
			}
			// Adapted a tiny bit from ExampleMod's Example Explosive.
			if (projectile.timeLeft == 1)
			{
				int explosionRadius = 10;
				int minTileX = (int)(projectile.position.X / 16f - (float)explosionRadius);
				int maxTileX = (int)(projectile.position.X / 16f + (float)explosionRadius);
				int minTileY = (int)(projectile.position.Y / 16f - (float)explosionRadius);
				int maxTileY = (int)(projectile.position.Y / 16f + (float)explosionRadius);
				if (minTileX < 0) 
				{
					minTileX = 0;
				}
				if (maxTileX > Main.maxTilesX) 
				{
					maxTileX = Main.maxTilesX;
				}
				if (minTileY < 0) 
				{
					minTileY = 0;
				}
				if (maxTileY > Main.maxTilesY) 
				{
					maxTileY = Main.maxTilesY;
				}
				bool canKillWalls = false;
				for (int x = minTileX; x <= maxTileX; x++) 
				{
					for (int y = minTileY; y <= maxTileY; y++) 
					{
						float diffX = Math.Abs((float)x - projectile.position.X / 16f);
						float diffY = Math.Abs((float)y - projectile.position.Y / 16f);
						double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
						if (distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0) 
						{
							canKillWalls = true;
							break;
						}
					}
				}
				AchievementsHelper.CurrentlyMining = true;
				for (int i = minTileX; i <= maxTileX; i++) 
				{
					for (int j = minTileY; j <= maxTileY; j++) 
					{
						float diffX = Math.Abs((float)i - projectile.position.X / 16f);
						float diffY = Math.Abs((float)j - projectile.position.Y / 16f);
						double distanceToTile = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
						if (distanceToTile < (double)explosionRadius) 
						{
							bool canKillTile = true;
							if (Main.tile[i, j] != null && Main.tile[i, j].active()) 
							{
								canKillTile = true;
								if (Main.tileDungeon[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 88 || Main.tile[i, j].type == 21 || Main.tile[i, j].type == 26 || Main.tile[i, j].type == 107 || Main.tile[i, j].type == 108 || Main.tile[i, j].type == 111 || Main.tile[i, j].type == 226 || Main.tile[i, j].type == 237 || Main.tile[i, j].type == 221 || Main.tile[i, j].type == 222 || Main.tile[i, j].type == 223 || Main.tile[i, j].type == 211 || Main.tile[i, j].type == 404) 
								{
									canKillTile = false;
								}
								if (!Main.hardMode && Main.tile[i, j].type == 58) 
								{
									canKillTile = false;
								}
								if (!TileLoader.CanExplode(i, j)) 
								{
									canKillTile = false;
								}
								if (canKillTile) 
								{
									WorldGen.KillTile(i, j, false, false, false);
									if (!Main.tile[i, j].active() && Main.netMode != 0) 
									{
										NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
									}
								}
							}
							if (canKillTile) 
							{
								for (int x = i - 1; x <= i + 1; x++) 
								{
									for (int y = j - 1; y <= j + 1; y++) 
									{
										if (Main.tile[x, y] != null && Main.tile[x, y].wall > 0 && canKillWalls && WallLoader.CanExplode(x, y, Main.tile[x, y].wall)) 
										{
											WorldGen.KillWall(x, y, false);
											if (Main.tile[x, y].wall == 0 && Main.netMode != 0) 
											{
												NetMessage.SendData(17, -1, -1, null, 2, (float)x, (float)y, 0f, 0, 0, 0);
											}
										}
									}
								}
							}
						}
					}
				}
				AchievementsHelper.CurrentlyMining = false;
			}
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 63, 0f, 0f, 50, default(Color), 1f);
			    Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 3f;
			}
            Lighting.AddLight(projectile.Center, 0.4f, 0.4f, 0.6f);
        }
    }
}