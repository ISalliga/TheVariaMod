using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.DataStructures;
using Varia.UI;

namespace Varia.UI
{
    public class InfectionBar : UIState
    {
        public Texture2D MainTexture;
        public Texture2D BarTexture;
        public int CalculateLength(int value, int maxvalue, int maxlength)
        {
            return (value * maxlength) / maxvalue;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (MainTexture == null)
                MainTexture = ModContent.GetTexture("Varia/UI/InfectionBar");
            if(BarTexture == null)
                BarTexture = ModContent.GetTexture("Varia/UI/InfectionBarFill");

            if(MainTexture != null && BarTexture != null)
            {
                Player player = Main.LocalPlayer;
                VariaPlayer MP = player.GetModPlayer<VariaPlayer>(); 
                if (player.GetModPlayer<VariaPlayer>().SeeInfectionBar)
                {
                    Vector2 position = new Vector2(Main.screenWidth / 2 - MainTexture.Width / 2, Main.screenHeight / 2 - 85);
                    // Change Infection and InfectionCap to whatever you have them called in the player file.
                    var length = CalculateLength((int)MP.Infection, (int)MP.InfectionCap, BarTexture.Width);
                    // Order matters. The current order is the fill bar behind the main bar. If you need it in front, swap these.
                    spriteBatch.Draw(MainTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    spriteBatch.Draw(BarTexture, position + new Vector2(8, 8), new Rectangle(0, 0, length, BarTexture.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    // Edit new Vector2(0, 0) with numbers until you fit it exactly where you need it.
                }
            }
            
        }
    }
}
