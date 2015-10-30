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
    public partial class AnimationEditor : Form
    {
        class Frame
        {
          public Image baseImage;
          public string imagePath;
          public int scene_x;
          public int scene_y;
        }

        Editor editor_handle;
        int animation_speed = 0;
        int current_frame = -1;
        int current_anim_frame = 1;
        List<Frame> frame_list = new List<Frame>();
        string cur_file = "";
        MenuItem shortmenu_createFrame = new MenuItem("Create Frame");
        MenuItem shortmenu_editFrame = new MenuItem("Change Frame");
        MenuItem shortmenu_deleteFrame = new MenuItem("Delete Frame");

        public AnimationEditor( Editor editor_handle , string start_file)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();

            if (File.Exists(start_file))
            {
                cur_file = start_file;
            }
            else
            {
                cur_file = "new_file";
            }
        }

        private void AnimationEditor_Load(object sender, EventArgs e)
        {
            this.canvas.Paint += canvas_Paint;

            this.shortmenu_createFrame.Click += shortmenu_createFrame_Click;
            this.shortmenu_editFrame.Click += shortmenu_editFrame_Click;
            this.shortmenu_deleteFrame.Click += shortmenu_deleteFrame_Click;
            this.canvas.MouseClick += canvas_MouseClick;
            this.txt_playSpeed.TextChanged += txt_playSpeed_TextChanged;

            if (cur_file != "new_file")
            {
                readAnimationFile(cur_file);
            }
        }

        void txt_playSpeed_TextChanged(object sender, EventArgs e)
        {
            if (txt_playSpeed.Text == "") return;

            foreach(char ch in txt_playSpeed.Text)
            {
                if (!Char.IsDigit(ch))
                {
                    txt_playSpeed.Text = "";
                    animation_speed = 0;
                    return;
                }
            }

            try
            {
                animation_speed = int.Parse(txt_playSpeed.Text);
            }
            catch(OverflowException ex)
            {
                animation_speed = 0;
                txt_playSpeed.Text = "";
            }
        }

        void shortmenu_editFrame_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                OpenFileDialog open_file_dlg = new OpenFileDialog();

                open_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Resouces";

                open_file_dlg.ShowDialog();


                if (open_file_dlg.FileName != "")
                {
                    if (Path.GetExtension(open_file_dlg.FileName) == ".png" || Path.GetExtension(open_file_dlg.FileName) == "png")
                    {
                        Frame frame = new Frame();

                        frame.baseImage = Image.FromFile(open_file_dlg.FileName);
                        frame.imagePath = Path.GetFileName(open_file_dlg.FileName);
                        frame.scene_x = canvas.Width / 2;
                        frame.scene_y = canvas.Height / 2;

                        frame_list[current_frame] = frame;

                  //      reloadFrameList();

                   //     current_frame = frame_list.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("File extension invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (tmr_play.Enabled) return;

            if (e.Button == MouseButtons.Left)
            {
                this.current_frame = -1;
            }
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenu short_menu = new ContextMenu();

                if (current_frame == -1)
                {
                    short_menu.MenuItems.AddRange(new MenuItem[] { shortmenu_createFrame });
                }
                else
                {
                    short_menu.MenuItems.AddRange(new MenuItem[] { shortmenu_editFrame , shortmenu_deleteFrame });
                }

                canvas.ContextMenu = short_menu;
            } 
        }

        void shortmenu_deleteFrame_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                frame_list.RemoveAt(current_frame);
            }

            reloadFrameList();
            current_frame = -1;
        }

        void shortmenu_selectFrame_Click(object sender, EventArgs e)
        {
           if (lb_animations.SelectedIndex != -1)
           {
               this.current_frame = lb_animations.SelectedIndex;
           }
        }

        void shortmenu_createFrame_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Resouces";

            open_file_dlg.ShowDialog();

             if (open_file_dlg.FileName != "")
             {
                 if (Path.GetExtension(open_file_dlg.FileName) == ".png" || Path.GetExtension(open_file_dlg.FileName) == "png")
                 {
                     Frame frame = new Frame();

                     frame.baseImage = Image.FromFile(open_file_dlg.FileName);
                     frame.imagePath = Path.GetFileName(open_file_dlg.FileName);
                     frame.scene_x = canvas.Width / 2;
                     frame.scene_y = canvas.Height / 2;

                     frame_list.Add(frame);

                     reloadFrameList();

                     current_frame = frame_list.Count - 1;
                 }
                 else
                 {
                     MessageBox.Show("File extension invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }

             editor_handle.reloadFileTree();
        }

        void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            if (tmr_play.Enabled)
            {
              if (current_anim_frame < frame_list.Count)  e.Graphics.DrawImage(frame_list[current_anim_frame].baseImage, new Point(frame_list[current_anim_frame].scene_x, frame_list[current_anim_frame].scene_y));
            }
            else
            {
                if (current_frame != -1)
                {
                    e.Graphics.DrawImage(frame_list[current_frame].baseImage, new Point(frame_list[current_frame].scene_x, frame_list[current_frame].scene_y));
                }
            }
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save current file ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                menuItem_SaveAnimation_Click(null, null);
            }

            this.Close();
        }

        private void menuItem_LoadAnimation_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dlg = new OpenFileDialog();

            open_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Animation";

            open_file_dlg.ShowDialog();

            if (open_file_dlg.FileName != "")
            {
                readAnimationFile(open_file_dlg.FileName);
            }
            else
            {
                MessageBox.Show("File extension invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reloadFrameList()
        {
            lb_animations.Items.Clear();

            foreach(Frame frame in frame_list)
            {
                lb_animations.Items.Add("Frame - " + (lb_animations.Items.Count + 1));
            }
        }

        private void readAnimationFile(string path)
        {
            StreamReader stm_rd = new StreamReader(path);
            List<string> token_list = new List<string>();

            while(!stm_rd.EndOfStream)
            {
                string line = stm_rd.ReadLine();
                bool isString = false;
                string cur_token = "";

                foreach (char ch in line)
                {
                    if (isString)
                    {
                        if (ch == '@')
                        {
                            isString = false;
                            token_list.Add(cur_token);
                            cur_token = "";
                        }
                        else
                        {
                            cur_token += ch;
                        }
                    }
                    else
                    {
                        if (ch == ' ')
                        {
                            if (cur_token != "")
                            {
                                token_list.Add(cur_token);
                                cur_token = "";
                            }
                        }
                        else if (ch == '@')
                        {
                            if (!isString)
                            {
                                isString = true;
                                cur_token = "@";
                            }
                        }
                        else
                        {
                            cur_token += ch;
                        }
                    }
                }

                if (cur_token != "")
                {
                    token_list.Add(cur_token);
                }
            }

            frame_list.Clear();
            lb_animations.Items.Clear();

            string cur_action = "";

            foreach(string str in token_list)
            {
                if (cur_action == "")
                {
                    if (str == "Speed:")
                    {
                        cur_action = "Speed";
                    }
                    else if (str == "Frames:")
                    {
                        cur_action = "Frames";
                    }
                }
                else
                {
                    if  (cur_action == "Speed" )
                    {
                        animation_speed = int.Parse(str);
                        
                        cur_action = "";
                    }
                    else if (cur_action == "Frames")
                    {
                        if (str == ";")
                        {
                            cur_action = "";
                            continue;
                        }

                        if (File.Exists(editor_handle.project_default_dir + "\\Game-Resouces\\" + str.Substring(1,str.Length - 1)))
                        {
                            Frame frame = new Frame();

                            frame.imagePath = str.Substring(1,str.Length - 1);
                            frame.baseImage = Image.FromFile(editor_handle.project_default_dir + "\\Game-Resouces\\" + str.Substring(1,str.Length - 1));
                            frame.scene_x = canvas.Width / 2;
                            frame.scene_y = canvas.Height / 2;

                            frame_list.Add(frame);
                            lb_animations.Items.Add("Frame - " + frame_list.Count);
                        }
                        else
                        {
                            MessageBox.Show("Error loading animation file ! Frame not found : " + str.Substring(1,str.Length - 1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            if (frame_list.Count > 0)
            {
                current_frame = 0;
            }

            txt_playSpeed.Text = animation_speed.ToString();
            cur_file = path;
        }

        private void menuItem_SaveAnimation_Click(object sender, EventArgs e)
        {
            if (cur_file == "new_file")
            {
                SaveFileDialog save_file_dlg = new SaveFileDialog();

                save_file_dlg.InitialDirectory = editor_handle.project_default_dir + "\\Game-Animation";

                save_file_dlg.ShowDialog();

                cur_file = save_file_dlg.FileName;
            }

            if (cur_file != "")
            {
                StreamWriter stm_wr = new StreamWriter(cur_file);

                stm_wr.WriteLine("Speed: " + animation_speed);
                stm_wr.Write("Frames: ");

                foreach(Frame frame in frame_list)
                {
                    stm_wr.WriteLine("@" + frame.imagePath + "@");
                }

                stm_wr.Write(";");

                stm_wr.Flush();

                stm_wr.Close();
            }

            editor_handle.reloadFileTree();
        }

        private void menuItem_NewAnimation_Click(object sender, EventArgs e)
        {
            cur_file = "new_file";
            
            frame_list.Clear();
            lb_animations.Items.Clear();

            current_frame = -1;
            animation_speed = 1;
        }

        private void menuItem_DeleteAnimation_Click(object sender, EventArgs e)
        {
            if (File.Exists(cur_file)) File.Delete(cur_file);
            menuItem_NewAnimation_Click(null, null);
            editor_handle.reloadFileTree();
        }

        private void tmr_draw_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        private void lb_animations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_animations.SelectedIndex != -1)
            {
                current_frame = lb_animations.SelectedIndex;
             }
        }

        private void btn_moveUp_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                this.frame_list[current_frame].scene_y -= 5;
            }
        }

        private void btn_moveDown_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                this.frame_list[current_frame].scene_y += 5;
            }
        }

        private void btn_moveLeft_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                this.frame_list[current_frame].scene_x -= 5;
            }
        }

        private void btn_moveRight_Click(object sender, EventArgs e)
        {
            if (current_frame != -1)
            {
                this.frame_list[current_frame].scene_x += 5;
            }
        }

        private void menuItem_PlayFrames_Click(object sender, EventArgs e)
        {
            lb_animations.Enabled = txt_playSpeed.Enabled  = menuItem_DeleteAnimation.Enabled = menuItem_NewAnimation.Enabled = menuItem_PlayFrames.Enabled = false;

            current_anim_frame = 0;

            tmr_play.Interval = (animation_speed > 0) ? animation_speed : 1;
            tmr_play.Enabled = true;
            tmr_play.Start();
        }

        private void tmr_play_Tick(object sender, EventArgs e)
        {
            if (current_anim_frame < frame_list.Count)
            {
                current_anim_frame++;
            }
            else
            {
                lb_animations.Enabled = txt_playSpeed.Enabled = menuItem_DeleteAnimation.Enabled = menuItem_NewAnimation.Enabled = menuItem_PlayFrames.Enabled = true;
                tmr_play.Enabled = false;
                tmr_play.Stop();
            }
        }

        private void menuItem_SpriteBaker_Click(object sender, EventArgs e)
        {
            SpriteBaker sprite_baker = new SpriteBaker(editor_handle);

            sprite_baker.Show();
        }
    }
}
