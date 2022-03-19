namespace Sentinel
{
    partial class SelectSessionType
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tcp = new System.Windows.Forms.RadioButton();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.usb = new System.Windows.Forms.RadioButton();
            this.spi = new System.Windows.Forms.RadioButton();
            this.telnet = new System.Windows.Forms.RadioButton();
            this.ssh = new System.Windows.Forms.RadioButton();
            this.ftp = new System.Windows.Forms.RadioButton();
            this.http = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FormPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(55, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP or Host";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 23);
            this.textBox1.TabIndex = 1;
            // 
            // tcp
            // 
            this.tcp.AutoSize = true;
            this.tcp.Checked = true;
            this.tcp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tcp.ForeColor = System.Drawing.Color.White;
            this.tcp.Location = new System.Drawing.Point(31, 23);
            this.tcp.Name = "tcp";
            this.tcp.Size = new System.Drawing.Size(47, 21);
            this.tcp.TabIndex = 2;
            this.tcp.TabStop = true;
            this.tcp.Text = "TCP";
            this.tcp.UseVisualStyleBackColor = true;
            this.tcp.CheckedChanged += new System.EventHandler(this.FormUpdate);
            this.tcp.Click += new System.EventHandler(this.FormUpdate);
            // 
            // FormPanel
            // 
            this.FormPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.FormPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FormPanel.Controls.Add(this.usb);
            this.FormPanel.Controls.Add(this.spi);
            this.FormPanel.Controls.Add(this.telnet);
            this.FormPanel.Controls.Add(this.ssh);
            this.FormPanel.Controls.Add(this.ftp);
            this.FormPanel.Controls.Add(this.http);
            this.FormPanel.Controls.Add(this.tcp);
            this.FormPanel.Location = new System.Drawing.Point(55, 128);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(648, 113);
            this.FormPanel.TabIndex = 3;
            // 
            // usb
            // 
            this.usb.AutoSize = true;
            this.usb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.usb.ForeColor = System.Drawing.Color.White;
            this.usb.Location = new System.Drawing.Point(133, 72);
            this.usb.Name = "usb";
            this.usb.Size = new System.Drawing.Size(85, 21);
            this.usb.TabIndex = 9;
            this.usb.Text = "Serial USB";
            this.usb.UseVisualStyleBackColor = true;
            this.usb.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // spi
            // 
            this.spi.AutoSize = true;
            this.spi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.spi.ForeColor = System.Drawing.Color.White;
            this.spi.Location = new System.Drawing.Point(31, 72);
            this.spi.Name = "spi";
            this.spi.Size = new System.Drawing.Size(79, 21);
            this.spi.TabIndex = 7;
            this.spi.Text = "Serial SPI";
            this.spi.UseVisualStyleBackColor = true;
            this.spi.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // telnet
            // 
            this.telnet.AutoSize = true;
            this.telnet.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.telnet.ForeColor = System.Drawing.Color.White;
            this.telnet.Location = new System.Drawing.Point(501, 23);
            this.telnet.Name = "telnet";
            this.telnet.Size = new System.Drawing.Size(60, 21);
            this.telnet.TabIndex = 6;
            this.telnet.Text = "Telnet";
            this.telnet.UseVisualStyleBackColor = true;
            this.telnet.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // ssh
            // 
            this.ssh.AutoSize = true;
            this.ssh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ssh.ForeColor = System.Drawing.Color.White;
            this.ssh.Location = new System.Drawing.Point(133, 23);
            this.ssh.Name = "ssh";
            this.ssh.Size = new System.Drawing.Size(49, 21);
            this.ssh.TabIndex = 5;
            this.ssh.Text = "SSH";
            this.ssh.UseVisualStyleBackColor = true;
            this.ssh.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // ftp
            // 
            this.ftp.AutoSize = true;
            this.ftp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ftp.ForeColor = System.Drawing.Color.White;
            this.ftp.Location = new System.Drawing.Point(263, 23);
            this.ftp.Name = "ftp";
            this.ftp.Size = new System.Drawing.Size(46, 21);
            this.ftp.TabIndex = 4;
            this.ftp.Text = "FTP";
            this.ftp.UseVisualStyleBackColor = true;
            this.ftp.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // http
            // 
            this.http.AutoSize = true;
            this.http.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.http.ForeColor = System.Drawing.Color.White;
            this.http.Location = new System.Drawing.Point(376, 23);
            this.http.Name = "http";
            this.http.Size = new System.Drawing.Size(56, 21);
            this.http.TabIndex = 3;
            this.http.Text = "HTTP";
            this.http.UseVisualStyleBackColor = true;
            this.http.CheckedChanged += new System.EventHandler(this.FormUpdate);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(542, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test Connection";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(364, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Port";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(408, 54);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(96, 23);
            this.numericUpDown1.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(542, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 35);
            this.button2.TabIndex = 12;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(55, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Connection: Untested";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(163, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 23);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Visible = false;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(763, 408);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FormPanel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form5";
            this.Text = "Sersion";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.FormPanel.ResumeLayout(false);
            this.FormPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private RadioButton tcp;
        private Panel FormPanel;
        private RadioButton telnet;
        private RadioButton ssh;
        private RadioButton ftp;
        private RadioButton http;
        private Button button1;
        private Label label2;
        private NumericUpDown numericUpDown1;
        private Button button2;
        private RadioButton usb;
        private RadioButton spi;
        private Label label3;
        private ComboBox comboBox1;
    }
}