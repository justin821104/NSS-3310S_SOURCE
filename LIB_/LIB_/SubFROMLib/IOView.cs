using Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class IOView : Form{
        Label mIn_Label, mOut_Label;
        Control ctlsender;
        short mCH_IN, mCH_OUT = 0;
        short nNUM = 0;

        //if (mDATA.eLoginLevel == eLogLevel.OP)    
        //if (mDATA.eLoginLevel == eLogLevel.ENG)   
        //if (mDATA.eLoginLevel == eLogLevel.ADMIN) 
        //if (mDATA.eLoginLevel == eLogLevel.SOFT)  

        public IOView(){
            InitializeComponent();

#if _NSS3300
            rbnIN_7.Visible = true;
            rbnIN_8.Visible = true;
            rbnIN_9.Visible = true;
            rbnIN_10.Visible = true;

            rbnIN_23.Visible = false;
            rbnIN_24.Visible = false;
            rbnIN_25.Visible = false;
            rbnIN_26.Visible = false;

            rbnOUT_6.Visible = true;
            rbnOUT_7.Visible = true;
            rbnOUT_8.Visible = true;
            rbnOUT_9.Visible = true;

            rbnOUT_22.Visible = false;
            rbnOUT_23.Visible = false;
            rbnOUT_24.Visible = false;
            rbnOUT_25.Visible = false;
#else
            rbnIN_7.Visible = false;
            rbnIN_8.Visible = false;
            rbnIN_9.Visible = false;
            rbnIN_10.Visible = false;

            rbnIN_23.Visible = true;
            rbnIN_24.Visible = true;
            rbnIN_25.Visible = true;
            rbnIN_26.Visible = true;

             rbnOUT_6.Visible = false;
            rbnOUT_7.Visible = false;
            rbnOUT_8.Visible = false;
            rbnOUT_9.Visible = false;

            rbnOUT_22.Visible = true;
            rbnOUT_23.Visible = true;
            rbnOUT_24.Visible = true;
            rbnOUT_25.Visible = true;
#endif
        }

        public void INI(){
            tmrIO.Enabled = true;
            Show();
            BringToFront();
        }

        void DEFINE_LABEL(Label L, int i, string s){
            L.BorderStyle = BorderStyle.FixedSingle;
            L.TextAlign = ContentAlignment.MiddleLeft;
            L.Font = new Font("Tahoma", 8, FontStyle.Regular); //Tahoma, 9pt, style=Bold
            L.Tag = i;
            L.Name = s;
            L.Size = new Size(455, 42);
        }

        void BTN_CLICK(object sender, EventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL) return;
            ctlsender = (Label)sender;
            nNUM = (short)(Convert.ToInt16(ctlsender.Tag) + Convert.ToInt16(mCH_OUT * 16));
            SET_OUTPUT(nNUM, !DATA_.mOUT[nNUM]);
        }
        void SET_OUTPUT(short n, bool b){
            if (DATA_.eLoginLevel == eLogLevel.ENG/* || mDATA.eLoginLevel == eLogLevel.ADMIN*/) {}
            else if (DATA_.eLoginLevel == eLogLevel.SOFT || DATA_.eLoginLevel == eLogLevel.ADMIN) {}
            else{
                //UTIL_.PRINT_MASSAGE("" + ETC.NewLine + "엔지니어 레벨 이상만 출력 비트 실행 할 수 없습니다.", false, true, true);
                return;
            }
            LAB_.BIT_OUT(n, b);
        }

        private void IOView_Load(object sender, EventArgs e){
            SET_LABEL();
            GET_LABEL(0, 0);
            GET_LABEL(0, 1);

            rbnIN_0.Checked = true;
            rbnOUT_0.Checked = true;
            tmrIO.Enabled = true;
            lblLABEL.Text = ProductVersion;
        }

        void SET_LABEL(){
            for (short i = 0; i < 16; i++){
                mIn_Label = new Label(){
                    Location = new Point(8, 10 + (49 * i)),
                    ForeColor = Color.Black
                };
                DEFINE_LABEL(mIn_Label, i, "LABEL" + i);
                pnlIN.Controls.Add(mIn_Label);

                mOut_Label = new Label(){
                    Location = new Point(8, 10 + (49 * i)),
                    ForeColor = Color.Black
                };
                DEFINE_LABEL(mOut_Label, i, "LABEL" + i);
                pnlOUT.Controls.Add(mOut_Label);
                mOut_Label.Click += new EventHandler(BTN_CLICK);
            }
        }

        void GET_LABEL(short i, short j){
            if (j == 0){
                mCH_IN = i;
                foreach (Control mControl in pnlIN.Controls){
                    //    if (mControl.GetType().Name == "Label") mControl.Text = mDATA.InputName[Convert.ToInt16(mControl.Tag) + (i * 16)] == null ? "NOTHING" : mDATA.InputName[Convert.ToInt16(mControl.Tag) + (i * 16)];
                    if (mControl.GetType().Name == "Label") mControl.Text = DATA_.InputName[Convert.ToInt16(mControl.Tag) + (i * 16)] ?? "NOTHING";
                }
            }
            else{
                mCH_OUT = i;
                foreach (Control mControl in pnlOUT.Controls){
                    if (mControl.GetType().Name == "Label") mControl.Text = DATA_.OutputName[Convert.ToInt16(mControl.Tag) + (i * 16)] ?? "NOTHING";
                }
            }
        }

        private void TimerIO_Tick(object sender, EventArgs e){
            foreach (Control mControl in pnlIN.Controls){
                if (mControl.GetType().Name == "Label") mControl.BackColor = (DATA_.mIN[Convert.ToInt16(mControl.Tag) + (mCH_IN * 16)] ? /*Color.Aquamarine*/Color.Lime : Color.White/*Color.FromArgb(0, 0, 60)*/);
            }

            foreach (Control mControl in pnlOUT.Controls){
                if (mControl.GetType().Name == "Label") mControl.BackColor = (DATA_.mOUT[Convert.ToInt16(mControl.Tag) + (mCH_OUT * 16)] ? /*Color.LightSalmon*/Color.Red : Color.White/*Color.FromArgb(0, 0, 60)*/);
            }
        }

        private void OUT_0_CheckedChanged(object sender, EventArgs e){
            ctlsender = (RadioButton)sender;
            GET_LABEL(Convert.ToInt16(ctlsender.TabIndex), 1);
        }
        private void IN_0_CheckedChanged(object sender, EventArgs e){
            ctlsender = (RadioButton)sender;
            GET_LABEL(Convert.ToInt16(ctlsender.TabIndex), 0);
        }
    }
}