using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Threading;

namespace Heavy_Engine
{
    public struct Vector2
    {
        public int x;
        public int y;
    }

    public partial class Editor : Form
    {
        Form menu_screen;
        public string project_default_dir;
        string project_name;
        public ProjectInfo project_info;
        public GameLevel current_level;
        public List<GameObject> gameObject_list;
        public List<GameObject_Scene> gameObjectScene_list;
        int mouse_PX = 0;
        int mouse_PY = 0;
        int zoom_rate = 10;
        public int cam_x = 0;
        public int cam_y = 0;
        public int platform_id = 1; // Default : Windows.
        bool isLcontrol = false;
        public bool canClose = true;
        public DrawableGameObject[] sortedArray;
        private List<Vector2> lines_array;
        MenuItem short_menu_createGameObject = new MenuItem("Create Game Object");
        MenuItem short_menu_addGameObject = new MenuItem("Add Game Object");
        MenuItem short_menu_openObjectEditor = new MenuItem("Open Object Editor");
        MenuItem short_menu_zooming = new MenuItem("Zoom");
        MenuItem short_menu_zoomingin = new MenuItem("Inside 10%");
        MenuItem short_menu_zoomingout = new MenuItem("Outside 10%");
        MenuItem short_menu_importresource = new MenuItem("Import Resource");
        MenuItem short_menu_importplugin = new MenuItem("Import Plugin");
        MenuItem short_menu_importheader = new MenuItem("Import Library Header");
        MenuItem short_menu_starteditor = new MenuItem("Start Editor");
        MenuItem short_menu_showGameObject = new MenuItem("Show Game Object Properties");
        MenuItem short_menu_select = new MenuItem("Select");
        MenuItem short_menu_release = new MenuItem("Release");
        MenuItem short_menu_cancel = new MenuItem("Cancel");
        MenuItem short_menu_showBaseProperty = new MenuItem("Show Base Property");
        MenuItem short_menu_showObjectProperty = new MenuItem("Show Object Property");
        int isMaximized = 30;

        public Editor(Form menu_screen,string project_dir,string project_name)
        {
            project_default_dir = project_dir;
            this.project_name = project_name;
            this.menu_screen = menu_screen;
            gameObject_list = new List<GameObject>();
            gameObjectScene_list = new List<GameObject_Scene>();
            lines_array = new List<Vector2>();
            LoadProject();
            InitializeComponent();
            tmr_draw.Enabled = true;
            tmr_draw.Start();
            canClose = true;

            short_menu_zooming.MenuItems.Add(short_menu_zoomingin);
            short_menu_zooming.MenuItems.Add(short_menu_zoomingout);

            short_menu_createGameObject.Click += new EventHandler(short_menu_createGameObject_Click);
            short_menu_addGameObject.Click += new EventHandler(short_menu_addGameObject_Click);
            short_menu_openObjectEditor.Click += new EventHandler(short_menu_openObjectEditor_Click);
            short_menu_zoomingin.Click += new EventHandler(short_menu_zoomingin_Click);
            short_menu_zoomingout.Click += new EventHandler(short_menu_zoomingout_Click);
            short_menu_importresource.Click += new EventHandler(short_menu_importresource_Click);
            short_menu_importplugin.Click += new EventHandler(short_menu_importplugin_Click);
            short_menu_starteditor.Click += new EventHandler(short_menu_starteditor_Click);
            short_menu_showGameObject.Click += new EventHandler(short_menu_showGameObject_Click);
            short_menu_select.Click += new EventHandler(short_menu_select_Click);
            short_menu_release.Click += new EventHandler(short_menu_release_Click);
            short_menu_cancel.Click += new EventHandler(short_menu_cancel_Click);
            short_menu_showBaseProperty.Click += new EventHandler(short_menu_showBaseProperty_Click);
            short_menu_showObjectProperty.Click += new EventHandler(short_menu_showObjectProperty_Click);
            short_menu_importheader.Click += short_menu_importheader_Click;
        }

        void short_menu_importheader_Click(object sender, EventArgs e)
        {
            menuItem_ImportHeader_Click(null, null);
        }

        void short_menu_showObjectProperty_Click(object sender, EventArgs e)
        {
            if (lb_objects.SelectedIndex == -1) return;

            foreach (GameObject_Scene gameObject in gameObjectScene_list)
            {
                if (gameObject.instance_name == (string)lb_objects.Items[lb_objects.SelectedIndex])
                {
                    gameObject_editor.SelectedObject = new GameObject_Scene_EDTIOR(gameObject);
                    return;
                }
            }
        }

        void short_menu_showBaseProperty_Click(object sender, EventArgs e)
        {
            if (lb_objects.SelectedIndex == -1) return;

            foreach (GameObject_Scene gameObject in gameObjectScene_list)
            {
                if (gameObject.instance_name == (string)lb_objects.Items[lb_objects.SelectedIndex])
                {
                    ObjectEditor obj_editor = new ObjectEditor(this, gameObject.mainObject.object_name);

                    obj_editor.Show();
                }
            }
        }

        void short_menu_cancel_Click(object sender, EventArgs e)
        {
            btn_cancel_Click(null, null);
        }

        void short_menu_release_Click(object sender, EventArgs e)
        {
            isLcontrol = false;
        }

        void short_menu_select_Click(object sender, EventArgs e)
        {
            isLcontrol = true;
        }

        void short_menu_showGameObject_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject == null) return;

            ObjectEditor obj_editor = new ObjectEditor(this, ((GameObject_Scene_EDTIOR ) gameObject_editor.SelectedObject)._obj.mainObject.object_name);

            obj_editor.Show();
        }

        void short_menu_starteditor_Click(object sender, EventArgs e)
        {
            menuItem_open_editor_Click(null, null);
        }

        void short_menu_importplugin_Click(object sender, EventArgs e)
        {
            menuItem_ImportPlugins_Click(null, null);
        }

        void short_menu_importresource_Click(object sender, EventArgs e)
        {
            menuItem_ImportResource_Click(null, null);
        }

        void short_menu_zoomingout_Click(object sender, EventArgs e)
        {
            zoomOut_Checked();
        }

        void short_menu_zoomingin_Click(object sender, EventArgs e)
        {
            zoomIn_Checked();
        }

        void short_menu_openObjectEditor_Click(object sender, EventArgs e)
        {
            menuItem_ObjectEditor_Click(null, null);
        }

        void short_menu_addGameObject_Click(object sender, EventArgs e)
        {
            menuItem_AddObject_Click(null, null);
        }

        void short_menu_createGameObject_Click(object sender, EventArgs e)
        {
            menuItem_CreateObject_Click(null, null);
        }

        public void onGameObjectDeleted(string instance_name)
        {
            for (int cnt = 0; cnt < lb_objects.Items.Count; cnt++)
            {
                if (lb_objects.Items[cnt] == instance_name)
                {
                    lb_objects.Items.RemoveAt(cnt);
                    return;
                }
            }

            if (gameObject_editor.SelectedObject == null) return;

            if (instance_name == ((GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject)._obj.instance_name)
            {
                gameObject_editor.SelectedObject = null;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouse_PX = e.X;
            mouse_PY = e.Y;
            mouse_positionX.Text = "Mouse X : " + e.X;
            mouse_positionY.Text = "Mouse Y : " + e.Y;

            if (!isLcontrol)
            {
                return;
            }

            if (gameObject_editor.SelectedObject != null)
            {
                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    if (gameObjectScene_list[cnt].instance_name == ((GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject)._obj.instance_name)
                    {
                        GameObject_Scene obj = gameObjectScene_list[cnt];

                        obj.position_scene_x = e.X;
                        obj.position_scene_y = e.Y;

                        gameObjectScene_list[cnt] = obj;

                        ((GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject).X = e.X + cam_x;
                        ((GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject).Y = e.Y + cam_y;

                        break;
                    }
                }
            }
        }

        public void SaveLevel()
        {
            menuItem_SaveLevel_Click(null, null);
        }

        public void addGameObject(string instance_name)
        {
            lb_objects.Items.Add(instance_name);
        }

        private void findDirectories(TreeNode current_node, string path)
        {
            string[] directories = Directory.GetDirectories(path);

            foreach (string file_path in Directory.GetFiles(path))
            {
                current_node.Nodes.Add(Path.GetFileName(file_path));
            }

            for (int cnt = 0; cnt < directories.Length; cnt++)
            {
                string dir_names = directories[cnt];

                TreeNode sub_node = new TreeNode(dir_names.Substring(dir_names.LastIndexOf('\\') + 1, dir_names.Length - (dir_names.LastIndexOf('\\') + 1)));

                current_node.Nodes.Add(sub_node);

                findDirectories(sub_node, dir_names);
            }
        }

        public void reloadFileTree()
        {
            TreeNode base_node = new TreeNode(project_info.project_name);

            file_tree.Nodes.Clear();

            file_tree.Nodes.Add(base_node);

            findDirectories(base_node, project_default_dir);
        }

        public void EnableHeaderImport( )
        {
            this.menuItem_ImportHeader.Enabled = true;
        }

        public void DisableHeaderImport()
        {
            this.menuItem_ImportHeader.Enabled = false;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            System.Reflection.PropertyInfo info = typeof(Control).GetProperty("DoubleBuffered",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            info.SetValue(canvas, true, null);

            this.canvas.MouseClick += Editor_MouseClick;
            this.canvas.MouseMove += canvas_MouseMove;
            this.canvas.Paint += canvas_Paint;
            this.base_container.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);
            this.FormClosing += new FormClosingEventHandler(Editor_FormClosing);
            this.SizeChanged += new EventHandler(Editor_SizeChanged);
            this.canvas.MouseClick += new MouseEventHandler(canvas_MouseClick);
            this.lb_objects.MouseClick += new MouseEventHandler(lb_objects_MouseClick);
            this.file_tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(file_tree_NodeMouseClick);

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                lb_objects.Items.Add(gameObjectScene_list[cnt].instance_name);
            }

            sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                sortedArray[cnt].depth = gameObjectScene_list[cnt].depth;
                sortedArray[cnt].index = cnt;
            }

            while (!checkSorted(sortedArray))
            {
                sortElements(sortedArray);
            }

            this.base_container.Panel1MinSize = 250;
            this.base_container.Panel2MinSize = this.contpane_base.Width;
            calculateLines();
            reloadFileTree();
        }

        void file_tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string abs_path = "";
            TreeNode cur_node = e.Node;

            while (cur_node != null )
            {
                abs_path = "\\" + cur_node.Text + abs_path;
                cur_node = cur_node.Parent;
            }

            abs_path = project_default_dir.Substring(0,project_default_dir.LastIndexOf('\\')) + abs_path;

            if (File.Exists(abs_path))
            {
                if (Path.GetExtension(abs_path) == ".cs" || Path.GetExtension(abs_path) == "cs" || Path.GetExtension(abs_path) == ".java" || Path.GetExtension(abs_path) == "java" || Path.GetExtension(abs_path) == ".bs" || Path.GetExtension(abs_path) == "bs")
                {
                    try
                    {
                        Process.Start("notepad++",abs_path);
                    }
                    catch(Win32Exception ex)
                    {
                        MessageBox.Show("Notepad++ Editor not found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Path.GetExtension(abs_path) == ".obj" || Path.GetExtension(abs_path) == "obj")
                {
                    ObjectEditor obj_editor = new ObjectEditor(this, Path.GetFileNameWithoutExtension(abs_path));

                    obj_editor.Show();
                }
                else if (Path.GetExtension(abs_path) == ".hvl" || Path.GetExtension(abs_path) == "hvl")
                {
                    menuItem_NewLevel_Click(null, null);

                    LoadLevel(abs_path);

                    sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

                    for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                    {
                        sortedArray[cnt].depth = gameObjectScene_list[cnt].depth;
                        sortedArray[cnt].index = cnt;
                    }

                    while (!checkSorted(sortedArray))
                    {
                        sortElements(sortedArray);
                    }

                    lb_objects.Items.Clear();

                    for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                    {
                        lb_objects.Items.Add(gameObjectScene_list[cnt].instance_name);
                    }
                }
                else if (Path.GetExtension(abs_path) == ".nav" || Path.GetExtension(abs_path) == "nav")
                {
                    NavigationEditor nav_editor = new NavigationEditor(this, abs_path);

                    nav_editor.Show();
                }
                else if (Path.GetExtension(abs_path) == ".anim" || Path.GetExtension(abs_path) == "anim")
                {
                    AnimationEditor anim_editor = new AnimationEditor(this, abs_path);

                    anim_editor.Show();
                }
                else if (Path.GetExtension(abs_path) == ".exe" || Path.GetExtension(abs_path) == "exe")
                {
                    Process.Start(abs_path);
                }
            }
        }

        void lb_objects_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenu sub_menu = new ContextMenu();

                sub_menu.MenuItems.AddRange(new MenuItem[] { short_menu_showBaseProperty , short_menu_showObjectProperty });

                canvas.ContextMenu = sub_menu;
            }
        }

        void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            calculateLines();
        }

        void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();

                if (gameObject_editor.SelectedObject != null)
                {
                    if (isLcontrol)
                    {
                        short_menu_select.Enabled = false;
                        short_menu_release.Enabled = true;
                    }
                    else
                    {
                        short_menu_select.Enabled = true;
                        short_menu_release.Enabled = false;
                    }

                    menu.MenuItems.AddRange(new MenuItem[] { short_menu_showGameObject , short_menu_select , short_menu_release , short_menu_cancel });
                }
                else
                {
                    if (platform_id == 4)
                    {
                        menu.MenuItems.AddRange(new MenuItem[] { short_menu_createGameObject, short_menu_addGameObject, short_menu_openObjectEditor, short_menu_zooming, short_menu_importresource, short_menu_importplugin , short_menu_importheader, short_menu_starteditor });
                    }
                    else
                    {
                        menu.MenuItems.AddRange(new MenuItem[] { short_menu_createGameObject, short_menu_addGameObject, short_menu_openObjectEditor, short_menu_zooming, short_menu_importresource, short_menu_importplugin, short_menu_starteditor });
                    }
                }

                menu.Collapse += new EventHandler(menu_Collapse);

                this.canvas.ContextMenu = menu;
            }
        }

        void menu_Collapse(object sender, EventArgs e)
        {
            this.canvas.ContextMenu = null;
        }

        void Editor_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                isMaximized = 30;
            }
            else
            {
                isMaximized = 0;
            }

            calculateLines();
        }

        void calculateLines() // Required for preformance improvement.
        {
            lines_array.Clear();

            for (int y = 0; y < canvas.Height; y += zoom_rate + isMaximized)
            {
                for (int x = 0; x < canvas.Width; x += zoom_rate + isMaximized)
                {
                    Vector2 vec_pos = new Vector2();

                    vec_pos.x = x;
                    vec_pos.y = y;

                    lines_array.Add(vec_pos);
                }
            }
        }

        void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
           if (canClose) Application.Exit();
        }
  
        void Editor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

             if (gameObject_editor.SelectedObject != null)
             {
                  isLcontrol = !isLcontrol;

             //     gameObject_editor.SelectedObject = null;

                  return;
             }

            for (int cntr = 0; cntr < gameObjectScene_list.Count; cntr++)
            {
                if (gameObjectScene_list[cntr].mainObject.object_img != null)
                {
                    if (gameObjectScene_list[cntr].position_scene_x + gameObjectScene_list[cntr].mainObject.object_img.Width > mouse_PX && gameObjectScene_list[cntr].position_scene_x < mouse_PX && gameObjectScene_list[cntr].position_scene_y + gameObjectScene_list[cntr].mainObject.object_img.Height > mouse_PY && gameObjectScene_list[cntr].position_scene_y < mouse_PY && e.Button == MouseButtons.Left)
                    {
                        GameObject_Scene_EDTIOR obj = new GameObject_Scene_EDTIOR(gameObjectScene_list[cntr]);
                        gameObject_editor.SelectedObject = obj;
                        isLcontrol = true;
                    }
                }
                else if (gameObjectScene_list[cntr].mainObject.object_text != "")
                {
                    if (gameObjectScene_list[cntr].position_x + 10 * gameObjectScene_list[cntr].mainObject.object_text.Length > mouse_PX && gameObjectScene_list[cntr].position_x < mouse_PX && gameObjectScene_list[cntr].position_y + 50 > mouse_PY && gameObjectScene_list[cntr].position_y < mouse_PY && e.Button == MouseButtons.Left)
                    {
                        GameObject_Scene_EDTIOR obj = new GameObject_Scene_EDTIOR(gameObjectScene_list[cntr]);
                        gameObject_editor.SelectedObject = obj;
                        isLcontrol = true;
                    }
                }
            }
        }

        public bool checkSorted(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.Length; cnt++)
            {
                if (cnt + 1 < index_array.Length)
                {
                    if (index_array[cnt].depth < index_array[cnt + 1].depth)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void sortElements(DrawableGameObject[] index_array)
        {
            for (int cnt = 0; cnt < index_array.Length; cnt++)
            {
                for (int c = cnt + 1; c < index_array.Length; c++)
                {
                    if (index_array[cnt].depth < index_array[c].depth)
                    {
                        DrawableGameObject cp = index_array[cnt];

                        index_array[cnt] = index_array[c];

                        index_array[c] = cp;

                        break;
                    }
                }
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {

                e.Graphics.Clear(current_level.back_color);

                /*      for (int y = 0; y < canvas.Height; y += zoom_rate)
                      {
                          for (int x = 0; x < canvas.Width; x += zoom_rate)
                          {
                              e.Graphics.DrawLine(new Pen(Brushes.Gray),new Point(x,0),new Point(x,canvas.Height)); // Up to down
                              e.Graphics.DrawLine(new Pen(Brushes.Gray),new Point(0,y),new Point(canvas.Width,y));  // Left to right
                          }
                      } */

                for (int cnt = 0; cnt < lines_array.Count; cnt++)
                {
                    e.Graphics.DrawLine(new Pen(Brushes.Gray), new Point(lines_array[cnt].x, 0), new Point(lines_array[cnt].x, canvas.Height)); // Up to down
                    e.Graphics.DrawLine(new Pen(Brushes.Gray), new Point(0, lines_array[cnt].y), new Point(canvas.Width, lines_array[cnt].y));  // Left to right
                }

                if (sortedArray == null)
                {
                    return;
                }

                for (int cnt = 0; cnt < sortedArray.Length; cnt++)
                {
                    int cntr = sortedArray[cnt].index;

                    if (gameObjectScene_list[cntr].mainObject.object_img != null)
                    {
                        e.Graphics.DrawImage(new Bitmap(gameObjectScene_list[cntr].mainObject.object_img, new Size(gameObjectScene_list[cntr].mainObject.object_img.Width + zoom_rate, gameObjectScene_list[cntr].mainObject.object_img.Height + zoom_rate)), new Point(gameObjectScene_list[cntr].position_scene_x, gameObjectScene_list[cntr].position_scene_y));
                    }
                    else if (gameObjectScene_list[cntr].mainObject.object_text != "")
                    {
                        e.Graphics.DrawString(gameObjectScene_list[cntr].mainObject.object_text, new Font("Verdana", 12), Brushes.Black, new Point(gameObjectScene_list[cntr].position_scene_x, gameObjectScene_list[cntr].position_scene_y));
                    }
                }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current level ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuItem_SaveLevel_Click(null, null);
            }

            menu_screen.Close();
            this.Close();
        }

        private void backToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current level ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuItem_SaveLevel_Click(null, null);
            }

            canClose = false;
            menu_screen.Visible = true;
            this.Close();
        }

        private void menuItem_NewProject_Click(object sender, EventArgs e)
        {
            canClose = false;
            menu_screen.Visible = true;
            NewProjectScreen proj_screen = new NewProjectScreen(menu_screen);

            proj_screen.Show();

            this.Close();
        }

        private void LoadProject()
        {
            StreamReader stm_rd = new StreamReader(project_default_dir + "\\" + project_name + ".prj");

            while(!stm_rd.EndOfStream)
            {
                ProcessLine(stm_rd.ReadLine());
            }

            stm_rd.Close();

            foreach (string path in Directory.GetFiles(project_default_dir + "\\Game-Objects"))
            {
              // Call LoadObject here with parameter path + .obj for providing extension.
                gameObject_list.Add(LoadObject(path));
            }

            stm_rd = new StreamReader(project_info.project_firstlevel);

            while(!stm_rd.EndOfStream)
            {
                ProcessLine(stm_rd.ReadLine());
            }

            stm_rd.Close();
        }

        delegate void processLine(string line);

        private GameObject LoadObject(string path)
        {
            GameObject newObject = new GameObject( );
            string cur_action = "";

            newObject.scripts = new List<string>();

            processLine processLine_code = delegate(string line)
            {
                List<object> token_list = new List<object>();
                string cur_token = "";
                bool isString = false;

                foreach (char ch in line)
                {
                    if (isString)
                    {
                        if (ch != '@')
                        {
                            cur_token += ch;
                        }
                        else
                        {
                            isString = false;
                        }
                    }
                    else
                    {
                        if (ch == ' ')
                        {
                            if (cur_token != "")
                            {
                                token_list.Add(cur_token);
                                cur_token = "";
                            }
                        }
                        else if (ch == '@')
                        {
                            isString = true;
                            cur_token = "@";
                        }
                        else
                        {
                            cur_token += ch;
                        }
                    }
                }

                if (cur_token != "")
                {
                    token_list.Add(cur_token);
                }

                foreach (object obj in token_list)
                {
                    if (cur_action == "")
                    {
                        if ((string) obj == "Name:")
                        {
                            cur_action = "Name";
                        }
                        else if ((string ) obj == "Image:")
                        {
                            cur_action = "Image";
                        }
                        else if ((string) obj == "Text:")
                        {
                            cur_action = "Text";
                        }
                        else if ((string)obj == "Tag:")
                        {
                            cur_action = "Tag";
                        }
                        else if ((string)obj == "Static:")
                        {
                            cur_action = "isStatic";
                        }
                        else if ((string) obj == "Physics:")
                        {
                            cur_action = "isPhysics";
                        }
                        else if ((string) obj == "RigidBody:")
                        {
                            cur_action = "isRigidBody";
                        }
                        else if ((string) obj == "Collider:")
                        {
                            cur_action = "isCollider";
                        }
                        else if ((string)obj == "Scripts:")
                        {
                            cur_action = "_Scripts";
                        }
                    }
                    else
                    {
                        if (cur_action == "Name")
                        {
                            string str = (string)obj;

                            if (str.Substring(0,1) == "@")
                            {
                                newObject.object_name = str.Substring(1, str.Length - 1);
                            }
                            else
                            {
                                newObject.object_name = str;
                            }

                            cur_action = "";
                        }
                        else if (cur_action == "Image")
                        {
                            string str = (string)obj;

                            if (str != "@Game-Resouces\\")
                            {
                                if (File.Exists(this.project_default_dir + "\\" + str.Substring(1, str.Length - 1)))
                                {
                                    newObject.path = str.Substring(1, str.Length - 1);
                                    newObject.object_img = Image.FromFile(this.project_default_dir + "\\" + str.Substring(1, str.Length - 1));
                                }
                                else
                                {
                                    MessageBox.Show(this.project_default_dir + "\\" + str.Substring(1, str.Length - 1) + " not found!", "Engine Crash!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                }

                            }

                            cur_action = "";
                        }
                        else if (cur_action == "Text")
                        {
                            string str = (string)obj;

                            if (str.Substring(0,1) == "@")
                            {
                                newObject.object_text = str.Substring(1, str.Length - 1);
                            }

                            cur_action = "";
                        }
                        else if (cur_action == "Tag")
                        {
                            string str = (string)obj;

                            newObject.object_tag = int.Parse("0" + str);

                            cur_action = "";
                        }
                        else if (cur_action == "isStatic")
                        {
                            string str = (string)obj;

                            newObject.isStatic = bool.Parse(str);

                            cur_action = "";
                        }
                        else if (cur_action == "isPhysics")
                        {
                            string str = (string)obj;

                            newObject.object_physics = bool.Parse(str);

                            cur_action = "";
                        }
                        else if (cur_action == "isRigidBody")
                        {
                            string str = (string)obj;

                            newObject.object_rigid = bool.Parse(str);

                            cur_action = "";
                        }
                        else if (cur_action == "isCollider")
                        {
                            string str = (string)obj;

                            newObject.object_collider = bool.Parse(str);

                            cur_action = "";
                        }
                        else if (cur_action == "_Scripts")
                        {
                            string str = (string)obj;
                            string pth = str.Substring(1, str.Length - 1);

                            if (str == ";")
                            {
                                cur_action = "";
                                continue;
                            }

                            if (File.Exists(this.project_default_dir + "\\" + pth))
                            {
                               // MessageBox.Show(str.Substring(1, str.Length - 1));
                                newObject.scripts.Add(this.project_default_dir + "\\" + pth);
                            }
                            else
                            {
                                MessageBox.Show(pth + " script not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }
                        }
                    }
                }

            };

            StreamReader stm_rd = new StreamReader(path);

            while(!stm_rd.EndOfStream)
            {
                processLine_code(stm_rd.ReadLine());
            }

            stm_rd.Close();

            return newObject;
        }

        private void ProcessLine(string line)
        {
            List<object> token_list = new List<object>();
            GameObject_Scene gameObject = new GameObject_Scene();
            string cur_token = "";
            bool isString = false;

            foreach(char ch in line)
            {
                if (isString)
                {
                    if (ch == '@')
                    {
                        isString = false;
                        token_list.Add(cur_token);
                        cur_token = "";
                    }
                    else
                    {
                        cur_token += ch;
                    }
                }
                else
                {
                    if (ch == ' ')
                    {
                        if (cur_token != "")
                        {
                            token_list.Add(cur_token);
                            cur_token = "";
                        }
                    }
                    else if (ch == '@')
                    {
                        if (!isString)
                        {
                            isString = true;
                            cur_token = "@";
                        }
                    }
                    else
                    {
                        cur_token += ch;
                    }
                }
            }

            if (cur_token != "")
            {
                token_list.Add(cur_token);
            }

            string cur_action = "";

            foreach(object obj in token_list)
            {
                if (cur_action == "")
                {
                    if ((string) obj ==  "Project_Name:")
                    {
                        cur_action =  "Project_Name";
                    }
                    else if ((string) obj == "Project_Version:")
                    {
                        cur_action = "Project_Version";
                    }
                    else if ((string) obj == "Project_Author:")
                    {
                        cur_action = "Project_Author";
                    }
                    else if ((string) obj == "Project_About:")
                    {
                        cur_action = "Project_About";
                    }
                    else if ((string) obj == "Project_FirstLevel:")
                    {
                        cur_action = "Project_FirstLevel";
                    }
                    else if ((string) obj == "Level_Name:")
                    {
                        cur_action = "Level_Name";
                    }
                    else if ((string) obj == "Speed:")
                    {
                        cur_action = "Speed";
                    }
                    else if ((string) obj == "Back_Colour:")
                    {
                        cur_action = "Back_Colour";
                    }
                    else if ((string) obj == "Object:")
                    {
                        cur_action = "Object";
                    }
                    // Object loading left with level-object loading.
                } 
                else
                {
                    if (cur_action == "Project_Name")
                    {
                        string str = (string) obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            project_info.project_name = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Project_Version")
                    {
                        string str = (string) obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            project_info.project_version = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Project_Author")
                    {
                        string str = (string)obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            project_info.project_author = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Project_About")
                    {
                        string str = (string)obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            project_info.project_about = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Project_FirstLevel")
                    {
                        string str = (string)obj;

                        if (File.Exists(project_default_dir + "\\" + str))
                        {
                            project_info.project_firstlevel = project_default_dir + "\\" + str;
                        }
                        else
                        {
                            MessageBox.Show("The default level file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Level_Name")
                    {
                        string str = (string)obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            current_level.level_name = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Speed")
                    {
                        string str = (string)obj;
                        current_level.level_speed = int.Parse(str);
                        cur_action = "";
                    }
                    else if (cur_action == "Back_Colour")
                    {
                        string str = (string)obj;
                        current_level.back_color = Color.FromArgb(int.Parse(str), 0, 0, 0);
                        cur_action = "Back_ColourR";
                    }
                    else if (cur_action == "Back_ColourR")
                    {
                        string str = (string)obj;
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, int.Parse(str), 0, 0);
                        cur_action = "Back_ColourG";
                    }
                    else if (cur_action == "Back_ColourG")
                    {
                        string str = (string)obj;
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, current_level.back_color.R, int.Parse(str), 0);
                        cur_action = "Back_ColourB";
                    }
                    else if (cur_action == "Back_ColourB")
                    {
                        string str = (string)obj;
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, current_level.back_color.R, current_level.back_color.G, int.Parse(str));
                        cur_action = "";
                    }
                    else if (cur_action == "Object")
                    {
                        string str = (string)obj;

                      //  MessageBox.Show(str.Substring(1, str.Length - 1));

                        if (str.Substring(0,1) == "@")
                        {
                            foreach(GameObject gameObj in gameObject_list)
                            {
                                if (gameObj.object_name == str.Substring(1,str.Length - 1))
                                {
                                    gameObject.mainObject = gameObj;
                                }
                            }
                        }

                        cur_action = "Object_NAME";
                    }
                    else if (cur_action == "Object_NAME")
                    {
                        string str = (string)obj;

                        if (str.Substring(0,1) == "@")
                        {
                            gameObject.instance_name = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "Object_PositionX";
                    }
                    else if (cur_action == "Object_PositionX")
                    {
                        string str = (string)obj;

                        gameObject.position_x = int.Parse(str);
                        gameObject.position_scene_x = gameObject.position_x;

                        cur_action = "Object_PositionY";
                    }
                    else if (cur_action == "Object_PositionY")
                    {
                        string str = (string)obj;

                        gameObject.position_y = int.Parse(str);
                        gameObject.position_scene_y = gameObject.position_y;

                        cur_action = "Object_Depth";
                    }
                    else if (cur_action == "Object_Depth")
                    {
                        string str = (string)obj;

                        gameObject.depth = int.Parse(str);

                        gameObjectScene_list.Add(gameObject);
                        gameObject = new GameObject_Scene();
                    }
                }
            }
        }

        private void menuItem_SaveProject_Click(object sender, EventArgs e)
        {
            StreamWriter stm_wr = new StreamWriter(project_default_dir + "\\" + project_name + ".prj");

            stm_wr.WriteLine("Project_Name: @" + project_info.project_name);
            stm_wr.WriteLine("Project_Author: @" + project_info.project_author);
            stm_wr.WriteLine("Project_Version: @" + project_info.project_version);
            stm_wr.WriteLine("Project_About: @" + project_info.project_about);
            stm_wr.WriteLine("Project_FirstLevel: Game-Levels\\" + Path.GetFileName(project_info.project_firstlevel));

            stm_wr.Flush();

            stm_wr.Close();

            MessageBox.Show("Project Saved Succesfully!", "Succesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItem_LoadProject_Click(object sender, EventArgs e)
        {
            canClose = false;
            Form loadproject_window = new LoadProjectScreen(menu_screen, this);
            loadproject_window.Show();
        }


        private void menuItem_LoadLevel_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_browser = new OpenFileDialog();
            
            file_browser.InitialDirectory = project_default_dir + "\\Game-Levels";

            file_browser.ShowDialog();

            if (file_browser.FileName == "") return;

            if (Path.GetExtension(file_browser.FileName) == ".hvl" || Path.GetExtension(file_browser.FileName) == "hvl")
            {
                menuItem_NewLevel_Click(null, null);

                LoadLevel(file_browser.FileName);

                sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    sortedArray[cnt].depth = gameObjectScene_list[cnt].depth;
                    sortedArray[cnt].index = cnt;
                }

                while (!checkSorted(sortedArray))
                {
                    sortElements(sortedArray);
                }

                lb_objects.Items.Clear();

                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    lb_objects.Items.Add(gameObjectScene_list[cnt].instance_name);
                }
            }
            else
            {
                MessageBox.Show("Cannot open unknown format files !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLevel(string path)
        {
            StreamReader stm_rd = new StreamReader(path);

            while(!stm_rd.EndOfStream)
            {
                ProcessLine(stm_rd.ReadLine());
            }

            stm_rd.Close();
        }

        private void menuItem_SaveLevel_Click(object sender, EventArgs e)
        {
            StreamWriter stm_wr = new StreamWriter(project_default_dir + "\\Game-Levels\\" + current_level.level_name + ".hvl");

            stm_wr.WriteLine("Level_Name: @" + current_level.level_name);
            stm_wr.WriteLine("Speed: " + current_level.level_speed);
            stm_wr.WriteLine("Back_Colour: " + current_level.back_color.A + " " + current_level.back_color.R + " " + current_level.back_color.G + " " + current_level.back_color.B);
            // Loop for printing object position.
            foreach(GameObject_Scene gameObject in gameObjectScene_list)
            {
                stm_wr.WriteLine("Object: @" + gameObject.mainObject.object_name + "@ @" + gameObject.instance_name + "@ " + gameObject.position_x + " " + gameObject.position_y + " " + gameObject.depth); 
            }

            stm_wr.Flush();
            stm_wr.Close();
        }

        private void menuItem_LevelManager_Click(object sender, EventArgs e)
        {
            Form level_manager = new LevelManager(this);

            level_manager.Show();
        }

        public struct GameLevel
        {
            public string level_name;
            public int level_speed;
            public Color back_color;
            // Objects also get a part.

            public void NewLevel()
            {
                level_name = "Untitled";
                level_speed = 0;
                back_color = Color.FromArgb(0, 0, 0, 0);
                //  Clear all objects.
            }
        }
        public struct ProjectInfo
        {
            public string project_name;
            public string project_author;
            public string project_version;
            public string project_firstlevel;
            public string project_about;
        }

        public struct GameObject
        {
            public string object_name;
            public string path;
            public Image object_img;
            public string object_text;
            public List<string> scripts;
            public int object_tag;
            public bool isStatic;
            public bool object_physics;
            public bool object_rigid;
            public bool object_collider;
        }

        public struct GameObject_Scene
        {
            public string instance_name;
            public GameObject mainObject;
            public int position_x;
            public int position_y;
            public int position_scene_x;
            public int position_scene_y;
            public int depth;
        }

        public struct DrawableGameObject
        {
            public int depth;
            public int index;
        }

        private void menuItem_ProjectManager_Click(object sender, EventArgs e)
        {
            Form window_handle = new ProjectManager(this);
            window_handle.Show();
        }

        private void menuItem_CreateObject_Click(object sender, EventArgs e)
        {
            CreateObject window_handle = new CreateObject(this);

            window_handle.Show();
        }

        private void menuItem_AddObject_Click(object sender, EventArgs e)
        {
            AddObject window_handle = new AddObject(this);
            window_handle.Show();
        }

        private void menuItem_ObjectManager_Click(object sender, EventArgs e)
        {
            ObjectManager obj_man = new ObjectManager(this);
            obj_man.Show();
        }

        private void menuItem_ImportResource_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

           // open_file_dlg.InitialDirectory = project_default_dir + "\\Game-Resources";
            open_file_dlg.ShowDialog();

            if (open_file_dlg.FileName != "")
            {
                File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Resouces\\" + Path.GetFileName(open_file_dlg.FileName));
            }

            reloadFileTree();
        }

        private void tmr_draw_Tick(object sender, EventArgs e)
        {
            lbl_view_x.Text = "Camera X : " + cam_x;
            lbl_view_y.Text = "Camera Y : " + cam_y;

           if (this.WindowState != FormWindowState.Maximized) this.WindowState = FormWindowState.Maximized;

            this.canvas.Refresh( );
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject != null)
            {
                GameObject_Scene_EDTIOR obj = (GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject;
                
                for(int cntr = 0;cntr < gameObjectScene_list.Count;cntr++)
                {
                   if (obj._obj.instance_name == gameObjectScene_list[cntr].instance_name)
                   {
                       for (int cnt = 0; cnt < gameObjectScene_list.Count;cnt++)
                       {
                           if (obj._obj.instance_name == gameObjectScene_list[cnt].instance_name)
                           {
                               break;
                           }

                           if (obj.obj.instance_name == gameObjectScene_list[cnt].instance_name)
                           {
                               MessageBox.Show("The name is already registered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                               return;
                           }
                       }

                       int scene_px = gameObjectScene_list[cntr].position_scene_x,scene_py = gameObjectScene_list[cntr].position_scene_y;

                       gameObjectScene_list[cntr] = obj.obj;

                      GameObject_Scene handle = gameObjectScene_list[cntr];

                       handle.position_scene_x = scene_px;
                       handle.position_scene_y = scene_py;

                       gameObjectScene_list[cntr] = handle;

                       break;
                   }
                }

                gameObject_editor.SelectedObject = null;
                isLcontrol = false;
            }
        }

        private void menuItem_ResouceManager_Click(object sender, EventArgs e)
        {
            ResourceManager res_manager = new ResourceManager(this);

            res_manager.Show();
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            
        }

        private void menuItem_NewScript_Click(object sender, EventArgs e)
        {
            NewScript newScript_window = new NewScript(this);

            newScript_window.Show();
        }

        private void menuItem_RunProject_Click(object sender, EventArgs e)
        {
            GameBuilder game_build = new GameBuilder();

            if (game_build.BuildGame(this))
            {
                if (platform_id == 1 || platform_id == 4)
                {
                    Process process_handle = System.Diagnostics.Process.Start(project_default_dir + "\\Build\\" + project_info.project_name + ".exe");
                re:
                    if (!process_handle.HasExited) goto re;
                }
                else
                {
                    MessageBox.Show("Java platform programs cannot run directly ! Goto to build directory in your project folder and run Build.bat to get a executable jar file","Message",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

        private void menuItem_ScriptManager_Click(object sender, EventArgs e)
        {
            ScriptManager script_manager = new ScriptManager(this);

            script_manager.Show();
        }

        private void menuItem_BuildProject_Click(object sender, EventArgs e)
        {
            GameBuilder game_build = new GameBuilder();

            game_build.BuildGame(this);
        }

        private void menuItem_AboutHeavyEngine_Click(object sender, EventArgs e)
        {
            AboutHeavyEngine about_window = new AboutHeavyEngine();

            about_window.Show();
        }

        private void menuItem_ImportPlugins_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.ShowDialog();
            
            if (open_file_dlg.FileName != "")
            {
                if (platform_id == 1)
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".dll" || Path.GetExtension(open_file_dlg.FileName) == "dll")
                    {
                        File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Plugins\\" + Path.GetFileName(open_file_dlg.FileName));
                    }
                }
                else if (platform_id == 2 || platform_id == 3)
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".jar" || Path.GetExtension(open_file_dlg.FileName) == "jar")
                    {
                        File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Plugins\\" + Path.GetFileName(open_file_dlg.FileName));
                    }
                }
                else
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".lib" || Path.GetExtension(open_file_dlg.FileName) == "lib" || Path.GetExtension(open_file_dlg.FileName) == ".dll" || Path.GetExtension(open_file_dlg.FileName) == "dll")
                    {
                        File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Plugins\\" + Path.GetFileName(open_file_dlg.FileName));
                    }
                }
            }
        }

        private void menuItem_PluginsManager_Click(object sender, EventArgs e)
        {
            PluginManager plugin_window = new PluginManager(this);

            plugin_window.Show();
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                if (gameObjectScene_list[cnt].mainObject.object_text == "")
                {
                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_y += 5;

                    gameObjectScene_list[cnt] = handle;
                }
            }

            cam_y -= 5;
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                if (gameObjectScene_list[cnt].mainObject.object_text == "")
                {

                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_y -= 5;

                    gameObjectScene_list[cnt] = handle;
                }
            }

            cam_y += 5;
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                if (gameObjectScene_list[cnt].mainObject.object_text == "")
                {

                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_x += 5;

                    gameObjectScene_list[cnt] = handle;
                }
            }

            cam_x -= 5;
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                if (gameObjectScene_list[cnt].mainObject.object_text == "")
                {

                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_x -= 5;

                    gameObjectScene_list[cnt] = handle;
                }
            }

            cam_x += 5;
        }

        private void zoomIn(int rate)
        {
                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_x -= 5 * rate;
                    handle.position_scene_y -= 5 * rate;

                    gameObjectScene_list[cnt] = handle;
                }

                calculateLines();
        }

        private void zoomIn_Checked()
        {
                if (zoom_rate < 50)
           {
               zoom_rate += 5;
            
              if (zoom_rate > 50)
               {
                   zoom_rate = 50;
               } 

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                GameObject_Scene handle = gameObjectScene_list[cnt];

                handle.position_scene_x -= 5;
                handle.position_scene_y -= 5;

                gameObjectScene_list[cnt] = handle;
            }

            calculateLines();
            }
        }

        private void zoomOut_Checked()
        {
             if (zoom_rate > 10)
             {
                 zoom_rate -= 5;

              if (zoom_rate < 10)
               {
                   zoom_rate = 10;
               } 

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                GameObject_Scene handle = gameObjectScene_list[cnt];

                handle.position_scene_x += 5;
                handle.position_scene_y += 5;

                gameObjectScene_list[cnt] = handle;
            }

            calculateLines();
               }
        }

        private void zoomOut(int rate)
        {
                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    GameObject_Scene handle = gameObjectScene_list[cnt];

                    handle.position_scene_x += 5 * rate;
                    handle.position_scene_y += 5 * rate;

                    gameObjectScene_list[cnt] = handle;
                }

                calculateLines();
        }

        private void menuItem_open_editor_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string path in Directory.GetFiles(project_default_dir + "\\Game-Scripts"))
                {
                    if (Path.GetExtension(path) == ".bs" || Path.GetExtension(path) == "bs")
                    {
                        Process.Start("notepad++", path);
                    }
                    else if (platform_id == 1)
                    {
                        if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == "cs")
                        {
                            Process.Start("notepad++", path);
                        }
                    }
                    else if (platform_id == 2 || platform_id == 3)
                    {
                        if (Path.GetExtension(path) == ".java" || Path.GetExtension(path) == "java")
                        {
                            Process.Start("notepad++", path);
                        }
                    }
                    else if (platform_id == 4)
                    {
                        if (Path.GetExtension(path) == ".cpp" || Path.GetExtension(path) == "cpp" || Path.GetExtension(path) == ".h" || Path.GetExtension(path) == "h")
                        {
                            Process.Start("notepad++", path);
                        }
                    }
                }
            }
            catch (Win32Exception eax)
            {
                MessageBox.Show("Notepad++ Editor not found !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void menuItem_ObjectEditor_Click(object sender, EventArgs e)
        {
            ObjectEditor obj_editor = new ObjectEditor(this,"");

            obj_editor.Show();
        }

       private void menuItem_Build_Options_Click(object sender, EventArgs e)
        {
            BuildOptions build_options = new BuildOptions(this);

            build_options.Show();
        }

       private void menuItem_NewLevel_Click(object sender, EventArgs e)
       {
           tmr_draw.Enabled = false;
           gameObjectScene_list.Clear();
           lb_objects.Items.Clear();
           cam_x = 0;
           cam_y = 0;
           isLcontrol = false;
           current_level.NewLevel();
           sortedArray = new DrawableGameObject[0];
           tmr_draw.Enabled = true;
           zoom_rate = 10;
           calculateLines();
       }

       private void btn_cancel_Click(object sender, EventArgs e)
       {
           if (gameObject_editor.SelectedObject == null) return;

           for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
           {
               if (gameObjectScene_list[cnt].instance_name == ((GameObject_Scene_EDTIOR) gameObject_editor.SelectedObject)._obj.instance_name)
               {
                   gameObjectScene_list[cnt] = ((GameObject_Scene_EDTIOR)gameObject_editor.SelectedObject)._obj;
                   break;
               }
           }

           isLcontrol = false;
           gameObject_editor.SelectedObject = null;
       }

       private void lbl_view_y_Click(object sender, EventArgs e)
       {

       }

       private void gameObject_editor_Click(object sender, EventArgs e)
       {

       }

       private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
       {
           
       }

       private void lb_objects_SelectedIndexChanged(object sender, EventArgs e)
       {

       }

       private void file_tree_AfterSelect(object sender, TreeViewEventArgs e)
       {

       }

       private void menuItem_OpenEditor_Click(object sender, EventArgs e)
       {
           NavigationEditor nav_editor = new NavigationEditor(this,"");

           nav_editor.Show();
       }

       private void menuItem_AOpenEditor_Click(object sender, EventArgs e)
       {
           AnimationEditor anim_editor = new AnimationEditor(this,"");

           anim_editor.Show();
       }

       private void menuItem_ImportHeader_Click(object sender, EventArgs e)
       {
           OpenFileDialog open_file_dlg = new OpenFileDialog();

           open_file_dlg.ShowDialog();

           if (open_file_dlg.FileName != "")
           {
                if (Path.GetExtension(open_file_dlg.FileName) == ".h" || Path.GetExtension(open_file_dlg.FileName) == "h")
                {
                     File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Scripts\\" + Path.GetFileName(open_file_dlg.FileName));
                }
           }
       }

       private void tb_zoom_Scroll(object sender, EventArgs e)
       {
           int cp = zoom_rate;

           zoom_rate = tb_zoom.Value * 5;

           if (cp < tb_zoom.Value * 5)
           {
               zoomIn(tb_zoom.Value - (cp / 5));
           }
           else
           {
               zoomOut((cp / 5) - tb_zoom.Value);
           }
       }
    }

    class GameObject_Scene_EDTIOR
    {
        public Editor.GameObject_Scene obj;
        public Editor.GameObject_Scene _obj;

        public GameObject_Scene_EDTIOR(Editor.GameObject_Scene obj)
        {
            this.obj = obj;
            this._obj = obj;
        }
        
        public int X
        {
            get
            {
                return obj.position_x;
            }
            set
            {
                obj.position_x = value;
            }
        }

        public int Y
        {
            get
            {
                return obj.position_y;
            }
            set
            {
                obj.position_y = value;
            }
        }

        public int Depth
        {
            get
            {
                return obj.depth;
            }
            set
            {
                obj.depth = value;
            }
        }

        public string NAME
        {
            get
            {
                return obj.instance_name;
            }
            set
            {
                obj.instance_name = value;
            }
        }
    }

    class GameBuilder
    {
        class Animation
        {
            public int animation_speed;
            public List<string> frame_list = new List<string>();
        }

        LogWindow log_window;

        private List<NavigationPoint> getNavigationPoints(string path )
        {
            StreamReader stm_rd = new StreamReader(path);
            List<string> token_list = new List<string>();
            List<NavigationPoint> ret_list = new List<NavigationPoint>();

            while (!stm_rd.EndOfStream)
            {
                string cur_line = stm_rd.ReadLine();
                string cur_token = "";

                foreach (char ch in cur_line)
                {
                    if (ch == ',')
                    {
                        if (cur_token != "")
                        {
                            token_list.Add(cur_token);
                        }

                        cur_token = "";
                    }
                    else
                    {
                        cur_token += ch;
                    }
                }


                if (cur_token != "")
                {
                    token_list.Add(cur_token);
                }
            }

            stm_rd.Close();

            NavigationPoint cur_point = null;
            string cur_action = "";

            foreach (string str in token_list)
            {
                if (cur_action == "")
                {
                    cur_point = new NavigationPoint();
                    cur_point.position_x = int.Parse(str);
                    cur_action = "get_posy";
                }
                else
                {
                    if (cur_action == "get_posy")
                    {
                        cur_point.position_y = int.Parse(str);
                        cur_action = "";

                        ret_list.Add(cur_point);
                    }
                }
            }

            return ret_list;
        }

        private Animation getAnimation(Editor editor_handle, string path)
        {
            StreamReader stm_rd = new StreamReader(path);
            List<string> token_list = new List<string>();

            while (!stm_rd.EndOfStream)
            {
                string line = stm_rd.ReadLine();
                bool isString = false;
                string cur_token = "";

                foreach (char ch in line)
                {
                    if (isString)
                    {
                        if (ch == '@')
                        {
                            isString = false;
                            token_list.Add(cur_token);
                            cur_token = "";
                        }
                        else
                        {
                            cur_token += ch;
                        }
                    }
                    else
                    {
                        if (ch == ' ')
                        {
                            if (cur_token != "")
                            {
                                token_list.Add(cur_token);
                                cur_token = "";
                            }
                        }
                        else if (ch == '@')
                        {
                            if (!isString)
                            {
                                isString = true;
                                cur_token = "@";
                            }
                        }
                        else
                        {
                            cur_token += ch;
                        }
                    }
                }

                if (cur_token != "")
                {
                    token_list.Add(cur_token);
                }
            }

            string cur_action = "";
            Animation ret_anim = new Animation();

            foreach (string str in token_list)
            {
                if (cur_action == "")
                {
                    if (str == "Speed:")
                    {
                        cur_action = "Speed";
                    }
                    else if (str == "Frames:")
                    {
                        cur_action = "Frames";
                    }
                }
                else
                {
                    if (cur_action == "Speed")
                    {
                        ret_anim.animation_speed = int.Parse(str);

                        cur_action = "";
                    }
                    else if (cur_action == "Frames")
                    {
                        if (str == ";")
                        {
                            cur_action = "";
                            continue;
                        }

                        if (File.Exists(editor_handle.project_default_dir + "\\Game-Resouces\\" + str.Substring(1, str.Length - 1)))
                        {
                            ret_anim.frame_list.Add(editor_handle.project_default_dir + "\\Game-Resouces\\" + str.Substring(1, str.Length - 1));
                        }
                        else
                        {
                            MessageBox.Show("Error loading animation file ! Frame not found : " + str.Substring(1, str.Length - 1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            return ret_anim;
        }

        string encryptFileName(string base_string)
        {
            string out_string = "";

            if (String.IsNullOrEmpty(base_string)) return "";

            base_string = base_string.ToUpper();
            
            for (int cnt = 0; cnt < base_string.Length; cnt++)
            {
                char cur_ch = base_string[cnt];

                if (cur_ch < 65 || cur_ch > 91)
                {
                    out_string += (int)2;
                    out_string += cur_ch; // Avoid Digits.
                }
                else if (cur_ch + 10 <= 91)
                {
                    out_string += (int)0;
                    out_string += (char)(cur_ch + 10);
                    out_string += (int)(91 - (cur_ch + 10));

                    if (91 - (cur_ch + 10) < 10)
                    {
                        out_string += (int)0;
                    }
                }
                else if (cur_ch - 10 >= 65)
                {
                    out_string += (int)1;
                    out_string += (char)(cur_ch - 10);
                    out_string += (int)((cur_ch - 10) - 65);

                    if ((cur_ch - 10) - 65 < 10)
                    {
                        out_string += (int)0;
                    }
                }
            }

            return out_string;
        }

        delegate void processLine(string line); 
        public bool BuildGame(Editor editor_handle)
        {
           // Compile Scripts
            List<string> rem_files = new List<string>();
            BuildProgress build_progress = new BuildProgress();
            log_window = new LogWindow();

            build_progress.Show();
            build_progress.Focus();

            log_window.addLog("Started Compiling Boch Scripts ....");

            foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
            {
                if (Path.GetExtension(path) == ".bs" || Path.GetExtension(path) == "bs")
                {
                    string platform_ext = "";

                    if (editor_handle.platform_id == 1) platform_ext = ".cs"; else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3) platform_ext = ".java"; else platform_ext = ".cpp";

                    Process compiler = Process.Start(Application.StartupPath + "\\bc.exe", "-p " + editor_handle.platform_id + " -o " + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + platform_ext + " " + path);

                wt:
                    if (!compiler.HasExited) goto wt;

                    if (compiler.ExitCode != 0x1)
                    {
                        MessageBox.Show("Error found in bos scripts!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        build_progress.Close();
                        return false;
                    }

                    rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + platform_ext);
                }
            }

            build_progress.progress();
            Thread.Sleep(500);

            log_window.addLog("Boch Scripts Compile Finished...");

            log_window.addLog("Started Generating Navigation Codes...");

            foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Navigation"))
            {
                if (Path.GetExtension(path) == ".nav" || Path.GetExtension(path) == "nav")
                {
                    string platform_ext = "";

                    if (editor_handle.platform_id == 1) platform_ext = ".cs"; else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3) platform_ext = ".java"; else platform_ext = ".cpp";

                    StreamWriter file_writer = new StreamWriter(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + "_Navigator" + platform_ext);

                    List<NavigationPoint> nav_points = getNavigationPoints(path);

                    if (editor_handle.platform_id == 1)
                    {
                        file_writer.WriteLine("using Runtime;");
                        file_writer.WriteLine("public class " + Path.GetFileNameWithoutExtension(path) + "_Navigator : Navigator {");
                        file_writer.WriteLine("public " + Path.GetFileNameWithoutExtension(path) + "_Navigator(GameObject_Scene baseObject,int navigation_speed) : base(baseObject,navigation_speed) { ");
                    }
                    else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                    {
                        if (editor_handle.platform_id == 3) file_writer.WriteLine("package bin;");

                        file_writer.WriteLine("import jruntime.*;");
                        file_writer.WriteLine("public class " + Path.GetFileNameWithoutExtension(path) + "_Navigator extends Navigator {");
                        file_writer.WriteLine("public " + Path.GetFileNameWithoutExtension(path) + "_Navigator(GameObject_Scene baseObject,int navigation_speed) { ");
                        file_writer.WriteLine("super(baseObject,navigation_speed);");
                    }
                    else if (editor_handle.platform_id == 4)
                    {
                        // Header File.
                        StreamWriter header_writer = new StreamWriter(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" " , "") + "_Navigator.h");

                        header_writer.WriteLine("#ifndef " + Path.GetFileNameWithoutExtension(path).Replace(' ', '_').ToUpper( ) + "_NAVIGATOR_H");
                        header_writer.WriteLine("#define " + Path.GetFileNameWithoutExtension(path).Replace(' ', '_').ToUpper() + "_NAVIGATOR_H");
                        header_writer.WriteLine("#include<Navigator.h>");
                        header_writer.WriteLine("class " + Path.GetFileNameWithoutExtension(path).Replace(' ','_') + "_Navigator : public Navigator {");
                        header_writer.WriteLine("public:");
                        // Public members and functions.
                        header_writer.WriteLine(Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Navigator(GameObject_Scene* , int );");
                        header_writer.WriteLine("private:");
                        // Private members and functions.
                        header_writer.WriteLine("};");
                        header_writer.WriteLine("#endif");

                        header_writer.Flush();
                        header_writer.Close();

                        // Source file.
                        file_writer.WriteLine("#include \"" + Path.GetFileNameWithoutExtension(path).Replace(" ","") + "_Navigator.h\"");
                        file_writer.WriteLine(Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Navigator::" + Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Navigator(GameObject_Scene* baseObject,int navigation_speed) : Navigator(baseObject,navigation_speed) {");
                    }

                    foreach (NavigationPoint point in nav_points)
                    {
                        if (editor_handle.platform_id != 4) file_writer.WriteLine("this.addPoint( new Vector2(" + point.position_x + "," + point.position_y + "));");
                        else file_writer.WriteLine("this->addPoint({" + point.position_x + "," + point.position_y + "});");
                    }

                    file_writer.WriteLine("}"); // End of constructor.
                    if (editor_handle.platform_id != 4) file_writer.WriteLine("}"); // End of class.

                    file_writer.Flush();

                    file_writer.Close();

                    rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + platform_ext);
                    if (editor_handle.platform_id == 4) rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" " , "") + "_Navigator.h");
                }
            }

            Thread.Sleep(250);

            log_window.addLog("Navigation Codes Generation Complete...");

            log_window.addLog("Started Generating Animation Codes...");

            foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Animation"))
            {
                if (Path.GetExtension(path) == ".anim" || Path.GetExtension(path) == "anim")
                {
                    string platform_ext = "";

                    if (editor_handle.platform_id == 1) platform_ext = ".cs"; else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3) platform_ext = ".java"; else platform_ext = ".cpp";

                    StreamWriter file_writer = new StreamWriter(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + "_Animation" + platform_ext);

                    Animation baseAnim = getAnimation(editor_handle,path);

                    if (editor_handle.platform_id == 1)
                    {
                        file_writer.WriteLine("using Runtime;");
                        file_writer.WriteLine("using System.Drawing;");
                        file_writer.WriteLine("using System.Windows.Forms;");
                        file_writer.WriteLine("public class " + Path.GetFileNameWithoutExtension(path) + "_Animation : Animation {");
                        file_writer.WriteLine("public " + Path.GetFileNameWithoutExtension(path) + "_Animation(GameObject_Scene baseObject,bool isRepeating) : base(baseObject," + baseAnim.animation_speed.ToString( ) + ",isRepeating) { ");

                        foreach (string anim_file in baseAnim.frame_list)
                        {
                            file_writer.WriteLine("this.addFrame(Image.FromFile(Application.StartupPath + \"\\\\Data\\\\" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file))  + ".X" +"\"));");
                        }
                    }
                    else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                    {
                        if (editor_handle.platform_id == 3) file_writer.WriteLine("package bin;");

                        file_writer.WriteLine("import jruntime.*;");
                        file_writer.WriteLine("import java.io.*;");
                        if (editor_handle.platform_id == 3) file_writer.WriteLine("import javax.microedition.lcdui.*;"); else file_writer.WriteLine("import javax.imageio.*;");
                        file_writer.WriteLine("public class " + Path.GetFileNameWithoutExtension(path) + "_Animation extends Animation {");
                        file_writer.WriteLine("public " + Path.GetFileNameWithoutExtension(path) + "_Animation(GameObject_Scene baseObject,boolean isRepeating) { ");
                        file_writer.WriteLine("super(baseObject," + baseAnim.animation_speed.ToString( ) + ",isRepeating);");
                        
                        foreach (string anim_file in baseAnim.frame_list)
                        {
                            if (editor_handle.platform_id == 3) file_writer.WriteLine("this.addFrame(Image.createImage(\"/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\"));"); else file_writer.WriteLine("this.addFrame(ImageIO.read(new File(\"/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\")));");
                        }
                    }
                    else if (editor_handle.platform_id == 4)
                    {
                        // Header File.
                        StreamWriter header_writer = new StreamWriter(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" ","") + "_Animation.h");

                        header_writer.WriteLine("#ifndef " + Path.GetFileNameWithoutExtension(path).Replace(' ', '_').ToUpper() + "_ANIMATION_H");
                        header_writer.WriteLine("#define " + Path.GetFileNameWithoutExtension(path).Replace(' ', '_').ToUpper() + "_ANIMATION_H");
                        header_writer.WriteLine("#include<Animation.h>");
                        header_writer.WriteLine("class " + Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Animation : public Animation {");
                        header_writer.WriteLine("public:");
                        // Public members and functions.
                        header_writer.WriteLine(Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Animation(GameObject_Scene* , bool );");
                        header_writer.WriteLine("private:");
                        // Private members and functions.
                        header_writer.WriteLine("};");
                        header_writer.WriteLine("#endif");

                        header_writer.Flush();
                        header_writer.Close();

                        // Source file.
                        file_writer.WriteLine("#include \"" + Path.GetFileNameWithoutExtension(path).Replace(" ","") + "_Animation.h\"");
                        file_writer.WriteLine(Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Animation::" + Path.GetFileNameWithoutExtension(path).Replace(' ', '_') + "_Animation(GameObject_Scene* baseObject,bool isRepeating) : Animation(baseObject," + baseAnim.animation_speed.ToString( ) + ",isRepeating) {");

                        foreach (string anim_file in baseAnim.frame_list)
                        {
                            file_writer.WriteLine("this->addFrame(\"./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\");");
                        }
                    }

                    file_writer.WriteLine("}"); // End of constructor.
                    if (editor_handle.platform_id != 4) file_writer.WriteLine("}"); // End of class.

                    file_writer.Flush();

                    file_writer.Close();

                    rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + platform_ext);
                    if (editor_handle.platform_id == 4) rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" ","") + "_Animation.h");
                }
            }

            Thread.Sleep(250);

            log_window.addLog("Animation Codes Generation Complete...");

            build_progress.progress();
            Thread.Sleep(500);

            log_window.addLog("Started Generating Base...");

            StreamWriter stm_wr = null;

            if (editor_handle.platform_id == 1)
            {
                stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\__build");
            }
            else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
            {
                stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\AppMain.java");
            }
            else
            {
                stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\AppMain.cpp");
            }

            StreamReader stm_rd = null; // new StreamReader(Application.StartupPath + "\\__code");
            List<Editor.GameObject_Scene> object_array = new List<Editor.GameObject_Scene>();
            Editor.GameLevel level = new Editor.GameLevel();
            Editor.GameObject_Scene gameObject = new Editor.GameObject_Scene();

            processLine processLine_code = delegate(string line)
            {
                 List<object> token_list = new List<object>();
            string cur_token = "";
            bool isString = false;

            foreach(char ch in line)
            {
                if (isString)
                {
                    if (ch == '@')
                    {
                        isString = false;
                        token_list.Add(cur_token);
                        cur_token = "";
                    }
                    else
                    {
                        cur_token += ch;
                    }
                }
                else
                {
                    if (ch == ' ')
                    {
                        if (cur_token != "")
                        {
                            token_list.Add(cur_token);
                            cur_token = "";
                        }
                    }
                    else if (ch == '@')
                    {
                        if (!isString)
                        {
                            isString = true;
                            cur_token = "@";
                        }
                    }
                    else
                    {
                        cur_token += ch;
                    }
                }
            }

            if (cur_token != "")
            {
                token_list.Add(cur_token);
            }

            string cur_action = "";

            foreach(object obj in token_list)
            {
                if (cur_action == "")
                {
                    if ((string) obj == "Level_Name:")
                    {
                        cur_action = "Level_Name";
                    }
                    else if ((string) obj == "Speed:")
                    {
                        cur_action = "Speed";
                    }
                    else if ((string) obj == "Back_Colour:")
                    {
                        cur_action = "Back_Colour";
                    }
                    else if ((string) obj == "Object:")
                    {
                        cur_action = "Object";
                    }
                    // Object loading left with level-object loading.
                } 
                else
                {
                     if (cur_action == "Level_Name")
                    {
                        string str = (string)obj;

                        if (str.Substring(0, 1) == "@")
                        {
                            level.level_name = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Speed")
                    {
                        string str = (string)obj;
                        level.level_speed = int.Parse(str);
                        cur_action = "";
                    }
                    else if (cur_action == "Back_Colour")
                    {
                        string str = (string)obj;
                        level.back_color = Color.FromArgb(int.Parse(str), 0, 0, 0);
                        cur_action = "Back_ColourR";
                    }
                    else if (cur_action == "Back_ColourR")
                    {
                        string str = (string)obj;
                        level.back_color = Color.FromArgb(level.back_color.A, int.Parse(str), 0, 0);
                        cur_action = "Back_ColourG";
                    }
                    else if (cur_action == "Back_ColourG")
                    {
                        string str = (string)obj;
                        level.back_color = Color.FromArgb(level.back_color.A, level.back_color.R, int.Parse(str), 0);
                        cur_action = "Back_ColourB";
                    }
                    else if (cur_action == "Back_ColourB")
                    {
                        string str = (string)obj;
                        level.back_color = Color.FromArgb(level.back_color.A, level.back_color.R, level.back_color.G, int.Parse(str));
                        cur_action = "";
                    }
                    else if (cur_action == "Object")
                    {
                        string str = (string)obj;

                      //  MessageBox.Show(str.Substring(1, str.Length - 1));

                        if (str.Substring(0,1) == "@")
                        {
                            foreach(Editor.GameObject gameObj in editor_handle.gameObject_list)
                            {
                                if (gameObj.object_name == str.Substring(1,str.Length - 1))
                                {
                                    gameObject.mainObject = gameObj;
                                }
                            }
                        }

                        cur_action = "Object_NAME";
                    }
                    else if (cur_action == "Object_NAME")
                    {
                        string str = (string)obj;

                        if (str.Substring(0,1) == "@")
                        {
                            gameObject.instance_name = str.Substring(1, str.Length - 1);
                        }

                        cur_action = "Object_PositionX";
                    }
                    else if (cur_action == "Object_PositionX")
                    {
                        string str = (string)obj;

                        gameObject.position_x = int.Parse(str);

                        cur_action = "Object_PositionY";
                    }
                    else if (cur_action == "Object_PositionY")
                    {
                        string str = (string)obj;

                        gameObject.position_y = int.Parse(str);

                        cur_action = "";
                        object_array.Add(gameObject);
                        gameObject = new Editor.GameObject_Scene();
                    }
                }
             }
            };

            // Copy Content of the defination file.
       /*     while(!stm_rd.EndOfStream)
            {
                stm_wr.WriteLine(stm_rd.ReadLine());
            }*/

            //stm_rd.Close();
            // Runtime change of canvas.
            
            if (editor_handle.platform_id == 1)
            {
                stm_wr.WriteLine("using System; \n using System.Drawing; \n using System.Windows.Forms; \n using System.Collections.Generic; \n using System.IO; \n using Joystick; using Runtime;");
                stm_wr.WriteLine("namespace App \n { \n public class AppMain \n { \n");

                stm_wr.WriteLine("public static void Main() \n {");
            }
            else if (editor_handle.platform_id == 2)
            {
                stm_wr.WriteLine("import java.awt.*; import javax.swing.*; import java.util.*; import java.io.*; import jruntime.*; \n");
                stm_wr.WriteLine("public class AppMain \n { \n ");
                stm_wr.WriteLine("public static void main(String args[]) { \n");
            }
            else if (editor_handle.platform_id == 3)
            {
                stm_wr.WriteLine("package bin;");
                stm_wr.WriteLine("import javax.microedition.lcdui.*; import javax.microedition.midlet.*; import java.util.*; import java.io.*; import jruntime.*; \n");
                stm_wr.WriteLine("public class AppMain extends MIDlet \n { \n ");
                stm_wr.WriteLine("public void destroyApp(boolean unconditional ) { } \n");
                stm_wr.WriteLine("public void pauseApp( ) {  } \n");
                stm_wr.WriteLine("public void startApp( ) { \n");
            }
            else if (editor_handle.platform_id == 4)
            {
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2main.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_image.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_ttf.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_mixer.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"NativeRuntime.lib\")");

                foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                {
                    if (Path.GetExtension(path) == ".lib" || Path.GetExtension(path) == "lib")
                    {
                        stm_wr.WriteLine("#pragma comment(lib,\"" + Path.GetFileName(path) + "\")");
                    }
                }

                stm_wr.WriteLine("#include<HeavyEngine.h>");
                stm_wr.WriteLine("#include \"Scripts.h\"");
                stm_wr.WriteLine("int main(int argc , char * argv[]) {");
            }

            if (editor_handle.platform_id == 3) stm_wr.WriteLine("HApplication.Initialize(\"" + editor_handle.project_info.project_name + "\",this);"); else if (editor_handle.platform_id == 1 || editor_handle.platform_id == 2) stm_wr.WriteLine("HApplication.Initialize(\"" + editor_handle.project_info.project_name + "\");"); else stm_wr.WriteLine("HApplication::Initialize(\"" + editor_handle.project_info.project_name + "\");");

            Thread.Sleep(100);

            log_window.addLog("Generating Objects...");

            for (int cntr = 0;cntr < editor_handle.gameObject_list.Count; cntr++)
            {
                if (editor_handle.platform_id == 1)
                {
                    stm_wr.WriteLine("ObjectManager.loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",Application.StartupPath + \"\\\\Data\\\\" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ");");
                }
                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                {
                    stm_wr.WriteLine("ObjectManager.loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ");");
                }
                else if (editor_handle.platform_id == 4)
                {
                    if (editor_handle.gameObject_list[cntr].object_img != null)
                    {
                        stm_wr.WriteLine("ObjectManager::loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_img.Width + "," + editor_handle.gameObject_list[cntr].object_img.Height + ");");
                    }
                    else
                    {
                        stm_wr.WriteLine("ObjectManager::loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ",0,0);");
                    }
                }
            }

            stm_wr.WriteLine("Scene firstScene;");
         
            if (Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels").Length > 1)
            {
                stm_wr.WriteLine("Scene newScene;");
            }

            if (editor_handle.platform_id != 4) stm_wr.WriteLine("GameObject_Scene obj;"); else stm_wr.WriteLine("GameObject_Scene * obj;");

            Thread.Sleep(100);

            log_window.addLog("Generating Levels...");

            foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels"))
            {
                stm_rd = new StreamReader(path);

                while (!stm_rd.EndOfStream)
                {
                    processLine_code(stm_rd.ReadLine());
                }

                if (level.level_name == Path.GetFileNameWithoutExtension(editor_handle.project_info.project_firstlevel))
                {
                    if (editor_handle.platform_id != 4) stm_wr.WriteLine("firstScene = new Scene( );"); else stm_wr.WriteLine("firstScene = Scene( );");
                    //stm_wr.WriteLine("obj = new GameObject_Scene( );");
                    stm_wr.WriteLine("firstScene.name = \"" + level.level_name + "\";");
                    stm_wr.WriteLine("firstScene.speed = " + level.level_speed + ";");
                    if (editor_handle.platform_id != 3) stm_wr.WriteLine("firstScene.A = " + level.back_color.A + ";");
                    stm_wr.WriteLine("firstScene.R = " + level.back_color.R + ";");
                    stm_wr.WriteLine("firstScene.G = " + level.back_color.G + ";");
                    stm_wr.WriteLine("firstScene.B = " + level.back_color.B + ";");
                }
                else
                {
                    if (editor_handle.platform_id != 4) stm_wr.WriteLine("newScene = new Scene( );"); else stm_wr.WriteLine("newScene = Scene( );");
                   // stm_wr.WriteLine("obj = new GameObject_Scene( );");
                    stm_wr.WriteLine("newScene.name = \"" + level.level_name + "\";");
                    stm_wr.WriteLine("newScene.speed = " + level.level_speed + ";");
                    if (editor_handle.platform_id != 3) stm_wr.WriteLine("newScene.A = " + level.back_color.A + ";");
                    stm_wr.WriteLine("newScene.R = " + level.back_color.R + ";");
                    stm_wr.WriteLine("newScene.G = " + level.back_color.G + ";");
                    stm_wr.WriteLine("newScene.B = " + level.back_color.B + ";");
                }

                for (int cntr = 0; cntr < object_array.Count; cntr++)
                {
                    stm_wr.WriteLine("obj = new GameObject_Scene( );");
                    
                    if (editor_handle.platform_id != 4)
                    {
                        stm_wr.WriteLine("obj.pos_x = " + object_array[cntr].position_x + ";");
                        stm_wr.WriteLine("obj.pos_y = " + object_array[cntr].position_y + ";");
                        stm_wr.WriteLine("obj.depth = " + object_array[cntr].depth + ";");
                        stm_wr.WriteLine("obj.instance_name = \"" + object_array[cntr].instance_name + "\";");
                        stm_wr.WriteLine("obj.obj_instance = ObjectManager.findGameObjectWithName(\"" + object_array[cntr].mainObject.object_name + "\");");

                        for (int cnt = 0; cnt < object_array[cntr].mainObject.scripts.Count; cnt++)
                        {
                            stm_wr.WriteLine("obj.registerScript(new " + Path.GetFileNameWithoutExtension(object_array[cntr].mainObject.scripts[cnt]) + "( ));");
                        }
                    }
                    else
                    {
                        stm_wr.WriteLine("obj->pos_x = " + object_array[cntr].position_x + ";");
                        stm_wr.WriteLine("obj->pos_y = " + object_array[cntr].position_y + ";");
                        stm_wr.WriteLine("obj->depth = " + object_array[cntr].depth + ";");
                        stm_wr.WriteLine("obj->instance_name = \"" + object_array[cntr].instance_name + "\";");
                        stm_wr.WriteLine("obj->obj_instance = ObjectManager::findGameObjectWithName(\"" + object_array[cntr].mainObject.object_name + "\");");

                        for (int cnt = 0; cnt < object_array[cntr].mainObject.scripts.Count; cnt++)
                        {
                            stm_wr.WriteLine("obj->registerScript(new " + Path.GetFileNameWithoutExtension(object_array[cntr].mainObject.scripts[cnt]) + "( ));");
                        }
                    }

                    if (level.level_name == Path.GetFileNameWithoutExtension(editor_handle.project_info.project_firstlevel))
                    {
                        stm_wr.WriteLine("firstScene.loadGameObject(obj);");
                    }
                    else
                    {
                        stm_wr.WriteLine("newScene.loadGameObject(obj);");
                    }
                }

                if (level.level_name == Path.GetFileNameWithoutExtension(editor_handle.project_info.project_firstlevel))
                {
                    if (editor_handle.platform_id != 4) stm_wr.WriteLine("SceneManager.addScene(firstScene);"); else stm_wr.WriteLine("SceneManager::addScene(firstScene);"); 
                }
                else
                {
                    if (editor_handle.platform_id != 4) stm_wr.WriteLine("SceneManager.addScene(newScene);"); else stm_wr.WriteLine("SceneManager::addScene(newScene);");
                }

                object_array.Clear();
                level = new Editor.GameLevel();
                stm_rd.Close();
            }

            if (editor_handle.platform_id != 4) stm_wr.WriteLine("HApplication.loadScene(firstScene);"); else stm_wr.WriteLine("HApplication::loadScene(firstScene);");

            if (editor_handle.platform_id != 3 && editor_handle.platform_id != 4)
            {
                stm_wr.WriteLine("firstScene.startScene(HApplication.getCanvas( ));");
            }
            else if (editor_handle.platform_id == 3)
            {
                stm_wr.WriteLine("Display.getDisplay(this).setCurrent(firstScene);");
                stm_wr.WriteLine("firstScene.startScene();");
            }
            else
            {
                stm_wr.WriteLine("HApplication::start( );");
            }

            if (editor_handle.platform_id == 1)
            {
                stm_wr.WriteLine("Application.Run(HApplication.getWindowHandle( ));");
            }

            if (editor_handle.platform_id == 4) stm_wr.WriteLine("return 0;");

            stm_wr.WriteLine("}"); // For Function
            if (editor_handle.platform_id != 4) stm_wr.WriteLine("}"); // For Class

      
            if (editor_handle.platform_id == 1) stm_wr.WriteLine("}"); // For Namespace
            stm_wr.Flush();
            stm_wr.Close();

           
            if (Directory.Exists(editor_handle.project_default_dir + "\\Build")) // Clean the whole directory.
            {
                flushDirectories(editor_handle.project_default_dir + "\\Build");
            }

            Thread.Sleep(250);
            log_window.addLog("Generating Base Complete...");

            log_window.addLog("Compiling Everything...");

jmp:
            DirectoryInfo info = null;

           info =  Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build");
           
           if (!info.Exists)
            {
                goto jmp;
            }

            build_progress.progress();
            Thread.Sleep(500);

            if (editor_handle.platform_id == 1) // For Compiling Windows.
            {

                log_window.addLog("Running C# Compiler...");

                CSharpCodeProvider cs_code_prov = new CSharpCodeProvider();
                ICodeCompiler compiler = cs_code_prov.CreateCompiler();
                CompilerParameters comp_param = new CompilerParameters();

                comp_param.GenerateExecutable = true;
                comp_param.OutputAssembly = editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".exe";
                comp_param.MainClass = "App.AppMain";

                foreach (Assembly path in AppDomain.CurrentDomain.GetAssemblies())
                {
                    comp_param.ReferencedAssemblies.Add(path.Location);
                }

                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                {
                        if (Path.GetExtension(path) == ".dll" || Path.GetExtension(path) == "dll")
                        {
                            comp_param.ReferencedAssemblies.Add(path);
                            File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                        }
                }

                comp_param.ReferencedAssemblies.Add(Application.StartupPath + "\\JVLIB.dll");
                comp_param.ReferencedAssemblies.Add(Application.StartupPath + "\\Runtime.dll");

                string[] src_array = new string[0];

                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                {
                    if (Path.GetExtension(path).ToLower() == ".cs" || Path.GetExtension(path).ToLower() == "cs")
                    {
                        Array.Resize<string>(ref src_array, src_array.Length + 1);
                        src_array[src_array.Length - 1] = path;
                    }
                }

                Array.Resize<string>(ref src_array, src_array.Length + 1);
                src_array[src_array.Length - 1] = editor_handle.project_default_dir + "\\__build";

                CompilerResults comp_res = compiler.CompileAssemblyFromFileBatch(comp_param, src_array);

                File.Delete(editor_handle.project_default_dir + "\\__build"); // Not needed any more.

                if (comp_res.Errors.Count > 0)
                {
                    Thread.Sleep(250);
                    log_window.addLog("Compile Failed!...");

                    foreach (CompilerError error in comp_res.Errors)
                    {
                        log_window.addLog( "Error : " + error.ErrorNumber + ": " + error.ErrorText + " - " + Path.GetFileName(error.FileName));
                    }

                    MessageBox.Show("Build Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    log_window.addLog("Build Failed...");
                    build_progress.Close();
                    log_window.Show();

                    foreach (string path in rem_files)
                    {
                        File.Delete(path);
                    }
                }
                else
                {
                    build_progress.progress();
                    Thread.Sleep(100);

                    Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\Data");

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                    {
                        File.Copy(file, editor_handle.project_default_dir + "\\Build\\Data\\" + encryptFileName(Path.GetFileNameWithoutExtension(file)) + ".X");
                    }

                    File.Copy(Application.StartupPath + "\\Runtime.dll", editor_handle.project_default_dir + "\\Build\\Runtime.dll");
                    File.Copy(Application.StartupPath + "\\JVLIB.dll", editor_handle.project_default_dir + "\\Build\\JVLIB.dll");

                    foreach (string path in rem_files)
                    {
                        File.Delete(path);
                    }

                    build_progress.progress();
                    Thread.Sleep(100);

                    log_window.addLog("Compile Finished...");
                    log_window.addLog("Build Completed");

                    MessageBox.Show("Build Successfull!", "Succesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    build_progress.Close();
                    log_window.Show();

                    return true;
                }
            }
            else if (editor_handle.platform_id == 2) // For Compiling to Java Desktop.
            {
                log_window.addLog("Running Java Compiler...");
                
                try
                {
                    string file_path_com = "";

                    foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                    {
                        if (Path.GetExtension(path) == "java" || Path.GetExtension(path) == ".java")
                        {
                            file_path_com += " \"" + path + "\"";
                        }
                    }

                    string lib_str = "";

                    foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                    {
                        if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                        {
                            lib_str += ";\"" + path + "\"";
                        }
                    }

                  //  MessageBox.Show("javac" + "-cp .;\"" + Application.StartupPath + "\\JRuntime.jar\"; " + editor_handle.project_default_dir + "\\AppMain.java " + file_path_com);
                    ProcessStartInfo pinfo = new ProcessStartInfo();

                    pinfo.Arguments = "-cp ." + lib_str + ";" + "\"" + Application.StartupPath + "\\JRuntime.jar\"; \""  + editor_handle.project_default_dir + "\\AppMain.java\"" + file_path_com;
                    pinfo.FileName = "javac";
                    pinfo.UseShellExecute = false;
                    pinfo.RedirectStandardOutput = true;
                    pinfo.RedirectStandardError = true;
                    pinfo.CreateNoWindow = true;

                    Process process_handle = Process.Start(pinfo);

                    log_window.addLog(process_handle.StandardError.ReadToEnd());
                    log_window.addLog(process_handle.StandardOutput.ReadToEnd());

                Wait:

                    if (!process_handle.HasExited) goto Wait;

                    if (process_handle.ExitCode % 2 == 1)
                    {
                        Thread.Sleep(250);

                        log_window.addLog("Compile Failed!...");

                        File.Delete(editor_handle.project_default_dir + "\\AppMain.java");
                        MessageBox.Show("Build Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Error found in scripts!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        log_window.addLog("Build Failed...");
                        build_progress.Close();

                        log_window.Show();

                        return false;
                    }
                    else
                    {
                        build_progress.progress();
                        Thread.Sleep(500);

                        string file_str = "";

                        Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\Data");

                        File.Copy(editor_handle.project_default_dir + "\\AppMain.class", editor_handle.project_default_dir + "\\Build\\AppMain.class");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                        {
                            if (Path.GetExtension(path) == ".class" || Path.GetExtension(path) == "class")
                            {
                                File.Copy(path,editor_handle.project_default_dir + "\\Build\\" + Path.GetFileNameWithoutExtension(path) + ".class");
                                file_str += Path.GetFileNameWithoutExtension(path) + ".class ";
                            }
                        }

                        StreamWriter bat_stm = File.CreateText(editor_handle.project_default_dir + "\\Build\\Build.bat");

                    //    bat_stm.WriteLine("jar cvfem \"" + editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".jar\" AppMain COM.T AppMain.class " + file_str + "Data");
                        bat_stm.WriteLine("jar cvfem " + editor_handle.project_info.project_name + ".jar AppMain COM.T AppMain.class " + file_str + "Data");
                        bat_stm.WriteLine("del *.class");
                        bat_stm.WriteLine("del Data");
                        bat_stm.WriteLine("rd Data");
                        bat_stm.WriteLine("del *.T");
                        bat_stm.WriteLine("del *.bat");

                        bat_stm.Flush();
                        bat_stm.Close();

                        bat_stm = File.CreateText(editor_handle.project_default_dir + "\\Build\\COM.T");

                        string libs = "";

                        foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        {
                            if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                            {
                             libs += " " + Path.GetFileName(path);
                            }
                        }

                        bat_stm.WriteLine("Class-Path: JRuntime.jar" + libs );

                        bat_stm.Flush();
                        bat_stm.Close();

                        File.Copy(Application.StartupPath + "\\JRuntime.jar" , editor_handle.project_default_dir + "\\Build\\JRuntime.jar");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        {
                            if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                            {
                                File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                            }
                        }

                        foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                        {
                            File.Copy(file, editor_handle.project_default_dir + "\\Build\\Data\\" + encryptFileName(Path.GetFileNameWithoutExtension(file)) + ".X");
                        }

                        File.Delete(editor_handle.project_default_dir + "\\AppMain.java");
                        File.Delete(editor_handle.project_default_dir + "\\AppMain.class");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                        {
                            if (Path.GetExtension(path) == ".class" || Path.GetExtension(path) == "class")
                            {
                                File.Delete(path);
                            }
                        }

                        foreach (string path in rem_files)
                        {
                            File.Delete(path);
                        }

                        build_progress.progress();
                        Thread.Sleep(500);

                        log_window.addLog("Compile Complete...");
                        log_window.addLog("Build Complete");

                        MessageBox.Show("Build Succesfull!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        build_progress.Close();

                        log_window.Show();

                        return true;
                    }
                }
                catch (Win32Exception ax)
                {
                    MessageBox.Show("JDK not installed properly!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    log_window.addLog("JDK Installation Not Found ! Please Install JDK For Heavy Engine And ReBuild");
                    log_window.addLog("Build Failed...");
                    build_progress.Close();
                    log_window.Show();
                }

                foreach (string path in rem_files)
                {
                    File.Delete(path);
                }

            }
            else if (editor_handle.platform_id == 3) // Compile for Java Mobiles.
            {
                log_window.addLog("Running Java Compiler...");

                try
                {
                    string file_path_com = "";

                    foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                    {
                        if (Path.GetExtension(path) == "java" || Path.GetExtension(path) == ".java")
                        {
                            file_path_com += " \"" + path + "\"";
                        }
                    }

                    string lib_str = "";

                    foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                    {
                        if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                        {
                            lib_str += ";\"" + path + "\"";
                        }
                    }

                    //  MessageBox.Show("javac" + "-cp .;\"" + Application.StartupPath + "\\JRuntime.jar\"; " + editor_handle.project_default_dir + "\\AppMain.java " + file_path_com);
                    //Process process_handle = Process.Start("javac", "-target 1.3 -source 1.3 -bootclasspath \"" + Application.StartupPath + "\\cldc_1.0.jar\";\"" + Application.StartupPath + "\\midp_2.0.jar\"" + " -cp ." + lib_str + ";" + "\"" + Application.StartupPath + "\\JMRuntime.jar\"; \"" + editor_handle.project_default_dir + "\\AppMain.java\"" + file_path_com);

                    ProcessStartInfo pinfo = new ProcessStartInfo();

                    pinfo.Arguments = "-target 1.3 -source 1.3 -bootclasspath \"" + Application.StartupPath + "\\cldc_1.0.jar\";\"" + Application.StartupPath + "\\midp_2.0.jar\"" + " -cp ." + lib_str + ";" + "\"" + Application.StartupPath + "\\JMRuntime.jar\"; \"" + editor_handle.project_default_dir + "\\AppMain.java\"" + file_path_com;
                    pinfo.FileName = "javac";
                    pinfo.UseShellExecute = false;
                    pinfo.RedirectStandardOutput = true;
                    pinfo.RedirectStandardError = true;
                    pinfo.CreateNoWindow = true;

                    Process process_handle = Process.Start(pinfo);

                    log_window.addLog(process_handle.StandardError.ReadToEnd());
                    log_window.addLog(process_handle.StandardOutput.ReadToEnd());

                Wait:

                    if (!process_handle.HasExited) goto Wait;

                    if (process_handle.ExitCode % 2 == 1)
                    {
                        Thread.Sleep(250);
                        log_window.addLog("Compile Failed!...");
                        log_window.addLog("Build Failed...");

                        File.Delete(editor_handle.project_default_dir + "\\AppMain.java");
                        MessageBox.Show("Build Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Error found in scripts!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        build_progress.Close();
                        log_window.Show();
                        return false;
                    }
                    else
                    {
                       // string file_str = "";
                        build_progress.progress();
                        Thread.Sleep(500);


                        Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\Data");
                        Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\bin");
                        Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\code");
                        Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\code\\bin");

                        File.Copy(editor_handle.project_default_dir + "\\AppMain.class", editor_handle.project_default_dir + "\\Build\\code\\bin\\AppMain.class");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                        {
                            if (Path.GetExtension(path) == ".class" || Path.GetExtension(path) == "class")
                            {
                                File.Copy(path, editor_handle.project_default_dir + "\\Build\\code\\bin\\" + Path.GetFileNameWithoutExtension(path) + ".class");
                               // file_str += Path.GetFileNameWithoutExtension(path) + ".class ";
                            }
                        }

                        StreamWriter bat_stm = File.CreateText(editor_handle.project_default_dir + "\\Build\\Build.bat");

                        string libs = "";

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        {
                            if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                            {
                                bat_stm.WriteLine("jar xf " + Path.GetFileName(path) + " " + Path.GetFileNameWithoutExtension(path).ToLower( ));
                                libs += " " + Path.GetFileNameWithoutExtension(path).ToLower( );
                            }
                        }

                        bat_stm.WriteLine("jar xf JMRuntime.jar jruntime");
                        bat_stm.WriteLine("preverify -classpath \"" + Application.StartupPath + "\\JMRuntime.jar\";\"" + Application.StartupPath + "\\midp_2.0.jar\";\"" + Application.StartupPath + "\\cldc_1.0.jar\"; -d \"" + editor_handle.project_default_dir + "\\Build\" code");
                  //      bat_stm.WriteLine("jar cvfm \"" + editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".jar\" COM.T bin " + "Data " + "jruntime" + libs);
                        bat_stm.WriteLine("jar cvfm " + editor_handle.project_info.project_name + ".jar COM.T bin " + "Data " + "jruntime" + libs);
                        bat_stm.WriteLine("del Data");
                        bat_stm.WriteLine("rd Data");
                        bat_stm.WriteLine("del *.T");
                        bat_stm.WriteLine("del jruntime");
                        bat_stm.WriteLine("rd jruntime");
                        bat_stm.WriteLine("del bin");
                        bat_stm.WriteLine("rd bin");
                        bat_stm.WriteLine("del code/bin");
                        bat_stm.WriteLine("rd code/bin");
                        bat_stm.WriteLine("del code");
                        bat_stm.WriteLine("rd code");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        {
                            if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                            {
                                bat_stm.WriteLine("del \"" + editor_handle.project_default_dir + "\\Build\\" + Path.GetFileNameWithoutExtension(path).ToLower( ) + "\"");
                                bat_stm.WriteLine("rd \"" + editor_handle.project_default_dir + "\\Build\\" + Path.GetFileNameWithoutExtension(path).ToLower( ) + "\"");
                            }
                        }

                        bat_stm.WriteLine("del *.bat");

                        bat_stm.Flush();
                        bat_stm.Close();

                        bat_stm = File.CreateText(editor_handle.project_default_dir + "\\Build\\COM.T");

                        bat_stm.WriteLine("MIDlet-1: AppMain, , bin.AppMain \nMIDlet-Name: " + editor_handle.project_info.project_name + " \nMIDlet-Vendor: Vendor \nMIDlet-Version: 1.0 \nMicroEdition-Configuration: CLDC-1.0 \nMicroEdition-Profile: MIDP-2.0");

                        bat_stm.Flush();
                        bat_stm.Close();

                        File.Copy(Application.StartupPath + "\\JMRuntime.jar", editor_handle.project_default_dir + "\\Build\\JMRuntime.jar");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        {
                            if (Path.GetExtension(path) == ".jar" || Path.GetExtension(path) == "jar")
                            {
                                File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                            }
                        }

                        foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                        {
                            File.Copy(file, editor_handle.project_default_dir + "\\Build\\Data\\" + encryptFileName(Path.GetFileNameWithoutExtension(file)) + ".X");
                        }

                        File.Delete(editor_handle.project_default_dir + "\\AppMain.java");
                        File.Delete(editor_handle.project_default_dir + "\\AppMain.class");

                        foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                        {
                            if (Path.GetExtension(path) == ".class" || Path.GetExtension(path) == "class")
                            {
                                File.Delete(path);
                            }
                        }

                        foreach (string path in rem_files)
                        {
                            File.Delete(path);
                        }

                        build_progress.progress();
                        Thread.Sleep(500);

                        log_window.addLog("Compile Failed!...");
                        log_window.addLog("Build Complete");

                        MessageBox.Show("Build Succesfull!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        build_progress.Close();

                        log_window.Show();

                        return true;
                    }
                }
                catch (Win32Exception ax)
                {
                    MessageBox.Show("JDK not installed properly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log_window.addLog("JDK Installation Not Found ! Please Install JDK For Heavy Engine And ReBuild");
                    log_window.addLog("Build Failed...");
                    build_progress.Close();
                    log_window.Show();
                }

                foreach (string path in rem_files)
                {
                    File.Delete(path);
                }
            }
            else if (editor_handle.platform_id == 4)
            {
                build_progress.progress();
                Thread.Sleep(500);

                log_window.addLog("Running C++ Compiler...");

                StreamWriter project_file_wr = new StreamWriter(editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj");
                StreamWriter script_header_wr = new StreamWriter(editor_handle.project_default_dir + "\\Build\\Scripts.h");
                string script_list = "";
                string header_list = "";

                script_header_wr.WriteLine("#ifndef SCRIPT_H");

                foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                {
                    if (Path.GetExtension(path) == ".cpp" || Path.GetExtension(path) == "cpp")
                    {
                        script_list += "<ClCompile Include = \"" + Path.GetFileName(path) + "\" />\n";
                        File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                    }
                    else if (Path.GetExtension(path) == ".h" || Path.GetExtension(path) == "h")
                    {
                        header_list += "<ClInclude Include = \"" + Path.GetFileName(path) + "\" />\n";
                        script_header_wr.WriteLine("#include \"" + Path.GetFileName(path) + "\"");
                        File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                    }
                }

                Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\Data");

                 foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                 {
                     File.Copy(file, editor_handle.project_default_dir + "\\Build\\Data\\" + encryptFileName(Path.GetFileNameWithoutExtension(file)) + ".X");
                 }

                // Copy DLL / Runtime Libraries.
                foreach(string file in Directory.GetFiles(Application.StartupPath + "\\Runtime"))
                {
                    if (Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == "dll")
                    {
                        File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file));
                    }
                }

                foreach(string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                {
                    if (Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == "dll")
                    {
                        File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file));
                    }
                }

                script_header_wr.WriteLine("#endif");
                script_header_wr.Flush( );
                script_header_wr.Close( );

                // Generate VC++ Project.
                project_file_wr.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                                          "<Project DefaultTargets=\"Build\" ToolsVersion=\"10.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n" +
                                          "<ItemGroup>\n" +
                                          "<ProjectConfiguration Include=\"Debug|Win32\">\n" +
                                          "<Configuration>Debug</Configuration>\n" +
                                          "<Platform>Win32</Platform>\n" +
                                          "</ProjectConfiguration>\n" +
                                          "<ProjectConfiguration Include=\"Release|Win32\">\n" +
                                          "<Configuration>Release</Configuration>\n" +
                                          "<Platform>Win32</Platform>\n" +
                                          "</ProjectConfiguration>\n" +
                                          "</ItemGroup>\n" +
                                          "<Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.default.props\" />\n" +
                                          "<PropertyGroup>\n" +
                                          "<ConfigurationType>Application</ConfigurationType>\n" +
                                          "<PlatformToolset>v120</PlatformToolset>\n" +
                                          "</PropertyGroup>\n" +
                                          "<Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.props\" />\n" +
                                          "<ItemGroup>\n" +
                                          "<ClCompile Include=\"AppMain.cpp\" />\n" +
                                          script_list +
                                          "</ItemGroup>\n" +
                                          "<ItemGroup>\n" +
                                          "<ClInclude Include=\"Scripts.h\" />\n" +
                                          header_list +
                                          "</ItemGroup>\n" +
                                          "<Import Project=\"$(VCTargetsPath)\\Microsoft.Cpp.Targets\" />\n" +
                                          "<PropertyGroup>\n" +
                                          "<IncludePath>" + Application.StartupPath + "\\header_includes;$(IncludePath)</IncludePath>\n" +
                                          "<LibraryPath>" + Application.StartupPath + "\\lib;" + editor_handle.project_default_dir + "\\Game-Plugins;$(LibraryPath)</LibraryPath>\n" +
                                          "</PropertyGroup>\n" +
                                          "<ItemDefinitionGroup>\n" +
                                          "<Link>\n" +
                                          "<GenerateDebugInformation>true</GenerateDebugInformation>\n" +
                                          "<SubSystem>Console</SubSystem>\n" +
                                          "</Link>\n" +
                                          "<ClCompile>\n" +
                                          "<RuntimeLibrary Condition=\"'$(Configuration)|$(Platform)'=='Debug|Win32'\">MultiThreadedDebugDLL</RuntimeLibrary>\n" +
                                          "</ClCompile>\n" +
                                          "</ItemDefinitionGroup>\n" +
                                          "</Project>");

                project_file_wr.Flush();
                project_file_wr.Close();

                File.Copy(editor_handle.project_default_dir + "\\AppMain.cpp", editor_handle.project_default_dir + "\\Build\\AppMain.cpp");

                // Run MSBuild.
                try
                {
                    //Process process_handle = Process.Start("MSBuild", editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj /p:configuration=debug");

                    ProcessStartInfo pinfo = new ProcessStartInfo();

                    pinfo.Arguments = editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj /p:configuration=debug";
                    pinfo.FileName = "MSBuild";
                    pinfo.UseShellExecute = false;
                    pinfo.RedirectStandardOutput = true;
                    pinfo.RedirectStandardError = true;
                    pinfo.CreateNoWindow = true;

                    Process process_handle = Process.Start(pinfo);

                    log_window.addLog(process_handle.StandardOutput.ReadToEnd());
                    log_window.addLog(process_handle.StandardError.ReadToEnd());

                re:
                    if (!process_handle.HasExited) goto re;

                    if (process_handle.ExitCode % 2 == 0x1)
                    {
                        Thread.Sleep(250);

                        log_window.addLog("Compile Failed!...");
                        log_window.addLog("Build Failed!...");

                        MessageBox.Show("Error found in scripts!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Build Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        File.Delete(editor_handle.project_default_dir + "\\AppMain.cpp");
                        flushDirectories(editor_handle.project_default_dir + "\\Build");
                        log_window.Show();
                        build_progress.Close();

                        foreach (string path in rem_files)
                        {
                            File.Delete(path);
                        }

                        return false;
                    }

                    build_progress.progress();
                    Thread.Sleep(500);

                    File.Copy(editor_handle.project_default_dir + "\\Build\\Debug\\" + editor_handle.project_info.project_name + ".exe", editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".exe");

                    flushDirectories(editor_handle.project_default_dir + "\\Build\\Debug");
                    File.Delete(editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj");
                    File.Delete(editor_handle.project_default_dir + "\\AppMain.cpp");

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Build"))
                    {
                        if (Path.GetExtension(file) == ".cpp" || Path.GetExtension(file) == "cpp" || Path.GetExtension(file) == ".h" || Path.GetExtension(file) == "h")
                        {
                            File.Delete(file);
                        }
                    }

                    build_progress.progress();
                    Thread.Sleep(500);

                    log_window.addLog("Compile Completed!...");
                    log_window.addLog("Build Complete");

                    // Finish.
                    MessageBox.Show("Build Succesfull!", "Succesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (string path in rem_files)
                    {
                        File.Delete(path);
                    }

                    build_progress.Close();
                    log_window.Show();

                    return true;
                }
                catch(Win32Exception ex)
                {
                    MessageBox.Show("MSBuild not installed properly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log_window.addLog("MSBuild Installation Not Found ! Please Install MSBuild v12.0 For Heavy Engine And ReBuild");
                    log_window.addLog("Build Failed...");
                }

                foreach (string path in rem_files)
                {
                    File.Delete(path);
                }

                build_progress.Close();
                log_window.Show();
            }

            return false;
        }

        private void flushDirectories(string path)
        {
            foreach (string dir_path in Directory.GetDirectories(path))
            {
                flushDirectories(dir_path);
            }

            foreach (string file_path in Directory.GetFiles(path))
            {
                File.Delete(file_path);
            }

            Directory.Delete(path);
        }
    }


}