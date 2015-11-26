namespace Heavy_Engine
{
    partial class AddObject
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
            this.lbl_instance_name = new System.Windows.Forms.Label();
            this.lbl_positionX = new System.Windows.Forms.Label();
            this.lbl_positionY = new System.Windows.Forms.Label();
            this.txt_instance_name = new System.Windows.Forms.TextBox();
            this.txt_position_x = new System.Windows.Forms.TextBox();
            this.txt_position_y = new System.Windows.Forms.TextBox();
            this.lbl_object = new System.Windows.Forms.Label();
            this.lb_object = new System.Windows.Forms.ListBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.lbl_depth = new System.Windows.Forms.Label();
            this.txt_depth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_instance_name
            // 
            this.lbl_instance_name.AutoSize = true;
            this.lbl_instance_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instance_name.Location = new System.Drawing.Point(32, 33);
            this.lbl_instance_name.Name = "lbl_instance_name";
            this.lbl_instance_name.Size = new System.Drawing.Size(140, 20);
            this.lbl_instance_name.TabIndex = 0;
            this.lbl_instance_name.Text = "Instance Name :";
            // 
            // lbl_positionX
            // 
            this.lbl_positionX.AutoSize = true;
            this.lbl_positionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_positionX.Location = new System.Drawing.Point(32, 71);
            this.lbl_positionX.Name = "lbl_positionX";
            this.lbl_positionX.Size = new System.Drawing.Size(100, 20);
            this.lbl_positionX.TabIndex = 1;
            this.lbl_positionX.Text = "Position X :";
            // 
            // lbl_positionY
            // 
            this.lbl_positionY.AutoSize = true;
            this.lbl_positionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_positionY.Location = new System.Drawing.Point(32, 112);
            this.lbl_positionY.Name = "lbl_positionY";
            this.lbl_positionY.Size = new System.Drawing.Size(100, 20);
            this.lbl_positionY.TabIndex = 2;
            this.lbl_positionY.Text = "Position Y :";
            // 
            // txt_instance_name
            // 
            this.txt_instance_name.Location = new System.Drawing.Point(178, 35);
            this.txt_instance_name.Name = "txt_instance_name";
            this.txt_instance_name.Size = new System.Drawing.Size(266, 20);
            this.txt_instance_name.TabIndex = 3;
            // 
            // txt_position_x
            // 
            this.txt_position_x.Location = new System.Drawing.Point(178, 73);
            this.txt_position_x.Name = "txt_position_x";
            this.txt_position_x.Size = new System.Drawing.Size(171, 20);
            this.txt_position_x.TabIndex = 4;
            this.txt_position_x.TextChanged += new System.EventHandler(this.txt_position_x_TextChanged);
            // 
            // txt_position_y
            // 
            this.txt_position_y.Location = new System.Drawing.Point(178, 114);
            this.txt_position_y.Name = "txt_position_y";
            this.txt_position_y.Size = new System.Drawing.Size(171, 20);
            this.txt_position_y.TabIndex = 5;
            this.txt_position_y.TextChanged += new System.EventHandler(this.txt_position_y_TextChanged);
            // 
            // lbl_object
            // 
            this.lbl_object.AutoSize = true;
            this.lbl_object.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object.Location = new System.Drawing.Point(211, 226);
            this.lbl_object.Name = "lbl_object";
            this.lbl_object.Size = new System.Drawing.Size(61, 20);
            this.lbl_object.TabIndex = 6;
            this.lbl_object.Text = "Object";
            // 
            // lb_object
            // 
            this.lb_object.FormattingEnabled = true;
            this.lb_object.Location = new System.Drawing.Point(122, 263);
            this.lb_object.Name = "lb_object";
            this.lb_object.Size = new System.Drawing.Size(266, 160);
            this.lb_object.TabIndex = 7;
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(122, 440);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(119, 45);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(269, 440);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(119, 45);
            this.btn_back.TabIndex = 9;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_depth
            // 
            this.lbl_depth.AutoSize = true;
            this.lbl_depth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_depth.Location = new System.Drawing.Point(32, 161);
            this.lbl_depth.Name = "lbl_depth";
            this.lbl_depth.Size = new System.Drawing.Size(68, 20);
            this.lbl_depth.TabIndex = 10;
            this.lbl_depth.Text = "Depth :";
            // 
            // txt_depth
            // 
            this.txt_depth.Location = new System.Drawing.Point(178, 161);
            this.txt_depth.Name = "txt_depth";
            this.txt_depth.Size = new System.Drawing.Size(171, 20);
            this.txt_depth.TabIndex = 11;
            this.txt_depth.Text = "0";
            // 
            // AddObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(509, 539);
            this.Controls.Add(this.txt_depth);
            this.Controls.Add(this.lbl_depth);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.lb_object);
            this.Controls.Add(this.lbl_object);
            this.Controls.Add(this.txt_position_y);
            this.Controls.Add(this.txt_position_x);
            this.Controls.Add(this.txt_instance_name);
            this.Controls.Add(this.lbl_positionY);
            this.Controls.Add(this.lbl_positionX);
            this.Controls.Add(this.lbl_instance_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddObject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Object";
            this.Load += new System.EventHandler(this.AddObject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_instance_name;
        private System.Windows.Forms.Label lbl_positionX;
        private System.Windows.Forms.Label lbl_positionY;
        private System.Windows.Forms.TextBox txt_instance_name;
        private System.Windows.Forms.TextBox txt_position_x;
        private System.Windows.Forms.TextBox txt_position_y;
        private System.Windows.Forms.Label lbl_object;
        private System.Windows.Forms.ListBox lb_object;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label lbl_depth;
        private System.Windows.Forms.TextBox txt_depth;
    }
}