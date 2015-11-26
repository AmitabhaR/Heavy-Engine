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
using Microsoft.Win32;

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
        public float cam_angle = 0f;
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
        MenuItem short_menu_duplicate = new MenuItem("Duplicate");
        MenuItem short_menu_rotation_scale = new MenuItem("Rotation and Scaling Editor");
        MenuItem short_menu_child_editor = new MenuItem("Child Editor");
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
            short_menu_duplicate.Click += short_menu_duplicate_Click;
            short_menu_rotation_scale.Click += short_menu_rotation_scale_Click;
            short_menu_child_editor.Click += short_menu_child_editor_Click;
        }

        void short_menu_child_editor_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject != null)
            {
                // Open the editor.
                ChildEditor child_editor = new ChildEditor(this);

                child_editor.Show();
            }
        }

        void short_menu_rotation_scale_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject != null)
            {
                RotationScaleEditor rotation_scale_editor = new RotationScaleEditor(this);

                rotation_scale_editor.Show();
            }
        }

        void addChildObject(GameObject_Scene gameObj,GameObject_Scene src)
        {
            for(int cnt = 0;cnt < src.child_list.Count;cnt++)
            {
                GameObject_Scene Obj;

                Obj = src.child_list[cnt];
                Obj.child_list = new List<GameObject_Scene>(); 
                Obj.instance_name += "_clone_" + (new Random()).Next(0, 100000000).ToString() + "_X_" + (new Random()).Next(0, 100000000).ToString() + "_X_" + (new Random()).Next(0, 100000000).ToString() + "_X_" + (new Random()).Next(0, 100000000).ToString();

                gameObj.child_list.Add(Obj);

                addChildObject(Obj,src.child_list[cnt]);

                addGameObject(Obj.instance_name);
                gameObjectScene_list.Add(Obj);
            }
        }

        void short_menu_duplicate_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject != null)
            {
                GameObject_Scene baseObject = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj; // Make a duplicate copy of the game object.
                GameObject_Scene cur_obj = baseObject;

                baseObject.child_list = new List<GameObject_Scene>();

                addChildObject(baseObject, cur_obj);
                baseObject.UpdateChildPosition(-cur_obj.position_scene_x ,-cur_obj.position_scene_y,true); // Return to (0,0) coordinate in editor.
                UpdateWorldChilds(baseObject);  // Update all instances in game-object list.
                baseObject.UpdateChildScenePosition(cam_x, cam_y, true); // Update the child position in the game world.

                baseObject.instance_name += "_clone_" + (new Random()).Next(0, 100000000).ToString() + "_X_" + (new Random()).Next(0, 100000000).ToString();
                baseObject.position_x = baseObject.position_y = baseObject.position_scene_x = baseObject.position_scene_y = 0;

                gameObjectScene_list.Add(baseObject);

                addGameObject(baseObject.instance_name);

                sortGameObjects();
            }
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
                    gameObject_editor.SelectedObject = new GameObject_Scene_EDITOR(gameObject,this);
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

            ObjectEditor obj_editor = new ObjectEditor(this, ((GameObject_Scene_EDITOR ) gameObject_editor.SelectedObject)._obj.mainObject.object_name);

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

            if (instance_name == ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name)
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
                    if (gameObjectScene_list[cnt].instance_name == ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name)
                    {
                        GameObject_Scene obj = gameObjectScene_list[cnt];
                        int prev_x = obj.position_scene_x,prev_y = obj.position_scene_y;

                        obj.position_scene_x = e.X;
                        obj.position_scene_y = e.Y;

                        gameObjectScene_list[cnt] = obj;

                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).X = e.X + cam_x;
                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).Y = e.Y + cam_y;

                        GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                        obj_handle.position_scene_x = e.X;
                        obj_handle.position_scene_y = e.Y;

                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.UpdateChildPosition(e.X - prev_x, e.Y - prev_y,true);
                        UpdateWorldChilds(gameObjectScene_list[cnt]);

                        break;
                    }
                }
            }
        }

        public void UpdateWorldChilds(GameObject_Scene gameObject)
        {
            for(int cnt = 0;cnt < gameObject.child_list.Count;cnt++)
            {
                for(int cntr = 0;cntr < gameObjectScene_list.Count;cntr++)
                {
                    if (gameObjectScene_list[cntr].instance_name == gameObject.child_list[cnt].instance_name)
                    {
                        GameObject_Scene gameObj = gameObjectScene_list[cntr];

                        gameObj.position_scene_x = gameObject.child_list[cnt].position_scene_x;
                        gameObj.position_scene_y = gameObject.child_list[cnt].position_scene_y;
                        gameObj.position_x = gameObject.child_list[cnt].position_x;
                        gameObj.position_y = gameObject.child_list[cnt].position_y;
                        gameObj.rotation_angle = gameObject.child_list[cnt].rotation_angle;
                        gameObj.scaling_rate = gameObject.child_list[cnt].scaling_rate;
                        gameObj.mainObject.object_img = gameObject.child_list[cnt].mainObject.object_img;     

                        gameObjectScene_list[cntr] = gameObj;

                        UpdateWorldChilds(gameObject.child_list[cnt]);
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

        public void sortGameObjects()
        {
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

        public GameObject_Scene_EDITOR GetActiveGameObject()
        {
            return (GameObject_Scene_EDITOR) gameObject_editor.SelectedObject;
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

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++) lb_objects.Items.Add(gameObjectScene_list[cnt].instance_name);

            this.menuItem_ImportHeader.Enabled = (platform_id == 4 || platform_id == 5) ? true : false;

           /* sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

            for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
            {
                sortedArray[cnt].depth = gameObjectScene_list[cnt].depth;
                sortedArray[cnt].index = cnt;
            }

            while (!checkSorted(sortedArray))
            {
                sortElements(sortedArray);
            } */

            sortGameObjects();

            this.base_container.Panel1MinSize = 250;
            this.base_container.Panel2MinSize = this.contpane_base.Width;
            calculateLines();
            reloadFileTree();

            RegistryKey reg_key = Registry.CurrentUser.CreateSubKey("Software\\HeavyEngine");
            object recent2 = reg_key.GetValue("Recent2");

            reg_key.SetValue("Recent2", (reg_key.GetValue("Recent1") == null) ? "" : reg_key.GetValue("Recent1"));
            reg_key.SetValue("Recent3", (recent2 == null) ? "" : recent2);
            reg_key.SetValue("Recent1", project_default_dir);

            menuItem_Recent1.Text = project_default_dir;
            menuItem_Recent2.Text = (string) reg_key.GetValue("Recent2");
            menuItem_Recent3.Text = (string) reg_key.GetValue("Recent3");
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

                   /* sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

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
                    } */
                    sortGameObjects();

                    foreach (GameObject_Scene gameObj in gameObjectScene_list) addGameObject(gameObj.instance_name);
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

                    menu.MenuItems.AddRange(new MenuItem[] { short_menu_showGameObject , short_menu_select , short_menu_release , short_menu_cancel , short_menu_duplicate , short_menu_rotation_scale , short_menu_child_editor });
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
           if (canClose)
           {
               if (MessageBox.Show("Save current level ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) menuItem_SaveLevel_Click(null, null);
               canClose = false;
               Application.Exit();
           }
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
                    if (gameObjectScene_list[cntr].position_scene_x + (int) gameObjectScene_list[cntr].scaling_rate + gameObjectScene_list[cntr].mainObject.object_img.Width > mouse_PX && gameObjectScene_list[cntr].position_scene_x < mouse_PX && gameObjectScene_list[cntr].position_scene_y + (int) gameObjectScene_list[cntr].scaling_rate + gameObjectScene_list[cntr].mainObject.object_img.Height > mouse_PY && gameObjectScene_list[cntr].position_scene_y < mouse_PY && e.Button == MouseButtons.Left)
                    {
                        GameObject_Scene_EDITOR obj = new GameObject_Scene_EDITOR(gameObjectScene_list[cntr],this);
                        gameObject_editor.SelectedObject = obj;
                        isLcontrol = true;
                    }
                }
                else if (gameObjectScene_list[cntr].mainObject.object_text != "")
                {
                    if (gameObjectScene_list[cntr].position_x + 10 * gameObjectScene_list[cntr].mainObject.object_text.Length + gameObjectScene_list[cntr].scaling_rate > mouse_PX && gameObjectScene_list[cntr].position_x < mouse_PX && gameObjectScene_list[cntr].position_y + 50 + gameObjectScene_list[cntr].scaling_rate > mouse_PY && gameObjectScene_list[cntr].position_y < mouse_PY && e.Button == MouseButtons.Left)
                    {
                        GameObject_Scene_EDITOR obj = new GameObject_Scene_EDITOR(gameObjectScene_list[cntr],this);
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

                if (sortedArray == null) return;

                for (int cnt = 0; cnt < sortedArray.Length; cnt++)
                {
                    int cntr = sortedArray[cnt].index;

                    if (gameObjectScene_list[cntr].Visibility)
                    {
                        if (gameObjectScene_list[cntr].mainObject.object_img != null)
                        {
                            e.Graphics.DrawImage(new Bitmap(gameObjectScene_list[cntr].mainObject.object_img, new Size(gameObjectScene_list[cntr].mainObject.object_img.Width + zoom_rate + (int)gameObjectScene_list[cntr].scaling_rate, gameObjectScene_list[cntr].mainObject.object_img.Height + zoom_rate + (int)gameObjectScene_list[cntr].scaling_rate)), new Point(gameObjectScene_list[cntr].position_scene_x, gameObjectScene_list[cntr].position_scene_y));
                        }
                        else if (gameObjectScene_list[cntr].mainObject.object_text != "")
                        {
                            e.Graphics.DrawString(gameObjectScene_list[cntr].mainObject.object_text, new Font("Verdana", 12 + (int)gameObjectScene_list[cntr].scaling_rate), Brushes.Black, new Point(gameObjectScene_list[cntr].position_scene_x, gameObjectScene_list[cntr].position_scene_y));
                        }
                        //else e.Graphics.DrawRectangle(Pens.White, gameObjectScene_list[cntr].position_scene_x, gameObjectScene_list[cntr].position_scene_y, 10, 10);
                    }
                }

                if (gameObject_editor.SelectedObject != null)
                {
                    if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img != null && ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.Visibility)
                    {
                        GameObject_Scene_EDITOR handle = (GameObject_Scene_EDITOR)  gameObject_editor.SelectedObject;

                        e.Graphics.DrawRectangle(Pens.White, handle.obj.position_scene_x - ((int)handle.obj.scaling_rate + zoom_rate), handle.obj.position_scene_y - ((int)handle.obj.scaling_rate + zoom_rate), handle.obj.mainObject.object_img.Width + 7 / 2 * ((int)handle.obj.scaling_rate + zoom_rate), handle.obj.mainObject.object_img.Height + 7 / 2 * ((int)handle.obj.scaling_rate + zoom_rate));
                    }
                    else if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img != null && !((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.Visibility)
                    {
                        e.Graphics.DrawRectangle(Pens.White, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_x, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_y, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img.Width, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img.Height);  // Draw the edge lines of the object.
                    }
                    else if (((GameObject_Scene_EDITOR) gameObject_editor.SelectedObject).obj.mainObject.object_img == null && ((GameObject_Scene_EDITOR) gameObject_editor.SelectedObject).obj.mainObject.object_text == "")
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.collision_rectX > 0 && ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.collision_rectY > 0) e.Graphics.DrawRectangle(Pens.White, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_x, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_y, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.collision_rectX, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.collision_rectY);
                        else e.Graphics.DrawRectangle(Pens.White, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_x, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_y, 15, 15);
                    }
                    else if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_text != "" && !((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.Visibility)
                    {
                        e.Graphics.DrawRectangle(Pens.White, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_x, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_y, 15, 15);
                    }
                }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current level ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) menuItem_SaveLevel_Click(null, null);

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

        private void LoadChilds()
        {
            for(int cntr = 0;cntr < gameObjectScene_list.Count;cntr++)
            {
                for(int cnt = 0;cnt < gameObjectScene_list[cntr].child_list.Count;cnt++)
                {
                    for (int c = 0; c < gameObjectScene_list.Count; c++)
                    {
                        if (cnt == cntr) continue;

                        if (gameObjectScene_list[cntr].child_list[cnt].instance_name == gameObjectScene_list[c].instance_name)
                        {
                            gameObjectScene_list[cntr].child_list[cnt] = gameObjectScene_list[c];
                            break;
                        }
                    }
                }
            }
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
                string str = (string)obj;

                if (cur_action == "")
                    if (str == "Project_Name:") cur_action = "Project_Name";
                    else if (str == "Project_Version:") cur_action = "Project_Version";
                    else if (str == "Project_Author:") cur_action = "Project_Author";
                    else if (str == "Project_About:") cur_action = "Project_About";
                    else if (str == "Project_FirstLevel:") cur_action = "Project_FirstLevel";
                    else if (str == "Project_Platform:") cur_action = "Project_Platform";
                    else if (str == "Level_Name:") cur_action = "Level_Name";
                    else if (str == "Speed:") cur_action = "Speed";
                    else if (str == "Back_Colour:") cur_action = "Back_Colour";
                    else if (str == "Object:") cur_action = "Object";
                    else ;
                else
                {
                    if (cur_action == "Project_Name")
                    {
                        if (str.Substring(0, 1) == "@") project_info.project_name = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Project_Version")
                    {
                        if (str.Substring(0, 1) == "@") project_info.project_version = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Project_Author")
                    {
                        if (str.Substring(0, 1) == "@") project_info.project_author = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Project_About")
                    {
                        if (str.Substring(0, 1) == "@") project_info.project_about = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Project_FirstLevel")
                    {
                        if (File.Exists(project_default_dir + "\\" + str)) project_info.project_firstlevel = project_default_dir + "\\" + str;
                        else
                        {
                            MessageBox.Show("The default level file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }

                        cur_action = "";
                    }
                    else if (cur_action == "Project_Platform")
                    {
                        platform_id = int.Parse(str);

                        cur_action = "";
                    }
                    else if (cur_action == "Level_Name")
                    {
                        if (str.Substring(0, 1) == "@") current_level.level_name = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Speed")
                    {
                        current_level.level_speed = int.Parse(str);
                        cur_action = "";
                    }
                    else if (cur_action == "Back_Colour")
                    {
                        current_level.back_color = Color.FromArgb(int.Parse(str), 0, 0, 0);
                        cur_action = "Back_ColourR";
                    }
                    else if (cur_action == "Back_ColourR")
                    {
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, int.Parse(str), 0, 0);
                        cur_action = "Back_ColourG";
                    }
                    else if (cur_action == "Back_ColourG")
                    {
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, current_level.back_color.R, int.Parse(str), 0);
                        cur_action = "Back_ColourB";
                    }
                    else if (cur_action == "Back_ColourB")
                    {
                        current_level.back_color = Color.FromArgb(current_level.back_color.A, current_level.back_color.R, current_level.back_color.G, int.Parse(str));
                        cur_action = "";
                    }
                    else if (cur_action == "Object")
                    {
                      //  MessageBox.Show(str.Substring(1, str.Length - 1));
                        if (str.Substring(0,1) == "@")
                        {
                            foreach(GameObject gameObj in gameObject_list)
                            {
                                if (gameObj.object_name == str.Substring(1,str.Length - 1))
                                {
                                    gameObject.mainObject = gameObj;
                                    gameObject.Initialize();
                                }
                            }
                        }

                        cur_action = "Object_NAME";
                    }
                    else if (cur_action == "Object_NAME")
                    {
                        if (str.Substring(0,1) == "@") gameObject.instance_name = str.Substring(1, str.Length - 1);

                        cur_action = "Object_PositionX";
                    }
                    else if (cur_action == "Object_PositionX")
                    {
                        gameObject.position_x = int.Parse(str);
                        gameObject.position_scene_x = gameObject.position_x;

                        cur_action = "Object_PositionY";
                    }
                    else if (cur_action == "Object_PositionY")
                    {
                        gameObject.position_y = int.Parse(str);
                        gameObject.position_scene_y = gameObject.position_y;

                        cur_action = "Object_Depth";
                    }
                    else if (cur_action == "Object_Depth")
                    {
                        gameObject.depth = int.Parse(str);

                        cur_action = "Object_Tiled";
                    }
                    else if (cur_action == "Object_Tiled")
                    {
                        gameObject.isTile = bool.Parse(str);

                        cur_action = "Object_Rotation";
                    }
                    else if (cur_action == "Object_Rotation")
                    {
                        gameObject.rotation_angle = float.Parse(str);
                        gameObject.ApplyRotation(-gameObject.rotation_angle);

                        cur_action = "Object_Scale";
                    }
                    else if (cur_action == "Object_Scale")
                    {
                        gameObject.scaling_rate = float.Parse(str);

                        cur_action = "Object_CameraTranslation";
                    }
                    else if (cur_action == "Object_CameraTranslation")
                    {
                        gameObject.AllowCameraTranslation = bool.Parse(str);

                        cur_action = "Object_CameraRotation";
                    }
                    else if (cur_action == "Object_CameraRotation")
                    {
                        gameObject.AllowCameraRotation = bool.Parse(str);

                        cur_action = "Object_Visibility";
                    }
                    else if (cur_action == "Object_Visibility")
                    {
                        gameObject.Visibility = bool.Parse(str);

                        cur_action = "Object_CollisionX";
                    }
                    else if (cur_action == "Object_CollisionX")
                    {
                        gameObject.collision_rectX = int.Parse(str);

                        cur_action = "Object_CollisionY";
                    }
                    else if (cur_action == "Object_CollisionY")
                    {
                        gameObject.collision_rectY = int.Parse(str);

                        cur_action = "Object_Childs";
                    }
                    else if (cur_action == "Object_Childs")
                    {
                        if (str == ";")
                        {
                            cur_action = "";
                            gameObjectScene_list.Add(gameObject);
                            gameObject = new GameObject_Scene();
                        }
                        else
                        {
                            GameObject_Scene gameObj = new GameObject_Scene();

                            gameObj.instance_name = str;

                            gameObject.child_list.Add(gameObj);
                        }
                    }
                }
            }

          if (gameObjectScene_list.Count > 0) LoadChilds(); // Reload the childs with proper instances.
        }

        private void menuItem_SaveProject_Click(object sender, EventArgs e)
        {
            StreamWriter stm_wr = new StreamWriter(project_default_dir + "\\" + project_name + ".prj");

            stm_wr.WriteLine("Project_Name: @" + project_info.project_name);
            stm_wr.WriteLine("Project_Author: @" + project_info.project_author);
            stm_wr.WriteLine("Project_Version: @" + project_info.project_version);
            stm_wr.WriteLine("Project_About: @" + project_info.project_about);
            stm_wr.WriteLine("Project_FirstLevel: Game-Levels\\" + Path.GetFileName(project_info.project_firstlevel));
            stm_wr.WriteLine("Project_Platform: " + platform_id);

            stm_wr.Flush();

            stm_wr.Close();
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

                /*sortedArray = new DrawableGameObject[gameObjectScene_list.Count];

                for (int cnt = 0; cnt < gameObjectScene_list.Count; cnt++)
                {
                    sortedArray[cnt].depth = gameObjectScene_list[cnt].depth;
                    sortedArray[cnt].index = cnt;
                }

                while (!checkSorted(sortedArray))
                {
                    sortElements(sortedArray);
                } */

                sortGameObjects();

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
                string obj_line = "Object: @" + gameObject.mainObject.object_name + "@ @" + gameObject.instance_name + "@ " + gameObject.position_x + " " + gameObject.position_y + " " + gameObject.depth + " " + gameObject.isTile + " " + gameObject.rotation_angle + " " + gameObject.scaling_rate + " " + gameObject.AllowCameraTranslation + " " + gameObject.AllowCameraRotation + " " + gameObject.Visibility + " " + gameObject.collision_rectX + " " + gameObject.collision_rectY;

                foreach(GameObject_Scene gameObj in gameObject.child_list)
                {
                    obj_line += " " + gameObj.instance_name;
                }

                obj_line += " ;";

                stm_wr.WriteLine(obj_line);
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
            public bool isTile; // This property is only present in the editor . This property is not found in the real game library.
            public float rotation_angle;
            public float scaling_rate;
            public List<GameObject_Scene> child_list;
            public bool AllowCameraTranslation;
            public bool AllowCameraRotation;
            public bool Visibility;
            public int collision_rectX, collision_rectY;
            private Image source_img;

            public void Initialize( )
            {
                source_img = mainObject.object_img;

                if (child_list == null) child_list = new List<GameObject_Scene>();
                AllowCameraTranslation = false;
                AllowCameraRotation = true;
            }

            public void UpdateChildPosition(int rate_x,int rate_y,bool isParent)
            {
                if (!isParent) { position_scene_x += rate_x; position_scene_y += rate_y; }
              
                for(int cnt = 0;cnt < child_list.Count;cnt++)
                {
                    GameObject_Scene gameObj = child_list[cnt];

                    gameObj.UpdateChildPosition(rate_x, rate_y,false);

                    child_list[cnt] = gameObj;
                }
            }

            public void UpdateChildScenePosition(int cam_x,int cam_y,bool isParent)
            {
                if (!isParent) { position_x = position_scene_x + cam_x; position_y = position_scene_y + cam_y; }
              
                for(int cnt = 0;cnt < child_list.Count;cnt++)
                {
                    GameObject_Scene gameObj = child_list[cnt];

                    gameObj.UpdateChildScenePosition(cam_x, cam_y,false);

                    child_list[cnt] = gameObj;
                }
            }

            public void RestoreChildPosition(int parent_x , int parent_y, bool isParent)
            {
                int prev_x = position_scene_x, prev_y = position_scene_y;
                if (!isParent) { position_scene_x = parent_x; position_scene_y = parent_y; }

                for (int cnt = 0; cnt < child_list.Count; cnt++)
                {
                    GameObject_Scene gameObj = child_list[cnt];

                    gameObj.RestoreChildPosition(parent_x - (prev_x - gameObj.position_scene_x), parent_y - (prev_y - gameObj.position_scene_y), false);

                    child_list[cnt] = gameObj;
                }
            }

            public void UpdateRotation(float rot_rate,int centerX , int centerY,bool isParent)
            {
                if (!isParent) Rotate(rot_rate);

                for (int cnt = 0; cnt < child_list.Count; cnt++)
                {
                    GameObject_Scene gameObj = child_list[cnt];
                    double angle = -( rot_rate * Math.PI / 180 );
                    double cos = Math.Cos(angle);
                    double sin = Math.Sin(angle);
                    int dx = gameObj.position_scene_x - centerX;
                    int dy = gameObj.position_scene_y - centerY;
                    double x = cos * dx - sin * dy + centerX;
                    double y = sin * dx + cos * dy + centerY;
                    int prev_x = gameObj.position_scene_x;
                    int prev_y = gameObj.position_scene_y;

                    gameObj.position_scene_x = (int) Math.Round(x);
                    gameObj.position_scene_y = (int) Math.Round(y );
                    gameObj.UpdateRotation(rot_rate,centerX,centerX,false);

                    child_list[cnt] = gameObj;
                }
            }

            public void UpdateScale(float scale_rate,bool isParent)
            {
                if (!isParent) scaling_rate += scale_rate;

                for (int cnt = 0; cnt < child_list.Count; cnt++)
                {
                    GameObject_Scene gameObj = child_list[cnt];

                    gameObj.UpdateScale(scale_rate, false);

                    child_list[cnt] = gameObj;
                }
            }

            public void ApplyRotation(float angle)
            {
                if (mainObject.object_img != null)
                {
                    double rad_angle = angle * Math.PI / 180;
                    Bitmap new_img = new Bitmap((int)(source_img.Width * Math.Abs(Math.Cos(rad_angle)) + source_img.Height * Math.Abs(Math.Sin(rad_angle))), (int)(source_img.Width * Math.Abs(Math.Sin(rad_angle)) + source_img.Height * Math.Abs(Math.Cos(rad_angle))));
                    Graphics g = Graphics.FromImage(new_img);

                    new_img.SetResolution(source_img.HorizontalResolution, source_img.VerticalResolution);

                    g.Clear(Color.Transparent);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.TranslateTransform(((new_img.Width - source_img.Width) / 2), ((new_img.Height - source_img.Height) / 2));
                    g.TranslateTransform((float)(source_img.Size.Width / 2), (float)(source_img.Size.Height / 2));
                    g.RotateTransform(angle);
                    g.TranslateTransform(-(float)(source_img.Size.Width / 2), -(float)(source_img.Size.Height / 2));
                    g.DrawImage(source_img, 0,0);
                    
                    mainObject.object_img = new_img;
                }
            }

            public void Rotate(float angle)
            {
                ApplyRotation(-(angle + rotation_angle));
                rotation_angle += angle;
            }
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
            AddObject window_handle = new AddObject(this,mouse_PX,mouse_PY);
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
            lbl_cam_angle.Text = "Camera Angle : " + cam_angle;

           if (this.WindowState != FormWindowState.Maximized) this.WindowState = FormWindowState.Maximized;

            this.canvas.Refresh( );
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (gameObject_editor.SelectedObject != null)
            {
                GameObject_Scene_EDITOR obj = (GameObject_Scene_EDITOR)gameObject_editor.SelectedObject;
                
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

                       if (obj.obj.mainObject.object_img != null)
                       {
                           obj.obj.UpdateChildScenePosition(cam_x, cam_y, true);
                           UpdateWorldChilds(gameObjectScene_list[cntr]);
                       }

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
                else if (platform_id == 2 || platform_id == 3)
                {
                    MessageBox.Show("Java platform programs cannot run directly ! Goto to build directory in your project folder and run Build.bat to get a executable jar file","Message",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else if (platform_id == 5)
                {
                    MessageBox.Show("Linux platform programs cannot run directly ! A linux platform is required with all required libraries to compile to linux platforms . For more information , visit linux platform page.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                else if (platform_id == 4)
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".lib" || Path.GetExtension(open_file_dlg.FileName) == "lib" || Path.GetExtension(open_file_dlg.FileName) == ".dll" || Path.GetExtension(open_file_dlg.FileName) == "dll")
                    {
                        File.Copy(open_file_dlg.FileName, project_default_dir + "\\Game-Plugins\\" + Path.GetFileName(open_file_dlg.FileName));
                    }
                }
                else if (platform_id == 5)
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".so" || Path.GetExtension(open_file_dlg.FileName) == "so" || Path.GetExtension(open_file_dlg.FileName) == ".a" || Path.GetExtension(open_file_dlg.FileName) == "a")
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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_y += 5;

                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_y -= 5;

                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_x += 5;
                            
                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_x -= 5;
                            
                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR) gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_x -= 5 * rate;
                            obj_handle.position_scene_y -= 5 * rate;

                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

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

                if (gameObject_editor.SelectedObject != null)
                {
                    if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                    {
                        GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                        obj_handle.position_scene_x -= 5;
                        obj_handle.position_scene_y -= 5;

                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                    }
                }

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

                if (gameObject_editor.SelectedObject != null)
                {
                    if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                    {
                        GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                        obj_handle.position_scene_x += 5;
                        obj_handle.position_scene_y += 5;

                        ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                    }
                }

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

                    if (gameObject_editor.SelectedObject != null)
                    {
                        if (((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.instance_name == handle.instance_name)
                        {
                            GameObject_Scene obj_handle = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj;

                            obj_handle.position_scene_x += 5 * rate;
                            obj_handle.position_scene_y += 5 * rate;

                            ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj = obj_handle;
                        }
                    }

                    gameObjectScene_list[cnt] = handle;
                }

                calculateLines();
        }

        private void menuItem_open_editor_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string path in Directory.GetFiles(project_default_dir + "\\Game-Scripts"))
                    if (Path.GetExtension(path) == ".bs" || Path.GetExtension(path) == "bs") Process.Start("notepad++", path);
                    else if (platform_id == 1)
                        if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == "cs") Process.Start("notepad++", path);
                    else if (platform_id == 2 || platform_id == 3)
                        if (Path.GetExtension(path) == ".java" || Path.GetExtension(path) == "java") Process.Start("notepad++", path);
                    else if (platform_id == 4 || platform_id == 5)
                        if (Path.GetExtension(path) == ".cpp" || Path.GetExtension(path) == "cpp" || Path.GetExtension(path) == ".h" || Path.GetExtension(path) == "h") Process.Start("notepad++", path);
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
               if (gameObjectScene_list[cnt].instance_name == ((GameObject_Scene_EDITOR) gameObject_editor.SelectedObject)._obj.instance_name)
               {
                   if (gameObjectScene_list[cnt].mainObject.object_img != null)
                   {
                       gameObjectScene_list[cnt].UpdateRotation(((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.rotation_angle - ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.rotation_angle, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_x + ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img.Width / 2, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.position_scene_y + ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.mainObject.object_img.Height / 2, true);
                       gameObjectScene_list[cnt].UpdateScale(-(((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject).obj.scaling_rate - ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.scaling_rate), true);
                       gameObjectScene_list[cnt].RestoreChildPosition(((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.position_scene_x, ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj.position_scene_y, true);
                       UpdateWorldChilds(gameObjectScene_list[cnt]);
                   }

                   gameObjectScene_list[cnt] = ((GameObject_Scene_EDITOR)gameObject_editor.SelectedObject)._obj;
                   break;
               }
           }

           isLcontrol = false;
           gameObject_editor.SelectedObject = null;
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

       private void menuItem_TOpenEditor_Click(object sender, EventArgs e)
       {
           TileMapEditor tile_map_editor = new TileMapEditor(this);

           tile_map_editor.Show();
       }

       private void menuItem_ImportPackage_Click(object sender, EventArgs e)
       {
           PackageManager.PackageManager pak_manager = new PackageManager.PackageManager(Application.StartupPath, project_default_dir);
           OpenFileDialog open_file_dlg = new OpenFileDialog( );

           open_file_dlg.InitialDirectory = Application.StartupPath + "\\Packages";
           open_file_dlg.ShowDialog();

           if (Path.GetExtension(open_file_dlg.FileName) == ".pak" || Path.GetExtension(open_file_dlg.FileName) == "pak")
           {
               PackageManager.Package pak = pak_manager.ReadPackage(open_file_dlg.FileName);

               if (pak != null)
               {
                   pak_manager.ExtractPackage(pak);

                   gameObject_list.Clear();
                   reloadFileTree();

                   foreach (string path in Directory.GetFiles(project_default_dir + "\\Game-Objects"))
                   {
                       // Call LoadObject here with parameter path + .obj for providing extension.
                       gameObject_list.Add(LoadObject(path));
                   }
               }
               else MessageBox.Show("Package Import Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }

       private void menuItem_CreatePackage_Click(object sender, EventArgs e)
       {
           CreatePackage create_pak = new CreatePackage(this);

           create_pak.Show();
       }


       private void LoadProjectDirectly(string data )
       {
           if (MessageBox.Show("Save current level ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) menuItem_SaveLevel_Click(null, null);

           LoadProjectScreen load_prj_screen = new LoadProjectScreen(menu_screen, this);

           load_prj_screen.txt_path.Text = data;
           load_prj_screen.txt_project_name.Text = data.Substring(data.LastIndexOf('\\') + 1, data.Length - (data.LastIndexOf('\\') + 1));

           canClose = false;

           load_prj_screen.btn_load_Click(null, null);
       }

       private void menuItem_Recent1_Click(object sender, EventArgs e)
       {
           if (Directory.Exists(menuItem_Recent1.Text)) LoadProjectDirectly(menuItem_Recent1.Text);
           else MessageBox.Show("Directory dosen't exist anymore.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }

       private void menuItem_Recent2_Click(object sender, EventArgs e)
       {
           if (Directory.Exists(menuItem_Recent2.Text)) LoadProjectDirectly(menuItem_Recent2.Text);
           else MessageBox.Show("Directory dosen't exist anymore.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }

       private void menuItem_Recent3_Click(object sender, EventArgs e)
       {
           if (Directory.Exists(menuItem_Recent3.Text)) LoadProjectDirectly(menuItem_Recent3.Text);
           else MessageBox.Show("Directory dosen't exist anymore.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
       
       private void rotateCamera()
       {
           for (int cntr = 0; cntr < gameObjectScene_list.Count; cntr++)
               if (gameObjectScene_list[cntr].mainObject.object_img != null)
               {
                   GameObject_Scene gameObj = gameObjectScene_list[cntr];

                   gameObj.ApplyRotation(-cam_angle);

                   gameObjectScene_list[cntr] = gameObj;
               }
       }

       private void btn_rotate_left_Click(object sender, EventArgs e)
       {
           cam_angle++;

           rotateCamera();
       }

       private void btn_rotate_right_Click(object sender, EventArgs e)
       {
           cam_angle--;

           rotateCamera();
       }
    }

    public class GameObject_Scene_EDITOR
    {
        public Editor.GameObject_Scene obj;
        public Editor.GameObject_Scene _obj;
        private Editor editor_handle;

        public GameObject_Scene_EDITOR(Editor.GameObject_Scene obj,Editor editor_handle)
        {
            this.obj = obj;
            this._obj = obj;
            this.editor_handle = editor_handle;
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

        public string Name
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

        public float Rotation
        {
            get
            {
                return obj.rotation_angle;
            }
            set
            {
                float prev_angle = obj.rotation_angle;
                obj.rotation_angle = value;

                if (obj.mainObject.object_img != null)
                {
                    obj.ApplyRotation(-obj.rotation_angle);
                    obj.UpdateRotation((obj.rotation_angle - prev_angle), obj.position_scene_x + obj.mainObject.object_img.Width / 2, obj.position_scene_y + obj.mainObject.object_img.Height / 2, true);

                    for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                    {
                        if (editor_handle.gameObjectScene_list[cnt].instance_name == _obj.instance_name)
                        {
                            editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);
                            break;
                        }
                    }

                    for(int cnt = 0;cnt < editor_handle.gameObjectScene_list.Count;cnt++)
                    {
                        if (editor_handle.gameObjectScene_list[cnt].instance_name == _obj.instance_name)
                        {
                            Editor.GameObject_Scene gameObject = editor_handle.gameObjectScene_list[cnt];

                            gameObject.ApplyRotation(-obj.rotation_angle);
                        
                            editor_handle.gameObjectScene_list[cnt] = gameObject;

                            break;
                        }
                    }
                }
            }
        }

        public float Scale
        {
            get
            {
                return obj.scaling_rate;
            }
            set
            {
                float prev_scale = obj.scaling_rate;
                obj.scaling_rate = value;

                if (obj.scaling_rate < 0) obj.scaling_rate = 0f;

                if (obj.mainObject.object_img != null)
                {
                    obj.UpdateScale((obj.scaling_rate - prev_scale), true);

                    for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                    {
                        if (editor_handle.gameObjectScene_list[cnt].instance_name == _obj.instance_name)
                        {
                            editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);
                            break;
                        }
                    }
                }

                for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                {
                    if (editor_handle.gameObjectScene_list[cnt].instance_name == _obj.instance_name)
                    {
                        Editor.GameObject_Scene gameObject = editor_handle.gameObjectScene_list[cnt];

                        gameObject.scaling_rate = obj.scaling_rate;

                        editor_handle.gameObjectScene_list[cnt] = gameObject;

                        break;
                    }
                }
            }
        }

        public bool AllowCameraTranslation
        {
            get { return obj.AllowCameraTranslation; }
            set { obj.AllowCameraTranslation = value; }
        }

        public bool AllowCameraRotation
        {
            get { return obj.AllowCameraRotation; }
            set { obj.AllowCameraRotation = value; }
        }

        public bool Visibility
        {
            get { return obj.Visibility; }
            set 
            { 
                obj.Visibility = value; 

                for(int cntr = 0;cntr < editor_handle.gameObjectScene_list.Count;cntr++)
                {
                    if (editor_handle.gameObjectScene_list[cntr].instance_name == _obj.instance_name)
                    {
                        Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cntr];

                        gameObj.Visibility = value;

                        editor_handle.gameObjectScene_list[cntr] = gameObj;
                    }
                }
            }
        }

        public int CollisionRectX
        {
            get { return obj.collision_rectX; }
            set 
            { 
                obj.collision_rectX = value;

                for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                {
                    if (editor_handle.gameObjectScene_list[cntr].instance_name == _obj.instance_name)
                    {
                        Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cntr];

                        gameObj.collision_rectX = value;

                        editor_handle.gameObjectScene_list[cntr] = gameObj;
                    }
                }
            }
        }

        public int CollisionRectY
        {
            get { return obj.collision_rectY; }
            set 
            { 
                obj.collision_rectY = value;

                for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                {
                    if (editor_handle.gameObjectScene_list[cntr].instance_name == _obj.instance_name)
                    {
                        Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cntr];

                        gameObj.collision_rectY = value;

                        editor_handle.gameObjectScene_list[cntr] = gameObj;
                    }
                }
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
                else if (cur_ch + 10 < 91)
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

                    Process compiler = Process.Start(Application.StartupPath + "\\bc.exe", "-p " + editor_handle.platform_id + " -o \"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + platform_ext + "\" \"" + path + "\"");

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
                    else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
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
                        if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) file_writer.WriteLine("this.addPoint( new Vector2(" + point.position_x + "," + point.position_y + "));");
                        else file_writer.WriteLine("this->addPoint({" + point.position_x + "," + point.position_y + "});");

                    file_writer.WriteLine("}"); // End of constructor.
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) file_writer.WriteLine("}"); // End of class.

                    file_writer.Flush();

                    file_writer.Close();

                    rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + "_Navigator" + platform_ext);
                    if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5) rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" " , "") + "_Navigator.h");
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
                            file_writer.WriteLine("this.addFrame(Image.FromFile(Application.StartupPath + \"\\\\Data\\\\" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file))  + ".X" +"\"));");
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
                        file_writer.WriteLine("try {");

                        foreach (string anim_file in baseAnim.frame_list)
                            if (editor_handle.platform_id == 3) file_writer.WriteLine("this.addFrame(Image.createImage(\"/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\"));"); else file_writer.WriteLine("this.addFrame(ImageIO.read(new File(\"/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\")));");

                        file_writer.WriteLine("} \n catch (IOException ex) {");
                        file_writer.WriteLine("}");
                    }
                    else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
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

                        foreach (string anim_file in baseAnim.frame_list) file_writer.WriteLine("this->addFrame(\"./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(anim_file)) + ".X" + "\");");
                    }

                    file_writer.WriteLine("}"); // End of constructor.
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) file_writer.WriteLine("}"); // End of class.

                    file_writer.Flush();

                    file_writer.Close();

                    rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path) + "_Animation" + platform_ext);
                    if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5) rem_files.Add(editor_handle.project_default_dir + "\\Game-Scripts\\" + Path.GetFileNameWithoutExtension(path).Replace(" ","") + "_Animation.h");
                }
            }

            Thread.Sleep(250);

            log_window.addLog("Animation Codes Generation Complete...");

            build_progress.progress();
            Thread.Sleep(500);

            log_window.addLog("Started Generating Base...");

            StreamWriter stm_wr = null;

            if (editor_handle.platform_id == 1) stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\__build");
            else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3) stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\AppMain.java");
            else stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\AppMain.cpp");

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
                    if (ch == '@')
                    {
                        isString = false;
                        token_list.Add(cur_token);
                        cur_token = "";
                    }
                    else cur_token += ch;
                else
                    if (ch == ' ')
                        if (cur_token != "")
                        {
                            token_list.Add(cur_token);
                            cur_token = "";
                        }
                        else ;
                    else if (ch == '@')
                        if (!isString)
                        {
                            isString = true;
                            cur_token = "@";
                        }
                        else ;
                    else cur_token += ch;
            }

            if (cur_token != "") token_list.Add(cur_token);

            string cur_action = "";

            foreach (object obj in token_list)
            {
                string str = (string)obj;

                if (cur_action == "")
                    if (str == "Level_Name:") cur_action = "Level_Name";
                    else if (str == "Speed:") cur_action = "Speed";
                    else if (str == "Back_Colour:") cur_action = "Back_Colour";
                    else if (str == "Object:") cur_action = "Object";
                    else ;
                else
                {
                    if (cur_action == "Level_Name")
                    {
                        if (str.Substring(0, 1) == "@") level.level_name = str.Substring(1, str.Length - 1);

                        cur_action = "";
                    }
                    else if (cur_action == "Speed")
                    {
                        level.level_speed = int.Parse(str);
                        cur_action = "";
                    }
                    else if (cur_action == "Back_Colour")
                    {
                        level.back_color = Color.FromArgb(int.Parse(str), 0, 0, 0);
                        cur_action = "Back_ColourR";
                    }
                    else if (cur_action == "Back_ColourR")
                    {
                        level.back_color = Color.FromArgb(level.back_color.A, int.Parse(str), 0, 0);
                        cur_action = "Back_ColourG";
                    }
                    else if (cur_action == "Back_ColourG")
                    {
                        level.back_color = Color.FromArgb(level.back_color.A, level.back_color.R, int.Parse(str), 0);
                        cur_action = "Back_ColourB";
                    }
                    else if (cur_action == "Back_ColourB")
                    {
                        level.back_color = Color.FromArgb(level.back_color.A, level.back_color.R, level.back_color.G, int.Parse(str));
                        cur_action = "";
                    }
                    else if (cur_action == "Object")
                    {
                        //  MessageBox.Show(str.Substring(1, str.Length - 1));

                        if (str.Substring(0, 1) == "@")
                            foreach (Editor.GameObject gameObj in editor_handle.gameObject_list)
                                if (gameObj.object_name == str.Substring(1, str.Length - 1))
                                {
                                    gameObject.mainObject = gameObj;
                                    gameObject.Initialize();
                                }

                        cur_action = "Object_NAME";
                    }
                    else if (cur_action == "Object_NAME")
                    {
                        if (str.Substring(0, 1) == "@") gameObject.instance_name = str.Substring(1, str.Length - 1);

                        cur_action = "Object_PositionX";
                    }
                    else if (cur_action == "Object_PositionX")
                    {
                        gameObject.position_x = int.Parse(str);

                        cur_action = "Object_PositionY";
                    }
                    else if (cur_action == "Object_PositionY")
                    {
                        gameObject.position_y = int.Parse(str);

                        cur_action = "Object_Depth";
                    }
                    else if (cur_action == "Object_Depth")
                    {
                        gameObject.depth = int.Parse(str);

                        cur_action = "Object_Tiled";
                    }
                    else if (cur_action == "Object_Tiled")
                    {
                        gameObject.isTile = bool.Parse(str);

                        cur_action = "Object_Rotation";
                    }
                    else if (cur_action == "Object_Rotation")
                    {
                        gameObject.rotation_angle = float.Parse(str);
                        gameObject.ApplyRotation(-gameObject.rotation_angle);

                        cur_action = "Object_Scale";
                    }
                    else if (cur_action == "Object_Scale")
                    {
                        gameObject.scaling_rate = float.Parse(str);

                        cur_action = "Object_CameraTranslation";
                    }
                    else if (cur_action == "Object_CameraTranslation")
                    {
                        gameObject.AllowCameraTranslation = bool.Parse(str);

                        cur_action = "Object_CameraRotation";
                    }
                    else if (cur_action == "Object_CameraRotation")
                    {
                        gameObject.AllowCameraRotation = bool.Parse(str);

                        cur_action = "Object_Visibility";
                    }
                    else if (cur_action == "Object_Visibility")
                    {
                        gameObject.Visibility = bool.Parse(str);

                        cur_action = "Object_CollisionX";
                    }
                    else if (cur_action == "Object_CollisionX")
                    {
                        gameObject.collision_rectX = int.Parse(str);

                        cur_action = "Object_CollisionY";
                    }
                    else if (cur_action == "Object_CollisionY")
                    {
                        gameObject.collision_rectY = int.Parse(str);

                        cur_action = "Object_Childs";
                    }
                    else if (cur_action == "Object_Childs")
                        if (str == ";")
                        {
                            cur_action = "";
                            object_array.Add(gameObject);
                            gameObject = new Editor.GameObject_Scene();
                        }
                        else
                        {
                            Editor.GameObject_Scene gameObj = new Editor.GameObject_Scene();

                            gameObj.instance_name = str;

                            gameObject.child_list.Add(gameObj);
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
            else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
            {
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2main.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_image.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_ttf.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_mixer.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"SDL2_net.lib\")");
                stm_wr.WriteLine("#pragma comment(lib,\"NativeRuntime.lib\")");

                foreach(string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                    if (Path.GetExtension(path) == ".lib" || Path.GetExtension(path) == "lib") stm_wr.WriteLine("#pragma comment(lib,\"" + Path.GetFileName(path) + "\")");

                stm_wr.WriteLine("#include<HeavyEngine.h>");
                stm_wr.WriteLine("#include \"Scripts.h\"");
                stm_wr.WriteLine("int main(int argc , char * argv[]) {");
            }

            if (editor_handle.platform_id == 3) stm_wr.WriteLine("HApplication.Initialize(\"" + editor_handle.project_info.project_name + "\",this);"); 
            else if (editor_handle.platform_id == 1 || editor_handle.platform_id == 2) stm_wr.WriteLine("HApplication.Initialize(\"" + editor_handle.project_info.project_name + "\");"); 
            else stm_wr.WriteLine("HApplication::Initialize(\"" + editor_handle.project_info.project_name + "\");");

            Thread.Sleep(100);

            log_window.addLog("Generating Objects...");

            for (int cntr = 0;cntr < editor_handle.gameObject_list.Count; cntr++)
                if (editor_handle.platform_id == 1) stm_wr.WriteLine("ObjectManager.loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",Application.StartupPath + \"\\\\Data\\\\" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ");");
                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3) stm_wr.WriteLine("ObjectManager.loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "/Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ");");
                else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
                    if (editor_handle.gameObject_list[cntr].object_img != null)
                        stm_wr.WriteLine("ObjectManager::loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_img.Width + "," + editor_handle.gameObject_list[cntr].object_img.Height + ");");
                    else
                        stm_wr.WriteLine("ObjectManager::loadObject(\"" + editor_handle.gameObject_list[cntr].object_name + "\",\"" + editor_handle.gameObject_list[cntr].object_text + "\",\"" + "./Data/" + encryptFileName(Path.GetFileNameWithoutExtension(editor_handle.gameObject_list[cntr].path)) + ".X" + "\"," + editor_handle.gameObject_list[cntr].object_tag + "," + editor_handle.gameObject_list[cntr].isStatic.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_physics.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_rigid.ToString().ToLower() + "," + editor_handle.gameObject_list[cntr].object_collider.ToString().ToLower() + ",0,0);");

            stm_wr.WriteLine("Scene firstScene;");
         
            if (Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels").Length > 1) stm_wr.WriteLine("Scene newScene;");

            if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("GameObject_Scene obj;"); else stm_wr.WriteLine("GameObject_Scene * obj;");

            Thread.Sleep(100);

            log_window.addLog("Generating Levels...");

            foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels"))
            {
                stm_rd = new StreamReader(path);

                while (!stm_rd.EndOfStream) processLine_code(stm_rd.ReadLine());

                if (level.level_name == Path.GetFileNameWithoutExtension(editor_handle.project_info.project_firstlevel))
                {
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("firstScene = new Scene( );"); else stm_wr.WriteLine("firstScene = Scene( );");
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
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("newScene = new Scene( );"); else stm_wr.WriteLine("newScene = Scene( );");
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
                    
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5)
                    {
                        stm_wr.WriteLine("obj.pos_x = " + object_array[cntr].position_x + ";");
                        stm_wr.WriteLine("obj.pos_y = " + object_array[cntr].position_y + ";");
                        stm_wr.WriteLine("obj.depth = " + object_array[cntr].depth + ";");
                        stm_wr.WriteLine("obj.instance_name = \"" + object_array[cntr].instance_name + "\";");
                        stm_wr.WriteLine("obj.obj_instance = ObjectManager.findGameObjectWithName(\"" + object_array[cntr].mainObject.object_name + "\");");
                        stm_wr.WriteLine("obj.Initialize();");
                        stm_wr.WriteLine("obj.SetScale(" + object_array[cntr].scaling_rate + ");");
                        stm_wr.WriteLine("obj.SetRotationAngle(" + object_array[cntr].rotation_angle + ");");
                        stm_wr.WriteLine("obj.AllowCameraTranslation = " + object_array[cntr].AllowCameraTranslation.ToString().ToLower() + ";");
                        stm_wr.WriteLine("obj.AllowCameraRotation = " + object_array[cntr].AllowCameraRotation.ToString().ToLower() + ";");
                        stm_wr.WriteLine("obj.Visibility = " + object_array[cntr].Visibility.ToString().ToLower() + ";");
                        stm_wr.WriteLine("obj.CollisionRectX = " + object_array[cntr].collision_rectX.ToString() + ";");
                        stm_wr.WriteLine("obj.CollisionRectY = " + object_array[cntr].collision_rectY.ToString() + ";");

                        for (int cnt = 0; cnt < object_array[cntr].child_list.Count;cnt++ )
                        {
                            stm_wr.WriteLine("obj.AddChild(\"" + object_array[cntr].child_list[cnt].instance_name + "\");");
                        }

                            for (int cnt = 0; cnt < object_array[cntr].mainObject.scripts.Count; cnt++)
                            {
                                bool isSuccess = false;

                                if (editor_handle.platform_id == 1)
                                {
                                    if (Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == ".cs" || Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == "cs") isSuccess = true;
                                }
                                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                                {
                                    if (Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == ".java" || Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == "java") isSuccess = true;
                                }
                                /* else if (editor_handle.platform_id == 4)
                                {
                                    if (Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == ".cpp" || Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == "cpp") isSuccess = true;
                                } */

                                if (isSuccess) stm_wr.WriteLine("obj.registerScript(new " + Path.GetFileNameWithoutExtension(object_array[cntr].mainObject.scripts[cnt]) + "( ));");
                            }
                    }
                    else
                    {
                        stm_wr.WriteLine("obj->pos_x = " + object_array[cntr].position_x + ";");
                        stm_wr.WriteLine("obj->pos_y = " + object_array[cntr].position_y + ";");
                        stm_wr.WriteLine("obj->depth = " + object_array[cntr].depth + ";");
                        stm_wr.WriteLine("obj->instance_name = \"" + object_array[cntr].instance_name + "\";");
                        stm_wr.WriteLine("obj->obj_instance = ObjectManager::findGameObjectWithName(\"" + object_array[cntr].mainObject.object_name + "\");");
                        stm_wr.WriteLine("obj->Initialize();");
                        stm_wr.WriteLine("obj->SetScale(" + ((object_array[cntr].scaling_rate == 0) ? 1 : object_array[cntr].scaling_rate) + ");");
                        stm_wr.WriteLine("obj->SetRotationAngle(" + object_array[cntr].rotation_angle + ");");
                        stm_wr.WriteLine("obj->AllowCameraTranslation = " + object_array[cntr].AllowCameraTranslation.ToString().ToLower( ) + ";");
                        stm_wr.WriteLine("obj->AllowCameraRotation = " + object_array[cntr].AllowCameraRotation.ToString().ToLower() + ";");
                        stm_wr.WriteLine("obj->Visibility = " + object_array[cntr].Visibility.ToString().ToLower() + ";");
                        stm_wr.WriteLine("obj->CollisionRectX = " + object_array[cntr].collision_rectX.ToString() + ";");
                        stm_wr.WriteLine("obj->CollisionRectY = " + object_array[cntr].collision_rectY.ToString() + ";");

                        for (int cnt = 0; cnt < object_array[cntr].child_list.Count; cnt++)
                        {
                            stm_wr.WriteLine("obj->AddChild(\"" + object_array[cntr].child_list[cnt].instance_name + "\");");
                        }

                        for (int cnt = 0; cnt < object_array[cntr].mainObject.scripts.Count; cnt++)
                        {
                            if (Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == ".cpp" || Path.GetExtension(object_array[cntr].mainObject.scripts[cnt]) == "cpp") stm_wr.WriteLine("obj->registerScript(new " + Path.GetFileNameWithoutExtension(object_array[cntr].mainObject.scripts[cnt]) + "( ));");
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
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("SceneManager.addScene(firstScene);"); else stm_wr.WriteLine("SceneManager::addScene(firstScene);"); 
                else
                    if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("SceneManager.addScene(newScene);"); else stm_wr.WriteLine("SceneManager::addScene(newScene);");

                object_array.Clear();
                level = new Editor.GameLevel();
                stm_rd.Close();
            }

            if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("HApplication.loadScene(firstScene);"); else stm_wr.WriteLine("HApplication::loadScene(firstScene);");

            if (editor_handle.platform_id != 3 && editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("firstScene.startScene(HApplication.getCanvas( ));");
            else if (editor_handle.platform_id == 3)
            {
                stm_wr.WriteLine("Display.getDisplay(this).setCurrent(firstScene);");
                stm_wr.WriteLine("firstScene.startScene();");
            }
            else stm_wr.WriteLine("HApplication::start( );");

            if (editor_handle.platform_id == 1) stm_wr.WriteLine("Application.Run(HApplication.getWindowHandle( ));");

            if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5) stm_wr.WriteLine("return 0;");

            stm_wr.WriteLine("}"); // For Function
            if (editor_handle.platform_id != 4 && editor_handle.platform_id != 5) stm_wr.WriteLine("}"); // For Class

      
            if (editor_handle.platform_id == 1) stm_wr.WriteLine("}"); // For Namespace
            stm_wr.Flush();
            stm_wr.Close();


            if (Directory.Exists(editor_handle.project_default_dir + "\\Build")) flushDirectories(editor_handle.project_default_dir + "\\Build",false); // Clean the whole directory.

            Thread.Sleep(250);
            log_window.addLog("Generating Base Complete...");

            log_window.addLog("Compiling Everything...");

jmp:
            DirectoryInfo info = null;

            info = Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build");
           
           if (!info.Exists) goto jmp;

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
                comp_param.TreatWarningsAsErrors = false;

                foreach (Assembly path in AppDomain.CurrentDomain.GetAssemblies()) comp_param.ReferencedAssemblies.Add(path.Location);

                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        if (Path.GetExtension(path) == ".dll" || Path.GetExtension(path) == "dll")
                        {
                            comp_param.ReferencedAssemblies.Add(path);
                            File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                        }

                comp_param.ReferencedAssemblies.Add(Application.StartupPath + "\\JVLIB.dll");
                comp_param.ReferencedAssemblies.Add(Application.StartupPath + "\\Runtime.dll");

                string[] src_array = new string[0];

                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                    if (Path.GetExtension(path).ToLower() == ".cs" || Path.GetExtension(path).ToLower() == "cs")
                    {
                        Array.Resize<string>(ref src_array, src_array.Length + 1);
                        src_array[src_array.Length - 1] = path;
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
                    File.Copy(Application.StartupPath + "\\Interop.WMPLib.dll", editor_handle.project_default_dir + "\\Build\\Interop.WMPLib.dll");

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

                        foreach (string path in rem_files)
                        {
                            File.Delete(path);
                        }

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

                        bat_stm.WriteLine("Class-Path: JRuntime.jar" + libs);

                        bat_stm.Flush();
                        bat_stm.Close();

                        File.Copy(Application.StartupPath + "\\JRuntime.jar" , editor_handle.project_default_dir + "\\Build\\JRuntime.jar");
                        File.Copy(Application.StartupPath + "\\jl1.0.1.jar", editor_handle.project_default_dir + "\\Build\\jl1.0.1.jar");
                        File.Copy(Application.StartupPath + "\\thumbnailator-0.4.8.jar", editor_handle.project_default_dir + "\\Build\\thumbnailator-0.4.8.jar");

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

                    pinfo.Arguments = "-target 3.4 -source 3.4 -bootclasspath \"" + Application.StartupPath + "\\cldc_1.1.jar\";\"" + Application.StartupPath + "\\midp_2.1.jar\"" + " -cp ." + lib_str + ";" + "\"" + Application.StartupPath + "\\JMRuntime.jar\"; \"" + editor_handle.project_default_dir + "\\AppMain.java\"" + file_path_com;
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
                        bat_stm.WriteLine("preverify -classpath \"" + Application.StartupPath + "\\JMRuntime.jar\";\"" + Application.StartupPath + "\\midp_2.1.jar\";\"" + Application.StartupPath + "\\cldc_1.1.jar\"; -d \"" + editor_handle.project_default_dir + "\\Build\" code");
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
                        flushDirectories(editor_handle.project_default_dir + "\\Build",false);
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

                    flushDirectories(editor_handle.project_default_dir + "\\Build\\Debug",false);
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
            else if (editor_handle.platform_id == 5)
            {
                // Linux Platform Generation.
                
                build_progress.progress();
                Thread.Sleep(500);

                log_window.addLog("Running C++ Compiler...");

                CppResourceCopy copyResources = delegate(bool isBuild)
                {
                    string script_list = "";
                    string header_list = "";
                    StreamWriter script_header_wr = null;

                    script_header_wr = new StreamWriter(editor_handle.project_default_dir + "\\Build\\Scripts.h"); 
                    script_header_wr.WriteLine("#ifndef SCRIPT_H");

                    foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                         if (Path.GetExtension(path) == ".cpp" || Path.GetExtension(path) == "cpp")
                         {
                                if (isBuild) script_list += "<ClCompile Include = \"" + Path.GetFileName(path) + "\" />\n";
                                File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                         }
                         else if (Path.GetExtension(path) == ".h" || Path.GetExtension(path) == "h")
                         {
                                if (isBuild) header_list += "<ClInclude Include = \"" + Path.GetFileName(path) + "\" />\n"; 
                                script_header_wr.WriteLine("#include \"" + Path.GetFileName(path) + "\"");
                                File.Copy(path, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(path));
                         }

                    script_header_wr.WriteLine("#endif"); 
                    script_header_wr.Flush(); 
                    script_header_wr.Close();

                    Directory.CreateDirectory(editor_handle.project_default_dir + "\\Build\\Data");

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                        File.Copy(file, editor_handle.project_default_dir + "\\Build\\Data\\" + encryptFileName(Path.GetFileNameWithoutExtension(file)) + ".X");

                    // Copy DLL / Runtime Libraries.
                    foreach (string file in Directory.GetFiles(Application.StartupPath + "\\Runtime"))
                        if (Path.GetExtension(file) == ".so" || Path.GetExtension(file) == "so") File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file));

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        if (Path.GetExtension(file) == ".so" || Path.GetExtension(file) == "so" || Path.GetExtension(file) == ".a" || Path.GetExtension(file) == "a") File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file));

                    File.Copy(editor_handle.project_default_dir + "\\AppMain.cpp", editor_handle.project_default_dir + "\\Build\\AppMain.cpp");

                    if (isBuild)
                    {
                        StreamWriter project_file_wr = new StreamWriter(editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj");

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
                    }
                };

                copyResources(true);

               /* StreamWriter project_file_wr = new StreamWriter(editor_handle.project_default_dir + "\\Build\\" + editor_handle.project_info.project_name + ".vcxproj");

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
                project_file_wr.Close(); */

                // Run MSBuild and compile the scripts on Windows.
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
                        flushDirectories(editor_handle.project_default_dir + "\\Build",false);
                        log_window.Show();
                        build_progress.Close();

                        foreach (string path in rem_files) File.Delete(path);

                        return false;
                    }

                    build_progress.progress();
                    Thread.Sleep(500);

                    flushDirectories(editor_handle.project_default_dir + "\\Build");
                   
                    copyResources(false); // Copy the resources again for final packing.


                    /*
                     *  Copy all the lib files to the build directory.
                     */
                    StreamWriter stm_wr_libs = new StreamWriter(editor_handle.project_default_dir + "\\Build\\Libs.prop");
                    
                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
                        if (Path.GetExtension(file) == ".a" || Path.GetExtension(file) == "a" || Path.GetExtension(file) == ".so" || Path.GetExtension(file) == "so") { stm_wr_libs.WriteLine(Path.GetFileNameWithoutExtension(file).Substring(3, Path.GetFileNameWithoutExtension(file).Length - 3)); File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file)); }

                    stm_wr_libs.Flush();
                    stm_wr_libs.Close();

                    foreach (string file in Directory.GetFiles(Application.StartupPath + "\\lib"))
                        if (Path.GetExtension(file) == ".a" || Path.GetExtension(file) == "a") File.Copy(file, editor_handle.project_default_dir + "\\Build\\" + Path.GetFileName(file));

                    // Packup the files in a pak file.
                    pinfo.FileName = Application.StartupPath + "\\FileZip.exe";
                    pinfo.Arguments = "-1 -o \"" + editor_handle.project_default_dir + "\\Build\\GameData.pak\" -i";

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Build")) pinfo.Arguments += " \"" + file + "\""; // Copy all files from build folder.
                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Build\\Data")) pinfo.Arguments += " \"" + file + "\""; // Copy all files from data folder.
                    foreach (string file in Directory.GetFiles(Application.StartupPath + "\\header_includes")) pinfo.Arguments += " \"" + file + "\""; // Copy all files from header folder.

                    process_handle = Process.Start(pinfo); // Call FileZip.

                    process_handle.WaitForExit();

                    log_window.addLog(process_handle.StandardOutput.ReadToEnd());
                    log_window.addLog(process_handle.StandardError.ReadToEnd());

                    File.Copy(Application.StartupPath + "\\FileZip.elf", editor_handle.project_default_dir + "\\Build\\FileZip.elf");
                    File.Copy(Application.StartupPath + "\\GameBuilder.elf", editor_handle.project_default_dir + "\\Build\\GameBuilder.elf");
                    flushDirectories(editor_handle.project_default_dir + "\\Build\\Data",false);

                    foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Build"))
                        if ((Path.GetExtension(file) != ".elf" && Path.GetExtension(file) != "elf") && (Path.GetExtension(file) != ".pak" && Path.GetExtension(file) != "pak")) File.Delete(file);

                    File.Delete(editor_handle.project_default_dir + "\\AppMain.cpp");
                    
                    build_progress.progress();
                    Thread.Sleep(500);

                    log_window.addLog("Compile Completed!...");
                    log_window.addLog("Build Complete");

                    // Finish.
                    MessageBox.Show("Build Succesfull!", "Succesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (string path in rem_files) File.Delete(path);

                    build_progress.Close();
                    log_window.Show();

                    return true;
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show("MSBuild not installed properly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log_window.addLog("MSBuild Installation Not Found ! Please Install MSBuild v12.0 For Heavy Engine And ReBuild");
                    log_window.addLog("Build Failed...");
                }

                foreach (string path in rem_files) File.Delete(path);

                build_progress.Close();
                log_window.Show();
            }

            return false;
        }

        private void flushDirectories(string path,bool KeepCurrentDirectory = true)
        {
            foreach (string dir_path in Directory.GetDirectories(path))
            {
                flushDirectories(dir_path,false);
            }

            foreach (string file_path in Directory.GetFiles(path))
            {
                File.Delete(file_path);
            }

            if (!KeepCurrentDirectory) Directory.Delete(path);
        }

        delegate void CppResourceCopy( bool isBuild );
    }


}