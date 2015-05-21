namespace Heavy_Engine
{
    partial class ObjectEditor
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
            this.lb_objects = new System.Windows.Forms.ListBox();
            this.lbl_object_name = new System.Windows.Forms.Label();
            this.lbl_object_image = new System.Windows.Forms.Label();
            this.lbl_object_text = new System.Windows.Forms.Label();
            this.lbl_object_tag = new System.Windows.Forms.Label();
            this.lbl_object_scripts = new System.Windows.Forms.Label();
            this.lbl_object_static = new System.Windows.Forms.Label();
            this.lbl_object_physics = new System.Windows.Forms.Label();
            this.lbl_object_rigidbody = new System.Windows.Forms.Label();
            this.lbl_object_collider = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_image = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_text = new System.Windows.Forms.TextBox();
            this.txt_tag = new System.Windows.Forms.TextBox();
            this.lb_scripts = new System.Windows.Forms.ListBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.cb_static = new System.Windows.Forms.CheckBox();
            this.cb_physics = new System.Windows.Forms.CheckBox();
            this.cb_rigid_body = new System.Windows.Forms.CheckBox();
            this.cb_collider = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_objects
            // 
            this.lb_objects.FormattingEnabled = true;
            this.lb_objects.Location = new System.Drawing.Point(12, 12);
            this.lb_objects.Name = "lb_objects";
            this.lb_objects.Size = new System.Drawing.Size(417, 264);
            this.lb_objects.TabIndex = 0;
            this.lb_objects.SelectedIndexChanged += new System.EventHandler(this.lb_objects_SelectedIndexChanged);
            // 
            // lbl_object_name
            // 
            this.lbl_object_name.AutoSize = true;
            this.lbl_object_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_name.Location = new System.Drawing.Point(22, 290);
            this.lbl_object_name.Name = "lbl_object_name";
            this.lbl_object_name.Size = new System.Drawing.Size(59, 20);
            this.lbl_object_name.TabIndex = 1;
            this.lbl_object_name.Text = "Name :";
            // 
            // lbl_object_image
            // 
            this.lbl_object_image.AutoSize = true;
            this.lbl_object_image.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_image.Location = new System.Drawing.Point(22, 317);
            this.lbl_object_image.Name = "lbl_object_image";
            this.lbl_object_image.Size = new System.Drawing.Size(62, 20);
            this.lbl_object_image.TabIndex = 2;
            this.lbl_object_image.Text = "Image :";
            // 
            // lbl_object_text
            // 
            this.lbl_object_text.AutoSize = true;
            this.lbl_object_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_text.Location = new System.Drawing.Point(22, 346);
            this.lbl_object_text.Name = "lbl_object_text";
            this.lbl_object_text.Size = new System.Drawing.Size(47, 20);
            this.lbl_object_text.TabIndex = 3;
            this.lbl_object_text.Text = "Text :";
            // 
            // lbl_object_tag
            // 
            this.lbl_object_tag.AutoSize = true;
            this.lbl_object_tag.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_tag.Location = new System.Drawing.Point(22, 376);
            this.lbl_object_tag.Name = "lbl_object_tag";
            this.lbl_object_tag.Size = new System.Drawing.Size(44, 20);
            this.lbl_object_tag.TabIndex = 4;
            this.lbl_object_tag.Text = "Tag :";
            // 
            // lbl_object_scripts
            // 
            this.lbl_object_scripts.AutoSize = true;
            this.lbl_object_scripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_scripts.Location = new System.Drawing.Point(22, 410);
            this.lbl_object_scripts.Name = "lbl_object_scripts";
            this.lbl_object_scripts.Size = new System.Drawing.Size(58, 20);
            this.lbl_object_scripts.TabIndex = 5;
            this.lbl_object_scripts.Text = "Script :";
            // 
            // lbl_object_static
            // 
            this.lbl_object_static.AutoSize = true;
            this.lbl_object_static.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_static.Location = new System.Drawing.Point(23, 498);
            this.lbl_object_static.Name = "lbl_object_static";
            this.lbl_object_static.Size = new System.Drawing.Size(58, 20);
            this.lbl_object_static.TabIndex = 6;
            this.lbl_object_static.Text = "Static :";
            // 
            // lbl_object_physics
            // 
            this.lbl_object_physics.AutoSize = true;
            this.lbl_object_physics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_physics.Location = new System.Drawing.Point(23, 534);
            this.lbl_object_physics.Name = "lbl_object_physics";
            this.lbl_object_physics.Size = new System.Drawing.Size(70, 20);
            this.lbl_object_physics.TabIndex = 7;
            this.lbl_object_physics.Text = "Physics :";
            // 
            // lbl_object_rigidbody
            // 
            this.lbl_object_rigidbody.AutoSize = true;
            this.lbl_object_rigidbody.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_rigidbody.Location = new System.Drawing.Point(22, 569);
            this.lbl_object_rigidbody.Name = "lbl_object_rigidbody";
            this.lbl_object_rigidbody.Size = new System.Drawing.Size(93, 20);
            this.lbl_object_rigidbody.TabIndex = 8;
            this.lbl_object_rigidbody.Text = "Rigid Body :";
            // 
            // lbl_object_collider
            // 
            this.lbl_object_collider.AutoSize = true;
            this.lbl_object_collider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_object_collider.Location = new System.Drawing.Point(23, 600);
            this.lbl_object_collider.Name = "lbl_object_collider";
            this.lbl_object_collider.Size = new System.Drawing.Size(69, 20);
            this.lbl_object_collider.TabIndex = 9;
            this.lbl_object_collider.Text = "Collider :";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(87, 292);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(342, 20);
            this.txt_name.TabIndex = 10;
            // 
            // txt_image
            // 
            this.txt_image.Location = new System.Drawing.Point(87, 319);
            this.txt_image.Name = "txt_image";
            this.txt_image.ReadOnly = true;
            this.txt_image.Size = new System.Drawing.Size(261, 20);
            this.txt_image.TabIndex = 11;
            // 
            // btn_browse
            // 
            this.btn_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browse.Location = new System.Drawing.Point(354, 317);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(75, 25);
            this.btn_browse.TabIndex = 12;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_text
            // 
            this.txt_text.Location = new System.Drawing.Point(87, 348);
            this.txt_text.Name = "txt_text";
            this.txt_text.Size = new System.Drawing.Size(342, 20);
            this.txt_text.TabIndex = 13;
            // 
            // txt_tag
            // 
            this.txt_tag.Location = new System.Drawing.Point(87, 374);
            this.txt_tag.Name = "txt_tag";
            this.txt_tag.Size = new System.Drawing.Size(261, 20);
            this.txt_tag.TabIndex = 14;
            // 
            // lb_scripts
            // 
            this.lb_scripts.FormattingEnabled = true;
            this.lb_scripts.Location = new System.Drawing.Point(86, 413);
            this.lb_scripts.Name = "lb_scripts";
            this.lb_scripts.ScrollAlwaysVisible = true;
            this.lb_scripts.Size = new System.Drawing.Size(232, 69);
            this.lb_scripts.TabIndex = 15;
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(333, 413);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(94, 29);
            this.btn_add.TabIndex = 16;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_remove.Location = new System.Drawing.Point(333, 448);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(94, 29);
            this.btn_remove.TabIndex = 17;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // cb_static
            // 
            this.cb_static.AutoSize = true;
            this.cb_static.Location = new System.Drawing.Point(121, 502);
            this.cb_static.Name = "cb_static";
            this.cb_static.Size = new System.Drawing.Size(15, 14);
            this.cb_static.TabIndex = 18;
            this.cb_static.UseVisualStyleBackColor = true;
            // 
            // cb_physics
            // 
            this.cb_physics.AutoSize = true;
            this.cb_physics.Location = new System.Drawing.Point(121, 537);
            this.cb_physics.Name = "cb_physics";
            this.cb_physics.Size = new System.Drawing.Size(15, 14);
            this.cb_physics.TabIndex = 19;
            this.cb_physics.UseVisualStyleBackColor = true;
            // 
            // cb_rigid_body
            // 
            this.cb_rigid_body.AutoSize = true;
            this.cb_rigid_body.Location = new System.Drawing.Point(121, 573);
            this.cb_rigid_body.Name = "cb_rigid_body";
            this.cb_rigid_body.Size = new System.Drawing.Size(15, 14);
            this.cb_rigid_body.TabIndex = 20;
            this.cb_rigid_body.UseVisualStyleBackColor = true;
            // 
            // cb_collider
            // 
            this.cb_collider.AutoSize = true;
            this.cb_collider.Location = new System.Drawing.Point(121, 604);
            this.cb_collider.Name = "cb_collider";
            this.cb_collider.Size = new System.Drawing.Size(15, 14);
            this.cb_collider.TabIndex = 21;
            this.cb_collider.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(333, 565);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(94, 29);
            this.btn_save.TabIndex = 22;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(333, 600);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(94, 29);
            this.btn_cancel.TabIndex = 23;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(446, 646);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.cb_collider);
            this.Controls.Add(this.cb_rigid_body);
            this.Controls.Add(this.cb_physics);
            this.Controls.Add(this.cb_static);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.lb_scripts);
            this.Controls.Add(this.txt_tag);
            this.Controls.Add(this.txt_text);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.txt_image);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.lbl_object_collider);
            this.Controls.Add(this.lbl_object_rigidbody);
            this.Controls.Add(this.lbl_object_physics);
            this.Controls.Add(this.lbl_object_static);
            this.Controls.Add(this.lbl_object_scripts);
            this.Controls.Add(this.lbl_object_tag);
            this.Controls.Add(this.lbl_object_text);
            this.Controls.Add(this.lbl_object_image);
            this.Controls.Add(this.lbl_object_name);
            this.Controls.Add(this.lb_objects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ObjectEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Editor";
            this.Load += new System.EventHandler(this.ObjectEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_objects;
        private System.Windows.Forms.Label lbl_object_name;
        private System.Windows.Forms.Label lbl_object_image;
        private System.Windows.Forms.Label lbl_object_text;
        private System.Windows.Forms.Label lbl_object_tag;
        private System.Windows.Forms.Label lbl_object_scripts;
        private System.Windows.Forms.Label lbl_object_static;
        private System.Windows.Forms.Label lbl_object_physics;
        private System.Windows.Forms.Label lbl_object_rigidbody;
        private System.Windows.Forms.Label lbl_object_collider;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_image;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_text;
        private System.Windows.Forms.TextBox txt_tag;
        private System.Windows.Forms.ListBox lb_scripts;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.CheckBox cb_static;
        private System.Windows.Forms.CheckBox cb_physics;
        private System.Windows.Forms.CheckBox cb_rigid_body;
        private System.Windows.Forms.CheckBox cb_collider;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
    }
}