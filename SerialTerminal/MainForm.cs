using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialTerminal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxPort.Text = serialPort.PortName;
        }

        private static string escape_of_(string sbuffer, byte[] buffer, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; ++i)
            {
                char c = sbuffer[i];
#if (false)
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
                else if (c == '\r')
                {
                    sb.Append("\\r");
                }
                else if (c == '\n')
                {
                    sb.Append("\\n");
                }
                else if (c == '\\')
                {
                    sb.Append("\\\\");
                }
                else
                {
                    sb.Append(String.Format("\\{0:x2}", buffer[i]));
                }
#else
                sb.Append(c);
#endif
            }
            return sb.ToString();
        }

        /// <summary>
        /// String representation of data.
        /// </summary>
        /// <param name="buffer">Data buffer.</param>
        /// <param name="count">Number of bytes in the buffer.</param>
        /// <param name="full_hex">Display only hex or something else, too.</param>
        /// <returns></returns>
        private static string escape(byte[] buffer, int count, bool full_hex)
        {
            if (full_hex)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < count; ++i)
                {
                    sb.Append(String.Format("|{0:x2}", buffer[i]));
                }
                return sb.ToString();
            }
            else
            {
                string sbuffer = Encoding.ASCII.GetString(buffer, 0, count);
                return escape_of_(sbuffer, buffer, count);
            }
        }

        private delegate void InvoketDelegate();
        private void AppendLog(string text)
        {
            if (textBoxLog.InvokeRequired)
            {
                Invoke(new InvoketDelegate(delegate() { textBoxLog.AppendText(text); }));
            }
            else
            {
                textBoxLog.AppendText(text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HandleSerialPortData(byte[] buffer)
        {
            AppendLog(escape(buffer, buffer.Length, checkBoxFullHex.Checked));
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int n = serialPort.BytesToRead;
            if (n > 0)
            {
                byte[] buffer = new byte[n];
                serialPort.Read(buffer, 0, n);
                if (InvokeRequired)
                {
                    Invoke(new InvoketDelegate(delegate() { HandleSerialPortData(buffer); }));
                }
                else
                {
                    HandleSerialPortData(buffer);
                }
            }
        }

        private void serialPort_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            AppendLog("|__");
        }

        private void timerStartup_Tick(object sender, EventArgs e)
        {
            timerStartup.Enabled = false;
            try {
                serialPort.Open();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void buttonSendLine_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(textBoxLineToSend.Text);
            if (radioButtonCR.Checked)
            {
                sb.Append('\r');
            }
            if (radioButtonLF.Checked)
            {
                sb.Append('\n');
            }
            if (radioButtonCRLF.Checked)
            {
                sb.Append("\r\n");
            }
            string s = sb.ToString();
            serialPort.Write(s);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxLog.Text = "";
        }

        private void buttonOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                CSUtils.SerialPortHelper.openSerialPortByDescription(serialPort, textBoxPort.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}