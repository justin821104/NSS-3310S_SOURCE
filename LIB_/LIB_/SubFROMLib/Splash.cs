using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class Splash : Form{
        public Splash(){
            InitializeComponent();
        }

        private void TimerSplash_Tick(object sender, EventArgs e){
            progressBar1.Value = DATA_.SPLASH_PROGRESS;
            lblMsg.Text = DATA_.SPLASH_STATUS;
            if (progressBar1.Value >= 100) tmrSplash.Stop();
        }
    }
}