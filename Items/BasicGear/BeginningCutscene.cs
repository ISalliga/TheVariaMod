using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.BasicGear
{
	public class BeginningCutscene : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("");
			Tooltip.SetDefault("You shouldn't be able to see this tooltip.");
		}

        int timer = 0;

		public override void SetDefaults()
        {
            item.damage = 5;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 20;
			item.pick = 35;
			item.useStyle = 1;
            item.knockBack = 0.02f;
            item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

        public override void UpdateInventory(Player player)
        {
            timer++;

            Main.playerInventory = false;

            if (timer == 5)
            {
                Main.NewText("Where... am I?");
            }

            if (timer == 305)
            {
                Main.NewText("What happened? Why am I not at home?!");
            }

            if (timer == 605)
            {
                Main.NewText("I need answers.");
            }

            player.controlLeft = false;
            player.controlRight = false;
            player.controlJump = false;
            player.controlMount = false;
            player.controlInv = false;
            player.velocity.X *= 0.8f;
            
            if (timer >= 665)
            {
                timer = 0;
                item.stack = 0;
            }
        }
    }
}