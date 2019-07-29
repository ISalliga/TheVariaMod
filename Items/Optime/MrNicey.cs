using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace Varia.Items.Optime
{
	public class MrNicey : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mr. Nicey");
			Tooltip.SetDefault("Conjures nightmarish hands from portals");
		}
		public override void SetDefaults()
		{
			item.damage = 54;
			item.summon = true;
            item.mana = 14;
            item.noMelee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 8;
			item.useAnimation = 32;
			item.useStyle = 5;
			item.knockBack = 2;
			item.shoot = mod.ProjectileType("NiceyHand");
			item.shootSpeed = 19;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X += Main.rand.Next(-15, 16);
            position.Y += Main.rand.Next(-15, 16);
            speedX += Main.rand.Next(-30, 40) * 0.1f;
            speedY += Main.rand.Next(-30, 40) * 0.1f;

            if (Main.rand.NextBool(1, 40))
            {
                Main.PlaySound(mod.GetSoundSlot(SoundType.Custom, "Sounds/Miscellaneous/Giggle"), position);
            }

            return true;
        }
    }
}
