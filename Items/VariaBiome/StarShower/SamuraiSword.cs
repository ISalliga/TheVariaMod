using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.VariaBiome.StarShower 
{
    public class SamuraiSword : ModItem
    {
        public int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Samurai Sword");
            Tooltip.SetDefault("Right click to teleport onto the cursor");
		}
        public override void SetDefaults()
        {
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.useTurn = true;
			item.UseSound = SoundID.Item1;
			item.crit = 4;
            item.damage = 14;
            item.melee = true;
            item.width = 60;
            item.height = 32;
            item.useTime = 24;
			item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 6;
            item.rare = 0;
            item.autoReuse = true;
            item.shootSpeed = 8f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 1;
                item.useTime = 120;
                item.useAnimation = 16;
                item.damage = 14;
            }
            else
            {
                item.useStyle = 1;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 14;
            }
            return base.CanUseItem(player);
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (player.altFunctionUse == 2 & timer == 0)
            {
                timer = 120;
                Vector2 mousePos;
                mousePos.X = (float)Main.mouseX + Main.screenPosition.X;
                if (player.gravDir == 1f)
                    mousePos.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                else
                    mousePos.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                player.Teleport(mousePos, 4);
                NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, mousePos.X, mousePos.Y, 1, 0, 0);
            }
            if (timer > 0)
                timer--;
        }
    }
}