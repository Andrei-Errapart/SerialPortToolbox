#define TOO_MUCH_LOGGING    // 
#undef TOO_MUCH_LOGGING
// choose the one you like above.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace SerialToKeyboard
{
    public partial class FormMain : Form
    {
        public enum CONVERSION_MODE
        {
            HEX_TO_DIGITS,
            PASS_EVERYTHING,
            PASS_WHITELIST,
            /// <summary>
            /// No-name packet structure.
            /// </summary>
            NONAME_PACKET,
        };

        public enum INVALID_INPUT
        {
            IGNORE,
            CHOKE,
            ESCAPE,
        }
        /// <summary>
        /// Command line arguments.
        /// </summary>
        string[] args_;
        /// <summary>
        /// Number of bytes read from serial port.
        /// </summary>
        int bytes_in_ = 0;

        /// <summary>
        /// Previous 4 chars, MSB=latest char, LSB=earliest char.
        /// </summary>
        UInt32 _previous_4chars = 0;

        /// <summary>
        /// Number of nibbles (half-bytes) read from the serial port. 0=high nibble, 1=low nibble.
        /// </summary>
        int nibble_count_ = 0;

        /// <summary>Send queue. This is used to avoid stalls in the serial port data received handler.
        /// Front: pop chars to send (by send timer).
        /// Back: push chars to send (by serial port read callback).
        /// </summary>
        FixedQueue<string> send_queue_ = new FixedQueue<string>(1000);

        /// <summary>
        /// Mode of conversion.
        /// </summary>
        CONVERSION_MODE _ConversionMode = CONVERSION_MODE.PASS_WHITELIST;

        /// <summary>
        /// Whitelist argument of "--accept"
        /// </summary>
        string _PassWhitelist = "0123456789ABCDEFabcdef";

        /// <summary>
        /// Shall we convert invalid input?
        /// </summary>
        INVALID_INPUT _InvalidInputMode = INVALID_INPUT.IGNORE;

        /// <summary>
        /// Serial port input queue.
        /// </summary>
        private StringBuilder sp_queue_ = new StringBuilder();
        /// <summary>
        /// Was any invalid character received?
        /// </summary>
        private bool sp_queue_valid_ = true;

        /// <summary>
        /// Countdown of the number of chars to print when in noname packet mode.
        /// If count .gt. 0: Print the received character.
        /// If count .eq. 0: send the newline.
        /// If count .lt. 0: Do the usual stuff.
        /// </summary>
        int _NonameCountdown = -1;

        private static string specialcode_enter_ = "{ENTER}";
        private static string specialcode_invalid_input_ = "Sobimatu kaart";

        private delegate void SetTextDelegate();

        public FormMain(string[] args)
        {
            args_ = args;
            InitializeComponent();
        }

        private void minimizeToTray()
        {
            // Hide ourselves.
            notifyIconInTray.Visible = true;
            notifyIconInTray.ShowBalloonTip(500);

            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        static void _PushIndividualChars(FixedQueue<string> queue, string s)
        {
            foreach (char c in s)
            {
                queue.PushBack(new string(c, 1));
            }
        }

        void _AppendToOutputOnCondition(byte b, char c, bool is_ok)
        {
            if (is_ok)
            {
                sp_queue_.Append(c);
            }
            switch (_InvalidInputMode)
            {
                case INVALID_INPUT.IGNORE:
                    break;
                case INVALID_INPUT.CHOKE:
                    sp_queue_valid_ = sp_queue_valid_ && is_ok;
                    break;
                case INVALID_INPUT.ESCAPE:
                    if (!is_ok)
                    {
                        sp_queue_.Append("\\x" + b.ToString("X2"));
                    }
                    break;
            }
        }

        // -----------------------------------------------------------------------------------------------------------------------------------
        static int _LengthOfNonameHeader(UInt32 header)
        {
            int length = (int)((header >> 16) & 0xFF);
            return length;
        }

        // -----------------------------------------------------------------------------------------------------------------------------------
        static bool _IsNonameHeaderOk(UInt32 header)
        {
            int length = _LengthOfNonameHeader(header);
            bool r = (header & 0xFF00FFFFU) == 0x000000AA && length>0 && length<128;
            return r;
        }

        // -----------------------------------------------------------------------------------------------------------------------------------
        const string _PermittedCharsHexMode = "0123456789";
        void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int n = serialPort.BytesToRead;
#if (TOO_MUCH_LOGGING)
            _LogToFile("serialPort_DataReceived: Got " + n + " bytes.");
#endif
            if (n > 0)
            {
                // 1. Fetch some bytes.
                byte[] buffer = new byte[n];
                serialPort.Read(buffer, 0, n);
                // 2. Increment the counter.
                bytes_in_ += n;
                if (labelBytesIn.InvokeRequired)
                {
                    this.BeginInvoke(new SetTextDelegate(delegate(){ labelBytesIn.Text = bytes_in_.ToString(); }));
                }
                else
                {
                    labelBytesIn.Text = bytes_in_.ToString();
                }
                // 3. Send the keys to the queue.
                foreach (byte b in buffer)
                {
                    byte previous_char = (byte)(_previous_4chars >> 24);
                    UInt32 next_4chars = (_previous_4chars >> 8) | (((UInt32)b) << 24);
                    if ((_ConversionMode==CONVERSION_MODE.PASS_WHITELIST
                        || _ConversionMode==CONVERSION_MODE.HEX_TO_DIGITS
                        || (_ConversionMode==CONVERSION_MODE.NONAME_PACKET && _NonameCountdown<0)) 
                        && _IsNonameHeaderOk(next_4chars))
                    {
                        if (_ConversionMode != CONVERSION_MODE.NONAME_PACKET)
                        {
                            this.BeginInvoke(new SetTextDelegate(delegate() { labelConversion.Text = "Super card reader"; }));
                            _ConversionMode = CONVERSION_MODE.NONAME_PACKET;
                        }
                        _NonameCountdown = _LengthOfNonameHeader(next_4chars);
                        sp_queue_.Remove(0, sp_queue_.Length);
                    }
                    else if (b == 0x0D || b == 0x0A || b == 0x03)
                    {
                        // Send enter unless previous char was 0x0D or 0x0A.
                        if (previous_char!=0x0D && previous_char!=0x0A && previous_char!=0x03)
                        {
                            // Push the chars into the output queue.
                            if (sp_queue_valid_)
                            { 
                                // there has to be 20 ms. pause between each send attempt.
                                _PushIndividualChars(send_queue_, sp_queue_.ToString());
                                send_queue_.PushBack(specialcode_enter_);
                            }
                            else                                                                
                            {
                                _PushIndividualChars(send_queue_, specialcode_invalid_input_);
                            }
                            // Clear the input queue.
                            if (sp_queue_.Length > 0)
                            {
                                sp_queue_.Remove(0, sp_queue_.Length);
                            }
                            sp_queue_valid_ = true;
                        }
                        nibble_count_ = 0;
                    } else {
                        // Send stuff.
                        switch (_ConversionMode)
                        {
                            case CONVERSION_MODE.HEX_TO_DIGITS:
                                if (nibble_count_ == 1)
                                {
                                    byte bc = (byte)(((previous_char - 0x30) << 4) + (b - 0x30));
                                    char c = (char)bc;
                                    bool is_ok = _PermittedCharsHexMode.IndexOf(c) >= 0;
                                    _AppendToOutputOnCondition(bc, c, is_ok);
                                }
                                nibble_count_ = (nibble_count_ + 1) % 2;
                                break;
                            case CONVERSION_MODE.PASS_WHITELIST:
                                {
                                    char c = (char)b;
                                    bool is_ok = _PassWhitelist.IndexOf(c) >= 0;
                                    _AppendToOutputOnCondition(b, c, is_ok);
                                }
                                break;
                            case CONVERSION_MODE.PASS_EVERYTHING:
                                {
                                    char c = (char)b;
                                    sp_queue_.Append(c);
                                }
                                break;
                            case CONVERSION_MODE.NONAME_PACKET:
                                if (_NonameCountdown > 0)
                                {
                                    char c = (char)b;
                                    sp_queue_.Append(c);
                                }
                                else if (_NonameCountdown == 0)
                                {
                                    // there has to be 20 ms. pause between each send attempt.
                                    _PushIndividualChars(send_queue_, sp_queue_.ToString());
                                    send_queue_.PushBack(specialcode_enter_);
                                }
                                if (_NonameCountdown >= 0)
                                {
                                    --_NonameCountdown;
                                }
                                break;
                        }
                    }
                    _previous_4chars = next_4chars;
                }
            }
#if (TOO_MUCH_LOGGING)
            _LogToFile("serialPort_DataReceived: Finished processing.");
#endif
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool _IsFirstTimerInit = true;
        private void timerInit_Tick(object sender, EventArgs e)
        {
            // 1. Disable timer.
            timerInit.Enabled = false;
#if (TOO_MUCH_LOGGING)
            _LogToFile("timerInit_Tick");
#endif

            // 2. Lookup command line arguments.
            for (int args_index = 0; args_index < args_.Length; ++args_index)
            {
                var s = args_[args_index];
                if (s == "--hexinput")
                {
                    _ConversionMode = CONVERSION_MODE.HEX_TO_DIGITS;
                    labelConversion.Text = "Hex to digits";
                }
                else if (s == "--pass-everything")
                {
                    _ConversionMode = CONVERSION_MODE.PASS_EVERYTHING;
                    labelConversion.Text = "Everything passes";
                }
                else if (s == "--whitelist")
                {
                    _ConversionMode = CONVERSION_MODE.PASS_WHITELIST;
                    if (args_index + 1 < args_.Length)
                    {
                        ++args_index;
                        _PassWhitelist = args_[args_index];
                    }
                    labelConversion.Text = "Whitelist: " + _PassWhitelist;
                }
                else if (s == "--escape-invalid-input")
                {
                    _InvalidInputMode = INVALID_INPUT.ESCAPE;
                    labelInvalidInputHandling.Text = "Escaped";
                }
                else if (s == "--choke-invalid-input")
                {
                    _InvalidInputMode = INVALID_INPUT.CHOKE;
                    labelInvalidInputHandling.Text = "Choked";
                }
                else
                {
                    labelSerialPort.Text = s;
                }
            }

            // 3. Open the port and enable output timer.
            try
            {
                // Start the timer stuff.
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                SerialPortHelper.openSerialPortByDescription(serialPort, labelSerialPort.Text);
                timerSend.Enabled = true;

                minimizeToTray();
#if (TOO_MUCH_LOGGING)
                _LogToFile("timerInit_Tick: Serial port initialized successfully!");
#endif
            }
            catch (Exception ex)
            {
                if (_IsFirstTimerInit)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                    Application.Exit();
                }
                else
                {
#if (TOO_MUCH_LOGGING)
                    _LogToFile("timerInit_Tick: Serial port initialization failure: " + ex.Message);
#endif
                    timerInit.Interval = 1000;
                    timerInit.Enabled = true;
                }
            }

            _IsFirstTimerInit = false;
        }

        private void timerSend_Tick(object sender, EventArgs e)
        {
            // Send keys, if any.
            if (!send_queue_.IsEmpty)
            {
#if (TOO_MUCH_LOGGING)
                _LogToFile("timerSend_Tick: started processing.");
#endif
                var s = send_queue_.PopFront();
                try
                {
                    SendKeys.Send(s);
                }
                catch (Exception)
                {
                    // pass.
                }
#if (TOO_MUCH_LOGGING)
                _LogToFile("timerSend_Tick: finished processing.");
#endif
            }

            // serialPort.BytesToRead will throw exception when Remote Desktop Session has been disconnected.
            // serialPort.IsOpen will not throw.
            if (serialPort.IsOpen)
            {
                try
                {
                    var n = serialPort.BytesToRead;
                    var o = serialPort.IsOpen;
                    ++n;
                    o = o || false;
#if (TOO_MUCH_LOGGING)
                    _LogToFile("timerSend_Tick: BytesToRead=" + n + ", IsOpen=" + o);
#endif
                }
                catch (Exception ex)
                {
#if (TOO_MUCH_LOGGING)
                    _LogToFile("timerSend_Tick: Exception: " + ex.Message);
#endif
                    timerInit.Enabled = true;
                    timerSend.Enabled = false;
                }
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            // FIXME: do what?
            if (FormWindowState.Minimized == this.WindowState)
            {
                minimizeToTray();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIconInTray.Visible = false;
            }
        }

        private void notifyIconInTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

#if (TOO_MUCH_LOGGING)
        void _LogToFile(string msg)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("SerialToKeyboard-log.txt"))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ": " + msg);
                }
            }
            catch (Exception)
            {
                // pass it on this time.
            }
        }
#endif
    }
}