using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SerialAnalyzer
{
    public partial class BitstreamChart : UserControl
    {
        #region Private variables
        private string filename_ = "";
        private OpenFileDialog file_open_dlg_ = null;
        /// <summary>
        /// Data read from the file...
        /// </summary>
        private int[] data_ = new int[] { };
        private int data_min_ = 100;
        private int data_max_ = 180;
        private int datagrid_stepx_ = 20;
        private int datagrid_stepy_ = 20;
        private int databutton_step_ = 20;

        /// <summary>
        /// First line index.
        /// </summary>
        private int data_first_index_ = 0;
        #endregion

        #region Properties
        public string Filename
        {
            get { return filename_; }
        }
        #endregion

        public BitstreamChart()
        {
            InitializeComponent();
            base.SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint, true
            );
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        /// <summary>
        /// Main function drawing our fancy chart.
        /// </summary>
        /// <param name="pe">Arguments for repaint.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.FillRectangle(System.Drawing.Brushes.Black,
                             new Rectangle(0, 0, Size.Width, Size.Height));

            // Update data_first_index_ if needed.
            if (data_first_index_ + Size.Width > data_.Length)
            {
                data_first_index_ = data_.Length - Size.Width;
            }
            if (data_first_index_ < 0)
            {
                data_first_index_ = 0;
            }

            textBoxFirst.Text = data_first_index_.ToString();
            textBoxLast.Text = (data_first_index_ + Size.Width - 1).ToString();

            // High-Low Y-coordinates.
            int ymax = 0;
            int ymin = Size.Height - 30;

            // Draw grid by datagrid_stepx_ and datagrid_stepy_.
            {
                Pen gridpen = System.Drawing.Pens.Green;

                // horizontal lines.
                int val;
                for (val = (data_min_ / datagrid_stepy_) * datagrid_stepy_;
                     val <= data_max_;
                     val += datagrid_stepy_)
                {
                    if (val >= data_min_)
                    {
                        int y = (val - data_min_) * (ymax - ymin) / (data_max_ - data_min_) + ymin;
                        g.DrawLine(gridpen, 0, y, Size.Width - 1, y);
                    }
                }

                // Vertical lines.
                for (val = (data_first_index_ / datagrid_stepx_) * datagrid_stepx_;
                     val <= data_first_index_ + Size.Width;
                     val += datagrid_stepx_)
                {
                    int x = val - data_first_index_;
                    g.DrawLine(gridpen, x, ymin, x, ymax);
                }
            }

            // Draw data in the grid :)
            {
                Pen datapen = System.Drawing.Pens.Yellow;
                int npoints = Size.Width;
                if (data_first_index_ + npoints > data_.Length)
                {
                    npoints = data_.Length - data_first_index_;
                }
                if (npoints > 0)
                {
                    int y1 = (data_[data_first_index_] - data_min_) * (ymax - ymin) / (data_max_ - data_min_) + ymin;
                    for (int i = 1; i < npoints; ++i)
                    {
                        int y2 = (data_[data_first_index_+i] - data_min_) * (ymax - ymin) / (data_max_ - data_min_) + ymin;
                        g.DrawLine(datapen, i - 1, y1, i, y2);
                        y1 = y2;
                    }
                }
            }
        }

        /// <summary>
        /// Open file using standard "Open file dialogue"...
        /// </summary>
        public bool OpenFileByDialog()
        {
            if (file_open_dlg_ == null)
            {
                file_open_dlg_ = new OpenFileDialog();
                file_open_dlg_.Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*";
                file_open_dlg_.RestoreDirectory = true;
            }
            if (file_open_dlg_.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenFile(file_open_dlg_.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
            return false;
        }

        /// <summary>
        /// Open file given by <ii>filename</ii>
        /// </summary>
        /// <param name="filename">File to open.</param>
        public void OpenFile(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open);
            try
            {
                // Read the file.
                fs.Seek(0x3a, System.IO.SeekOrigin.Begin);
                int nbytes = (int)(fs.Length - fs.Position);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] datax = br.ReadBytes(nbytes);
                data_ = new int[datax.Length];
                for (int i = 0; i < datax.Length; ++i)
                {
                    data_[i] = datax[i];
                }
            }
            finally
            {
                fs.Close();
            }

            // Update some variables...
            filename_ = filename;
            data_first_index_ = 0;
            if (data_.Length > 0)
            {
                data_min_ = data_[0];
                data_max_ = data_[0];
                for (int i = 0; i < data_.Length; ++i)
                {
                    int v = data_[i];
                    if (v > data_max_)
                    {
                        data_max_ = v;
                    }
                    if (v < data_min_)
                    {
                        data_min_ = v;
                    }
                }
            }
            else
            {
                data_min_ = 100;
                data_max_ = 180;
            }
            labelInfo.Text = String.Format("{0} samples in range {1}...{2}.", data_.Length, data_min_, data_max_);
            Invalidate();
        }

        private void UpdateFirstIndex(int index)
        {
            if (index < 0 || data_.Length<=Size.Width)
            {
                data_first_index_ = 0;
            }
            else if (index + Size.Width > data_.Length)
            {
                data_first_index_ = data_.Length - Size.Width;
            }
            else
            {
                data_first_index_ = index;
            }
            Invalidate();
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            UpdateFirstIndex(0);
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            UpdateFirstIndex(data_first_index_ - databutton_step_);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            UpdateFirstIndex(data_first_index_ + databutton_step_);
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            UpdateFirstIndex(data_.Length - Size.Width);
        }

        private void textBoxFirst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateFirstIndex(int.Parse(textBoxFirst.Text));
            }
            catch (Exception)
            {
                // pass
            }
        }
    }
}
