using LIB_.DateType;
using Object;
using System;

namespace NSS_3310S.SEQ.MODULE{
    public class MAGAZINE : BASE{
        readonly int nThread = T.Magazine;
        string cmds = string.Empty;
        long TackStart = 0, TackEnd = 0;
        eRTN eReturn;
        
        bool CheckRunThread(){
            if (eMCStatus != eMachineStatus.AUTO /*|| bDRYRUN*/){
                UTIL_.DELAY(100);
                return false;
            }
            return true;
        }
        public void DoAuto(){
            do{
                if (gExit) break;
                if (!CheckRunThread()) continue;

                GetCassate("매거진 공급");
                StripLoading("매거진 작업 진행");
                OutCassate("매거진 배출");
                Tack();
            } while (true);
        }

        #region >> SEQ
        public eRTN GetCassate(string comment){
            if (CheckLoadingMagazine("매거진 유무 확인")) return eRTN.SUCESS;

            LogStart(nThread, comment + " 시작");
            while (eRTN.SUCESS != UnClamp("매거진 언클램프")) ;
            
            // 로더 콘베어 매거진 감지 센서 확인
            //매거진 없음!!!

            //

            while (eRTN.SUCESS != MoveY(P.Ready, "", comment + " 엘리베이터 Y축 대기 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.Recive, "", comment + " 엘리베이터 Z축 로딩 매거진 위치 이송")) ;
            while (eRTN.SUCESS != MoveY(P.Recive, "", comment + " 엘리베이터 Y축 로딩 매거진 위치 이송")) ;
        ReCheck:
            eReturn = CcwConveyor("로더 컨베어 매거진 투입");
            if (eReturn != eRTN.SUCESS && !bDRYRUN){
                if (eReturn == eRTN.AREA_CHECK || eReturn == eRTN.PUSH_STOP){
                    UTIL_.DELAY(100);
                    goto ReCheck;
                }
                if (bMF) return eRTN.NotLoadingMagazine;
                AddMessage(nThread, comment + " 매거진 없음 !");
                W.ViewWarning(nThread, W.LDCst_Requst);
                bWaitProduct = true;
                while (W.WaitWarning(nThread, W.LDCst_Requst, "매거진 공급 요청")) ;
                B.SetBit(nThread, B.CstRequest, true, "매거진 공급 확인 플로그");
                while (B.WaitBIT(nThread, B.CstRequest, true, "매거진 공급 상태 확인")) ;
                UTIL_.DELAY(1000);
                bWaitProduct = false;
                goto ReCheck;
            }
            B.SetBit(nThread, B.MGZWorking, true, "엘리베이터 매거진 작업 진행");
            while (eRTN.SUCESS != MoveY(P.Recive, "", comment + " 엘리베이터 Y축 로딩 매거진 위치 이송")) ;
            
            if (prMACHINE[CP.UseRFID] == (int)eUSE.USE){

            } //RF ID READING....
#if _NSS3300
            cmds = "offset=-" + string.Format("{0:0.0}", prMODEL[RP.MGZ_CLAMP_UP_PITCH]) + ":spd=5";
#else
            cmds = "offset=" + string.Format("{0:0.0}", prMODEL[RP.MGZ_CLAMP_UP_PITCH]) + ":spd=5";
#endif
            while (eRTN.SUCESS != MoveZ(P.Recive, cmds, comment + " 엘리베이터 Z축 매거진 로딩 픽업 위치 이송"));
            if (!bDRYRUN) while (eRTN.SUCESS != Clamp("매거진 클램프")) ;
#if _NSS3300
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.ElvUpDownPitch]) + ":spd=10";
#else
            cmds = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvUpDownPitch]) + ":spd=10";
#endif
            while (eRTN.SUCESS != MoveZ(P.Recive, cmds, comment + " 엘리베이터 Z축 매거진 로딩 픽업 위치 이송")) ;
            while (eRTN.SUCESS != MoveY(P.FirstSlot, "", comment + " 엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) ;
            while (eRTN.SUCESS != MoveZ(P.FirstSlot, "", comment + " 엘리베이터 Z축 매거진 첫번째 슬롯 위치 이송")) ;
            AddMagazine();
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }

        public void StripLoading(string comment){
            LogStart(nThread, "매거진 스트립 공급 시작");
            do{
                if (!IsBIT[B.InRailRequest] && MAP_.IsExistsLoadSlot((int)prMODEL[RP.MGZSlotCnt])) Process(comment + "스트립 공급");
                if (!IsBIT[B.InRailRequest] && !MAP_.IsExistsLoadSlot((int)prMODEL[RP.MGZSlotCnt])) break;
            } while (true);
            LogEnd(nThread, "매거진 스트립 공급 완료");
        }
        int Process(string comment){
            if (LAB_.INPUT(I.ELV_MZ_EXIST1) || LAB_.INPUT(I.ELV_MZ_EXIST2) || bDRYRUN){
                IsLONG[L.CurSlotCount] = MAP_.GET_LoadSlotNo(); // 작업 슬롯 번호 가져옴
                if (IsLONG[L.CurSlotCount] >= prMODEL[RP.MGZSlotCnt]){
                    AddMessage(nThread, "매거진 스트립 배출 완료 !");
                    return 1;
                }

                AddMessage(nThread, "매거진 " + IsLONG[L.CurSlotCount].ToString("00") + " 슬롯 투입");
                while (eRTN.SUCESS != MoveY(P.FirstSlot, "", comment + "엘리베이터 Y축 매거진 첫번째 슬롯 위치 이송")) ;
                while (eRTN.SUCESS != MoveMGZSlot((int)IsLONG[L.CurSlotCount], "", "엘리베이터 Z축 매거진 슬롯 " + IsLONG[L.CurSlotCount].ToString("00") + " 위치 이송")) ;
                MAP_.SET_CstMapSlotWorking(M.ElvZ, (int)IsLONG[L.CurSlotCount]);
                while (B.WaitBIT(nThread, B.Dry, true, "스트립 공급 일시 정지")) ;
                if (!bDRYRUN){
                    while (!LAB_.INPUT(I.ELV_MZ_EXIST1) || !LAB_.INPUT(I.ELV_MZ_EXIST2)){
                        E.OnERROR(E.emsMagazineDisappear);
                        if (mIN[I.ELV_MZ_UNCLAMP]){
                            MAP_.SET_CstMapAllEmpty(M.ElvZ);
                            return 0;
                        }
                    }
                }
                B.SetBit(nThread, B.InRailRequest, true, "매거진 스트립 공급 위치");
                while (B.WaitBIT(nThread, B.InRailRequest, true, "스트립 인-레일로 공급 완료 때까지 대기")) ;
                MAP_.SET_CstMapSlotEmpty(M.ElvZ, (int)IsLONG[L.CurSlotCount]);
                AddMessage(nThread, "매거진 " + IsLONG[L.CurSlotCount].ToString("00") + " 슬롯 투입 완료");
            }
            else{
                AddMessage(nThread, comment + "CASSETE DISAPPEAR.(카세트 유무 확인 센서 감지 못했음)");
                E.OnERROR(E.emsMagazineDisappear);
                if (mIN[I.ELV_MZ_UNCLAMP]){
                    MAP_.SET_CstMapAllEmpty(M.ElvZ);
                }
            }
            return 0;
        }

        public eRTN OutCassate(string comment){
            if (CheckUnloadingMagazine("매거진 유무 확인")) return eRTN.SUCESS;

            LogStart(nThread, comment + " 시작");
            while (eRTN.SUCESS != PusherBackward("푸셔 후진")) ;
            while (eRTN.SUCESS != MoveY(P.Ready, "", "엘리베이터 Y축 대기 위치 이송")) ;
#if _NSS3300
            cmds = "offset=-" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]);
#else
            cmds = "offset=" + string.Format("{0:0.0}", prMACHINE[CP.ElvULDUpDownPitch]);
#endif
            while (eRTN.SUCESS != MoveZ(P.Give, cmds, comment + " 엘리베이터 Z축 매거진 언로딩 픽업 위치 이송")) ;
            while (!CheckUnloadingConveyor("언로더 콘베어 상태 확인")) ;
            while (eRTN.SUCESS != MoveY(P.Give, "", comment + " 엘리베이터 Y축 매거진 언로딩 위치 이송")) ;
            while (eRTN.SUCESS != UnClamp("매거진 언클램프")) ;
            while (eRTN.SUCESS != MoveZ(P.Give, "", comment + " 엘리베이터 Z축 매거진 언로딩 위치 이송")) ;
            while (eRTN.SUCESS != MoveY(P.Ready, "", comment + " 엘리베이터 Y축 대기 위치 이송")) ;
            LogEnd(nThread, comment + " 완료");
            return eRTN.SUCESS;
        }
#endregion

        #region>> Moudle
        void Tack(){
            TackEnd = Environment.TickCount;
            IsDOUBLE[D.MGZCycle] = (TackEnd - TackStart) / 1000;
            LogWR_.SaveLogTack(sJobName + "," + CLOT.GET_LOT.LotID + ",MGZ," + IsDOUBLE[D.MGZCycle].ToString(), "");
            B.SetBit(nThread, B.MGZWorking, false, "엘리베이터 매거진 작업 진행");
            TackStart = Environment.TickCount;
        }
        void AddMagazine(){
            MAP_.SET_CstMapAllExists(M.ElvZ);
            IsLONG[L.LotCnt]++;
            IsLONG[L.DayMGZCnt]++;
        }

        public bool CheckLoadingMagazine(string comment){
            if ((LAB_.INPUT(I.ELV_MZ_EXIST1) || LAB_.INPUT(I.ELV_MZ_EXIST2)) && !bDRYRUN){
                AddMessage(nThread, comment + " - 엘리베이터 카세트 잡고 있음 !");
                return true;
            }
            return false;
        }
        public bool CheckUnloadingMagazine(string comment){
            if ((!LAB_.INPUT(I.ELV_MZ_EXIST1) && LAB_.INPUT(I.ELV_MZ_EXIST2)) && !bDRYRUN){
                AddMessage(nThread, comment + " - 엘리베이터 카세트 없음 !");
                return true;
            }
            return false;
        }
        public bool CheckUnloadingConveyor(string comment){
        RECHECK_UNLOADING_MGZ:
#if _NSS3300
            if (LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK1) || !LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK2)){
#else
            if (LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK1) || LAB_.INPUT(I.ULD_CONV_MZ_FULL_CHECK2)){
#endif
                AddMessage(nThread, comment + " - 매거진 가득참 !");
                W.ViewWarning(nThread, W.ULDCst_FullCheck);
                bWaitProduct = true;
                while (W.WaitWarning(nThread, W.ULDCst_FullCheck, "매거진 제거 요청")) ;
                B.SetBit(nThread, B.CstRequest, true, "매거진 배출 확인 플로그");
                while (B.WaitBIT(nThread, B.CstRequest, true, "언로더 매거진 배출 상태 확인")) ;
                bWaitProduct = false;
                goto RECHECK_UNLOADING_MGZ;
            }
            return true;
        }

        public void Conveyor(eConv Status){
            if (eConv.FWD == Status){
#if _NSS3300
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CW, true);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_BRAKE, true);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CCW, false);
#else
                LAB_.BIT_OUT(O.LD_CONV_CW, true);
                LAB_.BIT_OUT(O.LD_CONV_CCW, false);
                LAB_.BIT_OUT(O.LD_CONV_STOP, false);
#endif
            }
            else if (eConv.BWD == Status){
#if _NSS3300
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CW, false);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_BRAKE, true);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CCW, true);
#else
                LAB_.BIT_OUT(O.LD_CONV_CW, false);
                LAB_.BIT_OUT(O.LD_CONV_CCW, true);
                LAB_.BIT_OUT(O.LD_CONV_STOP, false);
#endif
            }
            else{
#if _NSS3300
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CW, false);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_CCW, false);
                LAB_.BIT_OUT(O.LD_MGZ_CONVEYOR_BRAKE, false);
#else
                LAB_.BIT_OUT(O.LD_CONV_STOP, true);
#endif
            }
        }
        public eRTN CwConveyor(string comment) { //후진
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (bDRYRUN) {
                Conveyor(eConv.FWD);
                UTIL_.DELAY(3000);
                Conveyor(eConv.STOP);
                return eRTN.SUCESS;
            }
            while (!mIN[I.LD_CONV_AREA_SENSOR]) {
                if (mtCHK[M.StripPkX].Ev == "STOP" || mtCHK[M.StripPkX].Ev == "stop" || bPushStop) {
                    UTIL_.DELAY(100);
                    return eRTN.FAIL;
                }
                UTIL_.DELAY(100);
            }

#if _NSS3300
            int[] OffInpts = { I.LD_CONV_MZ_ARRIVAL_CHECK };
            int[] OnOutputs = { O.LD_MGZ_CONVEYOR_CCW, O.LD_MGZ_CONVEYOR_BRAKE };
            int[] OffOutputs = { O.LD_MGZ_CONVEYOR_CW };
            eRTN RETUTN = WRAP_.RunACMotor(nThread, E.ConverRunCheck, I.Null, OffInpts, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, (int)prMACHINE[CP.MgzArrivalDelay], prMACHINE[CP.ACMotorRunTime], comment);
            if (RETUTN == eRTN.AREA_CHECK) {
                return eRTN.AREA_CHECK;
            }
            else if (RETUTN == eRTN.FAIL) {
                return eRTN.TimeOver;
            }
            else if (RETUTN == eRTN.PUSH_STOP){
                return eRTN.PUSH_STOP;
            }
            //if (eRTN.SUCESS != WRAP_.RunACMotor(nThread, E.ConverRunCheck, I.Null, OffInpts, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR,(int)prMACHINE[CP.MgzArrivalDelay], prMACHINE[CP.ACMotorRunTime], comment)){
            //    return eRTN.TimeOver;
            //}
#else
            int[] OnInpts = { I.LD_CONV_MZ_ARRIVAL_CHECK };
            int[] OnOutputs = { O.LD_CONV_CW };
            int[] OffOutputs = { O.LD_CONV_CCW, O.LD_CONV_STOP };
            eRTN RETUTN = WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInpts, I.Null, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, (int)prMACHINE[CP.MgzArrivalDelay], prMACHINE[CP.ACMotorRunTime], comment);
            if (RETUTN == eRTN.AREA_CHECK){
                return eRTN.AREA_CHECK;
            }
            else if (RETUTN == eRTN.FAIL){
                return eRTN.TimeOver;
            }
            else if (RETUTN == eRTN.PUSH_STOP){
                return eRTN.PUSH_STOP;
            }
            //if (eRTN.SUCESS != WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInpts, I.Null, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, (int)prMACHINE[CP.MgzArrivalDelay], prMACHINE[CP.ACMotorRunTime], comment)){
            //    return eRTN.TimeOver;
            //}
#endif
            return eRTN.SUCESS;
        }
        public eRTN CcwConveyor(string comment){ //로딩 전진함
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (bDRYRUN){
                Conveyor(eConv.BWD);
                UTIL_.DELAY(2000);
                Conveyor(eConv.STOP);
                return eRTN.SUCESS;
            }
            while (!mIN[I.LD_CONV_AREA_SENSOR]){
                if (mtCHK[M.StripPkX].Ev == "STOP" || mtCHK[M.StripPkX].Ev == "stop" || bPushStop){
                    UTIL_.DELAY(100);
                    return eRTN.FAIL;
                }
                UTIL_.DELAY(100);
            }

#if _NSS3300
            int[] OnInputs = { I.ELV_MZ_EXIST1, I.ELV_MZ_EXIST2 };
            int[] OffInpts = { I.LD_CONV_MZ_ARRIVAL_CHECK };
            int[] OnOutputs = { O.LD_MGZ_CONVEYOR_CW, O.LD_MGZ_CONVEYOR_BRAKE };
            int[] OffOutputs = { O.LD_MGZ_CONVEYOR_CCW };
            //if (eRTN.SUCESS != WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInputs, OffInpts, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, 1000, prMACHINE[CP.ACMotorRunTime], comment)) return eRTN.FAIL;
            eRTN RETUTN = WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInputs, OffInpts, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, 1000, prMACHINE[CP.ACMotorRunTime], comment);
            if (RETUTN == eRTN.AREA_CHECK){
                return eRTN.AREA_CHECK;
            }
            else if (RETUTN == eRTN.FAIL){
                return eRTN.TimeOver;
            }
            else if (RETUTN == eRTN.PUSH_STOP){
                return eRTN.PUSH_STOP;
            }
#else
            int[] OnInpts = { I.LD_CONV_MZ_ARRIVAL_CHECK/*, I.ELV_MZ_EXIST1, I.ELV_MZ_EXIST2*/ };
            int[] OnOutputs = { O.LD_CONV_CCW };
            int[] OffOutputs = { O.LD_CONV_CW, O.LD_CONV_STOP };
            //if (eRTN.SUCESS != WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInpts, I.Null, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, 1000, prMACHINE[CP.ACMotorRunTime], comment)) return eRTN.FAIL;
            eRTN RETUTN = WRAP_.RunACMotor(nThread, E.ConverRunCheck, OnInpts, I.Null, OnOutputs, OffOutputs, I.LD_CONV_AREA_SENSOR, 1000, prMACHINE[CP.ACMotorRunTime], comment, false);
            if (RETUTN == eRTN.AREA_CHECK){
                return eRTN.AREA_CHECK;
            }
            else if (RETUTN == eRTN.FAIL){
                return eRTN.TimeOver;
            }
            else if (RETUTN == eRTN.PUSH_STOP){
                return eRTN.PUSH_STOP;
            }
#endif
            return eRTN.SUCESS;
        }

        public eRTN Clamp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
#if _NSS3300
            if (eMCStatus == eMachineStatus.AUTO) Conveyor(eConv.FWD);
#else
            if (eMCStatus == eMachineStatus.AUTO) Conveyor(eConv.BWD);
#endif

            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.MagazineClamp, -1, I.ELV_MZ_UNCLAMP, O.ELV_CLAMP, O.ELV_UNCLAMP, (int)prMACHINE[CP.ElvClampDelay], comment)){
                Conveyor(eConv.STOP);
                return eRTN.FAIL;
            }
            Conveyor(eConv.STOP);
            return eRTN.SUCESS;
        }
        public eRTN UnClamp(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.MagezineUnClamp, I.ELV_MZ_UNCLAMP, I.ELV_MZ_CLAMP, O.ELV_UNCLAMP, O.ELV_CLAMP, (int)prMACHINE[CP.ElvUnClampDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN PusherForward(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            //if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.PusherForward, I.PUSHER_FWD, I.PUSHER_BWD, O.PUSHER_FWD, O.PUSHER_BWD, (int)prMACHINE[CP.PusherFwdDelay], comment)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunPusherFwd(nThread, E.PusherForward, E.PusherOverloadCheck, I.PUSHER_FWD, I.PUSHER_BWD, I.PUSHER_OVERLOAD, O.PUSHER_FWD, O.PUSHER_BWD, (int)prMACHINE[CP.PusherFwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN PusherBackward(string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (eRTN.SUCESS != WRAP_.RunCylinder(nThread, E.PusherBackward, I.PUSHER_BWD, I.PUSHER_FWD, O.PUSHER_BWD, O.PUSHER_FWD, (int)prMACHINE[CP.PusherBwdDelay], comment)) return eRTN.FAIL;
            return eRTN.SUCESS;
        }

        public eRTN MoveY(int nPos, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsPusherNotBwd, true)) return eRTN.EMS;

            IsSTRING[S.MgzMessage] = comment + " " + LogWR_.LogPos(M.ElvY, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.ElvY, nPos, 0.005, false, false, false, cmd, IsSTRING[S.MgzMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveZ(int nPos, string cmd, string comment){
            if (!bBD) {
                while (!mIN[I.LD_CONV_AREA_SENSOR]) {
                    if (mtCHK[M.StripPkX].Ev == "STOP" || mtCHK[M.StripPkX].Ev == "stop" || bPushStop) {
                        UTIL_.DELAY(100);
                        return eRTN.FAIL;
                    }
                    UTIL_.DELAY(100);
                }
            }

            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (!C.Interlock.ChkInterlock(E.emsPusherNotBwd, true)) return eRTN.EMS;

            IsSTRING[S.MgzMessage] = comment + " " + LogWR_.LogPos(M.ElvZ, nPos);
            if (eRTN.SUCESS != WRAP_.MOVE(nThread, M.ElvZ, nPos, 0.005, false, false, false, cmd, IsSTRING[S.MgzMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        public eRTN MoveMGZSlot(int nSlot, string cmd, string comment){
            if (ChkRunning(nThread)) return eRTN.FAIL;
            if (nSlot < 0 || nSlot > prMODEL[RP.MGZSlotCnt]){
                E.OnERROR(E.emsMGZSlotCount);
                return eRTN.FAIL;
            }
            if (!bBD) {
                if (!mIN[I.RAIL_MOUTH]) {
                    E.OnERROR(E.emsMagazineSlotNotMove);
                    return eRTN.FAIL;
                }
            }

            M.SetMoveInfoRaw(M.ElvZ, P.FirstSlot, P.CAL_);
            double dPitch = prMODEL[RP.MGZSlotPitch] * nSlot;
            double dEndPitch = prMODEL[RP.MGZSlotPitch] * (prMODEL[RP.MGZSlotCnt] - 1);
#if _NSS3300
            if (prMODEL[RP.MGZ_DIR] == 0){
                mtDATA[M.ElvZ, P.CAL_].Pos -= dPitch; //기존
            }
            else{
                mtDATA[M.ElvZ, P.CAL_].Pos -= dEndPitch;
                mtDATA[M.ElvZ, P.CAL_].Pos += dPitch;
            }
#else
            if (prMODEL[RP.MGZ_DIR] == 0){
                mtDATA[M.ElvZ, P.CAL_].Pos += dPitch; //기존
            }
            else{
                mtDATA[M.ElvZ, P.CAL_].Pos += dEndPitch;
                mtDATA[M.ElvZ, P.CAL_].Pos -= dPitch;
            }
#endif

            double dPitchSpd = prMACHINE[CP.MGZPitchSpeed];
            if (dPitchSpd <= 0) dPitchSpd = 50;
            if ((prMODEL[RP.MGZSlotPitch] + 1) > Math.Abs(mtDATA[M.ElvZ, P.CAL_].Pos - mtSTS[M.ElvZ].CurrentPosition)){
                mtDATA[M.ElvZ, P.CAL_].Spd = dPitchSpd;
                mtDATA[M.ElvZ, P.CAL_].Acc = dPitchSpd * 10;
                mtDATA[M.ElvZ, P.CAL_].Dec = dPitchSpd * 10;
            }

            int[] ms            = { M.ElvY, M.ElvZ };
            int[] ps            = { P.FirstSlot, P.CAL_ };
            double[] tollers    = { 0.005, 0.005 };
            bool[] OnlyStarts   = { false, false };
            bool[] NoChanges    = { false, true };
            bool[] DontStops    = { false, false };
            string[] cmds       = { "", "" };
            IsSTRING[S.MgzMessage] = comment + " " + LogWR_.LogPos(ms, ps);
            if (eRTN.SUCESS != WRAP_.MUTI_MOVE(nThread, ms, ps, tollers, OnlyStarts, NoChanges, DontStops, cmds, IsSTRING[S.MgzMessage])) return eRTN.FAIL;
            return eRTN.SUCESS;
        }
        #endregion
    }
}