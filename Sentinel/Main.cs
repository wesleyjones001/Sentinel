using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Sentinel
{

    public partial class Main : Form
    {
        private Project project = new Project();
        private object session;
        public Main()
        {
            InitializeComponent();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = FileHandeler.CreateFile();
            project.currentFilePath = file;
            string[] array = file.Split('\\');
            file = string.Join('\\', array.Take(array.Length - 1));
            ListDirectory(treeView1, file);
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            if (path != null && path.Length > 0)
            {
                treeView.Nodes.Clear();
                // get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(path);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    treeView.Nodes.Add(path.Split('\\').Last());
                    return;
                }
                treeView.Nodes.Clear();
                var rootDirectoryInfo = new DirectoryInfo(path);
                treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            }
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project != null)
            {
                project.Close();
            }
            project.CreateProject();
            ListDirectory(treeView1, project.projectDirectory);
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFile f3 = new OpenFile();
            f3.ShowDialog();
            project.currentFilePath = f3.FilePath;
            project.IsProject = false;
            ListDirectory(treeView1, project.currentFilePath);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject f4 = new OpenProject();
            f4.ShowDialog();
            project.projectDirectory = f4.ProjectPath;
            ListDirectory(treeView1, project.projectDirectory);
            project.IsProject = true;
        }

        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.Close();
            if (project.IsProject)
            {
                ListDirectory(treeView1, project.projectDirectory);
                project.IsProject = true;
            }
            else
            {
                ListDirectory(treeView1, project.projectDirectory);
                project.IsProject = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.Close();
        }

        private void closeSafeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.Close();
        }
        private void OpenBinary(string filepath)
        {
            if (File.Exists(filepath))
            {
                byte[] data = File.ReadAllBytes(filepath);
                var chunks = data.Split(45);
                string temp = "";
                foreach (var chunk in chunks)
                {
                    temp += BitConverter.ToString(chunk.ToArray()).Replace("-", " ") + "\n";
                }
                richTextBox2.Text = temp.Trim();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                List<string> fullPath = new List<string>();
                TreeNode node = e.Node;
                while (node.Parent != null)
                {
                    fullPath.Add(node.Text);
                    node = node.Parent;
                }
                fullPath.Reverse();
                string path = "";
                if (project.IsProject)
                {

                    path = project.projectDirectory + "\\" + string.Join("\\", fullPath);
                }
                else
                {
                    path = project.currentFilePath;
                }
                if (File.Exists(path))
                {
                    richTextBox1.Text = File.ReadAllText(path);
                    project.currentFilePath = path;
                    this.Text = "Sentinel Suite / " + path;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            project.currentFilePath = "output.tmp";
        }
        private void hexdumpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenBinary(project.currentFilePath);
        }

        private void frequencyAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";

            int[] frequency = new int[256];

            byte[] data = File.ReadAllBytes(project.currentFilePath);

            foreach (byte b in data)
            {
                frequency[b]++;
            }

            byte i = 0;

            List<string> list = new List<string>();
            foreach (int b in frequency)
            {
                if (b != 0)
                {
                    richTextBox2.Text += ("0x" + i.ToString("X") + " : " + b.ToString() + "\n");
                }
                i++;
            }
        }

        private void percentReadableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes(project.currentFilePath);

            float i = 0;
            foreach (byte b in data)
            {
                if (b >= 32 && b <= 126 || b == '\n' || b == '\r')
                {
                    i++;
                }
            }
            float percent = ((float)i / data.Length) * 100;
            string output = @"Percent of binary that is readable (0x20 - 0x7e): " + (percent.ToString("0.0000")) + "%\n" + (percent > 90 ? "Likey a text file." : "Not likey a text file.");
            richTextBox2.Text = output;
        }
        private double getStandardDeviation(byte[] byteList)
        {

            double average = 0;

            foreach (byte b in byteList)
            {
                average += b;
            }
            average /= byteList.Length;
            double sumOfSquaresOfDifferences = byteList.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / byteList.Length);
            return sd;
        }
        private void standardDeveationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "Standard Deviation of bytes in file: " + getStandardDeviation(File.ReadAllBytes(project.currentFilePath)).ToString();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                List<string> fullPath = new List<string>();
                TreeNode node = e.Node;
                while (node.Parent != null)
                {
                    fullPath.Add(node.Text);
                    node = node.Parent;
                }
                fullPath.Reverse();
                string path = "";
                if (project.IsProject)
                {
                    path = project.projectDirectory + "\\" + string.Join("\\", fullPath);
                }
                else
                {
                    path = project.currentFilePath;
                }
                if (path[path.Length - 1] == '\\')
                {
                    path = path.Substring(0, path.Length - 1);
                }
                richTextBox1.Text = File.ReadAllText(path);
                project.currentFilePath = path;
            }
            catch (Exception ex)
            {

            };
        }

        private void newSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectSessionType f5 = new SelectSessionType();
            f5.ShowDialog();
            string connectionType = f5.connectionType;

            switch (connectionType)
            {
                case "SSH":

                    break;
            }
            sessionResetToolStripMenuItem.Enabled = true;
            closeAllSessionsToolStripMenuItem.Enabled = true;
            closeSessionToolStripMenuItem.Enabled = true;
        }

        private void closeSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            session = null;
            sessionResetToolStripMenuItem.Enabled = false;
            closeAllSessionsToolStripMenuItem.Enabled = false;
            closeSessionToolStripMenuItem.Enabled = false;
        }

        private void closeAllSessionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            session = null;
            sessionResetToolStripMenuItem.Enabled = false;
            closeAllSessionsToolStripMenuItem.Enabled = false;
            closeSessionToolStripMenuItem.Enabled = false;
        }

        private void sessionResetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void SetOutput(string data)
        {
            richTextBox1.Text = data;
        }

        public void AppendOutput(string data)
        {
            richTextBox1.Text += data + "\n";
        }
        private ExecuteCommand.Output LastOutput;
        private void richTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == '\r')
            {
                richTextBox1.Font = new Font(FontFamily.GenericMonospace, richTextBox1.Font.Size);
                string[] commands = richTextBox3.Text.Trim().Split("\n");
                string command = commands[commands.Length - 1];
                ExecuteCommand.Output o = new ExecuteCommand.Output();
                o = ExecuteCommand.Execute(command, project, LastOutput, richTextBox1.Text);
                if (o.mode == ExecuteCommand.OutputMode.Set)
                {
                    SetOutput(o.output.ToString());
                }
                else if (o.mode == ExecuteCommand.OutputMode.Append)
                {
                    AppendOutput(o.output.ToString());
                }
                LastOutput = o;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string command in richTextBox3.Text.Trim().Split("\n"))
            {
                ExecuteCommand.Output o = new ExecuteCommand.Output();
                o = ExecuteCommand.Execute(command, project, LastOutput);
                if (o.mode == ExecuteCommand.OutputMode.Set)
                {
                    SetOutput(o.output.ToString());
                }
                else if (o.mode == ExecuteCommand.OutputMode.Append)
                {
                    AppendOutput(o.output.ToString());
                }
                LastOutput = o;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (project.IsProject)
                {
                    File.WriteAllText(project.projectDirectory + "output.tmp", richTextBox1.Text);
                }
                else
                {
                    string[] tmp = project.currentFilePath.Split('\\');
                    string t = string.Join("\\", tmp.Take(tmp.Length - 1)) + "\\" + "output.tmp";
                    File.WriteAllText(t, richTextBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving tmp file.");
            }
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] bytes = File.ReadAllBytes(project.currentFilePath);
            List<string> strings = new List<string>();
            string temp = "";
            bool lastLineWasBlank = true;
            foreach (byte b in bytes)
            {
                if (b >= 32 && b <= 126)
                {
                    temp += (char)b;
                    lastLineWasBlank = false;
                }
                else
                {
                    lastLineWasBlank = true;
                }
                if (lastLineWasBlank != false)
                {
                    strings.Add(temp);
                }
            }

            string output = string.Join("\n", strings);
            richTextBox2.Text = string.Join("\n", strings);
            return;
        }
    }
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] arr, int size)
        {
            for (var i = 0; i < arr.Length / size + 1; i++)
            {
                yield return arr.Skip(i * size).Take(size);
            }
        }
    }
}