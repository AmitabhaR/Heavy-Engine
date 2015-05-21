using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Heavy_Engine
{
    public partial class NavigationEditor : Form
    {
        Editor editor_handle;
        private int mouseX = 0; 
        private int mouseY = 0;
        private List<Editor.GameObject_Scene> gameObject_list = new List<Editor.GameObject_Scene>( );
        private NavigationPoint navigation_start = null;
        private string cur_file = "";
        private NavigationPoint selected_nav_point = null;
        private int camX = 0;
        private int camY = 0;
        private MenuItem shortmenu_createPoint = new MenuItem("Create Point");
        private MenuItem shortmenu_deletePoint = new MenuItem("Delete Point");
        private MenuItem shortmenu_selectPoint = new MenuItem("Select Point");
        private MenuItem shortmenu_deselectPoint = new MenuItem("Deselect Point");

        public NavigationEditor(Editor editor_handle,string path)
        {
            this.editor_handle = editor_handle;

            if (File.Exists(path))
            {
                cur_file = path;
            }
            else
            {
                cur_file = "new_file";
            }

            InitializeComponent();
        }

        private void NavigationEditor_Load(object sender, EventArgs e)
        {
            this.canvas.Paint += canvas_Paint;
            this.tmr_draw.Tick += tmr_draw_Tick;
            this.canvas.MouseMove += canvas_MouseMove;
            this.canvas.MouseClick += canvas_MouseClick;
            this.KeyDown += NavigationEditor_KeyDown;
            this.shortmenu_createPoint.Click += shortmenu_createPoint_Click;
            this.shortmenu_deletePoint.Click += shortmenu_deletePoint_Click;
            this.shortmenu_deselectPoint.Click += shortmenu_deselectPoint_Click;
            this.shortmenu_selectPoint.Click += shortmenu_selectPoint_Click;

            reloadGameObjects();

           if (File.Exists(cur_file)) readNavigationFile(cur_file);
        }

        void NavigationEditor_KeyDown(object sender, KeyEventArgs e)
        {
           NavigationPoint nav_point = navigation_start;

           if (e.KeyCode == Keys.Up)
           {
              while(nav_point != null)
              {
                  nav_point.scene_y += 5;

                  nav_point = nav_point.next_handle;
              }

              camY -= 5;
           }
           else if (e.KeyCode == Keys.Down)
           {
               while (nav_point != null)
               {
                   nav_point.scene_y -= 5;

                   nav_point = nav_point.next_handle;
               }

               camY += 5;
           }
           else if (e.KeyCode == Keys.Left)
           {
               while (nav_point != null)
               {
                   nav_point.scene_x += 5;

                   nav_point = nav_point.next_handle;
               }

               camX -= 5;
           }
           else if (e.KeyCode == Keys.Right)
           {
               while (nav_point != null)
               {
                   nav_point.scene_x -= 5;

                   nav_point = nav_point.next_handle;
               }

               camX += 5;
           }
        }

        private void shortmenu_selectPoint_Click(object sender, EventArgs e)
        {
            if (lb_points.SelectedIndex != -1)
            {
                selected_nav_point = pointAt(lb_points.SelectedIndex);
            }
        }

        void shortmenu_deselectPoint_Click(object sender, EventArgs e)
        {
            selected_nav_point = null;
        }

        void shortmenu_deletePoint_Click(object sender, EventArgs e)
        {
            NavigationPoint cur_point = navigation_start;
            NavigationPoint cur_parent = null;
            bool isDeleted = false;

            while(cur_point != null)
            {
                if (!isDeleted)
                {
                    if (cur_point == selected_nav_point)
                    {
                        isDeleted = true;

                        if (cur_point.next_handle != null)
                        {
                            cur_point.position_x = cur_point.next_handle.position_x;
                            cur_point.position_y = cur_point.next_handle.position_y;
                            cur_point.scene_x = cur_point.next_handle.scene_x;
                            cur_point.scene_y = cur_point.next_handle.scene_y;
                        }
                    }
                    else
                    {
                        cur_parent = cur_point;
                    }
                    
                    cur_point = cur_point.next_handle;
                }
                else
                {
                    if (cur_point.next_handle != null)
                    {
                        cur_point.position_x = cur_point.next_handle.position_x;
                        cur_point.position_y = cur_point.next_handle.position_y;
                        cur_point.scene_x = cur_point.next_handle.scene_x;
                        cur_point.scene_y = cur_point.next_handle.scene_y;
                        cur_parent = cur_point;  
                    }

                    cur_point = cur_point.next_handle;
                }
            }

            if (cur_parent != null) cur_parent.next_handle = null;

            loadPointList();

            selected_nav_point = null;
        }

        void shortmenu_createPoint_Click(object sender, EventArgs e)
        {
            selected_nav_point = new NavigationPoint();

            selected_nav_point.position_x = mouseX;
            selected_nav_point.position_y = mouseY;

            NavigationPoint cur_point = navigation_start;

            while(cur_point != null)
            {
                if (cur_point.next_handle == null) break;
                cur_point = cur_point.next_handle;
            }

            if (cur_point == null)
            {
                navigation_start = selected_nav_point;
            }
            else
            {
                cur_point.next_handle = selected_nav_point;
            }
        }

        void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selected_nav_point != null)
                {
                    selected_nav_point = null;
                    return;
                }

                NavigationPoint nav_point = navigation_start;

                while(nav_point != null)
                {
                    if (nav_point.position_x + 20 > mouseX && nav_point.position_x < mouseX && nav_point.position_y + 20 > mouseY && nav_point.position_y < mouseY)
                    {
                        selected_nav_point = nav_point;
                        return;
                    }

                    nav_point = nav_point.next_handle;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenu short_menu = new ContextMenu();

                if (selected_nav_point != null)
                {
                    short_menu.MenuItems.AddRange(new MenuItem[] { shortmenu_deselectPoint , shortmenu_deletePoint });
                }
                else
                {
                    short_menu.MenuItems.AddRange(new MenuItem[] { shortmenu_createPoint });
                }

                this.canvas.ContextMenu = short_menu;
            }
        }

        private void reloadGameObjects()
        {
            gameObject_list.Clear();

            foreach (Editor.GameObject_Scene gameObject in editor_handle.gameObjectScene_list)
            {
                gameObject_list.Add(gameObject);
            }
        }

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            if (selected_nav_point != null)
            {
                selected_nav_point.position_x = mouseX + camX;
                selected_nav_point.position_y = mouseY + camY;
                selected_nav_point.scene_x = mouseX;
                selected_nav_point.scene_y = mouseY;

                loadPointList();
            }
        }

        void tmr_draw_Tick(object sender, EventArgs e)
        {
          //  reloadGameObjects();

            lbl_mouseX.Text = "Mouse X : " + mouseX;
            lbl_mouseY.Text = "Mouse Y : " + mouseY;
            lbl_cameraX.Text = "Camera X : " + camX;
            lbl_cameraY.Text = "Camera Y : " + camY;

            this.canvas.Refresh();
        }

        void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

           for(int y = 0;y < canvas.Height;y += 40)
           {
               for(int x = 0;x < canvas.Width;x += 40)
               {
                   e.Graphics.DrawLine(new Pen(Brushes.Gray), new Point(x,0), new Point(x,canvas.Height));
                   e.Graphics.DrawLine(new Pen(Brushes.Gray), new Point(0, y), new Point(canvas.Width, y));
               }
           }

           for (int cnt = 0; cnt < editor_handle.sortedArray.Length; cnt++)
           {
               int cntr = editor_handle.sortedArray[cnt].index;

               if (gameObject_list[cntr].mainObject.object_img != null)
               {
                   e.Graphics.DrawImage(new Bitmap(gameObject_list[cntr].mainObject.object_img, new Size(gameObject_list[cntr].mainObject.object_img.Width, gameObject_list[cntr].mainObject.object_img.Height)), new Point(gameObject_list[cntr].position_scene_x,gameObject_list[cntr].position_scene_y));
               }
               else if (gameObject_list[cntr].mainObject.object_text != "")
               {
                   e.Graphics.DrawString(gameObject_list[cntr].mainObject.object_text, new Font("Verdana", 12), Brushes.White, new Point(gameObject_list[cntr].position_scene_x, gameObject_list[cntr].position_scene_y));
               }
           }

           if (navigation_start != null)
           {
               NavigationPoint cur_point = navigation_start;
               Color start_col = Color.Green;

               while (cur_point != null)
               {
                   e.Graphics.DrawEllipse(new Pen(start_col), new Rectangle(cur_point.scene_x + 10, cur_point.scene_y + 10, 20, 20));

                   if (cur_point.next_handle != null)
                   {
                       e.Graphics.DrawLine(new Pen(Color.Red), new Point(cur_point.scene_x + 20, cur_point.scene_y + 20), new Point(cur_point.next_handle.scene_x + 20, cur_point.next_handle.scene_y + 20));

                       cur_point = cur_point.next_handle;
                   }
                   else
                   {
                       break;
                   }

                  if (start_col == Color.Green) start_col = Color.Yellow;
               }
           }
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current file ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuItem_SaveNavigation_Click(null, null);
            }

            this.Close();
        }

        private void menuItem_NewNavigation_Click(object sender, EventArgs e)
        {
            navigation_start = null;
            selected_nav_point = null;
            reloadGameObjects();
            loadPointList();
            camX = camY = 0;
            cur_file = "new_file";
            editor_handle.reloadFileTree();
        }

        private void menuItem_LoadNavigation_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Navigation";

            open_file_dlg.ShowDialog();

            if (open_file_dlg.FileName == "") return;

            if (Path.GetExtension(open_file_dlg.FileName) == ".nav" || Path.GetExtension(open_file_dlg.FileName) == "nav")
            {
                cur_file = open_file_dlg.FileName;
                readNavigationFile(open_file_dlg.FileName);
            }
            else
            {
                MessageBox.Show("File format invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            camX = camY = 0;
            selected_nav_point = null;
            reloadGameObjects();
        }

        private void readNavigationFile(string path)
        {
            StreamReader stm_rd = new StreamReader(path);
            List<string> token_list = new List<string>();

            while (!stm_rd.EndOfStream)
            {
                string cur_line = stm_rd.ReadLine();
                string cur_token = "";

                foreach(char ch in cur_line)
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

            navigation_start = new NavigationPoint();
            NavigationPoint cur_point = navigation_start;
            string cur_action = "";
            bool isStart = false;

            foreach(string str in token_list)
            {
                if (cur_action == "")
                {
                    if (isStart)
                    {
                        cur_point.next_handle = new NavigationPoint();
                        cur_point = cur_point.next_handle;
                    }

                    cur_point.position_x = int.Parse(str);
                    cur_point.scene_x = cur_point.position_x;
                    cur_action = "get_posy";
                }
                else
                {
                    if (cur_action == "get_posy")
                    {
                        cur_point.position_y = int.Parse(str);
                        cur_point.scene_y = cur_point.position_y;
                        cur_action = "";
                    }
                }

                isStart = true;
            }

            loadPointList();
        }

        private void loadPointList()
        {
            lb_points.Items.Clear();

            NavigationPoint nav_point = navigation_start;
            bool isFirst = true;

            while(nav_point != null)
            {
                if (isFirst)
                {
                    lb_points.Items.Add("Point X : " + nav_point.position_x + " Y : " + nav_point.position_y + " - First Point");
                    isFirst = false;
                }
                else
                {
                    lb_points.Items.Add("Point X : " + nav_point.position_x + " Y : " + nav_point.position_y);
                }

                nav_point = nav_point.next_handle;
            }
        }

        private void menuItem_SaveNavigation_Click(object sender, EventArgs e)
        {
            if (cur_file == "")
            {
                return;
            }
            else if (cur_file == "new_file")
            {
                SaveFileDialog save_file_dlg = new SaveFileDialog();

                save_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Navigation";

                save_file_dlg.ShowDialog();

                if (save_file_dlg.FileName != "")
                {
                    cur_file = save_file_dlg.FileName;
                }
                else
                {
                    return;
                }
            }

            StreamWriter stm_wr = new StreamWriter(cur_file);
            NavigationPoint nav_point = navigation_start;

            while(nav_point != null)
            {
                stm_wr.WriteLine(nav_point.position_x + "," + nav_point.position_y);

                nav_point = nav_point.next_handle;
            }


            stm_wr.Flush();
            stm_wr.Close();
        }

        private NavigationPoint pointAt(int count)
        {
            NavigationPoint cur_point = navigation_start;
            int cnt = 0;

            while(cur_point != null)
            {
                if (cnt == count)
                {
                    return cur_point;
                }

                cnt++;
                cur_point = cur_point.next_handle;
            }

            return null;
        }

        private void lb_points_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContextMenu short_menu = new ContextMenu();

            short_menu.MenuItems.AddRange(new MenuItem[] { shortmenu_selectPoint});

            this.canvas.ContextMenu = short_menu;
        }

        private void menuItem_DeleteNavigation_Click(object sender, EventArgs e)
        {
            if (File.Exists(cur_file)) File.Delete(cur_file);
            selected_nav_point = null;
            navigation_start = null;
            cur_file = "new_file";
            editor_handle.reloadFileTree();
        }

        private void btn_moveUp_Click(object sender, EventArgs e)
        {
            NavigationPoint nav_point = navigation_start;

            while (nav_point != null)
            {
                    nav_point.scene_y += 5;

                    nav_point = nav_point.next_handle;
            }

            for(int cnt = 0;cnt < gameObject_list.Count;cnt++)
            {
                Editor.GameObject_Scene gameObject = gameObject_list[cnt];

                gameObject.position_scene_y += 5;

                gameObject_list[cnt] = gameObject;
            }

            camY -= 5;
      
        }

        private void btn_moveDown_Click(object sender, EventArgs e)
        {
            NavigationPoint nav_point = navigation_start;


            while (nav_point != null)
            {
                nav_point.scene_y -= 5;

                nav_point = nav_point.next_handle;
            }

            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                Editor.GameObject_Scene gameObject = gameObject_list[cnt];

                gameObject.position_scene_y -= 5;

                gameObject_list[cnt] = gameObject;
            }

            camY += 5;
        }

        private void btn_moveLeft_Click(object sender, EventArgs e)
        {
            NavigationPoint nav_point = navigation_start;


            while (nav_point != null)
            {
                nav_point.scene_x += 5;

                nav_point = nav_point.next_handle;
            }

            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                Editor.GameObject_Scene gameObject = gameObject_list[cnt];

                gameObject.position_scene_x += 5;

                gameObject_list[cnt] = gameObject;
            }

            camX -= 5;
        }

        private void lbl_moveRight_Click(object sender, EventArgs e)
        {
            NavigationPoint nav_point = navigation_start;


            while (nav_point != null)
            {
                nav_point.scene_x -= 5;

                nav_point = nav_point.next_handle;
            }

            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                Editor.GameObject_Scene gameObject = gameObject_list[cnt];

                gameObject.position_scene_x -= 5;

                gameObject_list[cnt] = gameObject;
            }

            camX += 5;
        }
    }

    class NavigationPoint
    {
      public  int position_x = 0;
      public  int position_y = 0;
      public int scene_x = 0;
      public int scene_y = 0;
      public  NavigationPoint next_handle = null;
    }
}
