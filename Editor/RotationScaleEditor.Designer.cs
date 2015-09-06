namespace Heavy_Engine
{
    partial class RotationScaleEditor
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
            this.lbl_rotation = new System.Windows.Forms.Label();
            this.btn_rotate_anticlockwise = new System.Windows.Forms.Button();
            this.btn_rotate_clockwise = new System.Windows.Forms.Button();
            this.lbl_scale = new System.Windows.Forms.Label();
            this.btn_scale_positive = new System.Windows.Forms.Button();
            this.btn_scale_negative = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_rotation
            // 
            this.lbl_rotation.AutoSize = true;
            this.lbl_rotation.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rotation.Location = new System.Drawing.Point(12, 21);
            this.lbl_rotation.Name = "lbl_rotation";
            this.lbl_rotation.Size = new System.Drawing.Size(71, 18);
            this.lbl_rotation.TabIndex = 0;
            this.lbl_rotation.Text = "Rotation";
            // 
            // btn_rotate_anticlockwise
            // 
            this.btn_rotate_anticlockwise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rotate_anticlockwise.Location = new System.Drawing.Point(15, 54);
            this.btn_rotate_anticlockwise.Name = "btn_rotate_anticlockwise";
            this.btn_rotate_anticlockwise.Size = new System.Drawing.Size(176, 23);
            this.btn_rotate_anticlockwise.TabIndex = 1;
            this.btn_rotate_anticlockwise.Text = "Rotate Anti-Clockwise";
            this.btn_rotate_anticlockwise.UseVisualStyleBackColor = true;
            this.btn_rotate_anticlockwise.Click += new System.EventHandler(this.btn_rotate_anticlockwise_Click);
            // 
            // btn_rotate_clockwise
            // 
            this.btn_rotate_clockwise.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rotate_clockwise.Location = new System.Drawing.Point(15, 83);
            this.btn_rotate_clockwise.Name = "btn_rotate_clockwise";
            this.btn_rotate_clockwise.Size = new System.Drawing.Size(176, 23);
            this.btn_rotate_clockwise.TabIndex = 2;
            this.btn_rotate_clockwise.Text = "Rotate Clockwise";
            this.btn_rotate_clockwise.UseVisualStyleBackColor = true;
            this.btn_rotate_clockwise.Click += new System.EventHandler(this.btn_rotate_clockwise_Click);
            // 
            // lbl_scale
            // 
            this.lbl_scale.AutoSize = true;
            this.lbl_scale.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_scale.Location = new System.Drawing.Point(12, 153);
            this.lbl_scale.Name = "lbl_scale";
            this.lbl_scale.Size = new System.Drawing.Size(47, 18);
            this.lbl_scale.TabIndex = 3;
            this.lbl_scale.Text = "Scale";
            this.lbl_scale.Click += new System.EventHandler(this.lbl_scale_Click);
            // 
            // btn_scale_positive
            // 
            this.btn_scale_positive.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_scale_positive.Location = new System.Drawing.Point(15, 185);
            this.btn_scale_positive.Name = "btn_scale_positive";
            this.btn_scale_positive.Size = new System.Drawing.Size(176, 23);
            this.btn_scale_positive.TabIndex = 4;
            this.btn_scale_positive.Text = "Scale Positive";
            this.btn_scale_positive.UseVisualStyleBackColor = true;
            this.btn_scale_positive.Click += new System.EventHandler(this.btn_scale_positive_Click);
            // 
            // btn_scale_negative
            // 
            this.btn_scale_negative.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_scale_negative.Location = new System.Drawing.Point(15, 214);
            this.btn_scale_negative.Name = "btn_scale_negative";
            this.btn_scale_negative.Size = new System.Drawing.Size(176, 23);
            this.btn_scale_negative.TabIndex = 5;
            this.btn_scale_negative.Text = "Scale Negative";
            this.btn_scale_negative.UseVisualStyleBackColor = true;
            this.btn_scale_negative.Click += new System.EventHandler(this.btn_scale_negative_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(45, 283);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(88, 33);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // RotationScaleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(207, 335);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_scale_negative);
            this.Controls.Add(this.btn_scale_positive);
            this.Controls.Add(this.lbl_scale);
            this.Controls.Add(this.btn_rotate_clockwise);
            this.Controls.Add(this.btn_rotate_anticlockwise);
            this.Controls.Add(this.lbl_rotation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RotationScaleEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rotation - Scale Editor";
            this.Load += new System.EventHandler(this.RotationScaleEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_rotation;
        private System.Windows.Forms.Button btn_rotate_anticlockwise;
        private System.Windows.Forms.Button btn_rotate_clockwise;
        private System.Windows.Forms.Label lbl_scale;
        private System.Windows.Forms.Button btn_scale_positive;
        private System.Windows.Forms.Button btn_scale_negative;
        private System.Windows.Forms.Button btn_close;
    }
}