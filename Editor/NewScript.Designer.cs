namespace Heavy_Engine
{
    partial class NewScript
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
            this.lbl_script_name = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.txt_script_name = new System.Windows.Forms.TextBox();
            this.cb_bos_script = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbl_script_name
            // 
            this.lbl_script_name.AutoSize = true;
            this.lbl_script_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_script_name.Location = new System.Drawing.Point(12, 30);
            this.lbl_script_name.Name = "lbl_script_name";
            this.lbl_script_name.Size = new System.Drawing.Size(117, 20);
            this.lbl_script_name.TabIndex = 0;
            this.lbl_script_name.Text = "Script Name :";
            // 
            // btn_create
            // 
            this.btn_create.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create.Location = new System.Drawing.Point(16, 79);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(109, 29);
            this.btn_create.TabIndex = 1;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(135, 79);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(109, 29);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // txt_script_name
            // 
            this.txt_script_name.Location = new System.Drawing.Point(16, 53);
            this.txt_script_name.Name = "txt_script_name";
            this.txt_script_name.Size = new System.Drawing.Size(431, 20);
            this.txt_script_name.TabIndex = 3;
            this.txt_script_name.TextChanged += new System.EventHandler(this.txt_script_name_TextChanged);
            // 
            // cb_bos_script
            // 
            this.cb_bos_script.AutoSize = true;
            this.cb_bos_script.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_bos_script.Location = new System.Drawing.Point(336, 79);
            this.cb_bos_script.Name = "cb_bos_script";
            this.cb_bos_script.Size = new System.Drawing.Size(111, 24);
            this.cb_bos_script.TabIndex = 4;
            this.cb_bos_script.Text = "Bos Script";
            this.cb_bos_script.UseVisualStyleBackColor = true;
            // 
            // NewScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(459, 121);
            this.Controls.Add(this.cb_bos_script);
            this.Controls.Add(this.txt_script_name);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.lbl_script_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewScript";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Script";
            this.Load += new System.EventHandler(this.NewScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_script_name;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox txt_script_name;
        private System.Windows.Forms.CheckBox cb_bos_script;

    }
}