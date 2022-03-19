using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sentinel
{
    public partial class OpenProject : Form
    {
        public string ProjectPath = "";
        public OpenProject()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            ProjectPath = folderBrowserDialog1.SelectedPath;
            label3.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
