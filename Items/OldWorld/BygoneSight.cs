using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld
{
    public class BygoneSight : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 14;
            item.ranged = true;
            item.width = 40;
            item.height = 68;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0,  1,  0,  0);
            item.rare = 5;
            item.UseSound = SoundID.Item102;
            item.autoReuse = false;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 7;
            item.useAmmo = AmmoID.Arrow;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bygone Sight");
            Tooltip.SetDefault("Has a chance to turn arrows into stone arrows \nStone arrows do more damage to NPCs, but split into lingering pebbles upon hitting tiles");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0.8f, 0);
        }
        public override bool Shoot(Player player,  ref Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            bool rand = Main.rand.Next(3) == 0;
            if (rand)
            {
                type = mod.ProjectileType("StoneArrow");
                damage *= 5/3;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ShardOfThePast", 2);
            recipe.AddIngredient(null, "OldWorldAlloy", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
