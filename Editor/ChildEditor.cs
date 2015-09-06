using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heavy_Engine
{
    public partial class ChildEditor : Form
    {
        Editor editor_handle;

        public ChildEditor(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent(); 
        }

        private void ChildEditor_Load(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObj = null;

            if ((gameObj = editor_handle.GetActiveGameObject()) == null) { MessageBox.Show("No selected object found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close( ); }

            cb_objects.Items.Clear();

            foreach(Editor.GameObject_Scene gameObject in editor_handle.gameObjectScene_list)
            {
                if (gameObject.instance_name != gameObj._obj.instance_name) cb_objects.Items.Add(gameObject.instance_name);
            }

            lb_childs.Items.Clear();

            foreach(Editor.GameObject_Scene gameObject in gameObj.obj.child_list)
            {
               if (gameObject.instance_name != gameObj._obj.instance_name) lb_childs.Items.Add(gameObject.instance_name);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_add_child_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObject = null;

            if ((gameObject = editor_handle.GetActiveGameObject()) == null) { MessageBox.Show("No selected object found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }

            if (cb_objects.SelectedIndex != -1)
            {
                bool isSuccess = false;

                foreach(Editor.GameObject_Scene gameObj in editor_handle.gameObjectScene_list)
                {
                   if (gameObj.instance_name == gameObject._obj.instance_name)
                   {
                       isSuccess = true;
                       break;
                   }
                }

                if (!isSuccess)
                {
                    MessageBox.Show("The GameObject is deleted from the scene.");
                    ChildEditor_Load(null, null); // Reload our list.
                }
                else
                {
                    isSuccess = false;

                    for(int cnt = 0;cnt < editor_handle.gameObjectScene_list.Count;cnt++)
                    {
                        Editor.GameObject_Scene obj = editor_handle.gameObjectScene_list[cnt];

                        if (addChild(ref gameObject.obj,ref obj, (string) cb_objects.Items[cb_objects.SelectedIndex]))
                        {
                            editor_handle.gameObjectScene_list[cnt] = obj;
                            ChildEditor_Load(null, null);
                            isSuccess = true;
                            break;
                        }
                    }

                    if (!isSuccess)
                    {
                        if (!addChild(ref gameObject.obj,ref gameObject.obj,(string) cb_objects.Items[cb_objects.SelectedIndex]))
                        {
                            for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                            {
                                if (editor_handle.gameObjectScene_list[cnt].instance_name == (string) cb_objects.Items[cb_objects.SelectedIndex])
                                {
                                    gameObject.obj.child_list.Add(editor_handle.gameObjectScene_list[cnt]);
                                    ChildEditor_Load(null, null);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool addChild(ref Editor.GameObject_Scene gameObj_selected,ref Editor.GameObject_Scene gameObject ,string child_name)
        {
            for (int cnt = 0; cnt < gameObject.child_list.Count;cnt++ )
            {
                if (gameObject.child_list[cnt].instance_name == child_name)
                {
                    if (MessageBox.Show(child_name + " already belongs to " + gameObject.instance_name + ". You want to add this child to " + gameObject.instance_name + "?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        gameObj_selected.child_list.Add(gameObject.child_list[cnt]);
                        gameObject.child_list.Remove(gameObject.child_list[cnt]);
                    }
                    else return false;

                    return true;
                }
            }

            for (int cnt = 0; cnt < gameObject.child_list.Count;cnt++ )
            {
                Editor.GameObject_Scene obj = gameObject.child_list[cnt];

                if (addChild(ref gameObj_selected,ref obj, child_name))
                {
                    gameObject.child_list[cnt] = obj;
                    return true;
                }
            }

            return false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObj = null;

            if ((gameObj = editor_handle.GetActiveGameObject()) == null) { MessageBox.Show("No selected object found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close( ); }
            if (lb_childs.SelectedIndex < 0) return;

            for(int cnt = 0;cnt < gameObj.obj.child_list.Count;cnt++ )
            {
                if (gameObj.obj.child_list[cnt].instance_name == (string) lb_childs.Items[lb_childs.SelectedIndex])
                {
                    gameObj.obj.child_list.RemoveAt(cnt);
                    ChildEditor_Load(null, null); // Reload our lists.
                    return;
                }
            }
        }
    }
}
