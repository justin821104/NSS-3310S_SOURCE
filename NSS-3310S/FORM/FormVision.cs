using Object;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NSS_3310S{
    public partial class FormVision : Form{
        public double dRefCurPos_X = 0, dRefCurPos_Y = 0;
        Button btn = null;
        Label lbl = null;
        bool bOption = false;
        double dValue = 0.0;

        public FormVision(){
            InitializeComponent();

            tbxResolution.TabIndex = CP.PreCamResolution;
            bLedBright_0.TabIndex = RP.PreAlignLight;

            Pre_X1.TabIndex = M.StripPkX;
            Pre_X2.TabIndex = M.StripPkX;
            Pre_Z1.TabIndex = M.StripPkZ;
            Pre_Z2.TabIndex = M.StripPkZ;
#if _NSS3300
#else
            Pre_Y1.TabIndex = M.PreAlign;
            Pre_Y2.TabIndex = M.PreAlign;
            bMTY_CW.TabIndex = M.PreAlign;
            bMTY_CCW.TabIndex = M.PreAlign;
#endif

            bMTX_CW.TabIndex = M.StripPkX;
            bMX_Ccw.TabIndex = M.StripPkX;
            bMTZ_Cw.TabIndex = M.StripPkZ;
            bMTZ_Ccw.TabIndex = M.StripPkZ;

            btnSetRef_X.Click += (sender, e) => SetReferencePos(btnSetRef_X);
            btnSetRef_Y.Click += (sender, e) => SetReferencePos(btnSetRef_Y);

            GetX_1.Click += (sender, e) => GetPos(GetX_1);
            GetY_1.Click += (sender, e) => GetPos(GetY_1);
            GetZ_1.Click += (sender, e) => GetPos(GetZ_1);

            GetX_2.Click += (sender, e) => GetPos(GetX_2);
            GetY_2.Click += (sender, e) => GetPos(GetY_2);
            GetZ_2.Click += (sender, e) => GetPos(GetZ_2);

            Reset_1.Click += (sender, e) => ResetPos(Reset_1);
            Reset_2.Click += (sender, e) => ResetPos(Reset_2);

            GoInsp_1.Click += (sender, e) => GoPos(GoInsp_1);
            GoInsp_2.Click += (sender, e) => GoPos(GoInsp_2);

            tLedBright_0.ValueChanged += (sender, e) => tSendLight(tLedBright_0);
            bLedBright_0.ValueChanged += (sender, e) => bSendLight(bLedBright_0);

            btnSave.Click += (sender, e) => Save(btnSave);
        }

        public void Initailize_View(){
            UpdataDate();
            bSendLight(bLedBright_0);

            TmrVISION.Enabled = true;
            Show();
            BringToFront();
        }

        public void Set_Language(){

        }

        void SetReferencePos(object sender){
            btn = (Button)sender;
            if (btn.Name == "btnSetRef_X") dRefCurPos_X = LAB_.GET_ACTPOS(M.StripPkX);
#if _NSS3300
#else
                if (btn.Name == "btnSetRef_Y") dRefCurPos_Y = LAB_.GET_ACTPOS(M.PreAlign);
#endif
        }
        void GetPos(object sender){
            btn = (Button)sender;
            if (btn.Name == "GetX_1") Pre_X1.Text = LAB_.GET_ACTPOS(M.StripPkX).ToString();
            if (btn.Name == "GetX_2") Pre_X2.Text = LAB_.GET_ACTPOS(M.StripPkX).ToString();
#if _NSS3300
#else
            if (btn.Name == "GetY_1") Pre_Y1.Text = LAB_.GET_ACTPOS(M.PreAlign).ToString();
            if (btn.Name == "GetY_2") Pre_Y2.Text = LAB_.GET_ACTPOS(M.PreAlign).ToString();
#endif
            if (btn.Name == "GetZ_1") Pre_Z1.Text = LAB_.GET_ACTPOS(M.StripPkZ).ToString();
            if (btn.Name == "GetZ_2") Pre_Z2.Text = LAB_.GET_ACTPOS(M.StripPkZ).ToString();
        }
        void ResetPos(object sender){
            btn = (Button)sender;
            if (btn.Name == "Reset_1"){
                Pre_X1.Text = DATA_.mtDATA[M.StripPkX, P.FirstTrigger].Pos.ToString();
#if _NSS3300
#else
                Pre_Y1.Text = DATA_.mtDATA[M.PreAlign, P.StripTrigger1].Pos.ToString();
#endif
                Pre_Z1.Text = DATA_.mtDATA[M.StripPkZ, P.FirstTrigger].Pos.ToString();
            }
            if (btn.Name == "Reset_2"){
                Pre_X2.Text = DATA_.mtDATA[M.StripPkX, P.SecondTrigger].Pos.ToString();
#if _NSS3300
#else
                Pre_Y2.Text = DATA_.mtDATA[M.PreAlign, P.StripTrigger2].Pos.ToString();
#endif
                Pre_Z2.Text = DATA_.mtDATA[M.StripPkZ, P.SecondTrigger].Pos.ToString();
            }
        }
        void GoPos(object sender){
            btn = (Button)sender;
            if (btn.Name == "GoInsp_1"){
                if (DialogResult.OK == MessageBox.Show("프리 얼라인 첫번째 위치 이송 ?", "Select", MessageBoxButtons.OKCancel)){

                }
            }
            if (btn.Name == "GoInsp_2"){
                if (DialogResult.OK == MessageBox.Show("프리 얼라인 두번째 위치 이송 ?", "Select", MessageBoxButtons.OKCancel)){

                }
            }
        }

        public void SetAsyncTrackCtrl(int nCh, int nValue){
            if (!(Controls.Find("tLedBright_" + nCh, true).FirstOrDefault() is TrackBar pTrack)) return;
            pTrack.Value = nValue;
        }
        public void SetAsyncNmCtrl(int nCh, int nValue){
            if (!(Controls.Find("bLedBright_" + nCh, true).FirstOrDefault() is NumericUpDown pNm)) return;
            pNm.Value = nValue;
        }
        public void tSendLight(object sender){
            TrackBar tLight = sender as TrackBar;
            if (int.TryParse(tLight.Tag as string, out int nCh)){
                SetAsyncNmCtrl(nCh, tLight.Value);
            }
        }
        public void bSendLight(object sender){
            NumericUpDown nLight = sender as NumericUpDown;
            if (int.TryParse(nLight.Tag as string, out int nCh)){
                SetAsyncTrackCtrl(nCh, (int)nLight.Value);
                DATA_.cLightController.SetLight(nCh, (int)nLight.Value);
            }
        }

        void Save(object sender){
            btn = (Button)sender;
            if (btn.Name == "btnSave"){
                if (DialogResult.OK == MessageBox.Show("데이터 값 저장 하시겠습니까 ?", "Select", MessageBoxButtons.OKCancel)){
                    TEACH_.SaveMotorPos(M.StripPkX, P.FirstTrigger, double.Parse(Pre_X1.Text));
                    TEACH_.SaveMotorPos(M.StripPkX, P.SecondTrigger, double.Parse(Pre_X2.Text));
#if _NSS3300
#else
                    TEACH_.SaveMotorPos(M.PreAlign, P.StripTrigger1, double.Parse(Pre_Y1.Text));
                    TEACH_.SaveMotorPos(M.PreAlign, P.StripTrigger2, double.Parse(Pre_Y2.Text));
#endif
                    TEACH_.SaveMotorPos(M.StripPkZ, P.FirstTrigger, double.Parse(Pre_Z1.Text));
                    TEACH_.SaveMotorPos(M.StripPkZ, P.SecondTrigger, double.Parse(Pre_Z2.Text));

                    TEACH_.Write_Parameter(tbxResolution);
                    TEACH_.Write_ModelPara(RP.PreAlignLight, (double)bLedBright_0.Value);
                    UpdataDate();
                }
            }
        }

        private void ParaValue_DoubleClick(object sender, EventArgs e){
            lbl = (Label)sender;
            bOption = true;
            if (lbl.Tag.ToString() == "MC" || lbl.Tag.ToString() == "mc") DATA_.IsSTRING[S.VisionRecipeList] = DATA_.MCParaName[lbl.TabIndex];
            else DATA_.IsSTRING[S.VisionRecipeList] = DATA_.MDParaName[lbl.TabIndex];

            lbl.BackColor = Color.Lime;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.VisionRecipeList], ref lbl, bOption);
            lbl.BackColor = Color.White;
        }
        private void Value_DoubleClick(object sender, EventArgs e){
            lbl = (Label)sender;
            bOption = true;
            lbl.BackColor = Color.Lime;
            UTIL_.OPEN_KEYPAD_LABEL("", ref lbl, bOption);
            lbl.BackColor = Color.White;
        }
        private void MTPosValue_DoubleClick(object sender, EventArgs e){
            lbl = (Label)sender;
            bOption = true;
            DATA_.IsSTRING[S.VisionRecipeList] = DATA_.MtName[lbl.TabIndex] + " 위치 값";

            lbl.BackColor = Color.Lime;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.VisionRecipeList], ref lbl, bOption);
            lbl.BackColor = Color.White;
        }

        private void MotorStop_MouseUp(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF || CHK_USE_INC.Checked) return;
            btn = (Button)sender;
            LAB_.MTSSTOP(btn.TabIndex, "FormVISION -> MouseUp_Click");
        }

        double GetJogSpeed(int m){
            double dSpd = 0;
            if (rbnJogSpd_High.Checked) dSpd = DATA_.mtSoftData[m].JOG_HIGH_SPD;
            if (rbnJogSpd_Middle.Checked) dSpd = DATA_.mtSoftData[m].JOG_MIDDLE_SPD;
            if (rbnJogSpd_Low.Checked) dSpd = DATA_.mtSoftData[m].JOG_LOW_SPD;
            return dSpd;
        }
        private void MotorCw_MouseDown(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            btn = (Button)sender;
            double dSpd = GetJogSpeed(btn.TabIndex);
            dValue = Math.Abs(double.Parse(txtIncPitch.Text));

            if (CHK_USE_INC.Checked) LAB_.MT_PITCH_CW(btn.TabIndex, dSpd, dValue);
            else LAB_.MT_JOG_CW(btn.TabIndex, dSpd);
        }

        private void MotorCcw_MouseDown(object sender, MouseEventArgs e){
            if (DATA_.eMCStatus == eMachineStatus.AUTO || DATA_.eMCStatus == eMachineStatus.INITIAL || DATA_.bMF) return;
            btn = (Button)sender;
            double dSpd = GetJogSpeed(btn.TabIndex);
            dValue = Math.Abs(double.Parse(txtIncPitch.Text));

            if (CHK_USE_INC.Checked) LAB_.MT_PITCH_CCW(btn.TabIndex, dSpd, dValue);
            else LAB_.MT_JOG_CCW(btn.TabIndex, dSpd);
        }

        private void FormVision_Load(object sender, EventArgs e){

        }

        public void UpdataDate(){
            Pre_X1.Text = DATA_.mtDATA[M.StripPkX, P.FirstTrigger].Pos.ToString();
            Pre_X2.Text = DATA_.mtDATA[M.StripPkX, P.SecondTrigger].Pos.ToString();
#if _NSS3300
#else
            Pre_Y1.Text = DATA_.mtDATA[M.PreAlign, P.StripTrigger1].Pos.ToString();
            Pre_Y2.Text = DATA_.mtDATA[M.PreAlign, P.StripTrigger2].Pos.ToString();
#endif
            Pre_Z1.Text = DATA_.mtDATA[M.StripPkZ, P.FirstTrigger].Pos.ToString();
            Pre_Z2.Text = DATA_.mtDATA[M.StripPkZ, P.SecondTrigger].Pos.ToString();

            bLedBright_0.Value = (int)DATA_.prMODEL[RP.PreAlignLight];
            tbxResolution.Text = DATA_.prMACHINE[CP.PreCamResolution].ToString();
        }

        private void TmrVISION_Tick(object sender, EventArgs e){
            TmrVISION.Enabled = false;
            Invoke();
            TmrVISION.Enabled = true;
        }

        void Invoke(){
            dtxMT_X.DigitText = DATA_.mtSTS[M.StripPkX].CurrentPosition.ToString();
            lblRefPos_X.Text = string.Format("{0:0.###}", DATA_.mtSTS[M.StripPkX].CurrentPosition - dRefCurPos_X);
            lblRefHalfPos_X.Text = string.Format("{0:0.###}", (DATA_.mtSTS[M.StripPkX].CurrentPosition - dRefCurPos_X) / 2);
#if _NSS3300
#else
            dtxMT_Y.DigitText = DATA_.mtSTS[M.PreAlign].CurrentPosition.ToString();
            lblRefPos_Y.Text = string.Format("{0:0.###}", DATA_.mtSTS[M.PreAlign].CurrentPosition - dRefCurPos_Y);
            lblRefHalfPos_Y.Text = string.Format("{0:0.###}", (DATA_.mtSTS[M.PreAlign].CurrentPosition - dRefCurPos_Y) / 2);
#endif
            dtxMT_Z.DigitText = DATA_.mtSTS[M.StripPkZ].CurrentPosition.ToString();

        }
    }
}