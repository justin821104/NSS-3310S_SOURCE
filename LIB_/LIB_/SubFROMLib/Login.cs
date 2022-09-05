using Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public delegate void EventHandler_OPEN();

    public partial class Login : Form{
        public event EventHandler_OPEN EVENT_OPEN;

        Control ctl = null;
        string sPassWord = string.Empty;

        public Login(){
            InitializeComponent();
        }

        public void INI(){
            DATA_.eLoginLevelBuffer = eLogLevel.Null;
            txtPassWord.Text = string.Empty;
            sPassWord = string.Empty;
            LB_MACHINE_INFO.Text = DATA_.MachineInfo;

            tmrLogin.Enabled = true;
            Show();
            BringToFront();
        }

        private void LogInLevel_CLICK(object sender, EventArgs e){
            ctl = (Button)sender;
            try{
                if (ctl.Name == "btn_Opp")          DATA_.eLoginLevelBuffer = eLogLevel.OP;
                else if (ctl.Name == "btn_Maint")   DATA_.eLoginLevelBuffer = eLogLevel.ENG;
                else if (ctl.Name == "btn_Master")  DATA_.eLoginLevelBuffer = eLogLevel.ADMIN;
                else                                DATA_.eLoginLevelBuffer = eLogLevel.Null;
            }
            catch (Exception ex) { MessageBox.Show("SELECT LOGIN LEVEL CLICK FAIL ! (LoginLevel_Click)" + ETC.NewLine + ex.ToString()); }
        }

        void LoginClick(){
            DATA_.eLoginLevel = DATA_.eLoginLevelBuffer;
            DATA_.eLevelMainSw = eMainLevel.AUTO;
            LogWR_.SaveLogLogin("LOGIN - " + DATA_.eLoginLevel.ToString(), "");
            EVENT_OPEN();
        }

        private void LOGIN_CLICK(object sender, EventArgs e){
            ctl = (Button)sender;
            try{
                if (ctl.Name == "BTN_LOGIN"){
                    sPassWord = txtPassWord.Text;
                    if (DATA_.eLoginLevelBuffer == eLogLevel.OP) LoginClick();
                    else if (DATA_.eLoginLevelBuffer == eLogLevel.ENG){
                        if (DATA_.stPassWord.ENG == sPassWord) LoginClick();
                        else if (DATA_.stPassWord.ENG == null){
                            if ("" == sPassWord) LoginClick();
                        }
                        else MessageBox.Show("Enter enginner mode password again !" + ETC.CrLf + "PASSWORD FAIL !");
                    }
                    else if (DATA_.eLoginLevelBuffer == eLogLevel.ADMIN){
                        if (DATA_.stPassWord.SUP == sPassWord) LoginClick();
                        else if (DATA_.stPassWord.SUP == null){
                            if ("" == sPassWord) LoginClick();
                        }
                        else MessageBox.Show("Enter Master mode password aggin !" + ETC.CrLf + "PASSWORD FAIL !");
                    }
                }
                else if (ctl.Name == "BTN_PasswordChenge"){
                    if (DATA_.eLoginLevelBuffer == eLogLevel.OP || DATA_.eLoginLevelBuffer == eLogLevel.ENG || DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) SUBFRM_.gLogChangePwd.INI(PATH_.PASSWORD);
                    else MessageBox.Show("Select login !");
                }
            }
            catch (Exception ex) { MessageBox.Show("LOGIN CLICK FAIL ! (LOGIN_CLICK)" + ETC.NewLine + ex.ToString()); }
        }

        private void PassWord_KeyPress(object sender, KeyPressEventArgs e){
            if (e.KeyChar != ETC.cspCr) return;
            LOGIN_CLICK(this.BTN_LOGIN, EventArgs.Empty);
        }

        private void TimerLogin_Tick(object sender, EventArgs e){
            btn_Opp.BackColor       = DATA_.eLoginLevelBuffer == eLogLevel.OP ? Color.LightGoldenrodYellow : Color.White;
            btn_Maint.BackColor     = DATA_.eLoginLevelBuffer == eLogLevel.ENG ? Color.LightGoldenrodYellow : Color.White;
            btn_Master.BackColor    = DATA_.eLoginLevelBuffer == eLogLevel.ADMIN ? Color.LightGoldenrodYellow : Color.White;

            if (DATA_.eLoginLevelBuffer == eLogLevel.OP || DATA_.eLoginLevelBuffer == eLogLevel.ENG || DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) BTN_PasswordChenge.Enabled = true;
            else BTN_PasswordChenge.Enabled = false;

            if (DATA_.eLoginLevelBuffer == eLogLevel.ENG || DATA_.eLoginLevelBuffer == eLogLevel.ADMIN) BTN_PasswordChenge.Enabled = true;
            else BTN_PasswordChenge.Enabled = false;
        }
    }
}