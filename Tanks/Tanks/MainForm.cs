using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{ 
    public partial class MainForm : Form
    {
        public static Random rnd = new Random();
        public static int cellSize = 25;
        private MainGame newGame;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tanksMap.Width = 500;
            tanksMap.Height = 500;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {        
            newGame = new MainGame(5, 5);
            GameStep.Interval = 50;
            newGame.Start(this);
            GameStep.Enabled = true;
        }

        private void GameStep_Tick(object sender, EventArgs e)
        {
            newGame.Step();
            if (newGame.isGameOver && newGame.moveStep==30)
            {
                GameStep.Enabled = false;
                newGame.GameOver();
            }
            
        }

        private void TanksForm_KeyDown(object sender, KeyEventArgs e)
        {
            newGame.OnKeyPress(e.KeyCode);
        }

        private void tanksMap_Click(object sender, EventArgs e)
        {

        }
    }
}
