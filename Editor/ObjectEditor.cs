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
    public partial class ObjectEditor : Form
    {
        Editor editor_handle;
        string sel_name = "";
        string open_filter = "";

        public ObjectEditor(Editor editor_handle , string open_filter)
        {
            this.editor_handle = editor_handle;
            this.open_filter = open_filter;
            InitializeComponent();
        }

        private void ObjectEditor_Load(object sender, EventArgs e)
        {
            foreach (Editor.GameObject gameObject in editor_handle.gameObject_list)
            {
                if (open_filter == gameObject.object_name)
                {
                    loadObjectProperties(gameObject);
                }

                lb_objects.Items.Add(gameObject.object_name);
            }
        }

        private void lb_objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_objects.SelectedIndex != -1)
            {
                foreach (Editor.GameObject gameObject in editor_handle.gameObject_list)
                {
                        if (lb_objects.Items[lb_objects.SelectedIndex] == gameObject.object_name)
                        {
                            loadObjectProperties( gameObject );
                            return;
                        }
                } 
            }
        }

        private void loadObjectProperties(Editor.GameObject handle)
        {
            txt_name.Text = handle.object_name;
            lb_scripts.Items.Clear();
            txt_image.Text = "";

            if (handle.object_img != null)
            {
                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
                {
                    if (Path.GetExtension(path) == ".png" || Path.GetExtension(path) == "png")
                    {
                        Bitmap hnd = new Bitmap( Image.FromFile(path) );
                        Bitmap obj_img = new Bitmap(handle.object_img);

                        if (hnd.Width == obj_img.Width && hnd.Height == obj_img.Height)
                        {

                            for (int y = 0; y < hnd.Height; y++)
                            {
                                for (int x = 0; x < hnd.Width; x++)
                                {
                                    if (hnd.GetPixel(x, y) != obj_img.GetPixel(x, y))
                                    {
                                        goto image_not_matched;
                                    }
                                }
                            }

                            txt_image.Text = path;
                        }
                    }
                image_not_matched: ;
                }
            }


            txt_text.Text = handle.object_text;
            txt_tag.Text = handle.object_tag.ToString( );
            cb_static.Checked = handle.isStatic;
            cb_rigid_body.Checked = handle.object_rigid;
            cb_physics.Checked = handle.object_physics;
            cb_collider.Checked = handle.object_collider;
            
            foreach (string path in handle.scripts)
            {
                lb_scripts.Items.Add(path);
            }

            sel_name = txt_name.Text;
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fld_dialog = new OpenFileDialog();

            fld_dialog.InitialDirectory = editor_handle.project_default_dir + "\\Game-Resouces";

            fld_dialog.ShowDialog();

            if(fld_dialog.FileName != "")
            {
                if (Path.GetExtension(fld_dialog.FileName) == ".png" || Path.GetExtension(fld_dialog.FileName) == "png")
                {
                    if (Path.GetDirectoryName(fld_dialog.FileName) != editor_handle.project_default_dir + "\\Game-Resouces") // Auto resource import.
                    {
                        File.Copy(fld_dialog.FileName, editor_handle.project_default_dir + "\\Game-Resouces\\" + Path.GetFileName(fld_dialog.FileName));
                       
                        txt_image.Text = editor_handle.project_default_dir + "\\Game-Resouces\\" + Path.GetFileName(fld_dialog.FileName);
                    }
                    else
                    {
                        txt_image.Text = fld_dialog.FileName;
                    }
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.InitialDirectory = this.editor_handle.project_default_dir + "\\Game-Scripts";
            open_file_dlg.ShowDialog();

            if (open_file_dlg.FileName != "")
            {
                foreach (string path in lb_scripts.Items)
                {
                    if (Path.GetFileNameWithoutExtension(path).ToLower() == Path.GetFileNameWithoutExtension(open_file_dlg.FileName).ToLower())
                    {
                        MessageBox.Show("Script already exists!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                }

                if (Path.GetExtension(open_file_dlg.FileName).ToLower() == ".bs" || Path.GetExtension(open_file_dlg.FileName).ToLower( ) == "bs")
                {
                    lb_scripts.Items.Add(open_file_dlg.FileName);
                }
                else if (editor_handle.platform_id == 1)
                {
                    if (Path.GetExtension(open_file_dlg.FileName).ToLower() == ".cs" || Path.GetExtension(open_file_dlg.FileName).ToLower() == "cs")
                    {
                        lb_scripts.Items.Add(open_file_dlg.FileName);
                    }
                }
                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                {
                    if (Path.GetExtension(open_file_dlg.FileName).ToLower() == ".java" || Path.GetExtension(open_file_dlg.FileName).ToLower() == "java")
                    {
                        lb_scripts.Items.Add(open_file_dlg.FileName);
                    }
                }
                else if (editor_handle.platform_id == 4)
                {
                    if (Path.GetExtension(open_file_dlg.FileName).ToLower() == ".cpp" || Path.GetExtension(open_file_dlg.FileName).ToLower() == "cpp")
                    {
                        lb_scripts.Items.Add(open_file_dlg.FileName);
                    }
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lb_scripts.SelectedIndex != -1)
            {
                lb_scripts.Items.RemoveAt(lb_scripts.SelectedIndex);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                MessageBox.Show("Object name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StreamWriter stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\Game-Objects\\" + txt_name.Text + ".obj");

            stm_wr.WriteLine("Name: @" + txt_name.Text);
            stm_wr.WriteLine("Image: @" + "Game-Resouces\\" + Path.GetFileName(txt_image.Text));
            stm_wr.WriteLine("Text: @" + txt_text.Text);
            stm_wr.WriteLine("Tag: " + txt_tag.Text);

            stm_wr.WriteLine("Scripts: ");

            for (int cntr = 0; cntr < lb_scripts.Items.Count; cntr++)
            {
                stm_wr.Write("@Game-Scripts\\" + Path.GetFileName(lb_scripts.Items[cntr].ToString()) + "@ ");
            }

            stm_wr.Write(";");
            stm_wr.WriteLine("");

            stm_wr.WriteLine("Static: " + cb_static.Checked);
            stm_wr.WriteLine("Physics: " + cb_physics.Checked);
            stm_wr.WriteLine("RigidBody: " + cb_rigid_body.Checked);
            stm_wr.WriteLine("Collider: " + cb_collider.Checked);

            stm_wr.Flush();
            stm_wr.Close();

            Editor.GameObject newObject;

            newObject.object_name = txt_name.Text;
            newObject.object_img = (File.Exists(txt_image.Text)) ? Image.FromFile(txt_image.Text) : null;
            newObject.path = txt_image.Text;
            newObject.scripts = new List<string>();

            for (int cntr = 0; cntr < lb_scripts.Items.Count; cntr++)
            {
                newObject.scripts.Add((string)lb_scripts.Items[cntr]);
            }

            newObject.object_text = txt_text.Text;
            newObject.object_tag = int.Parse("0" + txt_tag.Text);
            newObject.isStatic = cb_static.Checked;
            newObject.object_physics = cb_physics.Checked;
            newObject.object_rigid = cb_rigid_body.Checked;
            newObject.object_collider = cb_collider.Checked;

            for(int cnt = 0; cnt < editor_handle.gameObject_list.Count; cnt++)
            {
                if (editor_handle.gameObject_list[cnt].object_name == sel_name)
                {
                    for (int cntr = 0; cntr < editor_handle.gameObjectScene_list.Count; cntr++)
                    {
                        if (editor_handle.gameObjectScene_list[cntr].mainObject.object_name == sel_name)
                        {
                            Editor.GameObject_Scene handle = editor_handle.gameObjectScene_list[cntr];

                            handle.mainObject = newObject;

                            editor_handle.gameObjectScene_list[cntr] = handle;
                        }
                    }

                    editor_handle.gameObject_list[cnt] = newObject;
                    break;
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
