namespace Tanks
{
    partial class PositionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gwPositions = new System.Windows.Forms.DataGridView();
            this.Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gwPositions)).BeginInit();
            this.SuspendLayout();
            // 
            // gwPositions
            // 
            this.gwPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gwPositions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Object,
            this.X,
            this.Y});
            this.gwPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gwPositions.Location = new System.Drawing.Point(0, 0);
            this.gwPositions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gwPositions.Name = "gwPositions";
            this.gwPositions.ReadOnly = true;
            this.gwPositions.RowHeadersWidth = 51;
            this.gwPositions.RowTemplate.Height = 24;
            this.gwPositions.Size = new System.Drawing.Size(474, 331);
            this.gwPositions.TabIndex = 0;
            // 
            // Object
            // 
            this.Object.HeaderText = "Object";
            this.Object.MinimumWidth = 6;
            this.Object.Name = "Object";
            this.Object.ReadOnly = true;
            this.Object.Width = 300;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.MinimumWidth = 6;
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 60;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.MinimumWidth = 6;
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 60;
            // 
            // PositionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 331);
            this.Controls.Add(this.gwPositions);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PositionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Objects";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutObjectsForm_FormClosed);
            this.Load += new System.EventHandler(this.AboutObjectsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gwPositions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView gwPositions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
    }
}