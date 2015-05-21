namespace Heavy_Engine
{
    partial class LogWindow
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
            this.txt_loglist = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_loglist
            // 
            this.txt_loglist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_loglist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_loglist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_loglist.ForeColor = System.Drawing.Color.Black;
            this.txt_loglist.Location = new System.Drawing.Point(0, 0);
            this.txt_loglist.Multiline = true;
            this.txt_loglist.Name = "txt_loglist";
            this.txt_loglist.ReadOnly = true;
            this.txt_loglist.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_loglist.Size = new System.Drawing.Size(452, 462);
            this.txt_loglist.TabIndex = 0;
            this.txt_loglist.Text = "Heavy Engine Buid Log v2.0.0\r\n====================\r\nBuild Started...\r\n";
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 462);
            this.Controls.Add(this.txt_loglist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LogWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Log";
            this.Load += new System.EventHandler(this.ErrorViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_loglist;

    }
}