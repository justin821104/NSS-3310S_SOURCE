using Object;
using System;
using System.Windows.Forms;
using LIB_.DateType;
using System.IO;

namespace NSS_3310S.SEQ.MODULE{
    public class GRIPPER : BASE{
        readonly int nThread = T.Gripper;
        string cmds;
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
                while (B.WaitBIT(nThread, B.InRailRequest, false, "레일단 앞단 공급 할때까지 대기")) ;
                Process("스트립 공급");
                Tack();
            } while (true);
        }

        #region >> SEQ
        public void Process(string comment){
            if (LAB_.INPUT(I.RAIL_EXIST2)){

            }//레일 위에 스트립 공급 되어 있으면

            LogStart(nThread, comment + " [" + IsLONG[L.CurSlotCount].ToString("00") + "]");
            B.SetBit(nThread, B.GripperWorking, true, "그리퍼 작업 진행");
            while (eRTN.SUCESS != InLET_DOWN("인-렛 테이블 다운")) ;
            while (eRTN.SUCESS != MoveRail(P.StripIn, "", "인-렛 레일 스트립 받은 위치로 이송")) ;
            while (eRTN.SUCESS != C.Magazine.PusherForward("푸셔 전진")) ;
            while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;

            while (mIN[I.RAIL_EXIST2]){
                E.OnERROR(E.emsInLetTableStripCheck, 500);
            }

            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.GripperBackPitch]);
            while (eRTN.SUCESS != MoveX(P.StripPick, cmds, "그리퍼 X축 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.StripPick, "spd=10", "그리퍼 X축 픽업 위치 이송")) ;
            while (eRTN.SUCESS != Grip("그리퍼 그립")) ;
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.GripperBackPitch]) + ":spd=30";
            while (eRTN.SUCESS != MoveX(P.StripPick, cmds, "그리퍼 X축 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != C.Magazine.PusherBackward("푸셔 후진")) ;
            if (!bDRYRUN){
                if (!LAB_.INPUT(I.RAIL_EXIST1) && !LAB_.INPUT(I.GRIPPER_DETECT)){
                    while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
                    B.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");
                    B.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
                    return;
                } // 레일 앞단 센서 && 그리퍼 스트립 그립 확인 센서 CHECK 
            }
        ReTrayBarcode:
            if (!ReadBarcode()){
                //STRIP 제거 할건지 다시 바코드 검사 할건지 CHECK
                W.ViewWarning(nThread, W.Rail_StripRemove);
                while (W.WaitWarning(nThread, W.Rail_StripRemove, "바코드 검사 실패")) ;
                if (ConfirmUser[W.Rail_StripRemove].result){ //스트립 제거
                    while (eRTN.SUCESS != Grip("그리퍼 그립")) ;
                    while (eRTN.SUCESS != MoveX(P.StripOpn, "", "그리퍼 X축 스트립 그립 OPEN 위치")) ;
                    while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
                    while (eRTN.SUCESS != MoveX(P.Ready, "", "그리퍼 X축 대기 위치 이송")) ;
                    //레일 위에 스트립 존재 확인 !
                    while (LAB_.INPUT(I.RAIL_EXIST1) || LAB_.INPUT(I.RAIL_EXIST2)){
                        E.OnERROR(E.emsRailStrpRemove);
                    }
                    B.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");
                    goto StripRemove;
                }
                goto ReTrayBarcode;
            }
            AddStrip();
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                CLOT.GET_LOT.InCnt++;
                try{
                    if (File.Exists(PATH_.StripOverlap)){
                        string[] CheckOverlap = File.ReadAllText(PATH_.StripOverlap).Split(ETC.CrLf);
                        for (int n = 0; n < CheckOverlap.Length; n++) {
                            string[] sRslt = CheckOverlap[n].Split(',');
                            if (sRslt.Length <= 1) continue;
                            if (CLOT.InfoStrip[nThread].Barcode == sRslt[0]){
                                CLOT.InfoStrip[nThread].Overlap = true;
                                CLOT.InfoStrip[nThread].Index = int.Parse(sRslt[1]);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex){
                    LogWR_.SaveLogException("OVERLAP CHECK FAIL", ex);
                }
                if (!CLOT.InfoStrip[nThread].Overlap) {
                    CLOT.InfoStrip[nThread].Index = (int)IsLONG[L.StripCnt];
                    SUBFRM_.gSecsGem.SetPanelLineIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode);
                    TEACH_.WRITE_STRIP_INFO(CLOT.InfoStrip[nThread].Barcode + "," + CLOT.InfoStrip[nThread].Index);
                }
                else{
                    LogWR_.SaveBarcodeHistory("LOT-VALIDATION -> [PANEL_LINE_IN] not send overlap true " + CLOT.InfoStrip[nThread].Barcode, "");
                    SubTractStrip();
                }
                CLOT.GET_LOT.LoadingCount = (int)IsLONG[L.StripCnt];

                if (CLOT.GET_LOT.LoadingCount <= 1){
                    CLOT.GET_LOT.Recipe = sGroupName + "/" + sJobName;
                }
            }
            while (eRTN.SUCESS != Grip("그리퍼 그립")) ;
            while (eRTN.SUCESS != MoveX(P.StripOpn, "", "그리퍼 X축 스트립 그립 OPEN 위치")) ;
            B.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");

        StripReCheck:
            while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
            if (!LAB_.INPUT(I.RAIL_EXIST2) && !bDRYRUN){
                W.ViewWarning(nThread, W.GripperStripPicFail);
                while (W.WaitWarning(nThread, W.GripperStripPicFail, "그리퍼 스트립 로딩 중 스트립 사라짐")) ;
                if (ConfirmUser[W.GripperStripPicFail].result) goto StripReCheck; 
                else {
                    B.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
                    return;
                }
            }//레일에 스트립 유무 확인

            while (eRTN.SUCESS != MoveStripAlign("스트립 피커 공급 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.Ready, "", "그리퍼 X축 대기 위치 이송")) ;
            InLetTableVac(stBIT.ON);
            while (eRTN.SUCESS != InLET_UP("인-렛 테이블 업")) ;
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                if (!CLOT.InfoStrip[nThread].Overlap)
                    SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.IN_LET);
            }
            if (bMF) return;
            B.SetBit(nThread, B.StripPkRequest, true, "레일 위 스트립 공급");
            while (B.WaitBIT(nThread, B.StripPkRequest, true, "스트립 픽업 해 갈때까지 대기")) ;
        StripRemove:
            B.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
            LogEnd(nThread, comment + " 완료");
        }

        public bool ReadBarcode() {
        //ReCheckBarcodeReading:
            SUBFRM_.cBarcode.ReadResult = "";
            bWriteBarcode = false;
            if (prMACHINE[CP.UseBarcode] == (int)eUSE.NotUSE) {
                CLOT.WriteBackupInfoBarcode(eSeqBacode.Gripper, CLOT.InfoStrip[nThread].Barcode, CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Overlap);
                return true;
            }

            while (eRTN.SUCESS != MoveBarcodeReading("스트립 바코드 리딩 위치 이송")) ;
            SUBFRM_.cBarcode.Invoke(new MethodInvoker(delegate ()
            {
                SUBFRM_.cBarcode.Trigger();
            }));

            int nCNT = 0;
            do{
                UTIL_.DELAY(2);
                nCNT++;
                if (bWriteBarcode) goto BarCodeOk;
            } while (nCNT < (int)prMACHINE[CP.BarcodeReadingCheck]);
            //if (!bBD) {
                W.ViewWarning(nThread, W.BarcodeReadingTimeOver, "바코드 리딩 시간 오버 되었습니다. 바코드 연결 상태 및 바코드 상태 확인 바랍니다."); //ConfirmUser[nWAR].msg = ;
                while (W.WaitWarning(nThread, W.BarcodeReadingTimeOver, "바코드 리딩 타임 오버")) ;
                //goto ReCheckBarcodeReading;
            //} // 바코드 리딩 시간 초과 되어 알람 발생 
        BarCodeOk:
            if ((!bWriteBarcode && prMACHINE[CP.UseBarcode] == (int)eUSE.USE) || (SUBFRM_.cBarcode.ReadResult == "" && prMACHINE[CP.UseBarcode] == (int)eUSE.USE)){
                while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
                W.ViewWarning(nThread, W.BarcoderReadingFail);
                while (W.WaitWarning(nThread, W.BarcoderReadingFail, "스트립 바코드 리딩 알람 발생"));
                if (ConfirmUser[W.BarcoderReadingFail].result){
                    return false;
                }
                else{
                    string sValue = UTIL_.INPUT_MESSAGE("BARCODE", "스트립 바코드 수동 입력", "", false);
                    if (sValue == ""){
                        E.OnERROR(E.BarcoderWriteFail);
                        return false;
                    }
                    CLOT.InfoStrip[nThread].Barcode = sValue;
                    goto Pass;
                }
            }

            if (SUBFRM_.cBarcode.ReadResult.Length > 17){
                SUBFRM_.cBarcode.ReadResult = SUBFRM_.cBarcode.ReadResult.Substring(0, 17);
            } // STRIP BARCODE 17 자리 이상이면 17자리까지만 가져오기!
            CLOT.InfoStrip[nThread].Barcode = SUBFRM_.cBarcode.ReadResult;
        Pass:            
            string[] sList  = CLOT.InfoStrip[nThread].Barcode.Split(' ');
            string sBCD     = CLOT.InfoStrip[nThread].Barcode;
            int itslength   = CLOT.GET_LOT.ItsID.Length;
            string sits     = "";
            if (sBCD.Length > CLOT.GET_LOT.ItsID.Length){
                sits = sBCD.Substring(0, itslength);
            }
            if (CLOT.GET_LOT.LotID != sList[0]){
                if (CLOT.GET_LOT.ItsID != sits){
                    E.OnERROR(E.emsLotIDFail);
                    return false;
                }    
            }
            //스트립 정보 저장
            TEACH_.WRITE_BARCODE_FILE(CLOT.InfoStrip[nThread].Barcode);
            CLOT.WriteBackupInfoBarcode(eSeqBacode.Gripper, CLOT.InfoStrip[nThread].Barcode, CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Overlap);
            TEACH_.WRITE_INFO_STIP_BARCODE(CLOT.InfoStrip[nThread].Barcode);
            return true;
        }
        #endregion

        #region>> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.GripperCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",GRIPPER," + IsDOUBLE[D.GripperCycle].ToString(), "");
            TackStart = Environment.TickCount;
        }
        void AddStrip(){
            IsLONG[L.StripCnt]++;
            IsLONG[L.DayStripCnt]++;
        }
        void SubTractStrip(){
            IsLONG[L.StripCnt] -= 1;
            IsLONG[L.DayStripCnt] -= 1;
        }

        public eRTN Grip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GripperGripFail, I.GRIPPER_CLOSE, I.GRIPPER_OPEN, O.GRIPPER_CLOSE, O.GRIPPER_OPEN, (int)prMACHINE[CP.GripperLockDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN UnGrip(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.GripperUnGripFail, I.GRIPPER_OPEN, I.GRIPPER_CLOSE, O.GRIPPER_OPEN, O.GRIPPER_CLOSE, (int)prMACHINE[CP.GripperUnlockDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN InLET_UP(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsGripperNotMoveRdy, true)) return eRTN.EMS;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.InLetTableUpFail, I.INLET_TABLE_UP, I.INLET_TABLE_DN, O.INLET_TABLE_UP, O.INLET_TABLE_DN, (int)prMACHINE[CP.InletTableUp], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN InLET_DOWN(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.InLetTableDnFail, I.INLET_TABLE_DN, I.INLET_TABLE_UP, O.INLET_TABLE_DN, O.INLET_TABLE_UP, (int)prMACHINE[CP.InletTableDn], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public void InLetTableVac(bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.InletVac] : 50;
#if _NSS3300
#else
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, false);
            LAB_.OUTPUT(O.INLET_TABLE_VAC, bFlog);
#endif
            UTIL_.DELAY(nDelay);
        }
        public void InletTableBlow(){
#if _NSS3300
#else
            LAB_.OUTPUT(O.INLET_TABLE_VAC, false);
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, true);
            UTIL_.DELAY((int)prMACHINE[CP.InletBlow]);
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, false);
#endif
        }

        public eRTN MoveRail(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (nPos == P.Ready){
                //UTIL_.OnERROR(E.emsInRailStripCheck);
                //return eRTN.FAIL;
            }
#if _NSS3300
            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(M.Rail, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.Rail, nPos, 0.005, false, false, false, cmd, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
#else
            int[] ps = { nPos, nPos };
            double[] tollers = { 0.005, 0.005 };
            bool[] OnlyStarts = { false, false };
            bool[] NoChanges = { false, true };
            bool[] DontStops = { false, false };
            string[] cmds = { cmd, cmd };
            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(M.RAIL, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, M.RAIL, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }
        public eRTN MoveX(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.GripperXNotMove, true)) return eRTN.EMS;
            
            if (nPos == P.StripLoad){
                if (prMODEL[RP.UseStripLoadngPos] == (int)ePARA.RECIPE) nPos = P.RecipStripLoad;
            }
            
            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(M.GrpX, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.GrpX, nPos, 0.005, false, false, false, cmd, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveBarcode(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(M.Barcode, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.Barcode, nPos, 0.005, false, false, false, cmd, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveBarcodeReading(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //strip pk z 높이 및 in-let table 상태 확인
            if (!C.Interlock.ChkInterlock(E.emsInLetTableNotDown, true)) return eRTN.EMS;

            int[] ms = { M.GrpX, M.Barcode };
            int[] ps = { P.StripBcd, P.BcdRead };
            double[] tollers = { 0.005, 0.005 };
            bool[] OnlyStarts = { false, false };
            bool[] NoChanges = { true, false };
            bool[] DontStops = { false, false };
            string[] cmds = { "", "" };
            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(ms, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, ms, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveStripAlign(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //strip pk z 높이 및 in-let table 상태 확인
            if (!C.Interlock.ChkInterlock(E.GripperXNotMove, true)) return eRTN.EMS;
#if _NSS3300
            int[] ms = { M.GrpX, M.Rail };
            int[] ps = { P.StripLoad, P.StripAlign };
            double[] tollers = { 0.005, 0.005 };
            bool[] OnlyStarts = { false, false };
            bool[] NoChanges = { true, true };
            bool[] DontStops = { true, true };
            string[] cmds = { "spd=5", "spd=5" };
#else
            int[] ms = { M.GrpX, M.RailF, M.RailR };
            int[] ps = { P.StripLoad, P.StripAlign, P.StripAlign };
            double[] tollers = { 0.005, 0.005, 0.005 };
            bool[] OnlyStarts = { false, false, false };
            bool[] NoChanges = { true, true, true };
            bool[] DontStops = { true, true, true };
            string[] cmds = { "spd=5", "spd=5", "spd=5" };

#endif

            if (prMODEL[RP.UseStripLoadngPos] == (int)ePARA.RECIPE) ps[0] = P.RecipStripLoad;

            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(ms, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, ms, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
#endregion
    }
}