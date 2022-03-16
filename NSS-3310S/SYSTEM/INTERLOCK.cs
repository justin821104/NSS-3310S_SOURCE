using NSS_3310S;
using Object;

namespace nINTERLOCK{
    public class INTERLOCK : DATA_ {
        public bool Check_Ccw(int m) {
            switch (m) {

                default:
                    break;
            }
            return true;
        }
        public bool Check_Cw(int m) {
            switch (m) {

                default:
                    break;
            }
            return true;
        }

        static void IsInterlock(int nBit, bool bStatus) { bINTRK[nBit - eEMSBegin] = bStatus; }
        public void CheckInterlock() {
            for (int m = 0; m < CNT_.MT; m++) LAB_.CHK_CURRENT_STATUS(m, 2);

            if (mIN[I.PUSHER_FWD] || (mOUT[O.PUSHER_FWD] && !mOUT[O.PUSHER_BWD]))               IsInterlock(E.emsPusherNotBwd, true);
            else                                                                                IsInterlock(E.emsPusherNotBwd, false);

            if (mIN[I.INLET_TABLE_UP] || (mOUT[O.INLET_TABLE_UP] && !mOUT[O.INLET_TABLE_DN]))   IsInterlock(E.emsInLetTableNotDown, true);
            else                                                                                IsInterlock(E.emsInLetTableNotDown, false);

            if (mtDATA[M.GrpX, P.StripOpn].Pos + 1 <= mtSTS[M.GrpX].CurrentPosition)            IsInterlock(E.emsGripperNotMoveRdy, true);
            else                                                                                IsInterlock(E.emsGripperNotMoveRdy, false);

            if (mtDATA[M.GrpX, P.Ready].Pos + 1 < mtSTS[M.GrpX].CurrentPosition)                IsInterlock(E.emsGripperNotRdyPos, true);
            else                                                                                IsInterlock(E.emsGripperNotRdyPos, false);

            if (mtDATA[M.StripPkX, P.StripPick].Pos > mtSTS[M.StripPkX].CurrentPosition) {
                if (P.StripPkZSafetyPos <= mtSTS[M.StripPkZ].CurrentPosition)                   IsInterlock(E.emsStripPkZNotReadyPos, true);
                else                                                                            IsInterlock(E.emsStripPkZNotReadyPos, false);
            }
            else                                                                                IsInterlock(E.emsStripPkZNotReadyPos, false);

            if (P.UnitPkZSafetyPos <= mtSTS[M.UnitPkZ].CurrentPosition) IsInterlock(E.emsUnitPkZnotSafetyLocation, true);
            else IsInterlock(E.emsUnitPkZnotSafetyLocation, false);

            if (mtDATA[M.StripPkX, P.StripPckUp].bPOS) IsInterlock(E.emsNotStripPkXPicPos, false);
            else IsInterlock(E.emsNotStripPkXPicPos, true);

            if (mtDATA[M.StripPkX, P.StripPlc].bPOS) IsInterlock(E.emsNotStripPkXPlcPos, false);
            else IsInterlock(E.emsNotStripPkXPlcPos, true);


            if (mtDATA[M.UnitPkX, P.UnitPckUp].bPOS) IsInterlock(E.emsNotUnitPkXPicPos, false);
            else IsInterlock(E.emsNotUnitPkXPicPos, true);

            if (mtDATA[M.UnitPkX, P.Cleaner].bPOS) IsInterlock(E.emsNotCleanerPos, false);
            else IsInterlock(E.emsNotCleanerPos, true);

            if (mtDATA[M.UnitPkX, P.PlacePallet1].bPOS) IsInterlock(E.emsNotUnitPkXStage1Pos, false);
            else IsInterlock(E.emsNotUnitPkXStage1Pos, true);

            if (mtDATA[M.UnitPkX, P.PlacePallet2].bPOS) IsInterlock(E.emsNotUnitPkXStage2Pos, false);
            else IsInterlock(E.emsNotUnitPkXStage2Pos, true);

            if (mtDATA[M.UnitPkX, P.PlacePallet1].bPOS && mtSTS[M.UnitPkZ].CurrentPosition > mtDATA[M.UnitPkZ, P.Ready].Pos + 5) IsInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true);
            else IsInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, false);

            if (mtDATA[M.UnitPkX, P.PlacePallet2].bPOS && mtSTS[M.UnitPkZ].CurrentPosition > mtDATA[M.UnitPkZ, P.Ready].Pos + 5) IsInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true);
            else IsInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, false);

            if (mtDATA[M.Table1, P.RecieveUnit].bPOS) IsInterlock(E.emsNotStage1Pos, false);
            else IsInterlock(E.emsNotStage1Pos, true);

            if (mtDATA[M.Table2, P.RecieveUnit].bPOS) IsInterlock(E.emsNotStage2Pos, false);
            else IsInterlock(E.emsNotStage2Pos, true);

            if (prMACHINE[CP.StripPkSafetyPosition] < mtSTS[M.StripPkX].CurrentPosition) IsInterlock(E.emsStripPkXSafetyPosition, true);
            else IsInterlock(E.emsStripPkXSafetyPosition, false);

            if (prMACHINE[CP.UnitPkSafetyPosition] < mtSTS[M.UnitPkX].CurrentPosition) IsInterlock(E.emsUnitPkXSafetyPosition, true);
            else IsInterlock(E.emsUnitPkXSafetyPosition, false);

            if (mIN[I.EMPTY_FEEDER_FWD]) IsInterlock(E.emsTrayPkPickUpFail_EmptyTray, false);
            else IsInterlock(E.emsTrayPkPickUpFail_EmptyTray, true);

            if (mtDATA[M.TrayFeeder3, P.TrayLoad].bPOS) IsInterlock(E.emsTrayPkPickUpFail_ReworkTray, false);
            else IsInterlock(E.emsTrayPkPickUpFail_ReworkTray, true);

            if (mtDATA[M.TrayFeeder1, P.TrayLoad].bPOS) IsInterlock(E.emsTrayPkPickUpFail_GoodTray1, false);
            else IsInterlock(E.emsTrayPkPickUpFail_GoodTray1, true);

            if (mtDATA[M.TrayFeeder2, P.TrayLoad].bPOS) IsInterlock(E.emsTrayPkPickUpFail_GoodTray2, false);
            else IsInterlock(E.emsTrayPkPickUpFail_GoodTray2, true);

            if (mIN[I.NG_STACKER_DN]) IsInterlock(E.emsReworkStackerNotDown, false);
            else IsInterlock(E.emsReworkStackerNotDown, true);

            if (mIN[I.NG_RAIL_STACKER_TRAY_CHECK]) IsInterlock(E.emsRworkStackerTraySensing, false);
            else IsInterlock(E.emsRworkStackerTraySensing, true);

            if (mIN[I.GOOD_STACKER_DN]) IsInterlock(E.emsGoodStackerNotDown, false);
            else IsInterlock(E.emsGoodStackerNotDown, true);

            if (mIN[I.GOOD_RAIL_STACKER_CHECK]) IsInterlock(E.emsGoodStackerTraySensing, false);
            else IsInterlock(E.emsGoodStackerTraySensing, true);

            if (mtDATA[M.TrayPickerX, P.OKTrayPlc].bPOS && mtSTS[M.TrayPickerZ].CurrentPosition > mtDATA[M.TrayPickerZ, P.Ready].Pos + 10) IsInterlock(E.emsNotMoveGoodTrayFeederBecauseTrayPk, true);
            else IsInterlock(E.emsNotMoveGoodTrayFeederBecauseTrayPk, false);

            if (mtDATA[M.TrayPickerX, P.NGTrayPlc].bPOS && mtSTS[M.TrayPickerZ].CurrentPosition > mtDATA[M.TrayPickerZ, P.Ready].Pos + 10) IsInterlock(E.emsNotMoveReworkTrayFeederBecauseTrayPk, true);
            else IsInterlock(E.emsNotMoveReworkTrayFeederBecauseTrayPk, false);

            if (mIN[I.NG_RAIL_LOADING_TRAY_CHECK]) IsInterlock(E.emsReworkLoadingLocationSensing, false);
            else IsInterlock(E.emsReworkLoadingLocationSensing, true);

            if (mIN[I.GOOD_RAIL_TRAY_LOADING_CHECK]) IsInterlock(E.emsGoodLoadingLocationSensing, false);
            else IsInterlock(E.emsGoodLoadingLocationSensing, true);

            if (mIN[I.NG_RAIL_HEAD1_TRAY_CHECK] && mIN[I.NG_RAIL_HEAD2_TRAY_CHECK]) IsInterlock(E.emsRworkPlaceLocationSensing, false);
            else IsInterlock(E.emsRworkPlaceLocationSensing, true);

            if (mIN[I.GOOD_RAIL_HEAD1_CHECK] && mIN[I.GOOD_RAIL_HEAD2_CHECK]) IsInterlock(E.emsGoodPlaceLocationSensing, false);
            else IsInterlock(E.emsGoodPlaceLocationSensing, true);
        }

        public bool ChkInterlock(int[] Interlock, bool WithError){
            CheckInterlock();
            bool bRTN = true;
            for (int i = 0; i < Interlock.Length; i++){
                if (UTIL_.OnINTERLOCK(Interlock[i], WithError)){
                    UTIL_.SYSTEM_MESSAGE(Interlock[i], false);
                    bRTN = false;
                }
            }
            return bRTN;
        }
        public bool ChkInterlock(int Interlock, bool WithError){
            CheckInterlock();
            bool bRTN = true;
            if (UTIL_.OnINTERLOCK(Interlock, WithError)){
                UTIL_.SYSTEM_MESSAGE(Interlock, false);
                bRTN = false;
            }
            return bRTN;
        }
        public bool ChkInterlock(int[] Interlock, bool WithError, eMachineStatus eCHK){
            CheckInterlock();
            bool bRTN = true;
            for (int i = 0; i < Interlock.Length; i++){
                if (eCHK == eMCStatus){
                    if (UTIL_.OnINTERLOCK(Interlock[i], WithError)){
                        UTIL_.SYSTEM_MESSAGE(Interlock[i], false);
                        bRTN = false;
                    }
                }
            }
            return bRTN;
        }
        public bool ChkInterlock(int Interlock, bool WithError, eMachineStatus eCHK){
            CheckInterlock();
            bool bRTN = true;
            if (eCHK == eMCStatus){
                if (UTIL_.OnINTERLOCK(Interlock, WithError)){
                    UTIL_.SYSTEM_MESSAGE(Interlock, false);
                    bRTN = false;
                }
            }
            return bRTN;
        }
    }

    public class WARNNING : DATA_{
        public void CheckWarnning(){
            do{
                if (gExit) break;
                UTIL_.DELAY(10);
                for (int n = 0; n < ConfirmUser.Length; n++){
                    if (!ConfirmUser[n].useable) continue;
                    ConfirmUser[n].num = n;
                    SetConfirMassage(n);
                    if (ConfirmUser[n].AfterReset && eMCStatus == eMachineStatus.ERRSTOP && bOnERROR) continue;
                    ConfirmG = ConfirmUser[n];
                    if (ConfirmG.msg == null){
                        ConfirmG.msg = "경고 메세지 입력 안되어 있습니다 !";
                    }
                    string[] nTEMP = ConfirmG.msg.Split('\n');
                    string Message = string.Empty;
                    for (int i = 0; i < nTEMP.Length; i++){
                        Message += nTEMP[i];
                    }

                    LogWR_.SaveLogWarning(ConfirmG.msg, "");
                    bViewConfirm = true;
                    while (ConfirmG.useable){
                        if (!ConfirmUser[n].useable && ConfirmUser[n].TypeOk) ConfirmG.useable = false;
                        if (gExit) return;
                        UTIL_.DELAY(10);
                    }
                    ConfirmUser[n] = ConfirmG;
                }
            } while (true);
        }

        static void SetConfirMassage(int nWAR){
            switch (nWAR){
                case W.JogMiss:
                case W.NotLotLoading:
                case W.NotLog:
                case W.ChkDoor:
                case W.EndInitial:
                    ConfirmUser[nWAR].TypeOk = true;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = -1;
                    break;

                case W.ProductRemove: //홈진행중 에러 발생시 경고 메세지
                case W.AllHomeFail:
                case W.ManualNotComplete:
                case W.DoorOpen:
                case W.ManualErrMassage:
                case W.SawStripReuestsingal:
                case W.UnitPlaceSignelOff:
                case W.WorkingCancel:
                case W.HDPkrNotUse:
                case W.WaitUnloaderConveyor:
                case W.PlaceFail:
                case W.DllWarnning:
                case W.ChkForm_LotIn:
                    ConfirmUser[nWAR].TypeOk = true;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = O.BUZZER_ERR;
                    break;

                case W.OldPasswordFail:
                case W.NewPasswordFail:
                case W.PasswordOK:
                    ConfirmUser[nWAR].TypeOk = true;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = -1;
                    break;

                case W.EmptyTray:
                case W.ReworkTrayFull:
                case W.GoodTrayFull:
                case W.NotRunSaw:
                case W.NotRunVision:
                case W.NotUnloaderConveyor:
                case W.LDCst_Requst:
                case W.ULDCst_FullCheck:
                case W.WorkEnd:
                    ConfirmUser[nWAR].TypeOk = true;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = O.BUZZER_END;
                    break;

                case W.LotEndComplete:
                    ConfirmUser[nWAR].TypeOk = false;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = O.BUZZER_END;
                    break;

                case W.BarcoderReadingFail:
                case W.GripperStripPicFail:
                case W.StripPk_RePic:
                case W.StripPk_Vanish:
                case W.CleanerWater:
                case W.UnitPkUnit_Vanish:
                case W.UnitInspectionNgCountOver:
                case W.PickUpFail:
                case W.TrayDisappear:
                case W.UnitPkrPickUpReCheck:
                case W.UnitPkrPickUnitCheck:
                case W.UnitSizeInspectionSkip:
                case W.UnitSizeReturnValueSkip:
                case W.XMarkInspectionFail:
                    ConfirmUser[nWAR].TypeOk = false;
                    ConfirmUser[nWAR].AfterReset = false;
                    ConfirmUser[nWAR].bz = O.BUZZER_ERR;
                    break;
            }
        }
    }
}