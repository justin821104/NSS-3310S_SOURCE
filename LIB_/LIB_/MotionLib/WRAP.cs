using Object;
using System;

public class WRAP_ : DATA_
{
    public static eRTN RunACMotor(int nThread, int[] nERR, int[] OnINPUT, int[] OffINPUT, int[] OnOUTPUT, int[] OffOUTPUT, int delay, double Timeover, string comment){
        long sTIME = Environment.TickCount;
        double eTIME;
        string sResult = "RunTime";

        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment, "START");
        for (int i = 0; i < OnOUTPUT.Length; i++){
            if (OnOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OnOUTPUT[i], true);
        } //ON OUTPUT
        for (int i = 0; i < OffOUTPUT.Length; i++){
            if (OffOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OffOUTPUT[i], false);
        } //OFF OUTPUT
        if (bBD){
            UTIL_.DELAY(1500);
            for (int i = 0; i < OnINPUT.Length; i++){
                if (OnINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? false : true;
            }
            for (int i = 0; i < OffINPUT.Length; i++){
                if (OffINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? true : false;
            }
            goto Sucess;
        }
        int ScanTime = 3;
        for (int i = 0; i < Timeover; i += ScanTime){
            if (gExit){
                for (int j = 0; j < OnOUTPUT.Length; j++){
                    if (OnOUTPUT[j] < 0) continue;
                    LAB_.BIT_OUT((short)OnOUTPUT[j], false);
                } //ON OUTPUT
                return eRTN.FAIL;
            }
            UTIL_.DELAY(ScanTime);

            bool bFLAG = true;
            for (int j = 0; j < OnINPUT.Length; j++){
                if (OnINPUT[j] < 0) continue;
                if (!LAB_.INPUT((short)OnINPUT[j])) bFLAG = false;
            }
            for (int j = 0; j < OffINPUT.Length; j++){
                if (OffINPUT[j] < 0) continue;
                if (LAB_.INPUT((short)OffINPUT[j])) bFLAG = false;
            }

            if (bFLAG){
                UTIL_.DELAY((int)ScanTime);
                for (int j = 0; j < OnOUTPUT.Length; j++){
                    if (OnOUTPUT[j] < 0) continue;
                    LAB_.BIT_OUT((short)OnOUTPUT[j], false);
                } //ON OUTPUT
                goto Sucess;
            }
        }
        for (int j = 0; j < OnOUTPUT.Length; j++){
            if (OnOUTPUT[j] < 0) continue;
            LAB_.BIT_OUT((short)OnOUTPUT[j], false);
        } //ON OUTPUT
        UTIL_.DELAY(10);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "FAIL");
        return eRTN.FAIL;
    Sucess:
        UTIL_.DELAY(delay);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS;
    }

    public static eRTN RunCylinder(int nThread, int[] nERR, int[] OnINPUT, int[] OffINPUT, int[] OnOUTPUT, int[] OffOUTPUT, double ScanTime, int nDelay, string comment){
        long sTIME = Environment.TickCount;
        double eTIME;
        string sResult = "RunTime";

        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment, "START");
        for (int i = 0; i < OnOUTPUT.Length; i++){
            if (OnOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OnOUTPUT[i], true);
        } //ON OUTPUT
        for (int i = 0; i < OffOUTPUT.Length; i++){
            if (OffOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OffOUTPUT[i], false);
        } //OFF OUTPUT

        if (bBD){
            UTIL_.DELAY(1500);
            for (int i = 0; i < OnINPUT.Length; i++){
                if (OnINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? false : true;
            }
            for (int i = 0; i < OffINPUT.Length; i++){
                if (OffINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? true : false;
            }
            goto Sucess;
        }

        ScanTime *= 1000;
        for (int i = 0; i < prMACHINE[CYLINDER_OVERTIME]; i += (int)ScanTime){
            if (gExit) return eRTN.FAIL;
            UTIL_.DELAY((int)ScanTime);

            bool bFLAG = true;
            for (int j = 0; j < OnINPUT.Length; j++){
                if (OnINPUT[j] < 0) continue;
                if (!LAB_.INPUT((short)OnINPUT[j])) bFLAG = false;
            }
            for (int j = 0; j < OffINPUT.Length; j++){
                if (OffINPUT[j] < 0) continue;
                if (LAB_.INPUT((short)OffINPUT[j])) bFLAG = false;
            }
            if (bFLAG) goto Sucess;
        }

        for (int i = 0; i < OnINPUT.Length; i++){
            if (OnINPUT[i] < 0) continue;
            if (!LAB_.INPUT((short)OnINPUT[i]) && nERR.Length - 1 >= i) UTIL_.OnERROR(nERR[i], 100);
        }
        for (int i = 0; i < OffINPUT.Length; i++){
            if (OffINPUT[i] < 0) continue;
            if (LAB_.INPUT((short)OffINPUT[i]) && nERR.Length - 1 >= i) UTIL_.OnERROR(nERR[i], 100);
        }
        UTIL_.DELAY(10);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "FAIL");
        return eRTN.FAIL;
    Sucess:
        UTIL_.DELAY(nDelay);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS;
    }

    public static eRTN RunCylinder(int nThread, int[] nERR, int[] OnINPUT, int[] OffINPUT, int[] OnOUTPUT, int[] OffOUTPUT, int nDelay, string comment){
        long sTIME = Environment.TickCount;
        double eTIME;
        string sResult = "RunTime";

        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment, "START");
        for (int i = 0; i < OnOUTPUT.Length; i++){
            if (OnOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OnOUTPUT[i], true);
        } //ON OUTPUT
        for (int i = 0; i < OffOUTPUT.Length; i++){
            if (OffOUTPUT[i] < 0) continue;
            LAB_.BIT_OUT((short)OffOUTPUT[i], false);
        } //OFF OUTPUT
        if (bBD){
            UTIL_.DELAY(1500);
            for (int i = 0; i < OnINPUT.Length; i++){
                if (OnINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? false : true;
            }
            for (int i = 0; i < OffINPUT.Length; i++){
                if (OffINPUT[i] < 0) continue;
                int iNUM = OnINPUT[i];
                mIN[iNUM] = (chkIN[iNUM].ContactB) ? true : false;
            }
            goto Sucess;
        }

        for (int i = 0; i < prMACHINE[CYLINDER_OVERTIME]; i += 2){
            if (gExit) return eRTN.FAIL;
            UTIL_.DELAY(2);

            bool bFLAG = true;
            for (int j = 0; j < OnINPUT.Length; j++){
                if (OnINPUT[j] < 0) continue;
                if (!LAB_.INPUT((short)OnINPUT[j])) bFLAG = false;
            }
            for (int j = 0; j < OffINPUT.Length; j++){
                if (OffINPUT[j] < 0) continue;
                if (LAB_.INPUT((short)OffINPUT[j])) bFLAG = false;
            }
            if (bFLAG) goto Sucess;
        }

        for (int i = 0; i < OnINPUT.Length; i++){
            if (OnINPUT[i] < 0) continue;
            if (!LAB_.INPUT((short)OnINPUT[i]) && nERR.Length - 1 >= i){
                UTIL_.OnERROR(nERR[i], 100);
                goto Fail;
            }
        }
        for (int i = 0; i < OffINPUT.Length; i++){
            if (OffINPUT[i] < 0) continue;
            if (LAB_.INPUT((short)OffINPUT[i]) && nERR.Length - 1 >= i){
                UTIL_.OnERROR(nERR[i], 100);
                goto Fail;
            }
        }
    Fail:
        UTIL_.DELAY(10);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "FAIL");
        return eRTN.FAIL;
    Sucess:
        UTIL_.DELAY(nDelay);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS;
    }

    public static eRTN RunCylinder(int nThread, int nERR, int OnINPUT, int OffINPUT, int OnOUTPUT, int OffOUTPUT, int nDELAY, string comment){
        long sTIME = Environment.TickCount;
        double eTIME;
        string sResult = "RunTime";

        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment, "START");
        //OUPUT
        if (OnOUTPUT > -1)  LAB_.BIT_OUT((short)OnOUTPUT, true);
        if (OffOUTPUT > -1) LAB_.BIT_OUT((short)OffOUTPUT, false);

        if (bBD){
            UTIL_.DELAY(1500);
            if (OnINPUT > -1){
                mIN[OnINPUT] = (chkIN[OnINPUT].ContactB) ? false : true;
            }
            if (OffINPUT > -1){
                mIN[OffINPUT] = (chkIN[OffINPUT].ContactB) ? true : false;
            }
            goto Sucess;
        }

        //CHECK INPUT
        for (int i = 0; i < prMACHINE[CYLINDER_OVERTIME]; i += 2){
            if (gExit) return eRTN.FAIL;
            UTIL_.DELAY(1);
            bool bFLAG = true;
            if (OnINPUT > -1){
                if (!LAB_.INPUT((short)OnINPUT)) bFLAG = false;
            }
            if (OffINPUT > -1){
                if (LAB_.INPUT((short)OffINPUT)) bFLAG = false;
            }
            if (bFLAG) goto Sucess;
        }
        if (OnINPUT > -1){
            if (!LAB_.INPUT((short)OnINPUT)){
                UTIL_.OnERROR(nERR, 100);
                goto Fail;
            }
        }
        if (OffINPUT > -1){
            if (LAB_.INPUT((short)OffINPUT)){
                UTIL_.OnERROR(nERR, 100);
                goto Fail;
            }
        }
    Fail:
        UTIL_.DELAY(10);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "FAIL");
        return eRTN.FAIL;
    Sucess:
        UTIL_.DELAY(nDELAY);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS;
    }

    public static eRTN RunPusherFwd(int nThread, int nERR, int nERR_OVERLOAD, int OnINPUT, int OffINPUT, int ChkOverloadINPUT, int OnOUTPUT, int OffOUTPUT, int nDELAY, string comment){
        long sTIME = Environment.TickCount;
        double eTIME;
        string sResult = "RunTime";

        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment, "START");
        //OUPUT
        if (OnOUTPUT > -1)  LAB_.BIT_OUT((short)OnOUTPUT, true);
        if (OffOUTPUT > -1) LAB_.BIT_OUT((short)OffOUTPUT, false);
        if (bBD){
            UTIL_.DELAY(1500);
            if (OnINPUT > -1){
                mIN[OnINPUT] = (chkIN[OnINPUT].ContactB) ? false : true;
            }
            if (OffINPUT > -1){
                mIN[OffINPUT] = (chkIN[OffINPUT].ContactB) ? true : false;
            }
            goto Sucess;
        }

        //CHECK INPUT
        for (int i = 0; i < prMACHINE[CYLINDER_OVERTIME]; i += 2){
            if (gExit) return eRTN.FAIL;
            UTIL_.DELAY(1);
            bool bFLAG = true;
            if (OnINPUT > -1){
                if (!LAB_.INPUT((short)OnINPUT)) bFLAG = false;
            }
            if (OffINPUT > -1){
                if (LAB_.INPUT((short)OffINPUT)) bFLAG = false;
            }
            if (ChkOverloadINPUT > -1){
                if (!LAB_.INPUT((short)ChkOverloadINPUT)){
                    if (OnOUTPUT > -1)  LAB_.BIT_OUT((short)OnOUTPUT, false);
                    if (OffOUTPUT > -1) LAB_.BIT_OUT((short)OffOUTPUT, true);
                    UTIL_.OnERROR(nERR_OVERLOAD, 100);
                    goto Fail;
                }
            }
            if (bFLAG) goto Sucess;
        }
        if (OnINPUT > -1){
            if (!LAB_.INPUT((short)OnINPUT)){
                UTIL_.OnERROR(nERR, 100);
                goto Fail;
            }
        }
        if (OffINPUT > -1){
            if (LAB_.INPUT((short)OffINPUT)){
                UTIL_.OnERROR(nERR, 100);
                goto Fail;
            }
        }

    Fail:
        UTIL_.DELAY(10);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "FAIL");
        return eRTN.FAIL;
    Sucess:
        UTIL_.DELAY(nDELAY);
        eTIME   = (Environment.TickCount - sTIME) / 1000;
        sResult += "(" + string.Format("{0:0.000}", eTIME) + " Sec)";
        LogWR_.SaveMarsLog(nThread, eLogTYPE.FNC, comment + "[" + sResult + "]", "END");
        return eRTN.SUCESS;
    }
    public static double GetOffset(string cmd){
        string[] arr    = cmd.Split(':');
        double offset   = 0;

        for (int i = 0; i < arr.Length; i++){
            string[] sArr = arr[i].Split('=');
            if (sArr[0].IndexOf("offset") > -1) offset = double.Parse(sArr[1]);
        }
        return offset;
    } //구동 위치에 offset 값 적용하려고.
    public static double GetSpeed(string cmd){
        string[] arr    = cmd.Split(':');
        double spd      = 0;

        for (int i = 0; i < arr.Length; i++){
            string[] sArr = arr[i].Split('=');
            if (sArr[0].IndexOf("spd") > -1) spd = double.Parse(sArr[1]);
        }
        return spd;
    }
    public static eRTN MOVE(int nThread, int m, int pos, double toller, bool OnlyStart, bool NoChange, bool DontStop, string cmd, string comment){
        stMoveInfo mv   = GetMoveInfo(m, pos);
        mv.Pos          += GetOffset(cmd);
        double spd      = GetSpeed(cmd);
        if (spd != 0)               mv.Spd = spd;
        if (mv.Acc < mv.Spd * 10)   mv.Acc = mv.Spd * 10;
        if (mv.Dec < mv.Spd * 10)   mv.Dec = mv.Spd * 10;
        if (mv.MoveTime < 5000)     mv.MoveTime = 10000;
        comment += " -> MOVE POS";

        int[] mt            = { m };
        stMoveInfo[] ms     = { mv };
        double[] tollers    = { toller };
        bool[] noChanges    = { NoChange };
        bool[] onlyStarts   = { OnlyStart };
        bool[] dontStop     = { DontStop };
        string[] cmds       = { cmd };
        return LAB_.MOVE(nThread, mt, ms, tollers, onlyStarts, noChanges, dontStop, cmds, comment);
    }
    public static eRTN MOVE(int nThread, int m, stMoveInfo mi, double toller, bool onlystart, bool nochange, bool DontStop, string cmd, string comment){
        mi.Pos      += GetOffset(cmd);
        double spd  = GetSpeed(cmd);
        if (spd != 0) mi.Spd = spd;
        comment += " -> MOVE POS";

        int[] mt            = { m };
        stMoveInfo[] pos    = { mi };
        double[] tollers    = { toller };
        bool[] noChanges    = { nochange };
        bool[] onlyStarts   = { onlystart };
        bool[] dontStop     = { DontStop };
        string[] cmds       = { cmd };
        return LAB_.MOVE(nThread, mt, pos, tollers, onlyStarts, noChanges, dontStop, cmds, comment);
    }

    public static eRTN MUTI_MOVE(int nThread, int[] m, int[] pos, double[] toller, bool[] OnlyStart, bool[] NoChange, bool[] DontStop, string[] cmd, string comment){
        int cMT         = m.Length;
        stMoveInfo[] mi = new stMoveInfo[cMT];
        for (int i = 0; i < cMT; i++){
            int mt      = m[i];
            int nPos    = pos[i];
            mi[i]       = GetMoveInfo(mt, nPos);
            mi[i].Pos   += GetOffset(cmd[i]);
            double spd  = GetSpeed(cmd[i]);
            if (spd != 0) mi[i].Spd = spd;
        }
        comment += "-> MUTI_MOVE";
        return LAB_.MOVE(nThread, m, mi, toller, OnlyStart, NoChange, DontStop, cmd, comment);
    }
    public static eRTN MUTI_MOVE(int nThread, int[] m, stMoveInfo[] mi, double[] toller, bool[] OnlyStart, bool[] NoChange, bool[] DontStop, string[] cmd, string comment){
        int cMT = m.Length;
        for (int i = 0; i < cMT; i++){
            mi[i].Pos   += GetOffset(cmd[i]);
            double spd  = GetSpeed(cmd[i]);
            if (spd != 0) mi[i].Spd = spd;
        }
        comment += "-> MUTI_POS_VALUE_MOVE";
        return LAB_.MOVE(nThread, m, mi, toller, OnlyStart, NoChange, DontStop, cmd, comment);
    }

    //같은보드내 동시구동함수(아진 함수에서 멀티구동, 펄스 출력시점 맞출 수 있다)
    public static eRTN MUTI_MOVE_InEachBoard(int[] m, int[] pos, double[] toller, bool[] OnlyStart, bool[] NoChange, string[] cmd, string comment){
        int cMT         = m.Length;
        stMoveInfo[] mi = new stMoveInfo[cMT];
        for (int i = 0; i < cMT; i++){
            int mt      = m[i];
            int ipos    = pos[i];
            mi[i]       = GetMoveInfo(mt, ipos);
            mi[i].Pos   += GetOffset(cmd[i]);
            double spd  = GetSpeed(cmd[i]);
            if (spd != 0) mi[i].Spd = spd;
        }
        comment += "-> MUTI_MOVE_InEachBoard";
        return LAB_.MUTI_MOVE_IN_EACH_BOARD(m, mi, toller, OnlyStart, NoChange, cmd, comment);
    }
    public static eRTN MUTI_MOVE_InEachBoard(int[] m, stMoveInfo[] mi, double[] toller, bool[] OnlyStart, bool[] NoChange, string[] cmd, string comment){
        int cMT = m.Length;
        for (int i = 0; i < cMT; i++){
            mi[i].Pos   += GetOffset(cmd[i]);
            double spd  = GetSpeed(cmd[i]);
            if (spd != 0) mi[i].Spd = spd;
        }
        comment += "-> MUTI_MOVE_InEachBoard";
        return LAB_.MUTI_MOVE_IN_EACH_BOARD(m, mi, toller, OnlyStart, NoChange, cmd, comment);
    }
}