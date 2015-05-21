namespace Heavy_Engine
{
    partial class AnimationEditor
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
            this.topStrip = new System.Windows.Forms.MenuStrip();
            this.menuContainer_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NewAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_LoadAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_SaveAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_DeleteAnimation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Bake = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_SpriteBaker = new System.Windows.Forms.ToolStripMenuItem();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_PlayFrames = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_playSpeed = new System.Windows.Forms.ToolStripTextBox();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.baseContainer = new System.Windows.Forms.TableLayoutPanel();
            this.subContainer = new System.Windows.Forms.TableLayoutPanel();
            this.buttonContainer = new System.Windows.Forms.Panel();
            this.btn_moveRight = new System.Windows.Forms.Button();
            this.btn_moveLeft = new System.Windows.Forms.Button();
            this.btn_moveDown = new System.Windows.Forms.Button();
            this.btn_moveUp = new System.Windows.Forms.Button();
            this.lbl_cameraControls = new System.Windows.Forms.Label();
            this.lb_animations = new System.Windows.Forms.ListBox();
            this.tmr_draw = new System.Windows.Forms.Timer(this.components);
            this.tmr_play = new System.Windows.Forms.Timer(this.components);
            this.topStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.baseContainer.SuspendLayout();
            this.subContainer.SuspendLayout();
            this.buttonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // topStrip
            // 
            this.topStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuContainer_File,
            this.menuContainer_Edit,
            this.menuContainer_Bake,
            this.animationToolStripMenuItem});
            this.topStrip.Location = new System.Drawing.Point(0, 0);
            this.topStrip.Name = "topStrip";
            this.topStrip.Size = new System.Drawing.Size(697, 24);
            this.topStrip.TabIndex = 0;
            this.topStrip.Text = "menuStrip1";
            // 
            // menuContainer_File
            // 
            this.menuContainer_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_NewAnimation,
            this.menuItem_LoadAnimation,
            this.menuItem_SaveAnimation,
            this.menuItem_Exit});
            this.menuContainer_File.Name = "menuContainer_File";
            this.menuContainer_File.Size = new System.Drawing.Size(37, 20);
            this.menuContainer_File.Text = "File";
            // 
            // menuItem_NewAnimation
            // 
            this.menuItem_NewAnimation.Name = "menuItem_NewAnimation";
            this.menuItem_NewAnimation.Size = new System.Drawing.Size(159, 22);
            this.menuItem_NewAnimation.Text = "New Animation";
            this.menuItem_NewAnimation.Click += new System.EventHandler(this.menuItem_NewAnimation_Click);
            // 
            // menuItem_LoadAnimation
            // 
            this.menuItem_LoadAnimation.Name = "menuItem_LoadAnimation";
            this.menuItem_LoadAnimation.Size = new System.Drawing.Size(159, 22);
            this.menuItem_LoadAnimation.Text = "Load Animation";
            this.menuItem_LoadAnimation.Click += new System.EventHandler(this.menuItem_LoadAnimation_Click);
            // 
            // menuItem_SaveAnimation
            // 
            this.menuItem_SaveAnimation.Name = "menuItem_SaveAnimation";
            this.menuItem_SaveAnimation.Size = new System.Drawing.Size(159, 22);
            this.menuItem_SaveAnimation.Text = "Save Animation";
            this.menuItem_SaveAnimation.Click += new System.EventHandler(this.menuItem_SaveAnimation_Click);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Name = "menuItem_Exit";
            this.menuItem_Exit.Size = new System.Drawing.Size(159, 22);
            this.menuItem_Exit.Text = "Exit";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // menuContainer_Edit
            // 
            this.menuContainer_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_DeleteAnimation});
            this.menuContainer_Edit.Name = "menuContainer_Edit";
            this.menuContainer_Edit.Size = new System.Drawing.Size(39, 20);
            this.menuContainer_Edit.Text = "Edit";
            // 
            // menuItem_DeleteAnimation
            // 
            this.menuItem_DeleteAnimation.Name = "menuItem_DeleteAnimation";
            this.menuItem_DeleteAnimation.Size = new System.Drawing.Size(166, 22);
            this.menuItem_DeleteAnimation.Text = "Delete Animation";
            this.menuItem_DeleteAnimation.Click += new System.EventHandler(this.menuItem_DeleteAnimation_Click);
            // 
            // menuContainer_Bake
            // 
            this.menuContainer_Bake.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_SpriteBaker});
            this.menuContainer_Bake.Name = "menuContainer_Bake";
            this.menuContainer_Bake.Size = new System.Drawing.Size(44, 20);
            this.menuContainer_Bake.Text = "Bake";
            // 
            // menuItem_SpriteBaker
            // 
            this.menuItem_SpriteBaker.Name = "menuItem_SpriteBaker";
            this.menuItem_SpriteBaker.Size = new System.Drawing.Size(152, 22);
            this.menuItem_SpriteBaker.Text = "Sprite Baker";
            this.menuItem_SpriteBaker.Click += new System.EventHandler(this.menuItem_SpriteBaker_Click);
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_PlayFrames,
            this.txt_playSpeed});
            this.animationToolStripMenuItem.Name = "animationToolStripMenuItem";
            this.animationToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.animationToolStripMenuItem.Text = "Animation";
            // 
            // menuItem_PlayFrames
            // 
            this.menuItem_PlayFrames.Name = "menuItem_PlayFrames";
            this.menuItem_PlayFrames.Size = new System.Drawing.Size(160, 22);
            this.menuItem_PlayFrames.Text = "Play Frames";
            this.menuItem_PlayFrames.Click += new System.EventHandler(this.menuItem_PlayFrames_Click);
            // 
            // txt_playSpeed
            // 
            this.txt_playSpeed.Name = "txt_playSpeed";
            this.txt_playSpeed.Size = new System.Drawing.Size(100, 23);
            // 
            // canvas
            // 
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(3, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(430, 523);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            // 
            // baseContainer
            // 
            this.baseContainer.ColumnCount = 2;
            this.baseContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 442F));
            this.baseContainer.Controls.Add(this.subContainer, 1, 0);
            this.baseContainer.Controls.Add(this.lb_animations, 0, 0);
            this.baseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseContainer.Location = new System.Drawing.Point(0, 24);
            this.baseContainer.Name = "baseContainer";
            this.baseContainer.RowCount = 1;
            this.baseContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.baseContainer.Size = new System.Drawing.Size(697, 596);
            this.baseContainer.TabIndex = 2;
            // 
            // subContainer
            // 
            this.subContainer.ColumnCount = 1;
            this.subContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.subContainer.Controls.Add(this.canvas, 0, 0);
            this.subContainer.Controls.Add(this.buttonContainer, 0, 1);
            this.subContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subContainer.Location = new System.Drawing.Point(258, 3);
            this.subContainer.Name = "subContainer";
            this.subContainer.RowCount = 2;
            this.subContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.subContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.subContainer.Size = new System.Drawing.Size(436, 590);
            this.subContainer.TabIndex = 3;
            // 
            // buttonContainer
            // 
            this.buttonContainer.Controls.Add(this.btn_moveRight);
            this.buttonContainer.Controls.Add(this.btn_moveLeft);
            this.buttonContainer.Controls.Add(this.btn_moveDown);
            this.buttonContainer.Controls.Add(this.btn_moveUp);
            this.buttonContainer.Controls.Add(this.lbl_cameraControls);
            this.buttonContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonContainer.Location = new System.Drawing.Point(3, 532);
            this.buttonContainer.Name = "buttonContainer";
            this.buttonContainer.Size = new System.Drawing.Size(430, 55);
            this.buttonContainer.TabIndex = 2;
            // 
            // btn_moveRight
            // 
            this.btn_moveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_moveRight.Location = new System.Drawing.Point(258, 25);
            this.btn_moveRight.Name = "btn_moveRight";
            this.btn_moveRight.Size = new System.Drawing.Size(75, 23);
            this.btn_moveRight.TabIndex = 4;
            this.btn_moveRight.Text = "Right";
            this.btn_moveRight.UseVisualStyleBackColor = true;
            this.btn_moveRight.Click += new System.EventHandler(this.btn_moveRight_Click);
            // 
            // btn_moveLeft
            // 
            this.btn_moveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_moveLeft.Location = new System.Drawing.Point(177, 25);
            this.btn_moveLeft.Name = "btn_moveLeft";
            this.btn_moveLeft.Size = new System.Drawing.Size(75, 23);
            this.btn_moveLeft.TabIndex = 3;
            this.btn_moveLeft.Text = "Left";
            this.btn_moveLeft.UseVisualStyleBackColor = true;
            this.btn_moveLeft.Click += new System.EventHandler(this.btn_moveLeft_Click);
            // 
            // btn_moveDown
            // 
            this.btn_moveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_moveDown.Location = new System.Drawing.Point(96, 25);
            this.btn_moveDown.Name = "btn_moveDown";
            this.btn_moveDown.Size = new System.Drawing.Size(75, 23);
            this.btn_moveDown.TabIndex = 2;
            this.btn_moveDown.Text = "Down";
            this.btn_moveDown.UseVisualStyleBackColor = true;
            this.btn_moveDown.Click += new System.EventHandler(this.btn_moveDown_Click);
            // 
            // btn_moveUp
            // 
            this.btn_moveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_moveUp.Location = new System.Drawing.Point(15, 25);
            this.btn_moveUp.Name = "btn_moveUp";
            this.btn_moveUp.Size = new System.Drawing.Size(75, 23);
            this.btn_moveUp.TabIndex = 1;
            this.btn_moveUp.Text = "Up";
            this.btn_moveUp.UseVisualStyleBackColor = true;
            this.btn_moveUp.Click += new System.EventHandler(this.btn_moveUp_Click);
            // 
            // lbl_cameraControls
            // 
            this.lbl_cameraControls.AutoSize = true;
            this.lbl_cameraControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cameraControls.Location = new System.Drawing.Point(12, 6);
            this.lbl_cameraControls.Name = "lbl_cameraControls";
            this.lbl_cameraControls.Size = new System.Drawing.Size(108, 16);
            this.lbl_cameraControls.TabIndex = 0;
            this.lbl_cameraControls.Text = "Camera Controls";
            // 
            // lb_animations
            // 
            this.lb_animations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_animations.FormattingEnabled = true;
            this.lb_animations.Location = new System.Drawing.Point(3, 3);
            this.lb_animations.Name = "lb_animations";
            this.lb_animations.Size = new System.Drawing.Size(249, 590);
            this.lb_animations.TabIndex = 4;
            this.lb_animations.SelectedIndexChanged += new System.EventHandler(this.lb_animations_SelectedIndexChanged);
            // 
            // tmr_draw
            // 
            this.tmr_draw.Enabled = true;
            this.tmr_draw.Interval = 1;
            this.tmr_draw.Tick += new System.EventHandler(this.tmr_draw_Tick);
            // 
            // tmr_play
            // 
            this.tmr_play.Tick += new System.EventHandler(this.tmr_play_Tick);
            // 
            // AnimationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(697, 620);
            this.Controls.Add(this.baseContainer);
            this.Controls.Add(this.topStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.topStrip;
            this.MaximizeBox = false;
            this.Name = "AnimationEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Animation Editor";
            this.Load += new System.EventHandler(this.AnimationEditor_Load);
            this.topStrip.ResumeLayout(false);
            this.topStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.baseContainer.ResumeLayout(false);
            this.subContainer.ResumeLayout(false);
            this.buttonContainer.ResumeLayout(false);
            this.buttonContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topStrip;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NewAnimation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LoadAnimation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_SaveAnimation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Edit;
        private System.Windows.Forms.ToolStripMenuItem menuItem_DeleteAnimation;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Bake;
        private System.Windows.Forms.ToolStripMenuItem menuItem_SpriteBaker;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.TableLayoutPanel baseContainer;
        private System.Windows.Forms.TableLayoutPanel subContainer;
        private System.Windows.Forms.ListBox lb_animations;
        private System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_PlayFrames;
        private System.Windows.Forms.Panel buttonContainer;
        private System.Windows.Forms.Button btn_moveRight;
        private System.Windows.Forms.Button btn_moveLeft;
        private System.Windows.Forms.Button btn_moveDown;
        private System.Windows.Forms.Button btn_moveUp;
        private System.Windows.Forms.Label lbl_cameraControls;
        private System.Windows.Forms.ToolStripTextBox txt_playSpeed;
        private System.Windows.Forms.Timer tmr_draw;
        private System.Windows.Forms.Timer tmr_play;
    }
}