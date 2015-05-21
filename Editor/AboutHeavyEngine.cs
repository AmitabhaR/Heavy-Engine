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
    public partial class AboutHeavyEngine : Form
    {
        public AboutHeavyEngine()
        {
            InitializeComponent();
        }

        private void AboutHeavyEngine_Load(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
