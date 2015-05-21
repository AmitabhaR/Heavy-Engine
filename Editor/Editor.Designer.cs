namespace Heavy_Engine
{
    public partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.canvas = new System.Windows.Forms.PictureBox();
            this.gameObject_editor = new System.Windows.Forms.PropertyGrid();
            this.bottom_dataview = new System.Windows.Forms.StatusStrip();
            this.mouse_positionX = new System.Windows.Forms.ToolStripStatusLabel();
            this.mouse_positionY = new System.Windows.Forms.ToolStripStatusLabel();
            this.top_menu_container = new System.Windows.Forms.MenuStrip();
            this.menuContainer_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_LoadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_SaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_NewLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_LoadLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_SaveLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_BackToMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_LevelManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ProjectManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Object = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_CreateObject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_AddObject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ObjectManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ObjectEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Resource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ImportResource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ResouceManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Navigation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_OpenEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Animation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_AOpenEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Scripts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_NewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ScriptManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_open_editor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_plugins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ImportPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_PluginsManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ImportHeader = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Project = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_RunProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_BuildProject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_BuildOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContainer_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_AboutHeavyEngine = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr_draw = new System.Windows.Forms.Timer(this.components);
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_right = new System.Windows.Forms.Button();
            this.lbl_editor_movement = new System.Windows.Forms.Label();
            this.lbl_zoom = new System.Windows.Forms.Label();
            this.btn_inside = new System.Windows.Forms.Button();
            this.btn_outside = new System.Windows.Forms.Button();
            this.lbl_view_x = new System.Windows.Forms.Label();
            this.lbl_view_y = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.contpane_base = new System.Windows.Forms.TableLayoutPanel();
            this.contpane_tools = new System.Windows.Forms.Panel();
            this.contpane_tabs = new System.Windows.Forms.TableLayoutPanel();
            this.contpane_camerazoom = new System.Windows.Forms.Panel();
            this.contpane_movement = new System.Windows.Forms.Panel();
            this.contpane_canvas = new System.Windows.Forms.Panel();
            this.contpane_buttons = new System.Windows.Forms.Panel();
            this.base_container = new System.Windows.Forms.SplitContainer();
            this.list_div = new System.Windows.Forms.TableLayoutPanel();
            this.file_tree = new System.Windows.Forms.TreeView();
            this.lb_objects = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.bottom_dataview.SuspendLayout();
            this.top_menu_container.SuspendLayout();
            this.contpane_base.SuspendLayout();
            this.contpane_tools.SuspendLayout();
            this.contpane_tabs.SuspendLayout();
            this.contpane_camerazoom.SuspendLayout();
            this.contpane_movement.SuspendLayout();
            this.contpane_canvas.SuspendLayout();
            this.contpane_buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.base_container)).BeginInit();
            this.base_container.Panel1.SuspendLayout();
            this.base_container.Panel2.SuspendLayout();
            this.base_container.SuspendLayout();
            this.list_div.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1046, 474);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Click += new System.EventHandler(this.canvas_Click);
            // 
            // gameObject_editor
            // 
            this.gameObject_editor.BackColor = System.Drawing.Color.Gray;
            this.gameObject_editor.CommandsBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gameObject_editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObject_editor.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gameObject_editor.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gameObject_editor.Location = new System.Drawing.Point(1055, 3);
            this.gameObject_editor.Name = "gameObject_editor";
            this.gameObject_editor.Size = new System.Drawing.Size(187, 474);
            this.gameObject_editor.TabIndex = 1;
            this.gameObject_editor.ViewBackColor = System.Drawing.Color.Gray;
            this.gameObject_editor.Click += new System.EventHandler(this.gameObject_editor_Click);
            // 
            // bottom_dataview
            // 
            this.bottom_dataview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bottom_dataview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouse_positionX,
            this.mouse_positionY});
            this.bottom_dataview.Location = new System.Drawing.Point(0, 589);
            this.bottom_dataview.Name = "bottom_dataview";
            this.bottom_dataview.Size = new System.Drawing.Size(1391, 22);
            this.bottom_dataview.TabIndex = 2;
            this.bottom_dataview.Text = "statusStrip1";
            // 
            // mouse_positionX
            // 
            this.mouse_positionX.Name = "mouse_positionX";
            this.mouse_positionX.Size = new System.Drawing.Size(59, 17);
            this.mouse_positionX.Text = "Mouse X :";
            // 
            // mouse_positionY
            // 
            this.mouse_positionY.Name = "mouse_positionY";
            this.mouse_positionY.Size = new System.Drawing.Size(59, 17);
            this.mouse_positionY.Text = "Mouse Y :";
            // 
            // top_menu_container
            // 
            this.top_menu_container.BackColor = System.Drawing.Color.White;
            this.top_menu_container.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuContainer_File,
            this.menuContainer_Edit,
            this.menuContainer_Object,
            this.menuContainer_Resource,
            this.menuContainer_Navigation,
            this.menuContainer_Animation,
            this.menuContainer_Scripts,
            this.menuContainer_plugins,
            this.menuContainer_Project,
            this.menuContainer_Help});
            this.top_menu_container.Location = new System.Drawing.Point(0, 0);
            this.top_menu_container.Name = "top_menu_container";
            this.top_menu_container.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.top_menu_container.Size = new System.Drawing.Size(1391, 24);
            this.top_menu_container.TabIndex = 3;
            this.top_menu_container.Text = "menuStrip1";
            // 
            // menuContainer_File
            // 
            this.menuContainer_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_NewProject,
            this.menuItem_LoadProject,
            this.menuItem_SaveProject,
            this.menuItemSeperator1,
            this.menuItem_NewLevel,
            this.menuItem_LoadLevel,
            this.menuItem_SaveLevel,
            this.menuItemSeperator2,
            this.menuItem_BackToMenu,
            this.menuItem_Exit});
            this.menuContainer_File.Name = "menuContainer_File";
            this.menuContainer_File.Size = new System.Drawing.Size(37, 20);
            this.menuContainer_File.Text = "File";
            // 
            // menuItem_NewProject
            // 
            this.menuItem_NewProject.Name = "menuItem_NewProject";
            this.menuItem_NewProject.Size = new System.Drawing.Size(147, 22);
            this.menuItem_NewProject.Text = "New Project";
            this.menuItem_NewProject.Click += new System.EventHandler(this.menuItem_NewProject_Click);
            // 
            // menuItem_LoadProject
            // 
            this.menuItem_LoadProject.Name = "menuItem_LoadProject";
            this.menuItem_LoadProject.Size = new System.Drawing.Size(147, 22);
            this.menuItem_LoadProject.Text = "Load Project";
            this.menuItem_LoadProject.Click += new System.EventHandler(this.menuItem_LoadProject_Click);
            // 
            // menuItem_SaveProject
            // 
            this.menuItem_SaveProject.Name = "menuItem_SaveProject";
            this.menuItem_SaveProject.Size = new System.Drawing.Size(147, 22);
            this.menuItem_SaveProject.Text = "Save Project";
            this.menuItem_SaveProject.Click += new System.EventHandler(this.menuItem_SaveProject_Click);
            // 
            // menuItemSeperator1
            // 
            this.menuItemSeperator1.Name = "menuItemSeperator1";
            this.menuItemSeperator1.Size = new System.Drawing.Size(144, 6);
            // 
            // menuItem_NewLevel
            // 
            this.menuItem_NewLevel.Name = "menuItem_NewLevel";
            this.menuItem_NewLevel.Size = new System.Drawing.Size(147, 22);
            this.menuItem_NewLevel.Text = "New Level";
            this.menuItem_NewLevel.Click += new System.EventHandler(this.menuItem_NewLevel_Click);
            // 
            // menuItem_LoadLevel
            // 
            this.menuItem_LoadLevel.Name = "menuItem_LoadLevel";
            this.menuItem_LoadLevel.Size = new System.Drawing.Size(147, 22);
            this.menuItem_LoadLevel.Text = "Load Level";
            this.menuItem_LoadLevel.Click += new System.EventHandler(this.menuItem_LoadLevel_Click);
            // 
            // menuItem_SaveLevel
            // 
            this.menuItem_SaveLevel.Name = "menuItem_SaveLevel";
            this.menuItem_SaveLevel.Size = new System.Drawing.Size(147, 22);
            this.menuItem_SaveLevel.Text = "Save Level";
            this.menuItem_SaveLevel.Click += new System.EventHandler(this.menuItem_SaveLevel_Click);
            // 
            // menuItemSeperator2
            // 
            this.menuItemSeperator2.Name = "menuItemSeperator2";
            this.menuItemSeperator2.Size = new System.Drawing.Size(144, 6);
            // 
            // menuItem_BackToMenu
            // 
            this.menuItem_BackToMenu.Name = "menuItem_BackToMenu";
            this.menuItem_BackToMenu.Size = new System.Drawing.Size(147, 22);
            this.menuItem_BackToMenu.Text = "Back to menu";
            this.menuItem_BackToMenu.Click += new System.EventHandler(this.backToMenuToolStripMenuItem_Click);
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Name = "menuItem_Exit";
            this.menuItem_Exit.Size = new System.Drawing.Size(147, 22);
            this.menuItem_Exit.Text = "Exit";
            this.menuItem_Exit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuContainer_Edit
            // 
            this.menuContainer_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_LevelManager,
            this.menuItem_ProjectManager});
            this.menuContainer_Edit.Name = "menuContainer_Edit";
            this.menuContainer_Edit.Size = new System.Drawing.Size(39, 20);
            this.menuContainer_Edit.Text = "Edit";
            // 
            // menuItem_LevelManager
            // 
            this.menuItem_LevelManager.Name = "menuItem_LevelManager";
            this.menuItem_LevelManager.Size = new System.Drawing.Size(161, 22);
            this.menuItem_LevelManager.Text = "Level Manager";
            this.menuItem_LevelManager.Click += new System.EventHandler(this.menuItem_LevelManager_Click);
            // 
            // menuItem_ProjectManager
            // 
            this.menuItem_ProjectManager.Name = "menuItem_ProjectManager";
            this.menuItem_ProjectManager.Size = new System.Drawing.Size(161, 22);
            this.menuItem_ProjectManager.Text = "Project Manager";
            this.menuItem_ProjectManager.Click += new System.EventHandler(this.menuItem_ProjectManager_Click);
            // 
            // menuContainer_Object
            // 
            this.menuContainer_Object.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_CreateObject,
            this.menuItem_AddObject,
            this.menuItem_ObjectManager,
            this.menuItem_ObjectEditor});
            this.menuContainer_Object.Name = "menuContainer_Object";
            this.menuContainer_Object.Size = new System.Drawing.Size(54, 20);
            this.menuContainer_Object.Text = "Object";
            // 
            // menuItem_CreateObject
            // 
            this.menuItem_CreateObject.Name = "menuItem_CreateObject";
            this.menuItem_CreateObject.Size = new System.Drawing.Size(159, 22);
            this.menuItem_CreateObject.Text = "Create Object";
            this.menuItem_CreateObject.Click += new System.EventHandler(this.menuItem_CreateObject_Click);
            // 
            // menuItem_AddObject
            // 
            this.menuItem_AddObject.Name = "menuItem_AddObject";
            this.menuItem_AddObject.Size = new System.Drawing.Size(159, 22);
            this.menuItem_AddObject.Text = "Add Object";
            this.menuItem_AddObject.Click += new System.EventHandler(this.menuItem_AddObject_Click);
            // 
            // menuItem_ObjectManager
            // 
            this.menuItem_ObjectManager.Name = "menuItem_ObjectManager";
            this.menuItem_ObjectManager.Size = new System.Drawing.Size(159, 22);
            this.menuItem_ObjectManager.Text = "Object Manager";
            this.menuItem_ObjectManager.Click += new System.EventHandler(this.menuItem_ObjectManager_Click);
            // 
            // menuItem_ObjectEditor
            // 
            this.menuItem_ObjectEditor.Name = "menuItem_ObjectEditor";
            this.menuItem_ObjectEditor.Size = new System.Drawing.Size(159, 22);
            this.menuItem_ObjectEditor.Text = "Object Editor";
            this.menuItem_ObjectEditor.Click += new System.EventHandler(this.menuItem_ObjectEditor_Click);
            // 
            // menuContainer_Resource
            // 
            this.menuContainer_Resource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_ImportResource,
            this.menuItem_ResouceManager});
            this.menuContainer_Resource.Name = "menuContainer_Resource";
            this.menuContainer_Resource.Size = new System.Drawing.Size(72, 20);
            this.menuContainer_Resource.Text = "Resources";
            // 
            // menuItem_ImportResource
            // 
            this.menuItem_ImportResource.Name = "menuItem_ImportResource";
            this.menuItem_ImportResource.Size = new System.Drawing.Size(172, 22);
            this.menuItem_ImportResource.Text = "Import Resource";
            this.menuItem_ImportResource.Click += new System.EventHandler(this.menuItem_ImportResource_Click);
            // 
            // menuItem_ResouceManager
            // 
            this.menuItem_ResouceManager.Name = "menuItem_ResouceManager";
            this.menuItem_ResouceManager.Size = new System.Drawing.Size(172, 22);
            this.menuItem_ResouceManager.Text = "Resource Manager";
            this.menuItem_ResouceManager.Click += new System.EventHandler(this.menuItem_ResouceManager_Click);
            // 
            // menuContainer_Navigation
            // 
            this.menuContainer_Navigation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_OpenEditor});
            this.menuContainer_Navigation.Name = "menuContainer_Navigation";
            this.menuContainer_Navigation.Size = new System.Drawing.Size(77, 20);
            this.menuContainer_Navigation.Text = "Navigation";
            // 
            // menuItem_OpenEditor
            // 
            this.menuItem_OpenEditor.Name = "menuItem_OpenEditor";
            this.menuItem_OpenEditor.Size = new System.Drawing.Size(137, 22);
            this.menuItem_OpenEditor.Text = "Open Editor";
            this.menuItem_OpenEditor.Click += new System.EventHandler(this.menuItem_OpenEditor_Click);
            // 
            // menuContainer_Animation
            // 
            this.menuContainer_Animation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_AOpenEditor});
            this.menuContainer_Animation.Name = "menuContainer_Animation";
            this.menuContainer_Animation.Size = new System.Drawing.Size(75, 20);
            this.menuContainer_Animation.Text = "Animation";
            // 
            // menuItem_AOpenEditor
            // 
            this.menuItem_AOpenEditor.Name = "menuItem_AOpenEditor";
            this.menuItem_AOpenEditor.Size = new System.Drawing.Size(137, 22);
            this.menuItem_AOpenEditor.Text = "Open Editor";
            this.menuItem_AOpenEditor.Click += new System.EventHandler(this.menuItem_AOpenEditor_Click);
            // 
            // menuContainer_Scripts
            // 
            this.menuContainer_Scripts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_NewScript,
            this.menuItem_ScriptManager,
            this.menuItem_open_editor});
            this.menuContainer_Scripts.Name = "menuContainer_Scripts";
            this.menuContainer_Scripts.Size = new System.Drawing.Size(54, 20);
            this.menuContainer_Scripts.Text = "Scripts";
            // 
            // menuItem_NewScript
            // 
            this.menuItem_NewScript.Name = "menuItem_NewScript";
            this.menuItem_NewScript.Size = new System.Drawing.Size(154, 22);
            this.menuItem_NewScript.Text = "New Script";
            this.menuItem_NewScript.Click += new System.EventHandler(this.menuItem_NewScript_Click);
            // 
            // menuItem_ScriptManager
            // 
            this.menuItem_ScriptManager.Name = "menuItem_ScriptManager";
            this.menuItem_ScriptManager.Size = new System.Drawing.Size(154, 22);
            this.menuItem_ScriptManager.Text = "Script Manager";
            this.menuItem_ScriptManager.Click += new System.EventHandler(this.menuItem_ScriptManager_Click);
            // 
            // menuItem_open_editor
            // 
            this.menuItem_open_editor.Name = "menuItem_open_editor";
            this.menuItem_open_editor.Size = new System.Drawing.Size(154, 22);
            this.menuItem_open_editor.Text = "Open Editor";
            this.menuItem_open_editor.Click += new System.EventHandler(this.menuItem_open_editor_Click);
            // 
            // menuContainer_plugins
            // 
            this.menuContainer_plugins.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_ImportPlugins,
            this.menuItem_PluginsManager,
            this.menuItem_ImportHeader});
            this.menuContainer_plugins.Name = "menuContainer_plugins";
            this.menuContainer_plugins.Size = new System.Drawing.Size(58, 20);
            this.menuContainer_plugins.Text = "Plugins";
            // 
            // menuItem_ImportPlugins
            // 
            this.menuItem_ImportPlugins.Name = "menuItem_ImportPlugins";
            this.menuItem_ImportPlugins.Size = new System.Drawing.Size(190, 22);
            this.menuItem_ImportPlugins.Text = "Import Plugins";
            this.menuItem_ImportPlugins.Click += new System.EventHandler(this.menuItem_ImportPlugins_Click);
            // 
            // menuItem_PluginsManager
            // 
            this.menuItem_PluginsManager.Name = "menuItem_PluginsManager";
            this.menuItem_PluginsManager.Size = new System.Drawing.Size(190, 22);
            this.menuItem_PluginsManager.Text = "Plugins Manager";
            this.menuItem_PluginsManager.Click += new System.EventHandler(this.menuItem_PluginsManager_Click);
            // 
            // menuItem_ImportHeader
            // 
            this.menuItem_ImportHeader.Enabled = false;
            this.menuItem_ImportHeader.Name = "menuItem_ImportHeader";
            this.menuItem_ImportHeader.Size = new System.Drawing.Size(190, 22);
            this.menuItem_ImportHeader.Text = "Import Library Header";
            this.menuItem_ImportHeader.Click += new System.EventHandler(this.menuItem_ImportHeader_Click);
            // 
            // menuContainer_Project
            // 
            this.menuContainer_Project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_RunProject,
            this.menuItem_BuildProject,
            this.menuItem_BuildOptions});
            this.menuContainer_Project.Name = "menuContainer_Project";
            this.menuContainer_Project.Size = new System.Drawing.Size(56, 20);
            this.menuContainer_Project.Text = "Project";
            // 
            // menuItem_RunProject
            // 
            this.menuItem_RunProject.Name = "menuItem_RunProject";
            this.menuItem_RunProject.Size = new System.Drawing.Size(146, 22);
            this.menuItem_RunProject.Text = "Run Project";
            this.menuItem_RunProject.Click += new System.EventHandler(this.menuItem_RunProject_Click);
            // 
            // menuItem_BuildProject
            // 
            this.menuItem_BuildProject.Name = "menuItem_BuildProject";
            this.menuItem_BuildProject.Size = new System.Drawing.Size(146, 22);
            this.menuItem_BuildProject.Text = "Build Project";
            this.menuItem_BuildProject.Click += new System.EventHandler(this.menuItem_BuildProject_Click);
            // 
            // menuItem_BuildOptions
            // 
            this.menuItem_BuildOptions.Name = "menuItem_BuildOptions";
            this.menuItem_BuildOptions.Size = new System.Drawing.Size(146, 22);
            this.menuItem_BuildOptions.Text = "Build Options";
            this.menuItem_BuildOptions.Click += new System.EventHandler(this.menuItem_Build_Options_Click);
            // 
            // menuContainer_Help
            // 
            this.menuContainer_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_AboutHeavyEngine});
            this.menuContainer_Help.Name = "menuContainer_Help";
            this.menuContainer_Help.Size = new System.Drawing.Size(44, 20);
            this.menuContainer_Help.Text = "Help";
            // 
            // menuItem_AboutHeavyEngine
            // 
            this.menuItem_AboutHeavyEngine.Name = "menuItem_AboutHeavyEngine";
            this.menuItem_AboutHeavyEngine.Size = new System.Drawing.Size(182, 22);
            this.menuItem_AboutHeavyEngine.Text = "About Heavy Engine";
            this.menuItem_AboutHeavyEngine.Click += new System.EventHandler(this.menuItem_AboutHeavyEngine_Click);
            // 
            // tmr_draw
            // 
            this.tmr_draw.Interval = 1;
            this.tmr_draw.Tick += new System.EventHandler(this.tmr_draw_Tick);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(3, 9);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(185, 26);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_up
            // 
            this.btn_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_up.Location = new System.Drawing.Point(6, 32);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(93, 23);
            this.btn_up.TabIndex = 5;
            this.btn_up.Text = "&Up";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_down
            // 
            this.btn_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_down.Location = new System.Drawing.Point(105, 32);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(93, 23);
            this.btn_down.TabIndex = 6;
            this.btn_down.Text = "&Down";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_left
            // 
            this.btn_left.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_left.Location = new System.Drawing.Point(209, 32);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(93, 23);
            this.btn_left.TabIndex = 7;
            this.btn_left.Text = "&Left";
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_right
            // 
            this.btn_right.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_right.Location = new System.Drawing.Point(308, 31);
            this.btn_right.Name = "btn_right";
            this.btn_right.Size = new System.Drawing.Size(93, 23);
            this.btn_right.TabIndex = 8;
            this.btn_right.Text = "&Right";
            this.btn_right.UseVisualStyleBackColor = true;
            this.btn_right.Click += new System.EventHandler(this.btn_right_Click);
            // 
            // lbl_editor_movement
            // 
            this.lbl_editor_movement.AutoSize = true;
            this.lbl_editor_movement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_editor_movement.Location = new System.Drawing.Point(3, 1);
            this.lbl_editor_movement.Name = "lbl_editor_movement";
            this.lbl_editor_movement.Size = new System.Drawing.Size(109, 16);
            this.lbl_editor_movement.TabIndex = 9;
            this.lbl_editor_movement.Text = "Editor Movement";
            // 
            // lbl_zoom
            // 
            this.lbl_zoom.AutoSize = true;
            this.lbl_zoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_zoom.Location = new System.Drawing.Point(3, 0);
            this.lbl_zoom.Name = "lbl_zoom";
            this.lbl_zoom.Size = new System.Drawing.Size(43, 16);
            this.lbl_zoom.TabIndex = 10;
            this.lbl_zoom.Text = "Zoom";
            // 
            // btn_inside
            // 
            this.btn_inside.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_inside.Location = new System.Drawing.Point(0, 25);
            this.btn_inside.Name = "btn_inside";
            this.btn_inside.Size = new System.Drawing.Size(93, 25);
            this.btn_inside.TabIndex = 11;
            this.btn_inside.Text = "&Inside";
            this.btn_inside.UseVisualStyleBackColor = true;
            this.btn_inside.Click += new System.EventHandler(this.btn_inside_Click);
            // 
            // btn_outside
            // 
            this.btn_outside.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_outside.Location = new System.Drawing.Point(99, 25);
            this.btn_outside.Name = "btn_outside";
            this.btn_outside.Size = new System.Drawing.Size(93, 25);
            this.btn_outside.TabIndex = 12;
            this.btn_outside.Text = "&Outside";
            this.btn_outside.UseVisualStyleBackColor = true;
            this.btn_outside.Click += new System.EventHandler(this.btn_outside_Click);
            // 
            // lbl_view_x
            // 
            this.lbl_view_x.AutoSize = true;
            this.lbl_view_x.BackColor = System.Drawing.Color.Gray;
            this.lbl_view_x.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_view_x.ForeColor = System.Drawing.Color.Red;
            this.lbl_view_x.Location = new System.Drawing.Point(365, 0);
            this.lbl_view_x.Name = "lbl_view_x";
            this.lbl_view_x.Size = new System.Drawing.Size(73, 16);
            this.lbl_view_x.TabIndex = 13;
            this.lbl_view_x.Text = "Camera X :";
            // 
            // lbl_view_y
            // 
            this.lbl_view_y.AutoSize = true;
            this.lbl_view_y.BackColor = System.Drawing.Color.Gray;
            this.lbl_view_y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_view_y.ForeColor = System.Drawing.Color.Red;
            this.lbl_view_y.Location = new System.Drawing.Point(365, 25);
            this.lbl_view_y.Name = "lbl_view_y";
            this.lbl_view_y.Size = new System.Drawing.Size(74, 16);
            this.lbl_view_y.TabIndex = 14;
            this.lbl_view_y.Text = "Camera Y :";
            this.lbl_view_y.Click += new System.EventHandler(this.lbl_view_y_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.Location = new System.Drawing.Point(5, 41);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(185, 26);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // contpane_base
            // 
            this.contpane_base.ColumnCount = 2;
            this.contpane_base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.contpane_base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 193F));
            this.contpane_base.Controls.Add(this.contpane_tools, 0, 1);
            this.contpane_base.Controls.Add(this.contpane_canvas, 0, 0);
            this.contpane_base.Controls.Add(this.contpane_buttons, 1, 1);
            this.contpane_base.Controls.Add(this.gameObject_editor, 1, 0);
            this.contpane_base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_base.Location = new System.Drawing.Point(0, 0);
            this.contpane_base.Name = "contpane_base";
            this.contpane_base.RowCount = 2;
            this.contpane_base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.97317F));
            this.contpane_base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.02683F));
            this.contpane_base.Size = new System.Drawing.Size(1245, 565);
            this.contpane_base.TabIndex = 16;
            // 
            // contpane_tools
            // 
            this.contpane_tools.Controls.Add(this.contpane_tabs);
            this.contpane_tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_tools.Location = new System.Drawing.Point(3, 483);
            this.contpane_tools.Name = "contpane_tools";
            this.contpane_tools.Size = new System.Drawing.Size(1046, 79);
            this.contpane_tools.TabIndex = 5;
            // 
            // contpane_tabs
            // 
            this.contpane_tabs.ColumnCount = 2;
            this.contpane_tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.contpane_tabs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.contpane_tabs.Controls.Add(this.contpane_camerazoom, 1, 0);
            this.contpane_tabs.Controls.Add(this.contpane_movement, 0, 0);
            this.contpane_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_tabs.Location = new System.Drawing.Point(0, 0);
            this.contpane_tabs.Name = "contpane_tabs";
            this.contpane_tabs.RowCount = 1;
            this.contpane_tabs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.contpane_tabs.Size = new System.Drawing.Size(1046, 79);
            this.contpane_tabs.TabIndex = 17;
            // 
            // contpane_camerazoom
            // 
            this.contpane_camerazoom.Controls.Add(this.lbl_zoom);
            this.contpane_camerazoom.Controls.Add(this.btn_inside);
            this.contpane_camerazoom.Controls.Add(this.lbl_view_y);
            this.contpane_camerazoom.Controls.Add(this.lbl_view_x);
            this.contpane_camerazoom.Controls.Add(this.btn_outside);
            this.contpane_camerazoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_camerazoom.Location = new System.Drawing.Point(526, 3);
            this.contpane_camerazoom.Name = "contpane_camerazoom";
            this.contpane_camerazoom.Size = new System.Drawing.Size(517, 73);
            this.contpane_camerazoom.TabIndex = 16;
            // 
            // contpane_movement
            // 
            this.contpane_movement.Controls.Add(this.lbl_editor_movement);
            this.contpane_movement.Controls.Add(this.btn_up);
            this.contpane_movement.Controls.Add(this.btn_right);
            this.contpane_movement.Controls.Add(this.btn_down);
            this.contpane_movement.Controls.Add(this.btn_left);
            this.contpane_movement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_movement.Location = new System.Drawing.Point(3, 3);
            this.contpane_movement.Name = "contpane_movement";
            this.contpane_movement.Size = new System.Drawing.Size(517, 73);
            this.contpane_movement.TabIndex = 15;
            // 
            // contpane_canvas
            // 
            this.contpane_canvas.Controls.Add(this.canvas);
            this.contpane_canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_canvas.Location = new System.Drawing.Point(3, 3);
            this.contpane_canvas.Name = "contpane_canvas";
            this.contpane_canvas.Size = new System.Drawing.Size(1046, 474);
            this.contpane_canvas.TabIndex = 7;
            // 
            // contpane_buttons
            // 
            this.contpane_buttons.Controls.Add(this.btn_save);
            this.contpane_buttons.Controls.Add(this.btn_cancel);
            this.contpane_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contpane_buttons.Location = new System.Drawing.Point(1055, 483);
            this.contpane_buttons.Name = "contpane_buttons";
            this.contpane_buttons.Size = new System.Drawing.Size(187, 79);
            this.contpane_buttons.TabIndex = 6;
            // 
            // base_container
            // 
            this.base_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.base_container.Location = new System.Drawing.Point(0, 24);
            this.base_container.Name = "base_container";
            // 
            // base_container.Panel1
            // 
            this.base_container.Panel1.Controls.Add(this.list_div);
            // 
            // base_container.Panel2
            // 
            this.base_container.Panel2.Controls.Add(this.contpane_base);
            this.base_container.Size = new System.Drawing.Size(1391, 565);
            this.base_container.SplitterDistance = 142;
            this.base_container.TabIndex = 17;
            // 
            // list_div
            // 
            this.list_div.ColumnCount = 1;
            this.list_div.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.list_div.Controls.Add(this.file_tree, 0, 1);
            this.list_div.Controls.Add(this.lb_objects, 0, 0);
            this.list_div.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_div.Location = new System.Drawing.Point(0, 0);
            this.list_div.Name = "list_div";
            this.list_div.RowCount = 2;
            this.list_div.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.list_div.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.list_div.Size = new System.Drawing.Size(142, 565);
            this.list_div.TabIndex = 0;
            this.list_div.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // file_tree
            // 
            this.file_tree.BackColor = System.Drawing.Color.Silver;
            this.file_tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.file_tree.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_tree.Location = new System.Drawing.Point(3, 285);
            this.file_tree.Name = "file_tree";
            this.file_tree.Size = new System.Drawing.Size(136, 277);
            this.file_tree.TabIndex = 0;
            this.file_tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.file_tree_AfterSelect);
            // 
            // lb_objects
            // 
            this.lb_objects.BackColor = System.Drawing.Color.Gray;
            this.lb_objects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_objects.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_objects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lb_objects.FormattingEnabled = true;
            this.lb_objects.ItemHeight = 15;
            this.lb_objects.Location = new System.Drawing.Point(3, 3);
            this.lb_objects.Name = "lb_objects";
            this.lb_objects.Size = new System.Drawing.Size(136, 276);
            this.lb_objects.TabIndex = 1;
            this.lb_objects.SelectedIndexChanged += new System.EventHandler(this.lb_objects_SelectedIndexChanged);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1391, 611);
            this.Controls.Add(this.base_container);
            this.Controls.Add(this.bottom_dataview);
            this.Controls.Add(this.top_menu_container);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.top_menu_container;
            this.MaximizeBox = false;
            this.Name = "Editor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Heavy Engine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Editor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.bottom_dataview.ResumeLayout(false);
            this.bottom_dataview.PerformLayout();
            this.top_menu_container.ResumeLayout(false);
            this.top_menu_container.PerformLayout();
            this.contpane_base.ResumeLayout(false);
            this.contpane_tools.ResumeLayout(false);
            this.contpane_tabs.ResumeLayout(false);
            this.contpane_camerazoom.ResumeLayout(false);
            this.contpane_camerazoom.PerformLayout();
            this.contpane_movement.ResumeLayout(false);
            this.contpane_movement.PerformLayout();
            this.contpane_canvas.ResumeLayout(false);
            this.contpane_canvas.PerformLayout();
            this.contpane_buttons.ResumeLayout(false);
            this.base_container.Panel1.ResumeLayout(false);
            this.base_container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.base_container)).EndInit();
            this.base_container.ResumeLayout(false);
            this.list_div.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.PropertyGrid gameObject_editor;
        private System.Windows.Forms.StatusStrip bottom_dataview;
        private System.Windows.Forms.ToolStripStatusLabel mouse_positionX;
        private System.Windows.Forms.ToolStripStatusLabel mouse_positionY;
        private System.Windows.Forms.MenuStrip top_menu_container;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_File;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NewProject;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LoadProject;
        private System.Windows.Forms.ToolStripMenuItem menuItem_SaveProject;
        private System.Windows.Forms.ToolStripSeparator menuItemSeperator1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NewLevel;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LoadLevel;
        private System.Windows.Forms.ToolStripMenuItem menuItem_SaveLevel;
        private System.Windows.Forms.ToolStripSeparator menuItemSeperator2;
        private System.Windows.Forms.ToolStripMenuItem menuItem_BackToMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Edit;
        private System.Windows.Forms.ToolStripMenuItem menuItem_LevelManager;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ProjectManager;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Object;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AddObject;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ObjectManager;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Resource;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ImportResource;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ResouceManager;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Scripts;
        private System.Windows.Forms.ToolStripMenuItem menuItem_NewScript;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ScriptManager;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Project;
        private System.Windows.Forms.ToolStripMenuItem menuItem_RunProject;
        private System.Windows.Forms.ToolStripMenuItem menuItem_BuildProject;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Help;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AboutHeavyEngine;
        private System.Windows.Forms.ToolStripMenuItem menuItem_CreateObject;
        private System.Windows.Forms.Timer tmr_draw;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_plugins;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ImportPlugins;
        private System.Windows.Forms.ToolStripMenuItem menuItem_PluginsManager;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_right;
        private System.Windows.Forms.Label lbl_editor_movement;
        private System.Windows.Forms.Label lbl_zoom;
        private System.Windows.Forms.Button btn_inside;
        private System.Windows.Forms.Button btn_outside;
        private System.Windows.Forms.Label lbl_view_x;
        private System.Windows.Forms.Label lbl_view_y;
        private System.Windows.Forms.ToolStripMenuItem menuItem_open_editor;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ObjectEditor;
        private System.Windows.Forms.ToolStripMenuItem menuItem_BuildOptions;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TableLayoutPanel contpane_base;
        private System.Windows.Forms.Panel contpane_tools;
        private System.Windows.Forms.Panel contpane_buttons;
        private System.Windows.Forms.Panel contpane_canvas;
        private System.Windows.Forms.TableLayoutPanel contpane_tabs;
        private System.Windows.Forms.Panel contpane_camerazoom;
        private System.Windows.Forms.Panel contpane_movement;
        private System.Windows.Forms.SplitContainer base_container;
        private System.Windows.Forms.TableLayoutPanel list_div;
        private System.Windows.Forms.TreeView file_tree;
        private System.Windows.Forms.ListBox lb_objects;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Navigation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_OpenEditor;
        private System.Windows.Forms.ToolStripMenuItem menuContainer_Animation;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AOpenEditor;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ImportHeader;
    }
}