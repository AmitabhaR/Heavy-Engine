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
    public partial class NewScript : Form
    {
        Editor editor_handle;
        public NewScript(Editor editor_handle)
        {
            this.editor_handle = editor_handle;
            InitializeComponent();
        }

        private void NewScript_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txt_script_name.Text != "")
            {
                StreamWriter stm_wr = null;

                foreach (string path in Directory.GetFiles(editor_handle.project_default_dir + "\\Game-Scripts"))
                {
                    if (cb_bos_script.Checked)
                    {
                        if (Path.GetFileName(path) == txt_script_name.Text + ".bs")
                        {
                            MessageBox.Show("File already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (Path.GetFileNameWithoutExtension(path) == txt_script_name.Text)
                        {
                            MessageBox.Show("Bos script file names must be unique!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (editor_handle.platform_id == 1)
                    {
                        if (Path.GetFileName(path) == txt_script_name.Text + ".cs")
                        {
                            MessageBox.Show("File already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                    {
                        if (Path.GetFileName(path) == txt_script_name.Text + ".java")
                        {
                            MessageBox.Show("File already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
                    {
                        if (Path.GetFileName(path) == txt_script_name.Text + ".cpp")
                        {
                            MessageBox.Show("File already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                if (cb_bos_script.Checked)
                {
                  stm_wr = File.CreateText(editor_handle.project_default_dir + "\\Game-Scripts\\" + txt_script_name.Text + ".bs");
                }
                else if (editor_handle.platform_id == 1)
                {
                  stm_wr =  File.CreateText(editor_handle.project_default_dir + "\\Game-Scripts\\" + txt_script_name.Text + ".cs");

                  stm_wr.WriteLine("using Runtime; \n");
                  stm_wr.WriteLine("public class " + txt_script_name.Text + " : HeavyScript \n {");

                  stm_wr.WriteLine("\tbool isInit = true;");

                  stm_wr.WriteLine("\tpublic override void process(GameObject_Scene gameObject) \n\t{");
                  stm_wr.WriteLine("\t\tif (isInit) \n\t\t{");
                  stm_wr.WriteLine("\t\t\t//Write your initialization part here.");
                  stm_wr.WriteLine("\t\t\tisInit = false;");
                  stm_wr.WriteLine("\t}");
                  stm_wr.WriteLine("\t\telse \n\t\t{");
                  stm_wr.WriteLine("\t\t\t//Write your update part here.");
                  stm_wr.WriteLine("\t\t}");
                  stm_wr.WriteLine("\t}");
                  stm_wr.WriteLine("}");
                }
                else if (editor_handle.platform_id == 2 || editor_handle.platform_id == 3)
                {
                  stm_wr = File.CreateText(editor_handle.project_default_dir + "\\Game-Scripts\\" + txt_script_name.Text + ".java");

                  stm_wr.WriteLine("import jruntime.*; \n");
                  stm_wr.WriteLine("public class " + txt_script_name.Text + " extends HeavyScript \n {");

                  stm_wr.WriteLine("\tboolean isInit = true;");

                  stm_wr.WriteLine("\tpublic void process(GameObject_Scene gameObject) \n\t{");
                  stm_wr.WriteLine("\t\tif (isInit) \n\t\t{");
                  stm_wr.WriteLine("\t\t\t//Write your initialization part here.");
                  stm_wr.WriteLine("\t\t\tisInit = false;");
                  stm_wr.WriteLine("\t}");
                  stm_wr.WriteLine("\t\telse \n\t\t{");
                  stm_wr.WriteLine("\t\t\t//Write your update part here.");
                  stm_wr.WriteLine("\t\t}");
                  stm_wr.WriteLine("\t}");
                  stm_wr.WriteLine("}");  
                }
                else if (editor_handle.platform_id == 4 || editor_handle.platform_id == 5)
                {
                    stm_wr = File.CreateText(editor_handle.project_default_dir + "\\Game-Scripts\\" + txt_script_name.Text + ".cpp");

                    stm_wr.WriteLine("#include \"" + txt_script_name.Text + ".h\"\n\n");
                    stm_wr.WriteLine("void " + txt_script_name.Text + "::process(void * gameObject) \n{");
                    stm_wr.WriteLine("\tif (isInit) \n\t{");
                    stm_wr.WriteLine("\t\t//Write your initialization part here.");
                    stm_wr.WriteLine("\t\tisInit = false;");
                    stm_wr.WriteLine("\t}");
                    stm_wr.WriteLine("\telse \n\t{");
                    stm_wr.WriteLine("\t\t//Write your update part here.");
                    stm_wr.WriteLine("\t}");
                    stm_wr.WriteLine("}");  

                    stm_wr.Close();

                    stm_wr = File.CreateText(editor_handle.project_default_dir + "\\Game-Scripts\\" + txt_script_name.Text + ".h");

                    stm_wr.WriteLine("#ifndef " + txt_script_name.Text.ToUpper() + "_H");
                    stm_wr.WriteLine("#define " + txt_script_name.Text.ToUpper() + "_H \n");

                    stm_wr.WriteLine("#include<HeavyEngine.h>");

                    stm_wr.WriteLine("class " + txt_script_name.Text + " : public HeavyScript {");
                    stm_wr.WriteLine("public:");
                    stm_wr.WriteLine("bool isInit = false;");
                    stm_wr.WriteLine("void process(void *);");
                    stm_wr.WriteLine("};");

                    stm_wr.WriteLine("#endif");
                }

                stm_wr.Close();

                editor_handle.reloadFileTree();

                this.Close();
            }
            else
            {
                MessageBox.Show("Script name required !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_script_name_TextChanged(object sender, EventArgs e)
        {
            foreach (char c in txt_script_name.Text)
            {
                if (c == ' ')
                {
                    txt_script_name.Text = txt_script_name.Text.Replace(" ", "");
                    break;
                }
            }
        }
    }
}
