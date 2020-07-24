using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tanks.Properties;

namespace Tanks
{
    public class MainGame
    {
        private Bitmap backgroundMap;
        private Graphics mapGraphics;
        private PictureBox map;

        public int Score;
        public Kolobok kolobok { get; set; }
        public List<Bullet> BulletsKolobok { get; set; } = new List<Bullet>();

        public List<Wall> Walls { get; set; } = new List<Wall>();
        public List<River> Rivers { get; set; } = new List<River>();
        public List<Apple> Apples { get; set; } = new List<Apple>();
        private int applesCount = 5;

        public List<Tank> Tanks { get; set; } = new List<Tank>();
        private int tanksCount = 5;
        public List<Bullet> BulletsTanks { get; set; } = new List<Bullet>();

        public bool isGameOver { get; set; } = false;
        public int moveStep { get; set; } = 30;
        public int moveShotStep { get; set; } = 30;

        private string[] wallsPattern = {"********************",
                                         "*                  *",
                                         "*  **  **  **  **  *",
                                         "*  **  ******  **  *",
                                         "*  **  **  **  **  *",
                                         "*  **          **  *",
                                         "*     **    **     *",
                                         "*        #         *",
                                         "*  ****  #   ****  *",
                                         "*  #######         *",
                                         "*  #               *",
                                         "*  #***      ****  *",
                                         "*###          #####*",
                                         "*     **    **#    *",
                                         "*  **       ###**  *",
                                         "*  **  **  **  **  *",
                                         "*  **  ******  **  *",
                                         "*  **  **  **  **  *",
                                         "*                  *",
                                         "********************"};

        public MainGame(int applesCount, int tanksCount)
        {
            this.applesCount = applesCount;
            this.tanksCount = tanksCount;
        }

        public void Start(MainForm fm)
        {
            //чистим всё
            Unsubscribe();
            Apples.Clear();
            Tanks.Clear();
            Walls.Clear();
            kolobok = null;
            Score = 0;
            //задаем начальные параметры
            moveStep = 30; //приращение шага
            map = fm.tanksMap;
            backgroundMap = new Bitmap(map.Width, map.Height);
            mapGraphics = Graphics.FromImage(backgroundMap);
            mapGraphics.FillRectangle(Brushes.Black, 0, 0, map.Width, map.Height);
            
            //генерируем стены из шаблона
            for (int i = 0; i < wallsPattern.Count(); i++)
            {
                for (int j = 0; j < wallsPattern[i].Length; j++)
                {
                    if (wallsPattern[i][j]=='*')
                    {
                        Walls.Add(new Wall(j, i));
                        mapGraphics.DrawImage(Walls.Last().Img, Walls.Last().X * MainForm.cellSize, Walls.Last().Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                    }
                    else if (wallsPattern[i][j] == '#')
                    {
                        Rivers.Add(new River(j, i));
                        mapGraphics.DrawImage(Rivers.Last().Img, Rivers.Last().X * MainForm.cellSize, Rivers.Last().Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                    }
                }              
            }
            
            SpawnApples();
            SpawnTanks();
            
            //отрисовка яблок и танков
            for (int i = 0; i < Apples.Count; i++)
            {
                mapGraphics.DrawImage(Apples[i].Img, Apples[i].X * MainForm.cellSize, Apples[i].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            }

            for (int i = 0; i < Tanks.Count; i++)
            {
                mapGraphics.DrawImage(Tanks[i].Img, Tanks[i].X * MainForm.cellSize, Tanks[i].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            }
            
            //создание и отрисовка колобка
            while(true)
            {
                kolobok = new Kolobok();
                if (!kolobok.CollidesWithWalls(Walls)&& !kolobok.CollidesWithRivers(Rivers))
                {                   
                    break;
                }
            }            
            mapGraphics.DrawImage(kolobok.Img, kolobok.X * MainForm.cellSize, kolobok.Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            
            //отрисовка всего на picturebox
            map.Image = backgroundMap;

            Subscribe();
        }

        public void SpawnApples()
        {
            while (Apples.Count < applesCount)
            {
                Apples.Add(new Apple());
                foreach (var item in Walls)
                {
                    //если коллизия со стеной, переспавнить
                    if (Apples.Last().CollidesWith(item))
                    {
                        Apples.RemoveAt(Apples.Count - 1);
                        break;
                    }
                }
                if (Apples.Count > 0)
                {
                    //если коллизия с рекой, переспавнить
                    for (int i = 0; i < Rivers.Count; i++)
                    {
                        if (Apples.Last().CollidesWith(Rivers[i]))
                        {
                            Apples.RemoveAt(Apples.Count - 1);
                            break;
                        }
                    }
                }
                
                //если коллизия с другим яблоком, переспавнить
                for (int i = 0; i < Apples.Count; i++)
                {
                    if (Apples.Last().CollidesWith(Apples[i]) && Apples.Last() != Apples[i])
                    {
                        Apples.RemoveAt(Apples.Count - 1);
                        break;
                    }
                }
            }
        }  
        
        public void SpawnTanks()
        {
            while (Tanks.Count < tanksCount)
            {
                Tanks.Add(new Tank());
                foreach (var item in Walls)
                {
                    //если коллизия со стеной, переспавнить
                    if (Tanks.Last().CollidesWith(item))
                    {
                        Tanks.RemoveAt(Tanks.Count - 1);
                        break;
                    }
                }
                //если коллизия с другим танком, переспавнить
                for (int i = 0; i < Tanks.Count; i++)
                {
                    if (Tanks.Last().CollidesWith(Tanks[i]) && Tanks.Last() != Tanks[i])
                    {
                       Tanks.RemoveAt(Tanks.Count - 1);
                        break;
                    }
                }
                if (Tanks.Count > 0)
                {
                    for (int i = 0; i < Rivers.Count; i++)
                    {
                        //если коллизия с рекой, переспавнить
                        if (Tanks.Last().CollidesWith(Rivers[i]))
                        {
                            Tanks.RemoveAt(Tanks.Count - 1);
                            break;
                        }
                    }
                }
            }          
        }
       
        public void Step() //все объекты на карте перемещаются на шаг
        {
            while (moveStep != MainForm.cellSize + 5)
            {               
                MoveObject(kolobok, moveStep);

                for (int i = 0; i < Tanks.Count; i++)
                {
                    MoveObject(Tanks[i], moveStep);
                }

                for (int i = 0; i < BulletsTanks.Count; i++)
                {
                    MoveObject(BulletsTanks[i], moveStep);
                }
                for (int i = 0; i < BulletsKolobok.Count; i++)
                {
                    MoveObject(BulletsKolobok[i], moveStep);
                }

                moveStep += 5;
                map.Image = backgroundMap;
                return;
            }

            kolobok.Move(Walls, Rivers);
            moveStep = 5;
            MoveObject(kolobok, moveStep);

            for (int i = 0; i < Tanks.Count; i++)
            {
                Tanks[i].Move(Walls, Rivers, Tanks);

                MoveObject(Tanks[i], moveStep);
            }

            for (int i = 0; i < Apples.Count; i++)
            {
                if (Apples[i].CollidesWith(kolobok))
                {
                    Apples.RemoveAt(i);
                    Score++;
                    break;
                }
                mapGraphics.DrawImage(Apples[i].Img, Apples[i].X * MainForm.cellSize, Apples[i].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            }
            SpawnApples();

            for (int i = 0; i < BulletsTanks.Count; i++)
            {
                BulletsTanks[i].Move();
                if (BulletsTanks[i].CollidesWithWalls(Walls))
                {
                    mapGraphics.FillRectangle(Brushes.Black, BulletsTanks[i].oldX * MainForm.cellSize,
                        BulletsTanks[i].oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                    BulletsTanks.RemoveAt(i);
                    i--;
                    continue;
                }
                MoveObject(BulletsTanks[i], moveStep);
            }

            for (int i = 0; i < BulletsKolobok.Count; i++)
            {
                BulletsKolobok[i].Move();
                if (BulletsKolobok[i].CollidesWithWalls(Walls))
                {
                    mapGraphics.FillRectangle(Brushes.Black, BulletsKolobok[i].oldX * MainForm.cellSize,
                        BulletsKolobok[i].oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                    BulletsKolobok.RemoveAt(i);
                    i--;
                    continue;
                }                
                MoveObject(BulletsKolobok[i], moveStep);
            }           

            for (int i = 0; i < BulletsTanks.Count; i++)
            {
                if (BulletsTanks[i].CollidesWith(kolobok))
                {
                    isGameOver = true;
                    break;
                }
            }
            for (int i = 0; i < Tanks.Count; i++)
            {
                if (Tanks[i].CollidesWith(kolobok))
                {
                    isGameOver = true;
                    return;
                }
                for (int j = 0; j < BulletsKolobok.Count; j++)
                {
                    if (Tanks[i].CollidesWith(BulletsKolobok[j]))
                    {
                        mapGraphics.FillRectangle(Brushes.Black, Tanks[i].oldX * MainForm.cellSize, Tanks[i].oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                        mapGraphics.FillRectangle(Brushes.Black, BulletsKolobok[j].X * MainForm.cellSize, BulletsKolobok[j].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                        mapGraphics.FillRectangle(Brushes.Black, Tanks[i].X * MainForm.cellSize, Tanks[i].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                        mapGraphics.FillRectangle(Brushes.Black, BulletsKolobok[j].oldX * MainForm.cellSize, BulletsKolobok[j].oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                        Tanks.RemoveAt(i);
                        BulletsKolobok.RemoveAt(j);
                        i--;
                        SpawnTanks();
                        break;
                    
                    }
                }
            }
            moveStep += 5;
            mapGraphics.DrawImage(Walls[0].Img, Walls[0].X * MainForm.cellSize, Walls[0].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            mapGraphics.DrawImage(Walls[1].Img, Walls[1].X * MainForm.cellSize, Walls[1].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            foreach (var item in Rivers)
            {
                mapGraphics.DrawImage(item.Img, item.X * MainForm.cellSize, item.Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);

            }
            map.Image = backgroundMap;

        }

        //визуальное перемещение объектов
        public void MoveObject(MovableObject obj, int step)
        {
            if (obj.oldX < obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.oldX * MainForm.cellSize + step - 5, obj.oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.oldX * MainForm.cellSize + step, obj.oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.oldX > obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.oldX * MainForm.cellSize - step + 5, obj.oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.oldX * MainForm.cellSize - step, obj.oldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.oldY < obj.Y)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.oldX * MainForm.cellSize, obj.oldY * MainForm.cellSize + step - 5, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.oldX * MainForm.cellSize, obj.oldY * MainForm.cellSize + step, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.oldY > obj.Y)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.oldX * MainForm.cellSize, obj.oldY * MainForm.cellSize - step + 5, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.oldX * MainForm.cellSize, obj.oldY * MainForm.cellSize - step, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.oldY == obj.Y && obj.oldX > obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.oldX * MainForm.cellSize, obj.Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.X * MainForm.cellSize, obj.Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                return;
            }
        }
       
        //проигрыш
        public void GameOver()
        {
            mapGraphics.DrawImage(new Bitmap(Resources.gameOver), 50, 50, 400, 400);
            map.Image = backgroundMap;
            Unsubscribe();
        }

        //генерация пуль
        public void AddBullet (MovableObject bulletSender)
        {           
            if (bulletSender is Tank t)
            {
                switch (bulletSender.direction)
                {
                    case (int)Direction.Down:
                        {
                            BulletsTanks.Add(new Bullet(bulletSender.X, bulletSender.Y + 1,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Up:
                        {
                            BulletsTanks.Add(new Bullet(bulletSender.X, bulletSender.Y - 1,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Left:
                        {
                            BulletsTanks.Add(new Bullet(bulletSender.X - 1, bulletSender.Y,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Right:
                        {
                            BulletsTanks.Add(new Bullet(bulletSender.X + 1, bulletSender.Y,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    default:
                        break;
                }
                if (BulletsTanks.Last().CollidesWithWalls(Walls))
                {
                    BulletsTanks.RemoveAt(BulletsTanks.Count - 1);
                }
            }
        }

        public void AddBulletKolobok (MovableObject bulletSender)
        {
            if (bulletSender is Kolobok k)
            {
                switch (bulletSender.direction)
                {
                    case (int)Direction.Down:
                        {
                            BulletsKolobok.Add(new Bullet(bulletSender.X, bulletSender.Y + 1,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Up:
                        {
                            BulletsKolobok.Add(new Bullet(bulletSender.X, bulletSender.Y - 1,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Left:
                        {
                            BulletsKolobok.Add(new Bullet(bulletSender.X - 1, bulletSender.Y,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    case (int)Direction.Right:
                        {
                            BulletsKolobok.Add(new Bullet(bulletSender.X + 1, bulletSender.Y,
                                bulletSender.direction, bulletSender));
                            break;
                        }
                    default:
                        break;
                }

                if (BulletsKolobok.Last().CollidesWithWalls(Walls))
                {
                    BulletsKolobok.RemoveAt(BulletsKolobok.Count - 1);
                }
            }
        }

        //событие, отписки и подписки колобка на нажатия клавиш
        //также подписки на выстрелы
        private event KeyEventHandler KeyPress;

        public void OnKeyPress(Keys key)
        {
            KeyPress?.Invoke(kolobok, new KeyEventArgs(key));
        }

        public void Subscribe()
        {
            KeyPress += new KeyEventHandler(kolobok.OnKeyPress);
            kolobok.Shot += AddBulletKolobok;
            for (int i = 0; i < Tanks.Count; i++)
            {
                Tanks[i].Shot += AddBullet;
            }
        }

        public void Unsubscribe()
        {
            if (KeyPress!=null)
            {
                KeyPress -= new KeyEventHandler(kolobok.OnKeyPress);
            }
        }
        
    }
}
