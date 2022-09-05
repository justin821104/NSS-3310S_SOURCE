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
                    if (prMACHINE[CP.UseTopInspection] == (int)eUSE.NotUSE){
                        COM_.ViewWarning(T.Op, W.UnitSizeInspectionSkip);
                        while (UTIL_.WaitWarning(T.Op, W.UnitSizeInspectionSkip, "TOP VISION SKIP 상태로 START 진행")) ;
                        if (ConfirmUser[W.UnitSizeInspectionSkip].result){
                            mIN[I.vtStart] = false;
                            bPushStart = true;
                        }//YES
                        else{
                            mIN[I.vtStart] = false;
                        }//NO
                    }
                    else{
                        if (prMACHINE[CP.UseTopInspectionResult] == (int)eUSE.NotUSE){
                            COM_.ViewWarning(T.Op, W.UnitSizeReturnValueSkip);
                            while (UTIL_.WaitWarning(T.Op, W.UnitSizeReturnValueSkip, "TOP VISION 검사 결과 SKIP 상태로 START 진행")) ;
                            if (ConfirmUser[W.UnitSizeReturnValueSkip].result){
                                mIN[I.vtStart] = false;
                                bPushStart = true;
                            }//YES
                            else{
                                mIN[I.vtStart] = false;
                            }//NO
                        }
                        else{
                            mIN[I.vtStart] = false;
                            bPushStart = true;
                        }
                    }
                }//START 

                if (!mIN[I.STOP] && !mIN[I.vtStop]) bPushStop = false;
                else{
                    bPushStop = true;
                    mIN[I.vtStop] = false;
                    if (mOUT[O.CLEANER_WATER_1] || mOUT[O.CLEANER_WATER_2]){
                        mOUT[O.CLEANER_WATER_1] = false;
                        mOUT[O.CLEANER_WATER_2] = false;
                    }
                    if (IsBIT[B.SawManualEvent]) C.SendSaw.FailSawManualEvent("FAIL MANUAL-RUN.");
                    COM_.STOP_ACMOTOR();
                    LAB_.MT_ALL_STOP(false, "OP -> RUN() -> MT_ALL_STOP" + ETC.CrLf + "PUSH STOP");
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
                    COM_.BZ_OFF();
                }//RESET

                if (!mIN[I.vtInitial]) bPushInitial = false;
                else{
                    mIN[I.vtInitial] = false;
                    bPushInitial = true;
                }//INITIAL

                if (bOnERROR && bPushReset){
                    UTIL_.CLEAR_ERROR();
                    LogWR_.SaveLogOperate("ERROR RESET", "MC");
                }//ERROR RESET
                #endregion

                #region >>Machine State
                if (eMCStatus == eMachineStatus.INITIAL && bInitialComplete && !bOnERROR){
                    eMCStatus = eMachineStatus.WAITRUN;
                    bEndInitial = true;
                    COM_.RESET_DOORLOCK();
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveLogOperate("INITIAL END", "MC");
                }//초기화 -> 운전대기 모드
                
                if ((eMCStatus == eMachineStatus.ERRSTOP || eMCStatus == eMachineStatus.USERSTOP || eMCStatus == eMachineStatus.WAITRUN) && bPushStart && bInitialComplete && !bOnERROR && (eLevelMainSw == eMainLevel.AUTO)){
                    if (!mDOOR_SKIP) COM_.SET_DOORLOCK();
                    if (RunThread()){
                        if (eMCStatus == eMachineStatus.USERSTOP) LogWR_.SaveMARS("MACHINE STOP", "OTHER", "STOP", 100);
                        if (eMCStatus == eMachineStatus.ERRSTOP) LogWR_.SaveMARS("MACHINE ERROR", "OTHER", "STOP", 100);
                        LogWR_.SaveMARS("MACHINE RUN", "OTHER", "START", 100);
                        for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "START";
                        SUBFRM_.gLOG.ERR_CLEAR();
                        bBzSTOP = false;
                        bOVER_5MINUTE = false;
                        eMCStatus = eMachineStatus.AUTO;
                        COM_.Bit(T.Op, B.MachineWaitProduct, false, "[STOP상태->RUN상태] 자재 없음 플러그 OFF");
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
                    COM_.RESET_DOORLOCK();
                    StopThread();
                    eMCStatus = eMachineStatus.USERSTOP;
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "STOP";
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveMARS("MACHINE RUN", "OTHER", "STOP", 100);
                    LogWR_.SaveMARS("MACHINE STOP", "OTHER", "START", 100);
                }//자동운전 -> USER STOP
                
                if ((eMCStatus == eMachineStatus.AUTO) && bOnERROR){
                    C.UnitPk.Cleaner(stBIT.OFF);
                    COM_.RESET_DOORLOCK();
                    StopThread();
                    LAB_.MT_ALL_STOP(false, "CLS_OP -> RUN()" + ETC.CrLf + "eMachineStatus.ERRSTOP");
                    eMCStatus = eMachineStatus.ERRSTOP;
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "ERR";
                    mOUT[O.BUZZER_ERR] = true;
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    LogWR_.SaveMARS("MACHINE RUN", "OTHER", "STOP", 100);
                    LogWR_.SaveMARS("MACHINE ERROR", "OTHER", "RUN", 100);
                }//자동운전 -> ERROR STOP
                
                if (eMCStatus != eMachineStatus.EMSSTOP && bPushEms){
                    COM_.RESET_DOORLOCK();
                    if (eMCStatus == eMachineStatus.AUTO) StopThread();
                    eMCStatus = eMachineStatus.EMSSTOP;
                    SUBFRM_.gSecsGem.SetPrecessState((int)CCEID.EQUIPMENT_STATE_IDLE);
                    for (int i = 0; i < CNT_.MT; i++) mtCHK[i].Ev = "EMS";
                    LogWR_.SaveLogOperate("EMS", "MC");
                }//비상정지 상태로
                
                if (eMCStatus == eMachineStatus.INITIAL && (bPushStop_Rec || bOnERROR || IsBIT[B.InitFail])){
                    COM_.RESET_DOORLOCK();
                    eMCStatus = eMachineStatus.READYSTOP;
                    bAllHomeComplete = false;
                    bInitialComplete = false;
                
                    for (int i = 0; i < CNT_.MT; i++) LAB_.MTESTOP(i, "CLS_OP -> INITIAL STOP");
                    COM_.ViewWarning(T.Op, W.AllHomeFail);
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
                    if (!mDOOR_SKIP) COM_.SET_DOORLOCK();
                    eMCStatus = eMachineStatus.INITIAL;
                    for (int i = 0; i < CNT_.MT; i++) mtSTS[i].strHome = "";
                    ResetProgram();
                    sLOG = "";
                    sINIT = "";
                    cMATH.GET_CPU_CLOCK(ref lTimeInitialStartTime);
                    bLotEnd = false;
                    COM_.Bit(T.Op, B.InitFail, false, "[초기화] 전체 초기화 실패 플러그 OFF"); 
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
                        COM_.Bit(T.Op, B.LotEnd, false, "LOT-END 플로그 OFF");
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
                UTIL_.OnERROR(E.PreRunPgm);
                return false;
            }
            if (!UTIL_.CHK_JOB_FILE()){
                bJobMiss = true;
                COM_.ViewWarning(-1, W.JogMiss);
                return false;
            }

            if (bMF){
                COM_.ViewWarning(-1, W.ManualNotComplete);
                return false;
            }

#if _NSS3300
#else
            if (prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP)    mOUT[O.QUAD_PCB] = false;
            else                                            mOUT[O.QUAD_PCB] = true;
#endif

            if (!mIN[I.CAM_CAL_ZIG_BWD] && !bBD){ 
                UTIL_.OnERROR(E.emsCalZigNotBackPos);
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
            if (!COM_.CHK_DOOR()){
                COM_.ViewWarning(-1, W.ChkDoor, sWarnningMessage);
                return false;
            }

            if (!DEF.ChkUsePicker()){
                COM_.ViewWarning(-1, W.HDPkrNotUse);
                return false;
            }

            if (prMACHINE[CP.UseTopInspection] == (int)eUSE.USE || prMACHINE[CP.UesBtmInspection] == (int)eUSE.USE){
                if (!mIN[I.VisionRdy] && !bDRYRUN){
                    COM_.ViewWarning(-1, W.NotRunVision);
                    return false;
                }
            }

            if (prMACHINE[CP.UseITSData] == (int)eUSE.USE){
                if (!MsSQL.bITSDataReading){
                    COM_.ViewWarning(-1, W.NotLotLoading);
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
                        COM_.ViewWarning(T.Op, W.RemoveGoodTray, "OK 트레이2 피더에 안착 되어 있는 트레이 제거 하거나 피더 클램프 오픈하셔야 합니다 !");
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
                        COM_.ViewWarning(T.Op, W.RemoveGoodTray, "OK 트레이1 피더에 안착 되어 있는 트레이 제거 하거나 피더 클램프 오픈하셔야 합니다 !");
                        return false;
                    }
                }
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