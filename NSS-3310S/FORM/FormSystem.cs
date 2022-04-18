using NSS_3310S.SEQ;
using Object;
using System;
using System.Drawing;
using System.Windows.Forms;
using SYSTEM;

namespace NSS_3310S{
    public partial class FormSystem : Form{
        Button btn = null;
        DataGridViewRow grow = null;
        DataGridView dg = null;
        RadioButton rbtn = null;
        Label lbl = null;
        string sLabel = string.Empty;
        int nIndex = 0;
        int nValue = 0;
        int nCol = 0, nRow = 0;
        double dValue = 0.0;
        RadioButton[] StackerUnloadMode = null;
        RadioButton[] ConveyorUnloadMode = null;
        Label[] PKOffsetXY = null;
        bool bChagePkOffsetView = false;
        int nSelectOffsetHD = 0;
        int nSelectHDTh = 0;
        dxy PkOffsetValue;
        RadioButton rbn = null;
        DataGridView dgv = null;

        string[] mTowerLamp = new string[] { "RUN", "STOP", "INITIALIZING", "WAIT RUN", "ERROR", "MANUAL", "LOT-END", "WARNING" };
        JCS.ToggleSwitch[] tUse = null;
        JCS.ToggleSwitch[] tUse1 = null;
        public Button[] PkrVac { get; set; }
        public Button[] PkrRej { get; set; }
        public Button[] PkrFree { get; set; }
        public Button[] OUT1 { get; set; }
        public Button[] OUT2 { get; set; }

        public FormSystem(){
            InitializeComponent();

            #region EVENT
            btSave_DataPara.Click += (sender, e) => SAVE(btSave_DataPara);
            btSave_DelayPara.Click += (sender, e) => SAVE(btSave_DelayPara);
            btSave_TowerLamp.Click += (sender, e) => SAVE(btSave_TowerLamp);
            btSave_AnalogData.Click += (sender, e) => SAVE(btSave_AnalogData);
            btSave_UseData.Click += (sender, e) => SAVE(btSave_UseData);
            btSave_UseData1.Click += (sender, e) => SAVE(btSave_UseData1);
            btSave_PkOffsetData.Click += (sender, e) => SAVE(btSave_PkOffsetData);

            T_LampMode0.Click += (sender, e) => ReadTowerLamp(T_LampMode0);
            T_LampMode1.Click += (sender, e) => ReadTowerLamp(T_LampMode1);
            T_LampMode2.Click += (sender, e) => ReadTowerLamp(T_LampMode2);
            T_LampMode3.Click += (sender, e) => ReadTowerLamp(T_LampMode3);
            T_LampMode4.Click += (sender, e) => ReadTowerLamp(T_LampMode4);
            T_LampMode5.Click += (sender, e) => ReadTowerLamp(T_LampMode5);
            T_LampMode6.Click += (sender, e) => ReadTowerLamp(T_LampMode6);
            T_LampMode7.Click += (sender, e) => ReadTowerLamp(T_LampMode7);

            btnR1.Click += (sender, e) => SetRad(btnR1);
            btnR2.Click += (sender, e) => SetRad(btnR2);
            btnR3.Click += (sender, e) => SetRad(btnR3);

            btnY1.Click += (sender, e) => SetYellow(btnY1);
            btnY2.Click += (sender, e) => SetYellow(btnY2);
            btnY3.Click += (sender, e) => SetYellow(btnY3);

            btnG1.Click += (sender, e) => SetGreen(btnG1);
            btnG2.Click += (sender, e) => SetGreen(btnG2);
            btnG3.Click += (sender, e) => SetGreen(btnG3);

            btnB1.Click += (sender, e) => SetBlue(btnB1);
            btnB2.Click += (sender, e) => SetBlue(btnB2);
            btnB3.Click += (sender, e) => SetBlue(btnB3);

            cbxModule.SelectionChangeCommitted += (sender, e) => VacMoudle(cbxModule);

            btnSetAnalog.Click += (sender, e) => SetVacuum(btnSetAnalog);
            #endregion

            tUse = new JCS.ToggleSwitch[] { chkLogSave, ChkBuzzer, ChkAreaSeneor,
                                            ChkGripperStripSensor, ChkPreAlign,
                                            ChkTopVisionInspection, ChkTopVisionInspectionResult, ChkUnitEachOffset,
                                            ChkScrapVacSensor,
                                            ChkClearBox, ChkUnitAirshower, ChkUnitBrush,
                                            ChkStageVacSensor, ChkUnitInspectionStageAirShower, ChkWorkedAirShowre, ChkPkrVacCheck, 
                                            ChkXMarkInspectionMode, ChkUseUnitPkWorkedAirshower, ChkUseUnitPkWorkedCleaner, ChkUseTrayFeederTrayCheck, 
                                            ChkUseLotStartPkAutoCal, ChkUsePickUpVac, ChkLotEnd, ChkUsePlaceCheck, ChkUseStripPkrCheck, 
                                            ChkUseCheckRejectBox, SelectMotorSpd };
            for (int i = 0; i < tUse.Length; i++){
                tUse[i].TabIndex = CP.UseData[i];
            }

            tUse1 = new JCS.ToggleSwitch[] { ChkMGZ_1, ChkMGZ_2, ChkITSDATA,
                ChkBtnInspection, ChkBtnInspectionResult, ChkUseMsSQL, ChkBarcode, ChkMES, ChkRFID
            };
            for (int i = 0; i < tUse1.Length; i++){
                tUse1[i].TabIndex = CP.UseData1[i];
            }

            PkrVac = new Button[] { bVAC_01, bVAC_02, bVAC_03, bVAC_04, bVAC_05, bVAC_06, bVAC_07, bVAC_08 };
            PkrRej = new Button[] { bBLOW_01, bBLOW_02, bBLOW_03, bBLOW_04, bBLOW_05, bBLOW_06, bBLOW_07, bBLOW_08 };
            PkrFree = new Button[] { bFREE_01, bFREE_02, bFREE_03, bFREE_04, bFREE_05, bFREE_06, bFREE_07, bFREE_08 };
            OUT1 = new Button[] { OUT1_0, OUT1_1, OUT1_2, OUT1_3, OUT1_4, OUT1_5, OUT1_6, OUT1_7 };
            OUT2 = new Button[] { OUT2_0, OUT2_1, OUT2_2, OUT2_3, OUT2_4, OUT2_5, OUT2_6, OUT2_7 };

            ChkMGZDir.TabIndex = RP.MGZ_DIR;
            ChkTrayUnloading.TabIndex = CP.TrayUnloadingMode;
            CHK_SCRAP_ALARM.TabIndex = RP.ScrapAlarm;
            CHK_SCRAP_VACUUM.TabIndex = RP.ScrapVacuum;

            StackerUnloadMode = new RadioButton[] { ChkGoodTray1, ChkGoodTray2 };
            ConveyorUnloadMode = new RadioButton[] { ChkULDTrayMode_GT1, ChkULDTrayMode_GT2, ChkULDTrayMode_All };

            PKOffsetXY = new Label[] {
                    lbPKR_PITCH, 
                    lbHD1Cam_Pkr_OffsetX, lbHD1Cam_Pkr_OffsetY, lbHD2Cam_Pkr_OffsetX, lbHD2Cam_Pkr_OffsetY,
                    lbHD1Cam_PkrPlace_OffsetX, lbHD1Cam_PkrPlace_OffsetY, lbHD2Cam_PkrPlace_OffsetX, lbHD2Cam_PkrPlace_OffsetY
            };
            for (int i = 0; i < PKOffsetXY.Length; i++){
                PKOffsetXY[i].TabIndex = CP.SysPara[i];
            }

            COM_.MakePickerOffset(dgvPickerOffsetPitch, 3);

            btnAutoPickerCal.TabIndex = ManualNumber.RunPickerAutoCal;
            lbUnitPlaceCheckVac.TabIndex = CP.PlaceCheckVac;

            cbxModule.SelectedIndex = 0;

            ChkSkipDoor.Checked = false;
            T_LampMode0.Checked = true;
            DATA_.IsLONG[L.SeletTower] = -1;
        }
        void VacMoudle(object sender) { ReadPkVac(); }
        void SetVacuum(object sender){
            try{
                if (string.IsNullOrEmpty(lblANALOG_VALUE.Text)){
                    MessageBox.Show("Space Err.");
                    return;
                }
                dValue = Convert.ToDouble(lblANALOG_VALUE.Text);
                dg = dgvAIR;
                for (int i = 0; i < 8; i++) dg[2, i + 1].Value = dValue.ToString();
            }
            catch (Exception ex){
                MessageBox.Show("Set vacuum value fail" + ETC.CrLf + ex.Message);
            }
        }

        public void Initailize_View(){
            COM_.MakePickerVac(dgvAIR, 9);
            ReadDataGridViewPara(eGridDataViewPara.DATA);
            ReadDataGridViewPara(eGridDataViewPara.DELAY);
            if (DATA_.IsLONG[L.SeletTower] < 0) ReadTowerLamp(T_LampMode0);
            ReadPkVac();
            ReadUsePara();
            ReadPkrOffsetPara();

            ChkSkipDoor.Checked = DATA_.IsBIT[B.SkipDoorLock];
            TmrSYSTEM.Enabled = true;
            Show();
            BringToFront();
        }
        public void Set_Language(){

        }

        void SAVE(object sender){
            btn = (Button)sender;
            if (btn.Name == "btSave_DataPara" || btn.Name == "btSave_DelayPara"){
                try{
                    dg = btn.Name == "btSave_DataPara" ? Grid_DATA as DataGridView : Grid_DELAY as DataGridView;
                    if (DialogResult.OK != MessageBox.Show("Save " + btn.Tag.ToString() + " ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                    for (int i = 0; i < dg.RowCount; i++){
                        nIndex = int.Parse(dg.Rows[i].Cells[1].Value.ToString());
                        dValue = double.Parse(dg.Rows[i].Cells[3].Value.ToString());

                        if (dg.Rows[i].Cells[0].Value.ToString() == "M" || dg.Rows[i].Cells[0].Value.ToString() == "m"){
                            if (nIndex == CP.PowerMeterComPort){
                                if (DATA_.prMACHINE[nIndex] != dValue){
                                    DATA_.cPM.Close();
                                    UTIL_.DELAY(100);
                                    DATA_.cPM.Open((int)DATA_.prMACHINE[CP.PowerMeterComPort], Baudrate.bps19200);
                                }
                            }
                            if (nIndex == CP.LightComPort){
                                if (DATA_.prMACHINE[nIndex] != dValue){
                                    DATA_.cLightController.CLOSE();
                                    UTIL_.DELAY(100);
                                    DATA_.cLightController.OPEN((short)dValue);
                                }
                            }
                            TEACH_.WR_MCPara(nIndex, dValue);
                        }
                        else{
                            TEACH_.WR_MDLPara(nIndex, dValue);
                        }
                    }
                    MessageBox.Show(btn.Tag + " Save Success");
                }
                catch (Exception ex){
                    MessageBox.Show(btn.Tag + " Save Fail" + ETC.CrLf + ex.Message);
                }
            }
            if (btn.Name == "btSave_TowerLamp"){
                try{
                    if (DialogResult.OK != MessageBox.Show("Save " + btn.Tag.ToString() + " ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                    for (int i = 0; i < DATA_.DataTowerStatus.Length; i++){
                        if (DATA_.DataTowerStatus[i] != DATA_.BackupTowerStatus[i]) LogWR_.SAVE_ChangeDataEvent(mTowerLamp[i] + " = " + DATA_.DataTowerStatus[i] + " -> " + DATA_.BackupTowerStatus[i]);
                        DATA_.DataTowerStatus[i] = DATA_.BackupTowerStatus[i];
                    }
                    TEACH_.WR_TowerLamp();
                    MessageBox.Show(btn.Tag + " Save Success");
                }
                catch (Exception ex){
                    MessageBox.Show(btn.Tag + " Save Fail" + ETC.CrLf + ex.Message);
                }
            }
            if (btn.Name == "btSave_AnalogData"){
                try{
                    if (DialogResult.OK != MessageBox.Show("Save " + btn.Tag.ToString() + " ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                    for (int i = 0; i < 8; i++){
                        DATA_.mSET_AI[i + (8 * cbxModule.SelectedIndex)] = DATA_.cMATH.IsNumber(Convert.ToString(dgvAIR[2, i + 1].Value));
                    }
                    TEACH_.WR_Analog();
                    TEACH_.Write_Parameter(lbUnitPlaceCheckVac);
                    MessageBox.Show(btn.Tag + " Save Success");
                }
                catch (Exception ex){
                    MessageBox.Show(btn.Tag + " Save Fail" + ETC.CrLf + ex.Message);
                }
            }
            if (btn.Name == "btSave_UseData"){
                try{
                    if (DialogResult.OK != MessageBox.Show("SAVE USE SKIP DATA ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                    int nOldSelectMtSpd = (int)DATA_.prMACHINE[CP.SelectMotorSpd];
                    for (int i = 0; i < tUse.Length; i++){
                        //tUse[i].TabIndex = CP.UseData[i];
                        nValue = tUse[i].Checked ? 1 : 0;
                        if (tUse[i].Tag.ToString() == "MC") TEACH_.WR_MCPara(tUse[i].TabIndex, nValue);
                        else TEACH_.WR_MDLPara(tUse[i].TabIndex, nValue);
                    }

                    TEACH_.Write_Parameter(ChkMGZDir);
                    TEACH_.Write_Parameter(ChkTrayUnloading);
                    TEACH_.Write_Parameter(CHK_SCRAP_ALARM);
                    TEACH_.Write_Parameter(CHK_SCRAP_VACUUM);

                    TEACH_.Write_Parameter(CP.SelectStackerUnloading, "MC", StackerUnloadMode);
                    TEACH_.Write_Parameter(CP.SelectConveyorUnloading, "MC", ConveyorUnloadMode);

                    if (nOldSelectMtSpd != DATA_.prMACHINE[CP.SelectMotorSpd]){
                        TEACH_.RD_MTDATA();
                        DATA_.mTeachChanged = true;
                    }
                    ReadUsePara();
                    MessageBox.Show(btn.Tag + " Save Success");
                }
                catch (Exception ex){
                    MessageBox.Show(btn.Tag + " Save Fail" + ETC.CrLf + ex.Message);
                }
            }
            if (btn.Name == "btSave_UseData1"){
                if (DialogResult.OK != MessageBox.Show("SAVE USE SKIP DATA ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                for (int i = 0; i < tUse1.Length; i++){
                    nValue = tUse1[i].Checked ? 1 : 0;
                    if (tUse1[i].Tag.ToString() == "MC")    TEACH_.WR_MCPara(tUse1[i].TabIndex, nValue);
                    else                                    TEACH_.WR_MDLPara(tUse1[i].TabIndex, nValue);
                }
                ReadUsePara();
                MessageBox.Show(btn.Tag + " Save Success");
            }
            if (btn.Name == "btSave_PkOffsetData"){
                try{
                    dxy XY;
                    string[] sROT = new string[] { "0", "P90", "P180", "P270", "M90", "M180", "M270" };
                    if (nSelectOffsetHD == 0){
                        for (int i = 0; i < 8; i++){
                            XY.x = Convert.ToDouble(dgvPickerOffsetPitch[i + 1, 1].Value.ToString());
                            XY.y = Convert.ToDouble(dgvPickerOffsetPitch[i + 1, 2].Value.ToString());
                            TEACH_.WR_PickerOffset(i, XY);
                            TEACH_.WR_PickerOffset(sROT[nSelectHDTh], i, XY);
                        }
                    }
                    else if (nSelectOffsetHD == 1){
                        for (int i = 0; i < 8; i++){
                            XY.x = Convert.ToDouble(dgvPickerOffsetPitch[i + 1, 1].Value.ToString());
                            XY.y = Convert.ToDouble(dgvPickerOffsetPitch[i + 1, 2].Value.ToString());
                            TEACH_.WR_PickerOffset(i + 8, XY);
                            TEACH_.WR_PickerOffset(sROT[nSelectHDTh], i + 8, XY);
                        }
                    }

                    for (int i = 0; i < PKOffsetXY.Length; i++){
                        TEACH_.Write_Parameter(PKOffsetXY[i]);
                    }
                    ReadPkrOffsetPara();
                    MessageBox.Show(btn.Tag + " Save Success");
                }
                catch(Exception ex){
                    MessageBox.Show(btn.Tag + " Save Fail" + ETC.CrLf + ex.Message);
                }
            }
            DEF.SetParaFDC();
        }

        private void Blow_Click(object sender, EventArgs e){
            btn = (Button)sender;
            int nCH = cbxModule.SelectedIndex;
            BASE.PkBlow((eHD)nCH, (ePK)btn.TabIndex, true);  //blow on -> off
        }
        private void Free_Click(object sender, EventArgs e){
            btn = (Button)sender;
            int nCH = cbxModule.SelectedIndex;
            BASE.PkFree((eHD)nCH, (ePK)btn.TabIndex);
        }
        private void Vac_Click(object sender, EventArgs e){
            btn = (Button)sender;
            int nCH = cbxModule.SelectedIndex;
            BASE.PkVac((eHD)nCH, (ePK)btn.TabIndex, stBIT.ON, stBIT.NotDelay, "");
        }

        private void Out1_Click(object sender, EventArgs e){
            btn = (Button)sender;
            int nCH = cbxModule.SelectedIndex;
            int nOUT1 = nCH == (int)eHD.HD1 ? O.HD1PkRej[btn.TabIndex] : O.HD2PkRej[btn.TabIndex];
            DATA_.mOUT[nOUT1] = !DATA_.mOUT[nOUT1];
        }
        private void Out2_Click(object sender, EventArgs e){
            btn = (Button)sender;
            int nCH = cbxModule.SelectedIndex;
            int nOUT2 = nCH == (int)eHD.HD1 ? O.HD1PkVac[btn.TabIndex] : O.HD2PkVac[btn.TabIndex];
            DATA_.mOUT[nOUT2] = !DATA_.mOUT[nOUT2];
        }

        private void ParaData_Click(object sender, EventArgs e){
            lbl = (Label)sender;
            DEF.TenKeyOption = true;
            if (lbl.Tag.ToString() == "MC") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[lbl.TabIndex];
            else DATA_.IsSTRING[S.MotorMessage] = DATA_.MDParaName[lbl.TabIndex];
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }

        string GetDataLabel(){
            sLabel = string.Empty;

            sLabel += "M," + CP.UphCalcCount.ToString() + "," + DATA_.MCParaName[CP.UphCalcCount] + ETC.CrLf;
            sLabel += "M," + CP.ManualRunRate.ToString() + "," + DATA_.MCParaName[CP.ManualRunRate] + ETC.CrLf;
            sLabel += "M," + CP.StripPkSafetyPosition.ToString() + "," + DATA_.MCParaName[CP.StripPkSafetyPosition] + ETC.CrLf;
            sLabel += "M," + CP.UnitPkSafetyPosition.ToString() + "," + DATA_.MCParaName[CP.UnitPkSafetyPosition] + ETC.CrLf;
            sLabel += "M," + CP.HandlerPkSafetyRangePitch.ToString() + "," + DATA_.MCParaName[CP.HandlerPkSafetyRangePitch] + ETC.CrLf;
            sLabel += "M," + CP.StripBlowRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.StripBlowRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.ScrapBlowRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.ScrapBlowRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.BrushRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.BrushRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.UnitAirshowRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.UnitAirshowRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.UnitBlowRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.UnitBlowRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.StageAirshowRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.StageAirshowRepeatCnt] + ETC.CrLf;
            sLabel += "M," + CP.StageWorkedAirshowerRepeatCnt.ToString() + "," + DATA_.MCParaName[CP.StageWorkedAirshowerRepeatCnt] + ETC.CrLf;

            sLabel += "M," + CP.RePick.ToString() + "," + DATA_.MCParaName[CP.RePick] + ETC.CrLf;
            sLabel += "M," + CP.PRSStartPos.ToString() + "," + DATA_.MCParaName[CP.PRSStartPos] + ETC.CrLf;
            sLabel += "M," + CP.PRSEndPos.ToString() + "," + DATA_.MCParaName[CP.PRSEndPos] + ETC.CrLf;

            sLabel += "M," + CP.TrayPlace_Interlock1.ToString() + "," + DATA_.MCParaName[CP.TrayPlace_Interlock1] + ETC.CrLf;
            sLabel += "M," + CP.TrayPlace_Interlock2.ToString() + "," + DATA_.MCParaName[CP.TrayPlace_Interlock2] + ETC.CrLf;

            sLabel += "M," + CP.PowerMeterComPort.ToString() + "," + DATA_.MCParaName[CP.PowerMeterComPort] + ETC.CrLf;
            sLabel += "M," + CP.LightComPort.ToString() + "," + DATA_.MCParaName[CP.LightComPort] + ETC.CrLf;

            sLabel += "M," + CP.GoodTray1PlaceFirstLine.ToString() + "," + DATA_.MCParaName[CP.GoodTray1PlaceFirstLine] + ETC.CrLf;
            sLabel += "M," + CP.GoodTray1PlaceLastLine.ToString() + "," + DATA_.MCParaName[CP.GoodTray1PlaceLastLine] + ETC.CrLf;

            sLabel += "M," + CP.GoodTray2PlaceFirstLine.ToString() + "," + DATA_.MCParaName[CP.GoodTray2PlaceFirstLine] + ETC.CrLf;
            sLabel += "M," + CP.GoodTray2PlaceLastLine.ToString() + "," + DATA_.MCParaName[CP.GoodTray2PlaceLastLine] + ETC.CrLf;

            sLabel += "M," + CP.ReworkTrayPlaceFirstLine.ToString() + "," + DATA_.MCParaName[CP.ReworkTrayPlaceFirstLine] + ETC.CrLf;
            sLabel += "M," + CP.ReworkTrayPlaceLastLine.ToString() + "," + DATA_.MCParaName[CP.ReworkTrayPlaceLastLine] + ETC.CrLf;

            return sLabel;
        }
        string GetDelayLabel(){
            sLabel = string.Empty;

            sLabel += "M," + CP.CylinderOverTime.ToString() + "," + DATA_.MCParaName[CP.CylinderOverTime] + ETC.CrLf;
            sLabel += "M," + CP.StartPushTime.ToString() + "," + DATA_.MCParaName[CP.StartPushTime] + ETC.CrLf;
            sLabel += "M," + CP.JogTime.ToString() + "," + DATA_.MCParaName[CP.JogTime] + ETC.CrLf;
            sLabel += "M," + CP.BzOffTime.ToString() + "," + DATA_.MCParaName[CP.BzOffTime] + ETC.CrLf;
            sLabel += "M," + CP.BlinkTime.ToString() + "," + DATA_.MCParaName[CP.BlinkTime] + ETC.CrLf;
            sLabel += "M," + CP.EjectChkTime.ToString() + "," + DATA_.MCParaName[CP.EjectChkTime] + ETC.CrLf;
            sLabel += "M," + CP.ACMotorRunTime.ToString() + "," + DATA_.MCParaName[CP.ACMotorRunTime] + ETC.CrLf;
            sLabel += "M," + CP.ACMotorRunStopDelay.ToString() + "," + DATA_.MCParaName[CP.ACMotorRunStopDelay] + ETC.CrLf;

            sLabel += "M," + CP.PusherFwdDelay.ToString() + "," + DATA_.MCParaName[CP.PusherFwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.PusherBwdDelay.ToString() + "," + DATA_.MCParaName[CP.PusherBwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.MgzArrivalDelay.ToString() + "," + DATA_.MCParaName[CP.MgzArrivalDelay] + ETC.CrLf;
            sLabel += "M," + CP.ElvClampDelay.ToString() + "," + DATA_.MCParaName[CP.ElvClampDelay] + ETC.CrLf;
            sLabel += "M," + CP.ElvUnClampDelay.ToString() + "," + DATA_.MCParaName[CP.ElvUnClampDelay] + ETC.CrLf;

            sLabel += "M," + CP.GripperLockDelay.ToString() + "," + DATA_.MCParaName[CP.GripperLockDelay] + ETC.CrLf;
            sLabel += "M," + CP.GripperUnlockDelay.ToString() + "," + DATA_.MCParaName[CP.GripperUnlockDelay] + ETC.CrLf;

            sLabel += "M," + CP.InletTableUp.ToString() + "," + DATA_.MCParaName[CP.InletTableUp] + ETC.CrLf;
            sLabel += "M," + CP.InletTableDn.ToString() + "," + DATA_.MCParaName[CP.InletTableDn] + ETC.CrLf;
            sLabel += "M," + CP.InletVac.ToString() + "," + DATA_.MCParaName[CP.InletVac] + ETC.CrLf;
            sLabel += "M," + CP.InletBlow.ToString() + "," + DATA_.MCParaName[CP.InletBlow] + ETC.CrLf;

            sLabel += "M," + CP.StripPkVacOn.ToString() + "," + DATA_.MCParaName[CP.StripPkVacOn] + ETC.CrLf;
            sLabel += "M," + CP.StripPkVacOff.ToString() + "," + DATA_.MCParaName[CP.StripPkVacOff] + ETC.CrLf;
            sLabel += "M," + CP.StripBlowOn.ToString() + "," + DATA_.MCParaName[CP.StripBlowOn] + ETC.CrLf;
            sLabel += "M," + CP.UnitPkVacOn.ToString() + "," + DATA_.MCParaName[CP.UnitPkVacOn] + ETC.CrLf;
            sLabel += "M," + CP.UnitPkVacOff.ToString() + "," + DATA_.MCParaName[CP.UnitPkVacOff] + ETC.CrLf;
            sLabel += "M," + CP.UnitPkBlowOn.ToString() + "," + DATA_.MCParaName[CP.UnitPkBlowOn] + ETC.CrLf;
            sLabel += "M," + CP.ScrapBlowOn.ToString() + "," + DATA_.MCParaName[CP.ScrapBlowOn] + ETC.CrLf;

            sLabel += "M," + CP.StageVacOn.ToString() + "," + DATA_.MCParaName[CP.StageVacOn] + ETC.CrLf;
            sLabel += "M," + CP.StageVacOff.ToString() + "," + DATA_.MCParaName[CP.StageVacOff] + ETC.CrLf;
            sLabel += "M," + CP.StageBlowDelay.ToString() + "," + DATA_.MCParaName[CP.StageBlowDelay] + ETC.CrLf;
            sLabel += "M," + CP.InpectionMoveEndDelay.ToString() + "," + DATA_.MCParaName[CP.InpectionMoveEndDelay] + ETC.CrLf;
            sLabel += "M," + CP.TriggerEnd.ToString() + "," + DATA_.MCParaName[CP.TriggerEnd] + ETC.CrLf;
            sLabel += "M," + CP.VisionReponseOverTime.ToString() + "," + DATA_.MCParaName[CP.VisionReponseOverTime] + ETC.CrLf;
            sLabel += "M," + CP.GoodTrayPreAlignFwdDelay.ToString() + "," + DATA_.MCParaName[CP.GoodTrayPreAlignFwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.GoodTrayPreAlignBwdDelay.ToString() + "," + DATA_.MCParaName[CP.GoodTrayPreAlignBwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.StackerUpDelay.ToString() + "," + DATA_.MCParaName[CP.StackerUpDelay] + ETC.CrLf;
            sLabel += "M," + CP.StackerDnDelay.ToString() + "," + DATA_.MCParaName[CP.StackerDnDelay] + ETC.CrLf;
            sLabel += "M," + CP.EmptyStopperLockDelay.ToString() + "," + DATA_.MCParaName[CP.EmptyStopperLockDelay] + ETC.CrLf;
            sLabel += "M," + CP.EmptyStopperUnlockDelay.ToString() + "," + DATA_.MCParaName[CP.EmptyStopperUnlockDelay] + ETC.CrLf;
            sLabel += "M," + CP.FeederGripDelay.ToString() + "," + DATA_.MCParaName[CP.FeederGripDelay] + ETC.CrLf;
            sLabel += "M," + CP.FeederUnGripDelay.ToString() + "," + DATA_.MCParaName[CP.FeederUnGripDelay] + ETC.CrLf;
            sLabel += "M," + CP.EmptyFeederFwdDelay.ToString() + "," + DATA_.MCParaName[CP.EmptyFeederFwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.EmptyFeederBwdDelay.ToString() + "," + DATA_.MCParaName[CP.EmptyFeederBwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.TrayCleampDelay.ToString() + "," + DATA_.MCParaName[CP.TrayCleampDelay] + ETC.CrLf;
            sLabel += "M," + CP.TrayUnCleampDelay.ToString() + "," + DATA_.MCParaName[CP.TrayUnCleampDelay] + ETC.CrLf;
            sLabel += "M," + CP.CamZigFwdDelay.ToString() + "," + DATA_.MCParaName[CP.CamZigFwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.CamZigBwdDelay.ToString() + "," + DATA_.MCParaName[CP.CamZigBwdDelay] + ETC.CrLf;
            sLabel += "M," + CP.BarcodeReadingCheck.ToString() + "," + DATA_.MCParaName[CP.BarcodeReadingCheck] + ETC.CrLf;
            sLabel += "MD," + RP.ULDConvWaitTime.ToString() + "," + DATA_.MDParaName[RP.ULDConvWaitTime] + ETC.CrLf;
            return sLabel;
        }
        void ReadDataGridViewPara(eGridDataViewPara ePara){
            var gdv = ePara == eGridDataViewPara.DATA ? Grid_DATA as DataGridView : Grid_DELAY as DataGridView;
            gdv.RowCount = 1;
            gdv.Rows[0].Cells[0].Value = "";
            gdv.Rows[0].Cells[1].Value = "";
            gdv.Rows[0].Cells[2].Value = "";
            gdv.Rows[0].Cells[3].Value = "";

            string AllLabel = ePara == eGridDataViewPara.DATA ? GetDataLabel() : GetDelayLabel();
            string[] SubLabel = AllLabel.Split(ETC.CrLf);
            gdv.RowCount = SubLabel.Length - 1 <= 0 ? 1 : SubLabel.Length - 1;
            gdv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            nIndex = 0;
            for (int i = 0; i < SubLabel.Length - 1; i++){
                string[] sList = SubLabel[i].Split(',');
                if (sList.Length < 3) continue;

                gdv.Rows[nIndex].Cells[0].Value = sList[0].Trim();
                gdv.Rows[nIndex].Cells[1].Value = sList[1].Trim();
                gdv.Rows[nIndex].Cells[2].Value = sList[2].Trim();
                if (gdv.Rows[nIndex].Cells[0].Value.ToString() == "M" || gdv.Rows[nIndex].Cells[0].Value.ToString() == "m"){
                    gdv.Rows[nIndex].Cells[3].Value = DATA_.prMACHINE[int.Parse(sList[1])].ToString();
                    //gdv[0, i].Style.BackColor = DATA_.ComBackColor;
                    //gdv[1, i].Style.BackColor = DATA_.ComBackColor;
                    //gdv[2, i].Style.BackColor = DATA_.ComBackColor;
                }
                else{
                    gdv.Rows[nIndex].Cells[3].Value = DATA_.prMODEL[int.Parse(sList[1])].ToString();
                }
                grow = gdv.Rows[i];
                grow.Height = 30;
                nIndex++;
            }
            UTIL_.CLEAR_GRID_SELECTED(ref gdv);
        }

        void ReadTowerLamp(object sender){
            rbtn = (RadioButton)sender;
            DATA_.LampStatus = Convert.ToInt16(rbtn.Tag);
            if (DATA_.DataTowerStatus[DATA_.LampStatus] == null){
                ViewTower(btnR1, btnR2, btnR3, 0, Color.White);
                ViewTower(btnY1, btnY2, btnY3, 0, Color.White);
                ViewTower(btnG1, btnG2, btnG3, 0, Color.White);
                ViewTower(btnB1, btnB2, btnB3, 0, Color.White);
                DATA_.BackupTowerStatus[DATA_.LampStatus] = "222";
                return;
            }

            for (int i = 0; i < CNT_.TowerLamp; i++){
                nIndex = Convert.ToInt16(DATA_.DataTowerStatus[DATA_.LampStatus].Substring(i, 1));
                switch (i){
                    case 0:
                        ViewTower(btnR1, btnR2, btnR3, nIndex, Color.Red);
                        break;
                    case 1:
                        ViewTower(btnY1, btnY2, btnY3, nIndex, Color.Yellow);
                        break;
                    case 2:
                        ViewTower(btnG1, btnG2, btnG3, nIndex, Color.Green);
                        break;
                    case 3:
                        ViewTower(btnB1, btnB2, btnB3, nIndex, Color.Blue);
                        break;
                    default:
                        break;
                }
            }
            DATA_.BackupTowerStatus[DATA_.LampStatus] = DATA_.DataTowerStatus[DATA_.LampStatus];
        }

        void ReadPkVac(){
            for (int i = 0; i < 8; i++){
                dgvAIR[0, i + 1].Value = DATA_.AI_NAME[i + (8 * cbxModule.SelectedIndex)];
                dgvAIR[2, i + 1].Value = DATA_.mSET_AI[i + (8 * cbxModule.SelectedIndex)];
            }
            lblANALOG_VALUE.Text = nValue.ToString();
            UTIL_.CLEAR_GRID_SELECTED(ref dgvAIR);

            lbUnitPlaceCheckVac.Text = DATA_.prMACHINE[CP.PlaceCheckVac].ToString();
        }

        void ReadUsePara(){
            for (int i = 0; i < tUse.Length; i++){
                tUse[i].Checked = DATA_.prMACHINE[CP.UseData[i]] == (int)eUSE.USE ? true : false;
            }
            ChkMGZDir.Checked           = DATA_.prMODEL[RP.MGZ_DIR] == (int)eUSE.USE ? true : false;
            ChkTrayUnloading.Checked    = DATA_.prMACHINE[CP.TrayUnloadingMode] == (int)eUSE.USE ? true : false;
            CHK_SCRAP_ALARM.Checked     = DATA_.prMODEL[RP.ScrapAlarm] == (int)eScrapAlarm.USE ? false : true;
            CHK_SCRAP_VACUUM.Checked    = DATA_.prMODEL[RP.ScrapVacuum] == (int)eScrapVacuum.USE ? true : false;

            StackerUnloadMode[(int)DATA_.prMACHINE[CP.SelectStackerUnloading]].Checked = true;
            ConveyorUnloadMode[(int)DATA_.prMACHINE[CP.SelectConveyorUnloading]].Checked = true;
        
            for (int i = 0; i < tUse1.Length; i++){
                tUse1[i].Checked = DATA_.prMACHINE[CP.UseData1[i]] == (int)eUSE.USE ? true : false;
            }
        }

        void ReadPkrOffsetPara(){
            for (int i = 0; i < PKOffsetXY.Length; i++){
                PKOffsetXY[i].Text = DATA_.prMACHINE[CP.SysPara[i]].ToString();
            }
            bChagePkOffsetView = true;
        }

        private void DataGrip_Para_CellClick(object sender, DataGridViewCellEventArgs e){
            dg = sender as DataGridView;
            UTIL_.GET_GRID_NUMBER(dg, ref nCol, ref nRow);
            if (nCol == 0 || nCol == 1 || nCol == 2){
                UTIL_.CLEAR_GRID_SELECTED(ref dg);
                return;
            }

            try{
                nIndex = int.Parse(dg.Rows[nRow].Cells[1].Value.ToString());
                if (dg.Rows[nRow].Cells[0].Value.ToString() == "M" || dg.Rows[nRow].Cells[0].Value.ToString() == "m") DATA_.IsSTRING[S.SystemMessage] = DATA_.MCParaName[nIndex]; //장비 파라메타
                else DATA_.IsSTRING[S.SystemMessage] = DATA_.MDParaName[nIndex]; //모델 파라메타
                UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.SystemMessage], ref dg, false);
            }
            catch (Exception ex){
                MessageBox.Show("PARA DATA CLICK ERROR" + ETC.CrLf + ex.Message);
            }
        }

        private void DataGrip_PkVac_CellClick(object sender, DataGridViewCellEventArgs e){
            dg = sender as DataGridView;
            UTIL_.GET_GRID_NUMBER(dg, ref nCol, ref nRow);
            if (nCol == 2 && nRow > 0){
                DATA_.IsSTRING[S.SystemMessage] = dg.Rows[nRow].Cells[0].Value.ToString();
                UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.SystemMessage], ref dg, false);
            }
            UTIL_.CLEAR_GRID_SELECTED(ref dg);
        }

        private void TenKey_DoubleClick(object sender, EventArgs e){
            lbl = (Label)sender;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.SystemMessage] = lbl.Tag.ToString();
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }

        void ViewTower(Button btnOn, Button btnBlink, Button btnOff, int nSelet, Color cColor){
            btnOn.BackColor = nSelet == 0 ? cColor : Color.White;
            btnOn.UseCompatibleTextRendering = nSelet == 0 ? true : false;
            btnBlink.BackColor = nSelet == 1 ? cColor : Color.White;
            btnBlink.UseCompatibleTextRendering = nSelet == 1 ? true : false;
            btnOff.BackColor = nSelet == 2 ? cColor : Color.White;
            btnOff.UseCompatibleTextRendering = nSelet == 2 ? true : false;
        }
        void SetRad(object sender){
            btn = (Button)sender;
            nIndex = Convert.ToInt16(btn.Tag);
            btnR1.UseCompatibleTextRendering = nIndex == 0 ? true : false; //on
            btnR2.UseCompatibleTextRendering = nIndex == 0 ? true : false; //blin
            btnR3.UseCompatibleTextRendering = nIndex == 0 ? true : false; //off
            DATA_.BackupTowerStatus[DATA_.LampStatus] = Convert.ToString(nIndex) + DATA_.BackupTowerStatus[DATA_.LampStatus].Substring(1, 2);
        }
        void SetYellow(object sender){
            btn = (Button)sender;
            nIndex = Convert.ToInt16(btn.Tag);
            btnY1.UseCompatibleTextRendering = nIndex == 0 ? true : false; //on
            btnY2.UseCompatibleTextRendering = nIndex == 0 ? true : false; //blin
            btnY3.UseCompatibleTextRendering = nIndex == 0 ? true : false; //off
            DATA_.BackupTowerStatus[DATA_.LampStatus] = DATA_.BackupTowerStatus[DATA_.LampStatus].Substring(0, 1) + Convert.ToString(nIndex) + DATA_.BackupTowerStatus[DATA_.LampStatus].Substring(2, 1);
        }
        void SetGreen(object sender){
            btn = (Button)sender;
            nIndex = Convert.ToInt16(btn.Tag);
            btnG1.UseCompatibleTextRendering = nIndex == 0 ? true : false; //on
            btnG2.UseCompatibleTextRendering = nIndex == 0 ? true : false; //blin
            btnG3.UseCompatibleTextRendering = nIndex == 0 ? true : false; //off
            DATA_.BackupTowerStatus[DATA_.LampStatus] = DATA_.BackupTowerStatus[DATA_.LampStatus].Substring(0, 2) + Convert.ToString(nIndex);
        }
        void SetBlue(object sender){
            btn = (Button)sender;
            nIndex = Convert.ToInt16(btn.Tag);
            btnB1.UseCompatibleTextRendering = nIndex == 0 ? true : false; //on
            btnB2.UseCompatibleTextRendering = nIndex == 0 ? true : false; //blin
            btnB3.UseCompatibleTextRendering = nIndex == 0 ? true : false; //off
            DATA_.BackupTowerStatus[DATA_.LampStatus] = DATA_.BackupTowerStatus[DATA_.LampStatus].Substring(0, 3) + Convert.ToString(nIndex);
        }

        private void ChkOffsetHead(object sender, EventArgs e){
            if (rbnHD1.Checked) nSelectOffsetHD = 0;
            else if (rbnHD2.Checked) nSelectOffsetHD = 1;
            bChagePkOffsetView = true;
        }

        private void SelectRotateOffsetView(object sender, EventArgs e){
            rbn = (RadioButton)sender;
            nSelectHDTh = rbn.TabIndex;
            bChagePkOffsetView = true;
        }

        private void PickerOffset_CellClick(object sender, DataGridViewCellEventArgs e){
            dgv = (DataGridView)sender;
            UTIL_.GET_GRID_NUMBER(dgv, ref nCol, ref nRow);
            if (nCol > 0 && nRow > 0){
                DATA_.IsSTRING[S.DeviceMessage] = dgv.Rows[nRow].Cells[0].Value.ToString();
                UTIL_.OPEN_KEYPAD_GRID(DATA_.IsSTRING[S.DeviceMessage], ref dgv, true);
            }
            UTIL_.CLEAR_GRID_SELECTED(ref dgv);
        }

        private void ManualRun_Click(object sender, EventArgs e){
            btn = (Button)sender;
            DATA_.iMANUAL.Number = btn.TabIndex;
            DATA_.IsSTRING[S.ManualMessage] = btn.Text;
            string[] arr = DATA_.IsSTRING[S.ManualMessage].Split('\n');
            DATA_.IsSTRING[S.ManualMessage] = string.Empty;
            for (int i = 0; i < arr.Length; i++){
                string[] temp = arr[i].Split('\r');
                for (int j = 0; j < temp.Length; j++)
                    DATA_.IsSTRING[S.ManualMessage] += temp[j] + " ";
            }
            switch (DATA_.iMANUAL.Number){
                case ManualNumber.RunPickerAutoCal:
                    string[] sROT = new string[] { "0", "P90", "P180", "P270", "M90", "M180", "M270" };
                    double[] dROT = new double[] { 0, 90, 180, 270, -90, -180, -270 };
                    DATA_.iMANUAL.int_1 = rbnHD1.Checked ? (int)eHD.HD1 : (int)eHD.HD2;
                    DATA_.iMANUAL.iMT1 = rbnHD1.Checked ? NSS_3310S.M.TRIGGER1 : NSS_3310S.M.TRIGGER2;
                    DATA_.iMANUAL.ManualCmd = sROT[nSelectHDTh];
                    DATA_.iMANUAL.double_1 = dROT[nSelectHDTh];
                    break;

                default: break;
            }
            DATA_.iMANUAL.bRESULT = COM_.RUN_MANUAL(DATA_.iMANUAL.Number, DATA_.IsSTRING[S.ManualMessage], true);
        }

        private void TmrSYSTEM_Tick(object sender, EventArgs e){
            TmrSYSTEM.Enabled = false;
            Invoke();
            TmrSYSTEM.Enabled = true;
        }

        private void ChkSkipDoor_CheckedChanged(object sender, EventArgs e){
            DATA_.IsBIT[B.SkipDoorLock] = ChkSkipDoor.Checked;
        }

        void Invoke(){
            lbINDEX.Text = cbxModule.SelectedIndex.ToString();
            for (int i = 0; i < 8; i++){
                if (cbxModule.SelectedIndex == 0){
                    dgvAIR[1, i + 1].Value = DATA_.mAI[i];
                    OUT1[i].BackColor = DATA_.mOUT[O.HD1PkRej[i]] ? Color.Yellow : Color.White;
                    OUT2[i].BackColor = DATA_.mOUT[O.HD1PkVac[i]] ? Color.Yellow : Color.White;
                }
                else{
                    dgvAIR[1, i + 1].Value = DATA_.mAI[i + 8];
                    OUT1[i].BackColor = DATA_.mOUT[O.HD2PkRej[i]] ? Color.Yellow : Color.White;
                    OUT2[i].BackColor = DATA_.mOUT[O.HD2PkVac[i]] ? Color.Yellow : Color.White;
                }
            }

            if (bChagePkOffsetView){
                bChagePkOffsetView = false;

                dgvPickerOffsetPitch.ClearSelection();
                dgvPickerOffsetPitch[0, 1].Value = "X";
                dgvPickerOffsetPitch[0, 2].Value = "Y";
                for (int i = 0; i < 8; i++){
                    if (nSelectHDTh == 0) PkOffsetValue = DATA_.PkOffset_0[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 1) PkOffsetValue = DATA_.PkOffset_P90[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 2) PkOffsetValue = DATA_.PkOffset_P180[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 3) PkOffsetValue = DATA_.PkOffset_P270[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 4) PkOffsetValue = DATA_.PkOffset_M90[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 5) PkOffsetValue = DATA_.PkOffset_M180[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 6) PkOffsetValue = DATA_.PkOffset_M270[i + (8 * nSelectOffsetHD)];

                    dgvPickerOffsetPitch[1 + i, 1].Value = PkOffsetValue.x.ToString();
                    dgvPickerOffsetPitch[1 + i, 2].Value = PkOffsetValue.y.ToString();
                }
            }
        }
    }
}