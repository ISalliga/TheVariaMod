using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel.TechHunk
{
	public class TrydaniumPhaserapier : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Trydanium Phaserapier");
			Tooltip.SetDefault("'This is too long to be called a shortsword - let's just call it a rapier.'");
		}
		public override void SetDefaults()
		{
			item.damage = 78;
			item.melee = true;
			item.useStyle = 3;
			item.knockBack = 3;
			item.useTime = 17;
			item.useAnimation = 17;
			item.width = 70;
			item.height = 70;
			item.value = 8000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("LanceSpark");
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player,  ref Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            int spark = Projectile.NewProjectile(player.Center.X + player.direction * 36,  player.Center.Y + 24,  0,  0,  mod.ProjectileType("LanceSpark"),  78,  0f,  Main.myPlayer,  0f,  0f);
            Main.projectile[spark].ai[0] = player.direction;
            return false;
        }
    }
}
