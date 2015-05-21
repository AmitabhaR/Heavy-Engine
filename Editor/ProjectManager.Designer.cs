namespace Heavy_Engine
{
    partial class ProjectManager
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
            this.lbl_project_name = new System.Windows.Forms.Label();
            this.lbl_project_author = new System.Windows.Forms.Label();
            this.lbl_project_version = new System.Windows.Forms.Label();
            this.lbl_project_about = new System.Windows.Forms.Label();
            this.lbl_project_firstlevel = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.txt_project_name = new System.Windows.Forms.TextBox();
            this.txt_project_author = new System.Windows.Forms.TextBox();
            this.txt_project_version = new System.Windows.Forms.TextBox();
            this.txt_project_about = new System.Windows.Forms.TextBox();
            this.txt_project_firstlevel = new System.Windows.Forms.TextBox();
            this.btn_change = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_project_name
            // 
            this.lbl_project_name.AutoSize = true;
            this.lbl_project_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_name.Location = new System.Drawing.Point(87, 50);
            this.lbl_project_name.Name = "lbl_project_name";
            this.lbl_project_name.Size = new System.Drawing.Size(60, 20);
            this.lbl_project_name.TabIndex = 0;
            this.lbl_project_name.Text = "Name ";
            // 
            // lbl_project_author
            // 
            this.lbl_project_author.AutoSize = true;
            this.lbl_project_author.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_author.Location = new System.Drawing.Point(87, 96);
            this.lbl_project_author.Name = "lbl_project_author";
            this.lbl_project_author.Size = new System.Drawing.Size(63, 20);
            this.lbl_project_author.TabIndex = 1;
            this.lbl_project_author.Text = "Author";
            // 
            // lbl_project_version
            // 
            this.lbl_project_version.AutoSize = true;
            this.lbl_project_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_version.Location = new System.Drawing.Point(87, 140);
            this.lbl_project_version.Name = "lbl_project_version";
            this.lbl_project_version.Size = new System.Drawing.Size(70, 20);
            this.lbl_project_version.TabIndex = 2;
            this.lbl_project_version.Text = "Version";
            // 
            // lbl_project_about
            // 
            this.lbl_project_about.AutoSize = true;
            this.lbl_project_about.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_about.Location = new System.Drawing.Point(87, 192);
            this.lbl_project_about.Name = "lbl_project_about";
            this.lbl_project_about.Size = new System.Drawing.Size(57, 20);
            this.lbl_project_about.TabIndex = 3;
            this.lbl_project_about.Text = "About";
            // 
            // lbl_project_firstlevel
            // 
            this.lbl_project_firstlevel.AutoSize = true;
            this.lbl_project_firstlevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_project_firstlevel.Location = new System.Drawing.Point(87, 234);
            this.lbl_project_firstlevel.Name = "lbl_project_firstlevel";
            this.lbl_project_firstlevel.Size = new System.Drawing.Size(86, 20);
            this.lbl_project_firstlevel.TabIndex = 4;
            this.lbl_project_firstlevel.Text = "First level";
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(193, 288);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(123, 48);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(322, 288);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(123, 48);
            this.btn_back.TabIndex = 7;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // txt_project_name
            // 
            this.txt_project_name.Location = new System.Drawing.Point(193, 47);
            this.txt_project_name.Name = "txt_project_name";
            this.txt_project_name.Size = new System.Drawing.Size(252, 20);
            this.txt_project_name.TabIndex = 8;
            // 
            // txt_project_author
            // 
            this.txt_project_author.Location = new System.Drawing.Point(193, 96);
            this.txt_project_author.Name = "txt_project_author";
            this.txt_project_author.Size = new System.Drawing.Size(252, 20);
            this.txt_project_author.TabIndex = 9;
            // 
            // txt_project_version
            // 
            this.txt_project_version.Location = new System.Drawing.Point(193, 140);
            this.txt_project_version.Name = "txt_project_version";
            this.txt_project_version.Size = new System.Drawing.Size(252, 20);
            this.txt_project_version.TabIndex = 10;
            this.txt_project_version.TextChanged += new System.EventHandler(this.txt_project_version_TextChanged);
            // 
            // txt_project_about
            // 
            this.txt_project_about.Location = new System.Drawing.Point(193, 189);
            this.txt_project_about.Name = "txt_project_about";
            this.txt_project_about.Size = new System.Drawing.Size(252, 20);
            this.txt_project_about.TabIndex = 11;
            // 
            // txt_project_firstlevel
            // 
            this.txt_project_firstlevel.Enabled = false;
            this.txt_project_firstlevel.Location = new System.Drawing.Point(193, 234);
            this.txt_project_firstlevel.Name = "txt_project_firstlevel";
            this.txt_project_firstlevel.Size = new System.Drawing.Size(252, 20);
            this.txt_project_firstlevel.TabIndex = 12;
            // 
            // btn_change
            // 
            this.btn_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change.Location = new System.Drawing.Point(464, 230);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(95, 27);
            this.btn_change.TabIndex = 14;
            this.btn_change.Text = "Change...";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // ProjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(565, 354);
            this.Controls.Add(this.btn_change);
            this.Controls.Add(this.txt_project_firstlevel);
            this.Controls.Add(this.txt_project_about);
            this.Controls.Add(this.txt_project_version);
            this.Controls.Add(this.txt_project_author);
            this.Controls.Add(this.txt_project_name);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lbl_project_firstlevel);
            this.Controls.Add(this.lbl_project_about);
            this.Controls.Add(this.lbl_project_version);
            this.Controls.Add(this.lbl_project_author);
            this.Controls.Add(this.lbl_project_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProjectManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Manager";
            this.Load += new System.EventHandler(this.ProjectManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_project_name;
        private System.Windows.Forms.Label lbl_project_author;
        private System.Windows.Forms.Label lbl_project_version;
        private System.Windows.Forms.Label lbl_project_about;
        private System.Windows.Forms.Label lbl_project_firstlevel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.TextBox txt_project_name;
        private System.Windows.Forms.TextBox txt_project_author;
        private System.Windows.Forms.TextBox txt_project_version;
        private System.Windows.Forms.TextBox txt_project_about;
        private System.Windows.Forms.TextBox txt_project_firstlevel;
        private System.Windows.Forms.Button btn_change;
    }
}