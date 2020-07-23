using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Properties;

namespace Tanks
{
    public class Bullet : MovableObject
    {
        public MovableObject sender { get; set; }
        public Bullet(int x, int y, int direction, MovableObject sender) : base(x, y, direction)
        {
            oldX = x;
            oldY = y;
            this.sender = sender;
        }

        public void Move()
        {
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        oldY = Y;
                        oldX = X;
                        Y++;
                        //if (CollidesWithWalls(Walls))
                        //{
                        //    Y--;
                        //} нужно будет если будешь делать взрывы
                        break;
                    }
                case (int)Direction.Left:
                    {
                        oldY = Y;
                        oldX = X;
                        X--;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        oldY = Y;
                        oldX = X;
                        X++;
                        break;
                    }
                case (int)Direction.Up:
                    {
                        oldY = Y;
                        oldX = X;
                        Y--;
                        break;
                    }
                default:
                    break;
            }
            RotateImage();
        }

       public void RotateImage()
        {
            if (Img == null)
            {
                Img = new Bitmap(Resources.bullet);
            }
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        Img = new Bitmap(Resources.bullet);
                        Img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    }
                case (int)Direction.Left:
                    {
                        Img = new Bitmap(Resources.bullet);
                        Img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    }
                case (int)Direction.Right:
                    {
                        Img = new Bitmap(Resources.bullet);
                        Img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    }
                case (int)Direction.Up:
                    {
                        Img = new Bitmap(Resources.bullet);
                        Img = new Bitmap(Resources.bullet);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
