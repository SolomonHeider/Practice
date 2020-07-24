using System;
using System.Windows.Forms;

namespace Tanks
{
    public partial class MainForm : Form
    {
        public static Random rnd = new Random();
        public static int cellSize = 25;
        private MainGame newGame;
        private PositionsForm pos;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;

            btnNewGame.PreviewKeyDown += btn_PreviewKeyDown;
            btnPositions.PreviewKeyDown += btn_PreviewKeyDown;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tanksMap.Width = 500;
            tanksMap.Height = 500;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {        
            newGame = new MainGame(5, 5);
            GameStep.Interval = 60;
            newGame.Start(this);
            GameStep.Enabled = true;
            pos = new PositionsForm();
            btnPositions.Enabled = true;
            tanksMap.Focus();

        }

        private void GameStep_Tick(object sender, EventArgs e)
        {
            newGame.Step();
            GetStats();
            labelScore.Text = "SCORE: " + newGame.Score;
            if (newGame.isGameOver && newGame.moveStep==30)
            {
                GameStep.Enabled = false;
                newGame.GameOver();
                pos.Close();
                btnPositions.Enabled = false;
            }
        }

        public void GetStats()
        {
            pos.gwPositions.Rows.Clear();
            pos.gwPositions.Rows.Add("Kolobok", newGame.kolobok.X, newGame.kolobok.Y);
            for (int i = 0; i < newGame.Tanks.Count; i++)
            {
                pos.gwPositions.Rows.Add("Tank "+i, newGame.Tanks[i].X, newGame.Tanks[i].Y);
            }
            for (int i = 0; i < newGame.Apples.Count; i++)
            {
                pos.gwPositions.Rows.Add("Apple " + i, newGame.Apples[i].X, newGame.Apples[i].Y);
            }
            for (int i = 0; i < newGame.BulletsTanks.Count; i++)
            {
                pos.gwPositions.Rows.Add("Tank's Bullet " + i, newGame.BulletsTanks[i].X, newGame.BulletsTanks[i].Y);
            }
            for (int i = 0; i < newGame.BulletsKolobok.Count; i++)
            {
                pos.gwPositions.Rows.Add("Kolobok's Bullet " + i, newGame.BulletsKolobok[i].X, newGame.BulletsKolobok[i].Y);
            }
        }

        private void btnPositions_Click(object sender, EventArgs e)
        {
            pos.Show();
            tanksMap.Focus();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            newGame.OnKeyPress(e.KeyCode);
            e.Handled = true;
        }

        private void btn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                case Keys.Space:
                    {
                        Console.WriteLine(e.KeyData);
                        e.IsInputKey = true;
                        break;
                    }
            }
        }
    }
}
