﻿using System;
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
    public partial class CreateProject : Form
    {
        public string FolderName = "";
        public string projectFolder = "";
        public CreateProject()
        {
            InitializeComponent();
        }

        void GetUserInput()
        {
            folderBrowserDialog1.ShowDialog();
            FolderName = folderBrowserDialog1.SelectedPath;
            label1.Text = FolderName;
        }
        void CreateProjectFolder()
        {
            string projectName = textBox1.Text;
            string ProjectFolder = FolderName + @"\" + projectName;
            Directory.CreateDirectory(ProjectFolder);
            projectFolder = ProjectFolder;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            GetUserInput();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetUserInput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateProjectFolder();
            this.Close();
        }
    }
}
