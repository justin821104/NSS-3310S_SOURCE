using LIB_.DateType;
using Object;
using System;

namespace NSS_3310S.SEQ.MODULE{
    public class REWORK_TRAY_FEEDER : BASE{
        readonly int nThread = T.ReworkTrayFeeder;
        long TackStart = 0, TackEnd = 0;
        int nRtnPX = 0, nRtnPY = 0;

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

                GetEmptyTray("RE-WORK FEEDER 빈트레이 공급");
                TrayUnitPlace("RE-WORK 트레이 유닛 플레이스 작업");
                OutTray("REWORK 트레이 배출");
            } while (true);
        }

        #region >> SEQ
        public eRTN GetEmptyTray(string comment){
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveY(P.TrayLoad, "", "RE-WORK 트레이 피터 트레이 공급 위치 이송")) ;
            //트레이 유무 확인

            while (eRTN.SUCESS != UnGrip("RE-WORK TRAY FEEDER 언그립")) ;
            B.SetBit(nThread, B.ReWorkTrayRequest, true, "RE-WORK TRAY FEEDER 트레이 공급 요청");
            while (B.WaitBIT(nThread, B.ReWorkTrayRequest, true, "트레이 피커에서 트레이 공급 할때까지 대기")) ;
            while (eRTN.SUCESS != Grip("RE-WORK TRAY FEEDER 그립")) ;
            MAP_.TrayMap_Reset(eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY]);
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public eRTN TrayUnitPlace(string comment){
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveY(P.Tray_Place[(int)IsLONG[L.CurWorkXPlc]], "", "RE-WORK TRAY FEEDER 유닛 플레이스 대기 위치 이송")) ;

            if (-1 != MAP_.GetTrayPocket(eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref nRtnPX, ref nRtnPY)){
                B.SetBit(nThread, B.ReWorkTrayWork, true, "RE-WORK 트레이 유닛 플레이스 작업 진행");
                while (B.WaitBIT(nThread, B.ReWorkTrayWork, true, "REWORK 트레이 유닛 플레이스 완료 할때까지 대기")) ;
            }
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public eRTN OutTray(string comment){
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != StackerTableDn("RE-WORK 스태커 테이블 다운")) ;
            while (eRTN.SUCESS != MoveY(P.Staker, "", "RE-WORK FEEDER Y축 스태커 배출 위치 이송")) ;
            while (eRTN.SUCESS != UnGrip("RE-WORK FEEDER 언그립")) ;
            while (eRTN.SUCESS != StackerTableUp("RE-WORK 스태커 테이블 업")) ;
            while (eRTN.SUCESS != StackerTableDn("RE-WORK 스태커 테이블 다운")) ;
            IsLONG[L.ReworkTrayCnt]++;
            LogEnd(nThread, comment + " 완료");
        ReCHECK:
            if (mIN[I.NG_TRAY_STACKER_FULL] && !bMF){
                TraySupply("REWORK 스태커 트레이 배출 요청");
                goto ReCHECK;
            }
            Tack();
            return eRTN.SUCESS;
        }

        void TraySupply(string comment){
            LogStart(nThread, comment + " 진행");
            W.ViewWarning(nThread, W.ReworkTrayFull);
            B.SetBit(nThread, B.ReWorkTrayStackerUldRequest, true, "RE-WORK 스태커 트레이 배출 할대 까지 대기");
            while (B.WaitBIT(nThread, B.ReWorkTrayStackerUldRequest, true, "RE-WORK STACKER 트레이 배출 대기")) ;
            LogEnd(nThread, comment + " 완료");
        }
        #endregion

        #region >> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.Tray3Cycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",REWORK 트레이," + IsDOUBLE[D.Tray3Cycle].ToString(), "");
            TackStart = Environment.TickCount;
        }

        public eRTN Grip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.ReWorkFeederGrip, I.ReworkFeederGrip, I.ReworkFeederUnGrip, O.ReWorkFeederGrip, O.ReWorkFeederUnGrip, (int)prMACHINE[CP.FeederGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN UnGrip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.ReWorkFeederUnGrip, I.ReworkFeederUnGrip, I.ReworkFeederGrip, O.ReWorkFeederUnGrip, O.ReWorkFeederGrip, (int)prMACHINE[CP.FeederUnGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN StackerTableUp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.ReWorkStackerUpFail, I.NG_STACKER_UP, I.NG_STACKER_DN, O.NG_STACKER_UP, O.NG_STACKER_DN, (int)prMACHINE[CP.StackerUpDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN StackerTableDn(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.ReWorkStaackerDnFail, I.NG_STACKER_DN, I.NG_STACKER_UP, O.NG_STACKER_DN, O.NG_STACKER_UP, (int)prMACHINE[CP.StackerDnDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN MoveY(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsNotMoveReworkTrayFeederBecauseTrayPk, true)) return eRTN.EMS;

            if (nPos == P.TrayLoad){
                if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]){
                    if (mtDATA[M.TrayFeeder3, P.TrayLoad].Pos - 200 < mtSTS[M.TrayFeeder3].CurrentPosition && mtDATA[M.TrayFeeder3, P.TrayLoad].Pos + 200 > mtSTS[M.TrayFeeder3].CurrentPosition) { }
                    else{
                        if (!C.Interlock.ChkInterlock(E.emsReworkLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                }
            }

            if (nPos == P.HD1TrayPocket || nPos == P.HD2TrayPocket){
                if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]){
                    if (prMACHINE[CP.ReworkTrayPlaceFirstLine] < mtSTS[M.TrayFeeder3].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsRworkPlaceLocationSensing, true)) return eRTN.EMS;
                        if (mtDATA[M.TrayFeeder3, P.TrayLoad].Pos + 200 < mtSTS[M.TrayFeeder3].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsReworkLoadingLocationSensing, true)) return eRTN.EMS;
                        }
                    }
                }
            }

            if (nPos == P.Staker){
                if (!C.Interlock.ChkInterlock(E.emsReworkStackerNotDown, true)) return eRTN.EMS;
                if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]){
                    if (mtDATA[M.TrayFeeder3, P.Staker].Pos - 200 > mtSTS[M.TrayFeeder3].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsRworkStackerTraySensing, true)) return eRTN.EMS;
                    }
                }
            }

            IsSTRING[S.ReWorkTrayMessage] = comment + " " + LogWR_.LogPos(M.TrayFeeder3, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.TrayFeeder3, nPos, 0.005, false, false, false, cmd, IsSTRING[S.ReWorkTrayMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion
    }
}