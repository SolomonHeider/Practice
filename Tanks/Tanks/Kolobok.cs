using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    public class Kolobok:MovableObject
    {
        public event CreateShot Shot;
        public Kolobok():base()
        {
            Img = new Bitmap(Resources.kolobok);
        }

        public Kolobok(int x, int y) : base(x, y)
        {
            Img = new Bitmap(Resources.kolobok);
        }
        //обработки нажатия клавиш
        public void OnKeyPress(object sender, KeyEventArgs e)
        {
                switch (e.KeyData)
                {
                    case Keys.W:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Up);
                            break;
                        }
                    case Keys.S:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Down);
                            break;
                        }
                    case Keys.A:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Left);
                            break;
                        }
                    case Keys.D:
                        {
                            ((Kolobok)sender).IdentifyDirection((int)Direction.Right);
                            break;
                        }
                    case Keys.E:
                    {
                        Shot?.Invoke(this);
                        break;
                    }
                default:
                        break;
                }             
        }
        //движение
        public void Move(List<Wall> Walls)
        {
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        oldY = Y;
                        oldX = X;
                        Y++;
                        if (CollidesWithWalls(Walls))
                        {
                            Y--;
                        }
                        break;
                    }
                case (int)Direction.Left:
                    {
                        oldY = Y;
                        oldX = X;
                        X--;
                        if (CollidesWithWalls(Walls))
                        {
                            X++;
                        }
                        break;
                    }
                case (int)Direction.Right:
                    {
                        oldY = Y;
                        oldX = X;
                        X++;
                        if (CollidesWithWalls(Walls))
                        {
                            X--;
                        }
                        break;
                    }
                case (int)Direction.Up:
                    {
                        oldY = Y;
                        oldX = X;
                        Y--;
                        if (CollidesWithWalls(Walls))
                        {
                            Y++;
                        }
                        break;
                    }
                default:
                    break;
            }            
        }
      
    }
}
