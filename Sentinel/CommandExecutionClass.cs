using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sentinel
{
    internal class CommandExecutionClass
    {
        public enum OutputMode
        {
            Append,
            Set
        }
        public struct Output
        {
            public object output;
            public OutputMode mode;
        }
        static private string FormatURL(string url)
        {
            string output = "";
            if (!url.StartsWith("https"))
            {
                if (!url.StartsWith("http"))
                {
                    output = "http://" + url;
                }
                else
                {
                    output = "https://" + url;
                }
            }
            else
            {
                output = url;
            }
            return output;
        }


        static string GetHttp(string url)
        {
            try
            {
                string html = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                return html;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
        static public string FormatHex(byte[] data, int bytesPerLine = 25, string Format = "none", string deliminator = " ", bool ShowChars = false)
        {
            string output = "";
            if (data.Length > 0)
            {
                byte[][] chunks = SplitByteArray(data, bytesPerLine);
                List<string> linesOfHex = new List<string>();
                foreach (byte[] chunk in chunks)
                {
                    List<string> line = new List<string>();
                    string[] temp;
                    switch (Format)
                    {
                        case "0x":
                            temp = BitConverter.ToString(chunk).Split('-');
                            foreach (string s in temp)
                            {
                                line.Add("0x" + s);
                            }
                            break;
                        case "\\x":
                            temp = BitConverter.ToString(chunk).Split('-');
                            foreach (string s in temp)
                            {
                                line.Add("\\x" + s);
                            }
                            break;
                        default:
                            line.Add(BitConverter.ToString(chunk).Replace("-", " "));
                            break;
                    }
                    linesOfHex.Add(string.Join(deliminator, line));
                }
                if (ShowChars)
                {
                    List<string> temp = new List<string>();
                    foreach (string s in linesOfHex)
                    {
                        string tempLine = s + " | ";
                        if (((s.Length - (bytesPerLine - 1)) / 2) < bytesPerLine)
                        {
                            tempLine = s + new String(' ', ((bytesPerLine * 2) + (bytesPerLine - 1)) - s.Length) + " | ";
                        }
                        byte[] array = HexToBytes(s);
                        foreach (char c in array)
                        {
                            if (c >= 32 && c <= 126)
                            {
                                tempLine += c.ToString();
                            }
                            else
                            {
                                tempLine += ".";
                            }
                        }
                        tempLine += " |";
                        temp.Add(tempLine);
                    }
                    linesOfHex = temp;
                }
                output = String.Join("\n", linesOfHex.ToArray());
            }
            return output;
        }
        public static byte[] HexToBytes(string hexString)
        {
            hexString = hexString.Trim().Replace(" ", "");
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
        private Dictionary<string, object> memory = new Dictionary<string, object>();
        private List<string> GetElementsFromString(string data = "")
        {
            return data.Split('"').Select((element, index) => index % 2 == 0 ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { element }).SelectMany(element => element).ToList();
        }
        public string ExecuteCommand(string command, Project project, string prev = "", int i = 0)
        {
            string output = string.Empty;
            List<string> commands = GetElementsFromString(command);
            if (command != string.Empty)
            {
                int skipIndexs = 1;

                if (commands.Count > i)
                {
                    try
                    {
                        switch (commands[i])
                        {
                            case "clearvars":
                                memory.Clear();
                                skipIndexs = 1;
                                break;
                            case "set":
                                if (memory.ContainsKey(commands[i + 1]))
                                {
                                    if (commands.Count() > i + 2)
                                    {
                                        memory[commands[i + 1]] = commands[i + 2];
                                    }
                                    else
                                    {
                                        memory.Remove(commands[i + 1]);
                                    }
                                }
                                else
                                {
                                    memory.Add(commands[i + 1], commands[i + 2]);
                                }
                                skipIndexs = 3;
                                break;
                            case "print":
                                if (commands[i + 1].StartsWith("$"))
                                {
                                    output = memory[commands[i + 1].Substring(1)].ToString();
                                    skipIndexs = 2;
                                }
                                else
                                {
                                    output = commands[i + 1];
                                    skipIndexs = 2;
                                }
                                break;
                            case "http":
                                if (commands[i + 1].StartsWith("get"))
                                {
                                    output = GetHttp(FormatURL(commands[i + 2]));
                                }
                                skipIndexs = 3;
                                break;
                            case "get":
                                output = memory[commands[i + 1]].ToString();
                                skipIndexs = 2;
                                break;
                            case "hexdump":
                                byte[] data;
                                if (commands.Count() > (i+1))
                                {
                                    data = Encoding.UTF8.GetBytes(commands[i+1]);
                                    output = FormatHex(data, 25, "None", " ", true);
                                    skipIndexs = 2;
                                }
                                else if (File.Exists(project.currentFilePath))
                                {
                                    data = File.ReadAllBytes(project.currentFilePath);
                                    output = FormatHex(data, 25, "None", " ", true);
                                    skipIndexs = 1;
                                } else
                                {
                                    output = "";
                                    skipIndexs = 1;
                                }
                                break;
                            case "analyze":
                                byte[] dataToProcess;
                                switch (commands[i + 1].ToLower())
                                {
                                    case "sd":
                                        dataToProcess = File.ReadAllBytes(project.currentFilePath);
                                        double average = 0;

                                        foreach (byte b in dataToProcess)
                                        {
                                            average += b;
                                        }
                                        average /= dataToProcess.Length;
                                        double sumOfSquaresOfDifferences = dataToProcess.Select(val => (val - average) * (val - average)).Sum();
                                        double sd = Math.Sqrt(sumOfSquaresOfDifferences / dataToProcess.Length);
                                        output = sd.ToString();
                                        skipIndexs = 2;
                                        break;
                                    case "readable":
                                    case "%":
                                    case "%readable":
                                        dataToProcess = File.ReadAllBytes(project.currentFilePath);

                                        float j = 0;
                                        foreach (byte b in dataToProcess)
                                        {
                                            if (b >= 32 && b <= 126 || b == '\n' || b == '\r')
                                            {
                                                j++;
                                            }
                                        }
                                        float percent = ((float)j / dataToProcess.Length) * 100;
                                        output = @"Percent of binary that is readable (0x20 - 0x7e): " + (percent.ToString("0.0000")) + "%\n" + (percent > 90 ? "Likey a text file." : "Not likey a text file.");
                                        break;
                                }
                                break;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }

                    ExecuteCommand(String.Join(" ", commands), project,  output, i + skipIndexs);
                }

            }
            return output;
        }

        public Output Execute(string command, Project project, Output lastCommand, string textInput = "")
        {
            Output o = new Output();
            o.mode = OutputMode.Set;
            o.output = "";

            Dictionary<string, object> memory = new Dictionary<string, object>();
            o.output = ExecuteCommand(command, project, "");


            //else if (test.StartsWith("to.hex"))
            //{
            //    try
            //    {
            //        byte[] data;
            //        if (command.Substring(6).Length > 0)
            //        {
            //            data = Encoding.UTF8.GetBytes(command.Substring(7));
            //        }
            //        else if (File.Exists(project.currentFilePath))
            //        {
            //            data = File.ReadAllBytes(project.currentFilePath);
            //        }
            //        else
            //        {
            //            data = (byte[])lastCommand.output;
            //        }
            //        o.output = FormatHex(data, 25, "None", " ", true);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //else
            //{
            //    o.output = "";
            //}
            return o;
        }
        static private byte[][] SplitByteArray(byte[] data, int size)
        {
            List<byte[]> list = new List<byte[]>();
            for (var i = 0; i < data.Length / size + 1; i++)
            {
                list.Add(data.Skip(i * size).Take(size).ToArray());
            }
            return list.ToArray();
        }
    }
}