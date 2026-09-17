using LIB_.DateType;
using Object;
using System;
using System.Diagnostics;
using System.IO;

namespace NSS_3310S.SEQ{
    public class BASE : DATA_{
        public static dxy Pkr_Offset;
        public static dxy Distance;
        public static bool[] isPrsErr = new bool[8];

        public delegate void procAddMSgEvent(int nThread, string sMsg);
        public static event procAddMSgEvent ProMsgEvent;
        static int msgThread = 0;
        public static void AddMessage(int nThread, string msg){
            try{
                msgThread = bMF ? T.Manual : nThread;
                ProMsgEvent?.Invoke(msgThread, msg);
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[BASE] ADD MESSAGE FAIL!" + ETC.NewLine + "THREAD NUMBER = " + nThread.ToString() + " / " + msg, EX);
            }
        }
        public static void LogStart(int nThread, string commend){
            try{
                tSeqTack[nThread] = Stopwatch.StartNew();
                //AddMessage(nThread, commend);
                LogWR_.SaveLogProcess(commend, ThreadName[nThread]);
            }
            catch (Exception ex){
                LogWR_.SaveLogException("[LogStart]" + ThreadName[nThread] + "=>" + commend, ex);
            }
        }
        public static void LogEnd(int nThread, string commend){
            try{
                tSeqTack[nThread].Stop();
                //AddMessage(nThread, commend);
                LogWR_.SaveLogProcess(commend + " [" + tSeqTack[nThread].ElapsedMilliseconds.ToString() + " msec / " + tSeqTack[nThread].Elapsed.ToString() + " sec]", ThreadName[nThread]);
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[LogEnd]" + ThreadName[nThread] + "=>" + commend, EX);
            }
        }

        public static void LogOneCycle(string StripBarcode){
            IsLONG[D.UnitPicTime] = Environment.TickCount;
            IsDOUBLE[D.CycleTime] = (IsLONG[D.UnitPicTime] - IsLONG[D.StripPlcTime]) / 1000;
            LogWR_.SaveLogOneCyle(sJobName + "," + CLOT.GET_LOT.LotID + "," + StripBarcode + "," + IsDOUBLE[D.CycleTime].ToString() + "," + IsDOUBLE[D.StripPkCycle].ToString(), "");
        }

        public static void RD_LOT_INF(){
            TEACH_.LOAD_COUNT(ref IsLONG[L.LotCnt], ref IsLONG[L.StripCnt], ref IsLONG[L.UnitCnt], ref IsLONG[L.GoodCnt], ref IsLONG[L.ReworkCnt], ref IsLONG[L.NGCnt], ref IsLONG[L.GoodTrayCnt], ref IsLONG[L.ReworkTrayCnt], ref IsLONG[L.OutCnt], ref IsLONG[L.ITSCount]);
            TEACH_.LOAD_INFO_COUNT(ref IsLONG[L.DayMGZCnt], ref IsLONG[L.DayStripCnt], ref IsLONG[L.DayGoodUnit], ref IsLONG[L.DayReworkUnit], ref IsLONG[L.DayRejectUnit]);
        }
        public static void WR_LOT_INF(){
            TEACH_.SAVE_COUNT(IsLONG[L.LotCnt], IsLONG[L.StripCnt], IsLONG[L.UnitCnt], IsLONG[L.GoodCnt], IsLONG[L.ReworkCnt], IsLONG[L.NGCnt], IsLONG[L.GoodTrayCnt], IsLONG[L.ReworkTrayCnt], IsLONG[L.OutCnt], IsLONG[L.ITSCount]);
            TEACH_.SAVE_INFO_COUNT(IsLONG[L.DayMGZCnt], IsLONG[L.DayStripCnt], IsLONG[L.DayGoodUnit], IsLONG[L.DayReworkUnit], IsLONG[L.DayRejectUnit]);
        }

        public static bool ChkRunning(int nThread){
            if ((eMCStatus == eMachineStatus.AUTO && nThread != T.Manual && !bMF)/*|| !bInitialComplete*/) return false;
            if (nThread == T.Manual || bMF) return false;
            if (editErrName != "NONE" && bMF){
                mIN[I.vtReset] = true;
                IsLONG[L.OffNumber] = I.vtReset;
                UTIL_.DELAY(100);
            }
            return true;
        }

        public static bool Trigger(int Ch){

            if (eMCStatus == eMachineStatus.AUTO){
                if (!mIN[I.VisionRdy] && !bDRYRUN){
                    E.OnERROR(E.emsNotVisionReady);
                    return false;
                }
            }
#if _NSS3300
            LAB_.ONE_SHOT_TRI(Ch);
#else
            LAB_.TriggerOutput(Ch, uVAL.High);
            LAB_.ONE_SHOT(Ch);
            LAB_.TriggerOutput(Ch, uVAL.Low);
#endif
            UTIL_.DELAY((int)prMACHINE[CP.TriggerEnd]);
            return true;
        }

        public static bool SawStageVac(int nTHREAD, bool bFlog){
            string sValue = bFlog ? "1" : "0";
            B.Bit(nTHREAD, B.SawVacuumInterface, false, "다이싱 테이블 진공 인터페이스 플러그 OFF");
            C.SendSaw.SEND("VACUUM," + sValue + ",*");
            for (int i = 0; i < 2000; i++){
                UTIL_.DELAY(1);
                if (IsBIT[B.SawVacuumInterface]) return true;
            }
            if (!IsBIT[B.SawVacuumInterface]) return false;
            return true;
        }
        public static bool SawStageBlow(int nTHREAD, bool bFlog){
            string sValue = bFlog ? "1" : "0";
            B.Bit(nTHREAD, B.SawVacuumInterface, false, "다이싱 테이블 파기 인터페이스 플러그 OFF");
            C.SendSaw.SEND("BLOW," + sValue + ",*");
            for (int i = 0; i < 2000; i++){
                UTIL_.DELAY(1);
                if (IsBIT[B.SawVacuumInterface]) return true;
            }
            if (!IsBIT[B.SawVacuumInterface]) return false;
            return true;
        }

#region >>TOP CAM
        public static eRTN MoveTopCam(int nThread, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.TopVisionX, M.TopVisionZ };
            int[] pos = { nPos, nPos };
            double[] toller = { 0.01, 0.01 };
            bool[] bOnlyStop = { false, false };
            bool[] bNoChange = { true, true };
            bool[] bDontStop = { false, false };
            string[] cmds = { cmd, "" };

            string sLocation = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, toller, bOnlyStop, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
#endregion

#region >>BOTTOM CAM
        public static eRTN BottomCameraCalibrationZig_Fwd(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.CameraCalibrationZigFwdFail, I.CAM_CAL_ZIG_FWD, I.CAM_CAL_ZIG_BWD, O.CAM_CAL_ZIG_FWD, O.CAM_CAL_ZIG_BWD, (int)prMACHINE[CP.CamZigFwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN BottomCameraCalibrationZig_Bwd(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.CameraCalibrationZigBwdFail, I.CAM_CAL_ZIG_BWD, I.CAM_CAL_ZIG_FWD, O.CAM_CAL_ZIG_BWD, O.CAM_CAL_ZIG_FWD, (int)prMACHINE[CP.CamZigBwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveBtmY(int nThread, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.BtnVisionY, M.BtnVisionZ };
            int[] pos = { nPos, nPos };
            double[] toller = { 0.01, 0.01 };
            bool[] bOnlyStop = { false, false };
            bool[] bNoChange = { true, true };
            bool[] bDontStop = { false, false };
            string[] cmds = { cmd, "" };

            string sLocation = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, toller, bOnlyStop, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
#endregion

#region >> STAGE
        public static void VisionBlow(eMAP_BLOCK eSTAGE, bool bFlog){
            LAB_.BIT_OUT(O.TOP_VISION_BLOW, bFlog);
            if (prMACHINE[CP.UseInspectionStageAir] == (int)eUSE.USE)
                StageAirshower(eSTAGE, bFlog);
        }
        public static void SetStage(eMAP_BLOCK eSTAGE){
            bool bSET = eSTAGE == eMAP_BLOCK.STAGE1 ? stBIT.TABLE_1 : stBIT.TABLE_2;
            LAB_.BIT_OUT(O.SelectMapBlock, bSET);
        }

        public static void StageVac(eMAP_BLOCK eSTAGE, bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.StageVacOn] : (int)prMACHINE[CP.StageVacOff];
            mOUT[O.StageVac[(int)eSTAGE]] = bFlog;
            mOUT[O.StageDrain[(int)eSTAGE]] = bFlog;
            mOUT[O.StageBackVac[(int)eSTAGE]] = false;
            UTIL_.DELAY(nDelay);
        }
        public static void StageVac(bool bFlog){
            LAB_.BIT_OUT(O.StageVac[(int)IsLONG[L.CurWorkStage]], bFlog);
            LAB_.BIT_OUT(O.StageDrain[(int)IsLONG[L.CurWorkStage]], bFlog);
            LAB_.BIT_OUT(O.StageBackVac[(int)IsLONG[L.CurWorkStage]], false);
        }
        public static void StageBlow(eMAP_BLOCK eSTAGE){
            mOUT[O.StageVac[(int)eSTAGE]] = false;
            mOUT[O.StageDrain[(int)eSTAGE]] = false;
            mOUT[O.StageBackVac[(int)eSTAGE]] = true;
            UTIL_.DELAY((int)prMACHINE[CP.StageBlowDelay]);
            mOUT[O.StageBackVac[(int)eSTAGE]] = false;
        }
        public static void StageBlow(eMAP_BLOCK eSTAGE, bool bFlog){
            mOUT[O.StageVac[(int)eSTAGE]] = false;
            mOUT[O.StageDrain[(int)eSTAGE]] = false;
            mOUT[O.StageBackVac[(int)eSTAGE]] = bFlog;
        }
        public static void StageAirshower(eMAP_BLOCK eSTAGE, bool bFlog) { mOUT[O.StageAirshowr[(int)eSTAGE]] = bFlog; }
        public static eRTN MoveStageY(int nThread, eMAP_BLOCK eSTAGE, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            
            if (eSTAGE == eMAP_BLOCK.STAGE1){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (eSTAGE == eMAP_BLOCK.STAGE2){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true)) return eRTN.EMS;
            }

            if (nPos == P.RecieveUnit){
                if (mtDATA[M.UnitPkX, P.PLACE_PALLET[(int)eSTAGE]].bPOS && ((mtDATA[M.UnitPkZ, P.PLACE_PALLET[(int)eSTAGE]].Pos - 5) < mtSTS[M.UnitPkZ].CurrentPosition)){
                    E.OnERROR(E.UnitPlaceFail[(int)eSTAGE]);
                    return eRTN.EMS;
                }
            }

            string sLocation = LogWR_.LogPos(M.DRY_TABLE[(int)eSTAGE], nPos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.DRY_TABLE[(int)eSTAGE], nPos, 0.005, false, false, false, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveUnitPlaceXY(int nThread, eMAP_BLOCK eSTAGE, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            if (eSTAGE == eMAP_BLOCK.STAGE1){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (eSTAGE == eMAP_BLOCK.STAGE2){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (!C.Interlock.ChkInterlock(E.emsUnitPkZnotSafetyLocation, true)) return eRTN.EMS;

            int[] mt            = { M.DRY_TABLE[(int)eSTAGE], M.UnitPkX };
            string[] cmds       = { "", "" };
            bool[] bNoChange    = { true, true };
            bool[] bOnlyStop    = { false, false };
            bool[] bDontStop    = { true, true };
            double[] toller     = { 0.005, 0.005 };

            stMoveInfo sx = M.GetMoveInfo(mt[0], P.RecieveUnit);
            stMoveInfo sy = M.GetMoveInfo(mt[1], P.PLACE_PALLET[(int)eSTAGE]);

            stMoveInfo[] ps = { sx, sy };
            string sLocation = LogWR_.LogPos(mt, ps);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, ps, toller, bOnlyStop, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveInpectionPos(int nThread, eMAP_BLOCK eSTAGE, int GX, int GY, int X, int Y, bool bTeaching, bool bOffset, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            if (eSTAGE == eMAP_BLOCK.STAGE1){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (eSTAGE == eMAP_BLOCK.STAGE2){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true)) return eRTN.EMS;
            }


            int[] mt = { M.TopVisionX, M.TopVisionZ, M.DRY_TABLE[(int)eSTAGE] };
            string[] cmds = { "", "", "" };
            bool[] bNoChange = { true, true, true };
            bool[] bOnlyStop = { false, false, false };
            bool[] bDontStop = { true, true, true };
            double[] toller = { 0.005, 0.005, 0.005 };
            stMoveInfo sx = M.GetMoveInfo(mt[0], P.TopCam_Pallet[(int)eSTAGE]);
            stMoveInfo sz = M.GetMoveInfo(mt[1], P.TopCam_Pallet[(int)eSTAGE]);
            stMoveInfo sy = M.GetMoveInfo(mt[2], P.TopVision_Unit);

            int idx;
            if ((Y + 1) > 1) idx = (int)(DEF.Utx[(int)eSTAGE] * Y) + (X + 1);
            else idx = (Y + 1) * (X + 1);

            double OffsetX = bOffset ? IsDOUBLE[D.UnitOffsetX[(int)eSTAGE]] : 0;
            double OffsetY = bOffset ? IsDOUBLE[D.UnitOffsetY[(int)eSTAGE]] : 0;
            sx.Pos = MarkCalPos_[(int)eSTAGE, GX, GY, idx - 1].x + OffsetX;
            sy.Pos = MarkCalPos_[(int)eSTAGE, GX, GY, idx - 1].y + OffsetY;

            double UnitCenterX = IsBIT[B.UnitEdgeInspection] ? (prMODEL[RP.UnitSizeX] / 2) : 0;
            double UnitCenterY = IsBIT[B.UnitEdgeInspection] ? (prMODEL[RP.UnitSizeY] / 2) : 0;
            if (bTeaching){
                sx.Pos += UnitCenterX;
                sy.Pos -= UnitCenterY;
            }

            stMoveInfo[] ps = { sx, sz, sy };
            string sLocation = LogWR_.LogPos(mt, ps);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, ps, toller, bOnlyStop, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            UTIL_.DELAY((int)prMACHINE[CP.InpectionMoveEndDelay]);
            return eRTN.SUCESS; //45
        }

        public static eRTN UnitReceive(int nThread, eMAP_BLOCK eSTAGE, string comment){
            AddMessage(nThread, "");
            B.SetBit(nThread, B.Stage_Work[(int)eSTAGE], true, eSTAGE.ToString() + " 유닛 공급");
            LogStart(nThread, comment + " 진행");
            if (!bMF) L.ResetStageUnitInfo(eSTAGE);
        RePLACE:
            //while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.RecieveUnit, "", eSTAGE.ToString() + " 유닛 받는 위치 이송")) ;
            //while (eRTN.SUCESS != C.UnitPk.MoveX(P.PLACE_PALLET[(int)eSTAGE], "", "유닛 피커 X축 " + eSTAGE.ToString() + " 유닛 받는 위치 이송")) ;
            while (eRTN.SUCESS != MoveUnitPlaceXY(nThread, eSTAGE, eSTAGE.ToString() + " 유닛 받는 위치 이송")) ;
            if (!mIN[I.UNIT_PK_VAC] && !bDRYRUN && !IsBIT[B.DirectUnitPlace]){
                W.ViewWarning(nThread, W.UnitPkUnit_Vanish);
                while (W.WaitWarning(nThread, W.UnitPkUnit_Vanish, "유닛 피커 유닛 중간에 사라져 대기")) ;
                if (ConfirmUser[W.UnitPkUnit_Vanish].result) goto RePLACE;
                while (mOUT[O.UNIT_PK_VAC] || mOUT[O.SCRAP_VAC_1] || mOUT[O.SCRAP_VAC_2]){
                    E.OnERROR(E.emsUnitPkVacNotOff);
                }
                B.SetBit(nThread, B.Stage_Receive[(int)eSTAGE], false, "유닛 피커 유닛 공급 중 유닛 사라짐");
                LogEnd(nThread, comment + " 중 유닛 사라짐");
                return eRTN.VANISH;
            }
            while (eRTN.SUCESS != C.UnitPk.MoveZ(P.PLACE_PALLET[(int)eSTAGE], "offset=-10", "유닛 피커 Z축 맵-블록 플레이스 대기 위치 이송")) ;
            if (!bDRYRUN){
                while (eRTN.SUCESS != C.UnitPk.MoveZ(P.PLACE_PALLET[(int)eSTAGE], "spd=10", "유닛 피커 Z축 맵-블록 플레이스 대기 위치 이송")) ;
            }
            StageVac(eSTAGE, stBIT.ON);
            C.UnitPk.AllVac(stBIT.OFF);
            for (int i = 0; i < (int)prMACHINE[CP.UnitBlowRepeatCnt]; i++){
                LAB_.OUTPUT(O.UNIT_PK_BLOW, true);
                LAB_.OUTPUT(O.SCRAP_BLOW_1, true);
                LAB_.OUTPUT(O.SCRAP_BLOW_2, true);
                UTIL_.DELAY((int)prMACHINE[CP.UnitPkBlowOn]);
                LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
                LAB_.OUTPUT(O.SCRAP_BLOW_1, false);
                LAB_.OUTPUT(O.SCRAP_BLOW_2, false);
                UTIL_.DELAY(500);
            }
            LAB_.OUTPUT(O.UNIT_PK_BLOW, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_1, false);
            LAB_.OUTPUT(O.SCRAP_BLOW_2, false);
            while (eRTN.SUCESS != C.UnitPk.MoveZ(P.PLACE_PALLET[(int)eSTAGE], "offset=-10:spd=5", "유닛 피커 Z축 테이블에 유닛 내려놓고 대기 위치 이송")) ;
            while (eRTN.SUCESS != C.UnitPk.MoveZ(P.Ready, "", "유닛 피커 Z축 대기 위치 이송")) ;

            eSeqBacode nGetBarcode = eSeqBacode.MapBlock1;
            int nThreadNum = T.DryTable1;
            if (eMAP_BLOCK.STAGE2 == eSTAGE) {
                nGetBarcode = eSeqBacode.MapBlock2;
                nThreadNum = T.DryTable2;
            }
            CLOT.SEND_STRIP_INFO(eSeqBacode.UnitPk, T.UnitPk, nGetBarcode, nThreadNum);
            TEACH_.WRITE_INFO_MAPBLOCK_STIP_BARCODE(eSTAGE, CLOT.InfoStrip[nThreadNum].Barcode);

            if (IsBIT[B.DirectUnitPlace]){
                IsBIT[B.DirectUnitPlace] = false;
                while (eRTN.SUCESS != C.UnitPk.MoveX(P.Ready, "", "유닛 피커 X축 대기 위치 이송")) ;
                mIN[I.vtStop] = true;
                UTIL_.DELAY(2000);
                mIN[I.vtStop] = true;
                UTIL_.DELAY(1000);
                if (IsBIT[B.JobCancel[(int)eSTAGE]]){
                    B.SetBit(nThread, B.JobCancel[(int)eSTAGE], false, eSTAGE.ToString() + " 작업 취소됨");
                    return eRTN.JOB_CANCEL;
                }
            }
            B.SetBit(nThread, B.Stage_Receive[(int)eSTAGE], false, "유닛 피커 유닛 공급 완료");
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public static eRTN StageAirshower(int nThread, eMAP_BLOCK eSTAGE, string comment){
            string cmds;
            while (!mIN[I.StageVac[(int)eSTAGE]] && !bDRYRUN){
                E.OnERROR(E.StageVacuum[(int)eSTAGE]);
                if (IsBIT[B.JobCancel[(int)eSTAGE]]){
                    B.SetBit(nThread, B.JobCancel[(int)eSTAGE], false, eSTAGE.ToString() + " 작업 취소됨");
                    return eRTN.JOB_CANCEL;
                }
            }
            while (B.WaitBIT(nThread, B.StageAirshowerWait, true, eSTAGE.ToString() + " 에어샤워 일시정지")) ;
            LogStart(nThread, comment + " 진행");
            for (int i = 0; i < (int)prMODEL[RP.StageAirshowerCnt]; i++){
                StageAirshower(eSTAGE, stBIT.ON);
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, "", eSTAGE.ToString() + " 에어샤워 시작 위치 이송")) ;
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.StageHeightPitch]) + ":spd=" + string.Format("{0:0}", prMODEL[RP.StageAirshowerLowSpeed]);
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, cmds, eSTAGE.ToString() + " 에어샤워 끝 위치 이송")) ;
            } //저속
            for (int i = 0; i < (int)prMACHINE[CP.StageAirshowRepeatCnt]; i++){
                StageAirshower(eSTAGE, stBIT.ON);
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, "", eSTAGE.ToString() + " 에어샤워 시작 위치 이송")) ;
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.StageHeightPitch]);
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, cmds, eSTAGE.ToString() + " 에어샤워 끝 위치 이송")) ;
            } //고속
            StageAirshower(eSTAGE, stBIT.OFF);
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public static eRTN UnitInspection(int nThread, eMAP_BLOCK eSTAGE, string comment){
            eRTN Result;
            while (!mIN[I.StageVac[(int)eSTAGE]] && !bDRYRUN){
                E.OnERROR(E.StageVacuum[(int)eSTAGE]);
                if (IsBIT[B.JobCancel[(int)eSTAGE]]){
                    B.SetBit(nThread, B.UnitInspection[(int)eSTAGE], false, comment + " 취소됨");
                    B.SetBit(nThread, B.JobCancel[(int)eSTAGE], false, comment + " 취소됨");
                    return eRTN.JOB_CANCEL;
                }
            }
            Result = SeqUnitInspection(nThread, eSTAGE, eSTAGE.ToString() + " 유닛 검사 진행");
            B.SetBit(nThread, B.UnitInspection[(int)eSTAGE], false, eSTAGE.ToString() + " 유닛 검사 진행 완료");
            if (Result == eRTN.JOB_CANCEL) return eRTN.JOB_CANCEL;
            return eRTN.SUCESS;
        }
        public static void ResetInspection(eMAP_BLOCK eSTAGE, int delay){
            LAB_.BIT_OUT(O.UnitAlignReading, false);
            LAB_.BIT_OUT(O.UnitReading, false);
            LAB_.BIT_OUT(O.MapBlockInspectionStart, true);
            UTIL_.DELAY(delay);
            LAB_.BIT_OUT(O.MapBlockInspectionStart, false);

            IsDOUBLE[D.UnitOffsetX[(int)eSTAGE]] = 0;
            IsDOUBLE[D.UnitOffsetY[(int)eSTAGE]] = 0;
            IsDOUBLE[D.UnitOffsetT[(int)eSTAGE]] = 0;
        }
        public static eRTN SeqUnitInspection(int nThread, eMAP_BLOCK eSTAGE, string comment){
            eRTN Result;
            string StageBarcode;
        RePLAY:
            int nDelayCnt = 0;
            SetStage(eSTAGE);
            DEF.ResetPallet(eSTAGE);
            //비전 사이즈 검사 재검사 요청 신호 ON시 대기 t초 기다리고 off 안되면 에러 처리!
            while (mIN[I.UnitInspectionReStart]){
                if (nDelayCnt > 2000){
                    E.OnERROR(E.emsUnitInspectionReTrayTimeOut);
                    mOUT[O.UnitReStart] = true;
                    goto RePLAY;
                }
                UTIL_.DELAY(1);
                nDelayCnt++;
            }
            mOUT[O.UnitReStart] = false;
            if (prMACHINE[CP.UseTopInspection] == (int)eUSE.USE){
                VisionBlow(eSTAGE, stBIT.ON);
                ResetInspection(eSTAGE, 33);
                while (eRTN.SUCESS != MoveInpectionPos(nThread, eSTAGE, 0, 0, 1, 1, false, false, comment + " 맵-블록 유닛 얼라인 위치 이송")) ;
                while (!Trigger((int)eTRIGGER.MARK)) ;
                DEF.ReadUnitAlign(eSTAGE);
                for (int gy = 0; gy < (int)prMODEL[RP.GroupCntY]; gy++){
                    for (int gx = 0; gx < (int)prMODEL[RP.GroupCntX]; gx++){
                        for (int y = 0; y < DEF.Uty[(int)eSTAGE]; y++){
                            for (int x = 0; x < DEF.Utx[(int)eSTAGE]; x++){
                                while (eRTN.SUCESS != MoveInpectionPos(nThread, eSTAGE, gx, gy, x, y, false, true, comment + " 유닛 얼라인 그룹XY = " + gx.ToString("00") + "/" + gy.ToString("00") + " 유닛XY = " + x.ToString("00") + "/" + y.ToString("00") + "" + " 위치 이송")) ;
                                if (mIN[I.UnitInspectionReStart]){ //비전 사이즈 검사 재검사 요청 신호 
                                    mOUT[O.UnitReStart] = true;
                                    goto RePLAY;
                                }
                                while (!Trigger((int)eTRIGGER.MARK)) ;
                                if (IsBIT[B.JobCancel[(int)eSTAGE]]){
                                    VisionBlow(eSTAGE, stBIT.OFF);
                                    B.SetBit(nThread, B.JobCancel[(int)eSTAGE], false, eSTAGE.ToString() + " 작업 취소됨");
                                    return eRTN.JOB_CANCEL;
                                }
                            }
                        }
                    }
                }
            }
            VisionBlow(eSTAGE, stBIT.OFF);
            Result = DEF.ReadInspectionResult(nThread, eSTAGE);
            O.SetOutput(nThread, O.UnitReading, false, "UNIT 데이터 읽었다고 비전에 신호 OFF.");
            if (Result == eRTN.NGOverCnt && !bDRYRUN){
                W.ViewWarning(nThread, W.UnitInspectionNgCountOver);
                while (W.WaitWarning(nThread, W.UnitInspectionNgCountOver, "유닛 검사 후 NG 수량 오버 하여 경고 발생")) ;
                if (ConfirmUser[W.UnitInspectionNgCountOver].result) goto RePLAY;
            }
            if (Result == eRTN.ResponseOverTime){
                E.OnERROR(E.TopVisionResponseOverTime);
                goto RePLAY;
            }
            if (Result == eRTN.NotDataFile){
                E.OnERROR(E.NotTopVisionDataFile);
                goto RePLAY;
            }
            if (Result == eRTN.ReadingDataFail){
                E.OnERROR(E.TopVaisionDataReadingFail);
                goto RePLAY;
            }
            if (Result == eRTN.IndexFail) goto RePLAY;

        ReCheckITS:
            if (prMACHINE[CP.UseITSData] == (int)eUSE.USE && prMACHINE[CP.UseMsSQL] == (int)eUSE.USE){
                ////잠시테스트
                //for (int idxY = 0; idxY < (int)prMODEL[RP.UnitCntY]; idxY++){
                //    for (int idxX = 0; idxX < (int)prMODEL[RP.UnitCntX]; idxX++){
                //        PALLET[(int)eSTAGE, 0, 0, idxX, idxY] = (int)eSTATUS.MARK;
                //    }
                //}
                ////잠시테스트

                if (eMAP_BLOCK.STAGE1 == eSTAGE)    StageBarcode = CLOT.InfoStrip[T.DryTable1].Barcode;
                else                                StageBarcode = CLOT.InfoStrip[T.DryTable2].Barcode;
                Result = DEF.ReadITSResult(eSTAGE, StageBarcode);
                if (Result != eRTN.SUCESS){
                    if (Result == eRTN.NothingBarcode)                      E.OnERROR(E.emsDesertUnitBarcodeMemory);
                    else if (Result == eRTN.NotITSCountFile)                E.OnERROR(E.emsNotFindITSCountFile);
                    else if (Result == eRTN.NotITSLocationFile)             E.OnERROR(E.emsNotFindITSLocationFile);
                    else if (Result == eRTN.FailITSCountDataParsingFail)    E.OnERROR(E.emsITSCountDataParsingFail);
                    else if (Result == eRTN.FailITSLocationDataParsingFail) E.OnERROR(E.emsITSLocationDataParsingFail);
                    else{
                        E.OnERROR(E.emsFailITSDataReading);
                        UTIL_.DELAY(1000);
                    }
                    if (IsBIT[B.JobCancel[(int)eSTAGE]]){
                        VisionBlow(eSTAGE, stBIT.OFF);
                        B.SetBit(nThread, B.JobCancel[(int)eSTAGE], false, eSTAGE.ToString() + " 작업 취소됨");
                        return eRTN.JOB_CANCEL;
                    }
                    goto ReCheckITS;
                }
            }
            else{
                //if (prMACHINE[CP.UseMsSQL] == (int)eUSE.NotUSE){
                //    if (prMACHINE[CP.UesBtmInspection] == (int)eUSE.NotUSE){
                //        E.OnERROR(E.emsBtmCamNotUse);
                //        UTIL_.DELAY(100);
                //        goto ReCheckITS;
                //    }
                //} // 무조건 하부 카메라 사용모드로 전환 후 진행 하셔야 합니다.
                //else{
                //    if (prMACHINE[CP.UseITSData] == (int)eUSE.NotUSE && prMACHINE[CP.UesBtmInspection] == (int)eUSE.NotUSE){
                //        E.OnERROR(E.emsBtmCamAndITSDataNotUse);
                //        UTIL_.DELAY(100);
                //        goto ReCheckITS;
                //    }
                //} // 
            }
            MAP_.PalletMap_ChkMarkCam(eSTAGE, (eMAP_DATA)IsLONG[L.ReverseMode[(int)eSTAGE]], (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitCntX], (int)prMODEL[RP.UnitCntY]);
            //TEST
            //UTIL_.DELAY(3000);
            //TEST
            return eRTN.SUCESS;
        }

        public static bool StageUnitPickUp(int nThread, eMAP_BLOCK eSTAGE){
            int gx = 0, gy = 0, x = 0, y = 0;
            IsLONG[L.CurWorkStage] = (int)eSTAGE;

            if (-1 != MAP_.GetPalletPocket(eSTAGE, (int)prMODEL[RP.GroupX[(int)eSTAGE]], (int)prMODEL[RP.GroupY[(int)eSTAGE]], (int)prMODEL[RP.UnitX[(int)eSTAGE]], (int)prMODEL[RP.UnitY[(int)eSTAGE]], ref gx, ref gy, ref x, ref y)){
                B.SetBit(nThread, B.Stage_Pic[(int)eSTAGE], true, eSTAGE.ToString() + " 테이블 픽업");
                while (B.WaitBIT(nThread, B.Stage_Pic[(int)eSTAGE], true, eSTAGE.ToString() + " 픽업 작업 완료 할때 까지 대기")) ;
            }

            if (IsBIT[B.StageUnitExist[(int)eSTAGE]]){
                B.Bit(nThread, B.StageUnitExist[(int)eSTAGE], false, "맵블록 작업 완료 후 유닛 제거 진행 비트 FALSE");
                double dMovingSped = prMACHINE[CP.MapBlockErrorMoveing] <= 0 ? 10 : prMACHINE[CP.MapBlockErrorMoveing];
                string cmds = "spd=" + dMovingSped.ToString();
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.RecieveUnit, cmds, eSTAGE.ToString() + " 유닛 받는 위치 이송")) ;
                E.OnERROR(E.StageUnitExist[(int)eSTAGE]);
            }
            DEF.ResetPallet(eSTAGE);
            B.SetBit(nThread, B.StageBusy[(int)eSTAGE], false, eSTAGE.ToString() + " 픽업 작업 진행");
            return true;
        }

        public static eRTN StageUnloadingAirshower(int nThread, eMAP_BLOCK eSTAGE, string comment){
            //if (!bMF) LogWR_.SavePCBUnitInfo(nThread, eSTAGE);
            if (prMACHINE[CP.UseWorkedAirshower] == (int)eUSE.NotUSE){
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.RecieveUnit, "", "유닛 받는 위치 이송")) ;
                return eRTN.SUCESS;
            }
            string cmds;

            LogStart(nThread, comment + " 진행");
            StageBlow(eSTAGE, stBIT.ON);
            StageAirshower(eSTAGE, stBIT.ON);
            for (int i = 0; i < (int)prMACHINE[CP.StageWorkedAirshowerRepeatCnt]; i++){
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, "", eSTAGE.ToString() + " 에어샤워 시작 위치 이송")) ;
                cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.StageHeightPitch]);
                while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.AirShowerStart, cmds, eSTAGE.ToString() + " 에어샤워 끝 위치 이송")) ;
            }
            StageBlow(eSTAGE, stBIT.OFF);
            StageAirshower(eSTAGE, stBIT.OFF);
            LogEnd(nThread, comment + " 완료");
            //while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.RecieveUnit, "", "유닛 받는 위치 이송")) ;
            return eRTN.SUCESS;
        }

        public static void StageWorkCencel(int nThread, eMAP_BLOCK eSTAGE, string comment){
            ConfirmUser[W.WorkingCancel].msg = eSTAGE.ToString() + " 작업 취소 되었습니다";
            W.ViewWarning(nThread, W.WorkingCancel);
            while (W.WaitWarning(nThread, W.WorkingCancel, "작AAAAAA업 취소 대기")) ;
            StageAirshower(eSTAGE, stBIT.OFF);
            while (eRTN.SUCESS != MoveStageY(nThread, eSTAGE, P.RecieveUnit, "", eSTAGE.ToString() + " 유닛 받는 위치 이송")) ;

            mIN[I.vtStop] = true;
            B.SetBit(nThread, B.Stage_Work[(int)eSTAGE], false, comment);
            UTIL_.DELAY(1000);
        }

        public static void StageTack(eMAP_BLOCK eSTAGE){
            IsLONG[L.StageTackEnd[(int)eSTAGE]] = Environment.TickCount;
            IsDOUBLE[D.StageTack[(int)eSTAGE]] = (IsLONG[L.StageTackEnd[(int)eSTAGE]] - IsLONG[L.StageTackNow[(int)eSTAGE]]) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + "," + eSTAGE.ToString() + "," + IsDOUBLE[D.StageTack[(int)eSTAGE]].ToString(), "");
            IsLONG[L.StageTackNow[(int)eSTAGE]] = Environment.TickCount;
        }

        public static eRTN MoveCalZigCenter(int nThread, eMAP_BLOCK eSTAGE, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.DRY_TABLE[(int)eSTAGE], M.TopVisionX, M.TopVisionZ };
            string[] cmds = { "", "", "" };
            bool[] bNoChange = { true, true, true };
            bool[] bOnlyStop = { false, false, false };
            bool[] bDontStop = { true, true, true };
            double[] toller = { 0.005, 0.005, 0.005 };

            stMoveInfo sy = M.GetMoveInfo(mt[0], P.CalZigCenter);
            stMoveInfo sx = M.GetMoveInfo(mt[1], P.TopCamCalZigCenter[(int)eSTAGE]);
            stMoveInfo sz = M.GetMoveInfo(mt[2], P.TopCamCalZigCenter[(int)eSTAGE]);

            stMoveInfo[] ps = { sy, sx, sz };
            string sLocation = LogWR_.LogPos(mt, ps);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, ps, toller, bOnlyStop, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion

#region >> GOOD TRAY FEEDER 1/2
        public static eRTN GoodTrayFeederFrontGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGripC : I.GoodFeeder2UnGripC;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1GripC : O.GoodFeeder2GripC;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UngGripC : O.GoodFeeder2UngGripC;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1FrontGrip : E.GoodFeeder2FrontGrip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        } //Conveyor
        public static eRTN GoodTrayFeederFrontUnGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGripC : I.GoodFeeder2UnGripC;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UngGripC : O.GoodFeeder2UngGripC;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1GripC : O.GoodFeeder2GripC;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1FrontUnGrip : E.GoodFeeder2FrontUnGrip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederUnGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        } //Conveyor
        public static eRTN GoodTrayFeederRearGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGripS : I.GoodFeeder2UnGripB;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1GripS : O.GoodFeeder2GripS;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UngGripS : O.GoodFeeder2UngGripS;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1RearGrip : E.GoodFeeder2RearGrip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        } //Stacker
        public static eRTN GoodTrayFeederRearUnGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGripS : I.GoodFeeder2UnGripB;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UngGripS : O.GoodFeeder2UngGripS;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1GripS : O.GoodFeeder2GripS;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1RearUnGrip : E.GoodFeeder2RearUnGrip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederUnGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        } //Stacker
        public static eRTN GoodTrayFeederGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGrip : I.GoodFeeder2UnGrip;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1Grip : O.GoodFeeder2Grip;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UnGrip : O.GoodFeeder2UnGrip;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1Grip : E.GoodFeeder2Grip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN GoodTrayFeederUnGrip(int nThread, eTRAY Tray, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            int[] OnInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1UnGrip : I.GoodFeeder2UnGrip;
            int[] OffInput = Tray == eTRAY.GOOD1 ? I.GoodFeeder1Grip : I.GoodFeeder2Grip;
            int[] OnOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1UnGrip : O.GoodFeeder2UnGrip;
            int[] OffOutput = Tray == eTRAY.GOOD1 ? O.GoodFeeder1Grip : O.GoodFeeder2Grip;
            int[] Error = Tray == eTRAY.GOOD1 ? E.GoodFeeder1UnGrip : E.GoodFeeder2UnGrip;

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, Error, OnInput, OffInput, OnOutput, OffOutput, (int)prMACHINE[CP.FeederUnGripDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN GoodTrayPreAlignFwd(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GoodTrayPreAlignFwdFail, I.GOOD_TRAY_PRE_ALIGN_FWD, I.GOOD_TRAY_PRE_ALIGN_BWD, O.GOOD_TRAY_PRE_ALIGN_FWD, O.GOOD_TRAY_PRE_ALIGN_BWD, (int)prMACHINE[CP.GoodTrayPreAlignFwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN GoodTrayPreAlignBwd(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GoodTrayPreAlignBwdFail, I.GOOD_TRAY_PRE_ALIGN_BWD, I.GOOD_TRAY_PRE_ALIGN_FWD, O.GOOD_TRAY_PRE_ALIGN_BWD, O.GOOD_TRAY_PRE_ALIGN_FWD, (int)prMACHINE[CP.GoodTrayPreAlignBwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN GoodTrayStackerUp(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GoodTrayStackerUpFail, I.GOOD_STACKER_UP, I.GOOD_STACKER_DN, O.GOOD_STACKER_UP, O.GOOD_STACKER_DN, (int)prMACHINE[CP.StackerUpDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN GoodTrayStackerDown(int nThread, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GoodTrayStackerDnFail, I.GOOD_STACKER_DN, I.GOOD_STACKER_UP, O.GOOD_STACKER_DN, O.GOOD_STACKER_UP, (int)prMACHINE[CP.StackerDnDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveFeederY(int nThread, eTRAY Tray, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (Tray == eTRAY.REWORK){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveReworkTrayFeederBecauseTrayPk, true)) return eRTN.EMS;
            }

            if (nPos == P.Ready){
                if (Tray == eTRAY.REWORK){
                    if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]){

                    }
                }
            }
            if (nPos == P.TrayLoad){
                if (Tray == eTRAY.GOOD1 && (!mIN[I.GOOD_TRAY1_UNGRIP_S] || !mIN[I.GOOD_TRAY1_UNGRIP_C])){
                    if (mtDATA[M.TrayFeeder1, P.TrayLoad].Pos - 200 < mtSTS[M.TrayFeeder1].CurrentPosition && mtDATA[M.TrayFeeder1, P.TrayLoad].Pos + 200 > mtSTS[M.TrayFeeder1].CurrentPosition) { }
                    else{
                        if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                    if (!mtDATA[M.TrayFeeder1, P.TrayLoad].bPOS){
                        if (mtDATA[M.TrayFeeder2, P.TrayLoad].bPOS){
                            if (!mIN[I.GOOD_TRAY2_UNGRIP_C] && !mIN[I.GOOD_TRAY2_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray1Loading_GoodTray2Clamp);
                                return eRTN.EMS;
                            } 
                        }
                    }
                }
                if (Tray == eTRAY.GOOD2 && (!mIN[I.GOOD_TRAY2_UNGRIP_S] || !mIN[I.GOOD_TRAY2_UNGRIP_C])){
                    if (mtDATA[M.TrayFeeder2, P.TrayLoad].Pos - 200 < mtSTS[M.TrayFeeder2].CurrentPosition && mtDATA[M.TrayFeeder2, P.TrayLoad].Pos + 200 > mtSTS[M.TrayFeeder2].CurrentPosition) { }
                    else{
                        if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                    if (!mtDATA[M.TrayFeeder2, P.TrayLoad].bPOS){
                        if (mtDATA[M.TrayFeeder1, P.TrayLoad].bPOS){
                            if (!mIN[I.GOOD_TRAY1_UNGRIP_C] && !mIN[I.GOOD_TRAY1_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray2Loading_GoodTray1Clamp);
                                return eRTN.EMS;
                            }
                        }
                    }
                }
                if (Tray == eTRAY.REWORK && (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2])){
                    if (mtDATA[M.TrayFeeder3, P.TrayLoad].Pos - 200 < mtSTS[M.TrayFeeder3].CurrentPosition && mtDATA[M.TrayFeeder3, P.TrayLoad].Pos + 200 > mtSTS[M.TrayFeeder3].CurrentPosition) {}
                    else{
                        if (!C.Interlock.ChkInterlock(E.emsReworkLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                }
            }

            if (nPos == P.FastPsh){

            }
            if (nPos == P.Psh){

            }
            if (nPos == P.TrayUnload){

            }
            
            if (nPos == P.HD1TrayPocket || nPos == P.HD2TrayPocket){
                if (eMCStatus == eMachineStatus.AUTO && (M.TRAY_FEEDER[(int)Tray] == M.TrayFeeder1 || M.TRAY_FEEDER[(int)Tray] == M.TrayFeeder2)){
                        mtDATA[M.TRAY_FEEDER[(int)Tray], P.CAL_] = mtDATA[M.TRAY_FEEDER[(int)Tray], nPos];
                        mtDATA[M.TRAY_FEEDER[(int)Tray], P.CAL_].Spd = mtDATA[M.TRAY_FEEDER[(int)Tray], P.FastPsh].Spd;
                        mtDATA[M.TRAY_FEEDER[(int)Tray], P.CAL_].Acc = mtDATA[M.TRAY_FEEDER[(int)Tray], P.FastPsh].Acc;
                        mtDATA[M.TRAY_FEEDER[(int)Tray], P.CAL_].Dec = mtDATA[M.TRAY_FEEDER[(int)Tray], P.FastPsh].Dec;
                        nPos = P.CAL_;
                }

                if (Tray == eTRAY.GOOD1 && (!mIN[I.GOOD_TRAY1_UNGRIP_S] || !mIN[I.GOOD_TRAY1_UNGRIP_C])){
                    if (prMACHINE[CP.GoodTray1PlaceFirstLine] < mtSTS[M.TrayFeeder1].CurrentPosition){
                        if (eMachineStatus.AUTO != eMCStatus){
                            if (!C.Interlock.ChkInterlock(E.emsGoodPlaceLocationSensing, true)) return eRTN.EMS;
                        }
                        if (mtDATA[M.TrayFeeder1, P.TrayLoad].Pos + 200 < mtSTS[M.TrayFeeder1].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                            if (!mIN[I.GOOD_TRAY2_UNGRIP_C] && !mIN[I.GOOD_TRAY2_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray1Place_GoodTray2Clamp);
                                return eRTN.EMS;
                            }
                        }
                    }
                    if (prMACHINE[CP.GoodTray2PlaceFirstLine] > mtSTS[M.TrayFeeder2].CurrentPosition && prMACHINE[CP.GoodTray2PlaceLastLine] < mtSTS[M.TrayFeeder2].CurrentPosition){
                        if (!mIN[I.GOOD_TRAY2_UNGRIP_C] && !mIN[I.GOOD_TRAY2_UNGRIP_S] && eMachineStatus.AUTO != eMCStatus){
                            E.OnERROR(E.emsNotGoodTray1Place_GoodTray2Clamp);
                            return eRTN.EMS;
                        }
                    }
                }
                if (Tray == eTRAY.GOOD2 && (!mIN[I.GOOD_TRAY2_UNGRIP_S] || !mIN[I.GOOD_TRAY2_UNGRIP_C])){
                    if (prMACHINE[CP.GoodTray2PlaceFirstLine] < mtSTS[M.TrayFeeder2].CurrentPosition){
                        if (eMachineStatus.AUTO != eMCStatus){
                            if (!C.Interlock.ChkInterlock(E.emsGoodPlaceLocationSensing, true)) return eRTN.EMS;
                        }
                        if (mtDATA[M.TrayFeeder2, P.TrayLoad].Pos + 200 < mtSTS[M.TrayFeeder2].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                            if (!mIN[I.GOOD_TRAY1_UNGRIP_C] && !mIN[I.GOOD_TRAY1_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray2Place_GoodTray1Clamp);
                                return eRTN.EMS;
                            }
                        }
                    }
                    if (prMACHINE[CP.GoodTray1PlaceFirstLine] > mtSTS[M.TrayFeeder1].CurrentPosition && prMACHINE[CP.GoodTray1PlaceLastLine] < mtSTS[M.TrayFeeder1].CurrentPosition){
                        if (!mIN[I.GOOD_TRAY1_UNGRIP_C] && !mIN[I.GOOD_TRAY1_UNGRIP_S] && eMachineStatus.AUTO != eMCStatus){
                            E.OnERROR(E.emsNotGoodTray2Place_GoodTray1Clamp);
                            return eRTN.EMS;
                        }
                    }
                }
                if (Tray == eTRAY.REWORK && (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2])){
                    if (prMACHINE[CP.ReworkTrayPlaceFirstLine] < mtSTS[M.TrayFeeder3].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsRworkPlaceLocationSensing, true)) return eRTN.EMS;
                        if (mtDATA[M.TrayFeeder3, P.TrayLoad].Pos + 200 < mtSTS[M.TrayFeeder3].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsReworkLoadingLocationSensing, true)) return eRTN.EMS;
                        }
                    }
                }
            }
            if (nPos == P.Staker){
                if (Tray == eTRAY.GOOD1 || Tray == eTRAY.GOOD2){
                    if (!C.Interlock.ChkInterlock(E.emsGoodStackerNotDown, true)) return eRTN.EMS;
                }
                if (Tray == eTRAY.GOOD1 && (!mIN[I.GOOD_TRAY1_UNGRIP_S] || !mIN[I.GOOD_TRAY1_UNGRIP_C])){
                    if (mtDATA[M.TrayFeeder1, P.Staker].Pos - 200 > mtSTS[M.TrayFeeder1].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsGoodStackerTraySensing, true)) return eRTN.EMS;
                    }
                    if (mtDATA[M.TrayFeeder1, P.TrayLoad].Pos - 200 > mtSTS[M.TrayFeeder1].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                    if (!mtDATA[M.TrayFeeder1, P.Staker].bPOS){
                        if (mtDATA[M.TrayFeeder2, P.Staker].bPOS){
                            if (!mIN[I.GOOD_TRAY2_UNGRIP_C] && !mIN[I.GOOD_TRAY2_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray1Stacker_GoodTray2Clamp);
                                return eRTN.EMS;
                            }
                        }
                    }
                } 
                if (Tray == eTRAY.GOOD2 && (!mIN[I.GOOD_TRAY2_UNGRIP_S] || !mIN[I.GOOD_TRAY2_UNGRIP_C])){
                    if (mtDATA[M.TrayFeeder2, P.Staker].Pos - 200 > mtSTS[M.TrayFeeder2].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsGoodStackerTraySensing, true)) return eRTN.EMS;
                    }
                    if (mtDATA[M.TrayFeeder2, P.TrayLoad].Pos - 200 > mtSTS[M.TrayFeeder2].CurrentPosition){
                        if (!C.Interlock.ChkInterlock(E.emsGoodLoadingLocationSensing, true)) return eRTN.EMS;
                    }
                    if (!mtDATA[M.TrayFeeder2, P.Staker].bPOS){
                        if (mtDATA[M.TrayFeeder1, P.Staker].bPOS){
                            if (!mIN[I.GOOD_TRAY1_UNGRIP_C] && !mIN[I.GOOD_TRAY1_UNGRIP_S]){
                                E.OnERROR(E.emsNotGoodTray2Stacker_GoodTray1Clamp);
                                return eRTN.EMS;
                            }
                        }
                    }
                }
                if (Tray == eTRAY.REWORK) { 
                    if (!C.Interlock.ChkInterlock(E.emsReworkStackerNotDown, true)) return eRTN.EMS;
                    if (!mIN[I.NG_TRAY_UNGRIP1] || !mIN[I.NG_TRAY_UNGRIP2]){
                        if (mtDATA[M.TrayFeeder3, P.Staker].Pos - 200 > mtSTS[M.TrayFeeder3].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsRworkStackerTraySensing, true)) return eRTN.EMS;
                        }
                        if (mtDATA[M.TrayFeeder3, P.TrayLoad].Pos - 200 > mtSTS[M.TrayFeeder3].CurrentPosition){
                            if (!C.Interlock.ChkInterlock(E.emsReworkLoadingLocationSensing, true)) return eRTN.EMS;
                        }
                    }
                }
            }

            string sLocation = LogWR_.LogPos(M.TRAY_FEEDER[(int)Tray], nPos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.TRAY_FEEDER[(int)Tray], nPos, 0.005, false, false, false, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN GetEmptyTray(int nThread, eTRAY Tray, string comment){
            LogStart(nThread, comment + " 진행");
            B.SetBit(nThread, B.TrayLoading[(int)Tray], true, Tray.ToString() + " 빈-트레이 공급 진행");
            while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.TrayLoad, "", Tray.ToString() + " 빈-트레이 공급 위치 이송")) ;
            while (eRTN.SUCESS != GoodTrayFeederUnGrip(nThread, Tray, Tray.ToString() + " 피터 언그립")) ;
            B.SetBit(nThread, B.TrayRequest[(int)Tray], true, Tray.ToString() + " 빈-트레이 공급 요청");
            while (B.WaitBIT(nThread, B.TrayRequest[(int)Tray], true, Tray.ToString() + " 빈-트레이 공급 완료 할때까지 대기")) ;
            while (eRTN.SUCESS != GoodTrayFeederGrip(nThread, Tray, Tray.ToString() + " 트레이 그립")) ;
            MAP_.TrayMap_Reset(Tray, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY]);
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }
        public static eRTN TrayUnitPlace(int nThread, eTRAY Tray, string comment){
            int nRtnPX = 0, nRtnPY = 0;
            while (B.WaitBIT(nThread, B.LotEnd, true, "LOT-END 처리 진행 중 대기")) ;

            if (prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD1 && Tray == eTRAY.GOOD2 && prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.STACKER){
                return eRTN.UnloadingStacker;
            }
            else if (prMACHINE[CP.SelectStackerUnloading] == (int)eGOOD_TRAY.GD2 && Tray == eTRAY.GOOD1 && prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.STACKER){
                return eRTN.UnloadingStacker;
            }
        
            LogStart(nThread, comment + " 진행");
            B.SetBit(nThread, B.TrayPlc[(int)Tray], true, Tray.ToString() + " PLACE");
            IsLONG[L.CurWorkTray] = (int)Tray;
            if (IsBIT[B.GoodTrayAutoUnloading]){
                while (B.WaitBIT(nThread, B.WaitTrayUldConv[(int)Tray], true, "콘베어 배출 진행 중 대기")) ;
                B.Bit(nThread, B.GoodTrayAutoUnloading, false, "구동 중 트레이 언로딩 진행 플로그 FALSE");
            }
            while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.Tray_Place[(int)Tray], "", Tray.ToString() + " Feeder Y축 유닛 플레이스 위치 이송")) ;
            B.SetBit(nThread, B.TrayLoading[(int)Tray], false, Tray.ToString() + " 빈-트레이 공급 완료");

            if (-1 != MAP_.GetTrayPocket(Tray, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref nRtnPX, ref nRtnPY)){
                B.SetBit(nThread, B.GoodTrayWork, true, Tray.ToString() + " 트레이 유닛 플레이스 작업 진행");
                while (B.WaitBIT(nThread, B.GoodTrayWork, true, Tray.ToString() + " 트레이 유닛 플레이스 완료 할때까지 대기")) ;
            }
            while (B.WaitBIT(nThread, B.WaitTrayUldConv[(int)Tray], true, "트레이 푸셔 콘베어로 밀어내기 전까지 대기")) ;
            if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR){
                B.SetBit(nThread, B.WaitTrayUldConv[(int)Tray], true, Tray.ToString() + "피터 콘베어 배출");
            }
            B.SetBit(nThread, B.GoodTrayUnlaidng[(int)Tray], true, Tray.ToString() + "언로딩 진행");
            B.SetBit(nThread, B.TrayPlc[(int)Tray], false, Tray.ToString() + " 트레이 작업 완료");
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public static eRTN OutTray(int nThread, eTRAY Tray, string comment){
            bool bStackerCheck = false;
            int nCNT = 0;
            LogStart(nThread, comment + " 진행");
            if (prMACHINE[CP.TrayUnloadingMode] == (int)eULD_TRAY.CONVEYOR){
                B.SetBit(nThread, B.TrayConveyorUnloadingWork, true, "굿 트레이 콘베어 배출 작업 진행");
            ReTrayUnloading:
#if _NSS3300
                if (mIN[I.GOOD_RAIL_CONVEYOR_CHECK]){
                    E.OnERROR(E.emsNotTrayUnloading_ConveyorUnloadingCheck, 500);
                    goto ReTrayUnloading;
                }
#else
                if (!bBD) {
                    if (!mIN[I.GOOD_RAIL_CONVEYOR_CHECK]) {
                        E.OnERROR(E.emsNotTrayUnloading_ConveyorUnloadingCheck, 500);
                        goto ReTrayUnloading;
                    }
                }
#endif
                while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.FastPsh, "", Tray.ToString() + " 언로딩 푸셔 위치 이송")) ;
                while (eRTN.SUCESS != GoodTrayFeederUnGrip(nThread, Tray, Tray.ToString() + " 트레이 언그립")) ;
                while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.Psh, "", Tray.ToString() + " 푸셔 시작 위치 이송")) ;
                while (eRTN.SUCESS != GoodTrayFeederFrontGrip(nThread, Tray, Tray.ToString() + " 푸셔 그립 락")) ;
                ChkConveyor:
                //언로더 콘베어 트레이 투입 신호 확인
                int WaitTime = prMODEL[RP.ULDConvWaitTime] <= 0 ? 5000 : (int)prMODEL[RP.ULDConvWaitTime];
                if (!bDRYRUN){
                    while (I.WaitInput(nThread, I.ULD_CONV_READY, false, "언로더 콘베어 투입 허가 신호 기다림")){
                        if (nCNT > WaitTime){
                            bWaitProduct = true;
                            W.ViewWarning(nThread, W.WaitUnloaderConveyor);
                            while (W.WaitWarning(nThread, W.WaitUnloaderConveyor, "콘베어 배출 신호 기다림")) ;
                            bWaitProduct = false;
                            nCNT = 0;
                            goto ChkConveyor;
                        }
                        nCNT++;
                    }
                }
                B.SetBit(nThread, B.GoodTrayUldEnd[(int)Tray], true, Tray.ToString() + " 콘베어 투입 확인");
                while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.TrayUnload, "", Tray.ToString() + " 콘베어 배출 위치 이송")) ;
                B.SetBit(nThread, B.WaitTrayUldConv[(int)Tray], false, Tray.ToString() + " 콘베어 배출 완료");
                UTIL_.DELAY((int)prMACHINE[CP.GoodTrayPushEndDealy]);
                while (eRTN.SUCESS != GoodTrayFeederUnGrip(nThread, Tray, Tray.ToString() + " 트레이 언그립")) ;
                while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.TrayLoad, "", Tray.ToString() + " 콘베어 배출 위치 이송")) ;
                //트레이 배출 완료
                mOUT[O.UldConveyorTrayUnloading] = true;
                UTIL_.DELAY(100);
                mOUT[O.UldConveyorTrayUnloading] = false;
                B.SetBit(nThread, B.GoodTrayUldEnd[(int)Tray], false, Tray.ToString() + " 콘베어 투입 확인");
                B.SetBit(nThread, B.TrayConveyorUnloadingWork, false, "굿 트레이 콘베어 배출 작업 진행 완료");
            } //트레이 콘베어로 배출
            else{
                while (eRTN.SUCESS != GoodTrayStackerDown(nThread, "GOOD TRAY 스태커 테이블 다운")) ;
                while (eRTN.SUCESS != MoveFeederY(nThread, Tray, P.Staker, "", Tray.ToString() + " 피더 스태커 위치 이송")) ;
                while (eRTN.SUCESS != GoodTrayFeederUnGrip(nThread, Tray, Tray.ToString() + " 트레이 언그립")) ;
                while (eRTN.SUCESS != GoodTrayStackerUp(nThread, "GOOD TRAY 스태커 테이블 업")) ;
                while (eRTN.SUCESS != GoodTrayStackerDown(nThread, "GOOD TRAY 스태커 테이블 다운")) ;
                bStackerCheck = true;
            } //트레이 스태커로 배출
            IsLONG[L.GoodTrayCnt]++;
            LogEnd(nThread, comment + " 완료");
            GoodTrayFeederTack();
        ReCHECK:
            if (mIN[I.GOOD_TRAY_STACKER_FULL] && bStackerCheck){
                TraySupply(nThread, "GOOD TRAY 스태커 트레이 배출 요청");
                goto ReCHECK;
            }
            B.SetBit(nThread, B.GoodTrayUnlaidng[(int)Tray], false, Tray.ToString() + "언로딩 진행 완료");
            return eRTN.SUCESS;
        }
        public static void TraySupply(int nThread, string comment){
            LogStart(nThread, comment + " 진행");
            W.ViewWarning(nThread, W.ReworkTrayFull);
            B.SetBit(nThread, B.GoodTrayStackerUldRequest, true, "GOOD 스태커 트레이 배출 할대 까지 대기");
            while (B.WaitBIT(nThread, B.GoodTrayStackerUldRequest, true, "GOOD STACKER 트레이 배출 대기")) ;
            LogEnd(nThread, comment + " 완료");
        }

        public static void GoodTrayFeederTack(){
            IsLONG[L.GoodTrayTackEnd] = Environment.TickCount;
            IsDOUBLE[D.TrayCycle] = (IsLONG[L.GoodTrayTackEnd] - IsLONG[L.GoodTrayTackNow]) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",OK 트레이," + IsDOUBLE[D.TrayCycle].ToString(), "");
            IsLONG[L.GoodTrayTackNow] = Environment.TickCount;
        }
#endregion

#region >> HEAD 1/2
        public static void PkVac(eHD nHEAD, ePK nPK, bool bFLOG, bool bDEALY, string cmd){
            int iOUT1, iOUT2;
            int iDelay = bDEALY ? (int)prMODEL[RP.PickupVacDelay] : 0;
            if (cmd == "first") iDelay = bDEALY ? (int)prMODEL[RP.FirstUnitPickupVacDelay] : 0;
            if (cmd == "firstline") iDelay = bDEALY ? (int)prMODEL[RP.FirstLinePickupVacDelay] : 0;
            if (bDRYRUN) iDelay = 0;

            if (M.TRIGGER1 == M.HD[(int)nHEAD]){
                iOUT1 = O.HD1PkRej[(int)nPK];
                iOUT2 = O.HD1PkVac[(int)nPK];
            }
            else{
                iOUT1 = O.HD2PkRej[(int)nPK];
                iOUT2 = O.HD2PkVac[(int)nPK];
            }

            if (bFLOG){
                LAB_.BIT_OUT((short)iOUT1, false);
                LAB_.BIT_OUT((short)iOUT2, true);
            }
            else{
                LAB_.BIT_OUT((short)iOUT1, true);
                LAB_.BIT_OUT((short)iOUT2, false);
                UTIL_.DELAY(10);
                LAB_.BIT_OUT((short)iOUT1, false);
            }
            if (bDEALY) iDelay = (iDelay < prMODEL[RP.PickupVacDelay]) ? (int)prMODEL[RP.PickupVacDelay] : iDelay;
            if (!bDRYRUN) UTIL_.DELAY(iDelay);
        }
        public static void PkBlow(eHD nHead, bool bFlog){
            int nOUT1, nOUT2;
            for (int i = 0; i < CNT_.PKR; i++){
                if (eHD.HD1 == nHead){
                    nOUT1 = O.HD1PkRej[i];
                    nOUT2 = O.HD1PkVac[i];
                }
                else{
                    nOUT1 = O.HD2PkRej[i];
                    nOUT2 = O.HD2PkVac[i];
                }
                if (bFlog){
                    LAB_.BIT_OUT((short)nOUT1, true);
                    LAB_.BIT_OUT((short)nOUT2, false);
                }
                else{
                    LAB_.BIT_OUT((short)nOUT1, false);
                    LAB_.BIT_OUT((short)nOUT2, false);
                }
            }
        }
        public static void PkBlow(eHD nHEAD, ePK nPK, bool bFLOG, bool bDEALY){
            int iOUT1, iOUT2;
            int iDelay = bDEALY ? (int)prMODEL[RP.PlaceBlowDelay] : 0;
            if (M.TRIGGER1 == M.HD[(int)nHEAD]){
                iOUT1 = O.HD1PkRej[(int)nPK];
                iOUT2 = O.HD1PkVac[(int)nPK];
            }
            else{
                iOUT1 = O.HD2PkRej[(int)nPK];
                iOUT2 = O.HD2PkVac[(int)nPK];
            }

            if (bFLOG){
                LAB_.BIT_OUT((short)iOUT1, true);
                LAB_.BIT_OUT((short)iOUT2, false);
                UTIL_.DELAY(iDelay + 5);
                LAB_.BIT_OUT((short)iOUT1, false);
            } //BLOW
            else{
                //LAB_.BIT_OUT((short)iOUT1, false);
                //LAB_.BIT_OUT((short)iOUT2, false);
                PkFree(nHEAD, nPK);
            } //OFF
        }
        public static void PkBlow(eHD nHEAD, ePK nPK, bool bDEALY){
            int iOUT1, iOUT2;
            int iDelay = bDEALY ? (int)prMODEL[RP.PlaceBlowDelay] : 0;
            //if (bDRYRUN) iDelay = 0;
            if (M.TRIGGER1 == M.HD[(int)nHEAD]){
                iOUT1 = O.HD1PkRej[(int)nPK];
                iOUT2 = O.HD1PkVac[(int)nPK];
            }
            else{
                iOUT1 = O.HD2PkRej[(int)nPK];
                iOUT2 = O.HD2PkVac[(int)nPK];
            } 
            //BLOW
            LAB_.BIT_OUT((short)iOUT1, true);
            LAB_.BIT_OUT((short)iOUT2, false);
            UTIL_.DELAY(iDelay + 5);
            //OFF
            LAB_.BIT_OUT((short)iOUT1, false);
            LAB_.BIT_OUT((short)iOUT2, false);
        }
        public static void PkFree(eHD nHEAD, ePK nPK){
            int iOUT1, iOUT2;
            if (M.TRIGGER1 == M.HD[(int)nHEAD]){
                iOUT1 = O.HD1PkRej[(int)nPK];
                iOUT2 = O.HD1PkVac[(int)nPK];
            }
            else{
                iOUT1 = O.HD2PkRej[(int)nPK];
                iOUT2 = O.HD2PkVac[(int)nPK];
            }
            LAB_.BIT_OUT((short)iOUT1, true);
            LAB_.BIT_OUT((short)iOUT2, false);
            UTIL_.DELAY(10);
            LAB_.BIT_OUT((short)iOUT1, false);
        }

        public static eRTN MovePk(int nThread, int mPk, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            string sLocation = LogWR_.LogPos(mPk, nPos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, mPk, nPos, 0.01, false, false, false, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveAllPkRdy(int nThread, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.X1Z12, M.X1Z34, M.X1Z56,
                         M.X2Z12, M.X2Z34, M.X2Z56
                    };
            int[] pos = { P.Ready, P.Ready, P.Ready,
                          P.Ready, P.Ready, P.Ready
                        };
            double[] Toller = { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 };
            bool[] NoChange = { true, true, true, true, true, true };
            bool[] OnlyStart = { false, false, false, false, false, false };
            bool[] DontStop = { true, true, true, true, true, true };
            string[] cmds = { cmd, "", "", "", "", "" };
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, Toller, OnlyStart, NoChange, DontStop, cmds, comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveHDPkRdy(int nThread, eHD nX, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.X1Z12, M.X1Z34, M.X1Z56 };
            if (nX == eHD.HD2){
                mt[0] = M.X2Z12;
                mt[1] = M.X2Z34;
                mt[2] = M.X2Z56;
            }
            int[] pos = { P.Ready, P.Ready, P.Ready };
            double[] dToller = { 0.1, 0.1, 0.1 };
            bool[] bNoChange = { true, true, true };
            bool[] bOnlyStart = { false, false, false };
            bool[] bDontStop = { true, true, true };
            string[] cmds = { cmd, "", "" };

            string sLogPos = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLogPos;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, dToller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MovePlcRdy(int nThread, eHD nX, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.X1Z12, M.X1Z34, M.X1Z56, M.X1T };
            if (nX == eHD.HD2){
                mt[0] = M.X2Z12;
                mt[1] = M.X2Z34;
                mt[2] = M.X2Z56;
                mt[3] = M.X2T;
            }
            int[] pos = { P.Ready, P.Ready, P.Ready, P.PkPlc };
            double[] dToller = { 0.1, 0.1, 0.1, 0.1 };
            bool[] bNoChange = { true, true, true, true };
            bool[] bOnlyStart = { false, false, false, false };
            bool[] bDontStop = { true, true, true, true };
            string[] cmds = { cmd, "", "", "" };

           

            string sLogPos = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLogPos;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, dToller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MovePicRdy(int nThread, eHD nX, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] mt = { M.X1Z12, M.X1Z34, M.X1Z56, M.X1T };
            if (nX == eHD.HD2){
                mt[0] = M.X2Z12;
                mt[1] = M.X2Z34;
                mt[2] = M.X2Z56;
                mt[3] = M.X2T;
            }
            int[] pos = { P.Ready, P.Ready, P.Ready, P.PkPckUp };
            double[] dToller = { 0.1, 0.1, 0.1, 0.1 };
            bool[] bNoChange = { true, true, true, true };
            bool[] bOnlyStart = { false, false, false, false };
            bool[] bDontStop = { true, true, true, true };
            string[] cmds = { cmd, "", "", "" };

            string sLogPos = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLogPos;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, dToller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MovePkTh(int nThread, int mHD, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //피커 다운 위치에서 회전 X 인터락 추가 필요~!

            string sLocation = LogWR_.LogPos(mHD, nPos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, mHD, nPos, 0.01, false, false, false, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MovePkTh(int nThread, int mHT, double dLocation, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //피커 다운 위치에서 회전 X 인터락 추가 필요~!

            stMoveInfo mT = M.GetMoveInfo(mHT, P.Ready);
            mT.Pos = dLocation;

            stMoveInfo[] ms = { mT };
            int[] aMotors = { mHT };
            double[] toller = { 0.1 };
            bool[] bOnlyStart = { false };
            bool[] bNoChange = { false };
            bool[] bDontStop = { false };
            string[] cmds = { cmd };

            string sLogPos = LogWR_.LogPos(aMotors, ms);
            string sLog = comment + " " + sLogPos;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, aMotors, ms, toller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveX(int nThread, eHD nX, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            string sLocation = LogWR_.LogPos(M.HD[(int)nX], nPos);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.HD[(int)nX], nPos, 0.01, false, false, false, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveHDCamCenter(int nThread, eHD nX, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //cal' zig 위치 확인 필요~

            int[] mt = { M.HD[(int)nX], M.BtnVisionZ, M.BtnVisionY };
            int[] pos = { P.BTMCamCenter, P.BtmCam_Cam[(int)nX], P.BtmCam_Cam[(int)nX] };
            double[] dToller = { 0.01, 0.01, 0.01 };
            bool[] bNoChange = { false, true, true };
            bool[] bOnlyStart = { false, false, false };
            bool[] bDontStop = { false, true, true, true };
            string[] cmds = { cmd, "", "" };

            string sLogPos = LogWR_.LogPos(mt, pos);
            string sLog = comment + " " + sLogPos;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mt, pos, dToller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MoveHDPkCenter(int nThread, eHD nX, int nPk, bool bBtmPkCalZ, string cmd, string comment, bool prsXspd = false){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            stMoveInfo mX = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
            stMoveInfo mY = M.GetMoveInfo(M.BtnVisionY, P.BtmCam_Pk[(int)nX]);
            stMoveInfo mT = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPckUp);
            stMoveInfo mZ = M.GetMoveInfo(M.BtnVisionZ, P.BtmCam_Pk[(int)nX]);

            double PkPitch = prMACHINE[CP.PickerPitch] * nPk;
            if (bMF) mT.Pos = mtSTS[M.HD_TH[(int)nX]].CurrentPosition;
            if (prsXspd) {
                mT.Pos = mtDATA[M.HD_TH[(int)nX], P.PkPlc].Pos;
                mT.Spd = 500;
                mT.Acc = mT.Spd * 10;
                mT.Dec = mT.Spd * 10;

                double PRSSpd = prMACHINE[CP.PRSStepSpeed] <= 1 ? 100 : prMACHINE[CP.PRSStepSpeed];
                if (nPk != 0) {
                    if (prMACHINE[CP.PRSStepSpeedHalf] == 1) PRSSpd /= 2;
                }
                mX.Spd = PRSSpd;
                mX.Acc = mX.Spd * 10;
                mX.Dec = mX.Spd * 10;
            }

            GetPkOffset(nX, (ePK)nPk, mT.Pos, ref Pkr_Offset);
            mX.Pos += PkPitch + Pkr_Offset.x; //hd1/2 x 방향 검증 완료
            mY.Pos += Pkr_Offset.y; //hd1/2 y 방향 검증 완료

            if (bBtmPkCalZ){
                mZ.Pos = mtDATA[M.BtnVisionZ, P.BtmCam_PkCal[(int)nX]].Pos;
            }

            
            stMoveInfo[] ms = { mX, mY, mT, mZ };
            int[] aMotors = { M.HD[(int)nX], M.BtnVisionY, M.HD_TH[(int)nX], M.BtnVisionZ };
            double[] toller = { 0.1, 0.1, 0.1, 0.1 };
            bool[] bOnlyStart = { false, false, false, false };
            bool[] bNoChange = { false, true, false, true };
            bool[] bDontStop = { false, false, false, false };
            string[] cmds = { cmd, "", "", "" };

            string sLocation = LogWR_.LogPos(aMotors, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, aMotors, ms, toller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveHDPkCenter(int nThread, eHD nX, int nPk, double dTh, bool bBtmPkCalZ, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            stMoveInfo mX = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
            stMoveInfo mY = M.GetMoveInfo(M.BtnVisionY, P.BtmCam_Pk[(int)nX]);
            stMoveInfo mT = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPckUp);
            stMoveInfo mZ = M.GetMoveInfo(M.BtnVisionZ, P.BtmCam_Pk[(int)nX]);

            double PkPitch = prMACHINE[CP.PickerPitch] * nPk;
            mT.Pos = dTh;
            GetPkOffset(nX, (ePK)nPk, mT.Pos, ref Pkr_Offset);
            mX.Pos += PkPitch + Pkr_Offset.x; //hd1/2 x 방향 검증 완료
            mY.Pos += Pkr_Offset.y; //hd1/2 y 방향 검증 완료

            if (bBtmPkCalZ){
                mZ.Pos = mtDATA[M.BtnVisionZ, P.BtmCam_PkCal[(int)nX]].Pos;
            }

            stMoveInfo[] ms = { mX, mY, mT, mZ };
            int[] aMotors = { M.HD[(int)nX], M.BtnVisionY, M.HD_TH[(int)nX], M.BtnVisionZ };
            double[] toller = { 0.1, 0.1, 0.1, 0.1 };
            bool[] bOnlyStart = { false, false, false, false };
            bool[] bNoChange = { false, true, false, true };
            bool[] bDontStop = { false, false, false, false };
            string[] cmds = { cmd, "", "", "" };

            string sLocation = LogWR_.LogPos(aMotors, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, aMotors, ms, toller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static string GetPkOffsetLabel(double PkThPos){
            //string[] sROT = new string[] { "0", "P90", "P180", "P270", "M90", "M180", "M270" };
            string sRtn = string.Empty;
            if (PkThPos > -20 && PkThPos < 20) sRtn = "0";    //-19 ~ 19 (0)
            if (PkThPos > 70 && PkThPos < 110) sRtn = "P90";  //71 ~ 109 (90)
            if (PkThPos > 160 && PkThPos < 200) sRtn = "P180"; //161 ~ 199 (180)
            if (PkThPos > 250 && PkThPos < 290) sRtn = "P270"; //251 ~ 289 (270)
            if (PkThPos < -70 && PkThPos > -110) sRtn = "M90";  //-71 ~ -109 (-90)
            if (PkThPos < -160 && PkThPos > -200) sRtn = "M180"; //-161 ~ -199 (-180)
            if (PkThPos < -250 && PkThPos > -290) sRtn = "M270"; //-251 ~ -289 (-270)
            return sRtn;
        }
        public static eRTN MovePkCalPitch(int nThread, eHD nX, string comment)
        {
            if (ChkRunning(nThread)) return eRTN.FAIL;

            stMoveInfo mX = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
            stMoveInfo mY = M.GetMoveInfo(M.BtnVisionY, P.BtmCam_Pk[(int)nX]);

            mX.Pos = mtSTS[M.HD[(int)nX]].CurrentPosition + IsDOUBLE[D.PkCal_OffsetX];
            mY.Pos = mtSTS[M.BtnVisionY].CurrentPosition - IsDOUBLE[D.PkCal_OffsetY];

            stMoveInfo[] ms = { mX, mY };
            int[] aMotors = { M.HD[(int)nX], M.BtnVisionY };
            double[] toller = { 0.1, 0.1 };
            bool[] bOnlyStart = { false, false };
            bool[] bNoChange = { false, false };
            bool[] bDontStop = { false, false };
            string[] cmds = { "", "" };

            string sLocation = LogWR_.LogPos(aMotors, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, aMotors, ms, toller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static void GetPkOffset(eHD nX, ePK nPK, double TargetThPos, ref dxy dPKOffset){
            if (TargetThPos > -20 && TargetThPos < 20)      dPKOffset = PkOffset_0[(int)nPK + (8 * (int)nX)];    //-19 ~ 19 (0)
            if (TargetThPos > 70 && TargetThPos < 110)      dPKOffset = PkOffset_P90[(int)nPK + (8 * (int)nX)];  //71 ~ 109 (90)
            if (TargetThPos > 160 && TargetThPos < 200)     dPKOffset = PkOffset_P180[(int)nPK + (8 * (int)nX)]; //161 ~ 199 (180)
            if (TargetThPos > 250 && TargetThPos < 290)     dPKOffset = PkOffset_P270[(int)nPK + (8 * (int)nX)]; //251 ~ 289 (270)
            if (TargetThPos < -70 && TargetThPos > -110)    dPKOffset = PkOffset_M90[(int)nPK + (8 * (int)nX)];  //-71 ~ -109 (-90)
            if (TargetThPos < -160 && TargetThPos > -200)   dPKOffset = PkOffset_M180[(int)nPK + (8 * (int)nX)]; //-161 ~ -199 (-180)
            if (TargetThPos < -250 && TargetThPos > -290)   dPKOffset = PkOffset_M270[(int)nPK + (8 * (int)nX)]; //-251 ~ -289 (-270)
            //mX.Pos += PkPitch + PkOffset.x; //hd1/2 x 방향 검증 완료
            //mY.Pos += PkOffset.y; //hd1/2 y 방향 검증 완료
        }
        public static void GetDistanceBetweenHDCamAndPkr(eHD nX, ref dxy pitch){
            pitch.x = mtDATA[M.HD[(int)nX], P.PkCenter].Pos - mtDATA[M.HD[(int)nX], P.BTMCamCenter].Pos;
            //pitch.y = Math.Abs(mtDATA[M.BtnVisionY, P.BtmCam_Pk[(int)nX]].Pos - mtDATA[M.BtnVisionY, P.BtmCam_Cam[(int)nX]].Pos);
            pitch.y = mtDATA[M.BtnVisionY, P.BtmCam_Pk[(int)nX]].Pos - mtDATA[M.BtnVisionY, P.BtmCam_Cam[(int)nX]].Pos;
        }

        public static eRTN MoveXPicReady(int nThread, eHD nX, eMAP_BLOCK nSTAGE, ePK nPK, bool bCAM, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int mt = M.HD_TH[(int)nX];

            int mz1 = M.X1Z12;//M.HD1Pk[(int)nPK];
            int mz2 = M.X1Z34;
            int mz3 = M.X1Z56;
            if (nX == eHD.HD2){
                mz1 = M.X2Z12; //M.HD2Pk[(int)nPK];
                mz2 = M.X2Z34;
                mz3 = M.X2Z56;
            }
            
            stMoveInfo sx   = M.GetMoveInfo(M.HD[(int)nX], P.HD_PIC[(int)nSTAGE]);
            stMoveInfo sz1  = M.GetMoveInfo(mz1, P.Ready);
            stMoveInfo sz2  = M.GetMoveInfo(mz2, P.Ready);
            stMoveInfo sz3  = M.GetMoveInfo(mz3, P.Ready);
            stMoveInfo st   = M.GetMoveInfo(mt, P.PkPckUp);

            GetPkOffset(nX, nPK, st.Pos, ref Pkr_Offset);
            double PKPitch = prMACHINE[CP.PickerPitch] * (int)nPK;
            sx.Pos = MapCalPos_[(int)nX, (int)nSTAGE, 0].x + PKPitch + Pkr_Offset.x;
            sx.Spd = 2000;
            sx.Acc = sx.Spd * 7;
            sx.Dec = sx.Spd * 7;

            GetDistanceBetweenHDCamAndPkr(nX, ref Distance);
            if (!bCAM) sx.Pos += Distance.x;

            stMoveInfo[] mi     = { sx, sz1, sz2, sz3, st };
            int[] ms            = { M.HD[(int)nX], mz1, mz2, mz3,  mt };
            double[] toller     = { 0.1, 0.1, 0.1, 0.1, 0.1 };
            bool[] onlystart    = { false, false, false, false, false };
            bool[] nochange     = { false, false, false, false, false };
            bool[] dontstop     = { false, true, true, true, true };
            string[] cmds       = { "", "", "", "", "" };

            string sLocation = LogWR_.LogPos(ms, mi);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, ms, mi, toller, onlystart, nochange, dontstop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveStageZigPos(int nThread, eHD nX, int nHole, bool CamView, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsCalZigNotBackPos, true)) return eRTN.EMS;
            if (prMACHINE[CP.ZigAttachStage] == (int)eMAP_BLOCK.STAGE1){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true)) return eRTN.EMS;
            }
            else{
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true)) return eRTN.EMS;
            }
            int mtZ = M.HD1Pk[/*nPK*/0];
            if (nX == eHD.HD2) mtZ = M.HD2Pk[/*nPK*/0];

            GetDistanceBetweenHDCamAndPkr(nX, ref Distance);
            double PkPitch      = prMACHINE[CP.PickerPitch] * 0;//nPK;
            double PkOffsetX    = prMACHINE[CP.PkPicOffsetX[(int)nX]];
            double PkOffsetY    = prMACHINE[CP.PkPicOffsetY[(int)nX]];

            stMoveInfo mx = M.GetMoveInfo(M.HD[(int)nX], P.HD_PIC[(int)prMACHINE[CP.ZigAttachStage]]);
            stMoveInfo mt = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPckUp);
            stMoveInfo my = M.GetMoveInfo(M.DRY_TABLE[(int)prMACHINE[CP.ZigAttachStage]], P.HD_Pallet[(int)nX]);
            stMoveInfo mz = M.GetMoveInfo(mtZ, P.Ready);
            GetPkOffset(nX, (ePK)/*nPK*/0, mt.Pos, ref Pkr_Offset);

            mx.Pos = prMACHINE[CP.ZigPosHD1_X[nHole]];
            my.Pos = prMACHINE[CP.ZigPosHD1_Y[nHole]];
            if (nX == eHD.HD2){
                mx.Pos = prMACHINE[CP.ZigPosHD2_X[nHole]];
                my.Pos = prMACHINE[CP.ZigPosHD2_Y[nHole]];
            }

            if (!CamView){
                mx.Pos = mx.Pos + Distance.x + PkPitch + PkOffsetX + Pkr_Offset.x;
                if (nX == (int)eHD.HD1) my.Pos = my.Pos + Distance.y + PkOffsetY + Pkr_Offset.y;
                else                    my.Pos = my.Pos + Distance.y + PkOffsetY + Pkr_Offset.y;
            }

            if (Math.Abs(mx.Pos - mtSTS[M.HD[(int)nX]].CurrentPosition) < 70){
                mx.Spd = (int)(mx.Spd / 2);
                mx.Acc = mx.Spd * 10;
                mx.Dec = mx.Spd * 7;
            }

            stMoveInfo[] ms         = { mx, mt, my, mz };
            int[] mot           = { M.HD[(int)nX], M.HD_TH[(int)nX], M.DRY_TABLE[(int)prMACHINE[CP.ZigAttachStage]], mtZ };
            double[] toller     = { 0.01, 0.01, 0.01, 0.01 };
            bool[] onlyStart    = { false, false, false, false };
            bool[] noChange     = { true, true, true, true };
            bool[] dontStop     = { true, true, true, true };
            string[] cmds       = { "", "", "", "" };

            string sLocation = LogWR_.LogPos(mot, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mot, ms, toller, onlyStart, noChange, dontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static void ResetHeadBuffer(int nThread, eHD nX){
            B.Bit(nThread, B.HD_NG[(int)nX], false, "유닛 NG 플러그 초기화");
            B.Bit(nThread, B.HD_REJECT[(int)nX], false, "유닛 REJECT 플러그 초기화");
            B.Bit(nThread, B.HD_INSPECTION[(int)nX], false, "유닛 INSPECTION 플러그 초기화");
        }
        public static eRTN UnitPic(int nThread, eHD nX, string comment){
            int gX = 0, gY = 0, pocketX = 0, pocketY = 0, nIndex = 0;
            eRTN ePIC;
            while (B.WaitBIT(nThread, B.Stage1Busy, B.Stage2Busy, false, false, "STAGE1 또는 STAGE2 - 유닛 공급 할때까지 대기", true)) ;
            B.SetBit(nThread, B.XWorking[(int)nX], true, nX.ToString() + " PIC AND PLC 진행");
            while (B.WaitBIT(nThread, B.PkrUnitPickUpStop, true, nX.ToString() + " 픽업 일시 정지")) ;
            LogStart(nThread, comment + " 진행");
            
            ResetHeadBuffer(nThread, nX);
            for (int z = 0; z < CNT_.PKR; z++){
            NextUnit:
                if (PK_Z[z + (CNT_.PKR * (int)nX)] == eSTATUS.NONE) continue; // 피커 스킵 상태 확인
                if (IsBIT[B.JobCancel[(int)IsLONG[L.CurWorkStage]]]){
                    B.Bit(nThread, B.JobCancel[(int)IsLONG[L.CurWorkStage]], false, "작업 취소 실행 플러그 OFF");
                    MAP_.CancelPalletData((eMAP_BLOCK)IsLONG[L.CurWorkStage], eMAP_DATA.FULL, (int)prMODEL[RP.GroupX[(int)IsLONG[L.CurWorkStage]]], (int)prMODEL[RP.GroupY[(int)IsLONG[L.CurWorkStage]]], (int)prMODEL[RP.UnitX[(int)IsLONG[L.CurWorkStage]]], (int)prMODEL[RP.UnitY[(int)IsLONG[L.CurWorkStage]]]);
                    B.SetBit(nThread, B.StageUnitExist[(int)IsLONG[L.CurWorkStage]], true, ((eMAP_BLOCK)IsLONG[L.CurWorkStage]).ToString() + " 작업 완료 후 유닛 유무 확인 플러그");
                }
                if (!DEF.GetPallet((eMAP_BLOCK)IsLONG[L.CurWorkStage], ref gX, ref gY, ref pocketX, ref pocketY, ref nIndex)){
                    B.SetBit(nThread, B.Stage_Pic[(int)IsLONG[L.CurWorkStage]], false, ((eMAP_BLOCK)IsLONG[L.CurWorkStage]).ToString() + " 유닛 픽업 완료");
                    break;
                }
                PICKER[(int)nX].finger[z].OffsetX = OFFSET[(int)IsLONG[L.CurWorkStage], gX, gY, pocketX, pocketY].x;
                PICKER[(int)nX].finger[z].OffsetY = OFFSET[(int)IsLONG[L.CurWorkStage], gX, gY, pocketX, pocketY].y;
                bool bCheckPicPos       = false;
                double dOldHeadXCurPos  = 0.0;
            RePIC:
                while (eRTN.SUCESS != MoveXUnitPic(nThread, nX, (eMAP_BLOCK)IsLONG[L.CurWorkStage], z, gX, gY, pocketX, pocketY, stBIT.NotCAM, stBIT.UnitOffset, "", nX.ToString() + "-" + (z + 1).ToString() + " 피커 [" + gX.ToString() + "/" + gY.ToString() + "/" + pocketX.ToString() + "/" + pocketY.ToString() + "] 유닛 픽업 위치 이송")) ;
                double dHeadXCurPos = mtSTS[M.HD[(int)nX]].CurrentPosition; 
                ePIC = PKPic(nThread, nX, (eMAP_BLOCK)IsLONG[L.CurWorkStage], z, nIndex, gX, gY, pocketX, pocketY, "UNIT PICKUP");
                MAP_.mapPallet[(int)IsLONG[L.CurWorkStage], gX, gY, pocketY, pocketX] = false;
                if (ePIC != eRTN.SUCESS){
                    ConfirmUser[W.PickUpFail].msg = nX.ToString() + " 피커" + (z + 1).ToString() + " 픽업 실패!" + ETC.CrLf + "(YES : RE-PICKUP || NO : NEXT UNIT PICKUP)";
                    W.ViewWarning(nThread, W.PickUpFail);
                    while (W.WaitWarning(nThread, W.PickUpFail, "PICK-UP 실패로 인하여 경고 메세지 발생")) ;
                    if (ConfirmUser[W.PickUpFail].result){
                        if (bCheckPicPos){
                            LogWR_.SaveLogOperate("리픽업 진행 -> OLD POS : " + dOldHeadXCurPos.ToString() + " / NEW POS : " + dHeadXCurPos.ToString(), "MC");
                        } //로그 추가
                        dOldHeadXCurPos = dHeadXCurPos;
                        bCheckPicPos = true;
                        goto RePIC; //YES-재픽업 
                    }
                    else{
                        MAP_.ARR_PALLET[(int)IsLONG[L.CurWorkStage], gX, (int)prMODEL[RP.GroupCntY] - 1 - gY, pocketX, (int)(prMODEL[RP.UnitY[(int)IsLONG[L.CurWorkStage]]] - 1) - pocketY] = eSTATUS.PICFAIL;
                        B.SetBit(nThread, B.StageUnitExist[(int)IsLONG[L.CurWorkStage]], true, "스테이즈 유닉 픽업 실패함");
                        goto NextUnit;
                    }
                }
                MAP_.ARR_PALLET[(int)IsLONG[L.CurWorkStage], gX, (int)prMODEL[RP.GroupCntY] - 1 - gY, pocketX, (int)(prMODEL[RP.UnitY[(int)IsLONG[L.CurWorkStage]]] - 1) - pocketY] = eSTATUS.EMPTY;
                IsLONG[L.StageUnit[IsLONG[L.CurWorkStage]]]++;
                IsLONG[L.InCnt]++;
                IsLONG[L.PicCnt]++;
            }
            B.SetBit(nThread, B.XPicBusy[(int)nX], false, nX.ToString() + " 유닛 픽업 완료");
            while (eRTN.SUCESS != MovePlcRdy(nThread, nX, "", nX.ToString() + " 피커 Z축 플레이스 대기 위치")) ;
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public static void RD_BladeThickness(ref double Thickness){
            if (!File.Exists(PATH_.BladeThickness)){
                Thickness = 0.3;
            }
            else{
                try{
                    string[] sLine = File.ReadAllText(PATH_.BladeThickness).Split(ETC.CrLf);
                    Thickness = Convert.ToDouble(sLine[0]); //x 피치 값
                }
                catch (Exception e){
                    LogWR_.SaveLogException("BLADE THICKNESS READ FAIL", e);
                    Thickness = 0.3;
                }
            }
        }
        public static eRTN MoveXUnitPic(int nThread, eHD nX, eMAP_BLOCK nSTAGE, int nPK, int gx, int gy, int ux, int uy, bool CamView, bool UseUnitOffset, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsCalZigNotBackPos, true)) return eRTN.EMS;
            if (nSTAGE == eMAP_BLOCK.STAGE1){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock1BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (nSTAGE == eMAP_BLOCK.STAGE2){
                if (!C.Interlock.ChkInterlock(E.emsNotMoveMapBlock2BecauseUnitPkr, true)) return eRTN.EMS;
            }
            if (prMACHINE[CP.StagePickupMovingVac] == (int)eUSE.USE) StageVac(stBIT.ON);

            int mtZ = M.HD1Pk[nPK];
            if (nX == eHD.HD2) mtZ = M.HD2Pk[nPK];

            GetDistanceBetweenHDCamAndPkr(nX, ref Distance);
            double PkPitch = prMACHINE[CP.PickerPitch] * nPK;
            double PicChkPitch = (prMACHINE[CP.PicUpCheckPitch] + prMODEL[RP.UnitThickess]) < (prMODEL[RP.UnitThickess] + 1) ? (prMODEL[RP.UnitThickess] + 1) : prMACHINE[CP.PicUpCheckPitch] + prMODEL[RP.UnitThickess];
            double PkOffsetX = prMACHINE[CP.PkPicOffsetX[(int)nX]];
            double PkOffsetY = prMACHINE[CP.PkPicOffsetY[(int)nX]];
            double UnitOffsetX = 0;
            double UnitOffsetY = 0;
            //double UnitOffsetT = 0;
            double BladeThickness = 0;
            double BladeOffsetX = 0;
            double BladeOffsetY = 0;
            if (UseUnitOffset){
                UnitOffsetX = IsDOUBLE[D.UnitOffsetX[(int)nSTAGE]];
                UnitOffsetY = IsDOUBLE[D.UnitOffsetY[(int)nSTAGE]];
                //UnitOffsetT = IsDOUBLE[D.UnitOffsetT[(int)nSTAGE]];
                RD_BladeThickness(ref BladeThickness);
                BladeOffsetX = BladeThickness / 2;//0.15;
                BladeOffsetY = BladeThickness / 2;//0.15;
            }

            int idx;
            if ((uy + 1) > 1) idx = (int)(prMODEL[RP.UnitX[(int)nSTAGE]] * uy) + (ux + 1);
            else idx = (uy + 1) * (ux + 1);

            stMoveInfo mx = M.GetMoveInfo(M.HD[(int)nX], P.HD_PIC[(int)nSTAGE]);
            stMoveInfo mt = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPckUp);
            stMoveInfo my = M.GetMoveInfo(M.DRY_TABLE[(int)nSTAGE], P.HD_Pallet[(int)nX]);
            stMoveInfo mz = M.GetMoveInfo(mtZ, P.PK_PIC[nPK]);
            GetPkOffset(nX, (ePK)nPK, mt.Pos, ref Pkr_Offset);

            mx.Pos = MapCalPos_[(int)nX, (int)nSTAGE, idx - 1].x + UnitOffsetX;
            if (nX == (int)eHD.HD1) my.Pos = MapCalPos_[(int)nX, (int)nSTAGE, idx - 1].y + UnitOffsetY;
            else                    my.Pos = MapCalPos_[(int)nX, (int)nSTAGE, idx - 1].y + UnitOffsetY;

            if (!CamView){ //블레이드 폭이 0.3mm
                mx.Pos = mx.Pos + Distance.x + PkPitch + PkOffsetX + Pkr_Offset.x + /*0.15*/BladeOffsetX;
                if (nX == (int)eHD.HD1) my.Pos = my.Pos + Distance.y + PkOffsetY + Pkr_Offset.y - /*0.15*/BladeOffsetY;
                else                    my.Pos = my.Pos + Distance.y + PkOffsetY + Pkr_Offset.y - /*0.15*/BladeOffsetY;
                //검증!
                if (prMACHINE[CP.UseTopInspectionOffset] == (int)eUSE.USE && !bMF){
                    mx.Pos += PICKER[(int)nX].finger[nPK].OffsetX;
                    my.Pos += PICKER[(int)nX].finger[nPK].OffsetY;
                }
            }

            if (Math.Abs(mx.Pos - mtSTS[M.HD[(int)nX]].CurrentPosition) < 50){
                mx.Spd = (int)(mx.Spd / 2);
                mx.Acc = mx.Spd * 10;
                mx.Dec = mx.Spd * 7;
            }

            if (idx > 1){
                my.Spd = prMODEL[RP.StagePickUpWorkSpeed];
                my.Acc = my.Spd * 7;
                my.Dec = my.Spd * 7;
            }

            if (nPK == (int)ePK.PKR1 || nPK == (int)ePK.PKR3 || nPK == (int)ePK.PKR5 || nPK == (int)ePK.PKR7) mz.Pos += PicChkPitch; //1,3,5,7 -
            else mz.Pos -= PicChkPitch; //2,4,6,8 +
            if (nPK == (int)ePK.PKR1 || eMCStatus != eMachineStatus.AUTO || mtDATA[M.HD[(int)nX], P.PkCenter].Pos > mtSTS[M.HD[(int)nX]].CurrentPosition) mz.Pos = M.GetPosData(mtZ, P.Ready); //

            stMoveInfo[] ms = { mx, mt, my, mz };
            int[] mot = { M.HD[(int)nX], M.HD_TH[(int)nX], M.DRY_TABLE[(int)nSTAGE], mtZ };
            double[] toller = { 0.01, 0.01, 0.01, 0.01 };
            bool[] onlyStart = { false, false, false, false };
            bool[] noChange = { true, true, true, true };
            bool[] dontStop = { true, true, true, true };
            string[] cmds = { cmd, "", "", "" };

            string sLocation = LogWR_.LogPos(mot, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mot, ms, toller, onlyStart, noChange, dontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        static void StagePic(int index){
            if (index <= prMODEL[RP.StageBlowUnitPickUpCnt]){
                LAB_.BIT_OUT(O.StageVac[(int)IsLONG[L.CurWorkStage]], false);
                LAB_.BIT_OUT(O.StageDrain[(int)IsLONG[L.CurWorkStage]], false);
                LAB_.BIT_OUT(O.StageBackVac[(int)IsLONG[L.CurWorkStage]], true);
            }
            else{
                if (prMACHINE[CP.StageUnitPickupVac] == (int)eUSE.USE){
                    LAB_.BIT_OUT(O.StageVac[(int)IsLONG[L.CurWorkStage]], true);
                    LAB_.BIT_OUT(O.StageDrain[(int)IsLONG[L.CurWorkStage]], true);
                }
                else{
                    LAB_.BIT_OUT(O.StageVac[(int)IsLONG[L.CurWorkStage]], false);
                    LAB_.BIT_OUT(O.StageDrain[(int)IsLONG[L.CurWorkStage]], false);
                }
                LAB_.BIT_OUT(O.StageBackVac[(int)IsLONG[L.CurWorkStage]], false);
            }
        }
        public static eRTN PKPic(int nThread, eHD nX, eMAP_BLOCK nSTAGE, int nPK, int index, int GX, int GY, int UX, int UY, string comment){
            StagePic(index);

            string sIdx = index <= 1 ? "first" : "";
            if (index > 1 && index <= prMODEL[RP.UnitX[(int)nSTAGE]]) sIdx = "firstline";

            string sOffset;
            if (nPK == (int)ePK.PKR1 || nPK == (int)ePK.PKR3 || nPK == (int)ePK.PKR5 || nPK == (int)ePK.PKR7) sOffset = "offset=" + prMODEL[RP.UnitThickess].ToString(); //1,3,5,7 -
            else sOffset = "offset=-" + prMODEL[RP.UnitThickess].ToString(); //2,4,6,8 +
            B.SetBit(nThread, B.StageAirshowerWait, false, "테이블 에어샤워 일시정지 플로그 OFF");
            for (int i = 0; i < (int)prMACHINE[CP.RePick]; i++){
                if (prMACHINE[CP.UsePickUpVac] == (int)eUSE.USE) PkVac(nX, (ePK)nPK, stBIT.ON, stBIT.NotDelay, "");
                while (eRTN.SUCESS != MovePK(nThread, nX, nPK, P.PK_PIC[nPK], sOffset, nX.ToString() + " 피커" + (nPK + 1).ToString() + comment + " 위치 이송")) ;
                PkVac(nX, (ePK)nPK, stBIT.ON, stBIT.Delay, sIdx);
                while (eRTN.SUCESS != MovePK(nThread, nX, nPK, P.PK_PIC[nPK], "ReCheckZ", nX.ToString() + " 피커" + (nPK + 1).ToString() + comment + " 체크 위치 이송")) ;
                if (mAI[nPK + (8 * (int)nX)] > mSET_AI[nPK + (8 * (int)nX)] || bDRYRUN){
                    if (bMF) return eRTN.SUCESS;

                    PK_Z[nPK + (CNT_.PKR * (int)nX)] = eSTATUS.MARK;
                    PICKER[(int)nX].finger[nPK].valid   = true;
                    PICKER[(int)nX].finger[nPK].iResult = PALLET[(int)nSTAGE, GX, GY, UX, UY];
                    if (PICKER[(int)nX].finger[nPK].iResult == (int)eSTATUS.NG)                                                                     IsBIT[B.HD_NG[(int)nX]]         = true; // 사이즈 불량 rework
                    if (PICKER[(int)nX].finger[nPK].iResult == (int)eSTATUS.FAIL)                                                                   IsBIT[B.HD_REJECT[(int)nX]]     = true; // x마크 불량 reject
                    if (PICKER[(int)nX].finger[nPK].iResult == (int)eSTATUS.FIRST || PICKER[(int)nX].finger[nPK].iResult == (int)eSTATUS.SECOND)    IsBIT[B.HD_INSPECTION[(int)nX]] = true; // 샘플링 검사 유닛
                    if (PICKER[(int)nX].finger[nPK].iResult == (int)eSTATUS.TOP_FAIL)                                                               IsBIT[B.HD_REJECT[(int)nX]]    = true; // top 카메라에서 불량 처리 reject
                    return eRTN.SUCESS;
                }
            }
            return eRTN.FAIL;
        }

        public static void PK_CAL_LIGHT(int nThread, bool bFLOG){
            O.SetOutput(nThread, O.UsePkCal, bFLOG, "피커 CAL' 조명 값 변경 ON 플러그");
            O.SetOutput(nThread, O.NotUsePkCal, !bFLOG, "피커 CAL' 조명 값 변경 OFF 플로그");
            UTIL_.DELAY(500);
        }
        public static eRTN PRS(int nThread, eHD nX, string comment){
            eRTN eRETURN;
        ReCHCK:
            if (mOUT[O.UsePkCal]) PK_CAL_LIGHT(nThread, false);

            mOUT[O.BTM_VISION_BLOW] = true;
            while (eRTN.SUCESS != MovePkTh(nThread, M.HD_TH[(int)nX], P.PkPlc, "", MtName[M.HD_TH[(int)nX]] + " 플레이스 위치 이송")) ;
            if (1 < Math.Abs(mtSTS[M.HD_TH[(int)nX]].CurrentPosition - mtDATA[M.HD_TH[(int)nX], P.PkPlc].Pos)){
                E.OnERROR(E.PkThNotPlcPos[(int)nX], 500);
                goto ReCHCK;
            }

            if (prMACHINE[CP.UesBtmInspection] == (int)eUSE.NotUSE) goto Pass;
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveBtmY(nThread, P.BtmCam_Pk[(int)nX], "", "하부 카메라 " + nX.ToString() + " 피커 중심 위치 이송")) ;
            while (eRTN.SUCESS != MoveHDPkRdy(nThread, nX, "", nX.ToString() + " 피커 대기 위치 이송")) ;
            while (B.WaitBIT(nThread, B.PkrUnitPlaceStop, true, nX.ToString() + " 플레이스 일시 정지")) ;

            if (!mIN[I.VisionRdy] && !bDRYRUN){
                E.OnERROR(E.emsNotVisionReady);
                goto ReCHCK;
            }
            O.SetOutput(nThread, O.PRSStart, true, ThreadName[nThread] + " PRS 시작");
            double dFingerPitch = prMACHINE[CP.PickerPitch];

            if (prMACHINE[CP.UseFlaying] == (int)InspectionMode.Flying){ //FLYNG 검사
                while (eRTN.SUCESS != MoveOnTheFlying(nThread, nX, "", "플라잉 이송")) ;
            } //FLYING
            else{ //STEP으로 검사
                //double PRSSpd;
                LAB_.TriggerOutput((int)/*eTRIGGER.BTM*/nX, uVAL.High);
                stMoveInfo mv = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
                for (int p = 0; p < 6; p++){
                    if (!mIN[I.VisionRdy] && !bDRYRUN){
                        E.OnERROR(E.emsNotVisionReady);
                        goto ReCHCK;
                    }

                    //mv      = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
                    //mv.Pos  += dFingerPitch * (5 - p);
                    //PRSSpd  = prMACHINE[CP.PRSStepSpeed] <= 1 ? 100 : prMACHINE[CP.PRSStepSpeed];
                    //if (p != 0) {
                    //    if (prMACHINE[CP.PRSStepSpeedHalf] == 1) PRSSpd /= 2;
                    //}
                    //mv.Spd = PRSSpd;
                    //mv.Acc = mv.Spd * 10;
                    //mv.Dec = mv.Spd * 10;
                    //while (eRTN.SUCESS != WRAP_.MOVE(nThread, M.HD[(int)nX], mv, 0.01, false, true, true, "", comment)) ;

                    int pkNum = 5 - p;
                    while (eRTN.SUCESS != MoveHDPkCenter(nThread, nX, pkNum, true, "", "PRS " + nX.ToString(), true)) ;
                    int pkZNum;
                    double pkZOffset;
                    string pkZCmd;
                    if (eHD.HD1 == nX) {
                        pkZNum = M.HD1Pk[p];
                        pkZOffset = prMODEL[RP.HD1_PRS_OFFSET[p]];
                        pkZCmd = pkZOffset == 0 ? "" : "offset=" + pkZOffset.ToString();  
                    }
                    else {
                        pkZNum = M.HD2Pk[p];
                        pkZOffset = prMODEL[RP.HD2_PRS_OFFSET[p]];
                        pkZCmd = pkZOffset == 0 ? "" : "offset=" + pkZOffset.ToString();
                    }
                    while (eRTN.SUCESS != MovePk(nThread, pkZNum, P.Ready, pkZCmd, "PRS Z OFFSET - " + nX.ToString() + " PK" + (p + 1).ToString() + pkZCmd)) ;
                    UTIL_.DELAY((int)prMACHINE[CP.PrsStapInspectionDelay]);

                    //LAB_.TriggerOutput((int)/*eTRIGGER.BTM*/nX, uVAL.High); //test
                    LAB_.ONE_SHOT((int)/*eTRIGGER.BTM*/nX);
                    //LAB_.TriggerOutput((int)nX, uVAL.Low); //test
                    UTIL_.DELAY(100);
                }
                LAB_.TriggerOutput((int)nX, uVAL.Low);
            } //STEP
            //UTIL_.DELAY(100);
            //LAB_.TriggerOutput((int)eTRIGGER.HD1, uVAL.Low);
            //LAB_.TriggerOutput((int)eTRIGGER.HD2, uVAL.Low);

            //if (eRTN.SUCESS != ReadPRS(nThread, nX)) goto ReCHCK; 
            eRETURN = ReadPRS(nThread, nX);
            if (eRTN.SUCESS != eRETURN){
                if (eRETURN == eRTN.PRS_MATCH_FAIL)
                {
                    //UTIL_.OnERROR(E.XMarkReTrain[(int)nX]);
                    ConfirmUser[W.XMarkInspectionFail].msg = nX.ToString() + " X-MARK 검사 실패 하였습니다." + ETC.NewLine + "YES : 다시 검사 / NO : REWORK 트레이로 배출";
                    W.ViewWarning(nThread, W.XMarkInspectionFail);
                    while (W.WaitWarning(nThread, W.XMarkInspectionFail, "X-MARK 검사 실패 메세지 발생")) ;
                    if (ConfirmUser[W.XMarkInspectionFail].result){
                        if (bMF) return eRTN.FAIL;
                        goto ReCHCK;
                    }
                    else{
                        for (int n = 0; n < isPrsErr.Length; n++){
                            if (isPrsErr[n]){
                                PICKER[(int)nX].finger[n].iResult = (int)eSTATUS.NG;
                                B.Bit(nThread, B.HD_NG[(int)nX], true, "유닛 NG 결과 플러그 ON");
                            }
                        }
                    } //rework 트레이로 배출
                }
                else{
                    if (bMF) return eRTN.FAIL;
                    goto ReCHCK;
                }
            }

            LogEnd(nThread, comment + " 완료");
        Pass:
            mOUT[O.BTM_VISION_BLOW] = false;
            B.SetBit(nThread, B.XPRSBusy[(int)nX], false, "PRS 작업 진행 완료");
            return eRTN.SUCESS;
        }
        public static eRTN MoveOnTheFlying(int nThread, eHD nX, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int[] aEMS = { E.emsNotVisionReady };
            if (!C.Interlock.ChkInterlock(aEMS, true)) return eRTN.EMS;

            double dFingerPitch = prMACHINE[CP.PickerPitch];
            double dStartPos = mtSTS[M.HD[(int)nX]].CurrentPosition;
            double dEndPos = mtDATA[M.HD[(int)nX], P.Feeder1].Pos;

            if (dStartPos < prMACHINE[CP.PRSStartPos]){
                mtDATA[M.HD[(int)nX], P.CAL_].Pos = prMACHINE[CP.PRSStartPos];
                mtDATA[M.HD[(int)nX], P.CAL_].Spd = 500;
                mtDATA[M.HD[(int)nX], P.CAL_].Acc = 500 * 7;
                mtDATA[M.HD[(int)nX], P.CAL_].Dec = 500 * 7;
                if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.HD[(int)nX], P.CAL_, 0.001, false, false, false, "", nX.ToString() + " 플라잉  시작 위치 이송")) return eRTN.FAIL;
            }

            stMoveInfo mv = M.GetMoveInfo(M.HD[(int)nX], P.PkCenter);
            double[] dAbsTrargetPos = {
                                        mv.Pos + (dFingerPitch * 5),
                                        mv.Pos + (dFingerPitch * 4),
                                        mv.Pos + (dFingerPitch * 3),
                                        mv.Pos + (dFingerPitch * 2),
                                        mv.Pos + (dFingerPitch * 1),
                                        mv.Pos
                                    };

#if _NSS3300
            //External Trigger 설정 // 펄스 타입
            CAXM.AxmTriggerSetReset(M.HD[(int)nX]);
            uint uiRet = CAXM.AxmTriggerSetTimeLevel(M.HD[(int)nX], 50, 1, 1, 0);
            if (uiRet != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
                CAXM.AxmTriggerSetReset(M.HD[(int)nX]);
                return eRTN.FAIL;//에러 발생.
            }
            uiRet = CAXM.AxmTriggerOnlyAbs(M.HD[(int)nX], dAbsTrargetPos.Length, dAbsTrargetPos);
            if (uiRet != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
                CAXM.AxmTriggerSetReset(M.HD[(int)nX]);
                return eRTN.FAIL;//에러 발생.
            }
#else
            //카운트 모듈
            CAXC.AxcTriggerSetFunction((int)nX, 2);
            //CAXC.AxcTriggerSetBlockLowerPos((int)nX, );
            //CAXC.AxcTriggerSetBlockUpperPos((int)nX, );
            CAXC.AxcTriggerSetAbsDouble((int)nX, (uint)dAbsTrargetPos.Length, dAbsTrargetPos, 0);
            LAB_.TriggerOutput((int)nX, uVAL.High);
#endif
            mv.Pos = dEndPos;
            mv.Spd = 1800; //1800; (j사 2500) // jjh 1000 기본
            mv.Acc = 18000;//30000; (j사 35000)
            mv.Dec = mv.Spd * 7;//12000; (j사 20000)

            string sLogPos = LogWR_.LogPos(M.HD[(int)nX], mv);
            string sLog = comment + " " + sLogPos;
            //속도변경 하지 않는다. 
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.HD[(int)nX], mv, 0.01, false, true, true, cmd, sLog)) return eRTN.FAIL;
            UTIL_.DELAY(100);
            LAB_.TriggerOutput((int)nX, uVAL.Low);
            return eRTN.SUCESS;
        }
        public static eRTN ReadPRS(int nThread, eHD nX){
            while (!mIN[I.PRSVisionWirte]){
                for (int i = 0; i < (int)prMACHINE[CP.VisionReponseOverTime]; i++){
                    UTIL_.DELAY(1);
                    if (mIN[I.PRSVisionWirte]) break;
                }
                if (!mIN[I.PRSVisionWirte]){
                    O.SetOutput(nThread, O.PRSReading, false, "PRS 데이터 읽었다고 비전에 신호 OFF");
                    O.SetOutput(nThread, O.PRSStart, false, ThreadName[nThread] + " PRS 리셋");
                    E.OnERROR(E.emsPRSReadingTimeOver); // prs 결과 값 없음!
                    return eRTN.TimeOver;
                }
            }
            //PRS 데이터 파일 읽고 삭제
            if (!File.Exists(PATH_.PRS)){
                E.OnERROR(E.emsNotPRSFile);
                return eRTN.NotDataFile;
            }
            Array.Clear(isPrsErr, 0, isPrsErr.Length);
            string[] sLine = File.ReadAllText(PATH_.PRS).Split(ETC.CrLf);
            try{
                for (int idxFinger = 0; idxFinger < CNT_.PKR; idxFinger++){
                    sLine[idxFinger] = sLine[idxFinger].Replace(";", "");
                    sLine[idxFinger] = sLine[idxFinger].Replace("\n", "");
                    sLine[idxFinger] = sLine[idxFinger].Replace("\r", "");

                    string[] sRslt = sLine[idxFinger].Split(',');
                    int iResult = int.Parse(sRslt[0]);
                    if (sRslt[0].IndexOf('1') >= 0){
                        double px = double.Parse(sRslt[1]);
                        double py = double.Parse(sRslt[2]);
                        double pth = double.Parse(sRslt[3]);
                        if (iResult == (int)eSTATUS.X_MARK) PICKER[(int)nX].finger[idxFinger].iResult = iResult;
                        PICKER[(int)nX].finger[idxFinger].prsX = Math.Abs(px) > 3 ? 0 : px;
                        PICKER[(int)nX].finger[idxFinger].prsY = Math.Abs(py) > 3 ? 0 : py;
                        PICKER[(int)nX].finger[idxFinger].prsR = Math.Abs(pth) > 3 ? 0 : pth;
                    }
                    else{
                        if (iResult == (int)eSTATUS.X_MARK) PICKER[(int)nX].finger[idxFinger].iResult = iResult;
                        PICKER[(int)nX].finger[idxFinger].prsX = 0;
                        PICKER[(int)nX].finger[idxFinger].prsY = 0;
                        PICKER[(int)nX].finger[idxFinger].prsR = 0;

                        if (PK_Z[idxFinger + (CNT_.PKR * (int)nX)] == eSTATUS.MARK){
                            if (iResult == 0){
                                isPrsErr[idxFinger] = true;
                            }
                        }
                    } //X마크 이거나 FAIL이거나 피커 스킵 상태임.
                }
            }
            catch (Exception exp){
                LogWR_.SaveLogException("ChipPkr_PRS", exp);
                E.OnERROR(E.emsPRSReadingFail, 500);
                O.SetOutput(nThread, O.PRSReading, false, "PRS 데이터 읽었다고 비전에 신호 OFF");
                O.SetOutput(nThread, O.PRSStart, false, ThreadName[nThread] + " PRS 완료");
                return eRTN.ReadingDataFail;
            }
            O.SetOutput(nThread, O.PRSReading, true, "PRS 데이터 읽었다고 비전에 신호 ON");
            for (int i = 0; i < (int)prMACHINE[CP.VisionReponseOverTime]; i++){
                if (!mIN[I.PRSVisionWirte]) break;
                UTIL_.DELAY(1);
            }
            UTIL_.DELAY(100);
            if (mIN[I.PRSVisionWirte]){
                O.SetOutput(nThread, O.PRSReading, false, "PRS 데이터 읽었다고 비전에 신호 OFF");
                O.SetOutput(nThread, O.PRSStart, false, ThreadName[nThread] + " PRS 완료");
                E.OnERROR(E.emsPRSReadedFail); // 결과 값 리딩 후 비전 prs data result 신호 off 안됨
                return eRTN.TimeOver;
            }
            O.SetOutput(nThread, O.PRSReading, false, "PRS 데이터 리딩 완료");
            O.SetOutput(nThread, O.PRSStart, false, ThreadName[nThread] + " PRS 완료");

            for (int n = 0; n < isPrsErr.Length; n++){
                if (isPrsErr[n]){
                    return eRTN.PRS_MATCH_FAIL;
                }
            }
            return eRTN.SUCESS;
        }

        public static void ResetPkrCal(int nThread){
            if (mIN[I.PRSCalWrite]){
                LAB_.BIT_OUT(O.PkCalReading, true);
                UTIL_.DELAY(100);
            }
            LAB_.BIT_OUT(O.PkCalReading, false);
            LAB_.BIT_OUT(O.PkCalStart, false);
            if (mOUT[O.UsePkCal]) PK_CAL_LIGHT(nThread, false);
        }
        public static eRTN PkrOnSHOT(int nThread){
            if (!mIN[I.VisionRdy] && !bDRYRUN){
                E.OnERROR(E.emsNotVisionReady);
                return eRTN.EMS;
            }
            O.SetOutput(nThread, O.PkCalStart, true, ThreadName[nThread] + " PICKER CALIBRATION START");
            if (!mOUT[O.UsePkCal]) PK_CAL_LIGHT(nThread, true);
            if (!Trigger((int)eTRIGGER.BTM)){
                O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 실패");
                return eRTN.FAIL;
            }
            return eRTN.SUCESS;
        }
        public static eRTN ReadPkCal(int nThread){
            while (!mIN[I.PRSCalWrite]){
                for (int i = 0; i < (int)prMACHINE[CP.VisionReponseOverTime]; i++){
                    UTIL_.DELAY(1);
                    if (mIN[I.PRSCalWrite]) break;
                }
                if (!mIN[I.PRSCalWrite]){
                    E.OnERROR(E.emsPkCalReadingTimeOver);
                    return eRTN.TimeOver;
                }
            }

            //PRS 데이터 파일 읽고 삭제
            if (!File.Exists(PATH_.PkCAL)){
                E.OnERROR(E.emsNotPkCalFile);
                return eRTN.NotDataFile;
            }
            string[] sLine = File.ReadAllText(PATH_.PkCAL).Split(ETC.CrLf);
            try{
                sLine[0] = sLine[0].Replace("\r", "");
                sLine[0] = sLine[0].Replace(";", "");
                string[] sRslt = sLine[0].Split(',');
                if (sRslt[3] == "1"){
                    IsDOUBLE[D.PkCal_OffsetX] = double.Parse(sRslt[0]);
                    IsDOUBLE[D.PkCal_OffsetY] = double.Parse(sRslt[1]);
                } //OK
                else{
                    O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 읽었다고 비전에 신호 OFF");
                    O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 검사 실패");
                    E.OnERROR(E.emsPkCalFail);
                    return eRTN.FAIL;
                } //FAIL
            }
            catch (Exception e){
                LogWR_.SaveLogException("PK_CAL FAIL", e);
                E.OnERROR(E.emsPkCalReadingFail, 500);
                O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 읽었다고 비전에 신호 OFF");
                O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 데이터 값 오류");
                return eRTN.ReadingDataFail;
            }
            O.SetOutput(nThread, O.PkCalReading, true, "피커 CAL' 데이터 읽었다고 비전에 신호 ON");
            for (int i = 0; i < (int)prMACHINE[CP.VisionReponseOverTime]; i++){
                if (!mIN[I.PRSCalWrite]) break;
                UTIL_.DELAY(1);
            }
            if (mIN[I.PRSCalWrite]){
                O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 읽었다고 비전에 신호 OFF");
                O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " PRS 완료");
                E.OnERROR(E.emsPkCalREadingFail); // pk cal결과값 리딩 실패
                return eRTN.TimeOver;
            }
            O.SetOutput(nThread, O.PkCalReading, false, "피커 CAL' 데이터 리딩 완료");
            O.SetOutput(nThread, O.PkCalStart, false, ThreadName[nThread] + " 피커 CAL' 완료");
            return eRTN.SUCESS;
        }

        public static eRTN Seq_HeadPkrAutoCal(int nThread){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!mIN[I.VisionRdy]){
                E.OnERROR(E.emsNotVisionReady);
                goto CalFail;
            }

            ResetPkrCal(nThread);
            UTIL_.DELAY(77);
            for (int nHD = 0; nHD < CNT_.HEAD; nHD++){
                //PICKUP
                for (int nPK = 0; nPK < CNT_.PKR; nPK++){
                    iMANUAL.bool_1 = false;
                    while (eRTN.SUCESS != MoveHDPkCenter(nThread, (eHD)nHD, nPK, mtDATA[M.HD_TH[nHD], P.PkPckUp].Pos, true, "", "[피커 AUTO CAL'] " + ((eHD)nHD).ToString() + "-" + (nPK+1).ToString() + " 피커 중심 위치 이송")) ;
                    if (eRTN.SUCESS != PkrOnSHOT(nThread)) goto CalFail;
                    if (eRTN.SUCESS != ReadPkCal(nThread)) goto CalFail;
                    MovePkCalPitch(nThread, (eHD)nHD, "피커 CAL' 옵셋 피치 이송");
                    iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                    iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];

                    for (int i = 0; i < 5; i++){
                        iMANUAL.ManualCmd = GetPkOffsetLabel(mtDATA[M.HD_TH[nHD], P.PkPckUp].Pos);
                        GetPkOffset((eHD)nHD, (ePK)nPK, mtDATA[M.HD_TH[nHD], P.PkPckUp].Pos, ref iMANUAL.XY_2);
                        iMANUAL.XY_2.x += iMANUAL.XY_1.x;
                        iMANUAL.XY_2.y -= iMANUAL.XY_1.y;
                        TEACH_.WR_PickerOffset(nPK + (8 * nHD), iMANUAL.XY_2);
                        TEACH_.WR_PickerOffset(iMANUAL.ManualCmd, nPK + (8 * nHD), iMANUAL.XY_2);
                        
                        //T: PK CAL Z / F:UNIT INSPECTION Z
                        while (eRTN.SUCESS != MoveHDPkCenter(nThread, (eHD)nHD, nPK, true, "", ((eHD)nHD).ToString() + " 피커 중심 위치 이송")) ;

                        if (eRTN.SUCESS != PkrOnSHOT(nThread)) goto CalFail;
                        if (eRTN.SUCESS != ReadPkCal(nThread)) goto CalFail;
                        if (Math.Abs(IsDOUBLE[D.PkCal_OffsetX]) < 0.01 && Math.Abs(IsDOUBLE[D.PkCal_OffsetY]) < 0.01){
                            iMANUAL.bool_1 = true;
                            break;
                        }
                        MovePkCalPitch(nThread, (eHD)nHD, "피커 CAL' 옵셋 피치 이송");
                        iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                        iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];
                    }
                    if (!iMANUAL.bool_1){
                        return eRTN.AllPkrCalFail;
                    }
                }
                //PLACE
                for (int nPK = 0; nPK < CNT_.PKR; nPK++){
                    iMANUAL.bool_1 = false;
                    while (eRTN.SUCESS != MoveHDPkCenter(nThread, (eHD)nHD, nPK, mtDATA[M.HD_TH[nHD], P.PkPlc].Pos, true, "", "[피커 AUTO CAL'] " + ((eHD)nHD).ToString() + "-" + (nPK + 1).ToString() + " 피커 중심 위치 이송")) ;
                    if (eRTN.SUCESS != PkrOnSHOT(nThread)) goto CalFail;
                    if (eRTN.SUCESS != ReadPkCal(nThread)) goto CalFail;
                    MovePkCalPitch(nThread, (eHD)nHD, "피커 CAL' 옵셋 피치 이송");
                    iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                    iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];

                    for (int i = 0; i < 5; i++){
                        iMANUAL.ManualCmd = GetPkOffsetLabel(mtDATA[M.HD_TH[nHD], P.PkPlc].Pos);
                        GetPkOffset((eHD)nHD, (ePK)nPK, mtDATA[M.HD_TH[nHD], P.PkPlc].Pos, ref iMANUAL.XY_2);
                        iMANUAL.XY_2.x += iMANUAL.XY_1.x;
                        iMANUAL.XY_2.y -= iMANUAL.XY_1.y;
                        TEACH_.WR_PickerOffset(nPK + (8 * nHD), iMANUAL.XY_2);
                        TEACH_.WR_PickerOffset(iMANUAL.ManualCmd, nPK + (8 * nHD), iMANUAL.XY_2);

                        //T: PK CAL Z / F:UNIT INSPECTION Z
                        while (eRTN.SUCESS != MoveHDPkCenter(nThread, (eHD)nHD, nPK, true, "", ((eHD)nHD).ToString() + " 피커 중심 위치 이송")) ;

                        if (eRTN.SUCESS != PkrOnSHOT(nThread)) goto CalFail;
                        if (eRTN.SUCESS != ReadPkCal(nThread)) goto CalFail;
                        if (Math.Abs(IsDOUBLE[D.PkCal_OffsetX]) < 0.01 && Math.Abs(IsDOUBLE[D.PkCal_OffsetY]) < 0.01){
                            iMANUAL.bool_1 = true;
                            break;
                        }
                        MovePkCalPitch(nThread, (eHD)nHD, "피커 CAL' 옵셋 피치 이송");
                        iMANUAL.XY_1.x = IsDOUBLE[D.PkCal_OffsetX];
                        iMANUAL.XY_1.y = IsDOUBLE[D.PkCal_OffsetY];
                    }
                    if (!iMANUAL.bool_1){
                        return eRTN.AllPkrCalFail;
                    }
                }
            }
            ResetPkrCal(nThread);
            return eRTN.SUCESS;
        CalFail:
            ResetPkrCal(nThread);
            return eRTN.FAIL;
        }

        public static eRTN UnitPlc(int nThread, eHD nX, string comment){
            int pocketX = 0, pocketY = 0;
            
            while (B.WaitBIT(nThread, B.GoodTrayWork, false, "GOOD 트레이 공급 중이면 대기")) ;
            while (B.WaitBIT(nThread, B.PkrUnitPlaceStop, true, nX.ToString() + " 플레이스 일시 정지")) ;
            IsLONG[L.CurWorkXPlc] = (int)nX;
            LogStart(nThread, comment + " 진행");
            for (int z = 0; z < CNT_.PKR; z++) {
            RePLACE:
                if (PK_Z[z + (CNT_.PKR * (int)nX)] == eSTATUS.NONE || !PICKER[(int)nX].finger[z].valid || IsBIT[B.HD_INSPECTION[(int)nX]] || PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.NG) continue; // 피커 스킵 상태 확인.
                if (PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.X_MARK || PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.TOP_FAIL) {
                    B.Bit(nThread, B.HD_REJECT[(int)nX], true, "유닛 REJECT 결과 플러그 ON");
                    continue;
                }

            ReChekTray:
                if (prMACHINE[CP.UseTrayFeederTrayCheck] == (int)eUSE.USE) {
                    if (!mIN[I.TrayCheck[IsLONG[L.CurWorkTray]]]) {  //
                        E.OnERROR(E.TrayFeederTrayVanish[IsLONG[L.CurWorkTray]], 500);
                        goto ReChekTray;
                    }
                }

                if (eRTN.SUCESS == MoveUnitPlc(nThread, nX, z, ref pocketX, ref pocketY)) {
                    while (eRTN.SUCESS != MovePK(nThread, nX, z, P.PK_PLC[z], "", nX.ToString() + " 피커" + (z + 1).ToString() + " 플레이스 위치 이송")) ;
                    PkBlow(nX, (ePK)z, stBIT.ON, stBIT.Delay);
                    UTIL_.DELAY((int)prMODEL[RP.PlaceDelay]);
                    while (eRTN.SUCESS != MovePK(nThread, nX, z, P.PK_PLC[z], "ReCheckZ", nX.ToString() + " 피커" + (z + 1).ToString() + " 플레이스 체크 위치 이송")) ;
                    PICKER[(int)nX].finger[z].valid = false;
                    PK_Z[z + (CNT_.PKR * (int)nX)] = eSTATUS.EMPTY;
                    MAP_.TrayMap_Work((int)IsLONG[L.CurWorkTray], (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], pocketX, pocketY);
                    MAP_.mapTray[(int)IsLONG[L.CurWorkTray], pocketY, pocketX] = true;
                    IsLONG[L.StageUnitGood[PICKER[(int)nX].StagePnP]]++;
                    LogWR_.DEBUG_PRINT(PICKER[(int)nX].StagePnP.ToString() + " GOOD");
                    DEF.UPH();

                    if (prMACHINE[CP.UsePlaceCheck] == (int)eUSE.USE){
                        PkVac(nX, (ePK)z, stBIT.ON, stBIT.NotDelay, "");
                        UTIL_.DELAY((int)prMACHINE[CP.PlaceCheckDalay]);
                        if (mAI[z + (8 * (int)nX)] > prMACHINE[CP.PlaceCheckVac]){
                            while (eRTN.SUCESS != MoveHDPkRdy(nThread, nX, "", (nX).ToString() + " 피커 Z축 대기 위치로 이송")) ;
                            while (eRTN.SUCESS != MoveX(nThread, nX, P.Reject, "", nX.ToString() + " 유닛 버리는 위치 이송")) ;

                            ConfirmUser[W.PlaceFail].msg = nX.ToString() + " 피커" + (z + 1).ToString() + " 플레이스 실패!" + ETC.CrLf + "피커 상태 확인 후 진행 하십시오!";
                            W.ViewWarning(nThread, W.PlaceFail);
                            while (W.WaitWarning(nThread, W.PlaceFail, "PLACE 실패로 인하여 경고 메세지 발생")) ;
                        }
                        PkBlow(nX, (ePK)z, stBIT.ON, stBIT.NotDelay);
                    }
                }
                else{
                    B.SetBit(nThread, B.GoodTrayAutoUnloading, true, "설비 RUNNING 중 GOOD TRAY 배출 모드 플로그 ON");
                    B.SetBit(nThread, B.GoodTrayWork, false, "GOOD TRAY 배출 신호 보냄");
                    while (B.WaitBIT(nThread, B.GoodTrayWork, false, "GOOD TRAY 준비 될 동안 대기")) ;
                    goto RePLACE;
                }

                if (-1 == MAP_.GetTrayPocket((eTRAY)IsLONG[L.CurWorkTray], (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref pocketX, ref pocketY)){
                    B.SetBit(nThread, B.GoodTrayWork, false, "GOOD TRAY 가득참");
                    if (z == CNT_.PKR - 1) break;  // 마지막 핑거면 빠져나가
                    while (B.WaitBIT(nThread, B.GoodTrayWork, false, "트레이 트레스퍼 준비 될 동안 대기")) ;
                }
            }
            //while (eRTN.SUCESS != MovePicRdy(nThread, nX, "", nX.ToString() + "  피커 Z축 픽업 대기 위치")) ;
            B.SetBit(nThread, B.XPlcBusy[(int)nX], false, nX.ToString() + " 유닛 플레이스 완료");
            while (eRTN.SUCESS != MoveHDPkRdy(nThread, nX, "", nX.ToString() + "  피커 Z축 대기 위치")) ;
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }
        public static eRTN MoveUnitPlc(int nThread, eHD nX, int nPK, ref int nPocketX, ref int nPocketY){
            MAP_.GetTrayPocket((eTRAY)IsLONG[L.CurWorkTray], (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref nPocketX, ref nPocketY);
            if (nPocketY >= 1){
                while (B.WaitBIT(nThread, B.GoodTray1ULDConv, B.GoodTray2ULDConv, true, true, "콘베어 언로딩 대기 중 대기", stBIT.OR)) ;
            }
        ReCHECK:
            if (!mOUT[O.GoodTrayFrontGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]] || !mOUT[O.GoodTrayBackGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]] ||
                    mOUT[O.GoodTrayFrontUnGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]] || mOUT[O.GoodTrayBackUnGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]] ||
                    mIN[I.GoodTrayFrontUnGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]] || mIN[I.GoodTrayBackUnGrip[(int)(eTRAY)IsLONG[L.CurWorkTray]]])
            {
                E.OnERROR(E.ChkGoodTrayTransfer[(int)(eTRAY)IsLONG[L.CurWorkTray]]);
                goto ReCHECK;
            } //그리퍼 그립 안되어 있음.
            if (prMACHINE[CP.UseTrayCheckSensor] == (int)eUSE.USE){
                if (!mIN[I.TrayCheck[(int)(eTRAY)IsLONG[L.CurWorkTray]]]){
                    ConfirmUser[W.TrayDisappear].msg = "GOOD TRAY TRANSFER " + ((int)(eTRAY)IsLONG[L.CurWorkTray] + 1).ToString() + "번  트레이 사라짐. 트레이 안착 상태 확인 바랍니다." + ETC.CrLf + "(YES : 트레이 배출 후 새로운 트레이 로딩 후 작업 진행 || NO : 트레이 안착 상태 재확인)";
                    W.ViewWarning(nThread, W.TrayDisappear);
                    while (W.WaitWarning(nThread, W.TrayDisappear, "HEAD2 PICK-UP 실패로 인하여 경고 메세지 발생")) ;
                    if (ConfirmUser[W.TrayDisappear].result){
                        DEF.ResetTray((eTRAY)IsLONG[L.CurWorkTray]);
                        return eRTN.VANISH; //yes : 트레이 
                    }
                    else goto ReCHECK;  //no : 트레이 안착 상태 재확인
                }
            }
            while (eRTN.SUCESS != MoveXPlc(nThread, nX, nPK, (eTRAY)IsLONG[L.CurWorkTray], nPocketX, nPocketY, stBIT.NotCAM, nX.ToString() + " -> " + ((eTRAY)IsLONG[L.CurWorkTray]).ToString() + " [" + nPocketX.ToString() + "/" + nPocketY.ToString() + "] 위치 이송", true)) ;
            return eRTN.SUCESS;
        }

        static void AddReworkUnit(){
            IsLONG[L.ReworkCnt]++;
            IsLONG[L.DayReworkUnit]++;
            IsLONG[L.OutCnt]++;
        }
        public static eRTN PkNGPlc(int nThread, eHD nX, string comment){
            int pocketX = 0, pocketY = 0;
            if (!IsBIT[B.HD_NG[(int)nX]] && !IsBIT[B.HD_INSPECTION[(int)nX]]){
                B.SetBit(nThread, B.NGPlcBusy[(int)nX], false, nX.ToString() + "  NG 유닛 플레이스 없음");
                return eRTN.SUCESS;
            }

            while (B.WaitBIT(nThread, B.ReWorkTrayWork, false, "NG 트레이 공급 중이면 대기")) ;
            LogStart(nThread, comment + " 진행");
            for (int z = 0; z < CNT_.PKR; z++) {
                if (PK_Z[z + (CNT_.PKR * (int)nX)] == eSTATUS.NONE || !PICKER[(int)nX].finger[z].valid ||
                    !(PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.NG || /*PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.X_MARK ||*/
                    PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.FIRST || PICKER[(int)nX].finger[z].iResult == (int)eSTATUS.SECOND)) continue;

                MAP_.GetTrayPocket(eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref pocketX, ref pocketY);

            ReChekTray:
                if (prMACHINE[CP.UseTrayFeederTrayCheck] == (int)eUSE.USE) {
                    if (!mIN[I.NG_TRAY_FEEDER_TRAY_CHECK]) {
                        E.OnERROR(E.emsReworkTrayVanish, 500);
                        goto ReChekTray;
                    }
                }
                while (eRTN.SUCESS != MoveXPlc(nThread, nX, z, eTRAY.REWORK, pocketX, pocketY, stBIT.NotCAM, nX.ToString() + " -> " + (eTRAY.REWORK).ToString() + " [" + pocketX.ToString() + "/" + pocketY.ToString() + "] 위치 이송", true)) ;
                while (eRTN.SUCESS != MovePK(nThread, nX, z, P.PK_PLC[z], "", nX.ToString() + " 피커" + (z + 1).ToString() + " 플레이스 위치 이송")) ;
                PkBlow(nX, (ePK)z, stBIT.ON, stBIT.Delay);
                UTIL_.DELAY((int)prMODEL[RP.PlaceDelay]);
                while (eRTN.SUCESS != MovePK(nThread, nX, z, P.PK_PLC[z], "ReCheckZ", nX.ToString() + " 피커" + (z + 1).ToString() + " 플레이스 체크 위치 이송")) ;
                PK_Z[z + (CNT_.PKR * (int)nX)] = eSTATUS.EMPTY;
                PICKER[(int)nX].finger[z].valid = false;
                MAP_.TrayMap_Reject((int)eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], pocketX, pocketY);
                MAP_.mapTray[(int)eTRAY.REWORK, pocketY, pocketX] = true;
                IsLONG[L.StageUnitNG[PICKER[(int)nX].StagePnP]] ++;
                AddReworkUnit();
                if (-1 == MAP_.GetTrayPocket(eTRAY.REWORK, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY], ref pocketX, ref pocketY)){
                    B.SetBit(nThread, B.ReWorkTrayWork, false, "REWORK TRAY 가득참");
                    if (z == CNT_.PKR - 1) break;  // 마지막 핑거면 빠져나가
                    while (B.WaitBIT(nThread, B.ReWorkTrayWork, false, "REWORK 트레이 준비 될 동안 대기")) ;
                }
            }
            //while (eRTN.SUCESS != MovePicRdy(nThread, nX, "", nX.ToString() + "  피커 Z축 픽업 대기 위치")) ;
            B.SetBit(nThread, B.NGPlcBusy[(int)nX], false, nX.ToString() + "  NG 유닛 플레이스 완료");
            while (eRTN.SUCESS != MoveHDPkRdy(nThread, nX, "", nX.ToString() + "  피커 Z축 대기 위치")) ;
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        static void AddRejectUnit(){
            IsLONG[L.NGCnt]++;
            IsLONG[L.DayRejectUnit]++;
            IsLONG[L.OutCnt]++;
        }
        public static eRTN PkRejectPlc(int nThread, eHD nX, eMAP_BLOCK nStage, string comment){
            //while (eRTN.SUCESS != MovePicRdy(nThread, nX, "", nX.ToString() + "  피커 Z축 픽업 대기 위치")) ;
        ReCheck:
            if (prMACHINE[CP.UseRejectBoxCheck] == (int)eUSE.USE){
                if (!mIN[I.REJECT_BOX]){
                    E.OnERROR(E.emsRejectBoxVanish, 500);
                    goto ReCheck;
                }
                if (!mIN[I.REJECT_BOX_FULL_CHECK1] || !mIN[I.REJECT_BOX_FULL_CHECK2]){
                    if (prMACHINE[CP.UseNGBoxError] == (int)eUSE.USE) {
                        E.OnERROR(E.emsRejectBoxFullCheck, 500);
                        goto ReCheck;
                    }
                }
            }
        
            if (!IsBIT[B.HD_REJECT[(int)nX]]) return eRTN.SUCESS;
            LogStart(nThread, comment + " 진행");
            while (eRTN.SUCESS != MoveX(nThread, nX, P.Reject, "", nX.ToString() + " 유닛 버리는 위치 이송")) ;
            for (int i = 0; i < CNT_.PKR; i++){
                if (PK_Z[i + (CNT_.PKR * (int)nX)] == eSTATUS.NONE || !PICKER[(int)nX].finger[i].valid) continue;
                PK_Z[i + (CNT_.PKR * (int)nX)] = eSTATUS.EMPTY;
                PICKER[(int)nX].finger[i].valid = false;
                IsLONG[L.StageUnitXOut[PICKER[(int)nX].StagePnP]]++;
                LogWR_.DEBUG_PRINT(PICKER[(int)nX].StagePnP.ToString() + " NG");
                if (prMACHINE[CP.UseSizeNGRejectBox] == (int)eUSE.USE){ 
                    if (IsLONG[L.StageNgCount[(int)nStage]] > 0){
                        IsLONG[L.StageNgCount[(int)nStage]]--;
                    }
                    else{
                        AddRejectUnit();
                    }
                }
                else AddRejectUnit();
            }
            PkBlow(nX, true);
            UTIL_.DELAY((int)prMACHINE[CP.RejectBlowDelay]);
            PkBlow(nX, false);
            for (int i = 0; i < CNT_.PKR; i++){
                if (PK_Z[i + (CNT_.PKR * (int)nX)] == eSTATUS.NONE) continue;
                PK_Z[i + (CNT_.PKR * (int)nX)] = eSTATUS.EMPTY;
                PICKER[(int)nX].finger[i].valid = false;
            }
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }
        public static void PkVacReset(eHD nX){
            for (int p = 0; p < CNT_.PKR; p++){
                PkFree(nX, (ePK)p);
            }
        }

        public static eRTN MoveXPlc(int nThread, eHD nX, int nPK, eTRAY nFeeder, int nPocketX, int nPocketY, bool CamView, string comment, bool PRSOffset = false){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsCalZigNotBackPos, true)) return eRTN.EMS;

            //인터락 추가
            bool bGT1_PLACE_POS = (mtSTS[M.TrayFeeder1].CurrentPosition > prMACHINE[CP.TrayPlace_Interlock2] && mtSTS[M.TrayFeeder1].CurrentPosition < prMACHINE[CP.TrayPlace_Interlock1]) ? true : false;
            bool bGT2_PLACE_POS = (mtSTS[M.TrayFeeder2].CurrentPosition > prMACHINE[CP.TrayPlace_Interlock2] && mtSTS[M.TrayFeeder2].CurrentPosition < prMACHINE[CP.TrayPlace_Interlock1]) ? true : false;
            int ChkGoodTrayGripF = nFeeder == eTRAY.GOOD1 ? I.GoodTrayFrontUnGrip[(int)eTRAY.GOOD2] : I.GoodTrayFrontUnGrip[(int)eTRAY.GOOD1]; //conveyor 방향 그립
            int ChkGoodTrayGripB = nFeeder == eTRAY.GOOD1 ? I.GoodTrayBackUnGrip[(int)eTRAY.GOOD2] : I.GoodTrayBackUnGrip[(int)eTRAY.GOOD1]; //stacker 방향 그립
            if (nFeeder == eTRAY.GOOD1 || nFeeder == eTRAY.GOOD2){
                if (!mIN[ChkGoodTrayGripF] && !mIN[ChkGoodTrayGripB]){
                    if (nFeeder == eTRAY.GOOD1){
                        if (!bGT1_PLACE_POS){
                            if (bGT2_PLACE_POS){
                                if (mtDATA[M.TrayFeeder2, P.Psh].Pos > mtSTS[M.TrayFeeder2].CurrentPosition){
#if _NSS3300
#else
                                    if (!mIN[I.GOOD_RAIL_HEAD1_CHECK] || !mIN[I.GOOD_RAIL_HEAD2_CHECK]){
                                        E.OnERROR(E.emsNotMoveFeeder1PlcPos, 500);
                                        return eRTN.FAIL;
                                    }
#endif
                                }
                                else{
#if _NSS3300
                                    if (!mIN[I.GOOD_TRAY2_UNGRIP_C]){
#else
                                    if (!mIN[I.GOOD_RAIL_HEAD1_CHECK] || !mIN[I.GOOD_RAIL_HEAD2_CHECK] || !mIN[I.GOOD_TRAY2_UNGRIP_C]){
#endif
                                        E.OnERROR(E.emsNotMoveFeeder1PlcPos, 500);
                                        return eRTN.FAIL;
                                    }
                                }
                            }
                        }
                    }
                    else{
                        if (!bGT2_PLACE_POS){
                            if (bGT1_PLACE_POS){
                                if (mtDATA[M.TrayFeeder1, P.Psh].Pos > mtSTS[M.TrayFeeder1].CurrentPosition){
#if _NSS3300
#else
                                    if (!mIN[I.GOOD_RAIL_HEAD1_CHECK] || !mIN[I.GOOD_RAIL_HEAD2_CHECK]){
                                        E.OnERROR(E.emsNotMoveFeeder2PlcPos, 500);
                                        return eRTN.FAIL;
                                    }
#endif
                                }
                                else{
#if _NSS3300
                                    if (!mIN[I.GOOD_TRAY1_UNGRIP_C]){
#else
                                    if (!mIN[I.GOOD_RAIL_HEAD1_CHECK] || !mIN[I.GOOD_RAIL_HEAD2_CHECK] || !mIN[I.GOOD_TRAY1_UNGRIP_C]){
#endif
                                        E.OnERROR(E.emsNotMoveFeeder2PlcPos, 500);
                                        return eRTN.FAIL;
                                    }
                                }
                            }
                        }
                    }
                }
            } //GOOD TRAY
            else{
                if (!mIN[I.NG_TRAY_UNGRIP1] && !mIN[I.NG_TRAY_UNGRIP2]){

                }
            } //REWORK TRAY

            int index;
            if ((nPocketY + 1) > 1) index = (int)(prMODEL[RP.TrayCntX] * (nPocketY)) + (nPocketX + 1);
            else index = (nPocketY + 1) * (nPocketX + 1);

            int mtZ = M.HD1Pk[nPK];
            if (nX == eHD.HD2) mtZ = M.HD2Pk[nPK];

            double dPkrPitch = prMACHINE[CP.PickerPitch] * nPK;
            double dPkrOffsetX = prMACHINE[CP.PkPlcOffsetX[(int)nX]];
            double dPkrOffsetY = prMACHINE[CP.PkPlcOffsetY[(int)nX]];
            GetDistanceBetweenHDCamAndPkr(nX, ref Distance);

            stMoveInfo mx = M.GetMoveInfo(M.HD[(int)nX], P.HD_PLC[(int)nFeeder]);
            stMoveInfo mt = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPlc);
            stMoveInfo mz = M.GetMoveInfo(mtZ, P.PK_PLC[nPK]);
            stMoveInfo my = M.GetMoveInfo(M.TRAY_FEEDER[(int)nFeeder], P.Tray_Place[(int)nX]);
            GetPkOffset(nX, (ePK)nPK, mt.Pos, ref Pkr_Offset);

            mx.Pos = TryCalPos_[(int)nX, (int)nFeeder, index - 1].x;
            my.Pos = TryCalPos_[(int)nX, (int)nFeeder, index - 1].y;
            if (!CamView){
                mx.Pos = mx.Pos + Distance.x + dPkrPitch + dPkrOffsetX + Pkr_Offset.x;
                if (nX == (int)eHD.HD1) my.Pos = my.Pos + Distance.y + dPkrOffsetY + Pkr_Offset.y;
                else                    my.Pos = my.Pos + Distance.y + dPkrOffsetY + Pkr_Offset.y;

                if (PRSOffset && prMACHINE[CP.UesBtmInspection] == (int)eUSE.USE) {
                    LogWR_.SavePRSLog("PLACE - " + nX.ToString() + " Pkr" + nPK.ToString() + " x = " + mx.Pos.ToString() + "[" + PICKER[(int)nX].finger[nPK].prsX.ToString() + "] y = " + my.Pos + " [" + PICKER[(int)nX].finger[nPK].prsY.ToString() +  "] t = " + mt.Pos.ToString() + " [" + PICKER[(int)nX].finger[nPK].prsR + " ]", "");
                    if (prMACHINE[CP.UsePRSOffset] == (int)eUSE.USE) {
                        mx.Pos = mx.Pos + (PICKER[(int)nX].finger[nPK].prsX * -1);
                        my.Pos = my.Pos + PICKER[(int)nX].finger[nPK].prsY;
                        if (prMACHINE[CP.UsePRSOffsetT] == (int)eUSE.USE) {
                            mt.Pos = mt.Pos + PICKER[(int)nX].finger[nPK].prsR;
                        }
                        LogWR_.SavePRSLog("[PLACE] x = " + mx.Pos.ToString() + " y = " + my.Pos + " t = " + mt.Pos.ToString(), "");
                    }
                } //x축은 부호 반대!/  t축은 방향 맞음 / y축 방향 맞음
            } //검증!

            if (nPK == (int)ePK.PKR1 || nPK == (int)ePK.PKR3 || nPK == (int)ePK.PKR5 || nPK == (int)ePK.PKR7) mz.Pos += prMACHINE[CP.PlaceCheckPitch];//1,3,5,7 -
            else mz.Pos -= prMACHINE[CP.PlaceCheckPitch]; //HD2 //2,4,6,8 +

            if (nPK == 0 || eMCStatus != eMachineStatus.AUTO || mtDATA[M.HD[(int)nX], P.BTMCamCenter].Pos < mtSTS[M.HD[(int)nX]].CurrentPosition) mz.Pos = M.GetPosData(mtZ, P.Ready); // 만약 TRAY TRANSFER에서 PALLET로 이송하면 (첫번째 구동 조건)
            if (index > 1){
                my.Spd = prMODEL[RP.TrayWorkSpeed];
                my.Acc = my.Spd * 10;
                my.Dec = my.Spd * 10;
            }
            if ((Math.Abs(mx.Pos - LAB_.GET_ACTPOS(M.HD[(int)nX])) < 50) || eMCStatus != eMachineStatus.AUTO){
                mx.Spd /= 2;
                mx.Acc = mx.Spd * 10;
                mx.Dec = mx.Spd * 10;
            }

            stMoveInfo[] ms = { mx, mt, my, mz };
            int[] mot = { M.HD[(int)nX], M.HD_TH[(int)nX], M.TRAY_FEEDER[(int)nFeeder], mtZ };
            double[] dToller = { 0.01, 0.01, 0.01, 0.01 };
            bool[] bOnlyStart = { false, false, false, false };
            bool[] bNoChange = { true, true, true, true };
            bool[] bDontStop = { false, false, false, false };
            string[] cmds = { "", "", "", "" };

            string sLocation = LogWR_.LogPos(mot, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mot, ms, dToller, bOnlyStart, bNoChange, bDontStop, cmds, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public static eRTN MovePK(int nThread, eHD nX, int nPK, int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            int motZ = M.HD1Pk[nPK];
            if (nX == eHD.HD2) motZ = M.HD2Pk[nPK];
            stMoveInfo mZ = M.GetMoveInfo(motZ, nPos);

            if (cmd == "ReCheckZ"){
                double PicChkPitch = (prMACHINE[CP.PicUpCheckPitch] + prMODEL[RP.UnitThickess]) < (prMODEL[RP.UnitThickess] + 1) ? (prMODEL[RP.UnitThickess] + 1) : prMACHINE[CP.PicUpCheckPitch] + prMODEL[RP.UnitThickess];
                if (nPK == (int)ePK.PKR1 || nPK == (int)ePK.PKR3 || nPK == (int)ePK.PKR5 || nPK == (int)ePK.PKR7) mZ.Pos += PicChkPitch;//+
                else mZ.Pos -= PicChkPitch;//-
            }

            string sLocation = LogWR_.LogPos(motZ, mZ);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, motZ, mZ, 0.005, false, true, true, cmd, sLog)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public static eRTN MoveMasterZigCenter(int nThread, eHD nX, ePK nPk, eMAP_BLOCK nStage, bool CamView, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            GetDistanceBetweenHDCamAndPkr(nX, ref Distance);
            double PkPitch = prMACHINE[CP.PickerPitch] * (int)nPk;
            double PkOffsetX = prMACHINE[CP.PkPicOffsetX[(int)nX]];
            double PkOffsetY = prMACHINE[CP.PkPicOffsetY[(int)nX]];

            stMoveInfo mx = M.GetMoveInfo(M.HD[(int)nX], P.HDMasterZig[(int)nStage]);
            stMoveInfo mt = M.GetMoveInfo(M.HD_TH[(int)nX], P.PkPckUp);
            stMoveInfo my = M.GetMoveInfo(M.DRY_TABLE[(int)nStage], P.StageMasterZig[(int)nX]);
            GetPkOffset(nX, nPk, mt.Pos, ref Pkr_Offset);

            if (!CamView){
                mx.Pos = mx.Pos + Distance.x + PkPitch + PkOffsetX + Pkr_Offset.x;
                if (nX == (int)eHD.HD1) my.Pos = my.Pos - Distance.y + PkOffsetY + Pkr_Offset.y;
                else my.Pos = my.Pos + Distance.y + PkOffsetY + Pkr_Offset.y;
            }

            stMoveInfo[] ms = { mx, mt, my };
            int[] mot = { M.HD[(int)nX], M.HD_TH[(int)nX], M.DRY_TABLE[(int)nStage] };
            double[] toller = { 0.01, 0.01, 0.01 };
            bool[] onlyStart = { false, false, false };
            bool[] noChange = { true, true, true };
            bool[] dontStop = { true, true, true };
            string[] cmds = { cmd, "", "" };

            string sLocation = LogWR_.LogPos(mot, ms);
            string sLog = comment + " " + sLocation;
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, mot, ms, toller, onlyStart, noChange, dontStop, cmds, sLog)) return eRTN.FAIL;

            return eRTN.SUCESS;
        }
#endregion
    }
}