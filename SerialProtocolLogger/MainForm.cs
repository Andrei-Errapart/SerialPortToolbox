using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

using CSUtils;

namespace SerialProtocolLogger
{
    public partial class MainForm : Form
    {
        private Dictionary<string, CMRAssembler> ProtocolCmrAssemblers = new Dictionary<string,CMRAssembler>();
        private Dictionary<string, GeotracerAssembler> ProtocolGeotracerAssemblers = new Dictionary<string, GeotracerAssembler>();

        public MainForm()
        {
            InitializeComponent();

            textBoxPort1.Text = serialPort1.PortName;
            textBoxBaudratePort1.Text = serialPort1.BaudRate.ToString();

            textBoxPort2.Text = serialPort2.PortName;
            textBoxBaudratePort2.Text = serialPort2.BaudRate.ToString();

            ProtocolCmrAssemblers.Add("Port1", new CMRAssembler());
            ProtocolCmrAssemblers.Add("Port2", new CMRAssembler());
            ProtocolGeotracerAssemblers.Add("Port1", new GeotracerAssembler());
            ProtocolGeotracerAssemblers.Add("Port2", new GeotracerAssembler());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenPort(SerialPort port, TextBox box, TextBox baudrateBox)
        {
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
                port.PortName = box.Text;
                port.BaudRate = int.Parse(baudrateBox.Text);
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void buttonPort1Open_Click(object sender, EventArgs e)
        {
            OpenPort(serialPort1, textBoxPort1, textBoxBaudratePort1);
        }

        private void buttonPort2Open_Click(object sender, EventArgs e)
        {
            OpenPort(serialPort2, textBoxPort2, textBoxBaudratePort2);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        // Startup timer.
        private void timerStartup_Tick(object sender, EventArgs e)
        {
            timerStartup.Enabled = false;
            OpenPort(serialPort1, textBoxPort1, textBoxBaudratePort1);
            OpenPort(serialPort2, textBoxPort2, textBoxBaudratePort2);
        }

        private static string ok_chars = "+>?-.,= ^_<>";

        private static string escape_of_(string sbuffer, byte[] buffer, int count)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append('[');
            for (int i = 0; i < count; ++i)
            {
                char c = sbuffer[i];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || ok_chars.IndexOf(c)>=0)
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
            }
            sb.Append(']');
            return sb.ToString();
        }

        private static string escape(byte[] buffer, int count)
        {
            string sbuffer = Encoding.ASCII.GetString(buffer, 0, count);
            return escape_of_(sbuffer, buffer, count);
        }

        private delegate void SetTextDelegate();
        /// <summary>
        /// Log a line of text.
        /// </summary>
        /// <param name="source">Textbox with the serial port name.</param>
        /// <param name="text">Text to be logged.</param>
        private void log(string name, string text)
        {
            string s = name + ":" + text + "\r\n";
            if (textBoxLog.InvokeRequired)
            {
                Invoke(new SetTextDelegate(delegate() { textBoxLog.AppendText(s); }));
            }
            else
            {
                textBoxLog.AppendText(s);
            }
        }

#if (false)
        private delegate void UpdateGeotracerDelegate(GeotracerPacket packet);

        private void DoUpdateGeotracer(GeotracerPacket packet)
        {
            ListBox lb = listBoxGeotracerLayout;
            bool updated = false;
            bool changed = false;
            string saddr = packet.Address.ToString();
            string old_value = "<new>";
            for (int i = 0; i < lb.Items.Count; ++i)
            {
                string s = lb.Items[i] as string;
                int eqpos = s.IndexOf('=');
                if (eqpos > 0 && s.Substring(0, eqpos) == saddr)
                {
                    int eqpos2 = s.IndexOf('=', eqpos + 1);
                    int count = int.Parse(s.Substring(eqpos + 1, eqpos2 - eqpos - 1));
                    string payload = s.Substring(eqpos2 + 1);
                    if (payload != packet.Payload)
                    {
                        old_value = payload;
                        changed = true;

                        int new_count = count + 1;
                        string spacket = saddr + "=" + new_count + "=" + packet.Payload;
                        lb.Items[i] = spacket;
                    }

                    updated = true;
                }
            }
            if (!updated)
            {
                lb.Items.Add(saddr + "=0=" + packet.Payload);
                changed = true;
            }
            if (changed)
            {
                textBoxLog.AppendText("Update:" + packet.ToString() + " ( " + old_value + " ) \r\n");
            }
        }

        static int[] Geotracer_Hacked = new int[]{
#if (false)
            130, 131,
            2032, 2483, 2484
#endif
        };
#endif

        private GeotracerPacket packet_last = null;
        private static int[] known_differences = new int[] {
            0x137, 0x138, 0x139, 0x13a, 0x13b, 0x13c,
            0x141, 0x142, 0x143, 0x144, 0x145, 0x146,
        };

        private void DoSerialPortRead(string name, SerialPort port)
        {
            int n = port.BytesToRead;
            if (n > 0)
            {
                byte[] buffer = new byte[n];
                port.Read(buffer, 0, n);
                if (radioButtonProtocolCMRplus.Checked)
                {
                    CMRAssembler assembler = ProtocolCmrAssemblers[name];
                    assembler.Feed(buffer, buffer.Length);
                    for (CMR packet = assembler.Pop(); packet != null; packet = assembler.Pop())
                    {
                        log(name, packet.ToString());
                    }
                }
                else if (radioButtonProtocolGeotracer.Checked)
                {
                    GeotracerAssembler assembler = ProtocolGeotracerAssemblers[name];
                    assembler.Feed(buffer, buffer.Length);
                    for (GeotracerPacket packet = assembler.Pop(); packet != null; packet = assembler.Pop())
                    {
                        if (packet.Type != "CU" && packet.Type != "APOS1" && packet.Type!="AVERSION__" && packet.Type!="ASTAT")
                        {
                            log(name, packet.ToString());
                            if ((packet.Type == GeotracerPacket.AFILEDATA || packet.Type == GeotracerPacket.AWRITE)
                                && packet.Payload != null && packet.Payload.Length > 0)
                            {
                                Invoke(new SetTextDelegate(delegate() { 
                                    int nr = int.Parse(textBoxWriteCount.Text);
                                    string filename = nr.ToString() + "_" + packet.Type + ".txt";
                                    packet.ToFile(filename);
                                    log(name, "Wrote file " + filename);
                                    if (packet.Type == GeotracerPacket.AWRITE)
                                    {
                                        textBoxWriteCount.Text = (nr + 1).ToString();
                                        if (packet_last != null)
                                        {
                                            int len1 = packet.Payload.Length;
                                            int len2 = packet_last.Payload.Length;
                                            int len = len1 < len2 ? len1 : len2;
                                            if (len1 != len2)
                                            {
                                                textBoxDifferences.Text +=
                                                    String.Format("Length was {0}, is {1} now.\r\n", len2, len1);
                                            }
                                            bool differ = false;
                                            for (int i = 0; i < len; ++i)
                                            {
                                                bool is_known = false;
                                                foreach (int offset in known_differences)
                                                {
                                                    if (offset == i)
                                                    {
                                                        is_known = true;
                                                        break;
                                                    }
                                                }
                                                if (is_known)
                                                {
                                                    continue;
                                                }
                                                byte b1 = packet.Payload[i];
                                                byte b2 = packet_last.Payload[i];
                                                if (b1 != b2)
                                                {
                                                    differ = true;
                                                    textBoxDifferences.Text +=
                                                        String.Format("{0:X2}: {1:X2} -> {2:X2}\r\n", i, b2, b1);
                                                }
                                            }
                                            textBoxDifferences.Text += differ ? "Differed\r\n" : "No differences.\r\n";
                                        }
                                        packet_last = packet;
                                    }
                                }));
                            }
                        }
#if (false)
                        bool hacked = false;
                        foreach (int addr in Geotracer_Hacked)
                        {
                            if (packet.Address == addr)
                            {
                                hacked = true;
                            }
                        }
                        if (!hacked)
                        {
                            log(name, packet.ToString());
                        }
                        if (packet.Type == GeotracerPacket.TYPE.DATA || (checkBoxDifferentialWrites.Checked && packet.Type==GeotracerPacket.TYPE.WRITE))
                        {
                            Invoke(new SetTextDelegate(delegate() { this.DoUpdateGeotracer(packet); }));
                        }
#endif
                    }
                }
                else if (radioButtonProtocolNone.Checked)
                {
                    log(name, escape(buffer, buffer.Length));
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DoSerialPortRead("Port1", serialPort1);
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            log("Port1", "Error");
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            DoSerialPortRead("Port2", serialPort2);
        }

        private void serialPort2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            log("Port2", "Error");
        }

        private SaveFileDialog file_save_dlg_ = null;
        private void buttonLogSave_Click(object sender, EventArgs e)
        {
            if (file_save_dlg_ == null)
            {
                file_save_dlg_ = new SaveFileDialog();
                file_save_dlg_.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                file_save_dlg_.RestoreDirectory = true;
            }
            if (file_save_dlg_.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(file_save_dlg_.FileName))
                    {
                        // Add some text to the file.
                        sw.Write(textBoxLog.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }

        private void buttonJoinLines_Click(object sender, EventArgs e)
        {
            string[] lines = (new Regex("\r\n")).Split(textBoxLog.Text);
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();

            for (int lineIndex = 0; lineIndex < lines.Length; )
            {
                // Check what we have.
                string this_line = lines[lineIndex];
                string[] lineparts = this_line.Split(new char[] { ':' });
                if (lineparts.Length >= 2)
                {
                    bool is_port1 = lineparts[0] == "Port1";

                    // Find out next index.
                    int nextIndex;
                    for (nextIndex = lineIndex + 1; nextIndex < lines.Length; ++nextIndex)
                    {
                        string[] next_lineparts = lines[nextIndex].Split(new char[] { ':' });
                        if (next_lineparts.Length >= 2)
                        {
                            bool next_port1 = next_lineparts[0] == "Port1";
                            if (is_port1 != next_port1)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Sum up in the range lineIndex .... nextIndex-1.

                    sb.Length = 0;
                    sb.Append(is_port1 ? "Port1:[" : "Port2:[");
                    for (int index = lineIndex; index < nextIndex; ++index)
                    {
                        string[] lp = lines[index].Split(new char[] { ':' });
                        if (lp.Length >= 2)
                        {
                            string s = lp[1];
                            if (s.Length >= 2 && s[0] == '[' && s[s.Length - 1] == ']')
                            {
                                sb.Append(s.Substring(1, s.Length - 2));
                            }
                            else
                            {
                                sb.Append(s);
                            }
                        }
                    }
                    sb.Append("]");
                    result.Add(sb.ToString());

                    lineIndex = nextIndex;
                }
                else
                {
                    ++lineIndex;
                }
            }

            // Update textBoxLog.
            {
                sb.Length = 0;
                foreach (string s in result)
                {
                    sb.AppendFormat("{0}\r\n", s);
                }
                textBoxLog.Text = sb.ToString();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxLog.Text = "";
        }

        private void buttonGeotracerResetCounts_Click(object sender, EventArgs e)
        {
#if (false)
            ListBox lb = listBoxGeotracerDifferences;
            for (int i = 0; i < lb.Items.Count; ++i)
            {
                string s = lb.Items[i] as string;
                int eqpos = s.IndexOf('=');
                int eqpos2 = s.IndexOf('=', eqpos + 1);
                string saddr = s.Substring(0, eqpos);
                string payload = s.Substring(eqpos2 + 1);
                lb.Items[i] = saddr + "=0=" + payload;
            }
#endif
        }
    }
}