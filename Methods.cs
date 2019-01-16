using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Varia
{
    public class Methods
    {
        public static float SlowRotation(float currentRotation, float targetAngle, float speed)
        {
            int f = 1; //this is used to switch rotation direction
            float actDirection = new Vector2((float)Math.Cos(currentRotation), (float)Math.Sin(currentRotation)).ToRotation();
            targetAngle = new Vector2((float)Math.Cos(targetAngle), (float)Math.Sin(targetAngle)).ToRotation();

            //this makes f 1 or -1 to rotate the shorter distance
            if (Math.Abs(actDirection - targetAngle) > Math.PI)
            {
                f = -1;
            }
            else
            {
                f = 1;
            }

            if (actDirection <= targetAngle + MathHelper.ToRadians(speed * 2) && actDirection >= targetAngle - MathHelper.ToRadians(speed * 2))
            {
                actDirection = targetAngle;
            }
            else if (actDirection <= targetAngle)
            {
                actDirection += MathHelper.ToRadians(speed) * f;
            }
            else if (actDirection >= targetAngle)
            {
                actDirection -= MathHelper.ToRadians(speed) * f;
            }
            actDirection = new Vector2((float)Math.Cos(actDirection), (float)Math.Sin(actDirection)).ToRotation();

            return actDirection;
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

        public static void AreaConvert(Vector2 Center, int radius, List<int> tileTypesToConvert, int convertTo, int rate = 1, int dustType = -1)
        {
            if (Main.rand.Next(rate) == 0) //rate inversly effects average time
            {
                bool found = false;
                for (int t = 0; t < 100; t++)
                {
                    int distance = Main.rand.Next(radius);
                    float rotation = (float)Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                    Vector2 convertAttempt = Center + PolarVector(distance, rotation);
                    int i = (int)convertAttempt.X / 16;
                    int j = (int)convertAttempt.Y / 16;
                    foreach (int tileType in tileTypesToConvert)
                    {
                        if (Main.tile[i, j].type == tileType && Main.tile[i, j].active())
                        {
                            WorldGen.PlaceTile(i, j, convertTo, false, true);
                            if (dustType != -1)
                            {
                                for (int d = 0; d < 40; d++)
                                {
                                    Dust.NewDust(new Vector2(i, j) * 16, 16, 16, dustType);
                                }
                            }
                            found = true;
                            break;

                        }
                        if (found)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}