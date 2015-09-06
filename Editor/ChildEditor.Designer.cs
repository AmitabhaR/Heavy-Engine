namespace Heavy_Engine
{
    partial class ChildEditor
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
            this.cb_objects = new System.Windows.Forms.ComboBox();
            this.lbl_objects = new System.Windows.Forms.Label();
            this.lb_childs = new System.Windows.Forms.ListBox();
            this.lbl_child_list = new System.Windows.Forms.Label();
            this.btn_add_child = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_objects
            // 
            this.cb_objects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_objects.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cb_objects.FormattingEnabled = true;
            this.cb_objects.Location = new System.Drawing.Point(27, 35);
            this.cb_objects.Name = "cb_objects";
            this.cb_objects.Size = new System.Drawing.Size(184, 21);
            this.cb_objects.TabIndex = 0;
            // 
            // lbl_objects
            // 
            this.lbl_objects.AutoSize = true;
            this.lbl_objects.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_objects.Location = new System.Drawing.Point(24, 9);
            this.lbl_objects.Name = "lbl_objects";
            this.lbl_objects.Size = new System.Drawing.Size(116, 18);
            this.lbl_objects.TabIndex = 1;
            this.lbl_objects.Text = "Game Objects";
            // 
            // lb_childs
            // 
            this.lb_childs.FormattingEnabled = true;
            this.lb_childs.Location = new System.Drawing.Point(24, 94);
            this.lb_childs.Name = "lb_childs";
            this.lb_childs.Size = new System.Drawing.Size(321, 225);
            this.lb_childs.TabIndex = 2;
            // 
            // lbl_child_list
            // 
            this.lbl_child_list.AutoSize = true;
            this.lbl_child_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_child_list.Location = new System.Drawing.Point(21, 73);
            this.lbl_child_list.Name = "lbl_child_list";
            this.lbl_child_list.Size = new System.Drawing.Size(125, 18);
            this.lbl_child_list.TabIndex = 3;
            this.lbl_child_list.Text = "Selected Childs";
            // 
            // btn_add_child
            // 
            this.btn_add_child.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_child.Location = new System.Drawing.Point(217, 35);
            this.btn_add_child.Name = "btn_add_child";
            this.btn_add_child.Size = new System.Drawing.Size(128, 23);
            this.btn_add_child.TabIndex = 4;
            this.btn_add_child.Text = "Add Child";
            this.btn_add_child.UseVisualStyleBackColor = true;
            this.btn_add_child.Click += new System.EventHandler(this.btn_add_child_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(27, 325);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(106, 23);
            this.btn_delete.TabIndex = 5;
            this.btn_delete.Text = "Delete Child";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(270, 325);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // ChildEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(357, 365);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_add_child);
            this.Controls.Add(this.lbl_child_list);
            this.Controls.Add(this.lb_childs);
            this.Controls.Add(this.lbl_objects);
            this.Controls.Add(this.cb_objects);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChildEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Child Editor";
            this.Load += new System.EventHandler(this.ChildEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_objects;
        private System.Windows.Forms.Label lbl_objects;
        private System.Windows.Forms.ListBox lb_childs;
        private System.Windows.Forms.Label lbl_child_list;
        private System.Windows.Forms.Button btn_add_child;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_close;
    }
}