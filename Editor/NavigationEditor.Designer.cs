namespace Heavy_Engine
{
    partial class NavigationEditor
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
            this.topStrip_menu = new System.Windows.Forms.MenuStrip();
            this.menuContainer_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NewNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_LoadNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_SaveNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_DeleteNavigation = new System.Windows.Forms.ToolStripMenuItem();
            this.downStrip = new System.Windows.Forms.StatusStrip();
            this.lbl_mouseX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_mouseY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_cameraX = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_cameraY = new System.Windows.Forms.ToolStripStatusLabel();
            this.baseContainer = new System.Windows.Forms.Panel();
            this.splitLayer = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.lb_points = new System.Windows.Forms.ListBox();
            this.tmr_draw = new System.Windows.Forms.Timer(this.components);
            this.mainContainer = new System.Windows.Forms.TableLayoutPanel();
            this.buttonContainer = new System.Windows.Forms.Panel();
            this.lbl_moveRight = new System.Windows.Forms.Button();
            this.btn_moveLeft = new System.Windows.Forms.Button();
            this.btn_moveDown = new System.Windows.Forms.Button();
            this.btn_moveUp = new System.Windows.Forms.Button();
            this.lbl_cameraFunctions = new System.Windows.Forms.Label();
            this.topStrip_menu.SuspendLayout();
            this.downStrip.SuspendLayout();
            this.baseContainer.SuspendLayout();
            this.splitLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.mainContainer.SuspendLayout();
            this.buttonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // topStrip_menu
            // 
            this.topStrip_menu.BackColor = System.Drawing.Color.White;
            this.topStrip_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuContainer_File,
            this.menuContainer_Edit});
            this.topStrip_menu.Location = new System.Drawing.Point(0, 0);
            this.topStrip_menu.Name = "topStrip_menu";
            this.topStrip_menu.Size = new System.Drawing.Size(688, 24);
            this.topStrip_menu.TabIndex = 0;
            this.topStrip_menu.Text = "menuStrip1";
            // 
            // menuContainer_File
            // 
            this.menuContainer_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_NewNavigation,
            this.menuItem_LoadNavigation,
            this.menuItem_SaveNavigation,
            this.menuItem_Exit});
            this.menuContainer_File.Name = "menuContainer_File";
            this.menuContainer_File.Size = new System.Drawing.Size(37, 20);
            this.menuContainer_File.Text = "File";
            // 
            // menuItem_NewNavigation
            // 
            this.menuItem_NewNavigation.Name = "menuItem_NewNavigation";
            this.menuItem_NewNavigation.Size = new System.Drawing.Size(161, 22);
            this.menuItem_NewNavigation.Text = "New Navigation";
            this.menuItem_NewNavigation.Click += new System.EventHandler(this.menuItem_NewNavigation_Click);
            // 
            // menuItem_LoadNavigation
            // 
            this.menuItem_LoadNavigation.Name = "menuItem_LoadNavigation";
            this.menuItem_LoadNavigation.Size = new System.Drawing.Size(161, 22);
            this.menuItem_LoadNavigation.Text = "Load Navigation";
            this.menuItem_LoadNavigation.Click += new System.EventHandler(this.menuItem_LoadNavigation_Click);
            // 
            // menuItem_SaveNavigation
            // 
            this.menuItem_SaveNavigation.Name = "menuItem_SaveNavigation";
            this.menuItem_SaveNavigation.Size = new System.Drawing.Size(161, 22);
            this.menuItem_SaveNavigation.Text = "Save Navigation";
            this.menuItem_SaveNavigation.Click += new System.EventHandler(this.menuItem_SaveNavigation_Click);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Name = "menuItem_Exit";
            this.menuItem_Exit.Size = new System.Drawing.Size(161, 22);
            this.menuItem_Exit.Text = "Exit Editor";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // menuContainer_Edit
            // 
            this.menuContainer_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_DeleteNavigation});
            this.menuContainer_Edit.Name = "menuContainer_Edit";
            this.menuContainer_Edit.Size = new System.Drawing.Size(39, 20);
            this.menuContainer_Edit.Text = "Edit";
            // 
            // menuItem_DeleteNavigation
            // 
            this.menuItem_DeleteNavigation.Name = "menuItem_DeleteNavigation";
            this.menuItem_DeleteNavigation.Size = new System.Drawing.Size(168, 22);
            this.menuItem_DeleteNavigation.Text = "Delete Navigation";
            this.menuItem_DeleteNavigation.Click += new System.EventHandler(this.menuItem_DeleteNavigation_Click);
            // 
            // downStrip
            // 
            this.downStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_mouseX,
            this.lbl_mouseY,
            this.lbl_cameraX,
            this.lbl_cameraY});
            this.downStrip.Location = new System.Drawing.Point(0, 510);
            this.downStrip.Name = "downStrip";
            this.downStrip.Size = new System.Drawing.Size(688, 22);
            this.downStrip.TabIndex = 1;
            this.downStrip.Text = "statusStrip1";
            // 
            // lbl_mouseX
            // 
            this.lbl_mouseX.Name = "lbl_mouseX";
            this.lbl_mouseX.Size = new System.Drawing.Size(62, 17);
            this.lbl_mouseX.Text = "Mouse X : ";
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
            // baseContainer
            // 
            this.baseContainer.Controls.Add(this.splitLayer);
            this.baseContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseContainer.Location = new System.Drawing.Point(3, 3);
            this.baseContainer.Name = "baseContainer";
            this.baseContainer.Size = new System.Drawing.Size(682, 404);
            this.baseContainer.TabIndex = 2;
            // 
            // splitLayer
            // 
            this.splitLayer.ColumnCount = 2;
            this.splitLayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.54651F));
            this.splitLayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.45349F));
            this.splitLayer.Controls.Add(this.canvas, 1, 0);
            this.splitLayer.Controls.Add(this.lb_points, 0, 0);
            this.splitLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLayer.Location = new System.Drawing.Point(0, 0);
            this.splitLayer.Name = "splitLayer";
            this.splitLayer.RowCount = 1;
            this.splitLayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.splitLayer.Size = new System.Drawing.Size(682, 404);
            this.splitLayer.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Silver;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(163, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(516, 398);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // lb_points
            // 
            this.lb_points.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_points.FormattingEnabled = true;
            this.lb_points.Location = new System.Drawing.Point(3, 3);
            this.lb_points.Name = "lb_points";
            this.lb_points.Size = new System.Drawing.Size(154, 398);
            this.lb_points.TabIndex = 1;
            this.lb_points.SelectedIndexChanged += new System.EventHandler(this.lb_points_SelectedIndexChanged);
            // 
            // tmr_draw
            // 
            this.tmr_draw.Enabled = true;
            this.tmr_draw.Interval = 1;
            // 
            // mainContainer
            // 
            this.mainContainer.ColumnCount = 1;
            this.mainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainContainer.Controls.Add(this.baseContainer, 0, 0);
            this.mainContainer.Controls.Add(this.buttonContainer, 0, 1);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 24);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.RowCount = 2;
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.49848F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.50152F));
            this.mainContainer.Size = new System.Drawing.Size(688, 486);
            this.mainContainer.TabIndex = 3;
            // 
            // buttonContainer
            // 
            this.buttonContainer.Controls.Add(this.lbl_moveRight);
            this.buttonContainer.Controls.Add(this.btn_moveLeft);
            this.buttonContainer.Controls.Add(this.btn_moveDown);
            this.buttonContainer.Controls.Add(this.btn_moveUp);
            this.buttonContainer.Controls.Add(this.lbl_cameraFunctions);
            this.buttonContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonContainer.Location = new System.Drawing.Point(3, 413);
            this.buttonContainer.Name = "buttonContainer";
            this.buttonContainer.Size = new System.Drawing.Size(682, 70);
            this.buttonContainer.TabIndex = 3;
            // 
            // lbl_moveRight
            // 
            this.lbl_moveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_moveRight.Location = new System.Drawing.Point(255, 35);
            this.lbl_moveRight.Name = "lbl_moveRight";
            this.lbl_moveRight.Size = new System.Drawing.Size(75, 23);
            this.lbl_moveRight.TabIndex = 4;
            this.lbl_moveRight.Text = "Right";
            this.lbl_moveRight.UseVisualStyleBackColor = true;
            this.lbl_moveRight.Click += new System.EventHandler(this.lbl_moveRight_Click);
            // 
            // btn_moveLeft
            // 
            this.btn_moveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_moveLeft.Location = new System.Drawing.Point(174, 35);
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
            this.btn_moveDown.Location = new System.Drawing.Point(93, 35);
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
            this.btn_moveUp.Location = new System.Drawing.Point(12, 35);
            this.btn_moveUp.Name = "btn_moveUp";
            this.btn_moveUp.Size = new System.Drawing.Size(75, 23);
            this.btn_moveUp.TabIndex = 1;
            this.btn_moveUp.Text = "Up";
            this.btn_moveUp.UseVisualStyleBackColor = true;
            this.btn_moveUp.Click += new System.EventHandler(this.btn_moveUp_Click);
            // 
            // lbl_cameraFunctions
            // 
            this.lbl_cameraFunctions.AutoSize = true;
            this.lbl_cameraFunctions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cameraFunctions.Location = new System.Drawing.Point(9, 7);
            this.lbl_cameraFunctions.Name = "lbl_cameraFunctions";
            this.lbl_cameraFunctions.Size = new System.Drawing.Size(116, 16);
            this.lbl_cameraFunctions.TabIndex = 0;
            this.lbl_cameraFunctions.Text = "Camera Functions";
            // 
            // NavigationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(688, 532);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.downStrip);
            this.Controls.Add(this.topStrip_menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.topStrip_menu;
            this.Name = "NavigationEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Navigation Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.NavigationEditor_Load);
            this.topStrip_menu.ResumeLayout(false);
            this.topStrip_menu.PerformLayout();
            this.downStrip.ResumeLayout(false);
            this.downStrip.PerformLayout();
            this.baseContainer.ResumeLayout(false);
            this.splitLayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.buttonContainer.ResumeLayout(false);
            this.buttonContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topStrip_menu;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NewNavigation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LoadNavigation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_SaveNavigation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Edit;
        private System.Windows.Forms.StatusStrip downStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbl_mouseX;
        private System.Windows.Forms.ToolStripStatusLabel lbl_mouseY;
        private System.Windows.Forms.Panel baseContainer;
        private System.Windows.Forms.TableLayoutPanel splitLayer;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.ListBox lb_points;
        private System.Windows.Forms.Timer tmr_draw;
        private System.Windows.Forms.ToolStripMenuItem menuItem_DeleteNavigation;
        private System.Windows.Forms.ToolStripStatusLabel lbl_cameraX;
        private System.Windows.Forms.ToolStripStatusLabel lbl_cameraY;
        private System.Windows.Forms.TableLayoutPanel mainContainer;
        private System.Windows.Forms.Panel buttonContainer;
        private System.Windows.Forms.Button lbl_moveRight;
        private System.Windows.Forms.Button btn_moveLeft;
        private System.Windows.Forms.Button btn_moveDown;
        private System.Windows.Forms.Button btn_moveUp;
        private System.Windows.Forms.Label lbl_cameraFunctions;
    }
}