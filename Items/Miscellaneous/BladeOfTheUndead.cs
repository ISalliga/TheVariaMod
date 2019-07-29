using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous
{
	public class BladeOfTheUndead : ModItem 
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Launches homing bones on enemy strikes."); 
		}
		
		public override void SetDefaults() 
		{
			item.damage = 38; 
			item.melee = true; 
			item.width = 52; 
			item.height = 54; 
			item.useTime = 16; 
			item.useAnimation = 16; 
			item.useStyle = 1; 
			item.knockBack = 6; 
			item.value = 1500; 
			item.rare = 2; 
			item.UseSound = SoundID.Item1;
			item.autoReuse = false; 
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            Vector2 Aim = player.Center - target.Center;
            Aim.Normalize();
            Projectile.NewProjectile(player.position.X + Main.rand.Next(-100, 100), player.position.Y + Main.rand.Next(-100, 100), Aim.X * 10, Aim.Y * 10, mod.ProjectileType("Skelebone"), damage, 0, player.whoAmI);
        }
    }
}