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
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        public void addLog(string error)
        {
             txt_loglist.Text += error + Environment.NewLine;
        }

        private void ErrorViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
