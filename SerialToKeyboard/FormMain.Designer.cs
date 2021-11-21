namespace SerialToKeyboard
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelBytesIn = new System.Windows.Forms.Label();
            this.labelSerialPort = new System.Windows.Forms.Label();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.notifyIconInTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.labelConversion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelInvalidInputHandling = new System.Windows.Forms.Label();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(303, 24);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Serial port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bytes in:";
            // 
            // labelBytesIn
            // 
            this.labelBytesIn.AutoSize = true;
            this.labelBytesIn.Location = new System.Drawing.Point(89, 119);
            this.labelBytesIn.Name = "labelBytesIn";
            this.labelBytesIn.Size = new System.Drawing.Size(13, 13);
            this.labelBytesIn.TabIndex = 5;
            this.labelBytesIn.Text = "0";
            // 
            // labelSerialPort
            // 
            this.labelSerialPort.AutoSize = true;
            this.labelSerialPort.Location = new System.Drawing.Point(89, 43);
            this.labelSerialPort.Name = "labelSerialPort";
            this.labelSerialPort.Size = new System.Drawing.Size(64, 13);
            this.labelSerialPort.TabIndex = 6;
            this.labelSerialPort.Text = "COM1,4800";
            // 
            // timerInit
            // 
            this.timerInit.Enabled = true;
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // timerSend
            // 
            this.timerSend.Interval = 20;
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // notifyIconInTray
            // 
            this.notifyIconInTray.BalloonTipText = "Double click to restore";
            this.notifyIconInTray.BalloonTipTitle = "Serial To Keyboard";
            this.notifyIconInTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconInTray.Icon")));
            this.notifyIconInTray.Text = "Serial To Keyboard";
            this.notifyIconInTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconInTray_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Conversion:";
            // 
            // labelConversion
            // 
            this.labelConversion.AutoSize = true;
            this.labelConversion.Location = new System.Drawing.Point(89, 71);
            this.labelConversion.Name = "labelConversion";
            this.labelConversion.Size = new System.Drawing.Size(33, 13);
            this.labelConversion.TabIndex = 8;
            this.labelConversion.Text = "None";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Invalid input:";
            // 
            // labelInvalidInputHandling
            // 
            this.labelInvalidInputHandling.AutoSize = true;
            this.labelInvalidInputHandling.Location = new System.Drawing.Point(89, 95);
            this.labelInvalidInputHandling.Name = "labelInvalidInputHandling";
            this.labelInvalidInputHandling.Size = new System.Drawing.Size(43, 13);
            this.labelInvalidInputHandling.TabIndex = 10;
            this.labelInvalidInputHandling.Text = "Ignored";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 145);
            this.Controls.Add(this.labelInvalidInputHandling);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelConversion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSerialPort);
            this.Controls.Add(this.labelBytesIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.Text = "Serial to Keyboard";
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBytesIn;
        private System.Windows.Forms.Label labelSerialPort;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.NotifyIcon notifyIconInTray;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelConversion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelInvalidInputHandling;
    }
}

