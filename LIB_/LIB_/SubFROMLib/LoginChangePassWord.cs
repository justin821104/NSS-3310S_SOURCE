using Object;
using System;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class LoginChangePassWord : Form{
        Control ctlSender = null;
        public string pPassword = string.Empty;
        string sOld = string.Empty;
        string sNew = string.Empty;
        string sChk = string.Empty;
        string sOldChk = string.Empty;


        public LoginChangePassWord(){
            InitializeComponent();
        }

        public void INI(string pathPassword){
            BringToFront();
            Width = 487;
            Height = 290;
            if (DATA_.eLoginLevelBuffer == eLogLevel.ENG) lblPassWord.Text = "[ENGINEER] PASSWORD CHANGE";
            if (DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) lblPassWord.Text = "[ADMIN] PASSWORD CHANGE";
            txtOldPassWord.Text = "";
            txtNewPassWord.Text = "";
            txtCheckNewPassWord.Text = "";
            pPassword = pathPassword;
            ShowDialog();
        }

        private void Password_Click(object sender, EventArgs e){
            ctlSender = (Button)sender;
            try{
                if (ctlSender.Name == "btn_Save"){
                    sOld = txtOldPassWord.Text.Trim();
                    sNew = txtNewPassWord.Text.Trim();
                    sChk = txtCheckNewPassWord.Text.Trim();

                    if (DATA_.eLoginLevelBuffer == eLogLevel.ENG) sOldChk = DATA_.stPassWord.ENG;
                    else if (DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) sOldChk = DATA_.stPassWord.SUP;
                    else return;

                    if (sOld == sOldChk){
                        if (sNew == sChk){
                            if (DATA_.eLoginLevelBuffer == eLogLevel.ENG) DATA_.stPassWord.ENG = sNew;
                            if (DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) DATA_.stPassWord.SUP = sNew;
                            TEACH_.WR_LoginPassword();
                            this.Hide();
                            MessageBox.Show("PASSWORD CHANGED SUCCESS !");
                        }
                        else MessageBox.Show("PASSWORD REGISTRATION FAILED !" + ETC.CrLf + "PASSWORD DO NOT MATCH !");
                    }
                    else if (sOld == null || sOld == ""){
                        if (sOldChk == "" || sOldChk == null){
                            if (sNew == sChk)
{
                                if (DATA_.eLoginLevelBuffer == eLogLevel.ENG) DATA_.stPassWord.ENG = sNew;
                                if (DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) DATA_.stPassWord.SUP = sNew;
                                TEACH_.WR_LoginPassword();
                                this.Hide();
                                MessageBox.Show("PASSWORD CHANGED SUCCESS !");
                            }
                            else MessageBox.Show("PASSWORD REGISTRATION FAILED !" + ETC.CrLf + "PASSWORD DO NOT MATCH !");
                        }
                    }
                    else MessageBox.Show("OLD PASSWORD FAIL !" + ETC.CrLf + "PASSWORD DO NOT MATCH !");
                }
                else if (ctlSender.Name == "btn_Exit") this.Hide();
            }
            catch (Exception ex){
                MessageBox.Show("swPasworkd_Click Fail" + ETC.CrLf + ex.ToString());
            }
        }
    }
}