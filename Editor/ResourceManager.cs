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
    public partial class ResourceManager : Form
    {
        Editor editor_handle;
        public ResourceManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResourceManager_Load(object sender, EventArgs e)
        {
            foreach(string resource_path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces"))
            {
                lb_resources.Items.Add(resource_path);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (lb_resources.SelectedIndex != -1)
            {
                if (File.Exists((string) lb_resources.Items[lb_resources.SelectedIndex]))
                {
                    File.Delete((string)lb_resources.Items[lb_resources.SelectedIndex]);
                    lb_resources.Items.RemoveAt(lb_resources.SelectedIndex);
                }
                else
                {
                    MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lb_resources.Items.RemoveAt(lb_resources.SelectedIndex);
                }

                editor_handle.reloadFileTree();
            }
        }
    }
}
