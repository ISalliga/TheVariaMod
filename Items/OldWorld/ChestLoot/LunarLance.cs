using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Varia.Items.OldWorld.ChestLoot
{
    public class LunarLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[item.type] = true;
            DisplayName.SetDefault("Lunar Lance");
            Tooltip.SetDefault("Summons a sigil that fires lunar beams at enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 25;
            item.summon = true;
            item.width = 46;
            item.height = 20;
            item.useTime = 40;
            item.mana = 19;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4.5f;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shootSpeed = 0.1f;
            item.shoot = mod.ProjectileType("LunarSigil");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            speedX = 0;
            speedY = 0;
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}