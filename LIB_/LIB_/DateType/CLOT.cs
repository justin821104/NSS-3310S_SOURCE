using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace LIB_.DateType
{
    public class CLOT
    {
        public static PANEL_INFO[] InfoStrip        = new PANEL_INFO[CNT_.THREAD];
        public static string SawStageStripBarcode   = "";
        public static int SawStageStripIndex        = 0;
        public static void SEND_STRIP_INFO(int Give, int Get){
            InfoStrip[Get].Barcode = InfoStrip[Give].Barcode;
            InfoStrip[Get].Index = InfoStrip[Give].Index;
        }
        public static void RESET_STRIP_INFO(int nTH){
            InfoStrip[nTH].Barcode  = "";
            InfoStrip[nTH].Index    = 0;
        }
        public static void RECEIVE_SAW_STRIP_INFO(int nTH){
            SawStageStripBarcode = InfoStrip[nTH].Barcode;
            SawStageStripIndex = InfoStrip[nTH].Index;
        }
        public static void SEND_SAW_STRIP_INFO(int nTH){
            InfoStrip[nTH].Barcode = SawStageStripBarcode;
            InfoStrip[nTH].Index   = SawStageStripIndex;
        }
        public static void RESET_SAW_STRIP_INFO(){
            SawStageStripBarcode    = "";
            SawStageStripIndex      = 0;
        }
        public static void ClearStripBarcodeInfo(){
            for (int n = 0; n < InfoStrip.Length; n++){
                InfoStrip[n].Barcode    = "";
                InfoStrip[n].Index      = 0;
            }
            RESET_SAW_STRIP_INFO();
        }
        public struct PANEL_INFO{
            public int Index;                   //PANEL 수량
            public string Barcode;              //바코드
        }


        public static string CurLotID               = "";
        public static int CurLotStripCnt            = 0;
        public static int nLotType                  = 0;
        public static int nLotCnt                   = 0;
        public static int nEndLotCnt                = 0;
        public static sLOT_INFO[] FINISH_LOT        = new sLOT_INFO[3]; //완료 LOT 정보
        public static sLOT_INFO[] WORK_LOT          = new sLOT_INFO[1]; //진행 LOT 정보
        public static sLOT_INFO GET_LOT             = new sLOT_INFO(); //MES에서 받은 LOT 정보
        public static sLOT_INFO DEL_LOT             = new sLOT_INFO(); //MES 삭제
        public static sLOT_INFO CANCEL_LOT          = new sLOT_INFO(); //MES 취소
        public static RTN_LOT_CANCEL RETURN_CANCEL  = new RTN_LOT_CANCEL(); //취소시 리턴값
        public static bool bMES_START               = false;

        public static bool bLotLoss                 = false;
        public static bool bEqpChange               = false;
        public static bool bLotValidationSusses     = false;
        public static bool bLotCanceled             = false;
        public static bool bFirstLot                = false;

        public static string LotLossStartTime;
        public static string LotLossEndTime;

        public static void CLEAR_FINISH_LOT(){
            for (int i = 0; i < FINISH_LOT.Length; i++){
                FINISH_LOT[i].WorkScope             = "";   //대기,진행,완료,강제완료
                FINISH_LOT[i].ItsID                 = "";   //ITS ID
                FINISH_LOT[i].Recipe                = "";

                FINISH_LOT[i].LotID                 = "";   //LOT ID
                FINISH_LOT[i].ToolNo                = "";   //TOL NUMBER (관리번호 기종)
                FINISH_LOT[i].ProcCD                = "";
                FINISH_LOT[i].ProcName              = "";
                FINISH_LOT[i].WorkCondition         = "";
                FINISH_LOT[i].ProcCondition_1       = "";
                FINISH_LOT[i].ProcCondition_2       = "";
                FINISH_LOT[i].ProcCondition_3       = "";
                FINISH_LOT[i].ProcCondition_4       = "";

                FINISH_LOT[i].LotType               = -1;   //LOT TYPE => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
                FINISH_LOT[i].Qty                   = 0;    //MES 수량
                FINISH_LOT[i].ProductType           = "";
                FINISH_LOT[i].ITS                   = 0;    //ITS 사용 유무
                FINISH_LOT[i].ITS_LotID_IN          = "";   //ITS ID 대덕전자(대덕 번호)
                FINISH_LOT[i].ITS_LotID_CT          = "";   //ITS ID 고객사(고객사 번호)

                FINISH_LOT[i].UnitSizeX             = 0;    //사이즈X
                FINISH_LOT[i].UnitSizeY             = 0;    //사이즈Y
                FINISH_LOT[i].UnitSize_USL          = 0;    //사이즈 상한값
                FINISH_LOT[i].UnitSize_LSL          = 0;    //사이즈 하한값
                FINISH_LOT[i].Thick                 = 0;    //두께정보
                FINISH_LOT[i].Thick_USL             = 0;    //두께 상한값
                FINISH_LOT[i].Thick_LSL             = 0;    //두께 하한값

                FINISH_LOT[i].ABFMATERIAL           = "";
                FINISH_LOT[i].LANDPKGX              = 0;
                FINISH_LOT[i].LANDPKGX_UPPER        = 0;
                FINISH_LOT[i].LANDPKGX_LOWER        = 0;
                FINISH_LOT[i].LANDPKGY              = 0;
                FINISH_LOT[i].LANDPKGY_UPPER        = 0;
                FINISH_LOT[i].LANDPKGY_LOWER        = 0;

                FINISH_LOT[i].InCnt                 = 0;    //panel 투입 수량
                FINISH_LOT[i].OutCnt                = 0;    //panel 배출 수량
                FINISH_LOT[i].CurCnt                = 0;    //배출 수량과 동일
                FINISH_LOT[i].PassCnt               = 0;    //제외 매수

                FINISH_LOT[i].LoadingCount          = 0;    //투입 매수
                FINISH_LOT[i].ExceptCount           = 0;    //제외 매수
                FINISH_LOT[i].UnloadingCount        = 0;    //배출 매수
                FINISH_LOT[i].WorkSort              = "";   //작업 종류  => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
            }
            nEndLotCnt = 0;
        }
        public static void CLEAR_WORK_LOT(int nIndex){
            WORK_LOT[nIndex].WorkScope              = "";   //대기,진행,완료,강제완료
            WORK_LOT[nIndex].ItsID                  = "";   //ITS ID
            WORK_LOT[nIndex].Recipe                 = "";

            WORK_LOT[nIndex].LotID                  = "";   //LOT ID
            WORK_LOT[nIndex].ToolNo                 = "";   //TOL NUMBER (관리번호 기종)
            WORK_LOT[nIndex].ProcCD                 = "";
            WORK_LOT[nIndex].ProcName               = "";
            WORK_LOT[nIndex].WorkCondition          = "";
            WORK_LOT[nIndex].ProcCondition_1        = "";
            WORK_LOT[nIndex].ProcCondition_2        = "";
            WORK_LOT[nIndex].ProcCondition_3        = "";
            WORK_LOT[nIndex].ProcCondition_4        = "";

            WORK_LOT[nIndex].LotType                = -1;   //LOT TYPE => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
            WORK_LOT[nIndex].Qty                    = 0;    //MES 수량
            WORK_LOT[nIndex].ProductType            = "";
            WORK_LOT[nIndex].ITS                    = 0;    //ITS 사용 유무
            WORK_LOT[nIndex].ITS_LotID_IN           = "";   //ITS ID 대덕전자(대덕 번호)
            WORK_LOT[nIndex].ITS_LotID_CT           = "";   //ITS ID 고객사(고객사 번호)

            WORK_LOT[nIndex].UnitSizeX              = 0;    //사이즈X
            WORK_LOT[nIndex].UnitSizeY              = 0;    //사이즈Y
            WORK_LOT[nIndex].UnitSize_USL           = 0;    //사이즈 상한값
            WORK_LOT[nIndex].UnitSize_LSL           = 0;    //사이즈 하한값
            WORK_LOT[nIndex].Thick                  = 0;    //두께정보
            WORK_LOT[nIndex].Thick_USL              = 0;    //두께 상한값
            WORK_LOT[nIndex].Thick_LSL              = 0;    //두께 하한값

            WORK_LOT[nIndex].ABFMATERIAL            = "";
            WORK_LOT[nIndex].LANDPKGX               = 0;
            WORK_LOT[nIndex].LANDPKGX_UPPER         = 0;
            WORK_LOT[nIndex].LANDPKGX_LOWER         = 0;
            WORK_LOT[nIndex].LANDPKGY               = 0;
            WORK_LOT[nIndex].LANDPKGY_UPPER         = 0;
            WORK_LOT[nIndex].LANDPKGY_LOWER         = 0;

            WORK_LOT[nIndex].InCnt                  = 0;    //panel 투입 수량
            WORK_LOT[nIndex].OutCnt                 = 0;    //panel 배출 수량
            WORK_LOT[nIndex].CurCnt                 = 0;    //배출 수량과 동일
            WORK_LOT[nIndex].PassCnt                = 0;    //제외 매수

            WORK_LOT[nIndex].LoadingCount           = 0;    //투입 매수
            WORK_LOT[nIndex].ExceptCount            = 0;    //제외 매수
            WORK_LOT[nIndex].UnloadingCount         = 0;    //배출 매수
            WORK_LOT[nIndex].WorkSort               = "";   //작업 종류  => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
        }
        public static void CLEAR_WORK_LOT(){
            for (int i = 0; i < WORK_LOT.Length; i++){
                CLEAR_WORK_LOT(i);
            }
            nLotCnt = 0;
        }
        public static void CLEAR_GET_LOT(){
            GET_LOT.WorkScope                       = "";   //대기,진행,완료,강제완료
            GET_LOT.ItsID                           = "";   //ITS ID
            GET_LOT.Recipe                          = "";

            GET_LOT.LotID                           = "";   //LOT ID
            GET_LOT.ToolNo                          = "";   //TOL NUMBER (관리번호 기종)
            GET_LOT.ProcCD                          = "";
            GET_LOT.ProcName                        = "";
            GET_LOT.WorkCondition                   = "";
            GET_LOT.ProcCondition_1                 = "";
            GET_LOT.ProcCondition_2                 = "";
            GET_LOT.ProcCondition_3                 = "";
            GET_LOT.ProcCondition_4                 = "";

            GET_LOT.LotType                         = -1;   //LOT TYPE => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
            GET_LOT.Qty                             = 0;    //MES 수량
            GET_LOT.ProductType                     = "";
            GET_LOT.ITS                             = 0;    //ITS 사용 유무
            GET_LOT.ITS_LotID_IN                    = "";   //ITS ID 대덕전자(대덕 번호)
            GET_LOT.ITS_LotID_CT                    = "";   //ITS ID 고객사(고객사 번호)

            GET_LOT.UnitSizeX                       = 0;    //사이즈X
            GET_LOT.UnitSizeY                       = 0;    //사이즈Y
            GET_LOT.UnitSize_USL                    = 0;    //사이즈 상한값
            GET_LOT.UnitSize_LSL                    = 0;    //사이즈 하한값
            GET_LOT.Thick                           = 0;    //두께정보
            GET_LOT.Thick_USL                       = 0;    //두께 상한값
            GET_LOT.Thick_LSL                       = 0;    //두께 하한값

            GET_LOT.ABFMATERIAL                     = "";
            GET_LOT.LANDPKGX                        = 0;
            GET_LOT.LANDPKGX_UPPER                  = 0;
            GET_LOT.LANDPKGX_LOWER                  = 0;
            GET_LOT.LANDPKGY                        = 0;
            GET_LOT.LANDPKGY_UPPER                  = 0;
            GET_LOT.LANDPKGY_LOWER                  = 0;

            GET_LOT.InCnt                           = 0;    //panel 투입 수량
            GET_LOT.OutCnt                          = 0;    //panel 배출 수량
            GET_LOT.CurCnt                          = 0;    //배출 수량과 동일
            GET_LOT.PassCnt                         = 0;    //제외 매수

            GET_LOT.LoadingCount                    = 0;    //투입 매수
            GET_LOT.ExceptCount                     = 0;    //제외 매수
            GET_LOT.UnloadingCount                  = 0;    //배출 매수
            GET_LOT.WorkSort                        = "";   //작업 종류  => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
        }
        public static void FinishLot(bool bFLAG){
            for (int i = 0; i < FINISH_LOT.Length; i++){
                if (i >= (FINISH_LOT.Length - 1)){
                    FINISH_LOT[(FINISH_LOT.Length - 1) - i]           = WORK_LOT[0];
                    FINISH_LOT[(FINISH_LOT.Length - 1) - i].WorkScope = bFLAG ? "강제완료" : "완료";
                }
                else { FINISH_LOT[(FINISH_LOT.Length - 1) - i] = FINISH_LOT[(FINISH_LOT.Length - 2) - i]; }
            }
            for (int i = 0; i < WORK_LOT.Length; i++){
                if (i >= (WORK_LOT.Length - 1)) { CLEAR_WORK_LOT(i); }
                else                            { WORK_LOT[i] = WORK_LOT[i + 1]; }
            }
            nLotCnt -= 1;
            nEndLotCnt = (nEndLotCnt >= 10) ? 10 : nEndLotCnt++;
        }
        public static void SetLOT(int nIdx){
            WORK_LOT[nIdx]          = GET_LOT;
            WORK_LOT[nIdx].WorkSort = "본 LOT"; //MES_FIRST == 0 ? "본 LOT" : "선행 LOT";
        }

        public static void CLEAR_RETURN_LOT_CANCEL(){
            RETURN_CANCEL.LOTNO     = "";
            RETURN_CANCEL.RETURN    = "";
            RETURN_CANCEL.MESSAGE   = "";
        }
        public static void LOT_CANCEL_SORTING(){
            int nIndex = 0;
            for (int i = 0; i < WORK_LOT.Length; i++){
                if (WORK_LOT[i].LotID == "" || WORK_LOT[nIndex].LotID != CANCEL_LOT.LotID){
                    if (i == (WORK_LOT.Length - 1)) { CLEAR_WORK_LOT(i); }
                    continue;
                }
                if (i == 0) { WORK_LOT[nIndex] = WORK_LOT[i + 1]; }
                else        { WORK_LOT[nIndex] = WORK_LOT[i]; }
                nIndex++;
            }
        }
        
        //LOT 정보
        public struct sLOT_INFO
        {
            public int nNUM;                    //LOT 등록 순서
            public string WorkScope;            //작업 구분  => 대기,진행,완료,강제완료
            public string ItsID;                //ITS ID
            public string Recipe;               //

            public string LotID;                //LOT ID
            public string ToolNo;               //TOL NUMBER (관리번호 기종)
            public string ProcCD;
            public string ProcName;
            public string WorkCondition;
            public string ProcCondition_1;
            public string ProcCondition_2;
            public string ProcCondition_3;
            public string ProcCondition_4;
            
            public int LotType;                 //LOT TYPE => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
            public int Qty;                     ///MES 수량
            public string ProductType;
            public int ITS;                     //ITS 사용 유무
            public string ITS_LotID_IN;         //ITS ID 대덕전자(대덕 번호)
            public string ITS_LotID_CT;         //ITS ID 고객사(고객사 번호)

            public double UnitSizeX;            //사이즈X
            public double UnitSizeY;            //사이즈Y
            public double UnitSize_USL;         //사이즈 상한값
            public double UnitSize_LSL;         //사이즈 하한값
            public double Thick;                //두께정보
            public double Thick_USL;            //두께 상한값
            public double Thick_LSL;            //두께 하한값

            public string ABFMATERIAL;
            public double LANDPKGX;
            public double LANDPKGX_UPPER;
            public double LANDPKGX_LOWER;
            public double LANDPKGY;
            public double LANDPKGY_UPPER;
            public double LANDPKGY_LOWER;

            public int InCnt;                   //panel 투입 수량
            public int OutCnt;                  //panel 배출 수량
            public int CurCnt;                  //배출 수량과 동일
            public int PassCnt;                 //제외 매수

            public int LoadingCount;            //투입 매수
            public int ExceptCount;             //제외 매수
            public int UnloadingCount;          //배출 매수
            public string WorkSort;             //작업 종류  => 0:초도, 1:본낫, 2:더미, 3:재초도, 4:재작업
        }

        public struct RTN_LOT_CANCEL
        {
            public string LOTNO;
            public string RETURN;
            public string MESSAGE;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LOTINFO_
    {
        public string strLotId      = "";
        public string strCstId      = "";
        public string strCstSlot    = "";
        public string strStartTime  = "";
        public string strEndTime    = "";
        public string strWorkTime   = "";
        public string strTotalCol   = "";
        public string strTotalRow   = "";

        public int nReceiveQty      = 0;

        public string[,] strPkg2d   = new string[100, 100];
        public bool[,] isAttached   = new bool[100, 100];

        public bool[,] isMapDown    = new bool[100, 100];

        public const int IN_LOT     = 0;    //메거진에서 꺼내서 스테이지에 투입전까지의 LOT정보
        public const int WORK_LOT   = 1;  //스테이지에서 작업중인 LOT정보
        public const int OUT_LOT    = 2;   //작업완료된 웨이퍼를 스테이지에서 꺼낸 후 부터의 LOT정보

        static public LOTINFO_ CreateDeepCopy(LOTINFO_ inputcls)
        {
            MemoryStream ms     = new MemoryStream();
            BinaryFormatter bf  = new BinaryFormatter();
            bf.Serialize(ms, inputcls);
            ms.Position = 0;
            return (LOTINFO_)bf.Deserialize(ms);
        }

        public void ClearLotInfo()
        {
            strLotId        = "";
            strCstId        = "";
            strCstSlot      = "";
            strStartTime    = "";
            strEndTime      = "";
            strWorkTime     = "";
            strTotalCol     = "";
            strTotalRow     = "";

            Array.Clear(strPkg2d, 0, strPkg2d.Length);
        }
    }
}
