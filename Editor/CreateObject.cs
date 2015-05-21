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
    public partial class CreateObject : Form
    {
        Editor editor_handle;

        public CreateObject(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void CreateObject_Load(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txt_object_name.Text == "")
            {
                MessageBox.Show("Object name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            for(int cntr = 0;cntr < editor_handle.gameObject_list.Count;cntr++)
            {
                if (editor_handle.gameObject_list[cntr].object_name == txt_object_name.Text)
                {
                    MessageBox.Show("The object is already created !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            StreamWriter stm_wr = new StreamWriter(editor_handle.project_default_dir + "\\Game-Objects\\" + txt_object_name.Text + ".obj");

            stm_wr.WriteLine("Name: @" + txt_object_name.Text);
            stm_wr.WriteLine("Image: @" + "Game-Resouces\\" +  Path.GetFileName(txt_object_img.Text));
            stm_wr.WriteLine("Text: @" + txt_object_text.Text);
            stm_wr.WriteLine("Tag: " + txt_tag.Text);

            stm_wr.WriteLine("Scripts: ");

            for (int cntr = 0; cntr < lb_scripts.Items.Count;cntr++ )
            {
                stm_wr.Write("@Game-Scripts\\" + Path.GetFileName(lb_scripts.Items[cntr].ToString( )) + "@ ");   
            }

            stm_wr.Write(";");
            stm_wr.WriteLine("");

            stm_wr.WriteLine("Static: " + cb_isStatic.Checked);
            stm_wr.WriteLine("Physics: " + cb_physics.Checked);
            stm_wr.WriteLine("RigidBody: " + cb_rigidbody.Checked);
            stm_wr.WriteLine("Collider: " + cb_collider.Checked);

            stm_wr.Flush();
            stm_wr.Close();

            Editor.GameObject newObject;

            newObject.object_name = txt_object_name.Text;
            newObject.object_img = (File.Exists(txt_object_img.Text)) ? Image.FromFile(txt_object_img.Text) : null;
            newObject.path = txt_object_img.Text;
            newObject.scripts = new List<string>();

            for (int cntr = 0; cntr < lb_scripts.Items.Count; cntr++)
            {
                newObject.scripts.Add((string) lb_scripts.Items[cntr]);
            }

            newObject.object_text = txt_object_text.Text;
            newObject.object_tag = int.Parse("0" + txt_tag.Text);
            newObject.isStatic = cb_isStatic.Checked;
            newObject.object_physics = cb_physics.Checked;
            newObject.object_rigid = cb_rigidbody.Checked;
            newObject.object_collider = cb_collider.Checked;

            editor_handle.gameObject_list.Add(newObject);

            editor_handle.reloadFileTree();
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_browser = new OpenFileDialog();

            file_browser.InitialDirectory = editor_handle.project_default_dir + "\\Game-Resources";

            file_browser.ShowDialog();

            if (file_browser.FileName == "")
            {
                return;
            }

            if (Path.GetExtension(file_browser.FileName) == ".png" || Path.GetExtension(file_browser.FileName) == "png")
            {
                txt_object_img.Text = file_browser.FileName;
            }
            else
            {
                MessageBox.Show("Cannot support this type of file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txt_tag_TextChanged(object sender, EventArgs e)
        {
            foreach(char ch in txt_tag.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    txt_tag.Text = "";
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
                if (Path.GetExtension(open_file_dlg.FileName).ToLower( ) == ".bs" || Path.GetExtension(open_file_dlg.FileName).ToLower( ) == "bs")
                {
                    lb_scripts.Items.Add(open_file_dlg.FileName);
                }
                if (editor_handle.platform_id == 1)
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

        

    }
}
