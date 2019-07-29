using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class AssassinDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Assassin's Dagger");
			Tooltip.SetDefault("Stabbing enemies inflicts poison");
		}
		public override void SetDefaults()
		{
			item.damage = 14;
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
			item.maxStack = 1;
			item.autoReuse = true;
			item.useTurn = true;
		}
        public override void OnHitNPC(Player player,  NPC target,  int damage,  float knockBack,  bool crit)
        {
            target.AddBuff(BuffID.Poisoned,  180);
        }
    }
}
