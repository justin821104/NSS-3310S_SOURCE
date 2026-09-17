using LIB_.DateType;
using Object;
using System;

namespace NSS_3310S.SEQ.MODULE{
    public class STRIP_PICKER : BASE{
        readonly int nThread = T.StripPk;
        string cmds = string.Empty;
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

            RePic:
                while (B.WaitBIT(nThread, B.StripPkRequest, false, "레일에 스트립 공급 할때까지 대기")){
                    if (mIN[I.STRIP_PK_VAC]){
                        B.SetBit(nThread, B.StripPkMask, true, "스트립 피커 스트립 유무");
                        goto Shortcut_Place;
                    }
                    if (bBD) {
                        UTIL_.DELAY(1000);
                        IsBIT[B.StripPkRequest] = true;
                    } // 시물레이션 중이면 그냥 true  
                }
                if (!Pic("스트립 픽업")) goto RePic;
                if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                    if (!CLOT.InfoStrip[nThread].Overlap)
                        SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.STRIP_PK);
                }
            Shortcut_Place:
                Plc("다이싱 테이블로 스트립 공급");
                B.SetBit(nThread, B.StripPkMask, false, "스트립 피커 스트립 유무");
            } while (true);
        }

        #region >> SEQ
        public void PreAling(){
            ResetAlignValue();
            if (prMACHINE[CP.UsePreAlign] == (int)eUSE.NotUSE){
                TEACH_.WRTIE_PRE_ALIGN(IsDOUBLE[D.PreAlignX], IsDOUBLE[D.PreAlignY], IsDOUBLE[D.PreAlignT]); //프리얼라인 옵셋값 적용
                return;
            }
            while (B.WaitBIT(nThread, B.UnitPickUp, true, "유닛 픽업 완료 할때 까지 대기")) ;
            B.SetBit(nThread, B.PreAligning, true, "스트립 프리-얼라인 진행");

            LogStart(nThread, "프리-얼라인 진행");
            while (eRTN.SUCESS != MoveZ(P.FirstTrigger, "", "스트립 피커 Z축 첫번째 얼라인 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.FirstTrigger, "", "스트립 피커 X축 첫번째 얼라인 위치 이송")) ;
            //프리 얼라인 매칭 

            while (eRTN.SUCESS != MoveZ(P.SecondTrigger, "", "스트립 피커 Z축 두번째 얼라인 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.SecondTrigger, "", "스트립 피커 X축 두번째 얼라인 위치 이송")) ;
            //프리 얼라인 매칭

            TEACH_.WRTIE_PRE_ALIGN(IsDOUBLE[D.PreAlignX], IsDOUBLE[D.PreAlignY], IsDOUBLE[D.PreAlignT]); //프리얼라인 옵셋값 적용
            LogEnd(nThread, "프리-얼라인 완료");
            B.SetBit(nThread, B.PreAligning, false, "스트립 프리-얼라인 완료");
        }

        public bool Pic(string comment){
            if (mIN[I.STRIP_PK_VAC]) return true;
            PreAling();
            LogStart(nThread, comment);
        RePIC:
            cmds = "offset=" + string.Format("{0:0.0}", IsDOUBLE[D.PreAlignX]);
            while (eRTN.SUCESS != MoveX(P.StripPckUp, cmds, "스트립 피커 X축 스트립 픽업 위치 이송")) ;
        
        RePkrChack:
            if (prMACHINE[CP.UseStipPkCheck] == (int)eUSE.USE){
                Vac(stBIT.ON);
                if (mIN[I.STRIP_PK_VAC]){
                    E.OnERROR(E.emsNotStripPkVac, 500);
                    goto RePkrChack;
                }
                Vac(stBIT.OFF);
            }

            while (eRTN.SUCESS != MoveZ(P.StripPckUp, "offset=-10", "스트립 피커 Z축 스트립 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.StripPckUp, "spd=10", "스트립 피커 Z축 스트립 픽업 위치 이송")) ;
            Vac(stBIT.ON);
            C.Gripper.InletTableBlow();
            while (eRTN.SUCESS != C.Gripper.MoveRail(P.Open, "", "레일 OPEN 위치 이송")) ;
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.StipPkCheckUpPitch]) + ":spd=10";
            while (eRTN.SUCESS != MoveZ(P.StripPckUp, cmds, "스트립 피커 Z축 스트립 픽업 위치 이송")) ;
            if (!mIN[I.STRIP_PK_VAC] && !bDRYRUN){
                Vac(stBIT.OFF);
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                W.ViewWarning(nThread, W.StripPk_RePic);
                while (W.WaitWarning(nThread, W.StripPk_RePic, "[WARNNING] 스트립 피커 픽업 실패")) ;
                if (ConfirmUser[W.StripPk_RePic].result) goto RePIC;
                while (LAB_.INPUT(I.RAIL_EXIST2)){
                    E.OnERROR(E.emsRemoveRailStrip);
                }
                while (eRTN.SUCESS != MoveX(P.StripPckUp, cmds, "스트립 피커 X축 스트립 픽업 위치 이송")) ;
                B.SetBit(nThread, B.StripPkRequest, false, "스트립 픽업 실패");
                return false;
            }
            while (eRTN.SUCESS != C.Gripper.InLET_DOWN("인-렛 테이블 다운")) ;
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
            if (!mIN[I.STRIP_PK_VAC] != bDRYRUN){
                Vac(stBIT.OFF); 
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                W.ViewWarning(nThread, W.StripPk_RePic);
                while (W.WaitWarning(nThread, W.StripPk_RePic, "[WARNNING] 스트립 피커 픽업 실패")) ;
                if (ConfirmUser[W.StripPk_RePic].result) goto RePIC;
                while (LAB_.INPUT(I.RAIL_EXIST2)){
                    E.OnERROR(E.emsRemoveRailStrip);
                }
                while (eRTN.SUCESS != MoveX(P.StripPckUp, cmds, "스트립 피커 X축 스트립 픽업 위치 이송")) ;
                B.SetBit(nThread, B.StripPkRequest, false, "스트립 픽업 실패");
                return false;
            }
            B.SetBit(nThread, B.StripPkMask, true, "스트립 피커 스트립 유무");
            //스트립 정보 저장
            //CLOT.InfoStrip[nThread] = CLOT.InfoStrip[T.Gripper];
            CLOT.SEND_STRIP_INFO(eSeqBacode.Gripper, T.Gripper, eSeqBacode.StipPk, nThread);
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                if (!CLOT.InfoStrip[T.Gripper].Overlap)
                    SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.InfoStrip[T.Gripper].Index, CLOT.InfoStrip[T.Gripper].Barcode, (int)CMES.ModuleID.IN_LET);
            }
            B.SetBit(nThread, B.StripPkRequest, false, "스트립 픽업 완료");
            LogEnd(nThread, comment + " 완료");
            return true;
        }

        public bool Plc(string comment){
            //if (!bDRYRUN)
            while (I.WaitInput(nThread, I.SAW_LD_REQ, false, "다이싱에서 스트립 공급 요청할때까지 대기")) { 
                if (bBD) {
                    if (!IsBIT[B.Simulation_Sawing]) {
                        break;
                    }
                }
            }
            LogStart(nThread, comment);
            C.SendSaw.SEND("GET_SVID,*");
            //if (bDRYRUN){
            //    while (eRTN.SUCESS != MoveX(P.StripPlc, "", "스트립 피커 X축 다이싱 내려놓는 위치 이송")) ;
            //    goto DrayRun;
            //}
            while (B.WaitBIT(nThread, B.StripPlacStop, B.UnitPkPic, true, true, "다싱에 스트립 공급 진행 대기", stBIT.OR)) ;
            B.SetBit(nThread, B.StripPkPlc, true, "스트립 피커 다이싱 테이블에 스트립 내려놓는 동작 진행");
        ReCheck:
            while (eRTN.SUCESS != MoveX(P.StripPlc, "", "스트립 피커 X축 다이싱 내려놓는 위치 이송")) ;
            if (!bBD) {
                while (I.WaitInput(nThread, I.SAW_LD_POS, false, "다이싱 테이블 스트립 로딩 위치에 있는지 확인")) {
                    if (!mIN[I.SAW_LD_REQ] /*|| (!IsBIT[B.StripPkRequest] && bMF)*/) {
                        W.ViewWarning(nThread, W.SawStripReuestsingal);
                        while (W.WaitWarning(nThread, W.SawStripReuestsingal, "다이싱 스트립 요청 신호 끊어짐")) ;
                        while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                        while (eRTN.SUCESS != MoveX(P.StripPckUp, "", "스트립 피커 X축 스트립 픽업 위치 이송")) ;
                        while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                        O.SetOutput(nThread, O.HANDLER_LD_COMPLETE, true, "다이싱 테이블에 스트립 전달 완료");
                        UTIL_.DELAY(100);
                        ResetInterface();
                        return false;
                    }
                    if (!mOUT[O.HANDLER_STRIP_PK_X_PLACE_POS])
                    {
                        while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                        goto ReCheck;
                    }
                }
            }
            if (!mIN[I.STRIP_PK_VAC] && !bDRYRUN) {
                W.ViewWarning(nThread, W.StripPk_Vanish);
                while (W.WaitWarning(nThread, W.StripPk_Vanish, "스트립 피커에 있던 스트립 사라짐")) ;
                if (ConfirmUser[W.StripPk_Vanish].result) goto ReCheck;
                while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
                while (eRTN.SUCESS != MoveX(P.StripPckUp, "", "스트립 피커 X축 스트립 픽업 위치 이송")) ;
                ResetInterface();
                Vac(stBIT.OFF);
                return false;
            }

            while (eRTN.SUCESS != MoveZ(P.StripPlc, "offset=-10", "스트립 피커 Z축 스트립 픽업 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.StripPlc, "spd=10", "스트립 피커 Z축 스트립 픽업 위치 이송")) ;
            if (!bBD) {
                while (I.WaitInput(nThread, I.SAW_STAGE_VAC_ON, false, "다이싱 테이블 진공 완료 할떄까지 대기")) ;
            }
            Blow();
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.StipPkCheckUpPitch]) + ":spd=5";
            while (eRTN.SUCESS != MoveZ(P.StripPlc, cmds, "스트립 피커 Z축 스트립 픽업 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveX(P.StripPckUp, "", "스트립 피커 X축 스트립 픽업 위치 이송")) ;
            ChekPlc();
            //스트립 정보 저장
            CLOT.RECEIVE_SAW_STRIP_INFO(eSeqBacode.StipPk, nThread);
            if (prMACHINE[CP.UseMES] == (int)eUSE.USE){
                if (!CLOT.InfoStrip[nThread].Overlap) { 
                    SUBFRM_.gSecsGem.SetPanelModuleOut(CLOT.InfoStrip[nThread].Index, CLOT.InfoStrip[nThread].Barcode, CMES.ModuleID.STRIP_PK);
                    SUBFRM_.gSecsGem.SetPanelModuleIn(CLOT.SawStageStripIndex, CLOT.SawStageStripBarcode, CMES.ModuleID.SAW_STAGE);
                }
            }
            if (bBD) {
                IsBIT[B.Simulation_Sawing] = true;
            }
            LogWR_.SaveBladeInfo(CLOT.SawStageStripBarcode, "In", CLOT.SawStageStripIndex);
            Tack();
            LogEnd(nThread, comment + " 완료");
            return true;
        }
        #endregion

        #region >> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.StripPkCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",스트립 피커," + IsDOUBLE[D.StripPkCycle].ToString(), "");
            TackStart = Environment.TickCount;
            IsDOUBLE[D.StripPlcTime] = TackStart;
        }

        public void ResetAlignValue(){
            IsDOUBLE[D.PreAlignX] = 0;
            IsDOUBLE[D.PreAlignY] = 0;
            IsDOUBLE[D.PreAlignT] = 0;
        }
        void ChekPlc(){
            while (eRTN.SUCESS != MoveZ(P.Ready, "", "스트립 피커 Z축 대기 위치 이송")) ;
            O.SetOutput(nThread, O.HANDLER_LD_COMPLETE, true, "다이싱 테이블에 스트립 전달 완료");
            while (mIN[I.SAW_LD_REQ] || mIN[I.SAW_LD_POS] || mIN[I.SAW_STAGE_VAC_ON]) UTIL_.DELAY(10);
            ResetInterface();
        }
        public void ResetInterface(){
            mOUT[O.HANDLER_LD_COMPLETE] = false;
            B.SetBit(nThread, B.StripPkPlc, false, "스트립 피커 다이싱 테이블에 스트립 내려놓는 동작 진행 플로그 OFF");
        }

        public void Vac(bool bFlog){
            int nDelay = bFlog ? (int)prMACHINE[CP.StripPkVacOn] : (int)prMACHINE[CP.StripPkVacOff];
            LAB_.OUTPUT(O.STRIP_PK_BLOW, false);
            LAB_.OUTPUT(O.STRIP_PK_VAC, bFlog);
#if _NSS3300
#else
            LAB_.OUTPUT(O.STRIP_PK_PURGE, bFlog);
            LAB_.OUTPUT(O.STRIP_PK_VAC_OFF, !bFlog);
#endif
            UTIL_.DELAY(nDelay);
        }
        public void Blow(){
            LAB_.OUTPUT(O.STRIP_PK_VAC, false);
#if _NSS3300
#else
            LAB_.OUTPUT(O.STRIP_PK_VAC_OFF, false);
            LAB_.OUTPUT(O.STRIP_PK_PURGE, false);
#endif
            LAB_.OUTPUT(O.STRIP_PK_BLOW, true);
            UTIL_.DELAY((int)prMACHINE[CP.StripBlowOn]);
            LAB_.OUTPUT(O.STRIP_PK_BLOW, false);
        }

        public eRTN MoveX(int nPos, string cmd, string comment){
            while (!mIN[I.DOOR_SAW_FRONT_RIGHT] || !mIN[I.DOOR_SAW_FRONT_LEFT]){
                if (mtCHK[M.StripPkX].Ev == "STOP" || mtCHK[M.StripPkX].Ev == "stop" || bPushStop){
                    UTIL_.DELAY(100);
                    return eRTN.FAIL;
                }
                UTIL_.DELAY(100);
                if (bBD) break;
            }
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsStripPkZNotReadyPos, true)) return eRTN.EMS;

            if (nPos == P.StripPlc || nPos == P.SecondTrigger){
                if (!C.Interlock.ChkInterlock(E.emsUnitPkXSafetyPosition, true)) return eRTN.EMS;
            }

            IsSTRING[S.StripPkMessage] = comment + " " + LogWR_.LogPos(M.StripPkX, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.StripPkX, nPos, 0.005, false, false, false, cmd, IsSTRING[S.StripPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveZ(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;

            if (nPos == P.StripPckUp){
                if (!C.Interlock.ChkInterlock(E.emsNotStripPkXPicPos, true)) return eRTN.EMS;
                if (!C.Interlock.ChkInterlock(E.emsGripperNotRdyPos, true)) return eRTN.EMS;
            }
            if (nPos == P.StripPlc)
                if (!C.Interlock.ChkInterlock(E.emsNotStripPkXPlcPos, true)) return eRTN.EMS;

            IsSTRING[S.StripPkMessage] = comment + " " + LogWR_.LogPos(M.StripPkZ, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.StripPkZ, nPos, 0.005, false, false, false, cmd, IsSTRING[S.StripPkMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN MovePreAlignY(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
#else
            IsSTRING[S.StripPkMessage] = comment + " " + LogWR_.LogPos(M.PreAlign, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.PreAlign, nPos, 0.005, false, false, false, cmd, IsSTRING[S.StripPkMessage])) return eRTN.FAIL;
#endif
            return eRTN.SUCESS;
        }
#endregion
    }
}