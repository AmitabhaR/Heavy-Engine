namespace Heavy_Engine
{
    partial class CreateObject
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
            this.lbl_object_name = new System.Windows.Forms.Label();
            this.lbl_object_img = new System.Windows.Forms.Label();
            this.lbl_object_text = new System.Windows.Forms.Label();
            this.lbl_object_static = new System.Windows.Forms.Label();
            this.lbl_object_physics = new System.Windows.Forms.Label();
            this.lbl_object_rigidbody = new System.Windows.Forms.Label();
            this.lbl_object_collider = new System.Windows.Forms.Label();
            this.txt_object_name = new System.Windows.Forms.TextBox();
            this.txt_object_img = new System.Windows.Forms.TextBox();
            this.txt_object_text = new System.Windows.Forms.TextBox();
            this.cb_isStatic = new System.Windows.Forms.CheckBox();
            this.cb_physics = new System.Windows.Forms.CheckBox();
            this.cb_rigidbody = new System.Windows.Forms.CheckBox();
            this.cb_collider = new System.Windows.Forms.CheckBox();
            this.btn_change = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.lbl_tag = new System.Windows.Forms.Label();
            this.txt_tag = new System.Windows.Forms.TextBox();
            this.lbl_scripts = new System.Windows.Forms.Label();
            this.lb_scripts = new System.Windows.Forms.ListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_object_name
            // 
            this.lbl_object_name.AutoSize = true;
            this.lbl_object_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_name.Location = new System.Drawing.Point(13, 33);
            this.lbl_object_name.Name = "lbl_object_name";
            this.lbl_object_name.Size = new System.Drawing.Size(55, 20);
            this.lbl_object_name.TabIndex = 0;
            this.lbl_object_name.Text = "Name";
            // 
            // lbl_object_img
            // 
            this.lbl_object_img.AutoSize = true;
            this.lbl_object_img.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_img.Location = new System.Drawing.Point(13, 78);
            this.lbl_object_img.Name = "lbl_object_img";
            this.lbl_object_img.Size = new System.Drawing.Size(59, 20);
            this.lbl_object_img.TabIndex = 1;
            this.lbl_object_img.Text = "Image";
            // 
            // lbl_object_text
            // 
            this.lbl_object_text.AutoSize = true;
            this.lbl_object_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_text.Location = new System.Drawing.Point(13, 130);
            this.lbl_object_text.Name = "lbl_object_text";
            this.lbl_object_text.Size = new System.Drawing.Size(43, 20);
            this.lbl_object_text.TabIndex = 2;
            this.lbl_object_text.Text = "Text";
            // 
            // lbl_object_static
            // 
            this.lbl_object_static.AutoSize = true;
            this.lbl_object_static.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_static.Location = new System.Drawing.Point(13, 332);
            this.lbl_object_static.Name = "lbl_object_static";
            this.lbl_object_static.Size = new System.Drawing.Size(56, 20);
            this.lbl_object_static.TabIndex = 3;
            this.lbl_object_static.Text = "Static";
            // 
            // lbl_object_physics
            // 
            this.lbl_object_physics.AutoSize = true;
            this.lbl_object_physics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_physics.Location = new System.Drawing.Point(13, 367);
            this.lbl_object_physics.Name = "lbl_object_physics";
            this.lbl_object_physics.Size = new System.Drawing.Size(69, 20);
            this.lbl_object_physics.TabIndex = 4;
            this.lbl_object_physics.Text = "Physics";
            // 
            // lbl_object_rigidbody
            // 
            this.lbl_object_rigidbody.AutoSize = true;
            this.lbl_object_rigidbody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_rigidbody.Location = new System.Drawing.Point(12, 405);
            this.lbl_object_rigidbody.Name = "lbl_object_rigidbody";
            this.lbl_object_rigidbody.Size = new System.Drawing.Size(96, 20);
            this.lbl_object_rigidbody.TabIndex = 5;
            this.lbl_object_rigidbody.Text = "Rigid-Body";
            // 
            // lbl_object_collider
            // 
            this.lbl_object_collider.AutoSize = true;
            this.lbl_object_collider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_collider.Location = new System.Drawing.Point(12, 438);
            this.lbl_object_collider.Name = "lbl_object_collider";
            this.lbl_object_collider.Size = new System.Drawing.Size(69, 20);
            this.lbl_object_collider.TabIndex = 6;
            this.lbl_object_collider.Text = "Collider";
            // 
            // txt_object_name
            // 
            this.txt_object_name.Location = new System.Drawing.Point(74, 33);
            this.txt_object_name.Name = "txt_object_name";
            this.txt_object_name.Size = new System.Drawing.Size(372, 20);
            this.txt_object_name.TabIndex = 7;
            // 
            // txt_object_img
            // 
            this.txt_object_img.Enabled = false;
            this.txt_object_img.Location = new System.Drawing.Point(74, 80);
            this.txt_object_img.Name = "txt_object_img";
            this.txt_object_img.Size = new System.Drawing.Size(278, 20);
            this.txt_object_img.TabIndex = 8;
            // 
            // txt_object_text
            // 
            this.txt_object_text.Location = new System.Drawing.Point(74, 130);
            this.txt_object_text.Name = "txt_object_text";
            this.txt_object_text.Size = new System.Drawing.Size(372, 20);
            this.txt_object_text.TabIndex = 9;
            // 
            // cb_isStatic
            // 
            this.cb_isStatic.AutoSize = true;
            this.cb_isStatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_isStatic.Location = new System.Drawing.Point(115, 332);
            this.cb_isStatic.Name = "cb_isStatic";
            this.cb_isStatic.Size = new System.Drawing.Size(15, 14);
            this.cb_isStatic.TabIndex = 10;
            this.cb_isStatic.UseVisualStyleBackColor = true;
            // 
            // cb_physics
            // 
            this.cb_physics.AutoSize = true;
            this.cb_physics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_physics.Location = new System.Drawing.Point(115, 371);
            this.cb_physics.Name = "cb_physics";
            this.cb_physics.Size = new System.Drawing.Size(15, 14);
            this.cb_physics.TabIndex = 11;
            this.cb_physics.UseVisualStyleBackColor = true;
            // 
            // cb_rigidbody
            // 
            this.cb_rigidbody.AutoSize = true;
            this.cb_rigidbody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_rigidbody.Location = new System.Drawing.Point(115, 405);
            this.cb_rigidbody.Name = "cb_rigidbody";
            this.cb_rigidbody.Size = new System.Drawing.Size(15, 14);
            this.cb_rigidbody.TabIndex = 12;
            this.cb_rigidbody.UseVisualStyleBackColor = true;
            // 
            // cb_collider
            // 
            this.cb_collider.AutoSize = true;
            this.cb_collider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_collider.Location = new System.Drawing.Point(115, 442);
            this.cb_collider.Name = "cb_collider";
            this.cb_collider.Size = new System.Drawing.Size(15, 14);
            this.cb_collider.TabIndex = 13;
            this.cb_collider.UseVisualStyleBackColor = true;
            // 
            // btn_change
            // 
            this.btn_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change.Location = new System.Drawing.Point(358, 75);
            this.btn_change.Name = "btn_change";
            this.btn_change.Size = new System.Drawing.Size(88, 28);
            this.btn_change.TabIndex = 14;
            this.btn_change.Text = "Change...";
            this.btn_change.UseVisualStyleBackColor = true;
            this.btn_change.Click += new System.EventHandler(this.btn_change_Click);
            // 
            // btn_create
            // 
            this.btn_create.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create.Location = new System.Drawing.Point(85, 478);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(134, 44);
            this.btn_create.TabIndex = 15;
            this.btn_create.Text = "Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(225, 478);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(134, 44);
            this.btn_back.TabIndex = 16;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // lbl_tag
            // 
            this.lbl_tag.AutoSize = true;
            this.lbl_tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tag.Location = new System.Drawing.Point(13, 182);
            this.lbl_tag.Name = "lbl_tag";
            this.lbl_tag.Size = new System.Drawing.Size(49, 20);
            this.lbl_tag.TabIndex = 17;
            this.lbl_tag.Text = "Tag :";
            // 
            // txt_tag
            // 
            this.txt_tag.Location = new System.Drawing.Point(74, 184);
            this.txt_tag.Name = "txt_tag";
            this.txt_tag.Size = new System.Drawing.Size(145, 20);
            this.txt_tag.TabIndex = 18;
            this.txt_tag.TextChanged += new System.EventHandler(this.txt_tag_TextChanged);
            // 
            // lbl_scripts
            // 
            this.lbl_scripts.AutoSize = true;
            this.lbl_scripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_scripts.Location = new System.Drawing.Point(12, 227);
            this.lbl_scripts.Name = "lbl_scripts";
            this.lbl_scripts.Size = new System.Drawing.Size(65, 20);
            this.lbl_scripts.TabIndex = 19;
            this.lbl_scripts.Text = "Scripts";
            // 
            // lb_scripts
            // 
            this.lb_scripts.FormattingEnabled = true;
            this.lb_scripts.Location = new System.Drawing.Point(115, 231);
            this.lb_scripts.Name = "lb_scripts";
            this.lb_scripts.Size = new System.Drawing.Size(187, 95);
            this.lb_scripts.TabIndex = 20;
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(308, 230);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(124, 39);
            this.btn_add.TabIndex = 21;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove.Location = new System.Drawing.Point(308, 289);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(124, 37);
            this.btn_remove.TabIndex = 22;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // CreateObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(458, 534);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.lb_scripts);
            this.Controls.Add(this.lbl_scripts);
            this.Controls.Add(this.txt_tag);
            this.Controls.Add(this.lbl_tag);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.btn_change);
            this.Controls.Add(this.cb_collider);
            this.Controls.Add(this.cb_rigidbody);
            this.Controls.Add(this.cb_physics);
            this.Controls.Add(this.cb_isStatic);
            this.Controls.Add(this.txt_object_text);
            this.Controls.Add(this.txt_object_img);
            this.Controls.Add(this.txt_object_name);
            this.Controls.Add(this.lbl_object_collider);
            this.Controls.Add(this.lbl_object_rigidbody);
            this.Controls.Add(this.lbl_object_physics);
            this.Controls.Add(this.lbl_object_static);
            this.Controls.Add(this.lbl_object_text);
            this.Controls.Add(this.lbl_object_img);
            this.Controls.Add(this.lbl_object_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreateObject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Object";
            this.Load += new System.EventHandler(this.CreateObject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_object_name;
        private System.Windows.Forms.Label lbl_object_img;
        private System.Windows.Forms.Label lbl_object_text;
        private System.Windows.Forms.Label lbl_object_static;
        private System.Windows.Forms.Label lbl_object_physics;
        private System.Windows.Forms.Label lbl_object_rigidbody;
        private System.Windows.Forms.Label lbl_object_collider;
        private System.Windows.Forms.TextBox txt_object_name;
        private System.Windows.Forms.TextBox txt_object_img;
        private System.Windows.Forms.TextBox txt_object_text;
        private System.Windows.Forms.CheckBox cb_isStatic;
        private System.Windows.Forms.CheckBox cb_physics;
        private System.Windows.Forms.CheckBox cb_rigidbody;
        private System.Windows.Forms.CheckBox cb_collider;
        private System.Windows.Forms.Button btn_change;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label lbl_tag;
        private System.Windows.Forms.TextBox txt_tag;
        private System.Windows.Forms.Label lbl_scripts;
        private System.Windows.Forms.ListBox lb_scripts;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_remove;
    }
}