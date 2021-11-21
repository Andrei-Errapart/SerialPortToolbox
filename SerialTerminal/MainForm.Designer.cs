namespace SerialTerminal
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonOpenPort = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLineToSend = new System.Windows.Forms.TextBox();
            this.buttonSendLine = new System.Windows.Forms.Button();
            this.radioButtonCR = new System.Windows.Forms.RadioButton();
            this.radioButtonLF = new System.Windows.Forms.RadioButton();
            this.radioButtonCRLF = new System.Windows.Forms.RadioButton();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.timerStartup = new System.Windows.Forms.Timer(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxFullHex = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(379, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(47, 27);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort.TabIndex = 2;
            // 
            // buttonOpenPort
            // 
            this.buttonOpenPort.Location = new System.Drawing.Point(153, 25);
            this.buttonOpenPort.Name = "buttonOpenPort";
            this.buttonOpenPort.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenPort.TabIndex = 3;
            this.buttonOpenPort.Text = "Open port";
            this.buttonOpenPort.UseVisualStyleBackColor = true;
            this.buttonOpenPort.Click += new System.EventHandler(this.buttonOpenPort_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log:";
            // 
            // textBoxLineToSend
            // 
            this.textBoxLineToSend.Location = new System.Drawing.Point(15, 53);
            this.textBoxLineToSend.Name = "textBoxLineToSend";
            this.textBoxLineToSend.Size = new System.Drawing.Size(259, 20);
            this.textBoxLineToSend.TabIndex = 5;
            this.textBoxLineToSend.Text = "RB6000100";
            // 
            // buttonSendLine
            // 
            this.buttonSendLine.Location = new System.Drawing.Point(280, 53);
            this.buttonSendLine.Name = "buttonSendLine";
            this.buttonSendLine.Size = new System.Drawing.Size(75, 23);
            this.buttonSendLine.TabIndex = 6;
            this.buttonSendLine.Text = "Send line";
            this.buttonSendLine.UseVisualStyleBackColor = true;
            this.buttonSendLine.Click += new System.EventHandler(this.buttonSendLine_Click);
            // 
            // radioButtonCR
            // 
            this.radioButtonCR.AutoSize = true;
            this.radioButtonCR.Checked = true;
            this.radioButtonCR.Location = new System.Drawing.Point(234, 28);
            this.radioButtonCR.Name = "radioButtonCR";
            this.radioButtonCR.Size = new System.Drawing.Size(40, 17);
            this.radioButtonCR.TabIndex = 7;
            this.radioButtonCR.TabStop = true;
            this.radioButtonCR.Text = "CR";
            this.radioButtonCR.UseVisualStyleBackColor = true;
            // 
            // radioButtonLF
            // 
            this.radioButtonLF.AutoSize = true;
            this.radioButtonLF.Location = new System.Drawing.Point(280, 28);
            this.radioButtonLF.Name = "radioButtonLF";
            this.radioButtonLF.Size = new System.Drawing.Size(37, 17);
            this.radioButtonLF.TabIndex = 8;
            this.radioButtonLF.Text = "LF";
            this.radioButtonLF.UseVisualStyleBackColor = true;
            // 
            // radioButtonCRLF
            // 
            this.radioButtonCRLF.AutoSize = true;
            this.radioButtonCRLF.Location = new System.Drawing.Point(323, 28);
            this.radioButtonCRLF.Name = "radioButtonCRLF";
            this.radioButtonCRLF.Size = new System.Drawing.Size(52, 17);
            this.radioButtonCRLF.TabIndex = 9;
            this.radioButtonCRLF.Text = "CRLF";
            this.radioButtonCRLF.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(15, 108);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(352, 251);
            this.textBoxLog.TabIndex = 10;
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 4800;
            this.serialPort.Parity = System.IO.Ports.Parity.Space;
            this.serialPort.PortName = "COM5";
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort_ErrorReceived);
            // 
            // timerStartup
            // 
            this.timerStartup.Enabled = true;
            this.timerStartup.Tick += new System.EventHandler(this.timerStartup_Tick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(72, 79);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBoxFullHex
            // 
            this.checkBoxFullHex.AutoSize = true;
            this.checkBoxFullHex.Location = new System.Drawing.Point(153, 83);
            this.checkBoxFullHex.Name = "checkBoxFullHex";
            this.checkBoxFullHex.Size = new System.Drawing.Size(62, 17);
            this.checkBoxFullHex.TabIndex = 12;
            this.checkBoxFullHex.Text = "Full hex";
            this.checkBoxFullHex.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 371);
            this.Controls.Add(this.checkBoxFullHex);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.radioButtonCRLF);
            this.Controls.Add(this.radioButtonLF);
            this.Controls.Add(this.radioButtonCR);
            this.Controls.Add(this.buttonSendLine);
            this.Controls.Add(this.textBoxLineToSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOpenPort);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Serial Terminal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonOpenPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLineToSend;
        private System.Windows.Forms.Button buttonSendLine;
        private System.Windows.Forms.RadioButton radioButtonCR;
        private System.Windows.Forms.RadioButton radioButtonLF;
        private System.Windows.Forms.RadioButton radioButtonCRLF;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Timer timerStartup;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxFullHex;
    }
}

