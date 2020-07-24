using System;
using System.Collections.Generic;
using System.Drawing;
using Tanks.Properties;

namespace Tanks
{
    public class Tank: MovableObject
    {
        private Random Rnd = new Random();
        public event CreateShot Shot;

        public Tank() : base()
        {
            Img = new Bitmap(Resources.tank);
        }

        public Tank(int x, int y) : base(x, y)
        {
            Img = new Bitmap(Resources.tank);
        }

        public void Move(List<Wall> Walls, List<River> Rivers, List<Tank> Tanks)
        {
            if (Rnd.NextDouble() < 0.4)
            {
                IdentifyDirection(MainForm.rnd.Next(0, 4));
            }
            if (Rnd.NextDouble() < 0.15)
            {
                Shot?.Invoke(this);
            }

            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        oldY = Y;
                        oldX = X;
                        Y++;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks) || CollidesWithRivers(Rivers))
                        {
                            Y--;
                            IdentifyDirection((int)Direction.Up);
                        }
                        break;
                    }
                case (int)Direction.Left:
                    {
                        oldY = Y;
                        oldX = X;
                        X--;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks) || CollidesWithRivers(Rivers))
                        {
                            X++;
                            IdentifyDirection((int)Direction.Right);
                        }
                        break;
                    }
                case (int)Direction.Right:
                    {
                        oldY = Y;
                        oldX = X;
                        X++;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks) || CollidesWithRivers(Rivers))
                        {
                            X--;
                            IdentifyDirection((int)Direction.Left);
                        }
                        break;
                    }
                case (int)Direction.Up:
                    {
                        oldY = Y;
                        oldX = X;
                        Y--;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks) || CollidesWithRivers(Rivers))
                        {
                            Y++;
                            IdentifyDirection((int)Direction.Down);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
       public bool CollidesWithTanks(List<Tank> Tanks)
        {
            for (int i = 0; i < Tanks.Count; i++)
            {
                if (CollidesWith(Tanks[i]) && this!=Tanks[i])
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
