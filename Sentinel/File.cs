using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel
{
    internal class FileHandeler
    {
        static public string CreateFile()
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.ShowDialog();
            File.Create(saveFileDialog1.FileName).Close();
            return saveFileDialog1.FileName;
        }
    }
}
