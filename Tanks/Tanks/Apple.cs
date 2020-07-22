using System.Drawing;
using Tanks.Properties;

namespace Tanks
{
    public class Apple:Object
    {
        public Apple() : base()
        {
            Img = new Bitmap(Resources.apple);
        }

        public Apple(int x, int y) : base(x, y)
        {
            Img = new Bitmap(Resources.apple);
        }
    }
}
