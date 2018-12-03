using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.FallenAngel
{
    public class GazeOfTheHolyCity : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 58;
            item.ranged = true;
            item.width = 16;
            item.height = 12;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 3; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaze of the Holy City");
			Tooltip.SetDefault("Turns arrows into Jester's Arrows which do not pierce \nGet ready for fireworks!");
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int arrow = Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, ProjectileID.JestersArrow, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            Main.projectile[arrow].penetrate = 1;
            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DarklightEssence", 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
