using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel
{
    internal class Project
    {
        public string projectDirectory = "";
        public string projectName = "";
        public string currentFilePath = "";
        public bool IsProject = true;
        List<FileStream> openFiles = new List<FileStream>();
        public void CreateProject()
        {
            CreateProject f = new CreateProject();
            f.ShowDialog();
            projectDirectory = f.projectFolder;
            projectName = projectDirectory.Split('\\').Last().Trim();
            IsProject = true;
            File.Create(projectDirectory + "\\" + "output.tmp").Close();
            return;
        }
        public void Close()
        {
            foreach (FileStream file in openFiles)
            {
                try
                {
                    file.Close();
                }
                catch { };
            }
            return;
        }
    }
}
