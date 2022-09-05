using NSS_3310S;
using System;

namespace nAZIN{
    public class MOTOR_STATUS : DATA_{
        public int nThread;
        int StartIndex = 0;
        int EndIndex = CNT_.MT;
        double dHalf = CNT_.MT / 2;

        public void DoReadMotion(){
            do{
                if (gExit) break;
                StartIndex = nThread == 0 ? 0 : (int)Math.Ceiling(dHalf);
                EndIndex = nThread == 0 ? (int)Math.Truncate(dHalf) : CNT_.MT;
                for (int m = StartIndex; m < EndIndex; m++) LAB_.READ_STATUS(m);
                //N3MLIII-CNT2 사용
                for (int c = 0; c < mtTrigger.Length; c++) LAB_.READ_COUNTER(c);
                UTIL_.DELAY(1);
            } while (true);
        }
    }

    public class INFO_MOTOR_LOCATION : DATA_{
        double ChkPos = 0;
        public void DoReadLocation(){
            do{
                if (gExit) break;
                for (int m = 0; m < CNT_.MT; m++) LAB_.CHK_CURRENT_STATUS(m, 2);
                RearTimeCheckPosition();
                UTIL_.DELAY(1);
            } while (true);
        }
        void RearTimeCheckPosition(){
            if (mtDATA[M.ElvZ, P.Recive].Pos - (prMACHINE[CP.ElvUpDownPitch] + 1) < mtSTS[M.ElvZ].CurrentPosition && mtSTS[M.ElvZ].CurrentPosition < mtDATA[M.ElvZ, P.Recive].Pos + (prMACHINE[CP.ElvUpDownPitch] + 1)
              && mtDATA[M.ElvY, P.Recive].bPOS)
            {
                IsBIT[B.ElvLDLocation] = true;
            } // 엘리베이터 로딩 위치에 있음
            else IsBIT[B.ElvLDLocation] = false;
            if (mtDATA[M.ElvZ, P.Give].Pos - (prMACHINE[CP.ElvULDUpDownPitch] + 1) < mtSTS[M.ElvZ].CurrentPosition && mtSTS[M.ElvZ].CurrentPosition < mtDATA[M.ElvZ, P.Give].Pos + (prMACHINE[CP.ElvULDUpDownPitch] + 1)
                && mtDATA[M.ElvY, P.Give].bPOS)
            {
                IsBIT[B.ElvULDLocation] = true;
            } // 엘리베이터 언로딩 위치에 있음
            else IsBIT[B.ElvULDLocation] = false;
            //ElvStripLoading
            if ((mtDATA[M.ElvZ, P.FirstSlot].Pos - 1) < mtSTS[M.ElvZ].CurrentPosition && mtSTS[M.ElvZ].CurrentPosition < (mtDATA[M.ElvZ, P.FirstSlot].Pos + (prMODEL[RP.MGZSlotPitch] * prMODEL[RP.MGZSlotCnt]))
                && mtDATA[M.ElvY, P.FirstSlot].bPOS)
            {
                IsBIT[B.ElvStripLoading] = true;
            } // 엘리베이터 스트립 로딩 위치
            else IsBIT[B.ElvStripLoading] = false;


            //[sawing interface] strip picker 
            //if (mtDATA[M.StripPkX, P.StripPlc].bPOS)    mOUT[O.HANDLER_STRIP_PK_X_PLACE_POS] = true;
            //else                                        mOUT[O.HANDLER_STRIP_PK_X_PLACE_POS] = false;
            // 스트립 피커 X축 다이싱 테이블 플레이스 위치 +/- 2mm 안에 들어와 있을 경우만 on
            if (mtDATA[M.StripPkX, P.StripPlc].Pos - 2 < mtSTS[M.StripPkX].CurrentPosition && mtDATA[M.StripPkX, P.StripPlc].Pos + 2 > mtSTS[M.StripPkX].CurrentPosition){
                mOUT[O.HANDLER_STRIP_PK_X_PLACE_POS] = true;
            }
            else{
                mOUT[O.HANDLER_STRIP_PK_X_PLACE_POS] = false;
            }

            ChkPos = 3;//prMACHINE[CP.StipPkCheckUpPitch] <= 0 ? 1 : prMACHINE[CP.StipPkCheckUpPitch] - 1;
            if ((mtDATA[M.StripPkZ, P.StripPlc].Pos - ChkPos < mtSTS[M.StripPkZ].CurrentPosition))  mOUT[O.HANDLER_STRIP_PK_Z_PLACE_POS] = true;
            else                                                                                    mOUT[O.HANDLER_STRIP_PK_Z_PLACE_POS] = false;

            //[sawing interface] unit picker
            //if (mtDATA[M.UnitPkX, P.UnitPckUp].bPOS)    mOUT[O.HANDLER_UNIT_PK_X_PICKUP_POS] = true;
            //else                                        mOUT[O.HANDLER_UNIT_PK_X_PICKUP_POS] = false;
            if (mtDATA[M.UnitPkX, P.UnitPckUp].Pos - 2 < mtSTS[M.UnitPkX].CurrentPosition && mtDATA[M.UnitPkX, P.UnitPckUp].Pos + 2 > mtSTS[M.UnitPkX].CurrentPosition) {
                mOUT[O.HANDLER_UNIT_PK_X_PICKUP_POS] = true;
            }
            else{
                mOUT[O.HANDLER_UNIT_PK_X_PICKUP_POS] = false;
            }

            ChkPos = 3;//prMACHINE[CP.UnitPkCheckUpPitch] <= 0 ? 1 : prMACHINE[CP.UnitPkCheckUpPitch] - 1;
            if (mtDATA[M.UnitPkZ, P.UnitPckUp].Pos - ChkPos < mtSTS[M.UnitPkZ].CurrentPosition) mOUT[O.HANDLER_UNIT_PK_Z_PICKUP_POS] = true;
            else                                                                                mOUT[O.HANDLER_UNIT_PK_Z_PICKUP_POS] = false;

            //[sawing interface] handler picker interlock
            if (mtDATA[M.StripPkX, P.StripPlc].bPOS){
                if ((mtDATA[M.StripPkZ, P.StripPlc].Pos - 5) < mtSTS[M.StripPkZ].CurrentPosition)   mOUT[O.HANDLER_PICKER_Z_INTERLOCK] = true;
                else                                                                                mOUT[O.HANDLER_PICKER_Z_INTERLOCK] = false;
            }
            else if (mtDATA[M.UnitPkX, P.UnitPckUp].bPOS){
                if ((mtDATA[M.UnitPkX, P.UnitPckUp].Pos - 10) < mtSTS[M.UnitPkZ].CurrentPosition)   mOUT[O.HANDLER_PICKER_Z_INTERLOCK] = true;
                else                                                                                mOUT[O.HANDLER_PICKER_Z_INTERLOCK] = false;
            }
            else mOUT[O.HANDLER_PICKER_Z_INTERLOCK] = false;

        }
    }

    public class HOMMING_STATUS : DATA_{
        public void DoReadHomming(){
            do{
                if (gExit) break;
                UTIL_.DELAY(3);
                for (int m = 0; m < CNT_.MT; m++){
                    LAB_.HOMEING_STATUS(m);
                }
            } while (true);
        }
    }

    public class INPUT_STATUS : DATA_{
        public void DoReadInput(){
            do{
                if (gExit) break;
                LAB_.READ_INPUT();
                UTIL_.DELAY(3);
            } while (true);
        }
    }

    public class OUTPUT_STATUS : DATA_{
        public void DoWirteOutput(){
            do{
                if (gExit) break;
                LAB_.WRITE_OUTPUT();
                UTIL_.DELAY(3);
            } while (true);
        }
    }

    public class PK_VACUUM_STATUS : DATA_{
#if _NSS3300
        readonly int[] INPUT    = { I.X1_VAC8, I.X2_VAC1 };
        readonly int[] OUTPUT1  = { O.X1_VAC8, O.X2_VAC1 };//{ O.X1_VAC8, O.X2_VAC1 };
        readonly int[] OUTPUT2  = { O.X1_BLOW8, O.X2_BLOW1 };//{ O.X1_BLOW8, O.X2_BLOW1 };

#else
        readonly int[] INPUT     = { I.X1_VAC1, I.X2_VAC1 };
        readonly int[] OUTPUT1   = { O.X1_BLOW1, O.X2_BLOW1 };
        readonly int[] OUTPUT2   = { O.X1_VAC1, O.X2_VAC1 };
#endif

        int iMODULE, iOFFSET, oOFFSET, no, iOUT_1, iOUT_2, nIN;
        uint uReadWord, sum, temp = 0;
        double Conversion_AD, Set_AD, dAI_VALUE = 0;
        int SetOUT1, SetOUT2 = 0;

        public void Do(){
            do{
                if (gExit) break;
                AIO();
                UTIL_.DELAY(2);
            } while (true);
        }
        void AIO(){
            if (aiModNum == null) return;
            for (int i = 0; i < aiModNum.Length; i++){
                for (int j = 0; j < 8; j++){
                    iMODULE = aiModNum[i];
                    iOFFSET = aiOffset[j + (i * 8)];
                    oOFFSET = aoOffset[j + (i * 8)];

                    uReadWord = 0;
                    CAXD.AxdiReadInportWord(iMODULE, iOFFSET, ref uReadWord);

                    no = (i * 8) + (j + (i * 8));
                    mAI_READ_WORD[no].Add(uReadWord);
                    if (mAI_READ_WORD[no].Count > 1) mAI_READ_WORD[no].RemoveAt(0);

                    sum = 0;
                    foreach (uint value in mAI_READ_WORD[no]) sum += value;
                    temp = sum / (uint)(mAI_READ_WORD[no].Count);

                    iOUT_1 = OUTPUT1[i]; //blow -> vac
                    iOUT_2 = OUTPUT2[i]; //vac -> blow
                    if (iOUT_1 == 172 || iOUT_2 == 172){
                        UTIL_.DELAY(1);
                    }
                    OUTPUT(iOUT_1, iOUT_2, iMODULE, /*iOFFSET*/oOFFSET);

                    Conversion_AD = (temp & /*0x03ff*/0x000003ff) * 0.1;   // 아날로그 값. // (m_Read & 0x000003ff) * 0.1
                    Set_AD = mSET_AI[j + (8 * i)];
                    nIN = INPUT[i] + (iOFFSET * 16);

                    if (Conversion_AD > Set_AD) mIN[nIN] = true;
                    else mIN[nIN] = false;

                    mAI[j + (8 * i)] = System.Math.Round(Conversion_AD, 1);
                    dAI_VALUE = (Conversion_AD * 0.04) + 1;
                    mSET_CONVERSION[j + (8 * i)] = (dAI_VALUE - 1) * 0.3; // 0.225
                }
            }
        }
        void OUTPUT(int Out1, int Out2, int module, int offset){
            SetOUT1 = Out1 + (offset * 16); //blow -> vac
            SetOUT2 = Out2 + (offset * 16); //vac -> blow
            if (mOUT[SetOUT1] && mOUT[SetOUT2]){
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 10, 1);
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 11, 1);
            } //
            else if (mOUT[SetOUT1] && !mOUT[SetOUT2]){
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 10, 1);
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 11, 0);
            } //blow -> vac
            else if (!mOUT[SetOUT1] && !mOUT[SetOUT2]){
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 10, 0);
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 11, 0);
            } //
            else{
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 10, 0);
                LAB_.SET_OUTPUT_BIT(module, (16 * offset) + 11, 1);
            } //vac -> blow
        }
    }
}