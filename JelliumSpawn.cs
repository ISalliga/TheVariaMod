using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Varia
{
    public class JelliumSpawn : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.Golem)
            {
                if (!VariaWorld.jelliumSpawned)
                {                                                          
                    Main.NewText("Jellium has generated in your world!", 255, 0, 223);
				    VariaWorld.jelliumSpawned = true;
					for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 18E-05); k++)
					{
						int i = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
						int j = WorldGen.genRand.Next((int) Main.worldSurface - 1, Main.maxTilesY - 10);
						Tile tile = Main.tile[i, j];
						if ((tile.type == 0 || tile.type == 1) && j > Main.worldSurface)
						{
							WorldGen.OreRunner(i, j, (double)WorldGen.genRand.Next(6, 7), WorldGen.genRand.Next(6, 7), (ushort)mod.TileType("JelliumCrystal")); 
						}
					}
                }
            }
        }
    }
}