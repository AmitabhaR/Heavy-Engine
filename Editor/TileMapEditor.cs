using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Heavy_Engine
{
    public partial class TileMapEditor : Form
    {
        Editor editor_handle;
        Editor.GameObject baseGameObject;
        List<Editor.GameObject_Scene> gameObject_list;
        int cam_x = 0, cam_y = 0;

        public TileMapEditor(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            editor_handle.Visible = false;
            gameObject_list = new List<Editor.GameObject_Scene>();
            InitializeComponent();
        }

        private void TileMapEditor_Load(object sender, EventArgs e)
        {
            reloadGameObjectList();

            foreach(Editor.GameObject gameObj in editor_handle.gameObject_list)
            {
                cb_object_box.Items.Add(gameObj.object_name);
            }

            this.canvas.Paint += canvas_Paint;
            this.canvas.MouseClick += canvas_MouseClick;
            this.canvas.MouseMove += canvas_MouseMove;
            this.FormClosing += TileMapEditor_FormClosing;
        }

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_mouseX.Text = "Mouse X : " + e.X;
            lbl_mouseY.Text = "Mouse Y : " + e.Y;
        }

        void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (baseGameObject.object_img == null) return;

            if (e.Button == MouseButtons.Left)
            {
                foreach(Editor.GameObject_Scene gameObject in gameObject_list)
                {
                    if (e.X >= gameObject.position_scene_x && e.X <= gameObject.position_scene_x + baseGameObject.object_img.Width && e.Y >= gameObject.position_scene_y && e.Y <= gameObject.position_scene_y + baseGameObject.object_img.Height && gameObject.isTile) return;
                }

                for (int y = 0; y < canvas.Height; y += baseGameObject.object_img.Height)
                {
                    for (int x = 0; x < canvas.Width; x += baseGameObject.object_img.Width)
                    {
                        if (e.X >= x && e.X <= x + baseGameObject.object_img.Width && e.Y >= y && e.Y <= y + baseGameObject.object_img.Height)
                        {
                            // Create GameObject_Scene.  
                            Editor.GameObject_Scene gameObj = new Editor.GameObject_Scene();

                            gameObj.isTile = true;
                            gameObj.mainObject = baseGameObject;
                            gameObj.instance_name = "tile_" + (new Random()).Next(0, 100000000).ToString( ) + "_X_" + (new Random()).Next(0, 100000000).ToString( );
                            gameObj.position_scene_x = x;
                            gameObj.position_scene_y = y;
                            gameObj.position_x = x + cam_x;
                            gameObj.position_y = y + cam_y;
                            gameObj.depth = 10000;

                            gameObject_list.Add(gameObj);
                            return;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (Editor.GameObject_Scene gameObject in gameObject_list)
                {
                    if (e.X >= gameObject.position_scene_x && e.X <= gameObject.position_scene_x + baseGameObject.object_img.Width && e.Y >= gameObject.position_scene_y && e.Y <= gameObject.position_scene_y + baseGameObject.object_img.Height && gameObject.isTile)
                    {
                      /*  for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)     // Delete instance in the main editor.
                        {
                            if (gameObject.instance_name == editor_handle.gameObjectScene_list[cnt].instance_name)
                            {
                                editor_handle.gameObjectScene_list.RemoveAt(cnt);
                                break;
                            }
                        }

                        editor_handle.sortedArray = new Editor.DrawableGameObject[editor_handle.gameObjectScene_list.Count];

                        for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                        {
                            editor_handle.sortedArray[cnt].depth = editor_handle.gameObjectScene_list[cnt].depth;
                            editor_handle.sortedArray[cnt].index = cnt;
                        }

                        while (!editor_handle.checkSorted(editor_handle.sortedArray))
                        {
                            editor_handle.sortElements(editor_handle.sortedArray);
                        } */

                        gameObject_list.Remove(gameObject);
                        return;
                    }
                }
            }
        }

        void TileMapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            editor_handle.Visible = true;
        }

        void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            if (baseGameObject.object_img == null) goto draw_objs;

            for(int y = 0;y < canvas.Height;y += baseGameObject.object_img.Height) // Draw Grid.
            {
                for(int x = 0;x < canvas.Width;x += baseGameObject.object_img.Width)
                {
                    e.Graphics.DrawLine(Pens.White, 0, y, canvas.Width, y);
                    e.Graphics.DrawLine(Pens.White, x, 0, x, canvas.Height);
                }
            }

            draw_objs:

            for (int cntr = 0; cntr < gameObject_list.Count; cntr++)
            {
                if (gameObject_list[cntr].mainObject.object_img != null)
                {
                    e.Graphics.DrawImage(new Bitmap(gameObject_list[cntr].mainObject.object_img, new Size(gameObject_list[cntr].mainObject.object_img.Width, gameObject_list[cntr].mainObject.object_img.Height)), new Point(gameObject_list[cntr].position_scene_x, gameObject_list[cntr].position_scene_y));
                }
                else if (gameObject_list[cntr].mainObject.object_text != "")
                {
                    e.Graphics.DrawString(gameObject_list[cntr].mainObject.object_text, new Font("Verdana", 12), Brushes.White, new Point(gameObject_list[cntr].position_scene_x, gameObject_list[cntr].position_scene_y));
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmr_draw_Tick(object sender, EventArgs e)
        {
            lbl_cameraX.Text = "Camera X : " + cam_x;
            lbl_cameraY.Text = "Camera Y : " + cam_y;

            canvas.Refresh();
        }

        private void reloadGameObjectList( )
        {
            gameObject_list.Clear();

            foreach (Editor.GameObject_Scene gameObject in editor_handle.gameObjectScene_list)
            {
                Editor.GameObject_Scene cp = gameObject;

                cp.position_scene_x = cp.position_x;
                cp.position_scene_y = cp.position_y;

                gameObject_list.Add(cp);
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                if (gameObject_list[cnt].mainObject.object_text == "")
                {
                    Editor.GameObject_Scene handle = gameObject_list[cnt];

                    handle.position_scene_y += (baseGameObject.object_img != null) ? baseGameObject.object_img.Height : 5;

                    gameObject_list[cnt] = handle;
                }
            }

            cam_y -= 5;
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                if (gameObject_list[cnt].mainObject.object_text == "")
                {
                    Editor.GameObject_Scene handle = gameObject_list[cnt];

                    handle.position_scene_y -= (baseGameObject.object_img != null) ? baseGameObject.object_img.Height : 5;

                    gameObject_list[cnt] = handle;
                }
            }

            cam_y += 5;
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                if (gameObject_list[cnt].mainObject.object_text == "")
                {
                    Editor.GameObject_Scene handle = gameObject_list[cnt];

                    handle.position_scene_x += (baseGameObject.object_img != null) ? baseGameObject.object_img.Width : 5;

                    gameObject_list[cnt] = handle;
                }
            }

            cam_x -= 5;
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            for (int cnt = 0; cnt < gameObject_list.Count; cnt++)
            {
                if (gameObject_list[cnt].mainObject.object_text == "")
                {
                    Editor.GameObject_Scene handle = gameObject_list[cnt];

                    handle.position_scene_x -= (baseGameObject.object_img != null) ? baseGameObject.object_img.Width : 5;

                    gameObject_list[cnt] = handle;
                }
            }

            cam_x += 5;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            foreach(Editor.GameObject_Scene gameObject in gameObject_list)
            {
                bool isSuccess = true;
               
                for (int cnt = 0 ; cnt < editor_handle.gameObjectScene_list.Count;cnt++ )
                {
                    if (gameObject.instance_name == editor_handle.gameObjectScene_list[cnt].instance_name && gameObject.isTile && editor_handle.gameObjectScene_list[cnt].isTile)
                    {
                        isSuccess = false;
                        break;
                    }
                }

                if (isSuccess && gameObject.isTile)
                {
                    gameObject.Initialize();
                    editor_handle.gameObjectScene_list.Add(gameObject);
                    editor_handle.addGameObject(gameObject.instance_name);
                }
            }

            for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
            {
                bool isSuccess = true;

                foreach(Editor.GameObject_Scene gameObject in gameObject_list)
                { 
                    if (gameObject.instance_name == editor_handle.gameObjectScene_list[cnt].instance_name && gameObject.isTile && editor_handle.gameObjectScene_list[cnt].isTile)
                    {
                        isSuccess = false;
                        break;
                    }
                }

                if (isSuccess && editor_handle.gameObjectScene_list[cnt].isTile)
                {
                    editor_handle.gameObjectScene_list.RemoveAt(cnt);
                    cnt--;
                }
            }

          /*  editor_handle.sortedArray = new Editor.DrawableGameObject[editor_handle.gameObjectScene_list.Count];

            for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
            {
                editor_handle.sortedArray[cnt].depth = editor_handle.gameObjectScene_list[cnt].depth;
                editor_handle.sortedArray[cnt].index = cnt;
            }

            while (!editor_handle.checkSorted(editor_handle.sortedArray))
            {
               editor_handle.sortElements(editor_handle.sortedArray);
            } */

            editor_handle.sortGameObjects();
        }

        private void cb_object_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_object_box.SelectedIndex != -1)
            {
                baseGameObject = editor_handle.gameObject_list[cb_object_box.SelectedIndex];
                txt_tile_width.Text = (baseGameObject.object_img != null) ? baseGameObject.object_img.Width.ToString( ) : "0";
                txt_tile_height.Text = (baseGameObject.object_img != null) ? baseGameObject.object_img.Height.ToString() : "0";
            }
        }
    }
}
