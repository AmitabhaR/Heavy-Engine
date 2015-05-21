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
    public partial class ProjectManager : Form
    {
        Editor editor_handle;
        public ProjectManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {
            txt_project_name.Text = editor_handle.project_info.project_name;
            txt_project_author.Text = editor_handle.project_info.project_author;
            txt_project_version.Text = editor_handle.project_info.project_version;
            txt_project_about.Text = editor_handle.project_info.project_about;
            txt_project_firstlevel.Text = editor_handle.project_info.project_firstlevel;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            OpenFileDialog file_browser = new OpenFileDialog();

            file_browser.InitialDirectory = editor_handle.project_default_dir + "\\Game-Levels";

            file_browser.ShowDialog();

            if (file_browser.FileName == "")
            {
                return;
            }

            if (Path.GetExtension(file_browser.FileName) == ".hvl" || Path.GetExtension(file_browser.FileName) == "hvl")
            {
                txt_project_firstlevel.Text = file_browser.FileName;
            }
            else
            {
                MessageBox.Show("Cannot allow this type of file!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_project_name.Text == "")
            {
                MessageBox.Show("Project name cannot be empty !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            editor_handle.project_info.project_name = txt_project_name.Text;
            editor_handle.project_info.project_author = txt_project_author.Text;
            editor_handle.project_info.project_version = txt_project_version.Text;
            editor_handle.project_info.project_about = txt_project_about.Text;
            editor_handle.project_info.project_firstlevel = txt_project_firstlevel.Text;

            this.Close();
        }

        private void txt_project_version_TextChanged(object sender, EventArgs e)
        {
            foreach (char ch in txt_project_version.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    if (ch != '.')
                    {
                        txt_project_version.Text = "";
                    }
                }
            }
        }

    }
}
