namespace Heavy_Engine
{
    partial class CreatePackage
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
            this.clb_packing_files = new System.Windows.Forms.CheckedListBox();
            this.lbl_package_name = new System.Windows.Forms.Label();
            this.lbl_packing_files = new System.Windows.Forms.Label();
            this.btn_create_pacakge = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.txt_package_name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // clb_packing_files
            // 
            this.clb_packing_files.FormattingEnabled = true;
            this.clb_packing_files.Location = new System.Drawing.Point(12, 85);
            this.clb_packing_files.Name = "clb_packing_files";
            this.clb_packing_files.Size = new System.Drawing.Size(435, 304);
            this.clb_packing_files.TabIndex = 0;
            // 
            // lbl_package_name
            // 
            this.lbl_package_name.AutoSize = true;
            this.lbl_package_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_package_name.Location = new System.Drawing.Point(8, 25);
            this.lbl_package_name.Name = "lbl_package_name";
            this.lbl_package_name.Size = new System.Drawing.Size(139, 20);
            this.lbl_package_name.TabIndex = 1;
            this.lbl_package_name.Text = "Package Name :";
            // 
            // lbl_packing_files
            // 
            this.lbl_packing_files.AutoSize = true;
            this.lbl_packing_files.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_packing_files.Location = new System.Drawing.Point(12, 62);
            this.lbl_packing_files.Name = "lbl_packing_files";
            this.lbl_packing_files.Size = new System.Drawing.Size(42, 20);
            this.lbl_packing_files.TabIndex = 2;
            this.lbl_packing_files.Text = "Files";
            // 
            // btn_create_pacakge
            // 
            this.btn_create_pacakge.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create_pacakge.Location = new System.Drawing.Point(141, 395);
            this.btn_create_pacakge.Name = "btn_create_pacakge";
            this.btn_create_pacakge.Size = new System.Drawing.Size(75, 34);
            this.btn_create_pacakge.TabIndex = 3;
            this.btn_create_pacakge.Text = "Create";
            this.btn_create_pacakge.UseVisualStyleBackColor = true;
            this.btn_create_pacakge.Click += new System.EventHandler(this.btn_create_pacakge_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(222, 395);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 34);
            this.btn_close.TabIndex = 4;
            this.btn_close.Text = "Cancel";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txt_package_name
            // 
            this.txt_package_name.Location = new System.Drawing.Point(153, 25);
            this.txt_package_name.Name = "txt_package_name";
            this.txt_package_name.Size = new System.Drawing.Size(294, 20);
            this.txt_package_name.TabIndex = 5;
            // 
            // CreatePackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(467, 435);
            this.Controls.Add(this.txt_package_name);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_create_pacakge);
            this.Controls.Add(this.lbl_packing_files);
            this.Controls.Add(this.lbl_package_name);
            this.Controls.Add(this.clb_packing_files);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreatePackage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Package";
            this.Load += new System.EventHandler(this.CreatePackage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clb_packing_files;
        private System.Windows.Forms.Label lbl_package_name;
        private System.Windows.Forms.Label lbl_packing_files;
        private System.Windows.Forms.Button btn_create_pacakge;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox txt_package_name;
    }
}