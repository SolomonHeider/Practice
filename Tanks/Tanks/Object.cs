using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public class Object
    {
        public Bitmap Img { get; protected set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; protected set; }

        public Object()
        {
            X = MainForm.rnd.Next(0, 20);
            Y= MainForm.rnd.Next(0, 20);
            Size = 25;
        }

        public Object(int x, int y)
        {
            X = x;
            Y = y;
            Size = 25;
        }

        public bool CollidesWith(Object obj)
        {
            Rectangle rectA = new Rectangle(X * MainForm.cellSize, Y * MainForm.cellSize, 25, 25);
            Rectangle rectB = new Rectangle(obj.X * MainForm.cellSize, obj.Y * MainForm.cellSize, 25, 25);
            return rectA.IntersectsWith(rectB);
        }
    }
}
