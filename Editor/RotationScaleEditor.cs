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
    public partial class RotationScaleEditor : Form
    {
        Editor editor_handle;

        public RotationScaleEditor(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void lbl_scale_Click(object sender, EventArgs e)
        {

        }

        private void RotationScaleEditor_Load(object sender, EventArgs e)
        {

        }

        private void btn_scale_positive_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObject = editor_handle.GetActiveGameObject();

            if (gameObject != null)
            {
                gameObject.obj.scaling_rate += 1f;
                gameObject.obj.UpdateScale(1f, true);

                for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                {
                    Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cnt];

                    if (editor_handle.gameObjectScene_list[cnt].instance_name == gameObject._obj.instance_name) 
                    { 
                        gameObj.scaling_rate += 1f; 
                        editor_handle.gameObjectScene_list[cnt] = gameObj;
                        editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);

                        break;
                    }
                }
            }
        }

        private void btn_rotate_clockwise_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObject = editor_handle.GetActiveGameObject();

            if (gameObject != null) 
            {
                gameObject.obj.Rotate(-1f);
                gameObject.obj.UpdateRotation(-1f, gameObject.obj.position_scene_x + gameObject.obj.mainObject.object_img.Width / 2, gameObject.obj.position_scene_y + gameObject.obj.mainObject.object_img.Height / 2, true);

                for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                {
                    Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cnt];

                    if (editor_handle.gameObjectScene_list[cnt].instance_name == gameObject._obj.instance_name) 
                    { 
                        gameObj.Rotate(-1f); 
                        editor_handle.gameObjectScene_list[cnt] = gameObj;
                        editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);

                        break;
                    }
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_rotate_anticlockwise_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObject = editor_handle.GetActiveGameObject( );

            if (gameObject != null) 
            {
                gameObject.obj.Rotate(1f);
                gameObject.obj.UpdateRotation(1f,gameObject.obj.position_scene_x + gameObject.obj.mainObject.object_img.Width / 2,gameObject.obj.position_scene_y + gameObject.obj.mainObject.object_img.Height / 2, true);

                for(int cnt = 0;cnt < editor_handle.gameObjectScene_list.Count;cnt++)
                {
                    Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cnt];

                    if (editor_handle.gameObjectScene_list[cnt].instance_name == gameObject._obj.instance_name) 
                    { 
                        gameObj.Rotate(1f);
                        editor_handle.gameObjectScene_list[cnt] = gameObj;
                        editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);

                        break; 
                    }
                }
            }
        }

        private void btn_scale_negative_Click(object sender, EventArgs e)
        {
            GameObject_Scene_EDITOR gameObject = editor_handle.GetActiveGameObject();

            if (gameObject != null)
            {
                if (gameObject.obj.scaling_rate > 0)
                {
                    gameObject.obj.scaling_rate -= 1f;
                    gameObject.obj.UpdateScale(1f, true);

                    for (int cnt = 0; cnt < editor_handle.gameObjectScene_list.Count; cnt++)
                    {
                        Editor.GameObject_Scene gameObj = editor_handle.gameObjectScene_list[cnt];

                        if (editor_handle.gameObjectScene_list[cnt].instance_name == gameObject._obj.instance_name) 
                        { 
                            gameObj.scaling_rate -= 1f; 
                            editor_handle.gameObjectScene_list[cnt] = gameObj;
                            editor_handle.UpdateWorldChilds(editor_handle.gameObjectScene_list[cnt]);

                            break;
                        }
                    }
                }
            }
        }

    }
}
