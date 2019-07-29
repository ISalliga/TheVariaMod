using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class BanditBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bandit Bomb");
            Tooltip.SetDefault("Explodes into fiery shrapnel in all directions");
		}
		public override void SetDefaults()
		{
			item.damage = 43;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.thrown = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 47;
			item.useAnimation = 47;
			item.width = 30;
			item.height = 30;
			item.value = 350;
            item.shoot = mod.ProjectileType("BanditBomb");
            item.shootSpeed = 7f;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 999;
            item.consumable = true;
		}
	}
}
