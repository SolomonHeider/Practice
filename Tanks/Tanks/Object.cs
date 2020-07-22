using System.Drawing;

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
            if (X == obj.X && Y == obj.Y || X < 0 || X >= 20 || Y < 0 || Y >= 20)
            {
                return true;
            }
            return false;
        }
    }
}
