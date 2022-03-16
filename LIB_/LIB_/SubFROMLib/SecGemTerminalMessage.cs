using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class SecGemTerminalMessage : Form{
        public bool bVIEW = false;
        public string sMESSAGE = "";
        public string sMESSAGE_1 = "";
        public SecGemTerminalMessage(){
            InitializeComponent();
            lbTitle.Text    = "";
            lbTitle1.Text   = "";
        }

        private void btnClose_Click(object sender, System.EventArgs e){
            bVIEW = false;
            this.Hide();
        }

        public void INI(){
            lbTitle.Text    = sMESSAGE;
            lbTitle1.Text   = sMESSAGE_1;
            Top = 880;

            if (!bVIEW){
                Show();
                BringToFront();
                bVIEW = false;
            }
        }

        private void SecGemTerminalMessage_FormClosed(object sender, FormClosedEventArgs e) { bVIEW = false; }

        private void lbTitle1_DoubleClick(object sender, System.EventArgs e){
            sMESSAGE_1 = "";
            lbTitle1.Text = sMESSAGE_1;
        }

        private void lbTitle_DoubleClick(object sender, System.EventArgs e){
            sMESSAGE = "";
            lbTitle.Text = sMESSAGE;
        }
    }
}