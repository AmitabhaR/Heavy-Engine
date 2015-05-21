namespace Heavy_Engine
{
    partial class ObjectManager
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
            this.lbl_objects = new System.Windows.Forms.Label();
            this.lbl_scene_objects = new System.Windows.Forms.Label();
            this.lb_objects = new System.Windows.Forms.ListBox();
            this.lb_scene_objects = new System.Windows.Forms.ListBox();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_sremove = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_objects
            // 
            this.lbl_objects.AutoSize = true;
            this.lbl_objects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_objects.Location = new System.Drawing.Point(113, 25);
            this.lbl_objects.Name = "lbl_objects";
            this.lbl_objects.Size = new System.Drawing.Size(70, 20);
            this.lbl_objects.TabIndex = 0;
            this.lbl_objects.Text = "Objects";
            // 
            // lbl_scene_objects
            // 
            this.lbl_scene_objects.AutoSize = true;
            this.lbl_scene_objects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_scene_objects.Location = new System.Drawing.Point(468, 25);
            this.lbl_scene_objects.Name = "lbl_scene_objects";
            this.lbl_scene_objects.Size = new System.Drawing.Size(126, 20);
            this.lbl_scene_objects.TabIndex = 1;
            this.lbl_scene_objects.Text = "Scene Objects";
            // 
            // lb_objects
            // 
            this.lb_objects.FormattingEnabled = true;
            this.lb_objects.Location = new System.Drawing.Point(50, 77);
            this.lb_objects.Name = "lb_objects";
            this.lb_objects.Size = new System.Drawing.Size(214, 225);
            this.lb_objects.TabIndex = 2;
            // 
            // lb_scene_objects
            // 
            this.lb_scene_objects.FormattingEnabled = true;
            this.lb_scene_objects.Location = new System.Drawing.Point(421, 77);
            this.lb_scene_objects.Name = "lb_scene_objects";
            this.lb_scene_objects.Size = new System.Drawing.Size(215, 225);
            this.lb_scene_objects.TabIndex = 3;
            // 
            // btn_Remove
            // 
            this.btn_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Remove.Location = new System.Drawing.Point(50, 309);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(214, 37);
            this.btn_Remove.TabIndex = 4;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_sremove
            // 
            this.btn_sremove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sremove.Location = new System.Drawing.Point(421, 312);
            this.btn_sremove.Name = "btn_sremove";
            this.btn_sremove.Size = new System.Drawing.Size(215, 34);
            this.btn_sremove.TabIndex = 7;
            this.btn_sremove.Text = "Remove";
            this.btn_sremove.UseVisualStyleBackColor = true;
            this.btn_sremove.Click += new System.EventHandler(this.btn_sremove_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(261, 409);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(162, 34);
            this.btn_close.TabIndex = 9;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.ForeColor = System.Drawing.Color.Black;
            this.btn_refresh.Location = new System.Drawing.Point(261, 369);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(160, 34);
            this.btn_refresh.TabIndex = 10;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // ObjectManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(733, 455);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_sremove);
            this.Controls.Add(this.btn_Remove);
            this.Controls.Add(this.lb_scene_objects);
            this.Controls.Add(this.lb_objects);
            this.Controls.Add(this.lbl_scene_objects);
            this.Controls.Add(this.lbl_objects);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ObjectManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Manager";
            this.Load += new System.EventHandler(this.ObjectManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_objects;
        private System.Windows.Forms.Label lbl_scene_objects;
        private System.Windows.Forms.ListBox lb_objects;
        private System.Windows.Forms.ListBox lb_scene_objects;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_sremove;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_refresh;
    }
}