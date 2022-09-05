using Object;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class ConfirmProcess : Form{
        public Stopwatch timeStamp = Stopwatch.StartNew();
        public bool meLive = false;
        public bool bOptionMeasuree = false;
        public int iMeasureLine = 0;
        int iBZ = 0;

        public ConfirmProcess(){
            InitializeComponent();

            #region "CONTROL EVENT"
            swYes1.Click    += (sender, e) => { YES_(); };
            swNo1.Click     += (sender, e) => { NO_(); };
            swOk.Click      += (sender, e) => { OK_(); };
            #endregion "CONTROL EVENT"
        }

        private void ConfirmProcess_FormClosing(object sender, FormClosingEventArgs e){
            meLive = false;
            DATA_.bWF = false;
            tmrConfirmProcess.Enabled = false;
        }
        private void ConfirmProcess_Activated(object sender, EventArgs e){
            meLive = true;
            if (bOptionMeasuree){
                Width = 525; //525, 370
                Height = 370;
            }
            else{
                Width = 525; //525, 225
                Height = 225;
            }

            lblMESSAGE.Text = DATA_.ConfirmG.msg;
            lbNum.Text = DATA_.ConfirmG.num.ToString("000");
            if (DATA_.ConfirmG.isShowEmptPocket) { }
            if (DATA_.ConfirmG.TypeOk){
                swOk.Visible = true;
                swYes1.Visible = false;
                swNo1.Visible = false;
            }
            else{
                swOk.Visible = false;
                swYes1.Visible = true;
                swNo1.Visible = true;
            }
        }

        public void SET_ACTION(){
            if (DATA_.ConfirmG.bz < 0) return;
            iBZ = DATA_.ConfirmG.bz;
            for (int i = 0; i < DATA_.oBZ.Length; i++) DATA_.mOUT[DATA_.oBZ[i]] = false;
            DATA_.mOUT[iBZ] = true;
            timeStamp.Restart();
            tmrConfirmProcess.Enabled = true;
        }

        public void OffBz(){
            if (DATA_.ConfirmG.bz < 0) return;
            iBZ = DATA_.ConfirmG.bz;
            DATA_.mOUT[iBZ] = false;
            timeStamp.Restart();
        }

        void Click_(){
            DATA_.ConfirmG.useable = false;
            meLive = false;
            DATA_.bWF = false;
            OffBz();
            Hide();
        }

        public void YES_(){
            DATA_.ConfirmG.result = true;
            Click_();
        }
        public void NO_(){
            DATA_.ConfirmG.result = false;
            Click_();
        }
        public void OK_(){
            if (DATA_.eMCStatus == eMachineStatus.WAITRUN && DATA_.ConfirmUser[DATA_.WAR_EndInitial].useable){
                SUBFRM_.gIni.tmrInitialStatus.Enabled = false;
                SUBFRM_.gIni.Hide();
            }
            DATA_.ConfirmG.result = true;
            Click_();
        }

        private void TimerConfirmProcess_Tick(object sender, EventArgs e){
            if (!DATA_.ConfirmG.useable) Hide();
            if (timeStamp.ElapsedMilliseconds > (DATA_.prMACHINE[DATA_.BZOffTime] * 1000)){
                if (DATA_.ConfirmG.AllBzNum != null){
                    for (int i = 0; i < DATA_.ConfirmG.AllBzNum.Length; i++){
                        int iBz = DATA_.ConfirmG.AllBzNum[i];
                        DATA_.mOUT[iBz] = false;
                    }
                }
                tmrConfirmProcess.Enabled = false;
            }
        }
    }
}