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
            this.labelScore = new System.Windows.Forms.Label();
            this.GameStep = new System.Windows.Forms.Timer(this.components);
            this.btnPositions = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tanksMap)).BeginInit();
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
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.Location = new System.Drawing.Point(15, 42);
            this.labelScore.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(89, 25);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "SCORE:";
            // 
            // GameStep
            // 
            this.GameStep.Interval = 50;
            this.GameStep.Tick += new System.EventHandler(this.GameStep_Tick);
            // 
            // btnPositions
            // 
            this.btnPositions.Enabled = false;
            this.btnPositions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPositions.Location = new System.Drawing.Point(14, 157);
            this.btnPositions.Margin = new System.Windows.Forms.Padding(6);
            this.btnPositions.Name = "btnPositions";
            this.btnPositions.Size = new System.Drawing.Size(228, 42);
            this.btnPositions.TabIndex = 3;
            this.btnPositions.TabStop = false;
            this.btnPositions.Text = "POSITIONS";
            this.btnPositions.UseVisualStyleBackColor = true;
            this.btnPositions.Click += new System.EventHandler(this.btnPositions_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 226);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "MOVE: WASD or arrows";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(15, 265);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "SHOOT: E or Space";
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnNewGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(764, 523);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPositions);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.tanksMap);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanks";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tanksMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Label labelScore;
        public System.Windows.Forms.PictureBox tanksMap;
        public System.Windows.Forms.Timer GameStep;
        private System.Windows.Forms.Button btnPositions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

