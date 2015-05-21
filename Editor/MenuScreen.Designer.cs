namespace Heavy_Engine
{
    partial class MenuScreen
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
            this.lbl_window_name = new System.Windows.Forms.Label();
            this.btn_newproject = new System.Windows.Forms.Button();
            this.btn_loadproject = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.lbl_legal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_window_name
            // 
            this.lbl_window_name.AutoSize = true;
            this.lbl_window_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_window_name.Location = new System.Drawing.Point(339, 27);
            this.lbl_window_name.Name = "lbl_window_name";
            this.lbl_window_name.Size = new System.Drawing.Size(194, 29);
            this.lbl_window_name.TabIndex = 0;
            this.lbl_window_name.Text = "Heavy Engine 2";
            // 
            // btn_newproject
            // 
            this.btn_newproject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_newproject.Location = new System.Drawing.Point(344, 116);
            this.btn_newproject.Name = "btn_newproject";
            this.btn_newproject.Size = new System.Drawing.Size(168, 53);
            this.btn_newproject.TabIndex = 1;
            this.btn_newproject.Text = "New Project";
            this.btn_newproject.UseVisualStyleBackColor = true;
            this.btn_newproject.Click += new System.EventHandler(this.btn_newproject_Click);
            // 
            // btn_loadproject
            // 
            this.btn_loadproject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loadproject.Location = new System.Drawing.Point(344, 175);
            this.btn_loadproject.Name = "btn_loadproject";
            this.btn_loadproject.Size = new System.Drawing.Size(168, 53);
            this.btn_loadproject.TabIndex = 2;
            this.btn_loadproject.Text = "Load Project";
            this.btn_loadproject.UseVisualStyleBackColor = true;
            this.btn_loadproject.Click += new System.EventHandler(this.btn_loadproject_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.Location = new System.Drawing.Point(344, 234);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(168, 53);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // lbl_legal
            // 
            this.lbl_legal.AutoSize = true;
            this.lbl_legal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_legal.Location = new System.Drawing.Point(333, 424);
            this.lbl_legal.Name = "lbl_legal";
            this.lbl_legal.Size = new System.Drawing.Size(200, 20);
            this.lbl_legal.TabIndex = 4;
            this.lbl_legal.Text = "Reo Studio 2014 - 2015";
            // 
            // MenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(871, 475);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_legal);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_loadproject);
            this.Controls.Add(this.btn_newproject);
            this.Controls.Add(this.lbl_window_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MenuScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_window_name;
        private System.Windows.Forms.Button btn_newproject;
        private System.Windows.Forms.Button btn_loadproject;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label lbl_legal;
    }
}

