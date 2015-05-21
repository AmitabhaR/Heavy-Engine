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
    public partial class NewProjectScreen : Form
    {
        Form menu_view;
        public NewProjectScreen(Form menu_view)
        {
            this.menu_view = menu_view;
            InitializeComponent();
        }

        private void btn_create_project_Click(object sender, EventArgs e)
        {
            if (txt_project_name.Text == "")
            {
                MessageBox.Show("Project name cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txt_project_path.Text == "")
            {
                MessageBox.Show("Project creation directory cannot be empty!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          // if (Directory.Exists(txt_project_path.Text))
          // {
               string project_dir = txt_project_path.Text + txt_project_name.Text;
               // Create Nessacery Files
               Directory.CreateDirectory(project_dir);
               Directory.CreateDirectory(project_dir + "\\Game-Levels");
               Directory.CreateDirectory(project_dir + "\\Game-Resouces");
               Directory.CreateDirectory(project_dir + "\\Game-Objects");
               Directory.CreateDirectory(project_dir + "\\Game-Scripts");
               Directory.CreateDirectory(project_dir + "\\Game-Plugins");
               Directory.CreateDirectory(project_dir + "\\Game-Data"); // Extra
               Directory.CreateDirectory(project_dir + "\\Game-Navigation");
               Directory.CreateDirectory(project_dir + "\\Game-Animation");

               StreamWriter stm_wr = File.CreateText(project_dir + "\\" + txt_project_name.Text + ".prj");

               stm_wr.WriteLine("Project_Name: @" + txt_project_name.Text);
               stm_wr.WriteLine("Project_Author: @");
               stm_wr.WriteLine("Project_Version: @");
               stm_wr.WriteLine("Project_About: @");
               stm_wr.WriteLine("Project_FirstLevel: Game-Levels\\Demo.hvl");

               stm_wr.Close();

               // Write data to the first level.
               stm_wr = File.CreateText(project_dir + "\\Game-Levels\\Demo.hvl");

               stm_wr.WriteLine("Level_Name: @Demo");
               stm_wr.WriteLine("Speed: 0");
               stm_wr.WriteLine("Back_Colour: 0 0 0 0");

               stm_wr.Close( );


               menu_view.Visible = false;
               Form editor_window = new Editor(menu_view, project_dir,txt_project_name.Text);

               editor_window.Show();
               // Open the editor window.
               this.Close();
               // Save current instance of menu-screen.
       /*    }
           else
           {
               MessageBox.Show("Project create directory not found!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }*/
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_change_dir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_browser = new FolderBrowserDialog();

            folder_browser.ShowDialog();

            txt_project_path.Text = folder_browser.SelectedPath;
            txt_project_path.Text += "\\";

        }
    }
}
