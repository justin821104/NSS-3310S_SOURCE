using LIB_.DateType;
using Object;
using System;

namespace NSS_3310S.SEQ.MODULE{
    public class TRAY_PICKER : BASE{
        int nThread = T.TrayPk;
        long TackStart = 0, TackEnd = 0;
        int nCurGoodTray = 0;

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

            RePIC:
                while (UTIL_.WaitBIT(nThread, B.LotEnd, true, "LOT-END 처리 진행 중")) ;
                if (!Pic("빈-트레이 픽업")){
                    COM_.SetBit(nThread, B.TrayPkPic, false, "트레이 피커 픽업 실패");
                    goto RePIC;
                }
                while (UTIL_.WaitBIT(nThread, B.GoodTray1TrayRequest, B.GoodTray2TrayRequest, B.ReWorkTrayRequest, false, false, false, "트레이 피터에서 트레이 달라고 요청 할때까지 대기", stBIT.AND)) ;
                while (UTIL_.WaitBIT(nThread, B.TrayStop, true, "트레이 공급 일시 정지")) ;
                while (UTIL_.WaitBIT(nThread, B.LotEnd, true, "LOT-END 처리 진행 중")) ;
                COM_.SetBit(nThread, B.TrayPkWorking, true, "트레이 피커 작업 진행");
                if (IsBIT[B.GoodTray1TrayRequest]){
                    nCurGoodTray = (int)eTRAY.GOOD1;
                    if (!Plc(eTRAY.GOOD1, "GOOD TRAY1 FEEDER에 트레이 플레이스")) goto RePIC;
                    COM_.SetBit(nThread, B.GoodTray1TrayRequest, false, "GOOD TRAY1 TRANSFER에 트레이 전달 완료 함.");
                }
                else if (IsBIT[B.GoodTray2TrayRequest]){
                    nCurGoodTray = (int)eTRAY.GOOD2;
                    if (!Plc(eTRAY.GOOD2, "GOOD TRAY2 FEEDER에 트레이 플레이스")) goto RePIC;
                    COM_.SetBit(nThread, B.GoodTray2TrayRequest, false, "GOOD TRAY2 TRANSFER에 트레이 전달 완료 함.");
                }
                else if (IsBIT[B.ReWorkTrayRequest]){
                    nCurGoodTray = (int)eTRAY.REWORK;
                    if (!Plc(eTRAY.REWORK, "REWORK TRAY FEEDER에 트레이 플레이스")) goto RePIC;
                    COM_.SetBit(nThread, B.ReWorkTrayRequest, false, "REWORK TRANSFER에 트레이 전달 완료 함.");
                }
                Tack();
            } while (true);
        }

        #region >> SEQ
        public bool Pic(string comment){
            if (mIN[I.TRAY_PKR_TRAY_CHECK]) return true;
            while (UTIL_.WaitBIT(nThread, B.EmptyTrayPicRequest, false, "빈-트레이 피터 트레이 픽업 준비 완료 시 까지 대기")) ;
            LogStart(nThread, comment + " 진행");
            COM_.SetBit(nThread, B.TrayPkPic, true, "트레이 피커 픽업 완료");
            while (eRTN.SUCESS != MoveX(P.TrayPckUp, "", "트레이 피커 X축 빈-트레이 픽업 위치 이송")) ;
            while (eRTN.SUCESS != UnClamp("트레이 피커 언클램프")) ;
            while (eRTN.SUCESS != MoveZ(P.TrayPckUp, "offset=-5", "트레이 피커 Z축 빈-트레이 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.TrayPckUp, "spd=5", "트레이 피커 Z축 빈-트레이 픽업 위치 이송")) ;
            while (eRTN.SUCESS != Clamp("트레이 피커 클램프")) ;
            while (eRTN.SUCESS != C.EmptyStacker.TransferUnGrip("빈-트레이 피터 트레이 언그립")) ;
            while (eRTN.SUCESS != MoveZ(P.TrayPckUp, "offset=-5:spd=5", "트레이 피커 Z축 빈-트레이 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) ;
            if (!mIN[I.TRAY_PKR_TRAY_CHECK] && !bDRYRUN){
                UTIL_.OnERROR(E.emsTrayPkEmtpyTrayPicFail);
#if _NSS3300
#else
                if (mIN[I.EMPTY_RAIL_ULD_TRAY_CHECK]){
                    COM_.SetBit(nThread, B.EmptyTrayPicRequest, false, "빈트레이 레일부에서 트레이 사라져 다시 요청");
                }
#endif
                return false;
            }
            COM_.SetBit(nThread, B.TrayPkPic, false, "트레이 피커 픽업 완료");
            COM_.SetBit(nThread, B.EmptyTrayPicRequest, false, "빈-트레이 픽업 완료");
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool Plc(eTRAY TrayFeeder, string comment){
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveX(P.TrayPlc[(int)TrayFeeder], "", TrayFeeder.ToString() + " 피터 트레이 플레이스 위치 이송")) ;
            if (!mIN[I.TRAY_PKR_TRAY_CHECK] && !bDRYRUN) return false;
            UTIL_.DELAY(1000);
#if _NSS3300
#else
            if (!mIN[I.LDTrayFeeder[(int)TrayFeeder]]) goto TrayEXIST;
#endif
            while (eRTN.SUCESS != MoveZ(P.TrayPlc[(int)TrayFeeder], "offset=-5", "트레이 피커 Z축 트레이 플레이스 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.TrayPlc[(int)TrayFeeder], "spd=10", "트레이 피커 Z축 트레이 플레이스 위치 이송")) ;

            if (eTRAY.GOOD1 == TrayFeeder || eTRAY.GOOD2 == TrayFeeder){
                while (eRTN.SUCESS != UnClamp("트레이 피커 언클램프")) ;
                while (eRTN.SUCESS != GoodTrayPreAlignFwd(nThread, "GOOD TRAY 프리-얼라인 전진")) ;
                while (eRTN.SUCESS != GoodTrayFeederGrip(nThread, TrayFeeder, "GOOD TRAY 피터 트레이 락")) ;
                while (eRTN.SUCESS != GoodTrayPreAlignBwd(nThread, "GOOD TRAY 프리-얼라인 후진")) ;
            }
            else{
                while (eRTN.SUCESS != UnClamp("트레이 피커 언클램프")) ;
                while (eRTN.SUCESS != C.ReworkTrayFeeder.Grip("RE-WORK TRAY 피터 트레이 락")) ;
            }
            while (eRTN.SUCESS != MoveZ(P.TrayPlc[(int)TrayFeeder], "offset=-5:spd=10", "트레이 피커 Z축 픽업 대기 위치 이송")) ;
            TrayEXIST:
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "트레이 피커 Z축 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.TrayPckUp, "", "트레이 피커 X축 트레이 픽업 위치 이송")) ;
            COM_.SetBit(nThread, B.TrayRequest[(int)TrayFeeder], false, TrayFeeder.ToString() + " 피터에 트레이 공곱");
            LogEnd(nThread, comment + " 완료");
            return true;
        }
#endregion

        #region >> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.TrayPkCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",트레이 피커," + IsDOUBLE[D.TrayPkCycle].ToString(), "");
            COM_.SetBit(nThread, B.TrayPkWorking, false, "트레이 피커 작업 진행 완료");
            TackStart = Environment.TickCount;
        }

        public eRTN Clamp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayPkCleampFail, /*I.TRAY_PKR_CLAMP*/-1, I.TRAY_PKR_UNCLAMP, O.TRAY_PK_GRIP, O.TRAY_PK_UNGRIP, (int)prMACHINE[CP.TrayCleampDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN UnClamp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayPkUnCleampFail, I.TRAY_PKR_UNCLAMP, I.TRAY_PKR_CLAMP, O.TRAY_PK_UNGRIP, O.TRAY_PK_GRIP, (int)prMACHINE[CP.TrayUnCleampDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN AlignUp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
#else
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayAlignUpFail, I.TrayAlignUp, I.TrayAlignDn, O.TrayAlignUp, O.TrayAlignDn, (int)prMACHINE[CP.CylinderOverTime], comment)) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }
        public eRTN AlignDown(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
#else
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayAlignDnFail, I.TrayAlignDn, I.TrayAlignUp, O.TrayAlignDn, O.TrayAlignUp, (int)prMACHINE[CP.CylinderOverTime], comment)) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }

        public eRTN AlignFwd(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
#else
            mOUT[O.TRAY_PK_TRAY_ALIGN_FWD] = true;
            mOUT[O.TRAY_PK_TRAY_ALIGN_BWD] = false;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayAlignFwd, I.TRAY_PK_ALIGN_FWD, I.TRAY_PK_ALIGN_BWD, O.TRAY_PK_TRAY_ALIGN_FWD, O.TRAY_PK_TRAY_ALIGN_BWD, (int)prMACHINE[CP.CylinderOverTime], comment)) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }
        public eRTN AlignBwd(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
#else
            mOUT[O.TRAY_PK_TRAY_ALIGN_FWD] = false;
            mOUT[O.TRAY_PK_TRAY_ALIGN_BWD] = true;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.TrayAlignBwd, I.TRAY_PK_ALIGN_BWD, I.TRAY_PK_ALIGN_FWD, O.TRAY_PK_TRAY_ALIGN_BWD, O.TRAY_PK_TRAY_ALIGN_FWD, (int)prMACHINE[CP.CylinderOverTime], comment)) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }

        public eRTN MoveX(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            IsSTRING[S.TrayPkMessage] = comment + " " + LogWR_.LogPos(M.TrayPickerX, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.TrayPickerX, nPos, 0.005, false, false, false, cmd, IsSTRING[S.StripPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveZ(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            if (nPos == P.TrayPckUp){
                if (!C.Interlock.ChkInterlock(E.emsTrayPkPickUpFail_EmptyTray, true)) return eRTN.EMS;
            }
            if (nPos == P.NGTrayPlc){
                if (!C.Interlock.ChkInterlock(E.emsTrayPkPickUpFail_ReworkTray, true)) return eRTN.EMS;
            }
            if (nPos == P.OKTrayPlc){
                if (nCurGoodTray == (int)eTRAY.GOOD1){
                    if (!C.Interlock.ChkInterlock(E.emsTrayPkPickUpFail_GoodTray1, true)) return eRTN.EMS;
                }
                if (nCurGoodTray == (int)eTRAY.GOOD2){
                    if (!C.Interlock.ChkInterlock(E.emsTrayPkPickUpFail_GoodTray2, true)) return eRTN.EMS;
                }
            }

            IsSTRING[S.TrayPkMessage] = comment + " " + LogWR_.LogPos(M.TrayPickerZ, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.TrayPickerZ, nPos, 0.005, false, false, false, cmd, IsSTRING[S.StripPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion
    }
}