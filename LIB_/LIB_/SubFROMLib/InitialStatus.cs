using Object;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LIB_.SubFROMLib{
    public partial class InitialStatus : Form{
        public long sTIME = 0, eTIME = 0;
        private eMachineStatus oldsts;
        double dT = 0;

        public InitialStatus(){
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e){
            tmrInitialStatus.Enabled = false;
            Hide();
        }

        private void InitialStatus_Shown(object sender, EventArgs e){
            gridMotor.RowCount = CNT_.MT;
            for (int i = 0; i < CNT_.MT; i++) gridMotor.Rows[i].Cells[0].Value = DATA_.MtName[i];
        }

        private void InitialStatus_Activated(object sender, EventArgs e){
            editLOG.Text = "";
            tmrInitialStatus.Enabled = true;
            sTIME = Environment.TickCount;
        }

        private void TimerInitialStatus_Tick(object sender, EventArgs e){
            for (int i = 0; i < CNT_.MT; i++) gridMotor.Rows[i].Cells[1].Value = DATA_.mtSTS[i].strHome;
            if (DATA_.eMCStatus == eMachineStatus.INITIAL){
                DATA_.cMATH.GET_CPU_CLOCK(ref eTIME);
                dT = (DATA_.cMATH.TimeMeasure(DATA_.lCPUSpeed, DATA_.lTimeInitialStartTime, eTIME)) / 1000;
                lbInitSts.Text = "INITIALIZING ! [" + string.Format("{0:0.0}", dT) + " sec]";
            }
            if (DATA_.sINIT.Length > 2) lbMsg.Text = DATA_.sINIT; // 초기화 중 발생 메세지 표시
            //editLOG

            if ((DATA_.eMCStatus == eMachineStatus.WAITRUN) && (oldsts == eMachineStatus.INITIAL)){
                tmrInitialStatus.Enabled = false;
                Hide();
            }
            oldsts = DATA_.eMCStatus;

            try{
                if (DATA_.sLOG != null){
                    if (DATA_.sLOG.Length > 2){
                        editLOG.AppendText(DATA_.sLOG); //로그 표시
                        editLOG.SelectionStart = editLOG.Text.Length;
                        editLOG.ScrollToCaret();
                    }
                    DATA_.sLOG = "";
                }
            }
            catch (Exception ex) { LogWR_.SaveLogException("fInitialStatus -> tmrInitialStatus_Tick", ex); }
            if (editLOG.Lines.Count() > 200) editLOG.Text = "";
        }
    }
}