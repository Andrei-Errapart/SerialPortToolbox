using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SerialAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create new MainForm...
        /// </summary>
        /// <param name="args">Command-line parameters.</param>
        public MainForm(string[] args)
        {
            InitializeComponent();
            if (args.Length>0)
            {
                try
                {
                    bitstreamChart.OpenFile(args[0]);
                    Text = String.Format("Serial Analyzer - {0}", bitstreamChart.Filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bitstreamChart.OpenFileByDialog())
            {
                Text = String.Format("Serial Analyzer - {0}", bitstreamChart.Filename);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog(this);
        }
    }
}