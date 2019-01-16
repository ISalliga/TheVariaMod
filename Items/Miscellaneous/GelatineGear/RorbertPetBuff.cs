using Terraria;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
	public class RorbertPetBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rorbert");
            Description.SetDefault("RIP Rorbert,  he will always live on in our hearts... and pet item slots.");
            Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player,  ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<VariaPlayer>(mod).rorPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("RorbertPet")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2),  player.position.Y + (float)(player.height / 2),  0f,  0f,  mod.ProjectileType("RorbertPet"),  0,  0f,  player.whoAmI,  0f,  0f);
			}
		}
	}
}