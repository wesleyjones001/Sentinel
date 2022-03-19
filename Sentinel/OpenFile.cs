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
    public partial class OpenFile : Form
    {
        public string FileName = "";
        public string FilePath = "";
        public OpenFile()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;
            FileName = FilePath.Split('\\').Last();
            label3.Text = FilePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
