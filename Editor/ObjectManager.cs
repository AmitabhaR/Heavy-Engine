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
    public partial class ObjectManager : Form
    {
        Editor editor_handle;
        public ObjectManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void ObjectManager_Load(object sender, EventArgs e)
        {
            lb_objects.Items.Clear();
            lb_scene_objects.Items.Clear();

            for (int cntr = 0; cntr < editor_handle.gameObject_list.Count; cntr++)
            {
                lb_objects.Items.Add(editor_handle.gameObject_list[cntr].object_name);
            }

            for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
            {
                lb_scene_objects.Items.Add(editor_handle.gameObjectScene_list[cntr].instance_name);
            }
        }


        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lb_objects.SelectedIndex != -1)
            {
                File.Delete(editor_handle.project_default_dir + "\\Game-Objects\\" + editor_handle.gameObject_list[lb_objects.SelectedIndex].object_name + ".obj");

            jmp:

                for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                {
                    if (editor_handle.gameObjectScene_list[cntr].mainObject.object_name == editor_handle.gameObject_list[lb_objects.SelectedIndex].object_name)
                    {
                        editor_handle.onGameObjectDeleted(editor_handle.gameObjectScene_list[cntr].instance_name);
                        editor_handle.gameObjectScene_list.RemoveAt(cntr);
                        goto jmp;
                    }
                }

                editor_handle.gameObject_list.RemoveAt(lb_objects.SelectedIndex);

                lb_scene_objects.Items.Clear();
                lb_objects.Items.Clear();

                for (int cntr = 0; cntr < editor_handle.gameObject_list.Count; cntr++)
                {
                    lb_objects.Items.Add(editor_handle.gameObject_list[cntr].object_name);
                }

                for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                {
                    lb_scene_objects.Items.Add(editor_handle.gameObjectScene_list[cntr].instance_name);
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

                editor_handle.reloadFileTree();
            }

            editor_handle.SaveLevel();
        }

        private void btn_sremove_Click(object sender, EventArgs e)
        {
            if (lb_scene_objects.SelectedIndex != -1)
            {
                editor_handle.onGameObjectDeleted(editor_handle.gameObjectScene_list[lb_scene_objects.SelectedIndex].instance_name);

                editor_handle.gameObjectScene_list.RemoveAt(lb_scene_objects.SelectedIndex);

                lb_scene_objects.Items.Clear();

                for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                {
                    lb_scene_objects.Items.Add(editor_handle.gameObjectScene_list[cntr].instance_name);
                }

             /*   editor_handle.sortedArray = new Editor.DrawableGameObject[editor_handle.gameObjectScene_list.Count];

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

                editor_handle.reloadFileTree();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ObjectManager_Load(null, null);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
