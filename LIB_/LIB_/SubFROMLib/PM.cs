using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class PM : Form{
        public Stopwatch tSTAMP = Stopwatch.StartNew();
        string strElapsedTime = string.Empty;

        public PM(){
            InitializeComponent();
        }

        public void RD_PM(){
            this.ShowDialog();
            tSTAMP.Restart();
        }
        private void TimerPM_Tick(object sender, EventArgs e){
            strElapsedTime = String.Format("{0:00}", tSTAMP.Elapsed.Hours) + ":";
            strElapsedTime += (String.Format("{0:00}", tSTAMP.Elapsed.Minutes) + ":");
            strElapsedTime += (String.Format("{0:00}", tSTAMP.Elapsed.Seconds));
            lbTxtElap.Text = strElapsedTime;
        }
        private void Close_Click(object sender, EventArgs e) { Visible = false; }
        private void PM_Load(object sender, EventArgs e) { tmrPM.Enabled = true; }
    }
}