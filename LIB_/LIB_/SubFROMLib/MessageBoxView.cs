using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class MessageBoxView : Form{
        Control ctlSender = null;
        public bool bRESULT = false;
        public bool bDEFAULT = false;

        public MessageBoxView(){
            InitializeComponent();
        }

        public void RD_MESSGEBOX(){
            ShowDialog();
            if (bDEFAULT) swYes.Focus();
            else swNo.Focus();
        }

        private void TimerMassageBox_Tick(object sender, EventArgs e){

        }

        private void Msg_Click(object sender, EventArgs e){
            ctlSender = (Button)sender;
            if (ctlSender.Name == "swYes") this.DialogResult = DialogResult.Yes;
            else if (ctlSender.Name == "swOk") this.DialogResult = DialogResult.OK;
            else if (ctlSender.Name == "swNo") this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void MassageBox_Load(object sender, EventArgs e){
            Top = 1;
            Left = 1;
        }
    }
}