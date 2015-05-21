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
    public partial class ScriptManager : Form
    {
        Editor editor_handle;
        public ScriptManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void ScriptManager_Load(object sender, EventArgs e)
        {
            foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
            {
                if (Path.GetExtension(path).ToLower() == ".bs" || Path.GetExtension(path).ToLower() == "bs")
                {
                    lb_scripts.Items.Add(path);
                }
                else if (editor_handle.platform_id == 1)
                {
                    if (Path.GetExtension(path) == ".cs" || Path.GetExtension(path) == "cs")
                    {
                        lb_scripts.Items.Add(path);
                    }
                }
                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                {
                    if (Path.GetExtension(path) == ".java" || Path.GetExtension(path) == "java")
                    {
                        lb_scripts.Items.Add(path);
                    }
                }
                else if (editor_handle.platform_id == 4)
                {
                    if (Path.GetExtension(path) == ".cpp" || Path.GetExtension(path) == "cpp" || Path.GetExtension(path) == ".h" || Path.GetExtension(path) == "h")
                    {
                        lb_scripts.Items.Add(path);
                    }
                }
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (lb_scripts.SelectedIndex != -1)
            {
                File.Delete((string) lb_scripts.Items[lb_scripts.SelectedIndex]);
                lb_scripts.Items.RemoveAt(lb_scripts.SelectedIndex);
            //    this.Close();
                editor_handle.reloadFileTree();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
