namespace Tanks
{
    partial class AboutObjectsForm
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
            this.ctlAboutObjects = new System.Windows.Forms.DataGridView();
            this.Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ctlAboutObjects)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlAboutObjects
            // 
            this.ctlAboutObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctlAboutObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Object,
            this.X,
            this.Y});
            this.ctlAboutObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlAboutObjects.Location = new System.Drawing.Point(0, 0);
            this.ctlAboutObjects.Margin = new System.Windows.Forms.Padding(5);
            this.ctlAboutObjects.Name = "ctlAboutObjects";
            this.ctlAboutObjects.ReadOnly = true;
            this.ctlAboutObjects.RowTemplate.Height = 24;
            this.ctlAboutObjects.Size = new System.Drawing.Size(323, 568);
            this.ctlAboutObjects.TabIndex = 0;
            // 
            // Object
            // 
            this.Object.HeaderText = "Object";
            this.Object.Name = "Object";
            this.Object.ReadOnly = true;
            this.Object.Width = 200;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 40;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 40;
            // 
            // AboutObjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 568);
            this.Controls.Add(this.ctlAboutObjects);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "AboutObjectsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About Objects";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutObjectsForm_FormClosed);
            this.Load += new System.EventHandler(this.AboutObjectsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctlAboutObjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView ctlAboutObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
    }
}