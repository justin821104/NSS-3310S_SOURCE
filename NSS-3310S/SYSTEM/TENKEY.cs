using NSS_3310S;
using Object;
using System;

namespace nTENKEY{
    public class TENKEY : DATA_{
        public static void TenkeyStatus() { READ(); }

        static void READ(){
            if (eMCStatus == eMachineStatus.AUTO || eMCStatus == eMachineStatus.INITIAL || IsBIT[B.TENKEY_JOG]){
                //for (short i = cDO.oTENKEY1; i < cDO.oTENKEY9; i++) mLAB.BIT_OUT(i, false);
                return;
            }
            DownCheck();
            UpCheck();

            if (!mKeyInputEnd) return;
            mKeyInputEnd = false;

            if (mKeyResult == "*") SET_COMMAND("", "000");
            if (mKeyResult != "#") SET_COMMAND(mBuf_KEY + mKeyResult, "");
            if (DECODEKEY()){
                BINARY_TWO(Convert.ToInt16(mBuf_KEY.Substring(2, 1)));
                BINARY_ONE(Convert.ToInt16(mBuf_KEY.Substring(1, 1)));
                LAB_.BIT_OUT(O.TENKEY9, Convert.ToInt16(mBuf_KEY) > 100 ? true : false);
            }
            if (mBuf_KEY != "" && mKeyResult == "#" && eMCStatus != eMachineStatus.AUTO && eMCStatus != eMachineStatus.INITIAL && !IsBIT[B.TENKEY_JOG]) ManualTenkeyOperation(Convert.ToInt16(mBuf_KEY));
        }

        public static void GetKeyValue(short mVal){
            if (mVal == 11) mKeyResult = "*";
            else if (mVal == 12) mKeyResult = "#";
            else mKeyResult = mKeyResult = mVal == 10 ? "0" : Convert.ToString(mVal); // Convert.ToString(mVal); // mKeyResult = mVal == 10 ? "0" : Convert.ToString(mVal);

            //System.Diagnostics.Trace.WriteLine(Convert.ToString(mVal));
            mKeyPress[mVal] = false;
            mKeyInputEnd = true;
        }

        public static void DownCheck(){
            if (!mKeyPress[0] && mIN[I.TENKEY4] && mIN[I.TENKEY6]) mKeyPress[0] = true;
            else if (!mKeyPress[1] && mIN[I.TENKEY1] && mIN[I.TENKEY5]) mKeyPress[1] = true;
            else if (!mKeyPress[2] && mIN[I.TENKEY1] && mIN[I.TENKEY6]) mKeyPress[2] = true;
            else if (!mKeyPress[3] && mIN[I.TENKEY1] && mIN[I.TENKEY7]) mKeyPress[3] = true;
            else if (!mKeyPress[4] && mIN[I.TENKEY2] && mIN[I.TENKEY5]) mKeyPress[4] = true;
            else if (!mKeyPress[5] && mIN[I.TENKEY2] && mIN[I.TENKEY6]) mKeyPress[5] = true;
            else if (!mKeyPress[6] && mIN[I.TENKEY2] && mIN[I.TENKEY7]) mKeyPress[6] = true;
            else if (!mKeyPress[7] && mIN[I.TENKEY3] && mIN[I.TENKEY5]) mKeyPress[7] = true;
            else if (!mKeyPress[8] && mIN[I.TENKEY3] && mIN[I.TENKEY6]) mKeyPress[8] = true;
            else if (!mKeyPress[9] && mIN[I.TENKEY3] && mIN[I.TENKEY7]) mKeyPress[9] = true;
            else if (!mKeyPress[11] && mIN[I.TENKEY4] && mIN[I.TENKEY5]) mKeyPress[11] = true;
            else if (!mKeyPress[12] && mIN[I.TENKEY4] && mIN[I.TENKEY7]) mKeyPress[12] = true;

            //if (bit.TENKEY_JOG && !bit.MOVE_JOG && AINON(I_KEY4) && AINON(I_KEY5)) EXCUTE_JIG(true, 10000);
            //else if (bit.TENKEY_JOG && !bit.MOVE_JOG && AINON(I_KEY4) && AINON(I_KEY7)) EXCUTE_JIG(true, -10000);
            //else if (bit.MOVE_JOG && AINOFF(I_KEY4) && AINOFF(I_KEY5) && AINOFF(I_KEY7)) EXCUTE_JIG(false, -10000);
        }
        public static void UpCheck(){
            if (mKeyPress[0] && !mIN[I.TENKEY4] && !mIN[I.TENKEY6]) GetKeyValue(0);
            else if (mKeyPress[1] && !mIN[I.TENKEY1] && !mIN[I.TENKEY5]) GetKeyValue(1);
            else if (mKeyPress[2] && !mIN[I.TENKEY1] && !mIN[I.TENKEY6]) GetKeyValue(2);
            else if (mKeyPress[3] && !mIN[I.TENKEY1] && !mIN[I.TENKEY7]) GetKeyValue(3);
            else if (mKeyPress[4] && !mIN[I.TENKEY2] && !mIN[I.TENKEY5]) GetKeyValue(4);
            else if (mKeyPress[5] && !mIN[I.TENKEY2] && !mIN[I.TENKEY6]) GetKeyValue(5);
            else if (mKeyPress[6] && !mIN[I.TENKEY2] && !mIN[I.TENKEY7]) GetKeyValue(6);
            else if (mKeyPress[7] && !mIN[I.TENKEY3] && !mIN[I.TENKEY5]) GetKeyValue(7);
            else if (mKeyPress[8] && !mIN[I.TENKEY3] && !mIN[I.TENKEY6]) GetKeyValue(8);
            else if (mKeyPress[9] && !mIN[I.TENKEY3] && !mIN[I.TENKEY7]) GetKeyValue(9);
            else if (mKeyPress[11] && !mIN[I.TENKEY4] && !mIN[I.TENKEY5]) GetKeyValue(11);
            else if (mKeyPress[12] && !mIN[I.TENKEY4] && !mIN[I.TENKEY7]) GetKeyValue(12);
        }

        public static void BINARY_TWO(int mVal){
            bool bValue;
            for (short k = O.TENKEY1; k <= O.TENKEY4; k++){
                bValue = !(Convert.ToBoolean(mVal & ((short)Math.Pow(2, k - O.TENKEY1))));
                LAB_.BIT_OUT(k, bValue);
            }
        }
        public static void BINARY_ONE(int mVal){
            bool bValue;
            for (short k = O.TENKEY5; k <= O.TENKEY8; k++){
                bValue = !(Convert.ToBoolean(mVal & ((short)Math.Pow(2, k - O.TENKEY5))));
                LAB_.BIT_OUT(k, bValue);
            }
        }

        public static void ManualTenkeyOperation(short mIndex){
            if (mIndex > 200 || eMCStatus == eMachineStatus.INITIAL || bMF) return;
            TENK_NUM = mIndex;
            mKeyResult = string.Empty;

            //if (mIndex == MN.ALL_HOME) ALL_HOME();
            //if (mIndex == 10){
            //    if (mIN[cDI.iMGZ_CLAMP_UNLOCK] && (!mIN[cDI.iMGZ_CLAMP_LOCK])){
            //        mCOM.RUN_MANUAL(37, "[TENKEY]");
            //    }
            //    else{
            //        mCOM.RUN_MANUAL(38, "[TENKEY]");
            //    }
            //}//37
        }

        public void ALL_HOME(){
            if (eLevelMainSw != eMainLevel.AUTO || eMCStatus == eMachineStatus.AUTO) return;
            mIN[I.vtInitial] = true;
            IsLONG[L.OffNumber] = I.vtInitial;
            bViewInitialStatus = true;
        }
    }
}