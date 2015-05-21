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
    public partial class SpriteBaker : Form
    {
        Editor editor_handle;

        public SpriteBaker(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void SpriteBaker_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Bake_Click(object sender, EventArgs e)
        {
            if (File.Exists(txt_image.Text))
            {
                Bitmap baseImage = new Bitmap(Image.FromFile(txt_image.Text));
                int current_part = 1;

                for(int y = 0;y < baseImage.Height && y + int.Parse(txt_height.Text) < baseImage.Height;y += int.Parse(txt_height.Text) )
                {
                    for (int x = 0; x < baseImage.Width && x + int.Parse(txt_width.Text) < baseImage.Width; x += int.Parse(txt_width.Text))
                    {
                        Bitmap img_frame = new Bitmap(int.Parse(txt_width.Text), int.Parse(txt_height.Text));

                        for (int _y = 0; _y < int.Parse(txt_height.Text);_y++ )
                        {
                            for(int _x = 0; _x < int.Parse(txt_width.Text);_x++)
                            {
                                img_frame.SetPixel(_x, _y, baseImage.GetPixel(x + _x , y + _y));
                            }
                        }

                        img_frame.Save(editor_handle.project_default_dir + "\\Game-Resouces\\" + Path.GetFileNameWithoutExtension(txt_image.Text) + "_part" + current_part + ".png");

                        current_part++;
                    }
                }
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Resouces";

            open_file_dlg.ShowDialog();

            if (open_file_dlg.FileName != "")
            {
                if (Path.GetExtension(open_file_dlg.FileName) == ".png" || Path.GetExtension(open_file_dlg.FileName) == "png")
                {
                    txt_image.Text = open_file_dlg.FileName;
                }
            }
        }

        private void txt_width_TextChanged(object sender, EventArgs e)
        {
            foreach(char c in txt_width.Text)
            {
                if (!Char.IsDigit(c))
                {
                    txt_width.Text = "";
                    return;
                }
            }
        }

        private void txt_height_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in txt_height.Text)
            {
                if (!Char.IsDigit(c))
                {
                    txt_height.Text = "";
                    return;
                }
            }
        }


    }
}
