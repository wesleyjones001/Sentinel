using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sentinel
{
    internal class ExecuteCommand
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
                            tempLine = s + new String(' ', ((bytesPerLine*2)+(bytesPerLine-1)) - s.Length) + " | ";
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
        static public Output Execute(string command, Project project, Output lastCommand, string textInput = "")
        {
            Output o = new Output();
            o.mode = OutputMode.Set;
            o.output = "";
            string test = command.ToLower();
            if (command.StartsWith("echo "))
            {
                o.output = command.Substring(5);
            }
            else if (command.StartsWith(".echo "))
            {
                o.output = command.Substring(6);
                o.mode = OutputMode.Append;
            }
            else if (test.StartsWith("http."))
            {
                string method = command.Substring(5);
                if (method.ToLower().StartsWith("get"))
                {
                    string url = FormatURL(method.Substring(4));
                    o.output = GetHttp(url);
                }
            }
            else if (test.StartsWith("http."))
            {
                string method = command.Substring(5);
                if (method.ToLower().StartsWith("post"))
                {
                    string url = method.Substring(5);

                }
            }
            else if (test.StartsWith("to.hex"))
            {
                try
                {
                    byte[] data;
                    if (command.Substring(6).Length > 0)
                    {
                        data = Encoding.UTF8.GetBytes(command.Substring(7));
                    }
                    else if (File.Exists(project.currentFilePath))
                    {
                        data = File.ReadAllBytes(project.currentFilePath);
                    }
                    else
                    {
                        data = (byte[])lastCommand.output;
                    }
                    o.output = FormatHex(data, 25, "None", " ", true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                o.output = "";
            }
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