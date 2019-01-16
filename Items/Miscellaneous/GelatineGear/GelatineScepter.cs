using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.Miscellaneous.GelatineGear
{
    public class GelatineScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gelatine Scepter");
            Tooltip.SetDefault("Summons a Grime Baby to fight for you");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
            item.summon = true;
            item.mana = 10;
            item.width = 40;
            item.height = 40;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0,  0,  10,  0);
            item.rare = 3;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("GrimeBaby");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("GrimeBuff"); //The buff added to player after used the item
            item.buffTime = 3600;               //The duration of the buff,  here is 60 seconds
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player,  ref Vector2 position,  ref float speedX,  ref float speedY,  ref int type,  ref int damage,  ref float knockBack)
        {
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
    }
}
