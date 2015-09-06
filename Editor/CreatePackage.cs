using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Heavy_Engine
{
    public partial class CreatePackage : Form
    {
        Editor editor_handle;

        public CreatePackage(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void CreatePackage_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Objects")) if (Path.GetExtension(file) == ".obj" || Path.GetExtension(file) == "obj") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels")) if (Path.GetExtension(file) == ".hvl" || Path.GetExtension(file) == "hvl") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Animation")) if (Path.GetExtension(file) == ".anim" || Path.GetExtension(file) == "anim") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Navigation")) if (Path.GetExtension(file) == ".nav" || Path.GetExtension(file) == "nav") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts")) if (Path.GetExtension(file) == ".cs" || Path.GetExtension(file) == "cs" || Path.GetExtension(file) == ".java" || Path.GetExtension(file) == "java" || Path.GetExtension(file) == ".cpp" || Path.GetExtension(file) == "cpp" || Path.GetExtension(file) == ".bs" || Path.GetExtension(file) == "bs") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins")) if (Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == "dll" || Path.GetExtension(file) == ".java" || Path.GetExtension(file) == "java" || Path.GetExtension(file) == ".cpp" || Path.GetExtension(file) == "cpp") clb_packing_files.Items.Add(Path.GetFileName(file));
            foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces")) clb_packing_files.Items.Add(Path.GetFileName(file));
        }

        private void btn_create_pacakge_Click(object sender, EventArgs e)
        {
            if (txt_package_name.Text == "") { MessageBox.Show("Please enter a package name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            PackageManager.PackageManager pak_manager = new PackageManager.PackageManager(Application.StartupPath, editor_handle.project_default_dir);
            List<string> files = new List<string>();

            for(int cnt = 0;cnt < clb_packing_files.CheckedItems.Count;cnt++)
            {
                bool isSuccess = false;

                foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Objects")) if (Path.GetExtension(file) == ".obj" || Path.GetExtension(file) == "obj") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; }
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Levels")) if (Path.GetExtension(file) == ".hvl" || Path.GetExtension(file) == "hvl") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; } } else continue;
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Animation")) if (Path.GetExtension(file) == ".anim" || Path.GetExtension(file) == "anim") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; } } else continue;
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Navigation")) if (Path.GetExtension(file) == ".nav" || Path.GetExtension(file) == "nav") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; } } else continue;
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts")) if (Path.GetExtension(file) == ".cs" || Path.GetExtension(file) == "cs" || Path.GetExtension(file) == ".java" || Path.GetExtension(file) == "java" || Path.GetExtension(file) == ".cpp" || Path.GetExtension(file) == "cpp" || Path.GetExtension(file) == ".bs" || Path.GetExtension(file) == "bs") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; } } else continue;
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Plugins")) if (Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == "dll" || Path.GetExtension(file) == ".java" || Path.GetExtension(file) == "java" || Path.GetExtension(file) == ".cpp" || Path.GetExtension(file) == "cpp") if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); isSuccess = true; break; } } else continue;
                if (!isSuccess) { foreach (string file in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Resouces")) if (Path.GetFileName(file) == (string)clb_packing_files.CheckedItems[cnt]) { files.Add(file); break; } } 
            }

            if (pak_manager.CreatePackage(txt_package_name.Text, files) == null) MessageBox.Show("Package Creation Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); else this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
