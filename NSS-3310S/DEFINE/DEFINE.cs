using LIB_.DateType;
using nAZIN;
using nINTERLOCK;
using NSS_3310S.SEQ.MODULE;
using nTENKEY;
using Object;
using System;
using System.Collections.Generic;
using System.IO;
using SYSTEM;

namespace NSS_3310S
{
    public class DEF : DATA_ 
    {
        public const string MCVersion   = "0415.22";
        public const string UpdataMemo  = "OVERLAP Ver1.0"; 

        public static int Ux, Uy            = 0; //검사 ROI X,Y 개수
        public static int[] Utx             = new int[2];
        public static int[] Uty             = new int[2]; //0;
        public static double[] Upx          = new double[2]; //0;
        public static double[] UpY          = new double[2];
        public static int[] CNT_UNIT_TRI    = new int[2]; //0;
        public static bool TenKeyOption     = false;
        public static int ManualPage        = -1;
        public static int MotorPage         = -1;
        public static long sUph, eUph, cntUph, tmNow, tmOld;
        public static bool[] bUSE_PK = new bool[2];

        #region >>UDP DEFINE
#if _UDP
        public static string SawIP              = "192.168.1.2";  //saw pc ip
        public static string HandlerIP          = "192.168.1.1";  //handler pc ip
        public static string SorterIP           = "192.168.1.110"; //sorter pc ip
        public static string VisionIP           = "192.168.1.111"; //vision pc ip

        public static bool bUDP = true;
#else
        public static string SawIP              = "127.0.0.1";
        public static string HandlerIP          = "127.0.0.1";

        public static string SorterIP           = "127.0.0.1";
        public static string VisionIP           = "127.0.0.1";

        public static bool bUDP                 = false;
#endif
        public static int SawPort               = 5000;
        public static int HandlerPort           = 5001;

        public static int SorterPort            = 6000;
        public static int VisionPort            = 6002;
        #endregion

        public static void SendDllDefine() {
            VT_START            = I.vtStart;
            VT_STOP             = I.vtStop;
            VT_RESET            = I.vtReset;
            STOP                = I.STOP;
            START               = I.START;
            RESET               = I.RESET;

            TOWER_RED           = O.TOWER_RED;
            TOWER_YELLOW        = O.TOWER_YELLOW;
            TOWER_GREEN         = O.TOWER_GREEN;
            TOWER_BLUE          = -1;

            CYLINDER_OVERTIME   = CP.CylinderOverTime;
            BZOffTime           = CP.BzOffTime;
            USE_LOG_SAVE        = CP.LogSaveSkip;
            UseLotEnd           = CP.UseLotEnd;
            SelectMTSpd         = CP.SelectMotorSpd;
            UseMES              = CP.UseMES;

            DllWarningMessage   = W.DllWarnning;
            WarningMessageBox   = W.ChkMessageBox;
            WAR_EndInitial      = W.EndInitial;
            MANUAL_REPEAT_DLAY  = D.ManualRepeat_Interval;

            //0(0123456)SORTER[128], 1HD1PICKER(128), 2HD2PICKER(128), 3(01)HANDLER1[32], 4(01)HANDLER2[32] 
            inModNum            = new int[] { 0, 1, 2, 3, 4 };
            InputNum            = new int[] { 111, 239, 367, 399, 431 };
            InputModule         = new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 4, 4 };

            //0(012345)SORTER[96], 1HD1PICKER(128), 2HD2PICKER(128), 5(01)HANDLER1[32], 6(01)HANDLER2[32] 
            outModNum           = new int[] { 0, 1, 2, 5, 6 };
            OutputNum           = new int[] { 95, 223, 351, 383, 415 };
            OutputModule        = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 6, 6 };

            //1HD1PICKER(128), 2HD2PICKER(128)
            aiModNum            = new int[] { 1, 2 };
            aoModNum            = new int[] { 1, 2 };
            for (int i = 0; i < mAI_READ_WORD.Length; i++) { 
                mAI_READ_WORD[i] = new List<uint>(); 
            }

            MT_GROUP_0          = new int[] { M.ElvY, M.ElvZ, M.RailF, M.RailR, M.GrpX, M.Barcode, M.StripPkX, M.StripPkZ, M.PreAlign, M.UnitPkX, M.UnitPkZ };
            MT_GROUP_1          = new int[] { M.TopVisionX, M.TopVisionZ, M.BtnVisionY, M.BtnVisionZ };
            MT_GROUP_2          = new int[] { M.Table1, M.Table2 };
            MT_GROUP_3          = new int[] { M.TRIGGER1, M.X1Z12, M.X1Z34, M.X1Z56, M.X1T, M.TRIGGER2, M.X2Z12, M.X2Z34, M.X2Z56, M.X2T };
            MT_GROUP_4          = new int[] { M.TrayFeeder1, M.TrayFeeder2, M.TrayFeeder3, M.TrayPickerX, M.TrayPickerZ, M.EmptyElv };

            MT_GROUP_0_NAME     = "HANDER ZONE";
            MT_GROUP_1_NAME     = "VISION ZONE";
            MT_GROUP_2_NAME     = "DAY-BLOCK ZONE";
            MT_GROUP_3_NAME     = "HEAD ZONE";
            MT_GROUP_4_NAME     = "TRY ZONE";

            mtTrigger           = new int[] { M.TRIGGER1, M.TRIGGER2 };
            SubTrigger          = new int[] { (int)eTRIGGER.HD1, (int)eTRIGGER.HD2 };

            iEMO                = new short[] { I.EMO_SAW_FRONT, I.EMO_SAW_REAR, I.EMO_SORTER_FRONT, I.EMO_SORTER_RIGHT, I.EMO_SORTER_BACK };
            eEMO                = new short[] { E.EMO_SAW_FRONT, E.EMO_SAW_REAR, E.EMO_SORTER_FRONT, E.EMO_SORTER_RIGHT, E.EMO_SORTER_BACK };
            iDOOR               = new short[] { I.DOOR_SAW_FRONT_LEFT, I.DOOR_SAW_FRONT_RIGHT, I.DOOR_SAW_SIDE_LEFT, I.DOOR_SORTER_FRONT_LEFT, I.DOOR_SORTER_FRONT_RIGHT, I.DOOR_SORTER_RIGHT_SIDE_LEFT, I.DOOR_SORTER_RIGHT_SIDE_RIGHT, I.DOOR_SORTER_BACK_LEFT, I.DOOR_SORTER_BACK_RIGHT };
            eDOOR               = new short[] { E.DOOR_SAW_FRONT_LEFT, E.DOOR_SAW_FRONT_RIGHT, E.DOOR_SAW_SIDE_LEFT, E.DOOR_SORTER_FRONT_LEFT, E.DOOR_SORTER_FRONT_RIGHT, E.DOOR_SORTER_RIGHT_SIDE_LEFT, E.DOOR_SORTER_RIGHT_SIDE_RIGHT, E.DOOR_SORTER_BACK_LEFT, E.DOOR_SORTER_BACK_RIGHT };
            iAEAR               = new short[] { I.LD_CONV_AREA_SENSOR };
            eAEAR               = new short[] { E.LD_CONV_AREA_SENSOR };
            iTRIP               = new short[] { I.SERVO1_SAW, I.SERVO2_SAW, I.SERVO3_SAW, I.SERVO4_SAW, I.CONV_TRIP, I.SERVO1_SORTER, I.SERVO2_SORTER, I.SERVO3_SORTER };
            eTRIP               = new short[] { E.SERVO1_SAW, E.SERVO2_SAW, E.SERVO3_SAW, E.SERVO4_SAW, E.CONV_TRIP, E.SERVO1_SORTER, E.SERVO2_SORTER, E.SERVO3_SORTER };
            iAIR                = new short[] { I.DRIVER_AIR_PRESSURE, I.BLOW_AIR_PRESSURE, I.STAGE_AIR_PRESSURE, I.PICKER_AIR_PRESSURE };
            eAIR                = new short[] { E.DRIVER_AIR_PRESSURE, E.BLOW_AIR_PRESSURE, E.STAGE_AIR_PRESSURE, E.PICKER_AIR_PRESSURE };
            
            iVAC                = new short[] { I.STRIP_PK_VAC, I.UNIT_PK_VAC, I.SCRAP_VAC1,  I.SCRAP_VAC2, I.STAGE_VACUUM1,   I.STAGE_VACUUM2 };

            oDOOR               = new short[] { O.DOOR_LOCK, O.SAW_DOOR_LOCK };
            oACMT               = new short[] { };
            oVAC                = new short[] { };
            oREJ                = new short[] { };
            oBZ                 = new short[] { O.BUZZER_ERR, O.BUZZER_END };

            mtHD                = new int[] { M.TRIGGER1, M.TRIGGER2 };
            mtHD1_PK            = new int[] { M.X1Z12, M.X1Z34, M.X1Z56 };
            mtHD2_PK            = new int[] { M.X2Z12, M.X2Z34, M.X2Z56 };
        }

        public static void CHK_MCDIR() {
            if (MC_DIR == 0) {
                InputOffset     = new int[] { 0, 1, 2, 3, 4, 5, 6, 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 0, 1 };  //0:정(STANDARD)
                OutputOffset    = new int[] { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 0, 1 }; //0:정(STANDARD)
                //진공모듈
                aiOffset        = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0 };  //0:정(STANDARD)
                aoOffset        = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0 };  //0:정
                O.HD1PkVac      = new int[] { O.X1_VAC1, O.X1_VAC2, O.X1_VAC3, O.X1_VAC4, O.X1_VAC5, O.X1_VAC6, O.X1_VAC7, O.X1_VAC8 };
                O.HD1PkRej      = new int[] { O.X1_BLOW1, O.X1_BLOW2, O.X1_BLOW3, O.X1_BLOW4, O.X1_BLOW5, O.X1_BLOW6, O.X1_BLOW7, O.X1_BLOW8 };
                O.HD2PkVac      = new int[] { O.X2_VAC8, O.X2_VAC7, O.X2_VAC6, O.X2_VAC5, O.X2_VAC4, O.X2_VAC3, O.X2_VAC2, O.X2_VAC1 };
                O.HD2PkRej      = new int[] { O.X2_BLOW8, O.X2_BLOW7, O.X2_BLOW6, O.X2_BLOW5, O.X2_BLOW4, O.X2_BLOW3, O.X2_BLOW2, O.X2_BLOW1 };
            } //정방향 (자재 투입 방향 : 왼쪽 -> 오른쪽)
            else {
                InputOffset     = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 0, 1 };  //1:역
                OutputOffset    = new int[] { 0, 1, 2, 3, 4, 5, 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 0, 1 }; //1:역
                //진공모듈
                aiOffset        = new int[] { 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 6, 7 };    //1:역
                aoOffset        = new int[] { 7, 6, 5, 4, 3, 2, 1, 0, 0, 1, 2, 3, 4, 5, 6, 7 };    //1:역
                O.HD1PkVac      = new int[] { O.X1_VAC8, O.X1_VAC7, O.X1_VAC6, O.X1_VAC5, O.X1_VAC4, O.X1_VAC3, O.X1_VAC2, O.X1_VAC1 };
                O.HD1PkRej      = new int[] { O.X1_BLOW8, O.X1_BLOW7, O.X1_BLOW6, O.X1_BLOW5, O.X1_BLOW4, O.X1_BLOW3, O.X1_BLOW2, O.X1_BLOW1 };
                O.HD2PkVac      = new int[] { O.X2_VAC1, O.X2_VAC2, O.X2_VAC3, O.X2_VAC4, O.X2_VAC5, O.X2_VAC6, O.X2_VAC7, O.X2_VAC8 };
                O.HD2PkRej      = new int[] { O.X2_BLOW1, O.X2_BLOW2, O.X2_BLOW3, O.X2_BLOW4, O.X2_BLOW5, O.X2_BLOW6, O.X2_BLOW7, O.X2_BLOW8 };
            } //역방향 (자재 투입 방향 : 오른쪽 -> 왼쪽)
        }

        public static void ReadName() {
            T.Label();
            S.Label();
            D.Label();
            L.Label();
            B.Label();

            TEACH_.Read_Color();
            TEACH_.Read_AnalogLabel();
            TEACH_.Read_InputLabel();
            TEACH_.Read_OutputLabel();
            TEACH_.Read_MachineParaLabel();
            TEACH_.Read_ModelParaLabel();
            TEACH_.Read_MotorLabel();
            COM_.LabelDEFINE_MOTION_ERR();
            TEACH_.Read_ErrorLabel();
            TEACH_.Read_InterlockErrorLabel();
            TEACH_.Read_WarningLabel();
            CUSER.READ();
        }
        public static void ReadMachine() {
            TEACH_.Read_PickerOffset();
            TEACH_.Read_AllPickerOffset();

            TEACH_.Read_LoginPassword();
            TEACH_.Read_TowerLamp();
            TEACH_.Read_Analog();
            TEACH_.Read_InfoIO();
            TEACH_.Read_MachinePara();
            TEACH_.Read_SoftLimit();

            TEACH_.Read_UnitPkWorkedCleanData();
        }

        public static bool CreateClass() {
            bool bRTN = LAB_.MOTION_INIALIZE();
            for (int i = 0; i < C.Motion.Length; i++){
                C.Motion[i] = new MOTOR_STATUS();
                COM_.RUN_THREAD(ref mcTH[T.Motion1 + i], C.Motion[i].DoReadMotion);
                C.Motion[i].nThread = i;
            }
            COM_.RUN_THREAD(ref mcTH[T.MotorLocation], C.MotorLocation.DoReadLocation);
            COM_.RUN_THREAD(ref mcTH[T.Homming], C.Homming.DoReadHomming);
            COM_.RUN_THREAD(ref mcTH[T.Input], C.Input.DoReadInput);
            COM_.RUN_THREAD(ref mcTH[T.Output], C.Output.DoWirteOutput);
            COM_.RUN_THREAD(ref mcTH[T.PkVac], C.PkVac.Do);
            COM_.RUN_THREAD(ref mcTH[T.Warnning], C.Warnning.CheckWarnning);
            COM_.RUN_THREAD(ref mcTH[T.Manual], C.Manual.Do);
            COM_.RUN_THREAD(ref mcTH[T.ManualRepeat], C.ManualRepeat.DoRepeat);
            COM_.RUN_THREAD(ref mcTH[T.StopEvent], C.StopEvent.DoReadStopEvent);
            COM_.RUN_THREAD(ref mcTH[T.ReceiveSaw], C.ReceiveSaw.DoReceiveEvent);
            COM_.RUN_THREAD(ref mcTH[T.ReceiveVision], C.RecieveVision.DoReceiveEvent);       
            COM_.RUN_THREAD(ref mcTH[T.Op], C.Op.Do);
            return bRTN;
        }

        public static bool ChkRecieverManual(int nThread) {
            if (eMCStatus == eMachineStatus.AUTO) {
                if (nThread == T.ReceiveSaw)    C.ReceiveSaw.ManualFail("SORTER MACHINE RUN STATE.");
                else                            C.RecieveVision.ManualFail("SORTER MACHINE RUN STATE.");
                return false;
            }
            else if (eMCStatus == eMachineStatus.INITIAL) {
                if (nThread == T.ReceiveSaw)    C.ReceiveSaw.ManualFail("SORTER MACHINE INITIALIZE.");
                else                            C.RecieveVision.ManualFail("SORTER MACHINE INITIALIZE.");
                return false;
            }
            else if (eMCStatus == eMachineStatus.EMSSTOP || eMCStatus == eMachineStatus.ERRSTOP) {
                if (nThread == T.ReceiveSaw)    C.ReceiveSaw.ManualFail("SORTER MACHINE ERROR.");
                else                            C.RecieveVision.ManualFail("SORTER MACHINE ERROR.");
                return false;
            }
            else if (eMCStatus == eMachineStatus.NONE || eMCStatus == eMachineStatus.READYSTOP) {
                if (nThread == T.ReceiveSaw)    C.ReceiveSaw.ManualFail("SORTER MACHINE NONE STATE.");
                else                            C.RecieveVision.ManualFail("SORTER MACHINE NONE STATE.");
                return false;
            }
            else if (bMF) {
                if (nThread == T.ReceiveSaw)    C.ReceiveSaw.ManualFail("SORTER MACHINE MANAUL-RUN STATE.");
                else                            C.RecieveVision.ManualFail("SORTER MACHINE MANAUL-RUN STATE.");
                return false;
            }

            if (nThread == T.ReceiveSaw)    IsBIT[B.SawManualRun] = true;
            else                            IsBIT[B.VisionManualRun] = true;
            return true;
        }

        public static void ChkEndMassage(string sMsg) {
            if (IsBIT[B.SawManualRun]) {

            }
            if (IsBIT[B.VisionManualRun]) {

            }
        }

        public static void Power(bool bFlog) {
            mOUT[O.LD_CONV_INVERTER_POWER]  = bFlog;
            mOUT[O.FLUORESENT_LIGHT]        = bFlog;
        }

        public static bool ChkUsePicker() {
            bUSE_PK[0] = false;
            bUSE_PK[1] = false;
            for (int i = 0; i < CNT_.PKR; i++) {
                if (PK_Z[i] != eSTATUS.NONE) bUSE_PK[0] = true;
                if (PK_Z[i + CNT_.PKR] != eSTATUS.NONE) bUSE_PK[1] = true;
            }
            if (prMODEL[CP.SelectHead] == (int)eHD.HD1) bUSE_PK[1] = true;
            if (prMODEL[CP.SelectHead] == (int)eHD.HD2) bUSE_PK[0] = true;
            if (!bUSE_PK[0] || !bUSE_PK[1]) return false;
            return true;
        }

        public static void ChangeSawRecipe() {
            IsBIT[B.SawRecipeOpen] = false;
            LAB_.BIT_OUT(O.HANDLER_RECIPE_CHANGE, true);
            UTIL_.DELAY(100);
            LAB_.BIT_OUT(O.HANDLER_RECIPE_CHANGE, false);
        }
        public static bool SawRecipeOpen() {
            for (int i = 0; i < 5000; i++) {
                UTIL_.DELAY(1);
                if (IsBIT[B.SawRecipeOpen]) return true;
            }
            if (!IsBIT[B.SawRecipeOpen]) return false;
            return true;
        }

        public static void ChangeVisionRecipe() {
            IsBIT[B.VisionRecipeOpen] = false;
            LAB_.BIT_OUT(O.RECIPE_CHANGE, true);
            UTIL_.DELAY(100);
            LAB_.BIT_OUT(O.RECIPE_CHANGE, false);
        }

        public static bool VisionRecipeOpen() {
            for (int i = 0; i < 5000; i++) {
                UTIL_.DELAY(1);
                if (IsBIT[B.VisionRecipeOpen]) return true;
            }
            if (!IsBIT[B.VisionRecipeOpen]) return false;
            return true;
        }

        static void Cal_TriggerCount(eMAP_BLOCK PALLET) {
            double mmm          = (short)(Math.Ceiling((double)(prMODEL[RP.UnitCntX] / Ux))); //MAPPING X방향 수량 / ROI X방향 검사 수량 = X 검사 그룹 개수
            Utx[(int)PALLET]    = (short)(Math.Ceiling(mmm));
            Upx[(int)PALLET]    = mmm;
            mmm                 = (prMODEL[RP.UnitCntY] / Convert.ToDouble(Uy)); //MAPPING Y방향 수량 / ROI Y방향 검사 수량 = Y 검사 그룹 개수
            Uty[(int)PALLET]    = (short)(Math.Ceiling(mmm));
            UpY[(int)PALLET]    = Uty[(int)PALLET];
            if (IsBIT[B.UnitEdgeInspection]) {
                Utx[(int)PALLET] += 1;
                Uty[(int)PALLET] += 1;
            }
            CNT_UNIT_TRI[(int)PALLET] = (short)((Utx[(int)PALLET] * Uty[(int)PALLET]) - 1);
        }
        public static bool ReadSearchRoiUnitCnt() {
            StreamReader sr = new StreamReader(PATH_.PathUnitSearchXY);
            string mSTR     = sr.ReadLine();
            string[] aStr   = mSTR.Split(';');
            Ux              = Convert.ToInt16(aStr[0]); //검사 ROI X 방향 CHIP 개수
            Uy              = Convert.ToInt16(aStr[1]); //검사 ROI Y 방향 CHIP 개수
            sr.Close();

            //for (int i = 0; i < 2; i++) { 
            //    double mmm              = (short)(Math.Ceiling((double)(prMODEL[/*MD.MAPBLOCK1_CNT_X*/MD.MAPBLOCK_UNIT_X[i]] / DEF.Ux))); //MAPPING X방향 수량 / ROI X방향 검사 수량 = X 검사 그룹 개수
            //    DEF.Utx[i]              = (short)(Math.Ceiling(mmm));
            //    DEF.Upx[i]              = mmm;
            //    mmm                     = (prMODEL[/*MD.MAPBLOCK1_CNT_Y*/MD.MAPBLOCK_UNIT_Y[i]] / Convert.ToDouble(DEF.Uy)); //MAPPING Y방향 수량 / ROI Y방향 검사 수량 = Y 검사 그룹 개수
            //    DEF.Uty[i]              = (short)(Math.Ceiling(mmm));
            //    DEF.UpY[i]              = DEF.Uty[i];
            //    if (IsBIT[mB.MarkInspection_Edge]){
            //        DEF.Utx[i] += 1;
            //        DEF.Uty[i] += 1;
            //    }
            //    DEF.CNT_UNIT_TRI[i]     = (short)((DEF.Utx[i] * DEF.Uty[i]) - 1);
            //}

            if (Ux > 0 && Uy > 0) IsBIT[B.UnitEdgeInspection] = false;
            else {
                IsBIT[B.UnitEdgeInspection] = true;
                Ux                          = 1;
                Uy                          = 1;
            }
            Cal_TriggerCount(eMAP_BLOCK.STAGE1);
            Cal_TriggerCount(eMAP_BLOCK.STAGE2);
            Cal_UnitInspectionPos();
            return true;
        }
        static void Cal_UnitInspectionPos() {
            short uY, uX;
            double pGX, pGY;
            double Px, Py;
            int[] posX = { P.Pallet1_Unit, P.Pallet2_Unit };
            int[] motY = { M.Table1, M.Table2 };
            //SAW 기준으로 X/Y
            //MAP-BLOCK기준으론Y/X
            double UnitEdgeX = IsBIT[B.UnitEdgeInspection] ? (prMODEL[RP.UnitSizeX] / 2) : 0;
            double UnitEdgeY = IsBIT[B.UnitEdgeInspection] ? (prMODEL[RP.UnitSizeY] / 2) : 0;
            double StartOffset_X;
            double STartOffset_Y;

            for (int p = 0; p < 2; p++) {
                StartOffset_X = (prMODEL[RP.UnitCntX] * prMODEL[RP.UnitPitchX]) - prMODEL[RP.UnitPitchX];
                STartOffset_Y = (prMODEL[RP.UnitCntY] * prMODEL[RP.UnitPitchY]) - prMODEL[RP.UnitPitchY];

                for (int y = 0; y < (int)prMODEL[RP.GroupCntY]; y++) {
                    for (int x = 0; x < (int)prMODEL[RP.GroupCntX]; x++) {
                        for (int i = 0; i < Utx[p] * Uty[p]; i++) {
                            uX  = Convert.ToInt16(i % Utx[p]);
                            uY  = (short)(i / Utx[p]);
                            pGX = prMODEL[RP.GroupPitchX] * x;
                            pGY = prMODEL[RP.GroupPitchY] * y;

                            //한쪽방향
                            Px  = (Ux * prMODEL[RP.UnitPitchX]) * uX; //cDEF.Upx UNIT_PITCH_X uX;
                            Py  = (Uy * prMODEL[RP.UnitPitchY]) * uY;

                            //지그제그
                            //if (uY % 2 == 0) Px = cDEF.Upx * uX;  
                            //else Px = (cDEF.Upx - uX - 1) * cDEF.Upx;  

                            MarkCalPos_[p/*motY[p]*/, x, y, i].x = (mtDATA[M.TopVisionX, posX[p]].Pos - StartOffset_X) + Px + pGX - UnitEdgeX;  //X  방향
                            MarkCalPos_[p/*motY[p]*/, x, y, i].y = (mtDATA[motY[p], P.TopVision_Unit].Pos + STartOffset_Y) - Py - pGY + UnitEdgeY; //(uY * cDEF.UpY);  //Y 방향  //Dev.TX_P
                            //System.Diagnostics.Trace.WriteLine(i + " " + MarkCalPos_[p/*motY[p]*/, x, y, i].y + " " + MarkCalPos_[p/*motY[p]*/, x, y, i].x);
                        }
                    }
                }
            }
            //Trace.WriteLine("CaL Makr Vision END");
        }

        public static bool ReadUnitAlign(eMAP_BLOCK eSTAGE) {
            for (int i = 0; i < 500; i++) {
                if (mIN[I.UnitAlignWrite]) break;
                UTIL_.DELAY(2);
                //응답시간 무시
            }

            if (File.Exists(PATH_.UNIT_ALIGN)) {
                try {
                    string[] sLine                          = File.ReadAllText(PATH_.UNIT_ALIGN).Split(ETC.CrLf);
                    sLine[0]                                = sLine[0].Replace(";", "");
                    string[] sRslt                          = sLine[0].Split(',');
                    IsDOUBLE[D.UnitOffsetX[(int)eSTAGE]]    = double.Parse(sRslt[0]);
                    IsDOUBLE[D.UnitOffsetY[(int)eSTAGE]]    = double.Parse(sRslt[1]);
                    IsDOUBLE[D.UnitOffsetT[(int)eSTAGE]]    = double.Parse(sRslt[2]);

                    if (File.Exists(PATH_.UNIT_ALIGN)) {
                        try { File.Delete(PATH_.UNIT_ALIGN); }
                        catch (Exception ex1) { LogWR_.SaveLogException("READ MAP-BLOCK ALIGN => D:\\ShareFile\\UNIT_ALIGN.txt DELETE FAIL !", ex1); }
                    }
                }
                catch (Exception ex) {
                    LogWR_.SaveLogException("unit align result Write Fail", ex);
                }
            }
            return true;
        }

        static string VirtualUnitInspectionData(eMAP_BLOCK nSTAGE) {
            string sData = string.Empty;
            for (int gy = 0; gy < (int)prMODEL[RP.GroupCntY]; gy++) {
                for (int gx = 0; gx < (int)prMODEL[RP.GroupCntX]; gx++) {
                    for (int uy = 0; uy < (int)prMODEL[RP.UnitY[(int)nSTAGE]]; uy++) {
                        for (int ux = 0; ux < (int)prMODEL[RP.UnitX[(int)nSTAGE]]; ux++) {
                            sData += "1";
                            if (ux >= ((int)prMODEL[RP.UnitX[(int)nSTAGE]] - 1))    sData += ";";
                            else                                                    sData += ",";
                        }
                        sData += ETC.NewLine;
                    }
                }
            }
            //if (File.Exists(mPATH.UNIT)) File.Delete(mPATH.UNIT);
            //mFILE.WR_File(mPATH.UNIT, sData, false);
            return sData;
        }
        static string VirtualUnitOffsetData(eMAP_BLOCK nSTAGE) {
            string sData = string.Empty;
            for (int gy = 0; gy < (int)prMODEL[RP.GroupCntY]; gy++) {
                for (int gx = 0; gx < (int)prMODEL[RP.GroupCntX]; gx++) {
                    for (int uy = 0; uy < (int)prMODEL[RP.UnitY[(int)nSTAGE]]; uy++) {
                        for (int ux = 0; ux < (int)prMODEL[RP.UnitX[(int)nSTAGE]]; ux++) {
                            sData += "0,0;";
                            //if (ux >= ((int)prMODEL[RP.UnitX[(int)nSTAGE]] - 1)) sData += ";";
                            //else sData += ",";
                        }
                        sData += ETC.NewLine;
                    }
                }
            }
            return sData;
        }
        public static eRTN ReadInspectionResult(int nThread, eMAP_BLOCK eSTAGE) {
            string vData;
            string vOffset;
            int nGROUP, nGX, nGY, nUY, nSX, nSY;
            IsLONG[L.UnitSizeNgCnt] = 0;
            OFFSET.Initialize();

            if (prMACHINE[CP.UseTopInspection] == (int)eUSE.USE && !bDRYRUN /*&& !bMF*/) {
                while (!mIN[I.UnitDataWrite]) {
                    if (bMF) break;
                    for (int t = 0; t < (int)prMACHINE[CP.VisionReponseOverTime]; t++) {
                        UTIL_.DELAY(1);
                        if (mIN[I.UnitDataWrite]) break;
                    }
                    if (!mIN[I.UnitDataWrite]) return eRTN.ResponseOverTime;
                }
                if (bMF) UTIL_.DELAY(500);
                else UTIL_.DELAY(100);
            }
            else {
                if (bDRYRUN) UTIL_.DELAY(300);
                vData   = VirtualUnitInspectionData(eSTAGE);
                vOffset = VirtualUnitOffsetData(eSTAGE);
                try {
                    if (File.Exists(PATH_.UNIT)) File.Delete(PATH_.UNIT);
                    FILE_.WR_File(PATH_.UNIT, vData, false);
                    if (File.Exists(PATH_.UNIT_OFFSET)) File.Delete(PATH_.UNIT_OFFSET);
                    FILE_.WR_File(PATH_.UNIT_OFFSET, vOffset, false);
                    UTIL_.DELAY(33);
                }
                catch (Exception ChkOpen) {
                    LogWR_.SaveLogException("WIRTE MAP-BLOCK DATA FAIL [VISION PC-FILE OPEN]", ChkOpen);
                    return eRTN.WrittingFail;
                }
            } // 비전 검사 SKIP 데이터 만들어야 함.

            if (!File.Exists(PATH_.UNIT))           return eRTN.NotDataFile;
            if (!File.Exists(PATH_.UNIT_OFFSET))    return eRTN.NotDataFile;

            string[] sLine      = File.ReadAllText(PATH_.UNIT).Split(ETC.CrLf);
            string[] sOFFSET    = File.ReadAllText(PATH_.UNIT_OFFSET).Split(ETC.CrLf);
            try {
                for (int idx = 0; idx < sLine.Length - 1; idx++) {
                    nGROUP = (short)(idx / prMODEL[RP.UnitY[(int)eSTAGE]]);
                    if (0 != Convert.ToInt16((sLine.Length - 1) % prMODEL[RP.UnitY[(int)eSTAGE]])) {
                        //UTIL_.OnERROR(E.NotUnitYCount, 500);
                        TEACH_.SAVE_VISION_RESULT_WRITE_FAIL((int)eSTAGE, sLine);
                        return eRTN.IndexFail;
                    }
                    sLine[idx]          = sLine[idx].Replace(";", "");
                    sLine[idx]          = sLine[idx].Replace("\n", "");
                    sLine[idx]          = sLine[idx].Replace("\r", "");
                    string[] sRslt      = sLine[idx].Split(',');

                    sOFFSET[idx]        = sOFFSET[idx].Replace("\n", "");
                    sOFFSET[idx]        = sOFFSET[idx].Replace("\r", "");
                    string[] sOffset    = sOFFSET[idx].Split(';');

                    if (prMODEL[RP.UnitX[(int)eSTAGE]] != sRslt.Length) {
                        //UTIL_.OnERROR(E.NotUnitXCount, 500);
                        TEACH_.SAVE_VISION_RESULT_WRITE_FAIL((int)eSTAGE, sLine);
                        return eRTN.IndexFail;
                    }
                    nGX = (short)(nGROUP / prMODEL[RP.GroupCntX]);
                    nGY = (short)(prMODEL[RP.UnitY[(int)eSTAGE]] * prMODEL[RP.GroupCntX] % (sLine.Length - 1));
                    nUY = (short)(idx % prMODEL[RP.UnitY[(int)eSTAGE]]);
                    for (int x = 0; x < sRslt.Length; x++) {
                        nSX = sRslt.Length - 1 - x;
                        nSY = (int)prMODEL[RP.UnitY[(int)eSTAGE]] - 1 - nUY;

                        if (int.Parse(sRslt[x]) == (int)eSTATUS.NG)                     IsLONG[L.UnitSizeNgCnt]++;
                        if (prMACHINE[CP.UseTopInspectionResult] == (int)eUSE.NotUSE)   sRslt[x] = "1";
                        PALLET[(int)eSTAGE, nGX, nGY, /*nSX*/x, nSY/*nUY*/]     = int.Parse(sRslt[x]); //TOP-PICKUP(nUY),BTM-PICKUP(setY)
                        string[] sOffsetXY                                      = sOffset[x].Split(',');
                        OFFSET[(int)eSTAGE, nGX, nGY, /*nSX*/x, nSY/*nUY*/].x   = double.Parse(sOffsetXY[0]);
                        OFFSET[(int)eSTAGE, nGX, nGY, /*nSX*/x, nSY/*nUY*/].y   = double.Parse(sOffsetXY[1]);
                    }
                }
            }
            catch (Exception exp) {
                LogWR_.SaveLogException("MapBlock_Unit Write Fail", exp);
                return eRTN.ReadingDataFail;
            }
            COM_.SetOutput(nThread, O.UnitReading, true, "UNIT 데이터 읽었다고 비전에 신호 보냄.");
            if (prMACHINE[CP.UseTopInspection] == (int)eUSE.USE && bDRYRUN) {
                while (mIN[I.UnitDataWrite]) {
                    for (int t = 0; t < (int)prMACHINE[CP.VisionReponseOverTime]; t++) {
                        UTIL_.DELAY(1);
                        if (!mIN[I.UnitDataWrite]) break;
                    }
                    if (mIN[I.UnitDataWrite]) return eRTN.ResponseOverTime;
                }
            }
            COM_.SetOutput(nThread, O.UnitReading, false, "UNIT 데이터 읽었다고 비전에 신호 OFF.");
            if (/*prMODEL[RP.UnitSizeNGOverCount]*/10 > 0 && eMCStatus == eMachineStatus.AUTO) {
                if (IsLONG[L.UnitSizeNgCnt] >= /*prMODEL[RP.UnitSizeNGOverCount]*/10) return eRTN.NGOverCnt;
            }
            return eRTN.SUCESS;
        }

        public static eRTN ReadITSResult(eMAP_BLOCK eSTAGE, string sBARCODE) {
            if (sBARCODE == null || sBARCODE == "") return eRTN.NothingBarcode;
            if (!File.Exists(PATH_.ITSCount))       return eRTN.NotITSCountFile;
            if (!File.Exists(PATH_.ITSLocation))    return eRTN.NotITSLocationFile;

            string[] sCountList     = File.ReadAllText(PATH_.ITSCount).Split(ETC.CrLf);
            string[] sLocationList  = File.ReadAllText(PATH_.ITSLocation).Split(ETC.CrLf);

            int nCount = 0;
            bool bSerch = false;
            try{
                for (int idx = 0; idx < sCountList.Length - 1; idx++){
                    sCountList[idx] = sCountList[idx].Replace("\n", "");
                    sCountList[idx] = sCountList[idx].Replace("\r", "");
                    string[] sRslt  = sCountList[idx].Split(',');

                    if (sRslt[1] == sBARCODE){
                        nCount = int.Parse(sRslt[2]);
                        bSerch = true;
                        goto Search;
                    }
                }
            }
            catch (Exception E){
                LogWR_.SaveLogException("ITS count infomation write fail", E);
                return eRTN.FailITSCountDataParsingFail;
            }
         Search:
            if (!bSerch)        return eRTN.NothingBarcode; //바코드 정보 없음
            if (nCount <= 0)    return eRTN.SUCESS; // 불량 하나도 없음!
            string[] InfoLocation   = new string[nCount];
            int nIDX                = 0;
            try{
                for (int idx = 0; idx < sLocationList.Length - 1; idx++){
                    sLocationList[idx]  = sLocationList[idx].Replace("\n", "");
                    sLocationList[idx]  = sLocationList[idx].Replace("\r", "");
                    string[] sList      = sLocationList[idx].Split(',');
                    if (sList[2] == sBARCODE){
                        InfoLocation[nIDX] = sLocationList[idx];
                        nIDX++;
                    }
                }
                
                for (int idx = 0; idx < InfoLocation.Length; idx++){
                    string[] sLocation  = InfoLocation[idx].Split(',');
                    int Y               = int.Parse(sLocation[5]);
                    int X               = int.Parse(sLocation[6]);

                    //초기버전
                    //for (int uy = 0; uy < (int)prMODEL[RP.UnitCntY]; uy++) {
                    //    int nY = (int)prMODEL[RP.UnitCntY] - 1 - uy;
                    //    if (nY == (Y - 1)){
                    //        for (int ux = 0; ux < (int)prMODEL[RP.UnitCntX]; ux++){
                    //            int nX = (int)prMODEL[RP.UnitCntX] - 1 - ux; //정방향 설비 
                    //            if (MC_DIR == 1) nX = ux;  //역방향 설비
                    //            if (ux == (X - 1)){
                    //                PALLET[(int)eSTAGE, /*nGX, nGY*/ 0, 0, nX, nY] = (int)eSTATUS.FAIL;
                    //                if (!bMF) IsLONG[L.ITSCount] += 1;
                    //            }
                    //        }
                    //    }
                    //}

                    //수정
                    int nX;
                    int nY;
                    for (int uy = 0; uy < (int)prMODEL[RP.UnitCntY]; uy++){
                        nY = (int)prMODEL[RP.UnitCntY] - 1 - uy; //y좌표값 반전
                        if (nY == (Y - 1)){
                            for (int ux = 0; ux < (int)prMODEL[RP.UnitCntX]; ux++){
                                nX = (int)prMODEL[RP.UnitCntX] - 1 - ux; //x좌표값 반전
                                if (MC_DIR == 0){
                                    if (prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP){
                                    
                                    }//strip 투입
                                    else{
                                        nX = ux;
                                        nY = uy;
                                    }//quad 투입
                                } //정방향 설비
                                else{
                                    if (prMODEL[RP.PCB_TYPE] == (int)ePCB.STRIP){
                                        nY = uy;
                                    }//strip 투입
                                    else{
                                        nX = ux;
                                    }//quad 투입
                                } //역방향 설비

                                if (ux == (X - 1)){
                                    PALLET[(int)eSTAGE, /*nGX, nGY*/ 0, 0, nX, nY] = (int)eSTATUS.FAIL;
                                    if (!bMF) IsLONG[L.ITSCount] += 1;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception E){
                LogWR_.SaveLogException("ITS location list infomation write fail", E);
                return eRTN.FailITSLocationDataParsingFail;
            }
            return eRTN.SUCESS;
        }

        public static void ReadMapBlockPickUp(){
            Cal_PickUpPos(eHD.HD1, P.Pallet1, eMAP_BLOCK.STAGE1, P.HD1_Unit);
            Cal_PickUpPos(eHD.HD1, P.Pallet2, eMAP_BLOCK.STAGE2, P.HD1_Unit);
            Cal_PickUpPos(eHD.HD2, P.Pallet1, eMAP_BLOCK.STAGE1, P.HD2_Unit);
            Cal_PickUpPos(eHD.HD2, P.Pallet2, eMAP_BLOCK.STAGE2, P.HD2_Unit);
        }
        public static void Cal_PickUpPos(eHD nX, short P, eMAP_BLOCK STAGE, short P1){
            short uX, uY;
            double dGX, dGY, Px, Py, dFirstX = 0;

            //if (prMODEL[MD.MAPBLOCK_MDOE] == (int)eREVERSE.FULL) dFirstX = 0;
            //else if (prMODEL[MD.MAPBLOCK_MDOE] == (int)eREVERSE.ODD){
            //    if (MT.MAP_BLOCK[(int)ePallet] == MT.PALLET1)    dFirstX = 0;
            //    else                                             dFirstX = prMODEL[MD.MAPBLOCK_PITCH_X];
            //}
            //else{
            //    if (MT.MAP_BLOCK[(int)ePallet] == MT.PALLET1)    dFirstX = prMODEL[MD.MAPBLOCK_PITCH_X];
            //    else                                             dFirstX = 0;
            //}

            //Trace.WriteLine("CaL MapBlock START");
            for (int gy = 0; gy < prMODEL[RP.GroupCntY]; gy++){
                dGY = prMODEL[RP.GroupPitchY] * gy;
                for (int gx = 0; gx < prMODEL[RP.GroupCntX]; gx++){
                    dGX = prMODEL[RP.GroupPitchX] * gx;
                    for (short i = 0; i < prMODEL[RP.UnitX[(int)STAGE]] * prMODEL[RP.UnitY[(int)STAGE]]; i++){
                        uX = Convert.ToInt16(i % prMODEL[RP.UnitX[(int)STAGE]]);
                        uY = (short)(i / prMODEL[RP.UnitX[(int)STAGE]]);

                        //한쪽방향
                        Px = prMODEL[RP.UnitPitchX] * uX;
                        Py = prMODEL[RP.UnitPitchY] * uY;
                        //지그제그
                        //if (uY % 2 == 0) Px = prMODEL[cPARA.prMAPBLOCK_PITCH_X] * uX;  //Dev.TY_P
                        //else Px = (prMODEL[cPARA.prMAPBLOCK_CNT_X] - uX - 1) * prMODEL[cPARA.prMAPBLOCK_PITCH_X];  //Dev.TY_P

                        MapCalPos_[(int)nX, (int)STAGE, i].x = (mtDATA[M.HD[(int)nX], P].Pos + dFirstX) - Px - dGX;  //X  방향
                        MapCalPos_[(int)nX, (int)STAGE, i].y = mtDATA[M.DRY_TABLE[(int)STAGE], P1].Pos + Py + dGY;  //Y 방향  //Dev.TX_P
                        //Trace.WriteLine("HEAD " + ehead.ToString() + "PALLET " + ePallet.ToString() + i + " " + MapCalPos_[(int)ehead, (int)ePallet, i].y + " " + MapCalPos_[(int)ehead, (int)ePallet, i].x);
                        //Trace.WriteLine(mtDATA[MT.HEAD[(int)ehead], p].Pos.ToString());
                        //Trace.WriteLine(MapCalPos_[(int)ehead, (int)ePallet, i].x.ToString());
                    }
                }
            }
            //Trace.WriteLine("CaL MapBlock END");
        }

        public static void ResetPallet(eMAP_BLOCK PALLET){
            MAP_.PalletMap_Reset(PALLET, (eMAP_DATA)prMACHINE[CP.MapBlockMode], (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitCntX], (int)prMODEL[RP.UnitCntY]);
            MAP_.CancelPalletData(PALLET, (eMAP_DATA)prMACHINE[CP.MapBlockMode], (int)prMODEL[RP.GroupCntX], (int)prMODEL[RP.GroupCntY], (int)prMODEL[RP.UnitCntX], (int)prMODEL[RP.UnitCntY]);
        }

        public static void ResetTray(eTRAY TRAY){
            MAP_.TrayMap_Reset(TRAY, (int)prMODEL[RP.TrayCntX], (int)prMODEL[RP.TrayCntY]);
        }

        public static bool GetPallet(eMAP_BLOCK nPALLET, ref int GX, ref int GY, ref int UX, ref int UY, ref int INDEX){
            if (-1 == MAP_.GetPalletPocket(nPALLET, (int)prMODEL[RP.GroupX[(int)nPALLET]], (int)prMODEL[RP.GroupY[(int)nPALLET]], (int)prMODEL[RP.UnitX[(int)nPALLET]], (int)prMODEL[RP.UnitY[(int)nPALLET]], ref GX, ref GY, ref UX, ref UY)){
                return false;
            }
            INDEX = ((UY + 1) > 1) ? (int)(prMODEL[RP.UnitX[(int)nPALLET]] * UY) + (UX + 1) : (UY + 1) * (UX + 1);
            return true;
        }

        public static void ReadTrayPlace(){
            Cal_PlacePos(eHD.HD1, P.Feeder1, eTRAY.GOOD1, P.HD1TrayPocket);
            Cal_PlacePos(eHD.HD2, P.Feeder1, eTRAY.GOOD1, P.HD2TrayPocket);
            Cal_PlacePos(eHD.HD1, P.Feeder2, eTRAY.GOOD2, P.HD1TrayPocket);
            Cal_PlacePos(eHD.HD2, P.Feeder2, eTRAY.GOOD2, P.HD2TrayPocket);
            Cal_PlacePos(eHD.HD1, P.Feeder3, eTRAY.REWORK, P.HD1TrayPocket);
            Cal_PlacePos(eHD.HD2, P.Feeder3, eTRAY.REWORK, P.HD2TrayPocket);
        }
        public static void Cal_PlacePos(eHD nX, short P, eTRAY TRAY, short P1){
            short uX, uY;
            double Px;
            for (short i = 0; i < prMODEL[RP.TrayCntX] * prMODEL[RP.TrayCntY]; i++){
                uX = Convert.ToInt16(i % prMODEL[RP.TrayCntX]);
                uY = (short)(i / prMODEL[RP.TrayCntX]);

                //한쪽방향
                Px = prMODEL[RP.TrayPitchX] * uX;
                //지그제그
                //if (uY % 2 == 0) Px = prMODEL[cPARA.prTRAY_PITCH_X] * uX;  //Dev.TY_P
                //else Px = (prMODEL[cPARA.prTRAY_CNT_X] - uX - 1) * prMODEL[cPARA.prTRAY_PITCH_X];  //Dev.TY_P

                TryCalPos_[(int)nX, (int)TRAY, i].x = mtDATA[M.HD[(int)nX], P].Pos - Px;  //X  방향
                TryCalPos_[(int)nX, (int)TRAY, i].y = mtDATA[M.TRAY_FEEDER[(int)TRAY], P1].Pos - (uY * prMODEL[RP.TrayPitchY]);  //Y 방향  //Dev.TX_P
                //System.Diagnostics.Trace.WriteLine(i + " " + TryCalPos_[(int)nX, (int)TRAY, i].y + " " + TryCalPos_[(int)nX, (int)TRAY, i].x);
                //Trace.WriteLine(mtDATA[MT.TRAY[(int)tray], p1].Pos.ToString());
                //Trace.WriteLine((uY * prMODEL[MD.TRAY_PITCH_Y]).ToString());
            }
            //Trace.WriteLine("CaL TRAY END");
        }

        public static void UPH(){
            IsLONG[L.GoodCnt]++;
            IsLONG[L.OutCnt]++;

            cntUph += 1;
            if (cntUph >= prMACHINE[CP.UphCalcCount]){ // 10 개 단위로.->나중에 100개 단위로
                cntUph                  = 0;
                eUph                    = Environment.TickCount;
                IsDOUBLE[D.Tact_Avg]    = ((eUph - sUph) / prMACHINE[CP.UphCalcCount]) / 1000;
                sUph                    = Environment.TickCount; // 절대 정확할수 없음.(에러정지시간 포함되기 때문..)
            }
            tmNow               = Environment.TickCount; // 택-타임
            IsDOUBLE[D.PkgUPH]  = (double)(tmNow - tmOld) / 1000;
            tmOld               = tmNow;
        }

        public static void SaveProduction(){
            TEACH_.SaveLotInfoCount(L.Production);
        }
        public static void CountReset(bool bSave){
            if (bSave) SaveProduction();
            L.Reset();
        }
    
        public static void SetParaFDC(){
            int nVALUE;
            SUBFRM_.gSecsGem.SetSVID(CSVID.MGZ_SLOT_CNT, prMODEL[RP.MGZSlotCnt].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MGZ_SLOT_PITCH, prMODEL[RP.MGZSlotPitch].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK1_TOP_CAM_FIRST_POS_X, mtDATA[M.TopVisionX, P.Pallet1_Unit].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK1_TOP_CAM_FIRST_POS_Y, mtDATA[M.Table1, P.TopVision_Unit].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK1_HEAD_CAM_FIRST_POS_X, mtDATA[M.TRIGGER1, P.Pallet1].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK1_HEAD_CAM_FIRST_POS_Y, mtDATA[M.Table1, P.HD1_Unit].Pos.ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK2_TOP_CAM_FIRST_POS_X, mtDATA[M.TopVisionX, P.Pallet2_Unit].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK2_TOP_CAM_FIRST_POS_Y, mtDATA[M.Table2, P.TopVision_Unit].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK2_HEAD_CAM_FIRST_POS_X, mtDATA[M.TRIGGER1, P.Pallet2].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK2_HEAD_CAM_FIRST_POS_Y, mtDATA[M.Table2, P.HD1_Unit].Pos.ToString());

            //HEAD 2번 추가 필요!

            SUBFRM_.gSecsGem.SetSVID(CSVID.UNIT_SIZE_X, prMODEL[RP.UnitSizeX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.UNIT_SIZE_Y, prMODEL[RP.UnitSizeY].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.UNIT_THICKNES, prMODEL[RP.UnitThickess].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.TRAY_CNT_X, prMODEL[RP.TrayCntX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.TRAY_CNT_Y, prMODEL[RP.TrayCntY].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.TRAY_PITCH_X, prMODEL[RP.TrayPitchX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.TRAY_PITCH_Y, prMODEL[RP.TrayPitchY].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_CNT_X, prMODEL[RP.UnitCntX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_CNT_Y, prMODEL[RP.UnitCntY].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_PITCH_X, prMODEL[RP.UnitPitchX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_PITCH_Y, prMODEL[RP.UnitPitchY].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_GROUP_CNT_X, prMODEL[RP.GroupCntX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_GROUP_CNT_Y, prMODEL[RP.GroupCntY].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_GROUP_PITCH_X, prMODEL[RP.GroupPitchX].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_GROUP_PITCH_Y, prMODEL[RP.GroupPitchY].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_BLOW_PICKUP_CNT, prMODEL[RP.StageBlowUnitPickUpCnt].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK_VAC_ON_PICKUP, prMACHINE[CP.StagePickupMovingVac].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.UNIT_INSPECTION_NG_COUNT, prMODEL[RP.SizeNGOverCnt].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.SELECT_MAP_BLOCK, prMACHINE[CP.SelectStage].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.SELECT_HEAD, prMACHINE[CP.SelectHead].ToString());

            nVALUE = PK_[0, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK1, nVALUE.ToString());
            nVALUE = PK_[1, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK2, nVALUE.ToString());
            nVALUE = PK_[2, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK3, nVALUE.ToString());
            nVALUE = PK_[3, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK4, nVALUE.ToString());
            nVALUE = PK_[4, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK5, nVALUE.ToString());
            nVALUE = PK_[5, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD1_PK6, nVALUE.ToString());

            nVALUE = PK_[0 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK1, nVALUE.ToString());
            nVALUE = PK_[1 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK2, nVALUE.ToString());
            nVALUE = PK_[2 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK3, nVALUE.ToString());
            nVALUE = PK_[3 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK4, nVALUE.ToString());
            nVALUE = PK_[4 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK5, nVALUE.ToString());
            nVALUE = PK_[5 + CNT_.PKR, 0] == eSTATUS.NONE ? 0 : 1;
            SUBFRM_.gSecsGem.SetSVID(CSVID.USE_HD2_PK6, nVALUE.ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.SELECT_TRAY_UNLOADER_MODE, prMACHINE[CP.TrayUnloadingMode].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.SELECT_TRAY_STACKER_MODE, prMACHINE[CP.SelectStackerUnloading].ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.SELECT_TRAY_CONV_MODE, prMACHINE[CP.SelectConveyorUnloading].ToString());

            SUBFRM_.gSecsGem.SetSVID(CSVID.GOOD_TRAY1_FIRST_POS_X, mtDATA[M.TRIGGER1, P.Feeder1].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.GOOD_TRAY1_FIRST_POS_Y, mtDATA[M.TrayFeeder1, P.HD1TrayPocket].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.GOOD_TRAY2_FIRST_POS_X, mtDATA[M.TRIGGER1, P.Feeder2].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.GOOD_TRAY2_FIRST_POS_Y, mtDATA[M.TrayFeeder2, P.HD1TrayPocket].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.REWORK_TRAY_FIRST_POS_X, mtDATA[M.TRIGGER1, P.Feeder3].Pos.ToString());
            SUBFRM_.gSecsGem.SetSVID(CSVID.REWORK_TRAY_FIRST_POS_Y, mtDATA[M.TrayFeeder3, P.HD1TrayPocket].Pos.ToString());

            //HEAD 2번 추가 필요!

        }
        public static void CurDataFDC(){
            int nVALUE;
            nVALUE = mIN[I.STAGE_VACUUM1] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK1_VAC, nVALUE.ToString());
            nVALUE = mIN[I.STAGE_VACUUM2] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.MAP_BLOCK2_VAC, nVALUE.ToString());
            nVALUE = mIN[I.ULD_CONV_READY] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.ULD_CONV_RADY, nVALUE.ToString());
            nVALUE = mIN[I.ULD_CONV_LOADING] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.ULD_CONV_LOADING, nVALUE.ToString());
            nVALUE = mIN[I.ULD_CONV_LOADING_END] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.ULD_CONV_END, nVALUE.ToString());

            nVALUE = mIN[I.DRIVER_AIR_PRESSURE] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.DRIVER_AIR, nVALUE.ToString());
            nVALUE = mIN[I.BLOW_AIR_PRESSURE] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.BLOW_AIR, nVALUE.ToString());
            nVALUE = mIN[I.STAGE_AIR_PRESSURE] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.STAGE_AIR, nVALUE.ToString());
            nVALUE = mIN[I.PICKER_AIR_PRESSURE] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.PICKER_AIR, nVALUE.ToString());

            nVALUE = mAI[0] > mSET_AI[0] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK1_VAC, mAI[0].ToString());
            nVALUE = mAI[1] > mSET_AI[1] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK2_VAC, mAI[1].ToString());
            nVALUE = mAI[2] > mSET_AI[2] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK3_VAC, mAI[2].ToString());
            nVALUE = mAI[3] > mSET_AI[3] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK4_VAC, mAI[3].ToString());
            nVALUE = mAI[4] > mSET_AI[4] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK5_VAC, mAI[4].ToString());
            nVALUE = mAI[5] > mSET_AI[5] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD1_PK6_VAC, mAI[5].ToString());
            
            nVALUE = mAI[8] > mSET_AI[8] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK1_VAC, mAI[8].ToString());
            nVALUE = mAI[9] > mSET_AI[9] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK2_VAC, mAI[9].ToString());
            nVALUE = mAI[10] > mSET_AI[10] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK3_VAC, mAI[10].ToString());
            nVALUE = mAI[11] > mSET_AI[11] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK4_VAC, mAI[11].ToString());
            nVALUE = mAI[12] > mSET_AI[12] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK5_VAC, mAI[12].ToString());
            nVALUE = mAI[13] > mSET_AI[13] ? 1 : 0;
            SUBFRM_.gSecsGem.SetSVID(CSVID.HD2_PK6_VAC, mAI[13].ToString());
            
        }
    } //DEFINE

    public class F
    {
        public static FormAuto Auto                         = new FormAuto();
        public static FormDevice Device                     = new FormDevice();
        public static FormManual Manual                     = new FormManual();
        public static FormMotor Motor                       = new FormMotor();
        public static FormRecipeCreate RecipeCreat          = new FormRecipeCreate();
        public static FormSystem System                     = new FormSystem();
        public static FormVision Vision                     = new FormVision();
    } //FORM

    public class C
    {
        #region >>SYSTEM
        public static MOTOR_STATUS[] Motion                 = new MOTOR_STATUS[2];
        public static INFO_MOTOR_LOCATION MotorLocation     = new INFO_MOTOR_LOCATION();
        public static HOMMING_STATUS Homming                = new HOMMING_STATUS();
        public static INPUT_STATUS Input                    = new INPUT_STATUS();
        public static OUTPUT_STATUS Output                  = new OUTPUT_STATUS();
        public static PK_VACUUM_STATUS PkVac                = new PK_VACUUM_STATUS();
        public static INTERLOCK Interlock                   = new INTERLOCK();
        public static WARNNING Warnning                     = new WARNNING();
        public static RUN_MANUAL Manual                     = new RUN_MANUAL();
        public static MANUAL_REPEAT ManualRepeat            = new MANUAL_REPEAT();
        public static OPERATOR Op                           = new OPERATOR();
        public static CHECK_STOP_EVENT StopEvent            = new CHECK_STOP_EVENT();
        public static TENKEY Tenkey                         = new TENKEY();
        public static RECEIVE_SAW ReceiveSaw                = new RECEIVE_SAW();
        public static SEND_SAW SendSaw                      = new SEND_SAW();
        public static RECEIVE_VISION RecieveVision          = new RECEIVE_VISION();
        public static SEND_VISION SendVision                = new SEND_VISION();
        #endregion

        #region >>SEQUENCE
        public static MAGAZINE Magazine                     = new MAGAZINE();
        public static GRIPPER Gripper                       = new GRIPPER();
        public static STRIP_PICKER StripPk                  = new STRIP_PICKER();
        public static UNIT_PICKER UnitPk                    = new UNIT_PICKER();
        public static DRY_TABLE_1 DryTable1                 = new DRY_TABLE_1();
        public static DRY_TABLE_2 DryTable2                 = new DRY_TABLE_2();
        public static HEAD_1 Head1                          = new HEAD_1();
        public static HEAD_2 Head2                          = new HEAD_2();
        public static EMPTY EmptyStacker                    = new EMPTY();
        public static TRAY_PICKER TrayPk                    = new TRAY_PICKER();
        public static GOOD_TRAY_FEEDER_1 GoodTrayFeeder1    = new GOOD_TRAY_FEEDER_1();
        public static GOOD_TRAY_FEEDER_2 GoodTrayFeeder2    = new GOOD_TRAY_FEEDER_2();
        public static REWORK_TRAY_FEEDER ReworkTrayFeeder   = new REWORK_TRAY_FEEDER();
        #endregion
    } //CLASS
    
    public class T : DATA_
    {
        #region >>SYSTEM
        public const int Motion1                            = 0;
        public const int Motion2                            = 1;
        public const int MotorLocation                      = 2;
        public const int Homming                            = 3;
        public const int Input                              = 4;
        public const int Output                             = 5;
        public const int PkVac                              = 6;
        public const int Warnning                           = 7;
        public const int Manual                             = 8;
        public const int ManualRepeat                       = 9;
        public const int Op                                 = 10;
        public const int StopEvent                          = 11;
        public const int ReceiveSaw                         = 12;
        public const int ReceiveVision                      = 13;
        #endregion

        #region >>OPTION 
        public const int Picker                             = 14;
        #endregion

        #region >>SEQUENCE
        public const int Magazine                           = 15;
        public const int Gripper                            = 16;
        public const int StripPk                            = 17;
        public const int UnitPk                             = 18;
        public const int DryTable1                          = 19;
        public const int DryTable2                          = 20;
        public const int Head1                              = 21;
        public const int Head2                              = 22;
        public const int EmptyStacker                       = 23;
        public const int TrayPk                             = 24;
        public const int GoodTrayFeeder1                    = 25;
        public const int GoodTrayFeeder2                    = 26;
        public const int ReworkTrayFeeder                   = 27;
        #endregion

        public static void Label(){
            thNotSeqThread  = new int[] { Motion1, Motion2, MotorLocation, Homming, Input, Output, PkVac, Warnning, Manual, ManualRepeat, Op, StopEvent, ReceiveSaw, ReceiveVision, Picker };
            thSeqThrad      = new int[] { Magazine, Gripper, StripPk, UnitPk, DryTable1, DryTable2, Head1, Head2, EmptyStacker, TrayPk, GoodTrayFeeder1, GoodTrayFeeder2, ReworkTrayFeeder };

            ThreadName[Motion1]                             = "MOTOR MODULE1";
            ThreadName[Motion2]                             = "MOTOR MODULE2";
            ThreadName[MotorLocation]                       = "MOTOR LOCATION";
            ThreadName[Homming]                             = "HOMMING";
            ThreadName[Input]                               = "INPUT";
            ThreadName[Output]                              = "OUTPUT";
            ThreadName[PkVac]                               = "VACUUM MODULE";
            ThreadName[Warnning]                            = "WARNNING";
            ThreadName[Manual]                              = "MANUAL";
            ThreadName[ManualRepeat]                        = "MANUAL REPEAT";
            ThreadName[Op]                                  = "OPERATOR";
            ThreadName[StopEvent]                           = "STOP EVENT";
            ThreadName[ReceiveSaw]                          = "RECEIVE SAW";
            ThreadName[ReceiveVision]                       = "REVEIVE VISION";

            ThreadName[Picker]                              = "PICKER";

            ThreadName[Magazine]                            = "MAGAZINE";
            ThreadName[Gripper]                             = "GRIPPER";
            ThreadName[StripPk]                             = "STRIP PICKER";
            ThreadName[UnitPk]                              = "UNIT PICKER";
            ThreadName[DryTable1]                           = "DRY TABLE1";
            ThreadName[DryTable2]                           = "DRY TABLE2";
            ThreadName[Head1]                               = "HEAD1";
            ThreadName[Head2]                               = "HEAD2";
            ThreadName[EmptyStacker]                        = "EMPTY STACKER";
            ThreadName[TrayPk]                              = "TRAY PICKER";
            ThreadName[GoodTrayFeeder1]                     = "GOOD TRAY FEEDER1";
            ThreadName[GoodTrayFeeder2]                     = "GOOD TRAY FEEDER2";
            ThreadName[ReworkTrayFeeder]                    = "REWORK TRAY FEEDER";
        }
    } //THREAD
}