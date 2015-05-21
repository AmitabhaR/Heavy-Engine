using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Heavy_Engine
{
    public partial class AddObject : Form
    {
        Editor editor_handle;

        public AddObject(Editor editor_handle)
        {
            this.editor_handle = editor_handle;

            InitializeComponent();

            foreach (Editor.GameObject obj in editor_handle.gameObject_list)
            {
                lb_object.Items.Add(obj.object_name);
            }
        }

        private void txt_position_x_TextChanged(object sender, EventArgs e)
        {
            foreach(char ch in txt_position_x.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    txt_position_x.Text = "";
                }
            }
        }

        private void txt_position_y_TextChanged(object sender, EventArgs e)
        {
            foreach (char ch in txt_position_y.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    txt_position_y.Text = "";
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_instance_name.Text == "")
            {
                MessageBox.Show("Instance name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lb_object.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a object!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_depth.Text == "")
            {
                MessageBox.Show("Please provide a depth!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            foreach(Editor.GameObject_Scene obj in editor_handle.gameObjectScene_list)
            {
                if (obj.instance_name == txt_instance_name.Text)
                {
                    MessageBox.Show("The instance is already declared!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }

            Editor.GameObject_Scene game_object = new Editor.GameObject_Scene( );

            game_object.instance_name = txt_instance_name.Text;
            game_object.position_x = int.Parse(txt_position_x.Text);
            game_object.position_y = int.Parse(txt_position_y.Text);
            game_object.depth = int.Parse(txt_depth.Text);
            game_object.position_scene_x = game_object.position_x + editor_handle.cam_x;
            game_object.position_scene_y = game_object.position_y + editor_handle.cam_y;
            game_object.mainObject = editor_handle.gameObject_list[lb_object.SelectedIndex];

            editor_handle.gameObjectScene_list.Add(game_object);

            editor_handle.sortedArray = new  Editor.DrawableGameObject[editor_handle.gameObjectScene_list.Count];

            for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
            {
                editor_handle.sortedArray[cnt].depth = editor_handle.gameObjectScene_list[cnt].depth;
                editor_handle.sortedArray[cnt].index = cnt;
            }

            while (!editor_handle.checkSorted(editor_handle.sortedArray))
            {
               editor_handle.sortElements(editor_handle.sortedArray);
            }

            editor_handle.addGameObject(game_object.instance_name);

            editor_handle.reloadFileTree();

            this.Close();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
