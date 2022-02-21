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
        private System.Diagnostics.Stopwatch mStopwatch = new System.Diagnostics.Stopwatch();
        private GlobalKeyboardHook keyboardHook;
        // We need this to keep the keyboard hook alive.
        private GlobalKeyboardHook.HookProc hookProc;

        public frmStopwatch()
        {
            InitializeComponent();

            keyboardHook = new GlobalKeyboardHook(new Keys[] { Keys.Space, Keys.R });
            keyboardHook.KeyboardPressed += OnKeyPressed;

            hookProc = keyboardHook.GetHookProc();
        }

        private void StartTimer()
        {
            mStopwatch.Start();
            tmrTick.Enabled = true;
        }

        private void StopTimer()
        {
            tmrTick.Enabled = false;
            mStopwatch.Stop();
        }

        private void ResetTimer()
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

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                // Now you can access both, the key and virtual code
                Keys loggedKey = e.KeyboardData.Key;
                int loggedVkCode = e.KeyboardData.VirtualCode;

                if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
                {
                    if (e.KeyboardData.Key == Keys.Space)
                    {
                        if (tmrTick.Enabled)
                        {
                            StopTimer();
                        }

                        else
                        {
                            StartTimer();
                        }
                    }

                    else if (e.KeyboardData.Key == Keys.R)
                    {
                        ResetTimer();
                    }

                    e.Handled = true;
                }
            }
        }

        private void timrTick_Tick(object sender, EventArgs e)
        {
            lblTime.Text = mStopwatch.Elapsed.ToString("c").Substring(0, 11);

            this.Text = "Stopwatch: " + lblTime.Text;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTimer();
        }

        private void frmStopwatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboardHook?.Dispose();
        }
    }
}
