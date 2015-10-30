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
    public partial class LoadProjectScreen : Form
    {
        Form menu_handle,editor_handle;
        public LoadProjectScreen(Form menu_handle,Form editor_handle)
        {
            this.menu_handle = menu_handle;
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void LoadProjectScreen_Load(object sender, EventArgs e)
        {

        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_browser = new FolderBrowserDialog();

            folder_browser.ShowDialog();

            txt_path.Text = folder_browser.SelectedPath;
            txt_project_name.Text = txt_path.Text.Substring(txt_path.Text.LastIndexOf('\\') + 1,txt_path.Text.Length - (txt_path.Text.LastIndexOf('\\') + 1));
        }

        public void btn_load_Click(object sender, EventArgs e)
        {
            bool succes = false;

            if (!Directory.Exists(txt_path.Text))
            {
                MessageBox.Show("Path is not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach(string path in Directory.GetFiles(txt_path.Text))
            {
                if (Path.GetExtension(path) == ".prj" || Path.GetExtension(path) == "prj")
                {
                    succes = true;
                    break;
                }
            }

            if (succes)
            {
                 if (menu_handle != null && editor_handle == null)
                 {
                     menu_handle.Visible = false;

                     editor_handle = new Editor(menu_handle, txt_path.Text, txt_project_name.Text);
                     editor_handle.Show();

                     this.Close();
                 }
                 else
                 {
                     editor_handle.Close();

                     editor_handle = new Editor(menu_handle, txt_path.Text, txt_project_name.Text);
                     editor_handle.Show();

                     this.Close();
                 }
            }
            else
            {
                MessageBox.Show("The folder dosen't contain any project file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (menu_handle != null && editor_handle == null)
            {
                menu_handle.Visible = true;
            }
            else
            {
                Editor editor = (Editor)editor_handle;

                editor.canClose = true;
            }

            this.Close();
        }
    }
}
