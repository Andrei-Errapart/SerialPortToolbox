namespace SerialProtocolLogger
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxBaudratePort1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPort1Open = new System.Windows.Forms.Button();
            this.textBoxPort1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBaudratePort2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonPort2Open = new System.Windows.Forms.Button();
            this.textBoxPort2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxDifferentialWrites = new System.Windows.Forms.CheckBox();
            this.buttonGeotracerResetCounts = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonJoinLines = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonLogSave = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.timerStartup = new System.Windows.Forms.Timer(this.components);
            this.groupBoxProtocol = new System.Windows.Forms.GroupBox();
            this.radioButtonProtocolGeotracer = new System.Windows.Forms.RadioButton();
            this.radioButtonProtocolCMRplus = new System.Windows.Forms.RadioButton();
            this.radioButtonProtocolNone = new System.Windows.Forms.RadioButton();
            this.textBoxWriteCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDifferences = new System.Windows.Forms.TextBox();
            this.menuStripMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxProtocol.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(842, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
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
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxBaudratePort1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonPort1Open);
            this.groupBox1.Controls.Add(this.textBoxPort1);
            this.groupBox1.Location = new System.Drawing.Point(0, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port 1";
            // 
            // textBoxBaudratePort1
            // 
            this.textBoxBaudratePort1.Location = new System.Drawing.Point(6, 51);
            this.textBoxBaudratePort1.Name = "textBoxBaudratePort1";
            this.textBoxBaudratePort1.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaudratePort1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "bps.";
            // 
            // buttonPort1Open
            // 
            this.buttonPort1Open.Location = new System.Drawing.Point(112, 17);
            this.buttonPort1Open.Name = "buttonPort1Open";
            this.buttonPort1Open.Size = new System.Drawing.Size(75, 23);
            this.buttonPort1Open.TabIndex = 1;
            this.buttonPort1Open.Text = "Open";
            this.buttonPort1Open.UseVisualStyleBackColor = true;
            this.buttonPort1Open.Click += new System.EventHandler(this.buttonPort1Open_Click);
            // 
            // textBoxPort1
            // 
            this.textBoxPort1.Location = new System.Drawing.Point(6, 19);
            this.textBoxPort1.Name = "textBoxPort1";
            this.textBoxPort1.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBaudratePort2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonPort2Open);
            this.groupBox2.Controls.Add(this.textBoxPort2);
            this.groupBox2.Location = new System.Drawing.Point(200, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port 2";
            // 
            // textBoxBaudratePort2
            // 
            this.textBoxBaudratePort2.Location = new System.Drawing.Point(6, 51);
            this.textBoxBaudratePort2.Name = "textBoxBaudratePort2";
            this.textBoxBaudratePort2.Size = new System.Drawing.Size(100, 20);
            this.textBoxBaudratePort2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "bps.";
            // 
            // buttonPort2Open
            // 
            this.buttonPort2Open.Location = new System.Drawing.Point(112, 17);
            this.buttonPort2Open.Name = "buttonPort2Open";
            this.buttonPort2Open.Size = new System.Drawing.Size(75, 23);
            this.buttonPort2Open.TabIndex = 1;
            this.buttonPort2Open.Text = "Open";
            this.buttonPort2Open.UseVisualStyleBackColor = true;
            this.buttonPort2Open.Click += new System.EventHandler(this.buttonPort2Open_Click);
            // 
            // textBoxPort2
            // 
            this.textBoxPort2.Location = new System.Drawing.Point(6, 19);
            this.textBoxPort2.Name = "textBoxPort2";
            this.textBoxPort2.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textBoxDifferences);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBoxWriteCount);
            this.groupBox3.Controls.Add(this.checkBoxDifferentialWrites);
            this.groupBox3.Controls.Add(this.buttonGeotracerResetCounts);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttonClear);
            this.groupBox3.Controls.Add(this.buttonJoinLines);
            this.groupBox3.Controls.Add(this.textBoxLog);
            this.groupBox3.Controls.Add(this.buttonLogSave);
            this.groupBox3.Location = new System.Drawing.Point(0, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(842, 441);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Communications log";
            // 
            // checkBoxDifferentialWrites
            // 
            this.checkBoxDifferentialWrites.AutoSize = true;
            this.checkBoxDifferentialWrites.Location = new System.Drawing.Point(471, 23);
            this.checkBoxDifferentialWrites.Name = "checkBoxDifferentialWrites";
            this.checkBoxDifferentialWrites.Size = new System.Drawing.Size(78, 17);
            this.checkBoxDifferentialWrites.TabIndex = 7;
            this.checkBoxDifferentialWrites.Text = "Diff. Writes";
            this.checkBoxDifferentialWrites.UseVisualStyleBackColor = true;
            // 
            // buttonGeotracerResetCounts
            // 
            this.buttonGeotracerResetCounts.Location = new System.Drawing.Point(648, 21);
            this.buttonGeotracerResetCounts.Name = "buttonGeotracerResetCounts";
            this.buttonGeotracerResetCounts.Size = new System.Drawing.Size(75, 23);
            this.buttonGeotracerResetCounts.TabIndex = 6;
            this.buttonGeotracerResetCounts.Text = "Reset counts";
            this.buttonGeotracerResetCounts.UseVisualStyleBackColor = true;
            this.buttonGeotracerResetCounts.Click += new System.EventHandler(this.buttonGeotracerResetCounts_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(557, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Geotracer layout";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(174, 19);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonJoinLines
            // 
            this.buttonJoinLines.Location = new System.Drawing.Point(93, 19);
            this.buttonJoinLines.Name = "buttonJoinLines";
            this.buttonJoinLines.Size = new System.Drawing.Size(75, 23);
            this.buttonJoinLines.TabIndex = 2;
            this.buttonJoinLines.Text = "Join lines";
            this.buttonJoinLines.UseVisualStyleBackColor = true;
            this.buttonJoinLines.Click += new System.EventHandler(this.buttonJoinLines_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxLog.Location = new System.Drawing.Point(6, 48);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(717, 387);
            this.textBoxLog.TabIndex = 1;
            // 
            // buttonLogSave
            // 
            this.buttonLogSave.Location = new System.Drawing.Point(12, 19);
            this.buttonLogSave.Name = "buttonLogSave";
            this.buttonLogSave.Size = new System.Drawing.Size(75, 23);
            this.buttonLogSave.TabIndex = 0;
            this.buttonLogSave.Text = "Save";
            this.buttonLogSave.UseVisualStyleBackColor = true;
            this.buttonLogSave.Click += new System.EventHandler(this.buttonLogSave_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 4800;
            this.serialPort1.ReadTimeout = 100;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            this.serialPort1.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort1_ErrorReceived);
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 4800;
            this.serialPort2.PortName = "COM2";
            this.serialPort2.ReadTimeout = 100;
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            this.serialPort2.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.serialPort2_ErrorReceived);
            // 
            // timerStartup
            // 
            this.timerStartup.Enabled = true;
            this.timerStartup.Tick += new System.EventHandler(this.timerStartup_Tick);
            // 
            // groupBoxProtocol
            // 
            this.groupBoxProtocol.Controls.Add(this.radioButtonProtocolGeotracer);
            this.groupBoxProtocol.Controls.Add(this.radioButtonProtocolCMRplus);
            this.groupBoxProtocol.Controls.Add(this.radioButtonProtocolNone);
            this.groupBoxProtocol.Location = new System.Drawing.Point(404, 27);
            this.groupBoxProtocol.Name = "groupBoxProtocol";
            this.groupBoxProtocol.Size = new System.Drawing.Size(235, 51);
            this.groupBoxProtocol.TabIndex = 4;
            this.groupBoxProtocol.TabStop = false;
            this.groupBoxProtocol.Text = "Protocol Selection";
            // 
            // radioButtonProtocolGeotracer
            // 
            this.radioButtonProtocolGeotracer.AutoSize = true;
            this.radioButtonProtocolGeotracer.Checked = true;
            this.radioButtonProtocolGeotracer.Location = new System.Drawing.Point(150, 20);
            this.radioButtonProtocolGeotracer.Name = "radioButtonProtocolGeotracer";
            this.radioButtonProtocolGeotracer.Size = new System.Drawing.Size(72, 17);
            this.radioButtonProtocolGeotracer.TabIndex = 2;
            this.radioButtonProtocolGeotracer.TabStop = true;
            this.radioButtonProtocolGeotracer.Text = "Geotracer";
            this.radioButtonProtocolGeotracer.UseVisualStyleBackColor = true;
            // 
            // radioButtonProtocolCMRplus
            // 
            this.radioButtonProtocolCMRplus.AutoSize = true;
            this.radioButtonProtocolCMRplus.Location = new System.Drawing.Point(80, 20);
            this.radioButtonProtocolCMRplus.Name = "radioButtonProtocolCMRplus";
            this.radioButtonProtocolCMRplus.Size = new System.Drawing.Size(55, 17);
            this.radioButtonProtocolCMRplus.TabIndex = 1;
            this.radioButtonProtocolCMRplus.Text = "CMR+";
            this.radioButtonProtocolCMRplus.UseVisualStyleBackColor = true;
            // 
            // radioButtonProtocolNone
            // 
            this.radioButtonProtocolNone.AutoSize = true;
            this.radioButtonProtocolNone.Location = new System.Drawing.Point(6, 20);
            this.radioButtonProtocolNone.Name = "radioButtonProtocolNone";
            this.radioButtonProtocolNone.Size = new System.Drawing.Size(68, 17);
            this.radioButtonProtocolNone.TabIndex = 0;
            this.radioButtonProtocolNone.Text = "Plain hex";
            this.radioButtonProtocolNone.UseVisualStyleBackColor = true;
            // 
            // textBoxWriteCount
            // 
            this.textBoxWriteCount.Location = new System.Drawing.Point(334, 21);
            this.textBoxWriteCount.Name = "textBoxWriteCount";
            this.textBoxWriteCount.Size = new System.Drawing.Size(37, 20);
            this.textBoxWriteCount.TabIndex = 8;
            this.textBoxWriteCount.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "WriteCount:";
            // 
            // textBoxDifferences
            // 
            this.textBoxDifferences.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDifferences.Location = new System.Drawing.Point(729, 48);
            this.textBoxDifferences.Multiline = true;
            this.textBoxDifferences.Name = "textBoxDifferences";
            this.textBoxDifferences.Size = new System.Drawing.Size(107, 387);
            this.textBoxDifferences.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 555);
            this.Controls.Add(this.groupBoxProtocol);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "Serial protocol logger";
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxProtocol.ResumeLayout(false);
            this.groupBoxProtocol.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPort1Open;
        private System.Windows.Forms.TextBox textBoxPort1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPort2;
        private System.Windows.Forms.Button buttonPort2Open;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonLogSave;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Timer timerStartup;
        private System.Windows.Forms.Button buttonJoinLines;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBoxProtocol;
        private System.Windows.Forms.RadioButton radioButtonProtocolCMRplus;
        private System.Windows.Forms.RadioButton radioButtonProtocolNone;
        private System.Windows.Forms.RadioButton radioButtonProtocolGeotracer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGeotracerResetCounts;
        private System.Windows.Forms.CheckBox checkBoxDifferentialWrites;
        private System.Windows.Forms.TextBox textBoxBaudratePort1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBaudratePort2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxWriteCount;
        private System.Windows.Forms.TextBox textBoxDifferences;
    }
}

