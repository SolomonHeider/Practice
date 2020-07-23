namespace Tanks
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tanksMap = new System.Windows.Forms.PictureBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctlScore = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.GameStep = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tanksMap)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tanksMap
            // 
            this.tanksMap.BackColor = System.Drawing.Color.Black;
            this.tanksMap.Location = new System.Drawing.Point(254, 13);
            this.tanksMap.Margin = new System.Windows.Forms.Padding(6);
            this.tanksMap.Name = "tanksMap";
            this.tanksMap.Size = new System.Drawing.Size(500, 500);
            this.tanksMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.tanksMap.TabIndex = 0;
            this.tanksMap.TabStop = false;
            this.tanksMap.Click += new System.EventHandler(this.tanksMap_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewGame.Location = new System.Drawing.Point(14, 103);
            this.btnNewGame.Margin = new System.Windows.Forms.Padding(6);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(228, 42);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.TabStop = false;
            this.btnNewGame.Text = "NEW GAME";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ctlScore);
            this.panel1.Controls.Add(this.labelScore);
            this.panel1.Location = new System.Drawing.Point(14, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 78);
            this.panel1.TabIndex = 2;
            // 
            // ctlScore
            // 
            this.ctlScore.AutoSize = true;
            this.ctlScore.Location = new System.Drawing.Point(101, 23);
            this.ctlScore.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ctlScore.Name = "ctlScore";
            this.ctlScore.Size = new System.Drawing.Size(0, 29);
            this.ctlScore.TabIndex = 1;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.Location = new System.Drawing.Point(6, 23);
            this.labelScore.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(89, 25);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "SCORE:";
            this.labelScore.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameStep
            // 
            this.GameStep.Interval = 50;
            this.GameStep.Tick += new System.EventHandler(this.GameStep_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(770, 523);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.tanksMap);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanks";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TanksForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tanksMap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ctlScore;
        private System.Windows.Forms.Label labelScore;
        public System.Windows.Forms.PictureBox tanksMap;
        public System.Windows.Forms.Timer GameStep;
    }
}

