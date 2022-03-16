using Object;
using System;
using System.Windows.Forms;
using LIB_.DateType;

namespace NSS_3310S.SEQ.MODULE{
    public class GRIPPER : BASE{
        
        int nThread = T.Gripper;
        string cmds;
        long TackStart = 0, TackEnd = 0;

        public void DoAuto(){
            do{
                if (gExit) break;
                UTIL_.DELAY(10);
                if (eMCStatus != eMachineStatus.AUTO) continue;

                while (UTIL_.WaitBIT(nThread, B.InRailRequest, false, "레일단 앞단 공급 할때까지 대기")) ;
                Process("스트립 공급");
                Tack();
            } while (true);
        }

        #region >> SEQ
        public void Process(string comment){
            if (LAB_.INPUT(I.RAIL_EXIST2)){

            }//레일 위에 스트립 공급 되어 있으면

            LogStart(nThread, comment + " [" + IsLONG[L.CurSlotCount].ToString("00") + "]");
            COM_.SetBit(nThread, B.GripperWorking, true, "그리퍼 작업 진행");
            while (eRTN.SUCESS != InLET_DOWN("인-렛 테이블 다운")) ;
            while (eRTN.SUCESS != MoveRail(P.StripIn, "", "")) ;
            while (eRTN.SUCESS != C.Magazine.PusherForward("푸셔 전진")) ;
            while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;

            while (mIN[I.RAIL_EXIST2]){
                UTIL_.OnERROR(E.emsInLetTableStripCheck, 500);
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
                    COM_.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");
                    COM_.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
                    return;
                } // 레일 앞단 센서 && 그리퍼 스트립 그립 확인 센서 CHECK 
            }
        ReTrayBarcode:
            if (!ReadBarcode()){
                //STRIP 제거 할건지 다시 바코드 검사 할건지 CHECK
                COM_.ViewWarning(nThread, W.Rail_StripRemove);
                while (UTIL_.WaitWarning(nThread, W.Rail_StripRemove, "바코드 검사 실패")) ;
                if (ConfirmUser[W.Rail_StripRemove].result){ //스트립 제거
                    while (eRTN.SUCESS != Grip("그리퍼 그립")) ;
                    while (eRTN.SUCESS != MoveX(P.StripOpn, "", "그리퍼 X축 스트립 그립 OPEN 위치")) ;
                    while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
                    while (eRTN.SUCESS != MoveX(P.Ready, "", "그리퍼 X축 대기 위치 이송")) ;
                    //레일 위에 스트립 존재 확인 !
                    while (LAB_.INPUT(I.RAIL_EXIST1) || LAB_.INPUT(I.RAIL_EXIST2)){
                        UTIL_.OnERROR(E.emsRailStrpRemove);
                    }
                    COM_.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");
                    goto StripRemove;
                }
                goto ReTrayBarcode;
            }
            IsLONG[L.StripCnt]++;
            CLOT.InfoStrip[nThread].Index = (int)IsLONG[L.StripCnt];
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                SUBFRM_.gSecsGem.SetPanelLineIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode);
            }
            while (eRTN.SUCESS != Grip("그리퍼 그립")) ;
            while (eRTN.SUCESS != MoveX(P.StripOpn, "", "그리퍼 X축 스트립 그립 OPEN 위치")) ;
            COM_.SetBit(nThread, B.InRailRequest, false, "매거진 스트립 요청");

        StripReCheck:
            while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
            if (!LAB_.INPUT(I.RAIL_EXIST2) && !bDRYRUN){
                COM_.ViewWarning(nThread, W.GripperStripPicFail);
                while (UTIL_.WaitWarning(nThread, W.GripperStripPicFail, "그리퍼 스트립 로딩 중 스트립 사라짐")) ;
                if (ConfirmUser[W.GripperStripPicFail].result) goto StripReCheck; 
                else {
                    COM_.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
                    return;
                }

            }//레일에 스트립 유무 확인

            while (eRTN.SUCESS != MoveStripAlign("스트립 피커 공급 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.Ready, "", "그리퍼 X축 대기 위치 이송")) ;
            InLetTableVac(stBIT.ON);
            while (eRTN.SUCESS != InLET_UP("인-렛 테이블 업")) ;
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.IN_LET);
            }
            if (bMF) return;
            COM_.SetBit(nThread, B.StripPkRequest, true, "레일 위 스트립 공급");
            while (UTIL_.WaitBIT(nThread, B.StripPkRequest, true, "스트립 픽업 해 갈때까지 대기")) ;
        StripRemove:
            COM_.SetBit(nThread, B.GripperWorking, false, "그리퍼 작업 진행");
            LogEnd(nThread, comment + " 완료");
        }

        public bool ReadBarcode() {
            SUBFRM_.cBarcode.ReadResult = "";
            bWriteBarcode = false;
            if (prMACHINE[CP.UseBarcode] == (int)eUSE.NotUSE) {
                CLOT.RESET_STRIP_INFO(nThread);
                return true;
            }

            while (eRTN.SUCESS != MoveBarcodeReading("스트립 바코드 리딩 위치 이송")) ;
            SUBFRM_.cBarcode.Invoke(new MethodInvoker(delegate ()
            {
                SUBFRM_.cBarcode.Trigger();
            }));

            int nCNT = 0;
            do{
                UTIL_.DELAY(1);
                if (bWriteBarcode) goto BarCodeOk;
            } while (nCNT < (int)prMACHINE[CP.BarcodeReadingCheck]);
        BarCodeOk:
            if ((!bWriteBarcode && prMACHINE[CP.UseBarcode] == (int)eUSE.USE) || (SUBFRM_.cBarcode.ReadResult == "" && prMACHINE[CP.UseBarcode] == (int)eUSE.USE)){
                while (eRTN.SUCESS != UnGrip("그리퍼 언그립")) ;
                COM_.ViewWarning(nThread, W.BarcoderReadingFail);
                while (UTIL_.WaitWarning(nThread, W.BarcoderReadingFail, "스트립 바코드 리딩 알람 발생"));
                if (ConfirmUser[W.BarcoderReadingFail].result){
                    return false;
                }
                else{
                    string sValue = UTIL_.INPUT_MESSAGE("BARCODE", "스트립 바코드 수동 입력", "", false);
                    if (sValue == ""){
                        UTIL_.OnERROR(E.BarcoderWriteFail);
                        return false;
                    }
                    CLOT.InfoStrip[nThread].Barcode = sValue;
                    goto Pass;
                }
            }
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
                    UTIL_.OnERROR(E.emsLotIDFail);
                    return false;
                }    
            }
            //스트립 정보 저장
            return true;
        }

        #endregion

        #region>> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.GripperCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "/" + IsDOUBLE[D.GripperCycle].ToString(), "");
            TackStart = Environment.TickCount;
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
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, false);
            LAB_.OUTPUT(O.INLET_TABLE_VAC, bFlog);
            UTIL_.DELAY(nDelay);
        }
        public void InletTableBlow(){
            LAB_.OUTPUT(O.INLET_TABLE_VAC, false);
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, true);
            UTIL_.DELAY((int)prMACHINE[CP.InletBlow]);
            LAB_.OUTPUT(O.INLET_TABLE_BACK_VAC, false);
        }

        public eRTN MoveRail(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (nPos == P.Ready){
                //UTIL_.OnERROR(E.emsInRailStripCheck);
                //return eRTN.FAIL;
            }

            int[] ps = { nPos, nPos };
            double[] tollers = { 0.005, 0.005 };
            bool[] OnlyStarts = { false, false };
            bool[] NoChanges = { false, true };
            bool[] DontStops = { false, false };
            string[] cmds = { cmd, cmd };
            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(M.RAIL, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, M.RAIL, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
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

            int[] ms = { M.GrpX, M.RailF, M.RailR };
            int[] ps = { P.StripLoad, P.StripAlign, P.StripAlign };
            double[] tollers = { 0.005, 0.005, 0.005 };
            bool[] OnlyStarts = { false, false, false };
            bool[] NoChanges = { true, true, true };
            bool[] DontStops = { true, true, true };
            string[] cmds = { "spd=5", "spd=5", "spd=5" };

            if (prMODEL[RP.UseStripLoadngPos] == (int)ePARA.RECIPE) ps[0] = P.RecipStripLoad;

            IsSTRING[S.GrpMessage] = comment + " " + LogWR_.LogPos(ms, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, ms, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.GrpMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion
    }
}