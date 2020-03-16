using System;
using System.Timers;
using System.Windows.Forms;

namespace BatteryNotifier {
    public partial class Form1 : Form {
        public const int HUNDRED = 100;

        System.Timers.Timer timer = new System.Timers.Timer();
        float percentage;
        bool notified = false;

        public Form1() {
            InitializeComponent();

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 1000;
            timer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show(percentageChecker(), "Battery Notifier");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e) {
            if (percentageChecker().Equals("100") && SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online && !notified) {
                notifyIcon1.ShowBalloonTip(1000, "Battery Notifier", "Your battery has fully charged.", ToolTipIcon.Info);
                notified = true;
            }
            if(!(percentageChecker().Equals("100")) || SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Online)
            {
                notified = false;
            }
        }
        
        private string percentageChecker() {
            percentage = HUNDRED * SystemInformation.PowerStatus.BatteryLifePercent;
            return percentage.ToString();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.Show();
        }
        
        private void showToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            functionsExecutesOnExit();
        }
        
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            functionsExecutesOnExit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.WindowsShutDown || 
                e.CloseReason == CloseReason.TaskManagerClosing || 
                e.CloseReason == CloseReason.ApplicationExitCall ) return;
            e.Cancel = true;
            this.Hide();
        }

        private void functionsExecutesOnExit() {
            Application.Exit();
        }
    }
}