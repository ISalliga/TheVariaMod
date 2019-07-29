using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
	public class BanditKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bandit Knife");
            Tooltip.SetDefault("Sticks to enemies and deals damage over time");
		}
		public override void SetDefaults()
		{
			item.damage = 14;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.thrown = true;
			item.useStyle = 1;
			item.knockBack = 3;
			item.useTime = 20;
			item.useAnimation = 20;
			item.width = 30;
			item.height = 30;
			item.value = 350;
            item.shoot = mod.ProjectileType("BanditKnife");
            item.shootSpeed = 16;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.maxStack = 999;
            item.consumable = true;
		}
	}
}
