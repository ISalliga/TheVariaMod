using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.JelliumGear
{
    public class StickyGreatbow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 62;
            item.ranged = true;
            item.width = 40;
            item.height = 68;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2;
            item.value = Item.sellPrice(0,  2,  0,  0);
            item.rare = 5;
            item.UseSound = SoundID.Item102;
            item.autoReuse = true;
            item.shoot = 3; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 20;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Greatbow");
			Tooltip.SetDefault("Turns arrows into Jelly Bean Arrows, which split into smaller shards that bounce \nFires three at once");
        }
        public override bool Shoot(Player player,  ref Microsoft.Xna.Framework.Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-2, 3), speedY + Main.rand.Next(-2, 3), mod.ProjectileType("JellyBeanArrow"), damage, knockBack, player.whoAmI, 0.0f, 0.5f + (float)Main.rand.NextDouble() * 0.9f);
            }
            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,  "JelliumCrystal",  10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
