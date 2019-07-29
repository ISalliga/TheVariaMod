using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class MemorialSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Memorial Sword");
			Tooltip.SetDefault("'Savor your mistakes...' \nHas a chance to create a shockwave on enemy hits");
		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.melee = true;          
			item.width = 40;           
			item.height = 40;           //Weapon's texture's height
			item.useTime = 19;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 19;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 1;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			item.rare = 0;              //The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            bool randBool = Main.rand.NextBool(2);
            Vector2 center = player.Center;
            if (randBool) Projectile.NewProjectile(center, player.DirectionTo(center + new Vector2(player.direction, 0)) * 5, mod.ProjectileType("MemorialShockwave"), 16, 0f, player.whoAmI, Main.rand.Next(-2, 3), Main.rand.Next(-2, 3));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ShardOfThePast", 10);
            recipe.AddIngredient(null, "OldWorldAlloy", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
