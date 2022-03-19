using Renci.SshNet;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;

namespace Sentinel
{
    public partial class SelectSessionType : Form
    {
        public string IPAddress = "";
        public int Port = 0;
        public string connectionType;
        public string COMPort = "";
        private List<RadioButton> radioButtons = new List<RadioButton>();
        public object ReturnConnection;
        public SelectSessionType()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            radioButtons.Add(tcp);
            radioButtons.Add(ftp);
            radioButtons.Add(ssh);
            radioButtons.Add(http);
            radioButtons.Add(telnet);
            radioButtons.Add(spi);
            radioButtons.Add(usb);
        }

        private bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            IPAddress = textBox1.Text;
            Port = (int)numericUpDown1.Value;

            foreach (RadioButton radio in radioButtons)
            {
                if (radio.Checked == true)
                {
                    connectionType = radio.Name.ToUpper();
                }
            }
            try
            {
                IPAddress ip;
                bool ValidateIP = System.Net.IPAddress.TryParse(IPAddress, out ip);

                if (!ValidateIP)
                {
                    ip = Dns.GetHostEntry(IPAddress).AddressList[0];
                }
                else if (System.Net.IPAddress.TryParse(IPAddress, out ip))
                {
                    this.Text = "Session " + ip.ToString() + ":" + Port + " //" + connectionType.ToUpper();
                } 
                else
                {
                    this.Text = "Session + " + COMPort;
                }
                switch (connectionType)
                {
                    case "TCP":
                        TcpClient client = new TcpClient();
                        client.ReceiveTimeout = 1000;
                        try
                        {
                            Stopwatch s = new Stopwatch();
                            s.Start();
                            int max = 2000;
                            client.ConnectAsync(ip, Port);
                            while (s.ElapsedMilliseconds < max && !client.Connected) ;
                            if (!client.Connected)
                            {
                                label3.Text = "Connection: FAIL";
                            }
                            else
                            {
                                label3.Text = "Connection: SUCCESS";
                            }
                            s.Stop();
                            client.Close();
                            client.Dispose();

                        }
                        catch (Exception ex)
                        {
                            label3.Text = "Connection: FAIL";
                            client.Close();
                            client.Dispose();
                        }
                        break;
                    case "SSH":
                    case "FTP":
                    case "TELNET":
                    case "HTTP":
                        bool status = PingHost(IPAddress, Port);
                        if (status)
                        {
                            label3.Text = "Connection: SUCCESS";
                        }
                        else
                        {
                            label3.Text = "Connection: FAIL";
                        }
                        break;
                    case "SPI":
                        try
                        {
                            SerialPort sp = new SerialPort(comboBox1.Text);
                            sp.Open();
                            if (sp.IsOpen)
                            {
                                label3.Text = "Connection: SUCCESS";
                                COMPort = comboBox1.Text;
                            }
                            else
                            {
                                label3.Text = "Connection: FAIL";
                            }
                            sp.Close();
                            sp.Dispose();
                        } catch
                        {
                            label3.Text = "Connection: FAIL";
                        }
                        break;
                    case "USB":
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void FormUpdate(object sender, EventArgs e)
        {
            foreach (RadioButton radio in radioButtons)
            {
                if (radio.Checked == true)
                {
                    connectionType = radio.Name.ToUpper();
                }
            }

            if (connectionType == "SPI" || connectionType == "USB")
            {
                comboBox1.Items.Clear();
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }
                label1.Text = "COM Port";
                label2.Hide();
                textBox1.Hide();
                numericUpDown1.Hide();
                comboBox1.Show();
            } else
            {
                label1.Text = "IP or Host";
                comboBox1.Items.Clear();
                comboBox1.Hide();
                textBox1.Show();
                numericUpDown1.Show();
                label2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IPAddress = textBox1.Text;
            Port = (int)numericUpDown1.Value;
            COMPort = comboBox1.Text;
            this.Close();
        }
    }
    }
