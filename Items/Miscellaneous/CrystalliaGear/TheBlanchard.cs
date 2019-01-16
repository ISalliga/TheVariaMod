using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.CrystalliaGear
{
    public class TheBlanchard : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 49;
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
            DisplayName.SetDefault("The Blanchard");
			Tooltip.SetDefault("Turns arrows into Crystal Arrows,  which split into little Arrow Shards");
        }
        public override bool Shoot(Player player,  ref Microsoft.Xna.Framework.Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
			float speedY2 = speedY * 2;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter,  true);
            float num117 = 0.314159274f;
            int num118 = 1;
            Vector2 vector7 = new Vector2(speedX,  speedY2);
            vector7.Normalize();
            vector7 *= 50f;
            bool flag11 = Collision.CanHit(vector2,  0,  0,  vector2 + vector7,  0,  0);
            for (int num119 = 0; num119 < num118; num119++)
            {
                float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
                Vector2 value9 = vector7.RotatedBy((double)(num117 * num120),  default(Vector2));
                if (!flag11)
                {
                    value9 -= vector7;
                }
                int laser = Projectile.NewProjectile(vector2.X + value9.X,  vector2.Y + value9.Y,  speedX,  speedY,  mod.ProjectileType("CrystalArrow"),  damage,  knockBack,  player.whoAmI,  0.0f,  0.0f);
                Main.projectile[laser].penetrate = 1;
            }
            return false;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock,  85);
			recipe.AddIngredient(null,  "CrystalliaBar",  10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
