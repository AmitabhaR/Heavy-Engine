namespace Heavy_Engine
{
    partial class SpriteBaker
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
            this.lbl_imagePath = new System.Windows.Forms.Label();
            this.lbl_Width = new System.Windows.Forms.Label();
            this.btn_Height = new System.Windows.Forms.Label();
            this.btn_Bake = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.txt_image = new System.Windows.Forms.TextBox();
            this.txt_width = new System.Windows.Forms.TextBox();
            this.txt_height = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_imagePath
            // 
            this.lbl_imagePath.AutoSize = true;
            this.lbl_imagePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_imagePath.Location = new System.Drawing.Point(23, 50);
            this.lbl_imagePath.Name = "lbl_imagePath";
            this.lbl_imagePath.Size = new System.Drawing.Size(56, 18);
            this.lbl_imagePath.TabIndex = 0;
            this.lbl_imagePath.Text = "Image :";
            // 
            // lbl_Width
            // 
            this.lbl_Width.AutoSize = true;
            this.lbl_Width.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Width.Location = new System.Drawing.Point(23, 96);
            this.lbl_Width.Name = "lbl_Width";
            this.lbl_Width.Size = new System.Drawing.Size(54, 18);
            this.lbl_Width.TabIndex = 1;
            this.lbl_Width.Text = "Width :";
            // 
            // btn_Height
            // 
            this.btn_Height.AutoSize = true;
            this.btn_Height.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Height.Location = new System.Drawing.Point(23, 148);
            this.btn_Height.Name = "btn_Height";
            this.btn_Height.Size = new System.Drawing.Size(58, 18);
            this.btn_Height.TabIndex = 2;
            this.btn_Height.Text = "Height :";
            // 
            // btn_Bake
            // 
            this.btn_Bake.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Bake.Location = new System.Drawing.Point(85, 224);
            this.btn_Bake.Name = "btn_Bake";
            this.btn_Bake.Size = new System.Drawing.Size(141, 34);
            this.btn_Bake.TabIndex = 4;
            this.btn_Bake.Text = "Bake";
            this.btn_Bake.UseVisualStyleBackColor = true;
            this.btn_Bake.Click += new System.EventHandler(this.btn_Bake_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(232, 224);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(141, 34);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // txt_image
            // 
            this.txt_image.Location = new System.Drawing.Point(85, 50);
            this.txt_image.Name = "txt_image";
            this.txt_image.ReadOnly = true;
            this.txt_image.Size = new System.Drawing.Size(260, 20);
            this.txt_image.TabIndex = 6;
            // 
            // txt_width
            // 
            this.txt_width.Location = new System.Drawing.Point(85, 96);
            this.txt_width.Name = "txt_width";
            this.txt_width.Size = new System.Drawing.Size(260, 20);
            this.txt_width.TabIndex = 7;
            this.txt_width.TextChanged += new System.EventHandler(this.txt_width_TextChanged);
            // 
            // txt_height
            // 
            this.txt_height.Location = new System.Drawing.Point(83, 148);
            this.txt_height.Name = "txt_height";
            this.txt_height.Size = new System.Drawing.Size(260, 20);
            this.txt_height.TabIndex = 8;
            this.txt_height.TextChanged += new System.EventHandler(this.txt_height_TextChanged);
            // 
            // btn_browse
            // 
            this.btn_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browse.Location = new System.Drawing.Point(370, 49);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(107, 23);
            this.btn_browse.TabIndex = 9;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // SpriteBaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(489, 310);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.txt_height);
            this.Controls.Add(this.txt_width);
            this.Controls.Add(this.txt_image);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Bake);
            this.Controls.Add(this.btn_Height);
            this.Controls.Add(this.lbl_Width);
            this.Controls.Add(this.lbl_imagePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SpriteBaker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprite Baker";
            this.Load += new System.EventHandler(this.SpriteBaker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_imagePath;
        private System.Windows.Forms.Label lbl_Width;
        private System.Windows.Forms.Label btn_Height;
        private System.Windows.Forms.Button btn_Bake;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox txt_image;
        private System.Windows.Forms.TextBox txt_width;
        private System.Windows.Forms.TextBox txt_height;
        private System.Windows.Forms.Button btn_browse;
    }
}