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
    public partial class MenuScreen : Form
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void CheckFiles()
        {
            if (!File.Exists(Application.StartupPath + "\\bc.exe"))
            {
                MessageBox.Show("Bos Compiler not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\cldc_1.1.jar")) 
            {
                MessageBox.Show("Java Mobile Library (cldc_1.0.jar) not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\midp_2.1.jar"))
            {
                MessageBox.Show("Java Mobile Library (midp_2.0.jar) not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\Runtime.dll"))
            {
                MessageBox.Show("Runtime.dll not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\JRuntime.jar"))
            {
                MessageBox.Show("JRuntime.jar not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\JMRuntime.jar"))
            {
                MessageBox.Show("JMRuntime.jar not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\PackageManager.HeavyEngine.dll"))
            {
                MessageBox.Show("PackageManager.dll not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\jl1.0.1.jar"))
            {
                MessageBox.Show("jl1.0.1.jar not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\thumbnailator-0.4.8.jar"))
            {
                MessageBox.Show("thumbnailator-0.4.8.jar not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!File.Exists(Application.StartupPath + "\\JVLIB.dll"))
            {
                MessageBox.Show("JVLIB.dll not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            if (!Directory.Exists(Application.StartupPath + "\\header_includes"))
            {
                MessageBox.Show("Header files directory not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!Directory.Exists(Application.StartupPath + "\\lib"))
            {
                MessageBox.Show("Libraries files directory not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            if (!Directory.Exists(Application.StartupPath + "\\Runtime"))
            {
                MessageBox.Show("Runtime files directory not found in the home directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {
            CheckFiles();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_loadproject_Click(object sender, EventArgs e)
        {
            Form loadproject_window = new LoadProjectScreen(this, null);
            loadproject_window.Show();
        }

        private void btn_newproject_Click(object sender, EventArgs e)
        {
            Form newproject_window = new NewProjectScreen(this);
            newproject_window.Show();
        }
    }
}
