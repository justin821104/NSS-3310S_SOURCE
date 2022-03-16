using Object;
using System;
using System.IO;
using System.Windows.Forms;

public class LAB_ : DATA_
{
    public static int nAICh         = 0;
    public static int nAIBoardNo    = 0;
    public static int nAIModulePos  = 0;
    public static uint nAIModuleID  = 0;
    public static uint iAIStatus    = 0;
    public static int iAIMoudleCnt  = 0;

    #region "모션 초기화"
    public static bool MOTION_INIALIZE(){
        if (!BOARD_INIALIZE()) return false;
        INI_BUFFER();
        bMOT = LD_MotFile();

        SET_TriggerModule(CNT_.TriggerCnt);
        return true;
    }
    public static void INI_BUFFER() { CLEAR_MOVEDATA(); }
    public static void CLEAR_MOVEDATA(){
        for (int i = 0; i < CNT_.MT; i++){
            mtOPTION[i].DontStop        = false;   //' 모타 구동옵션 초기화
            mtOPTION[i].SpeedNoChange   = false;

            mtSTS[i].strHome            = "";
            mtSTS[i].strMove            = "";
            mtSTS[i].strStop            = "";
            mtSTS[i].strOther           = "";

            mtCHK[i].axis               = i;
            mtCHK[i].spd                = 1;
            mtCHK[i].acc                = 1000;
            mtCHK[i].dcc                = 1000;
            mtCHK[i].time               = 10000;
            mtCHK[i].sLog               = string.Empty;
            mtCHK[i].errLog             = string.Empty;
            mtCHK[i].fLog               = string.Empty;
            mtCHK[i].sTime              = Environment.TickCount;
            mtCHK[i].eTime              = Environment.TickCount;
            mtCHK[i].timeStop           = Environment.TickCount;
            mtCHK[i].coment             = string.Empty;
            mtCHK[i].OnBusy             = false;
            mtCHK[i].posStop            = -1;
            mtCHK[i].cmd                = string.Empty;
            mtCHK[i].rslt               = string.Empty;
        }
    }
    public static bool BOARD_INIALIZE(){
        try{
            bBD = false;
            uint uRtn = CAXL.AxlOpen(7);
            //if (CAXL.AxlOpen(7) == (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS) { }
            //else bBD = true;
            if (uRtn == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS) { }
            else bBD = true;
            //if (CAXL.AxlIsOpened() == 1) { bBD = false; }
            // bBD = true;
            int iRtn = CAXL.AxlIsOpened();
            if (1 == iRtn) bBD = false;//초기화 되어 있음!
            return !bBD;
        }
        catch (Exception ex){
            bBD = true;
            LogWR_.SaveLogException("cAZIN -> BoardInialize", ex);
            return !bBD;
        }
    }
    public static bool LD_MotFile(){
        if (!File.Exists(PATH_.MotFILE)){
            //CAXM.AxmMotSaveParaAll(pathMOTFILE);
            MessageBox.Show("Not found motion parameter file !");
            bBD = true;
            return false;
        }
        uint iRtn = CAXM.AxmMotLoadParaAll(PATH_.MotFILE);
        if (iRtn != 0) return false;
        return true;
    }
    public static bool SET_MTRingCount(int[] mt, int delay){
        bool bStatus = true;
        for (int m = 0; m < CNT_.MT; m++){
            for (int iChk = 0; iChk < mt.Length; iChk++){
                if (m != mt[iChk]) continue;
                UTIL_.DELAY(100);
                CAXM.AxmStatusSetActPos(m, 0);
                CAXM.AxmStatusSetCmdPos(m, 0);
                UTIL_.DELAY(delay);
                //m축에 위치정보 표시 범위를 0~360으로 설정.
                uint nRtn = CAXM.AxmStatusSetPosType(m, 1, 360, 0);
                if (nRtn != 0) bStatus = false;
            }
        }
        return bStatus;
    }
    public static bool SET_AnlogInput(){
        //Analog Input (SIO-AI4RB) 4CH
        if (CAXA.AxaInfoGetModuleCount(ref nAICh) == (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
            for (int i = 0; i < nAICh; i++){
                //사용자가 원하는 시점에 AD컨버트 하는 모드로 설정
                if (CAXA.AxaInfoGetModule(i, ref nAIBoardNo, ref nAIModulePos, ref nAIModuleID) == (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
                    CAXA.AxaiSetTriggerMode(0, 1);
                    CAXA.AxaiSetRange(i, 0.0, 5.0);
                }
            }
            return true;
        }
        else{
            MessageBox.Show("Analog 입력모듈을 찾을 수 없습니다.");
            return false;
        }
    }
    public static bool SET_AnlogOutput(){
        if (CAXA.AxaInfoIsAIOModule(ref iAIStatus) == (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
            if (CAXA.AxaInfoGetModuleCount(ref iAIMoudleCnt) == (int)AXT_FUNC_RESULT.AXT_RT_SUCCESS){
                for (int i = 0; i < 4; i++) { 
                    CAXA.AxaoSetRange(i, 0.0, 10.0); 
                }
                return true;
            }
            else{
                //아날로그 채널 못 찾음.
                return false;
            }
        }
        else{
            //아날로그보드 없음.
            return false;
        }
    }

    //N3MLIII-CNT2 모듈
    public static bool SET_TriggerModule(int mCnt){
        for (int i = 0; i < mCnt; i++){
            CAXC.AxcMotSetMoveUnitPerPulse(i, 0.001);
            CAXC.AxcSignalSetEncReverse(i, 1);
            CAXC.AxcSignalSetEncInputMethod(i, 3);
            CAXC.AxcTriggerSetLevel(i, 1);
            CAXC.AxcTriggerSetTime(i, 1000);
            CAXC.AxcStatusSetActPos(i, 0);
            CAXC.AxcTriggerSetEnable(i, 0);
        }
        return true;
    }
    #endregion "모션 초기화"

    #region "IO"
    //READ
    public static void READ_INPUT(){
        for (int i = 0; i < InputModule.Length; i++){
            //if (i >= 23){
            //    UTIL_.DELAY(100);
            //}
            uint iVAL = 0;
            int iMODULE = InputModule[i];
            int iOFFSET = InputOffset[i];
            //GET_INPUT_WORD(iMODULE, iOFFSET, ref iVAL);
            CAXD.AxdiReadInportWord(iMODULE, iOFFSET, ref iVAL);
            for (int j = 0; j < 16; j++){
                int iNUM = (16 * i) + j;
                if ((iVAL & 1) == 1)    mIN[iNUM] = true;
                else                    mIN[iNUM] = false;
                iVAL >>= 1;
            }
        }
    }

    public static bool Chk_AOMoudle(int nMod){
        if (aoModNum != null){
            for (int a = 0; a < aoModNum.Length; a++){
                if (OutputModule[nMod] == aoModNum[a]) return false;
            }
        }
        return true;
    }
    public static void WRITE_OUTPUT(){
        for (int i = 0; i < OutputModule.Length; i++){
            if (!Chk_AOMoudle(i)) continue; //진공 모듈 PASS 위한 함수
            for (int j = 0; j < 16; j++){
                int iNUM = (16 * i) + j;
                int iBIT = (16 * OutputOffset[i]) + j;
                if (mOUT[iNUM]){
                    CAXD.AxdoWriteOutportBit(OutputModule[i], iBIT, 1); //SET_OUTPUT_BIT(OutputModule[i], iBIT, 1);
                }
                else{
                    if (iNUM == 235){
                        UTIL_.DELAY(1);
                    }
                    CAXD.AxdoWriteOutportBit(OutputModule[i], iBIT, 0); //SET_OUTPUT_BIT(OutputModule[i], iBIT, 0);
                }
                mOLD_OUT[iNUM] = mOUT[iNUM];
            }
        }
    }

    //INPUT
    public static uint GET_INPUT_WORD(int iMODULE, int iOFFSET, ref uint uVAL){
        return CAXD.AxdiReadInportWord(iMODULE, iOFFSET, ref uVAL);
    }
    public static int GET_INT(double srcVal, bool RoundDown){
        if (!RoundDown) return (int)srcVal;
        string[] sNum = srcVal.ToString().Split('.');
        return int.Parse(sNum[0]);
    }
    public static bool INPUT(short i){
        if (chkIN[i].ContactB) return !mIN[i];
        else return mIN[i];
    }
    public static bool AINON(short i){
        if (chkIN[i].ContactB) return !mIN[i];
        else return mIN[i];
    }
    public static bool AINOFF(short i){
        if (chkIN[i].ContactB) return mIN[i];
        else return !mIN[i];
    }
    public static bool GET_INPUT(int i, eRTN eCHK){
        //int Mod = i > InputNum[0] ? 1 : 0;
        int Mod = 0; //i > InputNum[0] ? 1 : 0;
        for (int m = 0; m < InputModule.Length; m++){
            if (InputNum[m] >= i){
                Mod = m;
                break;
            }
        }

        int Div16 = GET_INT((double)i / 16, true);
        int Mod16 = i % 16;
        if (Mod >= inModNum.Length || inModNum == null){
            eCHK = eRTN.NOT_MODULE;
            return false;
        }

        int nModule = inModNum[Mod];
        int nOffset = (InputOffset[Div16] * 16) + Mod16;
        uint uValue = 0;
        if (0 != CAXD.AxdiReadInportBit(nModule, nOffset, ref uValue)){
            UTIL_.DELAY(3);
            eCHK = eRTN.ERR_IN_MODULE;
            return false;
        }
        mIN[i] = uValue == 1 ? true : false;
        eCHK = eRTN.SUCESS;
        if (chkIN[i].ContactB) return   !mIN[i];
        else return                     mIN[i];
    }

    //OUTPUT
    public static uint SET_OUTPUT_BIT(int nMODULE, int nBIT, uint uVAL){
        return CAXD.AxdoWriteOutportBit(nMODULE, nBIT, uVAL);
    }
    public static void OUTPUT(short i, bool b)          { mOUT[i] = b; }
    public static void OUTPUT(short i, short j, bool b) { mOUT[i] = b; mOUT[j] = b; }
    public static void OUTPUT(short[] i, bool b){
        for (int n = 0; n < i.Length; n++) { 
            mOUT[i[n]] = b; 
        }
    }
    public static bool OUTON(short i)                   { return mOUT[i]; }
    public static bool OUTOFF(short i)                  { return !mOUT[i]; }
    public static void PULSE_ON(int module, int port, int msec){
        if (!bBD) CAXD.AxdoOutPulseOn(module, port, msec);
    }

    public static eRTN PULSE_ON(short i, int msec){
        if (bBD){
            OUTPUT(i, true);
            UTIL_.DELAY(msec);
            OUTPUT(i, false);
            return eRTN.SUCESS;
        }
        int Mod = 0; //i > OutputNum[0] ? 1 : 0;
        for (int m = 0; m < outModNum.Length; m++){
            if (OutputNum[m] > i){
                Mod = m;
                break;
            }
        }
        int Div16 = GET_INT((double)i / 16, true);
        int Mod16 = i % 16;
        if (Mod >= outModNum.Length || outModNum == null) return eRTN.NOT_MODULE;

        int nModule = outModNum[Mod];
        int nOffset = (OutputOffset[Div16] * 16) + Mod16;
        uint uRTN = CAXD.AxdoOutPulseOn(nModule, nOffset, msec);
        if (uRTN != 0){
            UTIL_.DELAY(3);
            return eRTN.ERR_OUT_MODULE;
        }
        //OUTPUT(i, true);
        return eRTN.SUCESS;
    }
    public static void OUTON_(short i){
        OUTPUT(i, true);
        if (!bBD) CAXD.AxdoWriteOutport(i, 1);
        OUTPUT(i, true);
    }
    public static void OUTOFF_(short i){
        OUTPUT(i, false);
        if (!bBD) CAXD.AxdoWriteOutport(i, 0);
        OUTPUT(i, false);
    }
    public static void OUTONOFF_(short i, short j){
        OUTON_(i);
        OUTOFF_(j);
    }
    public static void DOUBLE_OUT(short i, short j, bool b){
        OUTPUT(i, b);
        OUTPUT(j, b);
        if (!bBD){
            CAXD.AxdoWriteOutport(i, b ? 1 : (uint)0);
            CAXD.AxdoWriteOutport(j, b ? 1 : (uint)0);
        }
    }
    public static void ARR_OUT(short i, short j, bool b){
        for (short k = i; k < j; k++){
            OUTPUT(k, b);
            if (!bBD) CAXD.AxdoWriteOutport(k, b ? 1 : (uint)0);
        }
    }

    public static eRTN BIT_OUT(short i, bool b){
        if (bBD){
            OUTPUT(i, b);
            return eRTN.SUCESS;
        }
        int Mod = 0; //i > OutputNum[0] ? 1 : 0;
        for (int m = 0; m < outModNum.Length; m++){
            if (OutputNum[m] > i){
                Mod = m;
                break;
            }
        }
        int Div16 = GET_INT((double)i / 16, true);
        int Mod16 = i % 16;
        if (Mod >= outModNum.Length || outModNum == null) return eRTN.NOT_MODULE;

        int nModule = outModNum[Mod];
        int nOffset = (OutputOffset[Div16] * 16) + Mod16;
        uint uValue = 0;
        if (b) uValue = 1;

        uint uRTN = CAXD.AxdoWriteOutportBit(nModule, nOffset, uValue);
        if (uRTN != 0){
            UTIL_.DELAY(3);
            return eRTN.ERR_OUT_MODULE;
        }
        OUTPUT(i, b);
        return eRTN.SUCESS;
    }
    public static void SOL2(short on, short off){
        OUTON_(on);
        OUTOFF_(off);
    }
    public static void SOL4(short on, short on1, short off, short off1){
        SOL2(on, off);
        SOL2(on1, off1);
    }

    //ANALOG
    public static double GET_AI_VOLT(int iCH){
        double dVOLlt = 0;
        CAXA.AxaiSwReadVoltage(iCH, ref dVOLlt);
        return dVOLlt;
    }
    public static void SET_AO_VOLT(int iCH, double dVOLT){
        string sSetVOLT = (dVOLT * (0.416)).ToString("0.000");
        double dSetVOLT = double.Parse(sSetVOLT); //24V = 10V
        if (dSetVOLT > 10) dSetVOLT = 10;
        CAXA.AxaoWriteVoltage(iCH, dSetVOLT);
    }
    public static double GET_AO_VOLT(int iCH){
        double dVOLT = 0;
        CAXA.AxaoReadVoltage(iCH, ref dVOLT);
        return dVOLT;
    }
    #endregion "IO"

    #region "CounterAgent"
    public static double GET_COUNTER_ACTUAL(int m){
        double dCURPOS = 0;
        CAXC.AxcStatusGetActPos(m, ref dCURPOS);
        return double.Parse(dCURPOS.ToString("0.000"));
    }
    public static void READ_COUNTER(int m){
        double mCurPos = 0;
        mCurPos = GET_COUNTER_ACTUAL(m);
        cntSTS[m].CurrentPosition = mCurPos;
    }

    public static void ONE_SHOT(int i){
        //CAXM.AxmTriggerSetReset(i);
        //CAXM.AxmTriggerSetTimeLevel(i, 100, 1, 1, 0);
        //CAXM.AxmTriggerOneShot(i);

        //CAXC.AxcTriggerSetEnable(i, 1);
        CAXC.AxcTriggerSetOutput(i, 1);
    }
    public static void TriggerOutput(int nCh, uVAL eVal){
        CAXC.AxcTriggerSetEnable(nCh, (uint)eVal);
    }
    #endregion "CounterAgent"

    #region "MOTION"
    public static void READ_STATUS(int m){
        //uint upStopMode = 0, upPositiveLevel = 0, upNegativelevel = 0;
        //CAXM.AxmSignalGetLimit(m, ref upStopMode, ref upPositiveLevel, ref upNegativelevel);
        //uint dwErrHome = CAXM.AxmStatusReadMotionInfo(m, ref MTStatu);

        //mtSTS[m].bSensorCW      = (((MTStatu.uMechSig & 0x00001) == 0x00001) && (upPositiveLevel == (uint)AXT_MOTION_SIGNAL_LEVEL.ACTIVE))  ? true : false;
        //mtSTS[m].bSensorCCW     = (((MTStatu.uMechSig & 0x00002) == 0x00002) && (upNegativelevel == (uint)AXT_MOTION_SIGNAL_LEVEL.ACTIVE)) ? true : false;
        //mtSTS[m].bSensorHome    = ((MTStatu.uMechSig & 0x00080) == 0x00080) ? true : false;
        //mtSTS[m].bAlram         = (MTStatu.uMechSig & 0x00010) == 0x00010 ? true : false;

        uint uStatus;
        uint uP = 0, uN = 0, uStopMode = 0, uPS = 0, uNS = 0;
        double mCurPos, mComPos, mCurSpd;

        if (COM_.IsNotMotor(m)){
            mtSTS[m].bAlram = false;
            mtSTS[m].bHomeComplete = true;
        }

        uStatus = GET_SV(m);
        mtSTS[m].bSvOn = uStatus == 1 ? true : false;

        mtSTS[m].bBusy = MTRDY(m);

        mComPos = GET_CMDPOS(m);
        mtSTS[m].CmdPosition = mComPos;

        //if (COM_.IsNotEncMotor(m))  mCurPos = GET_CMDPOS(m);
        //else                        mCurPos = GET_ACTPOS(m);
        mCurPos = GET_ACTPOS(m);
        mtSTS[m].CurrentPosition = mCurPos;

        mCurSpd = GET_CURSPD(m);
        mtSTS[m].dCurSpeed = mCurSpd;

        uStatus = GET_HOME_SENSOR(m);
        mtSTS[m].bSensorHome = uStatus == 1 ? true : false;

        GET_LIMIT_STATUS(m, ref uStopMode, ref uP, ref uN, ref uPS, ref uNS);
        if (uP == 2) mtSTS[m].bSensorCW = false;
        else mtSTS[m].bSensorCW = uPS == 1 ? true : false;
        if (uN == 2) mtSTS[m].bSensorCCW = false;
        else mtSTS[m].bSensorCCW = uNS == 1 ? true : false;

        uStatus = GET_ALARM(m);
        mtSTS[m].bAlram = uStatus == 1 ? true : false;

        uStatus = GET_MT_READY(m);
        mtSTS[m].bReady = uStatus == 1 ? true : false;

        uStatus = GET_MT_EMO(m);
        mtSTS[m].bEMO = uStatus == 1 ? false : true;
    }
    public static void CHK_CURRENT_STATUS(int mt, double dRange){
        double dCP;
        double dPOS;

        for (int p = 0; p < CNT_.POS; p++){
            dCP = GET_ACTPOS(mt);//mtSTS[mt].CurrentPosition;
            dPOS = mtDATA[mt, p].Pos;

            if (dPOS - dRange < dCP && dCP < dPOS + dRange) mtDATA[mt, p].bPOS = true;
            else mtDATA[mt, p].bPOS = false;

            if (dCP < (dPOS + dRange)) mtDATA[mt, p].bMINUS = true;
            else mtDATA[mt, p].bMINUS = false;

            if (dCP > (dPOS + dRange)) mtDATA[mt, p].bPLUS = true;
            else mtDATA[mt, p].bPLUS = false;
        }
    }

    public static void HOMEING_STATUS(int mt){
        uint uStatus;
        if (mtCMD[mt].CMDReset){
            tmReset[mt] += 1;
            if (tmReset[mt] > 100) ALARMRESET(mCurTeachMotor);
            mtCMD[mt].CMDReset = false;
        }
        else tmReset[mt] = 0;

        if (mtCMD[mt].CMDHome){
            tmHome[mt] += 1;
            uStatus = GET_HOME_RESULT(mt);
            if (1 == uStatus){
                for (int i = 0; i < mtTrigger.Length; i++){
                    if (mtTrigger[i] == mt){
                        if (SubTrigger == null) break;
                        MAKE_COUNTER_ZERO(SubTrigger[i], 0);
                        UTIL_.DELAY(33);
                    }
                }
                mtSTS[mt].bHomeComplete = true;
                mtSTS[mt].bHomming = false;
                mtCMD[mt].CMDHome = false;
                UTIL_.LOG_HOME(mt, "HOME OK");
            } // 홈 동작 완료
            if (30000 < tmHome[mt]){
                MTSSTOP(mt, "HOME TIME-OVER");
                UTIL_.LOG_HOME(mt, "HOME TIME-OVER");
                mtCMD[mt].CMDHome = false;
            } // 홈 타임오버 알람.
            if (INPUT((short)STOP) || mIN[(short)VT_STOP] || bPushStop_Rec){
                MTSSTOP(mt, "HOME USE-STOP");
                UTIL_.LOG_HOME(mt, "HOME TIME-OVER");
                mtCMD[mt].CMDHome = false;
            } // 홈 진행 중 정지
            if (mtSTS[mt].bAlram){
                mtSTS[mt].bErrSVAlarm = true;
                mtCMD[mt].CMDHome = false;
            } // 홈 진행 중 모터 알람.
        }
        else tmHome[mt] = 0;

        if (bHomeZero[mt]){
            tmHomeZero[mt] += 1;
            if (tmHomeZero[mt] > 1000){
                MAKE_ZERO(mt, 0);
                bHomeZero[mt] = false;
            }
        }
        else tmHomeZero[mt] = 0;
    }

    public static bool PRE_MOVE_CHECK(int m){
        if (bBD) return true;
        string sLOG = "MOTOR : " + m.ToString() + " [ " + MtName[m] + " ] SET MISS -> ";

        if (!bAllHomeComplete && eMCStatus != eMachineStatus.INITIAL) return false;
        if (!mtSTS[m].bHomeComplete){
            UTIL_.OnERROR(eMTBegin + (eMTGap * m) + eNotHome, 500);
            return false;
        }
        if (!mtSTS[m].bSvOn) SVON(m);
        if (mtSTS[m].bAlram){
            UTIL_.OnERROR(eMTBegin + (eMTGap * m) + eALARM, 500);
            return false;
        }
        return true;
    }

    public static int GET_MAX_MOVE_TIME(int[] m){
        int tMOVE = mtCHK[m[0]].time;
        for (int i = 0; i < m.Length; i++){
            int mt = m[i];
            if (tMOVE < mtCHK[mt].time) tMOVE = mtCHK[mt].time;
        }
        return tMOVE;
    }

    public static void CHK_CURRENT_POS(int mt, int pos){
        if (CHK_POS_RANGE(mt, pos, 2)) mtDATA[mt, pos].bPOS = true;
        else mtDATA[mt, pos].bPOS = false;
    }
    public static void CHK_CURRENT_MANUS(int mt, int pos){
        if (CHK_POS_AREA_MINUS(mt, pos, 2)) mtDATA[mt, pos].bMINUS = true;
        else mtDATA[mt, pos].bMINUS = false;
    }
    public static void CHK_CURRENT_PLUSE(int mt, int pos){
        if (CHK_POS_AREA_PLUSE(mt, pos, 2)) mtDATA[mt, pos].bPLUS = true;
        else mtDATA[mt, pos].bPLUS = false;
    }
    public static eCOMP CHK_POS_CURR_POS_STATE(int iMT, double dPOS){
        double CP = 0;
        double POS = dPOS;
        CP = GET_ACTPOS(iMT);
        if (!mtSTS[iMT].bHomeComplete) return eCOMP.NotHome;
        if (Math.Abs(CP - POS) < 0.7) return eCOMP.Same;
        if (CP >= (POS + 0.7)) return eCOMP.Plus;
        return eCOMP.Minus;
    } //모터 위치값 지정 위치값에서의 차이 확인
    public static eCOMP CHK_POS_TEACH_POS_STATE(int iMT, int iPOS){
        double dCP = 0;
        double dPOS = mtDATA[iMT, iPOS].Pos;
        dCP = GET_ACTPOS(iMT);
        if (!mtSTS[iMT].bHomeComplete) return eCOMP.NotHome;
        if (Math.Abs(dCP - dPOS) < 0.7) return eCOMP.Same;
        if (dCP >= (dPOS + 0.7)) return eCOMP.Plus;
        return eCOMP.Minus;
    } //모터 위치값 지정 위치값에서의 차이 확인
    public static bool CHK_POS_RANGE(int iMT, int iPOS, double dRANGE){
        double dCP = mtSTS[iMT].CurrentPosition;
        double dPOS = mtDATA[iMT, iPOS].Pos;
        double NPos = dPOS - dRANGE;
        double PPos = dPOS + dRANGE;

        if (NPos < dCP && dCP < PPos) return true;
        return false;
    } //모터 위치 지정 범위 안에 있는지 확인
    public static bool CHK_POS_2POS(int mt, double dPOS1, double dPOS2){
        double dCP = mtSTS[mt].CurrentPosition;
        double p1 = dPOS1;
        double p2 = dPOS2;
        if (dPOS2 > dPOS1){
            p1 = dPOS2;
            p2 = dPOS1;
        }
        if (p1 < dCP && dCP < p2) return true;
        return false;
    } //두 위치 사이에 있는지 확인
    public static bool CHK_POS_2AXIS(int mt1, int mt2, int pos1, int pos2){
        if (CHK_POS_TEACH_POS_STATE(mt1, pos1) == eCOMP.Same && CHK_POS_TEACH_POS_STATE(mt2, pos2) == eCOMP.Same) return true;
        return false;
    }

    public static bool CHK_POS_AREA_PLUSE(int m, int pos, double toller){
        double dCP = GET_ACTPOS(m);
        if (dCP > (mtDATA[m, pos].Pos + toller)) return true;
        return false;
    } // 해당 축 티칭 위치보다 +인지 확인
    public static bool CHK_POS_AREA_PLUSE(int m, double pos, double toller){
        double dCP = GET_ACTPOS(m);
        if (dCP > (pos + toller)) return true;
        return false;
    }

    public static bool CHK_POS_AREA_MINUS(int m, int pos, double toller){
        double dCP = GET_ACTPOS(m);
        if (dCP < (mtDATA[m, pos].Pos + toller)) return true;
        return false;
    } // 해당 축 티칭 위치보다 -인지 확인
    public static bool CHK_POS_AREA_MINUS(int m, double pos, double toller){
        double dCP = GET_ACTPOS(m);
        if (dCP < (pos + toller)) return true;
        return false;
    }

    public static bool CHK_POS(int m, int pos, int clr){
        if (!mtSTS[m].bHomeComplete) return false;
        double dCP = GET_ACTPOS(m);
        if (Math.Abs(dCP - mtDATA[m, pos].Pos) < clr) return true;
        return false;
    } // 해당 축 위치 편차 값 확인

    public static bool CHK_MOVING(int m, uint uRTN, string cmd){
        if (uRTN == 0) UTIL_.DELAY(10);
        if (0 != uRTN){
            mtOPTION[m].DontStop = false;
            mtCHK[m].errLog = "MTSSTOP[MOVE FAIL]" + ETC.CrLf + cmd + " FAIL !";
            MTSSTOP(m, mtCHK[m].errLog);
            //if (uRTN == 4152){ //구동 중 다른 명령 들어가면.
            //
            //}
            UTIL_.OnERROR_MOTION(m, eMotMOVE, 500);
            return false;
        }
        return true;
    }
    public static bool CHK_ALARM(int m, string cmd){
        if (mtSTS[m].bAlram){
            mtOPTION[m].DontStop = false;
            mtCHK[m].errLog += "MTSSTOP[MOVE ALARM STOP]" + ETC.CrLf + cmd + " FAIL !";
            LAB_.MTSSTOP(m, "MT_ARRAY_STOP [MOVE ALARM STOP]");
            UTIL_.OnERROR_MOTION(m, eALARM, 500);
            return false;
        }
        return true;
    }

    public static uint GET_HOME_SENSOR(int m){
        uint upStatus = 0;
        CAXM.AxmHomeReadSignal(m, ref upStatus);
        return upStatus;
    }

    public static void GET_LIMIT_STATUS(int m, ref uint uStopMode, ref uint uPLevel, ref uint uNLevel, ref uint uPStatus, ref uint uNStatus){
        CAXM.AxmSignalGetLimit(m, ref uStopMode, ref uPLevel, ref uNLevel); // 리미트 셋팅 상태
        CAXM.AxmSignalReadLimit(m, ref uPStatus, ref uNStatus);
    }

    public static uint GET_HOME_RESULT(int m){
        uint uHomeResult = 0;
        CAXM.AxmHomeGetResult(m, ref uHomeResult);
        return uHomeResult;
    }

    public static uint GET_MT_EMO(int m){
        uint uStatus = 0;
        CAXM.AxmSignalReadStop(m, ref uStatus);
        return uStatus;
    }

    public static void ONE_SHOT_TRI(int i){
        CAXM.AxmTriggerSetReset(i);
        CAXM.AxmTriggerSetTimeLevel(i, 100, 1, 1, 0);
        CAXM.AxmTriggerOneShot(i);
    }

    public static double GET_CMDPOS(int m){
        double dCURPOS = 0;
        CAXM.AxmStatusGetCmdPos(m, ref dCURPOS);
        return double.Parse(dCURPOS.ToString("0.000"));
    }

    public static double GET_ACTPOS(int m){
        double dCURPOS = 0;
        if (COM_.IsNotEncMotor(m)) return GET_CMDPOS(m);
        CAXM.AxmStatusGetActPos(m, ref dCURPOS);
        return double.Parse(dCURPOS.ToString("0.000"));
    }

    public static double GET_CURSPD(int m){
        double dCURSPD = 0;
        CAXM.AxmStatusReadVel(m, ref dCURSPD);
        return double.Parse(dCURSPD.ToString("0.000"));
    }

    public static void MAKE_ZERO(int m, int delay){
        UTIL_.DELAY(delay);
        CAXM.AxmStatusSetActPos(m, 0);
        CAXM.AxmStatusSetCmdPos(m, 0);
        UTIL_.DELAY(10);
    }

    public static void MAKE_COUNTER_ZERO(int m, int delay){
        UTIL_.DELAY(delay);
        CAXC.AxcStatusSetActPos(m, 0);
        UTIL_.DELAY(10);
    }

    public static bool SET_POSZERO(int m, int delay){
        MAKE_ZERO(m, delay);
        double dCMDPOS = GET_CMDPOS(m);
        double dACTPOS = GET_ACTPOS(m);
        if (dCMDPOS < 0.005 && dACTPOS < 0.005) return true;
        return false;
    }

    public static void SET_POS_SYNCH(int m, int delay){
        UTIL_.DELAY(delay);
        double dACTPOS = GET_ACTPOS(m);
        CAXM.AxmStatusSetCmdPos(m, dACTPOS);
    }

    public static uint GET_SV(int m){
        uint uStatus = 0;
        CAXM.AxmSignalIsServoOn(m, ref uStatus);
        return uStatus;
    }

    public static bool SVON(int m){
        if (mtSTS[m].bAlram) return false;
        CAXM.AxmSignalServoOn(m, 1);
        SET_POS_SYNCH(m, 500);
        return true;
    }
    public static bool ALL_SVON(){
        uint status = 0;
        for (int m = 0; m < CNT_.MT; m++){
            if (COM_.IsNotMotor(m) || !enableHome[m]) continue;
            if (mtSTS[m].bAlram) return false;
            CAXM.AxmSignalIsServoOn(m, ref status);
            if (status == 1) continue;
            CAXM.AxmSignalServoOn(m, 1);
        }
        UTIL_.DELAY(1000);
        for (int m = 0; m < CNT_.MT; m++){
            if (COM_.IsNotMotor(m)) continue;
            SET_POS_SYNCH(m, 10);
        }
        UTIL_.DELAY(100);
        return true;
    }

    public static void SERVO(int m, bool bValue){
        uint iValue = bValue ? (uint)1 : (uint)0;
        CAXM.AxmSignalServoOn(m, iValue);
    }
    public static bool SVOFF(int m){
        if (eMCStatus == eMachineStatus.AUTO || eMCStatus == eMachineStatus.INITIAL) return false;
        if (bBD) return false;
        CAXM.AxmSignalServoOn(m, 0);
        return true;
    }
    public static bool ALL_SVOFF(){
        for (int i = 0; i < CNT_.MT; i++) SVOFF(i);
        UTIL_.DELAY(100);
        return true;
    }

    public static void MTSVON(int m, uint value){
        CAXM.AxmSignalServoOn(m, value);
    }

    public static uint GET_ALARM(int m){
        uint upStatus = 0;
        CAXM.AxmSignalReadServoAlarm(m, ref upStatus);
        return upStatus;
    }

    public static bool AlarmStatus(int nThread, string comment){
        for (int i = 0; i < CNT_.MT; i++){
            if (mtSTS[i].bAlram && enableHome[i]){
                UTIL_.OnERROR_MOTION(i, eALARM, 500);
                LogWR_.AddMessage(nThread, comment + " -> MOTOR ALARM INTIAL FAIL");
                return false;
            }
        }
        return true;
    }

    public static void ALARMRESET(int m){
        CAXM.AxmSignalServoAlarmReset(m, 1);
        UTIL_.DELAY(200);
        CAXM.AxmSignalServoAlarmReset(m, 0);
    }
    public static void ALL_ALARMCLEAR(){
        for (int i = 0; i < CNT_.MT; i++){
            if (mtSTS[i].bAlram){
                CAXM.AxmSignalServoAlarmReset(i, 1);
                UTIL_.DELAY(200);
                CAXM.AxmSignalServoAlarmReset(i, 0);
            }
        }
    }

    public static uint GET_MT_READY(int m){
        uint uOn = 0;
        CAXM.AxmSignalReadInputBit(m, 2, ref uOn);
        return uOn;
    }

    public static bool MTRDY(int m){
        uint mBUSY = 0;
        if (!bBD || !mtSTS[m].bAlram){
            CAXM.AxmStatusReadInMotion(m, ref mBUSY);
            return mBUSY == 0 ? false : true;
        } //T:RDY , F:NOT RDY
        return false;
    }

    public static bool MTBUSY(int m){
        uint mBUSY = 0;
        if (bBD || mtSTS[m].bAlram) return false;
        CAXM.AxmStatusReadInMotion(m, ref mBUSY);
        return mBUSY == 1 ? true : false; //T:MOVING, F:NOT MOVING
    }

    public static bool MTESTOP(int m, string sHistory){
        string sDATE = DateTime.Now.ToString() + " " + ETC.CrLf;
        if (eMCStatus == eMachineStatus.EMSSTOP){
            CAXM.AxmMoveEStop(m);
            mtSTS[m].strStop = sDATE + sHistory + ETC.NewLine + "EMS STOP !";
            return false;
        }
        if (!MTBUSY(m) || (mtOPTION[m].DontStop && eMCStatus == eMachineStatus.AUTO)) return false;
        CAXM.AxmMoveEStop(m);
        mtSTS[m].strStop = sDATE + sHistory;
        return true;
    }
    public static bool MTSSTOP(int m, string sHistory){
        string sDATE = DateTime.Now.ToString() + " " + ETC.CrLf;
        if (eMCStatus == eMachineStatus.EMSSTOP){
            CAXM.AxmMoveEStop(m);
            mtSTS[m].strStop = sDATE + sHistory + ETC.NewLine + "EMS STOP !";
            return false;
        }
        if (!MTBUSY(m) || (mtOPTION[m].DontStop && eMCStatus == eMachineStatus.AUTO)) return false;
        CAXM.AxmMoveSStop(m);
        mtSTS[m].strStop = sDATE + sHistory;
        return true;
    }
    public static bool MT_WAITSTOP(int m, int mSec){
        int Delay = mSec <= 0 ? 100 : mSec;
        for (int i = 0; i < Delay; i++){
            UTIL_.DELAY(3);
            if (!MTBUSY(m)) return true; //MOVING 완료.
        }
        return false; //MOVING 중.
    }
    public static bool MT_ARRAY_STOP(int[] m, string sHistory){
        for (int i = 0; i < m.Length; i++){
            int mt = m[i];
            if (mtOPTION[i].DontStop){
                mtSTS[mt].strStop = DateTime.Now.ToString() + "->MT_ARRAY_STOP" + ETC.NewLine + sHistory + ETC.NewLine + "Don't Stop Pass";
                continue;
            }
            MTSSTOP(mt, sHistory);
            mtSTS[mt].strStop = DateTime.Now.ToString() + "->MT_ARRAY_STOP" + ETC.NewLine + sHistory;
        }
        return true;
    }
    public static void MT_ALL_STOP(bool bOPTION, string sHISTORY){
        if (bOPTION){
            for (int i = 0; i < CNT_.MT; i++){
                mtOPTION[i].DontStop = false;
                mtCHK[i].Ev = "STOP";
            }
        } // 바로 정지 명령
        for (int i = 0; i < CNT_.MT; i++){
            if (mtOPTION[i].DontStop){
                mtSTS[i].strStop = DateTime.Now.ToString() + "->MT_ALL_STOP" + ETC.NewLine + sHISTORY + ETC.NewLine + ETC.NewLine + "DON'T STOP PASS";
                continue;
            }
            if (MTBUSY(i)){
                MTSSTOP(i, sHISTORY);
                mtSTS[i].strStop = DateTime.Now.ToString() + "->MT_ALL_STOP" + ETC.NewLine + sHISTORY;
            }
        }
    }

    public static eRTN MT_JOG_CCW(int mt, double spd){
        if (eMCStatus == eMachineStatus.AUTO || !mtSTS[mt].bSvOn) return eRTN.FAIL;
        if (!mtSTS[mt].bHomeComplete) return eRTN.FAIL;
        if (spd > mtSoftData[mt].MaxSpd) spd = mtSoftData[mt].MaxSpd;
        if (bMAINT || bDoorOpen) if (spd > 10) spd = 10;
        double TPos = mtSoftData[mt].CcwSoftLimit;
        bool bROTATE_MT = false;
        mtSTS[mt].bJoggingCCW = true;

        CAXM.AxmMotSetAbsRelMode(mt, 0); //절대좌표 이송
        for (int i = 0; i < CNT_.MT; i++){
            if (mtRingCount == null) break;
            if (mt == mtRingCount[i]) bROTATE_MT = false;
        }
        CAXM.AxmMotSetMinVel(mt, 0.01);
        if (bROTATE_MT) { CAXM.AxmMoveVel(mt, -spd, spd * 10, spd * 10); }
        else { CAXM.AxmMoveStartPos(mt, TPos, spd, spd * 10, spd * 10); }
        return eRTN.SUCESS;
    }

    public static eRTN MT_JOG_CW(int mt, double spd){
        if (eMCStatus == eMachineStatus.AUTO || !mtSTS[mt].bSvOn) return eRTN.FAIL;
        if (!mtSTS[mt].bHomeComplete) return eRTN.FAIL;
        if (spd > mtSoftData[mt].MaxSpd) spd = mtSoftData[mt].MaxSpd;
        if (bMAINT || bDoorOpen) if (spd > 10) spd = 10;
        double TPos = mtSoftData[mt].CwSoftLimit;
        bool bROTATE_MT = false;
        mtSTS[mt].bJoggingCW = true;

        CAXM.AxmMotSetAbsRelMode(mt, 0); //절대좌표 이송
        for (int i = 0; i < CNT_.MT; i++){
            if (mtRingCount == null) break;
            if (mt == mtRingCount[i]) bROTATE_MT = false;
        }
        CAXM.AxmMotSetMinVel(mt, 0.01);
        if (bROTATE_MT) { CAXM.AxmMoveVel(mt, spd, spd * 10, spd * 10); }
        else { CAXM.AxmMoveStartPos(mt, TPos, spd, spd * 10, spd * 10); }
        return eRTN.SUCESS;
    }

    public static eRTN MT_PITCH_CCW(int mt, double spd, double pitch){
        if (eMCStatus == eMachineStatus.AUTO || !mtSTS[mt].bSvOn || !mtSTS[mt].bHomeComplete || pitch <= 0) return eRTN.FAIL;
        if (spd > mtSoftData[mt].MaxSpd) spd = mtSoftData[mt].MaxSpd;
        if (bMAINT || bDoorOpen) if (spd > 10) spd = 10;
        double TPos = GET_CMDPOS(mt) - pitch;//GET_ACTPOS(mt) - pitch;

        if (mtSoftData[mt].CcwSoftLimit > TPos) return eRTN.FAIL;
        CAXM.AxmMotSetMinVel(mt, 0.01);
        CAXM.AxmMoveStartPos(mt, TPos, spd, spd * 10, spd * 10);
        return eRTN.SUCESS;
    }

    public static eRTN MT_PITCH_CW(int mt, double spd, double pitch){
        if (eMCStatus == eMachineStatus.AUTO || !mtSTS[mt].bSvOn || !mtSTS[mt].bHomeComplete || pitch <= 0) return eRTN.FAIL;
        if (spd > mtSoftData[mt].MaxSpd) spd = mtSoftData[mt].MaxSpd;
        if (bMAINT || bDoorOpen) if (spd > 10) spd = 10;
        double TPos = GET_CMDPOS(mt) + pitch;//GET_ACTPOS(mt) + pitch;

        if (mtSoftData[mt].CwSoftLimit < TPos) return eRTN.FAIL;
        CAXM.AxmMotSetMinVel(mt, 0.01);
        CAXM.AxmMoveStartPos(mt, TPos, spd, spd * 10, spd * 10);
        return eRTN.SUCESS;
    }

    public static bool MT_HOME(int m){
        mtSTS[m].bHomeComplete = false;
        if (mtSTS[m].bAlram){
            mtCMD[m].CMDReset = true;
            ALARMRESET(m);
        }
        if (mtSTS[m].bAlram){
            mtSTS[m].bErrHome = true;
            mtSTS[m].bHomming = false;
            UTIL_.OnERROR_MOTION(m, eHOME, 500);
            return false;
        }
        if (MTBUSY(m)){
            MTSSTOP(m, "MT_HOME -> MTBUSY STOP");
            UTIL_.OnERROR_MOTION(m, eHOME, 500);
            return false;
        }
        if (!mtSTS[m].bSvOn){
            SVON(m);
            UTIL_.DELAY(100);
            if (!mtSTS[m].bSvOn && !COM_.IsNotEncMotor(m)){
                UTIL_.OnERROR_MOTION(m, eHOME, 500);
                return false;
            }
        }

        if (COM_.CurrentLocationHome(m)) return true;// 현재 위치에서 홈

        int dir = 0;
        uint sig = 0, z = 0;
        double ct = 0, ost = 0;
        uint uRTN = CAXM.AxmHomeGetMethod(m, ref dir, ref sig, ref z, ref ct, ref ost);
        mtSTS[m].strHome        = "HOME START";
        mtSTS[m].bHomeComplete  = false;
        mtSTS[m].bErrHome       = false;
        mtSTS[m].bHomming       = true;
        uRTN                    = CAXM.AxmHomeSetStart(m);
        mtCMD[m].CMDHome        = true;
        return true;
    }

    public static bool WAIT_HOME(int m){
        while (mtCMD[m].CMDHome) UTIL_.DELAY(1);
        if (mtSTS[m].bErrHome){
            UTIL_.OnERROR_MOTION(m, eHOME, 500);
            return false;
        }
        if (!mtSTS[m].bHomeComplete) return false;
        return true;
    }

    public static uint SET_MOVE_MODE(int m, uint uAbsRelMode){
        return CAXM.AxmMotSetAbsRelMode(m, uAbsRelMode); // POS_ABS_MODE '0' - 절대 좌표계/POS_REL_MODE '1' - 상대 좌표계 
    }
    public static uint SET_PROFILE_MODE(int m, uint uProfileMode){
        // 지정 축의 구동 속도 프로파일 모드를 설정한다.
        // ProfileMode : SYM_TRAPEZOIDE_MODE    '0' - 대칭 Trapezode
        //               ASYM_TRAPEZOIDE_MODE   '1' - 비대칭 Trapezode
        //               QUASI_S_CURVE_MODE     '2' - 대칭 Quasi-S Curve
        //               SYM_S_CURVE_MODE       '3' - 대칭 S Curve
        //               ASYM_S_CURVE_MODE      '4' - 비대칭 S Curve
        //               SYM_TRAP_M3_SW_MODE    '5' - 대칭 Trapezode : MLIII 내부 S/W Profile
        //               ASYM_TRAP_M3_SW_MODE   '6' - 비대칭 Trapezode : MLIII 내부 S/W Profile
        //               SYM_S_M3_SW_MODE       '7' - 대칭 S Curve : MLIII 내부 S/W Profile
        //               ASYM_S_M3_SW_MODE      '8' - asymmetric S Curve : MLIII 내부 S/W Profile
        return CAXM.AxmMotSetProfileMode(m, uProfileMode);
    }
    public static uint SET_PROFILE_MODE(int[] m, uint uProfileMode){
        uint uRtn = 0;
        for (int i = 0; i < m.Length; i++){
            uRtn = CAXM.AxmMotSetProfileMode(m[i], uProfileMode);
        }
        return uRtn;
    }

    public static uint MT_STARTMOVE(int m, double p, double s){
        CAXM.AxmMotSetMinVel(m, s < 10 ? 0.01 : 10);
        return CAXM.AxmMoveStartPos(m, p, s, s * 7, s * 7); // 펄스가 출력되는 시점에서 함수를 벗어난다.
    }
    public static void MT_STARTMOVE(int[] m, double[] p, double[] s){
        for (int i = 0; i < m.Length; i++){
            CAXM.AxmMotSetMinVel(m[i], s[i] < 10 ? 0.01 : 10);
            CAXM.AxmMoveStartPos(m[i], p[i], s[i], s[i] * 7, s[i] * 7);
        }
    }
    public static uint MOVE_START(int m, double pos, double spd, double acc, double dec){
        CAXM.AxmMotSetMinVel(m, spd < 10 ? 0.01 : 10);
        return CAXM.AxmMoveStartPos(m, pos, spd, acc, dec); // 펄스가 출력되는 시점에서 함수를 벗어난다.
    }
    public static uint MOVE_POS(int m, double pos, double spd, double acc, double dec){
        CAXM.AxmMotSetMinVel(m, spd < 10 ? 0.01 : 10);
        return CAXM.AxmMovePos(m, pos, spd, acc, dec); // 펄스 출력이 종료되는 시점에서 함수를 벗어난다
    }
    public static uint MOVE_MUTI_POS(int[] m, double[] pos, double[] spd, double[] acc, double[] dec){
        for (int i = 0; i < m.Length; i++) CAXM.AxmMotSetMinVel(m[i], 5);
        return CAXM.AxmMoveStartMultiPos(m.Length, m, pos, spd, acc, dec);  // 함수를 실행하면 해당 Motion 동작을 시작한 후 Motion 이 완료될때까지 기다리지 않고 바로 함수를 빠져나간다."
    }
    public static uint MOVE_MUTI_LINE_POS(int[] m, double[] pos, double spd, double acc, double dec, uint uAbsRelMode){
        //uint lAxisSize = (uint)m.Length;     //{0,1,2,3}{4,5,6,7}{8,9,10,11}....
        uint lPosSize = (uint)pos.Length;
        int lCoordinate = 0;
        for (int i = 0; i < m.Length; i++) CAXM.AxmMotSetMinVel(m[i], 5);

        CAXM.AxmContiWriteClear(lCoordinate);
        CAXM.AxmContiSetAxisMap(lCoordinate, lPosSize, m);
        CAXM.AxmContiSetAbsRelMode(lCoordinate, uAbsRelMode);
        uint uRTN = CAXM.AxmLineMove(lCoordinate, pos, spd, acc, dec);
        return uRTN;
    }
    public static uint MOVE_OVERRIDE_VEL(int m, double Pos, double Vel, double Acc, double Dec, double[] ArrPos, double[] ArrSpd, int Target, uint OverrideMode, double OverrideMaxSpd){
        int CntPos = ArrPos.Length;
        CAXM.AxmOverrideSetMaxVel(m, OverrideMaxSpd);
        uint uRTN = CAXM.AxmOverrideVelAtMultiPos(m, Pos, Vel, Acc, Dec, CntPos, ArrPos, ArrSpd, Target, OverrideMode);
        return uRTN;
    }

    public static uint MOVE_OVERRIDE_VEL(int m, double Pos, double Vel, double Acc, double Dec, double OverPos, double OverVel, int lTarget){
        //CAXM.AxmMotSetMinVel(m, Vel < 10 ? 0.01 : 10);
        CAXM.AxmOverrideSetMaxVel(m, 2000);
        return CAXM.AxmOverrideVelAtPos(m, Pos, Vel, Acc, Dec, OverPos, OverVel, /*(int)AXT_MOTION_SELECTION.ACTUAL*/lTarget);
    }

    public static uint DIRECT_MOVE(int m, int pos, double spd){
        stMoveInfo mi = mtDATA[m, pos];
        if (spd > mtSoftData[m].MaxSpd) spd = mtSoftData[m].MaxSpd;
        if (spd < mtSoftData[m].MinSpd) spd = mtSoftData[m].MinSpd;

        CAXM.AxmMotSetMinVel(m, spd < 10 ? 0.01 : 10);
        return MOVE_POS(m, mi.Pos, spd, spd * 10, spd * 10);
    } //MOVING 완료 !

    public static eRTN MOVE(int nThread, int[] m, stMoveInfo[] mv, double[] toller, bool[] onlyStart, bool[] NoChange, bool[] DontStop, string[] cmd, string comment){
        long sTIME = System.Environment.TickCount;
        double eTIME;
        string sResult;
        int cMT = m.Length;
        eRTN eMoveFail = eRTN.FAIL;
        LogWR_.SaveMarsLog(nThread, eLogTYPE.EVT, comment, "START");
        for (int i = 0; i < m.Length; i++){ //모션 이송.
            int mt = m[i];
            CLEAR_MOVECHKECK(mt, mv[i]);
            mtCHK[mt].cmd           = cmd[i];
            mtCHK[mt].coment        = comment + " -> MOVE";
            mtCHK[mt].toller        = toller[i];
            mtCHK[mt].onlyStart     = onlyStart[i];
            mtCHK[mt].noChange      = NoChange[i];
            mtOPTION[mt].DontStop   = DontStop[i];
            mtCHK[mt].Ev = "";
            if (!PRE_MOVE_CHECK(mt)){
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "PRE_MOVE_CHECK() FAIL !");
                mtCHK[mt].errLog = "PRE_MOVE_CHECK() FAIL" + ETC.CrLf;
                goto Fail;
            }
            else mtCHK[mt].sts = "PRE_MOVE_CHECK() -> COMPLETE" + ETC.CrLf;
            if (MTBUSY(mt)){
                MTSSTOP(mt, "BUSY STOP");
                UTIL_.DELAY(100);
                mtCHK[mt].errLog += "BUSY STOP" + ETC.CrLf;
            }
            mtCHK[mt].sts = "MTBUSY -> COMPLETE" + ETC.CrLf;

            SET_MOVE_MODE(mt, 0); // 절대좌표 이송
            double dRUN_RATE = dRunRate;
            if (dRUN_RATE > 100) dRUN_RATE = 100;
            if (dRUN_RATE < 1) dRUN_RATE = 1;
            if (!mtCHK[mt].noChange && mtCHK[mt].spd > 50){
                mtCHK[mt].spd   = mtCHK[mt].spd * (dRUN_RATE / 100);
                mtCHK[mt].acc   = mtCHK[mt].acc * (dRUN_RATE / 100);
                mtCHK[mt].dcc   = mtCHK[mt].dcc * (dRUN_RATE / 100);
                mtCHK[mt].time  = mtCHK[mt].time * (int)(100 / dRUN_RATE);
            }

            double dPOS         = GET_ACTPOS(mt);
            mtCHK[mt].posBegin  = dPOS;
            double gPOS         = Math.Abs(dPOS - mtCHK[mt].pos);
            double ShortSpd     = mtSoftData[mt].ShortSpeed < 10 ? 10 : mtSoftData[mt].ShortSpeed;
            if (gPOS < mtSoftData[mt].ShortLength && mtCHK[mt].spd > ShortSpd) mtCHK[mt].spd = ShortSpd;
            mtCHK[mt].sts += "SPEED ADJUST -> COMPLETE" + ETC.CrLf;

            //소프트 리미트 값 확인 !
            if (mtSoftData[mt].CwSoftLimit < mtCHK[mt].pos){ //+
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]");
                mtCHK[mt].errLog += "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(mt, eCwSoftLime, 500);
                goto Fail;
            }
            if (mtSoftData[mt].CcwSoftLimit > mtCHK[mt].pos){ //-
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]");
                mtCHK[mt].errLog += "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(mt, eCcwSoftLime, 500);
                goto Fail;
            }

            uint dwEND = 0;
            for (int j = 0; j < 100; j++){
                if (bBD){
                    dwEND = 0;
                    break;
                }
                if (MTBUSY(mt)){
                    MTSSTOP(mt, "BUSY STOP");
                    UTIL_.DELAY(100);
                }
                SET_PROFILE_MODE(mt, /*(uint)AXT_MOTION_PROFILE_MODE.ASYM_TRAPEZOIDE_MODE*/(uint)AXT_MOTION_PROFILE_MODE.ASYM_S_CURVE_MODE);
                dwEND = MOVE_START(mt, mtCHK[mt].pos, mtCHK[mt].spd, mtCHK[mt].acc, mtCHK[mt].dcc);
                if (dwEND == 0){
                    mtCHK[mt].sts += "MTMOVE -> COMPLETE" + ETC.CrLf;
                    UTIL_.DELAY(10);
                    break;
                }
            }
            if (0 != dwEND){
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "MTMOVE() FAIL !");
                mtCHK[mt].errLog = "MT_ARRAY_STOP" + ETC.CrLf + "MTMOVE() FAIL !";
                //mLogWR.DEBUG_PRINT("MTMOVE FAILED -> dwEMD = " + dwEND.ToString());
                //if (dwEND == 4152)
                //{ //구동 중 다른 명령 들어가면.
                //}
                MT_ARRAY_STOP(m, mtCHK[mt].errLog);
                UTIL_.OnERROR_MOTION(mt, eMotMOVE, 500);
                goto Fail;
            }
        }

        //이송 완료 후 상태 확인
        bool bOnlyStart = true;
        for (int i = 0; i < cMT; i++){
            int mt = m[i];
            if (!mtCHK[mt].onlyStart) bOnlyStart = false;
        }
        if (bOnlyStart) goto Sucess;

        int tm = GET_MAX_MOVE_TIME(m);
        bool bPOS = true;
        for (int i = 0; i < tm; i++){
            if (bBD){
                UTIL_.DELAY(100);
                for (int j = 0; j < cMT; j++){
                    int nMT = m[j];
                    mtSTS[nMT].CurrentPosition  = mtCHK[nMT].pos;
                    mtSTS[nMT].CmdPosition      = mtCHK[nMT].pos;
                    mtOPTION[nMT].DontStop      = false;
                }
                goto Sucess;
            }
            UTIL_.DELAY(1);
            bPOS = true;
            for (int j = 0; j < cMT; j++){
                int nAXIS = m[j];
                if (mtSTS[nAXIS].bSensorCW){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE +LIMIT STOP]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [MOVE +LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nAXIS, eLimitP, 500);
                    goto Fail;
                }
                if (mtSTS[nAXIS].bSensorCCW){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE -LIMIT STOP]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [MOVE -LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nAXIS, eLimitM, 500);
                    goto Fail;
                }
                if (mtSTS[nAXIS].bAlram){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE ALARM STOP]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [MOVE ALARM STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nAXIS, eALARM, 500);
                    goto Fail;
                }

                if (mtCHK[nAXIS].Ev == "STOP" || mtCHK[nAXIS].Ev == "stop" || bPushStop){
                    //if (nAXIS == 4) mLogWR.DEBUG_PRINT("STRIP PICKER MOVING STOP !");
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [PUSH STOP SWITCH]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [PUSH STOP SWITCH]" + ETC.CrLf;
                    goto Fail;
                }
                if (mtCHK[nAXIS].Ev == "ERR" || mtCHK[nAXIS].Ev == "err"){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [ERROR STOP]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [ERROR STOP]" + ETC.CrLf;
                    goto Fail;
                }
                if (mtCHK[nAXIS].Ev == "EMS" || mtCHK[nAXIS].Ev == "ems"){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [EMS STOP]");
                    mtCHK[nAXIS].errLog += "MT_ARRAY_STOP [EMS STOP]" + ETC.CrLf;
                    goto Fail;
                }

                double cPOS         = GET_ACTPOS(nAXIS);
                double chkPOS       = mtCHK[nAXIS].pos;
                double chkTOLLER    = mtCHK[nAXIS].toller;
                if (UTIL_.IsFINGER(nAXIS)) chkTOLLER = 0.5;
                double errTOLLER = Math.Abs(cPOS - chkPOS);
                if (errTOLLER > chkTOLLER && !mtCHK[nAXIS].onlyStart){
                    if (UTIL_.IsFINGER(nAXIS)){
                        if (errTOLLER > 0.05) bPOS = false;
                        else bPOS = false;
                    }
                }
                if (MTBUSY(nAXIS) /*&& !mCOM.IsNotEncMOTOR(nAXIS)*/) bPOS = false;
                if (i >= (tm - 1)){
                    for (int k = 0; k < cMT; k++){
                        int cutm        = m[k];
                        double curps    = GET_ACTPOS(cutm);
                        double chkps    = mtCHK[cutm].pos;
                        double chktlr   = mtCHK[cutm].toller;
                        double errtlr   = Math.Abs(curps - chkps);
                        uint uSTOP      = 0;
                        uint uRTN       = CAXM.AxmStatusReadStop(cutm, ref uSTOP);
                        LogWR_.SAVE_MOVING_ERROR_LOG(cutm, uSTOP.ToString());
                        if (errtlr > chktlr && !mtCHK[cutm].onlyStart){
                            for (int u = 0; u < cMT; u++){
                                mtOPTION[m[u]].DontStop = false;
                            }
                            UTIL_.OnERROR_MOTION(cutm, eTimeOver, 500);
                            goto Fail;
                        }
                    }
                    bPOS = true;
                    break;
                }
                for (int k = 0; k < cMT; k++){
                    int n           = m[k];
                    double p        = mtCHK[n].pos;
                    double eToller  = Math.Abs(GET_ACTPOS(n) - p);
                    double cToller  = 0.3;// mtCHK[n].toller;
                    if (eToller > cToller || MTBUSY(n)) bPOS = false;
                }
            }
            if (bPOS) break;
        }

        for (int i = 0; i < cMT; i++){
            int mt = m[i];
            mtCHK[mt].sts       += "MOVE END" + ETC.CrLf;
            mtCHK[mt].eTime     = Environment.TickCount;
            mtCHK[mt].posStop   = GET_ACTPOS(mt);
            mtCHK[mt].posGap    = Math.Abs(mtCHK[mt].posStop - mtCHK[mt].pos);
            mtCHK[mt].rTime     = (mtCHK[mt].eTime - mtCHK[mt].sTime) / 1000;
        }
        if (!bPOS){ //구동 실패
            for (int i = 0; i < cMT; i++){
                int mt                  = m[i];
                string sERR             = "FAIL RUN TIME = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Gap = " + string.Format("{0:0.000}", mtCHK[mt].posGap);
                mtCHK[mt].sts           += sERR + ETC.CrLf;
                mtCHK[mt].rslt          = GET_MOVE_RESULT(mt);
                mtOPTION[mt].DontStop   = false;
            }
            goto Fail;
        }
        for (int i = 0; i < cMT; i++){
            int mt                  = m[i];
            mtCHK[mt].posStop       = GET_ACTPOS(mt);
            mtCHK[mt].posGap        = Math.Abs(mtCHK[mt].posStop - mtCHK[mt].pos);
            mtCHK[mt].sts           = "Succes Run Time = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Grap = " + string.Format("{0:0.000}", mtCHK[mt].posGap) + ETC.CrLf;
            mtCHK[mt].rslt          = GET_MOVE_RESULT(mt);
            mtOPTION[mt].DontStop   = false;
        }
    Sucess:
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult = "RunTime(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.EVT, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS; ; // Move Success !
    Fail:
        UTIL_.DELAY(500);
        return eMoveFail;
    }

    //같은 보드내에 있는 축 동시 구동.
    public static eRTN MUTI_MOVE_IN_EACH_BOARD(int[] m, stMoveInfo[] mv, double[] toller, bool[] onlyStart, bool[] NoChange, string[] cmd, string comment){
        int cMT = m.Length;
        for (int i = 0; i < m.Length; i++){
            int mt = m[i];
            CLEAR_MOVECHKECK(mt, mv[i]);
            mtCHK[mt].cmd       = cmd[i];
            mtCHK[mt].coment    = comment + " -> MOVE";
            mtCHK[mt].toller    = toller[i];
            mtCHK[mt].onlyStart = onlyStart[i];
            mtCHK[mt].noChange  = NoChange[i];
            mtCHK[mt].Ev        = "";
            if (!PRE_MOVE_CHECK(mt)){
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "PRE_MOVE_CHECK() FAIL !");
                mtCHK[mt].errLog = "PRE_MOVE_CHECK() FAIL" + ETC.CrLf;
                return eRTN.FAIL;
            }
            else mtCHK[mt].sts = "PRE_MOVE_CHECK() -> COMPLETE" + ETC.CrLf;

            if (MTBUSY(mt)){
                MTSSTOP(mt, "BUSY STOP");
                UTIL_.DELAY(100);
                mtCHK[mt].errLog += "BUSY STOP" + ETC.CrLf;
            }
            mtCHK[mt].sts = "MTBUSY -> COMPLETE" + ETC.CrLf;

            SET_MOVE_MODE(mt, 0); // 절대좌표 이송
            double dRUN_RATE = dRunRate;
            if (dRUN_RATE > 100) dRUN_RATE = 100;
            if (dRUN_RATE < 1) dRUN_RATE = 1;
            if (!mtCHK[mt].noChange && mtCHK[mt].spd > 50){
                mtCHK[mt].spd   = mtCHK[mt].spd * (dRUN_RATE / 100);
                mtCHK[mt].acc   = mtCHK[mt].acc * (dRUN_RATE / 100);
                mtCHK[mt].dcc   = mtCHK[mt].dcc * (dRUN_RATE / 100);
                mtCHK[mt].time  = mtCHK[mt].time * (int)(100 / dRUN_RATE);
            }
        }

        uint dwEND;
        int[] nAXIS     = new int[cMT];
        double[] dPOS   = new double[cMT];
        double[] dVEL   = new double[cMT];
        double[] dACC   = new double[cMT];
        double[] dDEC   = new double[cMT];
        for (int i = 0; i < cMT; i++){
            nAXIS[i]    = m[i];
            dPOS[i]     = mv[i].Pos;
            dVEL[i]     = mtCHK[m[i]].spd;
            dACC[i]     = mtCHK[m[i]].acc;
            dDEC[i]     = mtCHK[m[i]].dcc;
            if (MTBUSY(m[i])){
                MTSSTOP(m[i], "BUSY STOP");
                UTIL_.DELAY(100);
            }

            //소프트 리미트 값 확인 !
            if (mtSoftData[m[i]].CwSoftLimit < mtCHK[m[i]].pos){
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]");
                mtCHK[m[i]].errLog += "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m[i], eCwSoftLime, 500);
                return eRTN.FAIL;
            }
            if (mtSoftData[m[i]].CcwSoftLimit > mtCHK[m[i]].pos){
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]");
                mtCHK[m[i]].errLog += "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m[i], eCcwSoftLime, 500);
                return eRTN.FAIL;
            }
        }

        //멀티 구동 함수 !
        SET_PROFILE_MODE(nAXIS, /*(uint)AXT_MOTION_PROFILE_MODE.ASYM_TRAPEZOIDE_MODE*/(uint)AXT_MOTION_PROFILE_MODE.ASYM_S_CURVE_MODE);
        dwEND = MOVE_MUTI_POS(nAXIS, dPOS, dVEL, dACC, dDEC);
        if (0 != dwEND){
            MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "MUTI_MOVE_IN_EACH_BOARD() FAIL !");
            mtCHK[nAXIS[0]].errLog = "MT_ARRAY_STOP" + ETC.CrLf + "MUTI_MOVE_IN_EACH_BOARD() FAIL !";
            UTIL_.OnERROR_MOTION(nAXIS[0], eMotMOVE, 500);
            return eRTN.FAIL;
        }

        //이송 완료 후 상태 확인
        bool bOnlyStart = true; //모든 축 이송 온리
        for (int i = 0; i < cMT; i++){
            int mt = m[i];
            if (!mtCHK[mt].onlyStart) bOnlyStart = false;
        }
        if (bOnlyStart) return eRTN.SUCESS;

        int tm = GET_MAX_MOVE_TIME(m);
        bool bPOS = true;
        double dMaxToller = 0;
        for (int i = 0; i < tm; i++){
            if (bBD){
                UTIL_.DELAY(100);
                for (int j = 0; j < CNT_.MT; j++){
                    int nMT = m[j];
                    mtSTS[nMT].CurrentPosition  = mtCHK[nMT].pos;
                    mtSTS[nMT].CmdPosition      = mtCHK[nMT].pos;
                }
                return eRTN.SUCESS;
            }
            UTIL_.DELAY(1);
            bPOS = true;
            for (int j = 0; j < cMT; j++){
                int nMT = m[j];
                if (mtSTS[nMT].bSensorCW){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE +LIMIT STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE +LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eLimitP, 500);
                    return eRTN.FAIL;
                }
                if (mtSTS[nMT].bSensorCCW){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE -LIMIT STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE -LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eLimitM, 500);
                    return eRTN.FAIL;
                }
                if (mtSTS[nMT].bAlram){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE ALARM STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE ALARM STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eALARM, 500);
                    return eRTN.FAIL;
                }

                if (mtCHK[nMT].Ev == "STOP" || mtCHK[nMT].Ev == "stop"){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [PUSH STOP SWITCH]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [PUSH STOP SWITCH]" + ETC.CrLf;
                    return eRTN.FAIL;
                }
                if (mtCHK[nMT].Ev == "ERR" || mtCHK[nMT].Ev == "err"){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [ERROR STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [ERROR STOP]" + ETC.CrLf;
                    return eRTN.FAIL;
                }
                if (mtCHK[nMT].Ev == "EMS" || mtCHK[nMT].Ev == "ems"){
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [EMS STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [EMS STOP]" + ETC.CrLf;
                    return eRTN.FAIL;
                }

                double cPOS         = GET_ACTPOS(nMT);
                double chkPOS       = mtCHK[nMT].pos;
                double chkTOLLER    = mtCHK[nMT].toller;
                if (UTIL_.IsFINGER(nMT)) chkTOLLER = 0.5;
                double errTOLLER = Math.Abs(cPOS - chkPOS);
                if (errTOLLER > chkTOLLER && !mtCHK[nMT].onlyStart){
                    if (UTIL_.IsFINGER(nMT)){
                        if (errTOLLER > 0.05) bPOS = false;
                        else bPOS = false;
                    }
                }
                if (MTBUSY(nMT) /*&& !mCOM.IsNotEncMOTOR(nMT)*/) bPOS = false;
                if (i >= (tm - 1)){
                    dMaxToller  = errTOLLER;
                    double dCUR = GET_ACTPOS(nMT);
                    double dCMD = GET_CMDPOS(nMT);
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE TIME OVER]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE TIME OVER]" + ETC.CrLf;
                    uint uSTOP  = 0;
                    uint uRTN   = CAXM.AxmStatusReadStop(nMT, ref uSTOP);
                    errTOLLER   = Math.Abs(dCUR - chkPOS);
                    LogWR_.SAVE_MOVING_ERROR_LOG(nMT, uSTOP.ToString());
                    if (errTOLLER > chkTOLLER && mtCHK[nMT].onlyStart){
                        UTIL_.OnERROR_MOTION(nMT, eTimeOver, 500);
                        return eRTN.FAIL;
                    }
                    else if (errTOLLER < 0.02){
                        bPOS = true;
                        break;
                    }
                }
                //if (MTBUSY(nMT)) bPOS = false;
                for (int k = 0; k < cMT; k++){
                    int n           = m[k];
                    double p        = mtCHK[n].pos;
                    double eToller  = Math.Abs(GET_ACTPOS(n) - p);
                    double cToller  = 0.3;//mtCHK[n].toller;
                    if (eToller > cToller || MTBUSY(n)) bPOS = false;
                }
            }
            if (bPOS) break;
        }
        for (int i = 0; i < cMT; i++){
            int mt              = m[i];
            mtCHK[mt].sts       += "MOVE END" + ETC.CrLf;
            mtCHK[mt].eTime     = Environment.TickCount;
            mtCHK[mt].posStop   = GET_ACTPOS(mt);
            mtCHK[mt].posGap    = Math.Abs(mtCHK[mt].posStop - mtCHK[mt].pos);
            mtCHK[mt].rTime     = (mtCHK[mt].eTime - mtCHK[mt].sTime) / 1000;
        }
        if (!bPOS){
            for (int i = 0; i < cMT; i++){
                int mt          = m[i];
                string sERR     = "FAIL RUN TIME = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Gap = " + string.Format("{0:0.000}", mtCHK[mt].posGap);
                mtCHK[mt].sts   += sERR + ETC.CrLf;
                mtCHK[mt].rslt  = GET_MOVE_RESULT(mt);
            }
            UTIL_.DELAY(500);
            return eRTN.FAIL;
        }
        for (int i = 0; i < cMT; i++){
            int mt = m[i];
            mtCHK[mt].posStop   = GET_ACTPOS(mt);
            mtCHK[mt].posGap    = Math.Abs(mtCHK[mt].posStop = mtCHK[mt].pos);
            mtCHK[mt].sts       = "Succes Run Time = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Grap = " + string.Format("{0:0.000}", mtCHK[mt].posGap) + ETC.CrLf;
            mtCHK[mt].rslt      = GET_MOVE_RESULT(mt);
        }
        return eRTN.SUCESS; ; // Move Success !
    }

    //같은 보드내에 있는 축 - 연속 보간 구동 함수
    public static eRTN MUTI_LINE_MOVE(int[] m, stMoveInfo[] mv, double spd, double acc, double dec, int MoveTime, double toller, bool onlyStart, bool NoChange, string cmd, string comment){
        int cMT = m.Length;
        for (int i = 0; i < m.Length; i++){
            int mt = m[i];
            CLEAR_MOVECHKECK(mt, mv[i]);
            mtCHK[mt].cmd       = cmd;
            mtCHK[mt].coment    = comment + " -> MOVE";
            mtCHK[mt].Ev        = "";
            if (!PRE_MOVE_CHECK(mt)){
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "PRE_MOVE_CHECK() FAIL !");
                mtCHK[mt].errLog = "PRE_MOVE_CHECK() FAIL" + ETC.CrLf;
                return eRTN.FAIL;
            }
            else mtCHK[mt].sts = "PRE_MOVE_CHECK() -> COMPLETE" + ETC.CrLf;

            if (MTBUSY(mt)){
                MTSSTOP(mt, "BUSY STOP");
                UTIL_.DELAY(100);
                mtCHK[mt].errLog += "BUSY STOP" + ETC.CrLf;
            }
            mtCHK[mt].sts = "MTBUSY -> COMPLETE" + ETC.CrLf;
            SET_MOVE_MODE(mt, 0); // 절대좌표 이송
        }

        double dRUN_RATE = dRunRate;
        if (dRUN_RATE > 100)    dRUN_RATE = 100;
        if (dRUN_RATE < 1)      dRUN_RATE = 1;
        if (!NoChange && spd > 50){
            spd         *= (dRUN_RATE / 100);
            acc         *= (dRUN_RATE / 100);
            dec         *= (dRUN_RATE / 100);
            MoveTime    *= (int)(100 / dRUN_RATE);
        }

        //동일 위치 확인
        bool bChkPos = true;
        for (int i = 0; i < cMT; i++){
            double dCUR         = GET_ACTPOS(m[i]);
            double dMOVE_POS    = mtCHK[m[i]].pos;
            double ChkToller    = Math.Abs(dCUR - dMOVE_POS);
            if (ChkToller > toller) bChkPos = false;
        }
        if (bChkPos) goto END_MOVING;

        uint dwEND;
        int[] nAXIS     = new int[cMT];
        double[] dPOS   = new double[cMT];
        for (int i = 0; i < cMT; i++){
            nAXIS[i]    = m[i];
            dPOS[i]     = mv[i].Pos;
            if (MTBUSY(m[i])) MTSSTOP(m[i], "BUSY STOP");
            mtOPTION[m[i]].DontStop = true;
        }

        //소프트 리미트 위치 확인
        for (int i = 0; i < cMT; i++){
            if (mtSoftData[m[i]].CwSoftLimit < mtCHK[m[i]].pos){
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]");
                mtCHK[m[i]].errLog += "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m[i], eCwSoftLime, 500);
                return eRTN.FAIL;
            }
            if (mtSoftData[m[i]].CcwSoftLimit > mtCHK[m[i]].pos){
                for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]");
                mtCHK[m[i]].errLog += "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m[i], eCcwSoftLime, 500);
                return eRTN.FAIL;
            }
        }

        //멀티 라인 구동 함수 !
        SET_PROFILE_MODE(nAXIS, /*(uint)AXT_MOTION_PROFILE_MODE.ASYM_TRAPEZOIDE_MODE*/(uint)AXT_MOTION_PROFILE_MODE.ASYM_S_CURVE_MODE);
        dwEND = MOVE_MUTI_LINE_POS(nAXIS, dPOS, spd, acc, dec, 0); // 절대좌표 이송
        if (0 != dwEND){
            for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
            MT_ARRAY_STOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "MUTI_MOVE_IN_EACH_BOARD() FAIL !");
            mtCHK[nAXIS[0]].errLog = "MT_ARRAY_STOP" + ETC.CrLf + "MUTI_MOVE_IN_EACH_BOARD() FAIL !";
            UTIL_.OnERROR_MOTION(nAXIS[0], eMotMOVE, 500);
            return eRTN.FAIL;
        }
    END_MOVING:
        //이송 완료 후 상태 확인
        if (onlyStart) return eRTN.SUCESS;
        bool bPOS = true;
        double dMaxToller;
        for (int i = 0; i < MoveTime; i++){
            if (bBD){
                UTIL_.DELAY(100);
                for (int j = 0; j < cMT; j++){
                    int nMT = m[j];
                    mtSTS[nMT].CurrentPosition  = mtCHK[nMT].pos;
                    mtSTS[nMT].CmdPosition      = mtCHK[nMT].pos;
                    mtOPTION[nMT].DontStop      = false;
                }
                return eRTN.SUCESS;
            }
            UTIL_.DELAY(1);
            bPOS = true;
            for (int j = 0; j < cMT; j++){
                int nMT = m[j];
                if (mtSTS[nMT].bSensorCW){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE +LIMIT STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE +LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eLimitP, 500);
                    return eRTN.FAIL;
                }
                if (mtSTS[nMT].bSensorCCW){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE -LIMIT STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE -LIMIT STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eLimitM, 500);
                    return eRTN.FAIL;
                }
                if (mtSTS[nMT].bAlram){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE ALARM STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE ALARM STOP]" + ETC.CrLf;
                    UTIL_.OnERROR_MOTION(nMT, eALARM, 500);
                    return eRTN.FAIL;
                }

                if (mtCHK[nMT].Ev == "STOP" || mtCHK[nMT].Ev == "stop"){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;

                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [PUSH STOP SWITCH]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [PUSH STOP SWITCH]" + ETC.CrLf;
                    return eRTN.FAIL;
                }
                if (mtCHK[nMT].Ev == "ERR" || mtCHK[nMT].Ev == "err"){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [ERROR STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [ERROR STOP]" + ETC.CrLf;
                    return eRTN.FAIL;
                }
                if (mtCHK[nMT].Ev == "EMS" || mtCHK[nMT].Ev == "ems"){
                    for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [EMS STOP]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [EMS STOP]" + ETC.CrLf;
                    return eRTN.FAIL;
                }

                double cPOS         = GET_ACTPOS(nMT);
                double chkPOS       = mtCHK[nMT].pos;
                double chkTOLLER    = toller;
                if (UTIL_.IsFINGER(nMT)) chkTOLLER = 0.5;
                double errTOLLER = Math.Abs(cPOS - chkPOS);
                if (errTOLLER > chkTOLLER && !mtCHK[nMT].onlyStart){
                    if (UTIL_.IsFINGER(nMT)){
                        if (errTOLLER > 0.05) bPOS = false;
                        else bPOS = false;
                    }
                }
                if (MTBUSY(nMT)/* && !mCOM.IsNotEncMOTOR(nMT)*/) bPOS = false;
                if (i >= (MoveTime - 1)){
                    dMaxToller = errTOLLER;
                    double dCUR = GET_ACTPOS(nMT);
                    double dCMD = GET_CMDPOS(nMT);
                    MT_ARRAY_STOP(m, "MT_ARRAY_STOP [MOVE TIME OVER]");
                    mtCHK[nMT].errLog += "MT_ARRAY_STOP [MOVE TIME OVER]" + ETC.CrLf;
                    uint uSTOP  = 0;
                    uint uRTN   = CAXM.AxmStatusReadStop(nMT, ref uSTOP);
                    errTOLLER   = Math.Abs(dCUR - chkPOS);
                    LogWR_.SAVE_MOVING_ERROR_LOG(nMT, uSTOP.ToString());
                    if (errTOLLER > chkTOLLER && mtCHK[nMT].onlyStart){
                        for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
                        UTIL_.OnERROR_MOTION(nMT, eTimeOver, 500);
                        return eRTN.FAIL;
                    }
                    else if (errTOLLER < 0.02){
                        bPOS = true;
                        break;
                    }
                }
                //if (MTBUSY(nMT)) bPOS = false;
                for (int k = 0; k < cMT; k++){
                    int n           = m[k];
                    double p        = mtCHK[n].pos;
                    double eToller  = Math.Abs(GET_ACTPOS(n) - p);
                    double cToller  = 0.3;// mtCHK[n].toller;
                    if (eToller > cToller || MTBUSY(n)) bPOS = false;
                }
            }
            if (bPOS) break;
        }
        for (int i = 0; i < cMT; i++){
            int mt = m[i];
            mtCHK[mt].sts       += "MOVE END" + ETC.CrLf;
            mtCHK[mt].eTime     = Environment.TickCount;
            mtCHK[mt].posStop   = GET_ACTPOS(mt);
            mtCHK[mt].posGap    = Math.Abs(mtCHK[mt].posStop - mtCHK[mt].pos);
            mtCHK[mt].rTime     = (mtCHK[mt].eTime - mtCHK[mt].sTime) / 1000;
        }
        if (!bPOS){
            for (int i = 0; i < cMT; i++){
                int mt          = m[i];
                string sERR     = "FAIL RUN TIME = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Gap = " + string.Format("{0:0.000}", mtCHK[mt].posGap);
                mtCHK[mt].sts   += sERR + ETC.CrLf;
                mtCHK[mt].rslt  = GET_MOVE_RESULT(mt);
            }
            for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
            UTIL_.DELAY(500);
            return eRTN.FAIL;
        }
        for (int i = 0; i < cMT; i++){
            int mt              = m[i];
            mtCHK[mt].posStop   = GET_ACTPOS(mt);
            mtCHK[mt].posGap    = Math.Abs(mtCHK[mt].posStop = mtCHK[mt].pos);
            mtCHK[mt].sts       = "Succes Run Time = " + string.Format("{0:0.000}", mtCHK[mt].rTime) + " Sec , Grap = " + string.Format("{0:0.000}", mtCHK[mt].posGap) + ETC.CrLf;
            mtCHK[mt].rslt      = GET_MOVE_RESULT(mt);
        }
        for (int k = 0; k < cMT; k++) mtOPTION[m[k]].DontStop = false;
        return eRTN.SUCESS; ; // Move Success !
    }

    //속도 오버라이드 구동.
    public static eRTN MOVE_SPEED_OVERRIDE(int tn, int m, stMoveInfo mv, double overPos, double overSpd, int MoveTime, double toller, bool onlyStart, string cmd, string comment){
        CLEAR_MOVECHKECK(m, mv);
        mtCHK[m].cmd        = cmd;
        mtCHK[m].coment     = comment + " -> MOVE";
        mtCHK[m].toller     = toller;
        mtCHK[m].onlyStart  = onlyStart;
        mtCHK[m].noChange   = true;
        mtCHK[m].Ev         = "";

        if (!PRE_MOVE_CHECK(m)){
            MTSSTOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "PRE_MOVE_CHECK() FAIL !");
            mtCHK[m].errLog = "PRE_MOVE_CHECK() FAIL" + ETC.CrLf;
            return eRTN.FAIL;
        }
        else mtCHK[m].sts = "PRE_MOVE_CHECK() -> COMPLETE" + ETC.CrLf;
        if (MTBUSY(m)){
            MTSSTOP(m, "BUSY STOP");
            UTIL_.DELAY(100);
            mtCHK[m].errLog += "BUSY STOP" + ETC.CrLf;
        }
        mtCHK[m].sts = "MTBUSY -> COMPLETE" + ETC.CrLf;

        SET_MOVE_MODE(m, 0); // 절대좌표 이송
        double dRUN_RATE = dRunRate;
        if (dRUN_RATE > 100)    dRUN_RATE = 100;
        if (dRUN_RATE < 1)      dRUN_RATE = 1;
        if (!mtCHK[m].noChange && mtCHK[m].spd > 50){
            mtCHK[m].spd    = mtCHK[m].spd * (dRUN_RATE / 100);
            mtCHK[m].acc    = mtCHK[m].acc * (dRUN_RATE / 100);
            mtCHK[m].dcc    = mtCHK[m].dcc * (dRUN_RATE / 100);
            mtCHK[m].time   = mtCHK[m].time * (int)(100 / dRUN_RATE);
        }

        //소프트 리미트 위치 확인
        if (mtSoftData[m].CwSoftLimit < mtCHK[m].pos){
            mtOPTION[m].DontStop = false;
            MTSSTOP(m, "MT_STOP [MOVE CW SOFT LIMIT STOP]");
            mtCHK[m].errLog += "MT_ARRAY_STOP [MOVE CW SOFT LIMIT STOP]" + ETC.CrLf;
            UTIL_.OnERROR_MOTION(m, eCwSoftLime, 500);
            return eRTN.FAIL;
        }
        if (mtSoftData[m].CcwSoftLimit > mtCHK[m].pos){
            mtOPTION[m].DontStop = false;
            MTSSTOP(m, "MT_STOP [MOVE CCW SOFT LIMIT STOP]");
            mtCHK[m].errLog += "MT_ARRAY_STOP [MOVE CCW SOFT LIMIT STOP]" + ETC.CrLf;
            UTIL_.OnERROR_MOTION(m, eCcwSoftLime, 500);
            return eRTN.FAIL;
        }

        double dPOS         = GET_ACTPOS(m);
        mtCHK[m].posBegin   = dPOS;
        double gPOS         = Math.Abs(dPOS - mtCHK[m].pos);
        double ShortSpd     = mtSoftData[m].ShortSpeed < 10 ? 10 : mtSoftData[m].ShortSpeed;
        uint dwEND          = 0;
        if (gPOS < mtSoftData[m].ShortLength && mtCHK[m].spd > ShortSpd){
            mtCHK[m].spd = ShortSpd;
            mtCHK[m].sts += "SPEED ADJUST -> COMPLETE" + ETC.CrLf;

            for (int j = 0; j < 100; j++){
                if (bBD){
                    dwEND = 0;
                    break;
                }
                if (MTBUSY(m)){
                    MTSSTOP(m, "BUSY STOP");
                    UTIL_.DELAY(100);
                }
                SET_PROFILE_MODE(m, /*(uint)AXT_MOTION_PROFILE_MODE.ASYM_TRAPEZOIDE_MODE*/(uint)AXT_MOTION_PROFILE_MODE.ASYM_S_CURVE_MODE);
                dwEND = MOVE_START(m, mtCHK[m].pos, mtCHK[m].spd, mtCHK[m].acc, mtCHK[m].dcc);
                if (dwEND == 0){
                    mtCHK[m].sts += "MTMOVE -> COMPLETE" + ETC.CrLf;
                    UTIL_.DELAY(10);
                    break;
                }
            }
        }//짧은 거리면 속도 오버라이드 안하고 바로 StartMove함수로~
        else{
            mtCHK[m].sts += "SPEED ADJUST -> COMPLETE" + ETC.CrLf;

            for (int j = 0; j < 100; j++){
                if (bBD){
                    dwEND = 0;
                    break;
                }
                if (MTBUSY(m)){
                    MTSSTOP(m, "BUSY STOP");
                    UTIL_.DELAY(100);
                }
                SET_PROFILE_MODE(m, /*(uint)AXT_MOTION_PROFILE_MODE.ASYM_TRAPEZOIDE_MODE*/(uint)AXT_MOTION_PROFILE_MODE.ASYM_S_CURVE_MODE);
                dwEND = MOVE_OVERRIDE_VEL(m, mtCHK[m].pos, mtCHK[m].spd, mtCHK[m].acc, mtCHK[m].dcc, overPos, overSpd, (int)AXT_MOTION_SELECTION.COMMAND); //1231
                if (dwEND == 0){
                    mtCHK[m].sts += "MTMOVE -> COMPLETE" + ETC.CrLf;
                    UTIL_.DELAY(10);
                    break;
                }
            }
        }//속도 오버라이드 적용
        if (0 != dwEND){
            mtOPTION[m].DontStop = false;
            MTESTOP(m, "MT_ARRAY_STOP" + ETC.CrLf + "MTMOVE() FAIL !");
            mtCHK[m].errLog = "MT_STOP" + ETC.CrLf + "MTMOVE() FAIL !";
            //mLogWR.DEBUG_PRINT("MTMOVE FAILED -> dwEMD = " + dwEND.ToString());
            if (dwEND == 4152){ //구동 중 다른 명령 들어가면.
            }
            MTESTOP(m, mtCHK[m].errLog);
            UTIL_.OnERROR_MOTION(m, eMotMOVE, 500);
            return eRTN.FAIL;
        }

        //이송 완료 후 상태 확인
        bool bOnlyStart = true;
        if (!mtCHK[m].onlyStart) bOnlyStart = false;
        if (bOnlyStart) return eRTN.SUCESS;

        int tm      = mtCHK[m].time;
        bool bPOS   = true;
        double dMaxToller;
        for (int i = 0; i < tm; i++){
            if (bBD){
                UTIL_.DELAY(100);
                mtSTS[m].CurrentPosition    = mtCHK[m].pos;
                mtSTS[m].CmdPosition        = mtCHK[m].pos;
                mtOPTION[m].DontStop        = false;
                return eRTN.SUCESS;
            }
            UTIL_.DELAY(1);
            bPOS = true;

            if (mtSTS[m].bSensorCW){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [MOVE +LIMIT STOP]");
                mtCHK[m].errLog += "MT_STOP [MOVE +LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m, eLimitP, 500);
                return eRTN.FAIL;
            }
            if (mtSTS[m].bSensorCCW){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [MOVE -LIMIT STOP]");
                mtCHK[m].errLog += "MT_STOP [MOVE -LIMIT STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m, eLimitM, 500);
                return eRTN.FAIL;
            }
            if (mtSTS[m].bAlram){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [MOVE ALARM STOP]");
                mtCHK[m].errLog += "MT_STOP [MOVE ALARM STOP]" + ETC.CrLf;
                UTIL_.OnERROR_MOTION(m, eALARM, 500);
                return eRTN.FAIL;
            }

            if (mtCHK[m].Ev == "STOP" || mtCHK[m].Ev == "stop" || bPushStop){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [PUSH STOP SWITCH]");
                mtCHK[m].errLog += "MT_STOP [PUSH STOP SWITCH]" + ETC.CrLf;
                return eRTN.FAIL;
            }
            if (mtCHK[m].Ev == "ERR" || mtCHK[m].Ev == "err"){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [ERROR STOP]");
                mtCHK[m].errLog += "MT_STOP [ERROR STOP]" + ETC.CrLf;
                return eRTN.FAIL;
            }
            if (mtCHK[m].Ev == "EMS" || mtCHK[m].Ev == "ems"){
                mtOPTION[m].DontStop = false;
                MTESTOP(m, "MT_STOP [EMS STOP]");
                mtCHK[m].errLog += "MT_STOP [EMS STOP]" + ETC.CrLf;
                return eRTN.FAIL;
            }

            double cPOS         = GET_ACTPOS(m);
            double chkPOS       = mtCHK[m].pos;
            double chkTOLLER    = mtCHK[m].toller;
            if (UTIL_.IsFINGER(m)) chkTOLLER = 0.5;
            double errTOLLER = Math.Abs(cPOS - chkPOS);
            if (errTOLLER > chkTOLLER && !mtCHK[m].onlyStart){
                if (UTIL_.IsFINGER(m)){
                    if (errTOLLER > 0.05)   bPOS = false;
                    else                    bPOS = true;
                }
            }
            if (MTBUSY(m) /*&& !mCOM.IsNotEncMOTOR(m)*/) bPOS = false;
            if (i >= (tm - 1)){
                dMaxToller  = errTOLLER;
                double dCUR = GET_ACTPOS(m);
                double dCMD = GET_CMDPOS(m);
                MTESTOP(m, "MT_STOP [MOVE TIME OVER]");
                mtCHK[m].errLog += "MT_STOP [MOVE TIME OVER]" + ETC.CrLf;
                uint uSTOP  = 0;
                uint uRTN   = CAXM.AxmStatusReadStop(m, ref uSTOP);
                errTOLLER   = Math.Abs(dCUR - chkPOS);
                LogWR_.SAVE_MOVING_ERROR_LOG(m, uSTOP.ToString());
                if (errTOLLER > chkTOLLER && !mtCHK[m].onlyStart){
                    mtOPTION[m].DontStop = false;
                    UTIL_.OnERROR_MOTION(m, eTimeOver, 500);
                    return eRTN.FAIL;
                }
                else if (errTOLLER < 0.01){
                    bPOS = true;
                    break;
                }
            }
            double p        = mtCHK[m].pos;
            double eToller  = Math.Abs(GET_ACTPOS(m) - p);
            double cToller  = 0.3;// mtCHK[m].toller;
            if (eToller > cToller || MTBUSY(m)) bPOS = false;
            if (bPOS) break;
        }

        mtCHK[m].sts        += "MOVE END" + ETC.CrLf;
        mtCHK[m].eTime      = Environment.TickCount;
        mtCHK[m].posStop    = GET_ACTPOS(m);
        mtCHK[m].posGap     = Math.Abs(mtCHK[m].posStop - mtCHK[m].pos);
        mtCHK[m].rTime      = (mtCHK[m].eTime - mtCHK[m].sTime) / 1000;

        if (!bPOS){ // 구동실패.
            string sERR             = "FAIL RUN TIME = " + string.Format("{0:0.000}", mtCHK[m].rTime) + " Sec , Gap = " + string.Format("{0:0.000}", mtCHK[m].posGap);
            mtCHK[m].sts            += sERR + ETC.CrLf;
            mtCHK[m].rslt           = GET_MOVE_RESULT(m);
            mtOPTION[m].DontStop    = false;
            UTIL_.DELAY(500);
            return eRTN.FAIL;
        }

        mtCHK[m].posStop        = GET_ACTPOS(m);
        mtCHK[m].posGap         = Math.Abs(mtCHK[m].posStop - mtCHK[m].pos);
        mtCHK[m].sts            = "Succes Run Time = " + string.Format("{0:0.000}", mtCHK[m].rTime) + " Sec , Grap = " + string.Format("{0:0.000}", mtCHK[m].posGap) + ETC.CrLf;
        mtCHK[m].rslt           = GET_MOVE_RESULT(m);
        mtOPTION[m].DontStop    = false;
        return eRTN.SUCESS;
    }
    #endregion "MOTION"
}