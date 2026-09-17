using LIB_.DateType;
using Object;
using System;

namespace NSS_3310S.SEQ.MODULE{
    public class EMPTY : BASE{
        readonly int nThread = T.EmptyStacker;
        long TackStart = 0, TackEnd = 0;

        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            do{
                if (gExit) break;
                if (!CheckRunThread()) continue;
                //if (bDRYRUN){
                //    IsBIT[B.EmptyTrayPicRequest] = true;
                //    continue;
                //}

                ReSUPPLY:
                while (B.WaitBIT(nThread, B.LotEnd, true, "LOT-END 처리 진행 중")) ;
                if (eRTN.SUCESS != GetEmptyTray("빈-트레이 공급")) goto ReSUPPLY;

                B.SetBit(nThread, B.EmptyTrayPicRequest, true, "빈-트레이 픽업 요청");
                while (B.WaitBIT(nThread, B.EmptyTrayPicRequest, true, "트레이 피커 빈-트레이 픽업 할때까지 대기")) ;
                Tack();
            } while (true);
        }

        #region >> SEQ
        public eRTN GetEmptyTray(string comment){
#if _NSS3300
            if (mIN[I.EMPTY_RAIL_LD_TRAY_CHECK]){
#else
            if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && !mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){       
#endif
                if (!bBD) {
                    E.OnERROR(E.emsEmptyTrayRailTrayExist);
                    return eRTN.FAIL;
                } // 보드 없을 경우 패스 (노트북 확인)
            }
            while (eRTN.SUCESS != TransferBwd("빈-트레이 피터 트레이 로딩부로 후진")) ;

            LogStart(nThread, comment + " 진행");
#if _NSS3300
            if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK]){
#else
            if (mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){
#endif
                while (eRTN.SUCESS != TransferUnGrip("빈-트레이 피터 트레이 언그립")) ;
            ReCheck:
                if (!mIN[I.EMPTY_STACKER_NONE] && !bDRYRUN){
                    bWaitProduct = true;
                    W.ViewWarning(nThread, W.EmptyTray);
                    while (W.WaitWarning(nThread, W.EmptyTray, "빈-트레이 공급 요청")) ;
                    B.SetBit(nThread, B.EmptyStackerSupply, true, "빈-트레이 공급 플로그");
                    while (B.WaitBIT(nThread, B.EmptyStackerSupply, true, "빈-트레이 공급 대기")) ;
                    bWaitProduct = false;
                    goto ReCheck;
                }
                while (eRTN.SUCESS != MoveZ(P.EmptyTrayHold, "", "빈-트레이 스태커 트레이 공급 위치 이송")) ;
                while (eRTN.SUCESS != MoveZ(P.EmptyTraySupply, "", "빈-트레이 스태커 스토퍼 언락 위치 이송")) ;
                while (eRTN.SUCESS != StopperUnlock("빈-트레이 스토퍼 언락")) ;
                while (eRTN.SUCESS != MoveZ(P.EmptyTrayHold, "", "빈-트레이 스태커 트레이 공급 위치 이송")) ;
                while (eRTN.SUCESS != StopperLock("빈-트레이 스토퍼 락")) ;
                while (eRTN.SUCESS != MoveZ(P.EmptyTraySafeArrial, "offset=5", "빈-트레이 레일 공급 대기 위치 이송")) ;
                while (eRTN.SUCESS != MoveZ(P.EmptyTraySafeArrial, "spd=5", "빈-트레이 레일 공급 위치 이송")) ;
            }
        ReCHECK_TRAY:
            UTIL_.DELAY(500);
            while (eRTN.SUCESS != TransferGrip("빈-트레이 피터 트레이 그립")) ;
#if _NSS3300
            if (!mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && !bDRYRUN){
#else
            if (mIN[I.EMPTY_RAIL_LD_TRAY_CHECK] && !bDRYRUN){
#endif
                E.OnERROR(E.EmptyTrayLoadingFail);
                goto ReCHECK_TRAY;
            }
            while (eRTN.SUCESS != TransferFwd("빈-트레이 피터 트레이 픽업부로 전진")) ;
            IsLONG[L.EmptyTrayCnt]++;
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }
#endregion

        #region >> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.EmptyCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",EMPTY," + IsDOUBLE[D.EmptyCycle].ToString(), "");
            TackStart = Environment.TickCount;
        }

        public eRTN StopperLock(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyStopperLock, I.EmptyStopperLock, I.EmptyStopperUnlock, O.EmptyStopperLock, O.EmptyStopperUnlock, (int)prMACHINE[CP.EmptyStopperLockDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN StopperUnlock(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyStopperUnlock, I.EmptyStopperUnlock, I.EmptyStopperLock, O.EmptyStopperUnlock, O.EmptyStopperLock, (int)prMACHINE[CP.EmptyStopperUnlockDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN TransferGrip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyFeederGrip, I.Null, I.EmptyFeederUnGrip, O.EmptyFeederGrip, O.EmptyFeederUnGrip, (int)prMACHINE[CP.FeederGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN TransferUnGrip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyFeederUnGrip, I.EmptyFeederUnGrip, I.Null, O.EmptyFeederUnGrip, O.EmptyFeederGrip, (int)prMACHINE[CP.FeederUnGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN TransferFwd(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyFeederFwdFail, I.EMPTY_FEEDER_FWD, I.EMPTY_FEEDER_BWD, O.EMPTY_TRAY_FWD, O.EMPTY_TRAY_BWD, (int)prMACHINE[CP.EmptyFeederFwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN TransferBwd(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.EmptyFeederBwdFail, I.EMPTY_FEEDER_BWD, I.EMPTY_FEEDER_FWD, O.EMPTY_TRAY_BWD, O.EMPTY_TRAY_FWD, (int)prMACHINE[CP.EmptyFeederBwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN MoveZ(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            IsSTRING[S.EmptyMessage] = comment + " " + LogWR_.LogPos(M.EmptyElv, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.EmptyElv, nPos, 0.005, false, false, true, cmd, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion
    }
}