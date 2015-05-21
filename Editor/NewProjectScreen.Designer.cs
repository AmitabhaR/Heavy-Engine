namespace Heavy_Engine
{
    partial class NewProjectScreen
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
            this.btn_create_project = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.lbl_project_name = new System.Windows.Forms.Label();
            this.lbl_project_path = new System.Windows.Forms.Label();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.txt_project_path = new System.Windows.Forms.TextBox();
            this.btn_change_dir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_window_name
            // 
            this.lbl_window_name.AutoSize = true;
            this.lbl_window_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_window_name.Location = new System.Drawing.Point(307, 24);
            this.lbl_window_name.Name = "lbl_window_name";
            this.lbl_window_name.Size = new System.Drawing.Size(156, 29);
            this.lbl_window_name.TabIndex = 0;
            this.lbl_window_name.Text = "New Project";
            // 
            // btn_create_project
            // 
            this.btn_create_project.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create_project.Location = new System.Drawing.Point(473, 411);
            this.btn_create_project.Name = "btn_create_project";
            this.btn_create_project.Size = new System.Drawing.Size(150, 45);
            this.btn_create_project.TabIndex = 1;
            this.btn_create_project.Text = "Create";
            this.btn_create_project.UseVisualStyleBackColor = true;
            this.btn_create_project.Click += new System.EventHandler(this.btn_create_project_Click);
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(629, 411);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(150, 45);
            this.btn_back.TabIndex = 2;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_project_name
            // 
            this.lbl_project_name.AutoSize = true;
            this.lbl_project_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_name.Location = new System.Drawing.Point(123, 98);
            this.lbl_project_name.Name = "lbl_project_name";
            this.lbl_project_name.Size = new System.Drawing.Size(126, 20);
            this.lbl_project_name.TabIndex = 3;
            this.lbl_project_name.Text = "Project Name :";
            // 
            // lbl_project_path
            // 
            this.lbl_project_path.AutoSize = true;
            this.lbl_project_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_path.Location = new System.Drawing.Point(123, 168);
            this.lbl_project_path.Name = "lbl_project_path";
            this.lbl_project_path.Size = new System.Drawing.Size(56, 20);
            this.lbl_project_path.TabIndex = 4;
            this.lbl_project_path.Text = "Path :";
            // 
            // txt_project_name
            // 
            this.txt_project_name.Location = new System.Drawing.Point(255, 100);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(270, 20);
            this.txt_project_name.TabIndex = 5;
            // 
            // txt_project_path
            // 
            this.txt_project_path.Location = new System.Drawing.Point(255, 168);
            this.txt_project_path.Name = "txt_project_path";
            this.txt_project_path.ReadOnly = true;
            this.txt_project_path.Size = new System.Drawing.Size(270, 20);
            this.txt_project_path.TabIndex = 6;
            // 
            // btn_change_dir
            // 
            this.btn_change_dir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change_dir.Location = new System.Drawing.Point(577, 155);
            this.btn_change_dir.Name = "btn_change_dir";
            this.btn_change_dir.Size = new System.Drawing.Size(106, 45);
            this.btn_change_dir.TabIndex = 7;
            this.btn_change_dir.Text = "Change...";
            this.btn_change_dir.UseVisualStyleBackColor = true;
            this.btn_change_dir.Click += new System.EventHandler(this.btn_change_dir_Click);
            // 
            // NewProjectScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(791, 468);
            this.ControlBox = false;
            this.Controls.Add(this.btn_change_dir);
            this.Controls.Add(this.txt_project_path);
            this.Controls.Add(this.txt_project_name);
            this.Controls.Add(this.lbl_project_path);
            this.Controls.Add(this.lbl_project_name);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_create_project);
            this.Controls.Add(this.lbl_window_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_window_name;
        private System.Windows.Forms.Button btn_create_project;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label lbl_project_name;
        private System.Windows.Forms.Label lbl_project_path;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.TextBox txt_project_path;
        private System.Windows.Forms.Button btn_change_dir;
    }
}