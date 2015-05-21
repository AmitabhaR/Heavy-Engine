using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heavy_Engine
{
    public partial class BuildProgress : Form
    {
        string[] progress_array = {  "Generating Resources" , "Generating Levels" , "Compiling Scripts" , "Packing" , "Succesfull!" };

        public BuildProgress()
        {
            InitializeComponent();
        }

        private void BuildProgress_Load(object sender, EventArgs e)
        {

        }

        public void progress()
        {
            if (prg_progress.Value == 100) return;

            lbl_progress.Text = progress_array[prg_progress.Value / 20];
            prg_progress.Value += 20;
        }
    }
}
