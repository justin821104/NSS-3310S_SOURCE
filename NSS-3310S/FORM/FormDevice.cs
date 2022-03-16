using NSS_3310S.SEQ;
using Object;
using System;
using System.IO;
using System.Windows.Forms;
using SYSTEM;
using LIB_.DateType;

namespace NSS_3310S{
    public delegate void EventHandler_DEVICE_OPEN();

    public partial class FormDevice : Form{
        public event EventHandler_DEVICE_OPEN EVENT_OPEN;
        bool bDeleteFoler = false;
        public string[] ArrDevice;
        public string CheckGroup = string.Empty;
        public string CheckLastDevice = string.Empty;
        DataGridView dgv = null;
        ListView lv = null;
        Button btn = null;
        RadioButton rbn = null;
        Label lbl = null;
        Label[] MCRecipe = null;
        Label[] MDRecipe = null;
        RadioButton[] SetHead = null;
        RadioButton[] StageStatus = null;
        RadioButton[] StackerUnloadMode = null;
        RadioButton[] ConveyorUnloadMode = null;
        CheckBox[] HD1Pk = null;
        CheckBox[] HD2Pk = null;
        RadioButton[] PickerRotate = null;
        string SelectPPID = string.Empty;
        string SelectGroup = string.Empty;
        string SelectDevice = string.Empty;
        string SelectVision = string.Empty;
        string SelectSaw = string.Empty;
        int nIndex = 0;
        int nValue = 0;
        dxy PkOffset;
        public string mSubItem = string.Empty;
        public string mSelectedItem, mSelectedItem_Job = string.Empty;
        string sMakeJobFile = string.Empty;
        int nSelectOffsetHD = 0;
        int nSelectHDTh = 0;
        bool bChagePkOffsetView = false;
        int nCol, nRow = 0;
        public bool bRecipeCreate = false;

        ComboBox[] CleanMode = null;
        Label[] CleanRepeat = null;
        ComboBox[] WorkedCleanMode = null;
        Label[] WorkedCleanRepeat = null;

        enum eMake{
            Device = 0,
            Group = 1,
        }

        public FormDevice(){
            InitializeComponent();

            #region EVENT
            lvwGROUP.SelectedIndexChanged   += (sender, e) => SelectedIndex(lvwGROUP);
            lvwDEVICE.SelectedIndexChanged  += (sender, e) => SelectedIndex(lvwDEVICE);
            DGV_PPID_LIST.CellClick         += (sender, e) => SelectedPPID(DGV_PPID_LIST);

            btnGroup.Click                  += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) ViewFrame((int)eMake.Group, "NEW GROUP"); };
            btnSaveAs.Click                 += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) ViewFrame((int)eMake.Device, "NEW RECIPE"); };
            btnNewMake.Click                += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) NewMake(); };
            btnNewReturn.Click              += (sender, e) => { gNEW_DEVICE.Visible = false; };

            btnDEL.Click                    += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) RecipeSwitch(btnDEL); };
            btnOPEN.Click                   += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) RecipeSwitch(btnOPEN); };
            swSave.Click                    += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) RecipeSwitch(swSave); };

            WriteUserID.Click               += (sender, e) => SUBFRM_.gUserReqister.Initailize_View();

            swCHANGE.Click                  += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) EventDetailed_Information(swCHANGE); };
            swAPPEND.Click                  += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) EventDetailed_Information(swAPPEND); };
            swMODIFY.Click                  += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) EventDetailed_Information(swMODIFY); };
            swDELETE.Click                  += (sender, e) => { if (DATA_.eMCStatus != eMachineStatus.AUTO) EventDetailed_Information(swDELETE); };

            MCRecipe = new Label[] { lblGripper_ReCatchCnt,
                                     lbPKR_PITCH, lbHD1Cam_Pkr_OffsetX,lbHD1Cam_Pkr_OffsetY, lbHD2Cam_Pkr_OffsetX,lbHD2Cam_Pkr_OffsetY,
                                     lbHD1Cam_PkrPlace_OffsetX, lbHD1Cam_PkrPlace_OffsetY, lbHD2Cam_PkrPlace_OffsetX, lbHD2Cam_PkrPlace_OffsetY,
                                     BrushRepeatCnt, AirShowerRepeatCnt, WorkedAirShowerRepeatCnt, lblMGZ_Pitch_Spd
            };
            for (int i = 0; i < MCRecipe.Length; i++)
            {
                MCRecipe[i].TabIndex = CP.DVPara[i];
            }

            MDRecipe = new Label[] { lblMGZSlotCnt, lblMGZSlotPitch,
                                     lblUNIT_SIZE_X, lblUNIT_SIZE_Y, lblUNIT_Thickness,
                                     lblTRAY_CNT_X, lblTRAY_CNT_Y, lblTRAY_PITCH_X, lblTRAY_PITCH_Y, lblTRAY_SLOW_SPEED,
                                     lblMAP_BLOCK_CNT_X, lblMAP_BLOCK_CNT_Y, lblMAP_BLOCK_PITCH_X, lblMAP_BLOCK_PITCH_Y,
                                     lblMAP_BLOCK_GROUP_CNT_X, lblMAP_BLOCK_GROUP_CNT_Y, lblMAP_BLOCK_GROUP_PITCH_X, lblMAP_BLOCK_GROUP_PITCH_Y,
                                     lblMAP_BLOCK_REJ_INDEX_NUM, lblMAP_BLOCK_SLOW_SPEED, lblMAP_BLOCK_AIRSLOWER_SHOW_SPEED,lblMAP_BLOCK_AIRSLOWER_SHOW_CNT,lblUnitNGOverCnt
            };
            for (int i = 0; i < MDRecipe.Length; i++)
            {
                MDRecipe[i].TabIndex = RP.DVPara[i];
            }

            lblX1UnitPic.Tag = M.X1T.ToString();
            lblX2UnitPic.Tag = M.X2T.ToString();
            lblX1UnitPic.TabIndex = P.PkPckUp;
            lblX2UnitPic.TabIndex = P.PkPckUp;

            lblX1UnitPlace.Tag = M.X1T.ToString();
            lblX2UnitPlace.Tag = M.X2T.ToString();
            lblX1UnitPlace.TabIndex = P.PkPlc;
            lblX2UnitPlace.TabIndex = P.PkPlc;

            btnAutoPickerCal.TabIndex = ManualNumber.RunPickerAutoCal;

            chkPCBTYPE.TabIndex = RP.PCB_TYPE;
            chkPickUpMabBlockVac.TabIndex = CP.StageUnitPickupVac;
            chkMabBlockVac.TabIndex = CP.StagePickupMovingVac;

            ChkTrayUnloading.TabIndex = CP.TrayUnloadingMode;
            CHK_SCRAP_ALARM.TabIndex = RP.ScrapAlarm;
            CHK_SCRAP_VACUUM.TabIndex = RP.ScrapVacuum;

            StageStatus = new RadioButton[] { ChkPALLET1, ChkPALLET2, ChkPALLET_ALL };
            SetHead = new RadioButton[] { ChkHEAD_HD1, ChkHEAD_HD2, ChkHEAD_ALL };
            StackerUnloadMode = new RadioButton[] { ChkGoodTray1, ChkGoodTray2 };
            ConveyorUnloadMode = new RadioButton[] { ChkULDTrayMode_GT1, ChkULDTrayMode_GT2, ChkULDTrayMode_All };
            HD1Pk = new CheckBox[] { ChkHD1_PK1, ChkHD1_PK2, ChkHD1_PK3, ChkHD1_PK4, ChkHD1_PK5, ChkHD1_PK6 };
            HD2Pk = new CheckBox[] { ChkHD2_PK1, ChkHD2_PK2, ChkHD2_PK3, ChkHD2_PK4, ChkHD2_PK5, ChkHD2_PK6 };
            PickerRotate = new RadioButton[] { Rot_0, Rot_P90, Rot_P180, Rot_P270, Rot_M90, Rot_M180, Rot_M270 };
            #endregion

            CleanMode = new ComboBox[] { cbxCLEAN_MODE_0, cbxCLEAN_MODE_1, cbxCLEAN_MODE_2 };
            CleanRepeat = new Label[] { lblCLEAN_REPEAT_0, lblCLEAN_REPEAT_1, lblCLEAN_REPEAT_2 };

            WorkedCleanMode = new ComboBox[] { cbxWORKED_CLEAN_MODE_0, cbxWORKED_CLEAN_MODE_1, cbxWORKED_CLEAN_MODE_2 };
            WorkedCleanRepeat = new Label[] { lblWORKED_CLEAN_REPEAT_0, lblWORKED_CLEAN_REPEAT_1, lblWORKED_CLEAN_REPEAT_2 };

            COM_.MakePickerOffset(dgvPickerOffsetPitch, 3);

            rbnHD1.Checked = true;
            Rot_0.Checked = true;
        }

        void ViewFrame(int i, string s){
            nIndex = i;
            gNEW_DEVICE.Text = s;
            txNAME.Text = "";
            COM_.SetFrame(gNEW_DEVICE, true, 80, 220, 380, 260);
        }
        void SelectedIndex(object sender){
            lv = (ListView)sender;

            if (lv.Name == "lvwGROUP"){
                try{
                    if (lvwGROUP.SelectedIndices.Count <= 0) return;
                    mSubItem = lvwGROUP.SelectedItems[0].Text;
                    mSelectedItem = string.Empty;
                    lbWorkGroup.Text = "WORK GROUP : " + mSubItem;
                    UTIL_.GetWorkRecipe(lvwGROUP, lvwDEVICE, GRD_DEVICE, mSubItem, bDeleteFoler);
                }
                catch (Exception ex) { MessageBox.Show("WORK GROUP 속성 변경 에러 (listbox1_SelectedUndexChanged) " + ex.ToString()); }
            }
            else{
                try{
                    if (lvwDEVICE.SelectedIndices.Count <= 0) return;
                    mSelectedItem = lvwDEVICE.SelectedItems[0].Text;
                    lbWorkDevice.Text = "WORK RECIPE : " + mSelectedItem;
                }
                catch (Exception exc) { MessageBox.Show("WORK RECIPE CHANGE PROPERTIES ERROR (listView1_SelectedIndexChanged) " + exc.ToString()); }
            }
        }
        void NewMake(){
            try{
                if (string.IsNullOrEmpty(txNAME.Text)){
                    MessageBox.Show("Cannot Use Blank Space" + ETC.NewLine + "(공백은 허용하지 않습니다 !)");
                    return;
                }
                //System.Text.RegularExpressions.Regex BlockWord = new System.Text.RegularExpressions.Regex(@"[a-zA-Z0-9_]");
                //for (int i = 0; i < sNewName.Text.Length; i++){
                //    if (!BlockWord.IsMatch(string.Format("{0}", sNewName.Text[i]))){
                //        MessageBox.Show("Must be English Or Number");
                //        return;
                //    }
                //}

                if (nIndex == (int)eMake.Device){ //Make device
                    if (mSubItem == "" || mSubItem == null){
                        MessageBox.Show("Select WORK GROUP!" + ETC.NewLine + "(WORK GROUP 리스트를 먼저 선택하셔야 합니다 !)");
                        txNAME.Text = "";
                        gNEW_DEVICE.Visible = false;
                        return;
                    }
                    if (!UTIL_.GetSomeWorkDevice(mSubItem, txNAME.Text)){
                        MessageBox.Show("Already Exist Same Job. Use Differnt Job Name." + ETC.NewLine + "(동일한 잡파일명이 있습니다.)");
                        txNAME.Text = "";
                        return;
                    }
                    if (DATA_.sJobName == "" || DATA_.sJobName == null){
                        sMakeJobFile = PATH_.DATA + mSubItem + "\\" + txNAME.Text + ".job";
                        TEACH_.WR_NewJobFile();
                        if (DATA_.sCurrJobName == null || DATA_.sCurrJobName == ""){
                            DATA_.sCurrJobName = PATH_.DATA + mSubItem + "\\" + txNAME.Text + ".job";
                        }// 프로그램 처음 시작시 레스피가 하나도 없어서 지금 생성한 RECIPE로 지정 하려고
                        FILE_.WR_File(PATH_.CurrJOB, DATA_.sCurrJobName, false);
                    }
                    else{
                        File.Copy(DATA_.sCurrJobName, PATH_.DATA + mSubItem + "\\" + txNAME.Text + ".job");
                    }
                    UTIL_.GetWorkRecipe(lvwGROUP, lvwDEVICE, GRD_DEVICE, mSubItem, bDeleteFoler);
                    LogWR_.SAVE_ChangeDataEvent("MAKE NEW DEVICE -> " + txNAME.Text);
                    MessageBox.Show("SUCCESS MAKE NEW DEVICE !");
                }
                else{ //Make group
                    if (!UTIL_.GetSomeWorkGroup(txNAME.Text)){
                        MessageBox.Show("Already Exist Same GROUP. Use Differnt GROUP Name." + ETC.NewLine + "(동일한 GROUP명이 있습니다.)");
                        txNAME.Text = "";
                        return;
                    }
                    Directory.CreateDirectory(PATH_.DATA + txNAME.Text); // 폴더 만듬.
                    UTIL_.GetWorkGroup(lvwGROUP);
                    LogWR_.SAVE_ChangeDataEvent("MAKE NEW GROUP -> " + txNAME.Text);
                    MessageBox.Show("SUCCESS MAKE NEW GROUP FOLDER !");
                }
                gNEW_DEVICE.Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("New make file fail !" + ETC.NewLine + ex.ToString()); }
        }
        void RecipeSwitch(object sender){
            btn = (Button)sender;
            if (btn.Name == "btnOPEN"){
                try{
                    if (lvwDEVICE.SelectedIndices.Count <= 0){
                        MessageBox.Show("You must select a work device !");
                        return;
                    }
                    string sSelectGroup = mSubItem;
                    string sSelectJob = mSelectedItem.Substring(0, mSelectedItem.Length - 4);
                    string sOldGroupName = /*DATA_.sGroupName*/mSubItem;
                    string sOldCurName = DATA_.sCurrJobName;
                    string sOldJobName = DATA_.sJobName;

                    if (sOldGroupName == sSelectGroup && sOldJobName == sSelectJob){
                        MessageBox.Show("This work device is currently open !");
                        return;
                    }
                    DATA_.sCurrJobName = PATH_.DATA + mSubItem.Trim() + "\\" + mSelectedItem.Trim();
                    FILE_.WR_File(PATH_.CurrJOB, DATA_.sCurrJobName, false);
                    if (UTIL_.OpenJobFile()){
                        UTIL_.LD_JOB_FILE();
                        TEACH_.Read_ManualInspection((int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY]);
                    }
                    EVENT_OPEN();
                    ReadData();
                    //F.fVisionSet.LoadFinePattern();
                    UTIL_.DELAY(500);
                    LogWR_.SAVE_ChangeDataEvent("OPEN WORK DEVICE - " + sOldCurName + " -> " + mSelectedItem);
                    DEF.ChangeSawRecipe();
                    lbl_DEVICE.Text = "CURRENT DEVICE : " + DATA_.sCurrJobName;
                    //SUBFRM_.gSecsGem.SetRecipeIDChage(CMES.MES_LOT_ID, sSelectJob);
                    DATA_.mTeachChanged = true;
                    MessageBox.Show("Device Change Complete");
                }
                catch (Exception ex) { MessageBox.Show("Device open fail !" + ETC.NewLine + ex.ToString()); }
            }
            if (btn.Name == "btnDEL"){
                try{
                    if (DATA_.eLoginLevel < eLogLevel.ENG) return;
                    string sGroup = mSubItem;
                    string sDevice = mSelectedItem;
                    COM_.CheckDevice(ref CheckGroup, ref CheckLastDevice);

                    if (sGroup == "" && sDevice == ""){
                        MessageBox.Show("You need to select the item you want to delete.");
                        return;
                    }
                    if (CheckGroup == sGroup && (sDevice == "" || sDevice == null)){
                        MessageBox.Show("Cannot Delete Current Group");
                        return;
                    }
                    if (mSelectedItem == null || mSelectedItem == ""){
                        if (DialogResult.OK == MessageBox.Show(sGroup + " Group Folder Delete ?", "Select", MessageBoxButtons.OKCancel)){
                            //FileIO_.FolderCopy(PATH_.DATA + mSubItem, mSubItem);//백업?

                            Directory.Delete(PATH_.DATA + mSubItem, true);
                            LogWR_.SaveLogOperate("DELETE GROUP FOLDER -> " + mSubItem, "SCREEN");
                            MessageBox.Show("Success Delete Group Folder");
                            bDeleteFoler = true;
                            mSubItem = "";
                        }
                    } // delete folder
                    else{
                        if (DialogResult.OK == MessageBox.Show(sDevice + " Device Delete ?", "Select", MessageBoxButtons.OKCancel)){
                            //FileIO_.FileCopy(PATH_.DATA + mSubItem, mSubItem);//백업?

                            File.Delete(PATH_.DATA + mSubItem + "\\" + mSelectedItem);
                            LogWR_.SaveLogOperate("DELETE DEVCIE FILE -> " + mSelectedItem, "SCREEN");
                            MessageBox.Show("Success Delete Device File");
                            mSelectedItem = "";
                        }
                    } // delete device
                    UTIL_.GetWorkRecipe(lvwGROUP, lvwDEVICE, GRD_DEVICE, mSubItem, bDeleteFoler);
                    bDeleteFoler = true;
                }
                catch (Exception ex) { MessageBox.Show("DEVICE DEL FAIL !" + ETC.NewLine + ex.ToString()); }
            }
            if (btn.Name == "swSave"){
                if (MessageBox.Show("Do you want to save teaching data ?", "Save", MessageBoxButtons.YesNo) == DialogResult.No) return;
                for (int i = 0; i < MCRecipe.Length; i++){
                    TEACH_.Write_Parameter(MCRecipe[i]);
                }
                for (int i = 0; i < MDRecipe.Length; i++){
                    TEACH_.Write_Parameter(MDRecipe[i]);
                }
                TEACH_.Write_Parameter(chkPCBTYPE);
                TEACH_.Write_Parameter(chkPickUpMabBlockVac);
                TEACH_.Write_Parameter(chkMabBlockVac);

                TEACH_.Write_Parameter(ChkTrayUnloading);
                TEACH_.Write_Parameter(CHK_SCRAP_ALARM);
                TEACH_.Write_Parameter(CHK_SCRAP_VACUUM);

                TEACH_.Write_Parameter(CP.SelectStage, "MC", StageStatus);
                TEACH_.Write_Parameter(CP.SelectHead, "MC", SetHead);
                TEACH_.Write_Parameter(CP.SelectStackerUnloading, "MC", StackerUnloadMode);
                TEACH_.Write_Parameter(CP.SelectConveyorUnloading, "MC", ConveyorUnloadMode);

                //
                TEACH_.SaveMotorPos(int.Parse(lblX1UnitPic.Tag.ToString()), lblX1UnitPic.TabIndex, double.Parse(lblX1UnitPic.Text));
                TEACH_.SaveMotorPos(int.Parse(lblX2UnitPic.Tag.ToString()), lblX2UnitPic.TabIndex, double.Parse(lblX2UnitPic.Text));

                TEACH_.SaveMotorPos(int.Parse(lblX1UnitPlace.Tag.ToString()), lblX1UnitPlace.TabIndex, double.Parse(lblX1UnitPlace.Text));
                TEACH_.SaveMotorPos(int.Parse(lblX2UnitPlace.Tag.ToString()), lblX2UnitPlace.TabIndex, double.Parse(lblX2UnitPlace.Text));

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

                for (int i = 0; i < CNT_.PKR; i++){
                    DATA_.PK_[i, 0] = (HD1Pk[i].Checked ? eSTATUS.EMPTY : eSTATUS.NONE);
                    DATA_.PK_[i + CNT_.PKR, 0] = (HD2Pk[i].Checked ? eSTATUS.EMPTY : eSTATUS.NONE);

                    if (!HD1Pk[i].Checked) BASE.PkFree(eHD.HD1, (ePK)i);
                    if (!HD2Pk[i].Checked) BASE.PkFree(eHD.HD2, (ePK)i);
                }
                for (int i = 0; i < CNT_.PKR * 2; i++){
                    TEACH_.WR_UsePKR(i, DATA_.PK_[i, 0]);
                }

                nIndex = cbxCLEAN_MODE_0.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_0.SelectedIndex;
                nValue = lblCLEAN_REPEAT_0.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_0.Text);
                TEACH_.WR_CLEAN_DATA(0, nIndex, nValue, 0);
                nIndex = cbxCLEAN_MODE_1.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_1.SelectedIndex;
                nValue = lblCLEAN_REPEAT_1.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_1.Text);
                TEACH_.WR_CLEAN_DATA(1, nIndex, nValue, 0);
                nIndex = cbxCLEAN_MODE_2.SelectedIndex < 0 ? 0 : cbxCLEAN_MODE_2.SelectedIndex;
                nValue = lblCLEAN_REPEAT_2.Text == "" ? 0 : int.Parse(lblCLEAN_REPEAT_2.Text);
                TEACH_.WR_CLEAN_DATA(2, nIndex, nValue, 0);

                //WR_WORKED_CLEAN_DATA
                nIndex = cbxWORKED_CLEAN_MODE_0.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_0.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_0.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_0.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(0, nIndex, nValue, 0);

                nIndex = cbxWORKED_CLEAN_MODE_1.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_1.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_1.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_1.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(1, nIndex, nValue, 0);

                nIndex = cbxWORKED_CLEAN_MODE_2.SelectedIndex < 0 ? 0 : cbxWORKED_CLEAN_MODE_2.SelectedIndex;
                nValue = lblWORKED_CLEAN_REPEAT_2.Text == "" ? 0 : int.Parse(lblWORKED_CLEAN_REPEAT_2.Text);
                TEACH_.WR_WORKED_CLEAN_DATA(2, nIndex, nValue, 0);


                DEF.ReadSearchRoiUnitCnt();
                DEF.ReadMapBlockPickUp();

                bChagePkOffsetView = true;
                EVENT_OPEN();
                MessageBox.Show("OK SAVE");
            }
        }
        void SelectedPPID(object sender){
            dgv = (DataGridView)sender;
            UTIL_.GET_GRID_NUMBER(dgv, ref nCol, ref nRow);
            if (DATA_.eMCStatus == eMachineStatus.AUTO || nRow < 0 || nCol <= 0){
                UTIL_.CLEAR_GRID_SELECTED(ref dgv);
                return;
            }
            SelectPPID = dgv[nCol, nRow].Value.ToString();
            if (!File.Exists(PATH_.PPID + SelectPPID + ".txt")){
                MessageBox.Show("PPID 파일이 없습니다.");
                return;
            }
            string[] sLine = File.ReadAllText(PATH_.PPID + SelectPPID + ".txt").Split(ETC.CrLf);
            for (int i = 0; i < sLine.Length; i++){
                sLine[i] = sLine[i].Replace("\r", "");
            }
            CMES.SELECT_GROUP = sLine[0]; //SELECT_GROUP.Text   = sLine[0];
            CMES.SELECT_DEVICE = sLine[1]; //SELECT_DEVICE.Text  = sLine[1];
            CMES.SELECT_VISION = sLine[2]; //SELECT_VISION.Text  = sLine[2];
            CMES.SELECT_SAW = sLine[3]; //SELECT_SAW.Text     = sLine[3];
        }
        void EventDetailed_Information(object sender){
            btn = (Button)sender;
            if (btn.Name == "swCHANGE"){
                if (SelectPPID == ""){
                    MessageBox.Show("SELECTED PPID (PPEXECNAME) 선택 하셔야 합니다!");
                    return;
                }
                string sPATH = PATH_.PPID + SelectPPID + ".txt";
                if (!File.Exists(sPATH)){
                    MessageBox.Show("PPID가 존재 하지 않습니다!");
                    return;
                }

                if (DialogResult.OK != MessageBox.Show("선택 하신 PPID로 DEVICE 변경 하시겠습니까 ?", "SELECT", MessageBoxButtons.OKCancel)) return;
                string[] sLine = File.ReadAllText(sPATH).Split(ETC.CrLf);
                //NORMAL                        //GROUP명
                //25X35 - T08 - 124 - A0.job    //SORTER RECIPE
                //NORMAL_25X35 - T08 - 124 - A0 //VISION RECIEP
                //25X35 - T08 - 124 - A0        //SAW RECIEP
                for (int i = 0; i < sLine.Length; i++){
                    sLine[i] = sLine[i].Replace("\r", "");
                }

                CMES.SAW_GROUP = sLine[0];
                CMES.SAW_RECIPE = sLine[3];
                FILE_.WR_File(PATH_.SAW_RECIPE, CMES.SAW_GROUP + "," + CMES.SAW_RECIPE, false);

                DEF.ChangeSawRecipe();
                if (DEF.SawRecipeOpen()){
                    COM_.ViewWarning(-1, W.SawRecipeLoadingFail);
                }

                string GetRecipe = sLine[1].Replace(".job", "");
                string sOldCurName = DATA_.sCurrJobName;
                string sOldJobName = DATA_.sJobName;
                DATA_.sCurrJobName = PATH_.DATA + /*DATA_.sGroupName*/SELECT_GROUP.Text + "\\" + sLine[1];
                FILE_.WR_File(PATH_.CurrJOB, DATA_.sCurrJobName, false);
                FILE_.WR_File(PATH_.CurrPPID, sPATH, false);
                CMES.CurPPID = UTIL_.GET_PPID_NAME();
                UTIL_.GET_PPID_RECIPE_NAME(PATH_.PPID + CMES.CurPPID + ".txt", ref CMES.CUR_GROUP, ref CMES.CUR_DEVICE, ref CMES.CUR_VISION, ref CMES.CUR_SAW);
                if (sOldJobName == GetRecipe){
                    MessageBox.Show("This work device is currently open !");
                    return;
                }
                if (UTIL_.OpenJobFile()){
                    UTIL_.LD_JOB_FILE();
                    TEACH_.Read_ManualInspection((int)DATA_.prMODEL[RP.GroupCntX], (int)DATA_.prMODEL[RP.GroupCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY], (int)DATA_.prMODEL[RP.UnitCntX], (int)DATA_.prMODEL[RP.UnitCntY]);
                }
                ReadData();
                DEF.ChangeVisionRecipe();
                EVENT_OPEN();

                LogWR_.SAVE_ChangeDataEvent("OPEN WORK DEVICE - " + sOldCurName + " -> " + DATA_.sCurrJobName);
                MessageBox.Show("PPID OPEN !");
            }
            if (btn.Name == "swAPPEND"){
                if (mSubItem == "" || mSubItem == null){
                    MessageBox.Show("WORK GROUP를 먼저 선택 하셔야 합니다 !");
                    return;
                }
                F.RecipeCreat.nSelect = btn.TabIndex;
                F.RecipeCreat.Initailize_View();
            }
            if (btn.Name == "swMODIFY"){
                if (CMES.SELECT_PPID == ""){
                    MessageBox.Show("PPID OPEN 되어 있지 않습니다 !");
                    return;
                }
                F.RecipeCreat.nSelect = btn.TabIndex;
                F.RecipeCreat.Initailize_View();
            }
            if (btn.Name == "swDELETE"){
                if (SelectPPID == ""){
                    MessageBox.Show("SELECTED PPID (PPEXECNAME) 선택 하셔야 합니다!");
                    return;
                }
                if (SelectPPID == CMES.CurPPID){
                    MessageBox.Show("현재 등록 되어 있는 PPID는 삭제 할 수 없습니다 !");
                    return;
                }
                if (DialogResult.OK != MessageBox.Show("선택 하신 PPID를 삭제 하시겠습니까 ?", "SELECT", MessageBoxButtons.OKCancel)) return;

                CMES.PPID = CUR_PPID.Text;
                CMES.PPID_SAW_GROUP = SELECT_GROUP.Text;
                CMES.PPID_SAW_DEVICE = SELECT_SAW.Text;
                CMES.PPID_SORTER_GROUP = SELECT_GROUP.Text;
                CMES.PPID_SORTER_DEVICE = SELECT_DEVICE.Text.Replace(".job", "");
                CMES.PPID_VISION_DEVICE = SELECT_VISION.Text;//GROUP.Text + "_" + (DEVCIE.Text).Replace(".job", "");

                TEACH_.DEL_PPID(SelectPPID);
                UTIL_.GetPPID(DGV_PPID_LIST);
            }
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

        private void ParaData_Click(object sender, EventArgs e){
            lbl = (Label)sender;
            DEF.TenKeyOption = true;
            if (lbl.Tag.ToString() == "MC") DATA_.IsSTRING[S.MotorMessage] = DATA_.MCParaName[lbl.TabIndex];
            else DATA_.IsSTRING[S.MotorMessage] = DATA_.MDParaName[lbl.TabIndex];
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }

        private void CleanerData_Click(object sender, EventArgs e){
            lbl = sender as Label;
            DEF.TenKeyOption = true;
            /*if (LBL.Tag.ToString() == "COUNT")*/
            DATA_.IsSTRING[S.MotorMessage] = groupBox7.Text + " " + lbl.Text;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }
        private void WorkedCleanerData_Click(object sender, EventArgs e){
            lbl = sender as Label;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.MotorMessage] = groupBox3.Text + " " + lbl.Text;
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }


        private void MTData_Click(object sender, EventArgs e){
            lbl = (Label)sender;
            DEF.TenKeyOption = true;
            DATA_.IsSTRING[S.MotorMessage] = DATA_.PosName[int.Parse(lbl.Tag.ToString()), lbl.TabIndex];
            UTIL_.OPEN_KEYPAD_LABEL(DATA_.IsSTRING[S.MotorMessage], ref lbl, DEF.TenKeyOption);
        }

        public void Initailize_View(){
            if (DATA_.sCurrJobName != null) lbl_DEVICE.Text = "CURRENT DEVICE : " + DATA_.sCurrJobName;
            COM_.SetFrame(gNEW_DEVICE, false, 80, 290, 380, 260);
            UTIL_.GetWorkGroup(lvwGROUP);
            COM_.CheckDevice(ref CheckGroup, ref CheckLastDevice);
            UTIL_.GetPPID(DGV_PPID_LIST);
            COM_.SetDetailedInfomation();
            ReadData();
            ReadDetailedInformation();

            SelectPPID = "";
            SelectGroup = "";
            SelectDevice = "";
            SelectVision = "";
            SelectSaw = "";

            DATA_.IsBIT[B.ManualPkrAutoCalView] = false;
            TmrRECIPE.Enabled = true;
            Show();
            BringToFront();
        }
        public void Set_Language(){

        }

        public void ReadData(){
            for (int i = 0; i < MCRecipe.Length; i++){
                MCRecipe[i].Text = DATA_.prMACHINE[CP.DVPara[i]].ToString();
            }
            for (int i = 0; i < MDRecipe.Length; i++){
                MDRecipe[i].Text = DATA_.prMODEL[RP.DVPara[i]].ToString();
            }
            chkPCBTYPE.Checked = DATA_.prMODEL[RP.PCB_TYPE] == (int)eUSE.USE ? true : false;
            chkPickUpMabBlockVac.Checked = DATA_.prMACHINE[CP.StageUnitPickupVac] == (int)eUSE.USE ? true : false;
            chkMabBlockVac.Checked = DATA_.prMACHINE[CP.StagePickupMovingVac] == (int)eUSE.USE ? true : false;

            ChkTrayUnloading.Checked = DATA_.prMACHINE[CP.TrayUnloadingMode] == (int)eUSE.USE ? true : false;
            CHK_SCRAP_ALARM.Checked = DATA_.prMODEL[RP.ScrapAlarm] == (int)eScrapAlarm.USE ? false : true;
            CHK_SCRAP_VACUUM.Checked = DATA_.prMODEL[RP.ScrapVacuum] == (int)eScrapVacuum.USE ? true : false;

            StageStatus[(int)DATA_.prMACHINE[CP.SelectStage]].Checked = true;
            SetHead[(int)DATA_.prMACHINE[CP.SelectHead]].Checked = true;
            StackerUnloadMode[(int)DATA_.prMACHINE[CP.SelectStackerUnloading]].Checked = true;
            ConveyorUnloadMode[(int)DATA_.prMACHINE[CP.SelectConveyorUnloading]].Checked = true;

            for (int i = 0; i < CNT_.PKR; i++){
                HD1Pk[i].Checked = DATA_.PK_Z[(short)(i)] != eSTATUS.NONE ? true : false;
                HD2Pk[i].Checked = DATA_.PK_Z[(short)(i + CNT_.PKR)] != eSTATUS.NONE ? true : false;
            }

            lblX1UnitPic.Text = DATA_.mtDATA[M.X1T, P.PkPckUp].Pos.ToString();
            lblX2UnitPic.Text = DATA_.mtDATA[M.X2T, P.PkPckUp].Pos.ToString();

            lblX1UnitPlace.Text = DATA_.mtDATA[M.X1T, P.PkPlc].Pos.ToString();
            lblX2UnitPlace.Text = DATA_.mtDATA[M.X2T, P.PkPlc].Pos.ToString();

            //cleaner para
            for (int i = 0; i < CleanMode.Length; i++){
                CleanMode[i].SelectedIndex = DATA_.CleanData.MODE[i];
                if (DATA_.CleanData.MODE[i] == 0) CleanRepeat[i].Text = "";
                else CleanRepeat[i].Text = DATA_.CleanData.COUNTER[i].ToString();
                CleanRepeat[i].Text = DATA_.CleanData.COUNTER[i].ToString();
            }
            //worked cleaner para
            for (int i = 0; i < WorkedCleanMode.Length; i++){
                WorkedCleanMode[i].SelectedIndex = DATA_.WorkedCleanData.MODE[i];
                if (DATA_.WorkedCleanData.MODE[i] == 0) WorkedCleanRepeat[i].Text = "";
                else WorkedCleanRepeat[i].Text = DATA_.WorkedCleanData.COUNTER[i].ToString();
                WorkedCleanRepeat[i].Text = DATA_.WorkedCleanData.COUNTER[i].ToString();
            }

            bChagePkOffsetView = true;
        }

        void ReadDetailedInformation(){
            LBL_USER_ID.Text = CUSER.Current.ID;
            CUR_PPID.Text = CMES.SELECT_PPID;//DATA_.CurPPID;
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
                    DATA_.iMANUAL.iMT1 = rbnHD1.Checked ? M.TRIGGER1 : M.TRIGGER2;
                    DATA_.iMANUAL.ManualCmd = sROT[nSelectHDTh];
                    DATA_.iMANUAL.double_1 = dROT[nSelectHDTh];
                    break;

                default: break;
            }
            DATA_.iMANUAL.bRESULT = COM_.RUN_MANUAL(DATA_.iMANUAL.Number, DATA_.IsSTRING[S.ManualMessage], true);
        }

        private void TmrRECIPE_Tick(object sender, EventArgs e){
            TmrRECIPE.Enabled = false;
            Invoke();
            TmrRECIPE.Enabled = true;
        }
        void Invoke(){
            SELECT_GROUP.Text = CMES.SELECT_GROUP;
            SELECT_DEVICE.Text = CMES.SELECT_DEVICE;
            SELECT_VISION.Text = CMES.SELECT_VISION;
            SELECT_SAW.Text = CMES.SELECT_SAW;
            SELECT_LOADER.Text = "-";

            if (bRecipeCreate){
                bRecipeCreate = false;
                UTIL_.GetPPID(DGV_PPID_LIST);
                ReadData();
                ReadDetailedInformation();
                F.RecipeCreat.Hide();
                UTIL_.PRINT_MASSAGE(DATA_.IsSTRING[S.DeviceMessage], false, true, false);
            }

            if (DATA_.IsBIT[B.ManualPkrAutoCalView]){
                DATA_.IsBIT[B.ManualPkrAutoCalView] = false;
                bChagePkOffsetView = true;
            }
            if (bChagePkOffsetView){
                bChagePkOffsetView = false;

                dgvPickerOffsetPitch.ClearSelection();
                dgvPickerOffsetPitch[0, 1].Value = "X";
                dgvPickerOffsetPitch[0, 2].Value = "Y";
                for (int i = 0; i < 8; i++){
                    //gbxSelectHead.Text = "HEAD " + (nSelectOffsetHD + 1).ToString();

                    if (nSelectHDTh == 0) PkOffset = DATA_.PkOffset_0[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 1) PkOffset = DATA_.PkOffset_P90[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 2) PkOffset = DATA_.PkOffset_P180[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 3) PkOffset = DATA_.PkOffset_P270[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 4) PkOffset = DATA_.PkOffset_M90[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 5) PkOffset = DATA_.PkOffset_M180[i + (8 * nSelectOffsetHD)];
                    if (nSelectHDTh == 6) PkOffset = DATA_.PkOffset_M270[i + (8 * nSelectOffsetHD)];

                    dgvPickerOffsetPitch[1 + i, 1].Value = PkOffset.x.ToString();
                    dgvPickerOffsetPitch[1 + i, 2].Value = PkOffset.y.ToString();
                }
            }
        }
    }
}