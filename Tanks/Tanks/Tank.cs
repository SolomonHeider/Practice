using System;
using System.Collections.Generic;
using System.Drawing;
using Tanks.Properties;

namespace Tanks
{
    public class Tank: MovableObject
    {
        private Random rnd = new Random();
        public Tank() : base()
        {
            Img = new Bitmap(Resources.tank);
        }

        public Tank(int x, int y) : base(x, y)
        {
            Img = new Bitmap(Resources.tank);
        }

        public void Move(List<Wall> Walls, List<Tank> Tanks)
        {
            if (rnd.NextDouble() < 0.4)
            {
                IdentifyDirection(MainForm.rnd.Next(0, 4));
            }

            switch (DirectionTo)
            {
                case (int)Direction.Down:
                    {
                        OldY = Y;
                        OldX = X;
                        Y++;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks))
                        {
                            Y--;
                            IdentifyDirection((int)Direction.Up);
                        }
                        break;
                    }
                case (int)Direction.Left:
                    {
                        OldY = Y;
                        OldX = X;
                        X--;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks))
                        {
                            X++;
                            IdentifyDirection((int)Direction.Right);
                        }
                        break;
                    }
                case (int)Direction.Right:
                    {
                        OldY = Y;
                        OldX = X;
                        X++;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks))
                        {
                            X--;
                            IdentifyDirection((int)Direction.Left);
                        }
                        break;
                    }
                case (int)Direction.Up:
                    {
                        OldY = Y;
                        OldX = X;
                        Y--;
                        if (CollidesWithWalls(Walls) || CollidesWithTanks(Tanks))
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
