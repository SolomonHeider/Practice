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

        public Kolobok kolobok { get; set; }
        public List<Wall> Walls { get; set; } = new List<Wall>();
        public List<Apple> Apples { get; set; } = new List<Apple>();
        private int applesCount = 5;
        public List<Tank> Tanks { get; set; } = new List<Tank>();
        private int tanksCount = 5;

        public bool isGameOver { get; set; } = false;
        public int moveStep { get; set; } = 30;

        private string[] wallsPattern = {"********************",
                                         "*                  *",
                                         "*  **  **  **  **  *",
                                         "*  **  ******  **  *",
                                         "*  **  **  **  **  *",
                                         "*  **          **  *",
                                         "*     **    **     *",
                                         "*                  *",
                                         "*  ****      ****  *",                                         
                                         "*      **  **      *",
                                         "*      ******      *",
                                         "*  ****      ****  *",
                                         "*                  *",
                                         "*     **    **     *",
                                         "*  **          **  *",
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
                }              
            }
            
            SpawnApples();//ну тут всё понятно
            
            //спавн танков
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
            }
            
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
            if (!kolobok.CollidesWithWalls(Walls))
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
                    if (Apples.Last().CollidesWith(item))
                    {
                        Apples.RemoveAt(Apples.Count - 1);
                        break;
                    }
                }
                //если коллизия со стеной, переспавнить
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
       
        public void Step() //все объекты на карте перемещаются на шаг
        {
            while (moveStep != MainForm.cellSize + 5)
            {               
                MoveObject(kolobok, moveStep);

                for (int i = 0; i < Tanks.Count; i++)
                {
                    MoveObject(Tanks[i], moveStep);
                }                
                moveStep += 5;
                map.Image = backgroundMap;
                return;
            }

            kolobok.Move(Walls);
            moveStep = 5;
            MoveObject(kolobok, moveStep);
           
            for (int i = 0; i < Tanks.Count; i++)
            {
                if (Tanks[i].CollidesWith(kolobok))
                {
                    isGameOver = true;
                    return;
                }
                Tanks[i].Move(Walls, Tanks);

                if (Tanks[i].CollidesWith(kolobok))
                {
                    isGameOver = true;
                    return;
                }
                MoveObject(Tanks[i], moveStep);
            }

            for (int i = 0; i < Apples.Count; i++)
            {
                if (Apples[i].CollidesWith(kolobok))
                {
                    Apples.RemoveAt(i);
                    break;
                }
                mapGraphics.DrawImage(Apples[i].Img, Apples[i].X * MainForm.cellSize, Apples[i].Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
            }
            SpawnApples();

            moveStep += 5;
            map.Image = backgroundMap;
        }
        //перемещение объектов
        public void MoveObject(MovableObject obj, int step)
        {
            if (obj.OldX < obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.OldX * MainForm.cellSize + step - 5, obj.OldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.OldX * MainForm.cellSize + step, obj.OldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.OldX > obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.OldX * MainForm.cellSize - step + 5, obj.OldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.OldX * MainForm.cellSize - step, obj.OldY * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.OldY < obj.Y)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.OldX * MainForm.cellSize, obj.OldY * MainForm.cellSize + step - 5, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.OldX * MainForm.cellSize, obj.OldY * MainForm.cellSize + step, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.OldY > obj.Y)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.OldX * MainForm.cellSize, obj.OldY * MainForm.cellSize - step + 5, MainForm.cellSize, MainForm.cellSize);
                mapGraphics.DrawImage(obj.Img, obj.OldX * MainForm.cellSize, obj.OldY * MainForm.cellSize - step, MainForm.cellSize, MainForm.cellSize);
                return;
            }
            if (obj.OldY == obj.Y && obj.OldX > obj.X)
            {
                mapGraphics.FillRectangle(Brushes.Black, obj.OldX * MainForm.cellSize, obj.Y * MainForm.cellSize, MainForm.cellSize, MainForm.cellSize);
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
        //событие, отписки и подписки колобка на нажатия клавиш
        private event KeyEventHandler KeyPress;

        public void OnKeyPress(Keys key)
        {
            KeyPress?.Invoke(kolobok, new KeyEventArgs(key));
        }

        public void Subscribe()
        {
            KeyPress += new KeyEventHandler(kolobok.OnKeyPress);
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
