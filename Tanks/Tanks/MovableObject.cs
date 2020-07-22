using System.Collections.Generic;

namespace Tanks
{
    public abstract class MovableObject:Object
    {
        public int OldX { get; set; }
        public int OldY { get; set; }
        public int DirectionTo { get; set; }

        public delegate void CreateShot(MovableObject sender);

        public MovableObject() : base()
        {
            DirectionTo = MainForm.rnd.Next(0, 4);
        }

        public MovableObject(int x, int y) : base(x, y)
        {
            DirectionTo = MainForm.rnd.Next(0, 4);
        }

        public MovableObject(int x, int y, int direction) : base(x, y)
        {
            DirectionTo = direction;
        }

        public void IdentifyDirection(int direction)
        {
            switch (direction)
            {
                case (int)Direction.Down:
                    {
                        DirectionTo = (int)Direction.Down;
                        break;
                    }
                case (int)Direction.Left:
                    {
                        DirectionTo = (int)Direction.Left;
                        break;
                    }
                case (int)Direction.Right:
                    {
                        DirectionTo = (int)Direction.Right;
                        break;
                    }
                case (int)Direction.Up:
                    {
                        DirectionTo = (int)Direction.Up;
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

    }
}
