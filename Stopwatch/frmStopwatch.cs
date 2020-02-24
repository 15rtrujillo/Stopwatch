using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Stopwatch
{
    public partial class frmStopwatch : Form
    {
        // Create new stopwatch
        System.Diagnostics.Stopwatch mStopwatch = new System.Diagnostics.Stopwatch();



        public frmStopwatch()
        {
            InitializeComponent();
        }

        private void timrTick_Tick(object sender, EventArgs e)
        {
            lblTime.Text = mStopwatch.Elapsed.ToString("c").Substring(0, 11);

            this.Text = "Stopwatch: " + lblTime.Text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mStopwatch.Start();
            tmrTick.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrTick.Enabled = false;
            // Stop timing
            mStopwatch.Stop();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                mStopwatch.Stop();
                tmrTick.Enabled = false;
                mStopwatch.Reset();

                lblTime.Text = "00:00:00.00";
                this.Text = "Stopwatch";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void frmStopwatch_Load(object sender, EventArgs e)
        {

        }
    }
}
