using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Varia.Items.FallenAngel
{
	public class GuardiansValor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Valor");
			Tooltip.SetDefault("Striking enemies with the blade will cause an explosion of light");
		}
		public override void SetDefaults()
		{
			item.damage = 51;
			item.melee = true;
			item.width = 68;
            item.scale = 1.3f;
			item.height = 68;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 1;
			item.knockBack = 2;
			item.shootSpeed = 10;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectile(new Vector2(target.Center.X - 25, target.Center.Y - 25), Vector2.Zero, mod.ProjectileType("Angelsplosion"), 51, 0f, player.whoAmI);
        }
    }
}
