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
    public partial class LevelManager : Form
    {
        Editor editor_handle;
        public LevelManager(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void LevelManager_Load(object sender, EventArgs e)
        {
            txt_level_name.Text = editor_handle.current_level.level_name;
            txt_level_speed.Text = editor_handle.current_level.level_speed.ToString();
            tb_colorA.Value = editor_handle.current_level.back_color.A;
            tb_colorR.Value = editor_handle.current_level.back_color.R;
            tb_colorG.Value = editor_handle.current_level.back_color.G;
            tb_colorB.Value = editor_handle.current_level.back_color.B;
            
            if (editor_handle.platform_id == 3)
            {
                lbl_color_A.Enabled = false;
                tb_colorA.Enabled = false;
            }

            this.pb_img_sample.BackColor = Color.FromArgb(tb_colorA.Value, tb_colorR.Value, tb_colorG.Value, tb_colorB.Value);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_level_name.Text == "")
            {
                MessageBox.Show("Level name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            editor_handle.current_level.level_name = txt_level_name.Text;
            editor_handle.current_level.level_speed = int.Parse(txt_level_speed.Text);
            if (editor_handle.platform_id != 3) editor_handle.current_level.back_color = Color.FromArgb(tb_colorA.Value, 0, 0, 0); else editor_handle.current_level.back_color = Color.FromArgb(0, 0, 0, 0);
            editor_handle.current_level.back_color = Color.FromArgb(editor_handle.current_level.back_color.A, tb_colorR.Value, 0, 0);
            editor_handle.current_level.back_color = Color.FromArgb(editor_handle.current_level.back_color.A, editor_handle.current_level.back_color.R, tb_colorG.Value,0);
            editor_handle.current_level.back_color = Color.FromArgb(editor_handle.current_level.back_color.A, editor_handle.current_level.back_color.R, editor_handle.current_level.back_color.G, tb_colorB.Value);

       
            this.Close();
        }

        private void tb_color_Scroll(object sender, EventArgs e)
        {
            this.pb_img_sample.BackColor = Color.FromArgb(tb_colorA.Value, tb_colorR.Value, tb_colorG.Value, tb_colorB.Value);
        }
    }
}
