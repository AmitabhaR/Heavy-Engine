using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Heavy_Engine
{
    public partial class BuildOptions : Form
    {
        Editor editor_handle;

        public BuildOptions(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void BuildOptions_Load(object sender, EventArgs e)
        {
            if (editor_handle.platform_id == 1) rb_Windows.Checked = true;
            else if (editor_handle.platform_id == 2) rb_JavaDesktop.Checked = true;
            else if (editor_handle.platform_id == 3) rb_JavaMobile.Checked = true;
            else if (editor_handle.platform_id == 4) rb_WindowsNative.Checked = true;
            else if (editor_handle.platform_id == 5) rb_LinuxNative.Checked = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (rb_Windows.Checked)
            {
                editor_handle.platform_id = 1;
                editor_handle.DisableHeaderImport();
            }
            else if (rb_JavaDesktop.Checked)
            {
                editor_handle.platform_id = 2;
                editor_handle.DisableHeaderImport();
            }
            else if (rb_JavaMobile.Checked)
            {
                editor_handle.platform_id = 3;
                editor_handle.current_level.back_color = Color.FromArgb(0, editor_handle.current_level.back_color.R, editor_handle.current_level.back_color.G, editor_handle.current_level.back_color.B);
                editor_handle.DisableHeaderImport();
            }
            else if (rb_WindowsNative.Checked)
            {
                editor_handle.platform_id = 4;
                editor_handle.EnableHeaderImport();
            }
            else if (rb_LinuxNative.Checked)
            {
                editor_handle.platform_id = 5;
                editor_handle.EnableHeaderImport();
            }

            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
