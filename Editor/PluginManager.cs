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
    public partial class PluginManager : Form
    {
        Editor editor_handle;
        public PluginManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void PluginManager_Load(object sender, EventArgs e)
        {
            foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins"))
            {
                lb_plugins.Items.Add(path);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lb_plugins.SelectedIndex != -1)
            {
                if (File.Exists((string) lb_plugins.Items[lb_plugins.SelectedIndex]))
                {
                    File.Delete((string) lb_plugins.Items[lb_plugins.SelectedIndex]);
                }

                lb_plugins.Items.RemoveAt(lb_plugins.SelectedIndex);
                editor_handle.reloadFileTree();
            }
        }
    }
}
