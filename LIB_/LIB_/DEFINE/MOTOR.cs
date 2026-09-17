using Object;
using System;

namespace NSS_3310S
{
    public class M : DATA_
    {
#if _NSS3300
        //PCI-N404
        public const int TRIGGER1       = 0;   //HEAD X1
        public const int TRIGGER2       = 1;   //HEAD X2
        public const int SPARE1         = 2;
        public const int SPARE2         = 3;
        //ML-III
        public const int ElvY           = 4;    //ELEVATOR Y AXIS
        public const int ElvZ           = 5;    //ELEVATOR Z AXIS
        public const int Rail           = 6;    //RAIL AXIS
        public const int GrpX           = 7;    //GRIPPER X AXIS
        public const int StripPkX       = 8;    //STRIP PICKER X AXIS
        public const int StripPkZ       = 9;    //STRIP PICKER Z AXIS
        public const int UnitPkX        = 10;    //UNIT PICKER X AXIS 
        public const int UnitPkZ        = 11;   //UNIT PICKER Z AXIS 
        public const int TopVisionX     = 12;   //TOP VISION X AXIS
        public const int TopVisionZ     = 13;   //TOP VISION Z AXIS 
        public const int BtnVisionY     = 14;   //BOTTOM VISION Y AXIS
        public const int Table1         = 15;   //MAP-BLOCK TABLE Y1 AXIS
        public const int Table2         = 16;   //MAP-BLOCK TABLE Y2 AXIS
        public const int TrayFeeder1    = 17;   //OK-TRAY FEEDER Y1 AXIS
        public const int TrayFeeder2    = 18;   //OK-TRAY FEEDER Y2 AXIS
        public const int TrayFeeder3    = 19;   //NG-TRAY FEEDER Y AXIS
        //ML-III
        public const int EmptyElv       = 20;   //EMPTY ELEVATOR Z AXIS
        public const int TrayPickerZ    = 21;   //TRAY PICKER Z AXIS
        public const int TrayPickerX    = 22;   //TRAY PICKER X AXIS
        public const int X1Z12          = 23;   //HEAD X1 PICKER 1/2
        public const int X1Z34          = 24;   //HEAD X1 PICKER 3/4 
        public const int X1Z56          = 25;   //HEAD X1 PICKER 5/6
        public const int X2Z12          = 26;   //HEAD X2 PICKER 1/2
        public const int X2Z34          = 27;   //HEAD X2 PICKER 3/4
        public const int X2Z56          = 28;   //HEAD X2 PICKER 5/6
        public const int BtnVisionZ     = 29;   //BOTTOM VISION Z AXIS
        public const int X1T            = 30;   //HEAD X1 PICKER TH
        public const int X2T            = 31;   //HEAD X2 PICKER TH
        
        public const int Barcode        = 32;    //BARCODE Y AXIS

#else
        //SAW HANDLER
        public const int ElvY                   = 0;    //ELEVATOR Y AXIS
        public const int ElvZ                   = 1;    //ELEVATOR Z AXIS
        public const int RailF                  = 2;    //RAIL FRONT AXIS
        public const int RailR                  = 3;    //RAIL REAR AXIS
        public const int GrpX                   = 4;    //GRIPPER X AXIS
        public const int Barcode                = 5;    //BARCODE Y AXIS
        public const int StripPkX               = 6;    //STRIP PICKER X AXIS
        public const int StripPkZ               = 7;    //STRIP PICKER Z AXIS
        public const int PreAlign               = 8;    //PRE ALIGN Y AXIS
        public const int UnitPkX                = 9;    //UNIT PICKER X AXIS 
        public const int UnitPkZ                = 10;   //UNIT PICKER Z AXIS 

        //SORTER HANDLER
        public const int TopVisionX             = 11;   //TOP VISION X AXIS
        public const int TopVisionZ             = 12;   //TOP VISION Z AXIS 
        public const int BtnVisionY             = 13;   //BOTTOM VISION Y AXIS
        public const int BtnVisionZ             = 14;   //BOTTOM VISION Z AXIS
        public const int Table1                 = 15;   //MAP-BLOCK TABLE Y1 AXIS
        public const int Table2                 = 16;   //MAP-BLOCK TABLE Y2 AXIS
        public const int TrayFeeder1            = 17;   //OK-TRAY FEEDER Y1 AXIS
        public const int TrayFeeder2            = 18;   //OK-TRAY FEEDER Y2 AXIS
        public const int TrayFeeder3            = 19;   //NG-TRAY FEEDER Y AXIS
        public const int EmptyElv               = 20;   //EMPTY ELEVATOR Z AXIS
        public const int TrayPickerZ            = 21;   //TRAY PICKER Z AXIS
        public const int TrayPickerX            = 22;   //TRAY PICKER X AXIS
        public const int X1T                    = 23;   //HEAD X1 PICKER TH
        public const int X2T                    = 24;   //HEAD X2 PICKER TH
        public const int X1Z12                  = 25;   //HEAD X1 PICKER 1/2
        public const int X1Z34                  = 26;   //HEAD X1 PICKER 3/4 
        public const int X1Z56                  = 27;   //HEAD X1 PICKER 5/6
        public const int X2Z12                  = 28;   //HEAD X2 PICKER 1/2
        public const int X2Z34                  = 29;   //HEAD X2 PICKER 3/4
        public const int X2Z56                  = 30;   //HEAD X2 PICKER 5/6
        public const int TRIGGER1               = 31;   //HEAD X1
        public const int TRIGGER2               = 32;   //HEAD X2
#endif

        #region >>MOTOR ARRAY
        public static int[] MAGZINE             = { ElvY, ElvZ };
        public static int[] STRIP_PK            = { StripPkX, StripPkZ };
        public static int[] UNIT_PK             = { UnitPkX, UnitPkZ };
        public static int[] DRY_TABLE           = { Table1, Table2 };
        public static int[] TOP_CAM             = { TopVisionX, TopVisionZ };
        public static int[] BTM_CAM             = { BtnVisionY, BtnVisionZ };
        public static int[] HD                  = { TRIGGER1, TRIGGER2 };
        public static int[] HD1_PK              = { X1Z12, X1Z34, X1Z56 };
        public static int[] HD2_PK              = { X2Z12, X2Z34, X2Z56 };
        public static int[] HD_TH               = { X1T, X2T };
        public static int[] TRAY_PK             = { TrayPickerX, TrayPickerZ };
        public static int[] TRAY_FEEDER         = { TrayFeeder1, TrayFeeder2, TrayFeeder3 };
        public static int[] HD1Pk               = { X1Z12, X1Z12, X1Z34, X1Z34, X1Z56, X1Z56 };
        public static int[] HD2Pk               = { X2Z12, X2Z12, X2Z34, X2Z34, X2Z56, X2Z56 };

#if _NSS3300
        public static int[] RAIL                = { Rail };
        public static int[] MN_MT = { ElvY, ElvZ, Barcode, Rail, GrpX,
                                        StripPkX, StripPkZ,
                                        UnitPkX, UnitPkZ,
                                        Table1, TopVisionX, TopVisionZ,
                                        BtnVisionY, BtnVisionZ, TRIGGER1, X1T, X1Z12, TrayFeeder1,
                                        TrayPickerX, TrayPickerZ, EmptyElv
                                    };
        public static int[] MT = { ElvY, ElvZ, Barcode, Rail, GrpX,
                                        StripPkX, StripPkZ,
                                        UnitPkX, UnitPkZ,
                                        Table1, TopVisionX, TopVisionZ,
                                        BtnVisionY, BtnVisionZ, TRIGGER1, X1T, X1Z12,
                                        TrayFeeder1, TrayFeeder2, TrayFeeder3,
                                        TrayPickerX, TrayPickerZ, EmptyElv,
                                        TopVisionX, TopVisionZ, Table1, TRIGGER1, TrayFeeder1
                                     };
#else
        public static int[] RAIL                = { RailF, RailR };
        public static int[] MN_MT = { ElvY, ElvZ, Barcode, RailF, RailR, GrpX,
                                        StripPkX, StripPkZ, PreAlign,
                                        UnitPkX, UnitPkZ,
                                        Table1, TopVisionX, TopVisionZ,
                                        BtnVisionY, BtnVisionZ, TRIGGER1, X1T, X1Z12, TrayFeeder1,
                                        TrayPickerX, TrayPickerZ, EmptyElv
                                    };
        public static int[] MT = { ElvY, ElvZ, Barcode, RailF, RailR, GrpX,
                                        StripPkX, StripPkZ, PreAlign,
                                        UnitPkX, UnitPkZ,
                                        Table1, TopVisionX, TopVisionZ,
                                        BtnVisionY, BtnVisionZ, TRIGGER1, X1T, X1Z12,
                                        TrayFeeder1, TrayFeeder2, TrayFeeder3,
                                        TrayPickerX, TrayPickerZ, EmptyElv,
                                        TopVisionX, TopVisionZ, Table1, TRIGGER1, TrayFeeder1
                                     };
#endif
        #endregion

        #region >> FUNCTION
        public static void SET_INI(){
#if _NSS3300
            DATA_.mtSPARE         = new int[] { M.SPARE1, M.SPARE2 };
            DATA_.MT_GROUP_0      = new int[] { M.ElvY, M.ElvZ, M.Rail, M.GrpX, M.Barcode, M.StripPkX, M.StripPkZ, M.UnitPkX, M.UnitPkZ };
#else
            DATA_.MT_GROUP_0 = new int[] { ElvY, ElvZ, RailF, RailR, GrpX, Barcode, StripPkX, StripPkZ, PreAlign, UnitPkX, UnitPkZ };
#endif
            DATA_.MT_GROUP_1 = new int[] { TopVisionX, TopVisionZ, BtnVisionY, BtnVisionZ };
            DATA_.MT_GROUP_2 = new int[] { Table1, Table2 };
            DATA_.MT_GROUP_3 = new int[] { TRIGGER1, X1Z12, X1Z34, X1Z56, X1T, TRIGGER2, X2Z12, X2Z34, X2Z56, X2T };
            DATA_.MT_GROUP_4 = new int[] { TrayFeeder1, TrayFeeder2, TrayFeeder3, TrayPickerX, TrayPickerZ, EmptyElv };

            DATA_.MT_GROUP_0_NAME = "HANDER ZONE";
            DATA_.MT_GROUP_1_NAME = "VISION ZONE";
            DATA_.MT_GROUP_2_NAME = "DAY-BLOCK ZONE";
            DATA_.MT_GROUP_3_NAME = "HEAD ZONE";
            DATA_.MT_GROUP_4_NAME = "TRY ZONE";

            DATA_.mtTrigger = new int[] { TRIGGER1, TRIGGER2 };
            DATA_.SubTrigger = new int[] { (int)eTRIGGER.HD1, (int)eTRIGGER.HD2 };


            DATA_.mtHD = new int[] { TRIGGER1, TRIGGER2 };
            DATA_.mtHD1_PK = new int[] { X1Z12, X1Z34, X1Z56 };
            DATA_.mtHD2_PK = new int[] { X2Z12, X2Z34, X2Z56 };
        }

        public static void IniHomeBuffer(){
            for (int i = 0; i < CNT_.MT; i++){
                mtSTS[i].bErrHome = false;
                enableHome[i] = true;
                mtSTS[i].bHomeComplete = false;
            }
        }

        public static void CLEAR_MOVECHKECK(int m, stMoveInfo mv){
            mtCHK[m].axis = m;
            mtCHK[m].posBegin = LAB_.GET_ACTPOS(m);
            mtCHK[m].pos = mv.Pos;
            mtCHK[m].spd = mv.Spd;
            mtCHK[m].acc = mv.Acc;
            mtCHK[m].dcc = mv.Dec;
            mtCHK[m].time = mv.MoveTime;
            mtCHK[m].sLog = "";
            mtCHK[m].errLog = "";
            mtCHK[m].fLog = "";
            mtCHK[m].sTime = Environment.TickCount;
            mtCHK[m].eTime = Environment.TickCount;
            mtCHK[m].timeStop = Environment.TickCount;
            mtCHK[m].coment = "";
            mtCHK[m].OnBusy = false;
            mtCHK[m].posStop = -1;
            mtCHK[m].cmd = "";
            mtCHK[m].sts = "";
            mtCHK[m].onlyStart = false;
            mtCHK[m].noChange = false;
            mtCHK[m].Ev = "";
            mtCHK[m].rslt = "";
        }

        public static bool IsNotEncMotor(int m){
            if (mtNotEncoder == null) return false;
            for (int i = 0; i < mtNotEncoder.Length; i++){
                if (m == mtNotEncoder[i]) return true;
            }
            return false;
        }

        public static bool IsNotMotor(int m){
            if (mtSPARE == null) return false;
            for (int i = 0; i < mtSPARE.Length; i++){
                if (m == mtSPARE[i]) return true;
            }
            return false;
        }

        public static bool CurrentLocationHome(int m){
            bool bFLAG = false;
            if (mtCurrentHome == null) return false;
            for (int i = 0; i < mtCurrentHome.Length; i++){
                if (m == mtCurrentHome[i]){
                    bFLAG = true;
                    break;
                }
            }
            if (!bFLAG) return false;
            LAB_.SET_POSZERO(m, 10);
            mtSTS[m].bHomeComplete = true;
            return true;
        }

        public static bool CurPkPosition(int mt, int pos){
            if (2 < Math.Abs(mtSTS[mt].CurrentPosition - mtDATA[mt, pos].Pos)) return false;
            return true;
        }

        public static bool MTCEP(int m, int p){
            if (mtSTS[m].bAlram) return false;
            if (bBD) return true;
            if (mtSTS[m].CurrentPosition != 0 && Math.Abs(mtSTS[m].CurrentPosition - mtDATA[m, p].Pos) < 0.1) return true;
            else return false;
        }

        public static double GetPosData(int axis, int ipos){
            return mtDATA[axis, ipos].Pos;
        }// 모타 데이타값

        public static stMoveInfo GetMoveInfo(int m, int pos){
            stMoveInfo mv = mtDATA[m, pos];
            return mv;
        } //해당 모터 정보 구조체 가져옴.

        public static void GetMoveInfo(int[] m, int[] p, ref double dSPD, ref double dACC, ref double dDEC, ref int tm){
            dSPD = 0; dACC = 0; dDEC = 0; tm = 0;
            for (int i = 0; i < m.Length; i++){
                if (dSPD < mtDATA[m[i], p[i]].Spd) dSPD = mtDATA[m[i], p[i]].Spd;
                if (dACC < mtDATA[m[i], p[i]].Acc) dACC = mtDATA[m[i], p[i]].Acc;
                if (dDEC < mtDATA[m[i], p[i]].Dec) dDEC = mtDATA[m[i], p[i]].Dec;
                if (tm < mtDATA[m[i], p[i]].MoveTime) tm = mtDATA[m[i], p[i]].MoveTime;
            }
        }

        public static void GetMoveInfoRaw(int m, int p, ref double pos, ref double spd, ref double acc, ref double dcc, ref int tm){
            pos = mtDATA[m, p].Pos;
            spd = mtDATA[m, p].Spd;
            acc = mtDATA[m, p].Acc;
            dcc = mtDATA[m, p].Dec;
            tm = mtDATA[m, p].MoveTime;
        }

        public static void SetMoveInfoRaw(int m, int GET_POS, int SET_POS) { mtDATA[m, SET_POS] = mtDATA[m, GET_POS]; }

        public static void GetPosXYInfo(int mX, int mY, int pX, int pY, ref dxy dXY){
            dXY.x = mtDATA[mX, pX].Pos;
            dXY.y = mtDATA[mY, pY].Pos;
        }
        public static void GetPosXYZInfo(int mX, int mY, int mZ, int pX, int pY, int pZ, ref dxyz dXYZ){
            dXYZ.x = mtDATA[mX, pX].Pos;
            dXYZ.y = mtDATA[mY, pY].Pos;
            dXYZ.z = mtDATA[mZ, pZ].Pos;
        }
        public static void GetPosXYTInfo(int mX, int mY, int mT, int pX, int pY, int pT, ref dxyt dXYT){
            dXYT.x = mtDATA[mX, pX].Pos;
            dXYT.y = mtDATA[mY, pY].Pos;
            dXYT.t = mtDATA[mT, pT].Pos;
        }
        public static void GetPosXYZTInfo(int mX, int mY, int mZ, int mT, int pX, int pY, int pZ, int pT, ref dxyzt dXYZT){
            dXYZT.x = mtDATA[mX, pX].Pos;
            dXYZT.y = mtDATA[mY, pY].Pos;
            dXYZT.z = mtDATA[mZ, pZ].Pos;
            dXYZT.t = mtDATA[mT, pT].Pos;
        }
        public static void GetPosXYInfo(double dX, double dY, ref dxy dXY){
            dXY.x = dX;
            dXY.y = dY;
        }
        public static void GetPosXYZInfo(double dX, double dY, double dZ, ref dxyz dXYZ){
            dXYZ.x = dX;
            dXYZ.y = dY;
            dXYZ.z = dZ;
        }
        public static void GetPosXYTInfo(double dX, double dY, double dT, ref dxyt dXYT){
            dXYT.x = dX;
            dXYT.y = dY;
            dXYT.t = dT;
        }
        public static void GetPosXYZTInfo(double dX, double dY, double dZ, double dT, ref dxyzt dXYZT){
            dXYZT.x = dX;
            dXYZT.y = dY;
            dXYZT.z = dZ;
            dXYZT.t = dT;
        }
        public static void GetPosXYInfo(double dX, double dY, ref double rX, ref double rY){
            rX = dX;
            rY = dY;
        }
        public static void GetPosXYZInfo(double dX, double dY, double dZ, ref double rX, ref double rY, ref double rZ){
            rX = dX;
            rY = dY;
            rZ = dZ;
        }
        public static void GetPosXYTInfo(double dX, double dY, double dT, ref double rX, ref double rY, ref double rT){
            rX = dX;
            rY = dY;
            rT = dT;
        }
        public static void GetPosXYZTInfo(double dX, double dY, double dZ, double dT, ref double rX, ref double rY, ref double rZ, ref double rT){
            rX = dX;
            rY = dY;
            rZ = dZ;
            rT = dT;
        }

        public static string GET_MOVE_RESULT(int m){
            string s = "[" + MtName[m] + "]" + ETC.CrLf;
            s += "CMD = " + mtCHK[m].cmd + ETC.CrLf;
            s += "COMENT = " + mtCHK[m].coment + ETC.CrLf;

            s += "========================================================" + ETC.CrLf + ETC.CrLf;
            s += "POS = " + string.Format("{0:0.000}", mtCHK[m].pos) + ETC.CrLf;
            s += "SPEED = " + string.Format("{0:0.000}", mtCHK[m].spd) + ETC.CrLf;
            s += "ACC = " + mtCHK[m].acc.ToString() + ETC.CrLf;
            s += "DCC = " + mtCHK[m].dcc.ToString() + ETC.CrLf;
            s += "MOVETIME = " + mtCHK[m].time.ToString() + ETC.CrLf;
            s += "TOLLER = " + string.Format("{0:0.000}", mtCHK[m].toller) + ETC.CrLf;
            s += "MOVE-ONLY = " + mtCHK[m].onlyStart.ToString() + ETC.CrLf;
            s += "NO-CHANGE = " + mtCHK[m].noChange.ToString() + ETC.CrLf;

            s += "========================================================" + ETC.CrLf + ETC.CrLf;
            s += "POS-BEGIN = " + string.Format("{0:0.000}", mtCHK[m].posBegin) + ETC.CrLf;
            s += "POS-END = " + string.Format("{0:0.000}", mtCHK[m].posStop) + ETC.CrLf;
            s += "POS-GAP = " + string.Format("{0:0.000}", mtCHK[m].posGap) + ETC.CrLf;
            s += "OnBusy = " + mtCHK[m].time.ToString() + ETC.CrLf;
            s += "RUN-TIME = " + mtCHK[m].rTime.ToString() + ETC.CrLf;

            s += "========================================================" + ETC.CrLf + ETC.CrLf;
            s += "ERROR = " + mtCHK[m].errLog + ETC.CrLf;
            s += "STATUS = " + mtCHK[m].sts + ETC.CrLf;
            s += "EVENT = " + mtCHK[m].Ev + ETC.CrLf;
            return s;
        }
        #endregion << FUNCTION
    } //MOTOR DEFINE
    public class P : DATA_
    {
        public const int Ready                  = 0;                    //대기 위치
        public const int CAL_                   = CNT_.POS - 1;         //연산 버퍼

#region >>ELAVATOR Y,Z AXIS
        public const int Recive                 = 1;                    //카세트 로딩 위치
        public const int Give                   = 2;                    //카세트 언로딩 위치
        public const int FirstSlot              = 10;//3;               //첫번째 슬롯 위치
#endregion

#region >>RAIL 
        public const int StripIn                = 10;//1;                //카세트에서 스트립 투입 위치 
        public const int StripAlign             = 11;//2;                //스트립 로딩 위치 
        public const int Open                   = 12;//3;                //스트립 픽업 OPEN 위치
#endregion

#region >>GRIPPER
        public const int StripPick              = 1;                //스트립 그립 위치
        public const int StripOpn               = 3;                //스트립 그립 OPEN 위치
        public const int StripLoad              = 4;                //스트립 픽업 위치

        public const int RecipStripLoad         = 10;               //스트립 픽업 위치
        public const int StripBcd               = 11;                //스트립 바코드 리딩 위치
#endregion

#region >>BARCODE
        public const int BcdRead                = 10;               //스트립 바코드 리딩 위치
#endregion

#region >>PRE-ALIGN
        public const int StripTrigger1          = 10;               //스트립 프리-얼라인 위치
        public const int StripTrigger2          = 11;
#endregion

#region >>STRIP PICKER
        public const int StripPckUp             = 1;                //스트립 픽업 위치
        public const int StripPlc               = 2;                //스트립 플레이스 위치
        public const int FirstTrigger           = 10;               //스트립 첫번째 매칭 위치
        public const int SecondTrigger          = 11;               //스트립 두번째 매칭 위치
#endregion

#region >>UNIT PICKER
        public const int UnitPckUp              = 1;                //유닛 픽업 위치
        public const int BrushStart             = 4;                //브러쉬 시작 위치
        public const int Cleaner                = 5;                //클리너 박스 위치
        public const int AirBlowStart           = 6;                //에어샤워 시작 위치
        public const int PlacePallet1           = 7;                //맵-블록1 내려놓는 위치
        public const int PlacePallet2           = 8;                //맵-블록2 내려놓는 위치

        public const int Scrap1                 = 10;//2;                //첫번째 스크랩 버리는 위치
        public const int Scrap2                 = 11;//3;                //두번째 스크랩 버리는 위치
#endregion

#region >>TOP VISION
        public const int CalZigCenter_MB1       = 1;    //켈' 지그 중심 위치 
        public const int CalZigCenter_MB2       = 2;    //켈' 지그 중심 위치 

        public const int Pallet1_Unit           = 10;               //맵-블록1 좌측 하단 포켓 중심 위치
        public const int Pallet2_Unit           = 11;               //맵-블록2 좌측 하단 포켓 중심 위치
#endregion

#region >>BOTTOM VISION
        public const int HD1CamCenter           = 1;                //헤드 1 카메라 중심 위치
        public const int HD1PkCenter            = 2;                //헤드 1 피커 1번 중심 위치
        public const int HD2CamCenter           = 3;                //헤드 2 카메라 중심 위치
        public const int HD2PkCenter            = 4;                //헤드 2 피커 1번 중심 위치

        public const int pHD1_PK                = 5;                //헤드 1 피커 1 홀 중심 위치 (피커 기준 Z 위치)
        public const int pHD2_PK                = 6;                //헤드 2 피커 1 홀 중심 위치 (피커 기준 Z 위치)
#endregion

#region >>MAP-BLOCK 1/2
        public const int RecieveUnit            = 1;                //유닛 받는 위치
        public const int AirShowerStart         = 2;                //에어 샤워 시작 위치
        public const int CalZigCenter           = 3;                //켈' 지그 중심 위치 

        public const int HD1MasterZigCenter     = 8; // HD1 CAM 마스터 지그 센터 위치
        public const int HD2MasterZigCenter     = 9; // HD2 CAM 마스터 지그 센터 위치

        public const int TopVision_Unit         = 10;               //상부 비전 유닛 보는 위치
        public const int HD1_Unit               = 11;               //헤드1 카메라 유닛 보는 위치
        public const int HD2_Unit               = 12;               //헤드1 카메라 유닛 보는 위치
#endregion

#region >>TRAY FEEDER (1/2/NG[1,2,10,11])
        public const int TrayLoad               = 1;            //빈-트레이 공급 위치
        public const int Staker                 = 2;            //트레이 스태커 배출 위치
        public const int FastPsh                = 3;            //OK-트레이 푸쉬 위치
        public const int Psh                    = 4;            //OK-트레이 핑거 푸쉬 시작 위치
        public const int TrayUnload             = 5;            //OK-트레이 콘베어 배출 위치

        public const int HD1TrayPocket          = 10;           //헤드1 트레이 첫번째 포켓 중심 위치
        public const int HD2TrayPocket          = 11;           //헤드2 트레이 첫번째 포켓 중심 위치
#endregion

#region >>EMPTY ELEVATOR
        public const int EmptyTraySupply        = 1;            //빈-트레이 공급 위치
        public const int EmptyTrayHold          = 2;            //빈-트레이 스토퍼 락 위치
        public const int EmptyTraySafeArrial    = 3;            //빈-트레이 레일 안착 위치
#endregion

#region >>TRAY PICKER
        public const int TrayPckUp              = 1;            //빈-트레이 픽업 위치
        public const int OKTrayPlc              = 2;            //OK-트레이 플레이스 위치
        public const int NGTrayPlc              = 3;            //NG-트레이 플레이스 위치
#endregion

#region >>PICKER TH
        public const int PkPckUp                = 10;           //피커 픽업 위치
        public const int PkPlc                  = 11;           //피커 플레이스 위치
#endregion

#region >>PICKER
        public const int OddPckUp               = 1;            //피커 1,3,5 픽업 위치
        public const int EvenPckUp              = 2;            //피커 2,4,6 픽업 위치
        public const int OddPlc                 = 3;            //피커 1,3,5 플레이스 위치
        public const int EvenPlc                = 4;            //피커 2,4,6 플레이스 위치
#endregion

#region >>HEAD
        public const int BTMCamCenter           = 1;            //하부 카메라 중심 위치
        public const int PkCenter               = 2;            //피커1번 중심 위치
        public const int Reject                 = 3;            //유닛 버리는 박스 위치

        public const int Pallet1MasterZigCenter = 8;    // stage1 마스터 지그 센터 위치        
        public const int Pallet2MasterZigCenter = 9;    // stage2 마스터 지그 센터 위치

        public const int Pallet1                = 10;           // 맵-블록1 우측 하단 포켓 중심 위치
        public const int Pallet2                = 11;           // 맵-블록2 우측 하단 포켓 중심 위치
        public const int Feeder1                = 12;           // OK 트레이1 첫번째 포켓 중심 위치
        public const int Feeder2                = 13;           // OK 트레이2 첫번째 포켓 중심 위치
        public const int Feeder3                = 14;           // NG 트레이 첫번째 포켓 중심 위치
#endregion

#region >>POS TEACHING ARRAY
        public static int[] MAGZINE             = { Ready, Recive, Give, FirstSlot };
        public static int[] RAIL                = { Ready, StripIn, StripAlign, Open };
        public static int[] GRIPPER             = { Ready, StripPick, StripBcd, StripOpn, StripLoad, RecipStripLoad };
        public static int[] BARCODE             = { Ready, BcdRead };
        public static int[] PRE_AIGN            = { Ready, StripTrigger1, StripTrigger2 };
        public static int[] STRIP_PK            = { Ready, StripPckUp, StripPlc, FirstTrigger, SecondTrigger };
        public static int[] UNIT_PK             = { Ready, UnitPckUp, Scrap1, Scrap2, BrushStart, Cleaner, AirBlowStart, PlacePallet1, PlacePallet2 };
        public static int[] DRY_TABLE           = { Ready, RecieveUnit, AirShowerStart, CalZigCenter, TopVision_Unit, HD1_Unit, HD2_Unit };
        public static int[] TOP_CAM             = { Ready, CalZigCenter_MB1, CalZigCenter_MB2, Pallet1_Unit, Pallet2_Unit };
        public static int[] BTM_CAM             = { Ready, HD1CamCenter, HD1PkCenter, HD2CamCenter, HD2PkCenter, pHD1_PK, pHD2_PK };
        public static int[] HD                  = { Ready, BTMCamCenter, PkCenter, Reject, Pallet1, Pallet2, Feeder1, Feeder2, Feeder3 };
        public static int[] HD_PK               = { Ready, OddPckUp, EvenPckUp, OddPlc, EvenPlc };
        public static int[] HD_TH               = { Ready, PkPckUp, PkPlc };
        public static int[] TRAY_PK             = { Ready, TrayPckUp, OKTrayPlc, NGTrayPlc };
        public static int[] GOOD_TRAY_FEEDER    = { Ready, TrayLoad, Staker, FastPsh, Psh, TrayUnload, HD1TrayPocket, HD2TrayPocket };
        public static int[] REWORK_TRAY_FEEDER  = { Ready, TrayLoad, Staker, HD1TrayPocket, HD2TrayPocket };
        public static int[] EMPTY               = { Ready, EmptyTraySupply, EmptyTrayHold, EmptyTraySafeArrial };
#endregion

#region >>POS MOVING ARRAY
        public static int[] SCRAP               = { Scrap1, Scrap2 };
        public static int[] PLACE_PALLET        = { PlacePallet1, PlacePallet2 };
        public static int[] TopCam_Pallet       = { Pallet1_Unit, Pallet2_Unit };
        public static int[] BtmCam_Cam          = { HD1CamCenter, HD2CamCenter };
        public static int[] BtmCam_Pk           = { HD1PkCenter, HD2PkCenter };
        public static int[] BtmCam_PkCal        = { pHD1_PK, pHD2_PK };
        public static int[] HD_Pallet           = { HD1_Unit, HD2_Unit };
        public static int[] HD_PIC              = { Pallet1, Pallet2 };
        public static int[] HD_PLC              = { Feeder1, Feeder2, Feeder3 };
        public static int[] PK_PIC              = { OddPckUp, EvenPckUp,
                                                    OddPckUp, EvenPckUp,
                                                    OddPckUp, EvenPckUp
                                                };
        public static int[] PK_PLC              = { OddPlc, EvenPlc,
                                                    OddPlc, EvenPlc,
                                                    OddPlc, EvenPlc
                                                };
        public static int[] HDMasterZig         = { Pallet1MasterZigCenter, Pallet2MasterZigCenter };
        public static int[] StageMasterZig      = { HD1MasterZigCenter, HD2MasterZigCenter };
        public static int[] Tray_Place          = { HD1TrayPocket, HD2TrayPocket };
        public static int[] TrayPkr             = { TrayPckUp, OKTrayPlc, NGTrayPlc };
        public static int[] TrayPlc             = { OKTrayPlc, OKTrayPlc, NGTrayPlc };
        public static int[] TopCamCalZigCenter  = { CalZigCenter_MB1, CalZigCenter_MB2 };
#endregion

        public static double StripPkZSafetyPos;
        public static double UnitPkZSafetyPos;

        #region >> FUNCTION
        public static void GetHandlerPkZSafetyPos(){
            double[] dStripPkZ                  = { DATA_.mtDATA[M.StripPkZ, Ready].Pos, DATA_.mtDATA[M.StripPkZ, FirstTrigger].Pos, DATA_.mtDATA[M.StripPkZ, SecondTrigger].Pos };
            double[] dUnitPkZ                   = { DATA_.mtDATA[M.UnitPkZ, Ready].Pos, DATA_.mtDATA[M.UnitPkZ, Scrap1].Pos, DATA_.mtDATA[M.UnitPkZ, Scrap2].Pos, DATA_.mtDATA[M.UnitPkZ, AirBlowStart].Pos };

            DATA_.cMATH.IsGetArrMaxValue(dStripPkZ, ref StripPkZSafetyPos);
            DATA_.cMATH.IsGetArrMaxValue(dUnitPkZ, ref UnitPkZSafetyPos);

            StripPkZSafetyPos   += 1;
            UnitPkZSafetyPos    += 1;
        }

        public static void INI_PICKER_INFO(){
            for (int n = 0; n < PICKER.Length; n++){
                PICKER[n].finger = new stFinger[CNT_.PKR];
                PICKER[n].CMD = "";
            } // 피커정보 초기화
        }

        public static void INFO_PICKER_DATA_CLEANER(eHD HEAD, int SetCurWorkHD, int nPnPStage){
            for (int z = 0; z < CNT_.PKR; z++){
                PICKER[(int)HEAD].finger[z].valid = false;
                PICKER[(int)HEAD].finger[z].pocketX = 0;
                PICKER[(int)HEAD].finger[z].pocketY = 0;
                PICKER[(int)HEAD].finger[z].iResult = 0;
                PICKER[(int)HEAD].finger[z].prsX = 0;
                PICKER[(int)HEAD].finger[z].prsY = 0;
                PICKER[(int)HEAD].finger[z].prsR = 0;
                PICKER[(int)HEAD].finger[z].prsOffsetX = 0;
                PICKER[(int)HEAD].finger[z].prsOffsetY = 0;
                PICKER[(int)HEAD].finger[z].prsOffsetT = 0;
            }
            PICKER[(int)HEAD].StagePnP = nPnPStage;
            IsLONG[SetCurWorkHD] = (int)HEAD;
        }

        public static void HDBufferClear(ref bool NG, ref bool REJECT, ref bool INSPECTION){
            NG = false;
            REJECT = false;
            INSPECTION = false;
        }
        #endregion << FUNCTION
    } //MOTOR POSITION
}