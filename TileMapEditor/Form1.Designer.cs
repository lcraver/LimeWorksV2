namespace TileMapEditor
{
    partial class Form1
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
            this.editor1 = new TileMapEditor.Editor();
            this.tileDisplay1 = new TileMapEditor.TileDisplay(ref editor1);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editor1
            // 
            this.editor1.Location = new System.Drawing.Point(6, 3);
            this.editor1.Name = "editor1";
            this.editor1.Size = new System.Drawing.Size(709, 512);
            this.editor1.TabIndex = 0;
            this.editor1.Text = "editor1";
            // 
            // tileDisplay1
            // 
            this.tileDisplay1.Location = new System.Drawing.Point(2, 3);
            this.tileDisplay1.Name = "tileDisplay1";
            this.tileDisplay1.Size = new System.Drawing.Size(1024, 1024);
            this.tileDisplay1.TabIndex = 1;
            this.tileDisplay1.Text = "tileDisplay1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.editor1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tileDisplay1);
            this.splitContainer1.Size = new System.Drawing.Size(1240, 592);
            this.splitContainer1.SplitterDistance = 720;
            this.splitContainer1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "TileMap Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Editor editor1;
        private TileDisplay tileDisplay1;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}

