using System.Collections.Generic;

namespace Tanks
{
    public abstract class MovableObject:Object
    {
        public int oldX { get; set; }
        public int oldY { get; set; }
        public int direction { get; set; }

        public delegate void CreateShot(MovableObject sender);

        public MovableObject() : base()
        {
            direction = MainForm.rnd.Next(0, 4);
        }

        public MovableObject(int x, int y) : base(x, y)
        {
            direction = MainForm.rnd.Next(0, 4);
        }

        public MovableObject(int x, int y, int direction) : base(x, y)
        {
            this.direction = direction;
        }

        public void IdentifyDirection(int direction)
        {
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        this.direction = (int)Direction.Down;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        this.direction = (int)Direction.Left;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        this.direction = (int)Direction.Right;
                        break;
                    }
                case (int)Direction.Up:
                    {
                        this.direction = (int)Direction.Up;
                        break;
                    }
                default:
                    break;
            }      
        }

       public bool CollidesWithWalls(List<Wall> Walls)
        {
            foreach (var item in Walls)
            {
                if (CollidesWith(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CollidesWithRivers(List<River> Rivers)
        {
            for (int i = 0; i < Rivers.Count; i++)
            {
                if (CollidesWith(Rivers[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
