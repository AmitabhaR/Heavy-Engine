namespace Heavy_Engine
{
    partial class LoadProjectScreen
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
            this.lbl_window_title = new System.Windows.Forms.Label();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.lbl_path = new System.Windows.Forms.Label();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_browse = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.lbl_project_name = new System.Windows.Forms.Label();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_window_title
            // 
            this.lbl_window_title.AutoSize = true;
            this.lbl_window_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_window_title.Location = new System.Drawing.Point(303, 33);
            this.lbl_window_title.Name = "lbl_window_title";
            this.lbl_window_title.Size = new System.Drawing.Size(161, 29);
            this.lbl_window_title.TabIndex = 0;
            this.lbl_window_title.Text = "Load Project";
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(259, 147);
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            this.txt_path.Size = new System.Drawing.Size(338, 20);
            this.txt_path.TabIndex = 1;
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_path.Location = new System.Drawing.Point(126, 145);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(56, 20);
            this.lbl_path.TabIndex = 2;
            this.lbl_path.Text = "Path :";
            // 
            // btn_load
            // 
            this.btn_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_load.Location = new System.Drawing.Point(526, 385);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(116, 32);
            this.btn_load.TabIndex = 3;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browse.Location = new System.Drawing.Point(620, 139);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(116, 32);
            this.btn_browse.TabIndex = 4;
            this.btn_browse.Text = "Browse...";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(647, 385);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(116, 32);
            this.btn_back.TabIndex = 5;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_project_name
            // 
            this.lbl_project_name.AutoSize = true;
            this.lbl_project_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_name.Location = new System.Drawing.Point(127, 105);
            this.lbl_project_name.Name = "lbl_project_name";
            this.lbl_project_name.Size = new System.Drawing.Size(126, 20);
            this.lbl_project_name.TabIndex = 6;
            this.lbl_project_name.Text = "Project Name :";
            // 
            // txt_project_name
            // 
            this.txt_project_name.Location = new System.Drawing.Point(259, 105);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.ReadOnly = true;
            this.txt_project_name.Size = new System.Drawing.Size(338, 20);
            this.txt_project_name.TabIndex = 7;
            // 
            // LoadProjectScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(775, 429);
            this.Controls.Add(this.txt_project_name);
            this.Controls.Add(this.lbl_project_name);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.lbl_path);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.lbl_window_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadProjectScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Project";
            this.Load += new System.EventHandler(this.LoadProjectScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_window_title;
        public System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label lbl_project_name;
        public System.Windows.Forms.TextBox txt_project_name;
    }
}