namespace Heavy_Engine
{
    partial class TileMapEditor
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
            this.components = new System.ComponentModel.Container();
            this.gb_options = new System.Windows.Forms.GroupBox();
            this.cb_object_box = new System.Windows.Forms.ComboBox();
            this.btn_right = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.lbl_editor_movement = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_tile_height = new System.Windows.Forms.Label();
            this.txt_tile_height = new System.Windows.Forms.TextBox();
            this.txt_tile_width = new System.Windows.Forms.TextBox();
            this.lbl_tile_width = new System.Windows.Forms.Label();
            this.lbl_tile_source = new System.Windows.Forms.Label();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.baseContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tmr_draw = new System.Windows.Forms.Timer(this.components);
            this.bottomStrip = new System.Windows.Forms.StatusStrip();
            this.lbl_mouseX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_mouseY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_cameraX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_cameraY = new System.Windows.Forms.ToolStripStatusLabel();
            this.gb_options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.baseContainer.SuspendLayout();
            this.bottomStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_options
            // 
            this.gb_options.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.gb_options.Controls.Add(this.cb_object_box);
            this.gb_options.Controls.Add(this.btn_right);
            this.gb_options.Controls.Add(this.btn_left);
            this.gb_options.Controls.Add(this.btn_down);
            this.gb_options.Controls.Add(this.btn_up);
            this.gb_options.Controls.Add(this.lbl_editor_movement);
            this.gb_options.Controls.Add(this.btn_save);
            this.gb_options.Controls.Add(this.btn_close);
            this.gb_options.Controls.Add(this.lbl_tile_height);
            this.gb_options.Controls.Add(this.txt_tile_height);
            this.gb_options.Controls.Add(this.txt_tile_width);
            this.gb_options.Controls.Add(this.lbl_tile_width);
            this.gb_options.Controls.Add(this.lbl_tile_source);
            this.gb_options.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_options.Location = new System.Drawing.Point(855, 3);
            this.gb_options.Name = "gb_options";
            this.gb_options.Size = new System.Drawing.Size(182, 623);
            this.gb_options.TabIndex = 0;
            this.gb_options.TabStop = false;
            this.gb_options.Text = "Options";
            // 
            // cb_object_box
            // 
            this.cb_object_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_object_box.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_object_box.FormattingEnabled = true;
            this.cb_object_box.Location = new System.Drawing.Point(9, 52);
            this.cb_object_box.Name = "cb_object_box";
            this.cb_object_box.Size = new System.Drawing.Size(138, 21);
            this.cb_object_box.TabIndex = 14;
            this.cb_object_box.SelectedIndexChanged += new System.EventHandler(this.cb_object_box_SelectedIndexChanged);
            // 
            // btn_right
            // 
            this.btn_right.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_right.Location = new System.Drawing.Point(43, 458);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(75, 23);
            this.btn_right.TabIndex = 13;
            this.btn_right.Text = "Right";
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // btn_left
            // 
            this.btn_left.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_left.Location = new System.Drawing.Point(43, 429);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(75, 23);
            this.btn_left.TabIndex = 12;
            this.btn_left.Text = "Left";
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_down
            // 
            this.btn_down.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_down.Location = new System.Drawing.Point(43, 400);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(75, 23);
            this.btn_down.TabIndex = 11;
            this.btn_down.Text = "Down";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_up.Location = new System.Drawing.Point(43, 371);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(75, 23);
            this.btn_up.TabIndex = 10;
            this.btn_up.Text = "Up";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // lbl_editor_movement
            // 
            this.lbl_editor_movement.AutoSize = true;
            this.lbl_editor_movement.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_editor_movement.Location = new System.Drawing.Point(28, 342);
            this.lbl_editor_movement.Name = "lbl_editor_movement";
            this.lbl_editor_movement.Size = new System.Drawing.Size(119, 16);
            this.lbl_editor_movement.TabIndex = 9;
            this.lbl_editor_movement.Text = "Editor Movement";
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(6, 538);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(160, 31);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_close
            // 
            this.btn_close.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(6, 575);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(160, 28);
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lbl_tile_height
            // 
            this.lbl_tile_height.AutoSize = true;
            this.lbl_tile_height.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tile_height.Location = new System.Drawing.Point(6, 210);
            this.lbl_tile_height.Name = "lbl_tile_height";
            this.lbl_tile_height.Size = new System.Drawing.Size(50, 16);
            this.lbl_tile_height.TabIndex = 6;
            this.lbl_tile_height.Text = "Height";
            // 
            // txt_tile_height
            // 
            this.txt_tile_height.Location = new System.Drawing.Point(9, 229);
            this.txt_tile_height.Name = "txt_tile_height";
            this.txt_tile_height.ReadOnly = true;
            this.txt_tile_height.Size = new System.Drawing.Size(97, 20);
            this.txt_tile_height.TabIndex = 5;
            // 
            // txt_tile_width
            // 
            this.txt_tile_width.Location = new System.Drawing.Point(9, 138);
            this.txt_tile_width.Name = "txt_tile_width";
            this.txt_tile_width.ReadOnly = true;
            this.txt_tile_width.Size = new System.Drawing.Size(97, 20);
            this.txt_tile_width.TabIndex = 4;
            // 
            // lbl_tile_width
            // 
            this.lbl_tile_width.AutoSize = true;
            this.lbl_tile_width.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tile_width.Location = new System.Drawing.Point(6, 119);
            this.lbl_tile_width.Name = "lbl_tile_width";
            this.lbl_tile_width.Size = new System.Drawing.Size(46, 16);
            this.lbl_tile_width.TabIndex = 3;
            this.lbl_tile_width.Text = "Width";
            // 
            // lbl_tile_source
            // 
            this.lbl_tile_source.AutoSize = true;
            this.lbl_tile_source.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tile_source.Location = new System.Drawing.Point(6, 33);
            this.lbl_tile_source.Name = "lbl_tile_source";
            this.lbl_tile_source.Size = new System.Drawing.Size(82, 16);
            this.lbl_tile_source.TabIndex = 0;
            this.lbl_tile_source.Text = "Tile Source";
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(3, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(846, 623);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            // 
            // baseContainer
            // 
            this.baseContainer.ColumnCount = 2;
            this.baseContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.92308F));
            this.baseContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.07692F));
            this.baseContainer.Controls.Add(this.canvas, 0, 0);
            this.baseContainer.Controls.Add(this.gb_options, 1, 0);
            this.baseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseContainer.Location = new System.Drawing.Point(0, 0);
            this.baseContainer.Name = "baseContainer";
            this.baseContainer.RowCount = 1;
            this.baseContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseContainer.Size = new System.Drawing.Size(1040, 629);
            this.baseContainer.TabIndex = 2;
            // 
            // tmr_draw
            // 
            this.tmr_draw.Enabled = true;
            this.tmr_draw.Interval = 1;
            this.tmr_draw.Tick += new System.EventHandler(this.tmr_draw_Tick);
            // 
            // bottomStrip
            // 
            this.bottomStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_mouseX,
            this.lbl_mouseY,
            this.lbl_cameraX,
            this.lbl_cameraY});
            this.bottomStrip.Location = new System.Drawing.Point(0, 607);
            this.bottomStrip.Name = "bottomStrip";
            this.bottomStrip.Size = new System.Drawing.Size(1040, 22);
            this.bottomStrip.TabIndex = 3;
            this.bottomStrip.Text = "statusStrip1";
            // 
            // lbl_mouseX
            // 
            this.lbl_mouseX.Name = "lbl_mouseX";
            this.lbl_mouseX.Size = new System.Drawing.Size(59, 17);
            this.lbl_mouseX.Text = "Mouse X :";
            // 
            // lbl_mouseY
            // 
            this.lbl_mouseY.Name = "lbl_mouseY";
            this.lbl_mouseY.Size = new System.Drawing.Size(59, 17);
            this.lbl_mouseY.Text = "Mouse Y :";
            // 
            // lbl_cameraX
            // 
            this.lbl_cameraX.Name = "lbl_cameraX";
            this.lbl_cameraX.Size = new System.Drawing.Size(64, 17);
            this.lbl_cameraX.Text = "Camera X :";
            // 
            // lbl_cameraY
            // 
            this.lbl_cameraY.Name = "lbl_cameraY";
            this.lbl_cameraY.Size = new System.Drawing.Size(64, 17);
            this.lbl_cameraY.Text = "Camera Y :";
            // 
            // TileMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 629);
            this.Controls.Add(this.bottomStrip);
            this.Controls.Add(this.baseContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TileMapEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TileMap Editor";
            this.Load += new System.EventHandler(this.TileMapEditor_Load);
            this.gb_options.ResumeLayout(false);
            this.gb_options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.baseContainer.ResumeLayout(false);
            this.bottomStrip.ResumeLayout(false);
            this.bottomStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_options;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.TableLayoutPanel baseContainer;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label lbl_tile_height;
        private System.Windows.Forms.TextBox txt_tile_height;
        private System.Windows.Forms.TextBox txt_tile_width;
        private System.Windows.Forms.Label lbl_tile_width;
        private System.Windows.Forms.Label lbl_tile_source;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Label lbl_editor_movement;
        private System.Windows.Forms.Timer tmr_draw;
        private System.Windows.Forms.ComboBox cb_object_box;
        private System.Windows.Forms.StatusStrip bottomStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbl_mouseX;
        private System.Windows.Forms.ToolStripStatusLabel lbl_mouseY;
        private System.Windows.Forms.ToolStripStatusLabel lbl_cameraX;
        private System.Windows.Forms.ToolStripStatusLabel lbl_cameraY;
    }
}