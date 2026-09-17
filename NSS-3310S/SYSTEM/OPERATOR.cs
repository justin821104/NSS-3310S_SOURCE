using NSS_3310S;
using NSS_3310S.ITS;
using Object;
using System;
using System.Threading;
using LIB_.DateType;

namespace SYSTEM{
    public class OPERATOR : DATA_{
        bool OnStopEdge = false;

        public void Do(){
            eMCStatus = eMachineStatus.NONE;
            bAllHomeComplete = false;
            bInitialComplete = false;
            do{
                if (gExit){
                    UTIL_.DELAY(1000);
                    DeleteThread();
                    return;
                }
                UTIL_.DELAY(3);

                #region >>Operator Swith
                if ((!mIN[I.START]) && !mIN[I.vtStart]) { bPushStart = false; } 
                else{
                    //bLotValidationWait true -> mes validation sucess 대기 !!!
                    if (!mIN[I.RESET]) {
                        if (prMACHINE[CP.UseTopInspection] == (int)eUSE.NotUSE) {
                            W.ViewWarning(T.Op, W.UnitSizeInspectionSkip);
                            while (W.WaitWarning(T.Op, W.UnitSizeInspectionSkip, "TOP VISION SKIP 상태로 START 진행")) ;
                            if (ConfirmUser[W.UnitSizeInspectionSkip].result) {
                                mIN[I.vtStart] = false;
                                bPushStart = true;
                            }//YES
                            else {
                                mIN[I.vtStart] = false;
                            }//NO
                        }
                        else {
                            if (prMACHINE[CP.UseTopInspectionResult] == (int)eUSE.NotUSE) {
                                W.ViewWarning(T.Op, W.UnitSizeReturnValueSkip);
                                while (W.WaitWarning(T.Op, W.UnitSizeReturnValueSkip, "TOP VISION 검사 결과 SKIP 상태로 START 진행")) ;
                                if (ConfirmUser[W.UnitSizeReturnValueSkip].result) {
                                    mIN[I.vtStart] = false;
                                    bPushStart = true;
                                }//YES
                                else {
                                    mIN[I.vtStart] = false;
                                }//NO
                            }
                            else {
                                mIN[I.vtStart] = false;
                                bPushStart = true;
                            }
                        }
                    }
                }//START 

                if (!mIN[I.STOP] && !mIN[I.vtStop]) bPushStop = false;
                else{
                    if (!IsBIT[B.WaitLotEndProcessing]) {
                        bPushStop = true;
                        mIN[I.vtStop] = false;
                        if (mOUT[O.CLEANER_WATER_1] || mOUT[O.CLEANER_WATER_2]) {
                            mOUT[O.CLEANER_WATER_1] = false;
                            mOUT[O.CLEANER_WATER_2] = false;
                        }
                        O.STOP_ACMOTOR();
                        LAB_.MT_ALL_STOP(false, "OP -> RUN() -> MT_ALL_STOP" + ETC.CrLf + "PUSH STOP");
                    }
                }//STOP

                if (OnStopEdge != bPushStop){
                    OnStopEdge = bPushStop;
                    if (OnStopEdge && bMF){
                        ReCreateManualThread();
                        if (iMANUAL.Number != 0 && iMANUAL.bLABEL) lbDumy.BackColor = iMANUAL.BackColor1;
                        else if (iMANUAL.Number != 0 && iMANUAL.bBUTTON) btDumy.BackColor = iMANUAL.BackColor1;
                    }
                }

                if (!mIN[I.RESET] && !mIN[I.vtReset]) bPushReset = false;
                else{
                    mIN[I.vtReset] = false;
                    bPushReset = true;
                    O.BZ_OFF();
                }//RESET

                if (!mIN[I.vtInitial]) bPushInitial = false;
                else{
                    mIN[I.vtInitial] = false;
                    bPushInitial = true;
                }//INITIAL

                if (bOnERROR && bPushReset){
                    E.CLEAR_ERROR();
                    LogWR_.SaveLogOperate("ERROR RESET", "MC");
                }//ERROR RESET
                #endregion

                #region >>Machine State
                if (eMCStatus == eMachineStatus.INITIAL && bInitialComplete && !bOnERROR){
                    eMCStatus = eMachineStatus.WAITRUN;
                    bEndInitial = true;
                    O.RESET_DOORLOCK();
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveLogOperate("INITIAL END", "MC");
                }//초기화 -> 운전대기 모드
                
                if ((eMCStatus == eMachineStatus.ERRSTOP || eMCStatus == eMachineStatus.USERSTOP || eMCStatus == eMachineStatus.WAITRUN) && bPushStart && bInitialComplete && !bOnERROR && (eLevelMainSw == eMainLevel.AUTO)){
                    if (!mDOOR_SKIP) O.SET_DOORLOCK();
                    if (RunThread()){
                        if (eMCStatus == eMachineStatus.USERSTOP) LogWR_.SaveMARS("MACHINE STOP", "OTHER", "STOP", 100);
                        if (eMCStatus == eMachineStatus.ERRSTOP) LogWR_.SaveMARS("MACHINE ERROR", "OTHER", "STOP", 100);
                        LogWR_.SaveMARS("MACHINE RUN", "OTHER", "START", 100);
                        for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "START";
                        SUBFRM_.gLOG.ERR_CLEAR();
                        bBzSTOP = false;
                        bOVER_5MINUTE = false;
                        eMCStatus = eMachineStatus.AUTO;
                        B.Bit(T.Op, B.MachineWaitProduct, false, "[STOP상태->RUN상태] 자재 없음 플러그 OFF");
                        bPushStart = true;
                        SUBFRM_.gSecsGem.SetPrecessState(CCEID.EQUIPMENT_STATE_RUN);
                        C.SendSaw.SEND("GET_SVID,*");
                        DEF.SetParaFDC();
                        UTIL_.DELAY(10);
                        if (CLOT.bFirstLot){
                            CLOT.bFirstLot = false;
                            SUBFRM_.gSecsGem.SetLotLoading();
                        }
                    }
                }//정지 -> 자동운전 모드
                
                if (eMCStatus == eMachineStatus.AUTO && bPushStop_Rec){
                    C.UnitPk.Cleaner(stBIT.OFF);
                    O.RESET_DOORLOCK();
                    StopThread();
                    eMCStatus = eMachineStatus.USERSTOP;
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "STOP";
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveMARS("MACHINE RUN", "OTHER", "STOP", 100);
                    LogWR_.SaveMARS("MACHINE STOP", "OTHER", "START", 100);
                }//자동운전 -> USER STOP
                
                if ((eMCStatus == eMachineStatus.AUTO) && bOnERROR){
                    C.UnitPk.Cleaner(stBIT.OFF);
                    O.RESET_DOORLOCK();
                    StopThread();
                    LAB_.MT_ALL_STOP(false, "CLS_OP -> RUN()" + ETC.CrLf + "eMachineStatus.ERRSTOP");
                    eMCStatus = eMachineStatus.ERRSTOP;
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "ERR";
                    mOUT[O.BUZZER_ERR] = true;
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID./*EQUIPMENT_STATE_IDLE*/EQUIPMENT_STATE_DOWN);
                    LogWR_.SaveMARS("MACHINE RUN", "OTHER", "STOP", 100);
                    LogWR_.SaveMARS("MACHINE ERROR", "OTHER", "RUN", 100);
                }//자동운전 -> ERROR STOP
                
                if (eMCStatus != eMachineStatus.EMSSTOP && bPushEms){
                    O.RESET_DOORLOCK();
                    if (eMCStatus == eMachineStatus.AUTO) StopThread();
                    eMCStatus = eMachineStatus.EMSSTOP;
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID./*EQUIPMENT_STATE_IDLE*/EQUIPMENT_STATE_DOWN);
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "EMS";
                    LogWR_.SaveLogOperate("EMS", "MC");
                }//비상정지 상태로
                
                if (eMCStatus == eMachineStatus.INITIAL && (bPushStop_Rec || bOnERROR || IsBIT[B.InitFail])){
                    O.RESET_DOORLOCK();
                    eMCStatus = eMachineStatus.READYSTOP;
                    bAllHomeComplete = false;
                    bInitialComplete = false;
                
                    for (int i = 0; i < CNT_.MT; i++) LAB_.MTESTOP(i, "CLS_OP -> INITIAL STOP");
                    W.ViewWarning(T.Op, W.AllHomeFail);
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveLogOperate("INITIAL FAIL", "MC");
                }//초기화 중지
                
                if (eMCStatus == eMachineStatus.EMSSTOP && !bPushEms && bPushReset){
                    eMCStatus = eMachineStatus.NONE; //비상정지 -> NONE(RESET SW PUSH)
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                }
                if (eMCStatus == eMachineStatus.NONE && !bPushEms){
                    eMCStatus = eMachineStatus.READYSTOP; //NONE ->READY STOP 상태로.
                    //SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                }
                if ((eMCStatus == eMachineStatus.READYSTOP || eMCStatus == eMachineStatus.WAITRUN || eMCStatus == eMachineStatus.USERSTOP || eMCStatus == eMachineStatus.ERRSTOP) && bPushInitial && !bOnERROR && !bPushStop_Rec){
                    if (!mDOOR_SKIP) O.SET_DOORLOCK();
                    eMCStatus = eMachineStatus.INITIAL;
                    for (int i = 0; i < CNT_.MT; i++) mtSTS[i].strHome = "";
                    ResetProgram();
                    sLOG = "";
                    sINIT = "";
                    cMATH.GET_CPU_CLOCK(ref lTimeInitialStartTime);
                    bLotEnd = false;
                    B.Bit(T.Op, B.InitFail, false, "[초기화] 전체 초기화 실패 플러그 OFF"); 
                    bInitialComplete = false;
                    iMANUAL.Number = ManualNumber.AllHome;
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    bMF = true;
                    LogWR_.SaveLogOperate("INITIAL START", "MC");
                }//초기화
                
                if (bLotEndProcess){
                    if (eMCStatus == eMachineStatus.AUTO){
                        mIN[I.vtStop] = true;
                        UTIL_.DELAY(100);
                    }
                    else{
                        LogWR_.SaveLogOperate("LOT-END", "MC");
                        bLotEndProcess = false;
                        B.Bit(T.Op, B.LotEnd, false, "LOT-END 플로그 OFF");
                    }
                }//LOT-END 후 시퀸스 초기화
                #endregion
            } while (true);
        }

        #region >>Thread Handling
        bool CheckStartCondition(){
            prMACHINE[CP.ManualRunRate] = 50;
            dRunRate = prMACHINE[CP.RunRate];
            if (!UTIL_.ChkAllReadyRun(PATH_.EXE_NAME)){
                E.OnERROR(E.PreRunPgm);
                return false;
            }
            if (!UTIL_.CHK_JOB_FILE()){
                bJobMiss = true;
                W.ViewWarning(-1, W.JogMiss);
                return false;
            }

            if (bMF){
                W.ViewWarning(-1, W.ManualNotComplete);
                return false;
            }

#if _NSS3300
#else
            if (prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP)    mOUT[O.QUAD_PCB] = false;
            else                                            mOUT[O.QUAD_PCB] = true;
#endif

            if (!mIN[I.CAM_CAL_ZIG_BWD] && !bBD){ 
                E.OnERROR(E.emsCalZigNotBackPos);
                return false;
            }
            if (bSTART_INTRK_CHK){ // 정지상태에서 인터록 조건 변경되었는지 확인.
                bSTART_INTRK_CHK = false;
                bool bCHK_INTRK = false;
                for (int i = eEMSBegin; i < CNT_.ERR; i++){
                    if (IsChkErr[i - eEMSBegin] != IsERR[i]){
                        UTIL_.SYSTEM_MESSAGE(i, false);
                        bCHK_INTRK = true;
                    }
                }
                if (bCHK_INTRK) return false;
            }
            if (!I.CHK_DOOR()){
                W.ViewWarning(-1, W.ChkDoor, sWarnningMessage);
                return false;
            }

            if (!DEF.ChkUsePicker()){
                W.ViewWarning(-1, W.HDPkrNotUse);
                return false;
            }

            if (prMACHINE[CP.UseTopInspection] == (int)eUSE.USE || prMACHINE[CP.UesBtmInspection] == (int)eUSE.USE){
                if (!mIN[I.VisionRdy] && !bDRYRUN){
                    W.ViewWarning(-1, W.NotRunVision);
                    return false;
                }
            }

            if (prMACHINE[CP.UseITSData] == (int)eUSE.USE){
                if (!MsSQL.bITSDataReading){
                    W.ViewWarning(-1, W.NotLotLoading);
                    return false;
                }
            }

            if (SUBFRM_.gLotID.bLotInView){
                //LOT 등록 창 열려 있음 창 닫고 실행 하셔야 합니다. 

            }

            if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.STACKER){
                if (prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD1){
                    // OK2 트레이 피더 클램프 상태 및 트레이 유무 확인
#if _NSS3300
                    if (mOUT[O.GOOD_TRAY2_UNGRIP_C] && mOUT[O.GOOD_TRAY2_UNGRIP_S] && !mOUT[O.GOOD_TRAY2_GRIP_C] && !mOUT[O.GOOD_TRAY2_GRIP_S]){
#else
                    if (mOUT[O.GOOD_TRAY2_UNGRIP_C] && mOUT[O.GOOD_TRAY2_UNGRIP_S] && !mOUT[O.GOOD_TRAY2_GRIP_C] && !mOUT[O.GOOD_TRAY2_GRIP_S]){
#endif
                    }
                    else{
                        W.ViewWarning(T.Op, W.RemoveGoodTray, "OK 트레이2 피더에 안착 되어 있는 트레이 제거 하거나 피더 클램프 오픈하셔야 합니다 !");
                        return false;
                    }
                }
                else if (prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD2){
                    // OK1 트레이 피더 클램프 상태 및 트레이 유무 확인
#if _NSS3300
                    if (mOUT[O.GOOD_TRAY1_UNGRIP_C] && mOUT[O.GOOD_TRAY1_UNGRIP_S] && !mOUT[O.GOOD_TRAY1_GRIP_C] && !mOUT[O.GOOD_TRAY1_GRIP_S]){
#else
                    if (mOUT[O.GOOD_TRAY1_UNGRIP_C] && mOUT[O.GOOD_TRAY1_UNGRIP_S] && !mOUT[O.GOOD_TRAY1_GRIP_C] && !mOUT[O.GOOD_TRAY1_GRIP_S]){
#endif
                    }
                    else { 
                        W.ViewWarning(T.Op, W.RemoveGoodTray, "OK 트레이1 피더에 안착 되어 있는 트레이 제거 하거나 피더 클램프 오픈하셔야 합니다 !");
                        return false;
                    }
                }
            }
            if (prMACHINE[CP.UseABF] == (int)eUSE.USE && prMACHINE[CP.UseMES] == (int)eUSE.USE) {
                if (mIN[I.SAW_BLADE_CHANGE]){
                    mOUT[O.HANDLER_BLADE_CHANGE_RESET] = true;
                    CLOT.bBladeInfo_Sp1 = UTIL_.GET_SPINDLE_BLADE_BARCODE(eSPINDLE.SP1);
                    CLOT.bBladeInfo_Sp2 = UTIL_.GET_SPINDLE_BLADE_BARCODE(eSPINDLE.SP2);
                    string sCurLotInfo = CLOT.GET_LOT.LotID + "," + CLOT.GET_LOT.LotType + "," + CLOT.GET_LOT.Qty + "," + CLOT.GET_LOT.ProductType + "," + CLOT.GET_LOT.ToolNo + "," +
                                    CLOT.GET_LOT.ITS + "," + CLOT.GET_LOT.ITS_LotID_IN + "," + CLOT.GET_LOT.ITS_LotID_CT + "," +
                                    CLOT.GET_LOT.UnitSizeX + "," + CLOT.GET_LOT.UnitSizeY + "," + CLOT.GET_LOT.UnitSize_USL + "," + CLOT.GET_LOT.UnitSize_LSL + "," + CLOT.GET_LOT.ABFMATERIAL + "," +
                                    CLOT.GET_LOT.LANDPKGX + "," + CLOT.GET_LOT.LANDPKGX_UPPER + "," + CLOT.GET_LOT.LANDPKGX_LOWER + "," + CLOT.GET_LOT.LANDPKGY + "," + CLOT.GET_LOT.LANDPKGY_UPPER + "," + CLOT.GET_LOT.LANDPKGY_LOWER + "," +
                                    CLOT.GET_LOT.WorkSort + "," + CLOT.GET_LOT.WorkScope + "," + CLOT.GET_LOT.BarcodeSp1 + "," + CLOT.GET_LOT.BarcodeSp2 + "," +
                                    CLOT.GET_LOT.Thick + "," + CLOT.GET_LOT.Thick_USL + "," + CLOT.GET_LOT.Thick_LSL + "," +
                                    CLOT.GET_LOT.ProcCD + "," + CLOT.GET_LOT.ProcName + "," + CLOT.GET_LOT.WorkCondition + "," + CLOT.GET_LOT.ProcCondition_1 + "," + CLOT.GET_LOT.ProcCondition_2 + "," + CLOT.GET_LOT.ProcCondition_3 + "," + CLOT.GET_LOT.ProcCondition_4 + "," + 
                                    CLOT.GET_LOT.BeginTime + "," +
                                    CLOT.GET_LOT.BOT_LANDTOPKG_X + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_X + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_X + "," + CLOT.GET_LOT.BOT_LANDTOPKG_Y + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TM_Y + "," + CLOT.GET_LOT.BOT_CHAMFERLEN_TP_Y + "," +
                                    CLOT.GET_LOT.TOP_LANDTOPKG_X + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_X + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_X + "," + CLOT.GET_LOT.TOP_LANDTOPKG_Y + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TM_Y + "," + CLOT.GET_LOT.TOP_CHAMFERLEN_TP_Y;
                    UTIL_.SET_LOT_INFO(sCurLotInfo);
                }
                if (CLOT.bABF){
                    if (CLOT.bBladeInfo_Sp1 && CLOT.bBladeInfo_Sp2){
                        if (CLOT.GET_LOT.BarcodeSp1 == "" || CLOT.GET_LOT.BarcodeSp2 == ""){
                            W.ViewWarning(T.Op, W.ABF_MESSAGE, "현재 스핀들 블레이드 바코드 입력 되어 있지 않습니다 !" + ETC.NewLine + "스핀들 블레이드 바코드 입력 확인 바랍니다 !", true);
                            return false;
                        }
                        if (!DEF.CheckingABFInterlock()){
                            W.ViewWarning(T.Op, W.ABF_MESSAGE, "현재 진행 LOT에서는 다이싱 쏘 설비에 장착된 스핀들 블레이드로 설비 구동 할 수 없습니다 !" + ETC.NewLine + "현재 진행 LOT 자재명 과 스핀들 블레이드 확인 바랍니다 !", true);
                            return false;
                        }
                    }
                    else{
                        W.ViewWarning(T.Op, W.ABF_MESSAGE, "다이싱 쏘 설비에서 스핀들 블레이드 정보 리딩 되어 있지 않습니다 !" + ETC.NewLine + "확인 후 다시 진행 하셔야 합니다 !", true);
                        return false;
                    }
                }
                else{
                    W.ViewWarning(T.Op, W.ABF_MESSAGE, "MES 자재명으로 블레이드 바코드 비교 사용 진행 중 자재명 리스트 파일 없어서 비교 불가 합니다." + ETC.NewLine + "블레이드 바코드 비교 없이 설비 진행 하시겠습니까 ?", false);
                    while (W.WaitWarning(T.Op, W.ABF_MESSAGE, "MES 자재명으로 블레이드 바코드 비교 사용 진행 중 자재명 리스트 파일 없어서 비교 불가 합니다.")) ;
                    if (!ConfirmUser[W.ABF_MESSAGE].result) return false;
                }
            }
            
            if (IsBIT[B.LotStart_KitCleanning]){
                if (IsBIT[B.KitCleaning]){
                    IsBIT[B.KitCleaning] = false;
                    LogWR_.SaveLogOperate("키트 클린 진행 후 작업 진행 하셔야 합니다.", "MC");
                }
                W.ViewWarning(T.Op, W.KIT_CLEANNING, "키트 클린 진행 후 작업 진행 하셔야 합니다." + ETC.NewLine + "(YES : 키트 클린 완료 함 / NO : 키트 클린 진행 완료 안함)");
                while (W.WaitWarning(T.Op, W.KIT_CLEANNING, "키트 클린 진행 여부 확인")) ;
                if (!ConfirmUser[W.KIT_CLEANNING].result){
                    LogWR_.SaveLogOperate("키트 클린 진행 중 입니다.", "MC");
                    return false;
                }
                IsBIT[B.LotStart_KitCleanning] = false;
                LogWR_.SaveLogOperate("키트 클린 진행 완료 하였습니다.", "MC");
            }
            return true;
        }

        private bool RunThread(){
            if (!CheckStartCondition()) return false;
            bBzSTOP = false;
            for (int t = 0; t < CNT_.THREAD; t++){
                if (!UseThread[t]) continue;
                if (COM_.GetThreadState(t, ThreadState.Unstarted)) mcTH[t].Start();
                else if (COM_.GetThreadState(t, ThreadState.Suspended)) mcTH[t].Resume();
                mcTH[t].Join(1);
            }
            LogWR_.SaveLogOperate("START", "MC");
            return true;
        }
        private void StopThread(){
            for (int t = 0; t < CNT_.THREAD; t++){
                if (mcTH[t] == null || COM_.NotCheckThread(t)) continue;
                try{
                    if (COM_.GetThreadState(t, ThreadState.Running) || COM_.GetThreadState(t, ThreadState.WaitSleepJoin)) mcTH[t].Suspend();
                }
                catch (Exception ex){
                    LogWR_.SaveLogException("MrgOP -> StopThread()", ex);
                    continue;
                }
            }
        }
        private void ResetProgram(){
            DeleteThread();
            MakeThread();
            UTIL_.DELAY(500);
        }

        void DeleteThread(){
            double spd = prMACHINE[CP.RunRate];
            prMACHINE[CP.RunRate] = 1;
            for (int t = 0; t < CNT_.THREAD; t++){
                if (mcTH[t] == null) continue;
                if (COM_.NotCheckThread(t)) continue;
                if (COM_.GetThreadState(t, ThreadState.Unstarted)) continue; //ThreadState.Unstarted = Thread.Start 스레드에서 메서드를 호출 되지 않았습니다.
                if (COM_.GetThreadState(t, ThreadState.Suspended) || !COM_.GetThreadState(t, ThreadState.Running)){
                    //ThreadState.Suspended = 스레드가 일시 중단 되었습니다. / ThreadState.Running = 스레드가 시작 되었는지, 차단 되지 않으며, 및는 보류 중인 더 ThreadAbortException합니다.
                    mcTH[t].Resume();
                }
                UTIL_.DELAY(10);
                mcTH[t].Abort();
            }
            prMACHINE[CP.RunRate] = spd;
        }
        void MakeThread(){
            COM_.MAKE_THRAED(ref mcTH[T.Magazine], C.Magazine.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.Gripper], C.Gripper.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.StripPk], C.StripPk.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.UnitPk], C.UnitPk.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.DryTable1], C.DryTable1.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.DryTable2], C.DryTable2.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.Head1], C.Head1.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.Head2], C.Head2.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.EmptyStacker], C.EmptyStacker.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.TrayPk], C.TrayPk.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.GoodTrayFeeder1], C.GoodTrayFeeder1.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.GoodTrayFeeder2], C.GoodTrayFeeder2.DoAuto);
            COM_.MAKE_THRAED(ref mcTH[T.ReworkTrayFeeder], C.ReworkTrayFeeder.DoAuto);
        }
        void ReCreateManualThread(){
            COM_.RECREATE_THREAD(ref mcTH[T.Manual], C.Manual.Do);
        }
#endregion
    }

    public class CHECK_STOP_EVENT : DATA_{
        public void DoReadStopEvent(){
            do{
                if (gExit) break;
                UTIL_.DELAY(3);
                if (bPushStop){
                    bPushStop_Rec = true;
                    UTIL_.DELAY(500);
                    bPushStop_Rec = false;
                }
            } while (true);
        }
    }
}