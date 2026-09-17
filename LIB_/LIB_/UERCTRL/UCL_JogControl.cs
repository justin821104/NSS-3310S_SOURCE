using Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.UERCTRL{
    public partial class UCL_JogControl : UserControl{
        int iMT = 0;
        bool bSlowSpd = true;
        bool bMiddleSpd = false;
        bool bHighSpd = false;
        double dJogSpd = 0;
        double dMovePitch = 0;

        public UCL_JogControl(){
            InitializeComponent();
        }

        public string AddMotorLabel { set { lblMotorName.Text = value; } }

        public int AddMotorNumber{
            set{
                JogCcw.TabIndex = value;
                JogCw.TabIndex = value;
                iMT = value;
            }
            get{
                return iMT;
            }
        }

        public eArrow SetBtnImage_Cw { set { JogCw.ImageIndex = (int)value; } }
        public eArrow SetBtnImage_Ccw { set { JogCcw.ImageIndex = (int)value; } }

        public Color StateMotor { set; get; }

        public double CurPosition{
            set { lbCurPOS.Text = value.ToString("0.000"); }
            get { return double.Parse(lbCurPOS.Text); }
        }

        public Color StateLimit_M { set { LIMIT_M.BackColor = value; } }
        public Color StateLimit_P { set { LIMIT_P.BackColor = value; } }
        public Color StateALARM { set { ALARM.BackColor = value; } }
        public Color StateINPOS { set { INPOS.BackColor = value; } }

        private void IncPitch_Click(object sender, EventArgs e){
            Button pBtn = (Button)sender;
            textBoxIncMove.Text = pBtn.Text;
        }

        private void TMR_Tick(object sender, EventArgs e){
            panelSpeedLow.BackColor = bSlowSpd ? Color.Lime : Color.Black;
            panelSpeedMiddle.BackColor = bMiddleSpd ? Color.Lime : Color.Black;
            panelSpeedHigh.BackColor = bHighSpd ? Color.Lime : Color.Black;
        }
        private void SelectSpd_Click(object sender, EventArgs e){
            Button pBtn = (Button)sender;
            if (pBtn.Name == "buttonSpeedLow"){
                bSlowSpd = true;
                bMiddleSpd = false;
                bHighSpd = false;
            }
            if (pBtn.Name == "buttonSpeedMiddle"){
                bSlowSpd = false;
                bMiddleSpd = true;
                bHighSpd = false;
            }
            if (pBtn.Name == "buttonSpeedHigh"){
                bSlowSpd = false;
                bMiddleSpd = false;
                bHighSpd = true;
            }
        }

        bool CheckJog(){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return false;
            if (DATA_.mtSTS[iMT].bAlram || !DATA_.mtSTS[iMT].bSvOn) return false;
            dJogSpd = DATA_.mtSoftData[iMT].MinSpd;
            dMovePitch = Math.Abs(double.Parse(textBoxIncMove.Text));

            if (bSlowSpd) dJogSpd = DATA_.mtSoftData[iMT].JOG_LOW_SPD;
            if (bMiddleSpd) dJogSpd = DATA_.mtSoftData[iMT].JOG_MIDDLE_SPD;
            if (bHighSpd) dJogSpd = DATA_.mtSoftData[iMT].JOG_HIGH_SPD;
            return true;
        }
        private void JogCcw_MouseDown(object sender, MouseEventArgs e){
            if (!CheckJog()) return;
            if (chkIncMove.Checked){
                if (dMovePitch <= 0) return;
                LAB_.MT_PITCH_CCW(iMT, dJogSpd, dMovePitch);
            }
            else{
                LAB_.MT_JOG_CCW(iMT, dJogSpd);
            }
        }

        private void JogCw_MouseDown(object sender, MouseEventArgs e){
            if (!CheckJog()) return;
            if (chkIncMove.Checked){
                if (dMovePitch < 0) return;
                LAB_.MT_PITCH_CW(iMT, dJogSpd, dMovePitch);
            }
            else{
                LAB_.MT_JOG_CW(iMT, dJogSpd);
            }
        }

        private void JogMove_MouesUp(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            if (chkIncMove.Checked) return;

            LAB_.MTSSTOP(iMT, "ucJogControl->JogMove_MouseUp (" + iMT.ToString() + ")");
        }
    }
}