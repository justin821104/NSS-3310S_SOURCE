using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZGemPlusCS;

namespace ezGEM{
    public struct ControlValue{
        public const short CONTROL_UNKOWN           = 0;
        public const short CONTROL_EQ_OFFLINE       = 1;
        public const short CONTROL_ATTEMPT_ONLINE   = 2;
        public const short CONTROL_HOST_OFFLINE     = 3;
        public const short CONTROL_LOCAL            = 4;
        public const short CONTROL_REMOTE           = 5;
        public const short RESERVED                 = 6;
    }
    public struct EquipmentValue{
        public const short EQUIPMENT_INIT           = 1;
        public const short EQUIPMENT_IDLE           = 2;
        public const short EQUIPMENT_SETUP          = 3;
        public const short EQUIPMENT_READY          = 4;
        public const short EQUIPMENT_RUN            = 5;
        public const short EQUIPMENT_DOWN           = 6;
        public const short EQUIPMENT_MANUAL         = 7;
        public const short RESERVED                 = 8;
    }
    public struct Comm{
        public const int COMM_DISABLE               = 1;
        public const int COMM_ENABLE_NOT_COMM       = 2;
        public const int COMM_ENABLE_CUMM           = 3;
    }
    public struct Communication{
        public const short nNotCom                  = 0;
        public const short nCom                     = 1;
    }
    public struct Connection{
        public const bool bDISCONNECTION            = false;
        public const bool bCONNECTION               = true;

        public const string DISCONNECTION_STRING    = "DISCONNECT";
        public const string CONNECTION_STRING       = "CONNECT";
    }

    public partial class SecsGEM : Form {
        CEZGemPlusLib m_gem = new CEZGemPlusLib(); // dll 참조
        public List<string> m_listPPID; // 테스트를 위하여 가지고 있는 레시피 리스트
        public bool m_bCancel               = true;

        //시스템 바이트를 저장할 전역 변수 선언
        public double m_nLotStart_System = 0;
        public double m_nLotEnd_System = 0;
        public double m_nPPSelected = 0;

        public static string LOCATION       = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        public FileInfo fileinfo            = new FileInfo(Application.ExecutablePath);
        public string m_strExePath          = "";

        public string m_strIP               = "127.0.0.1";
        public string m_strMODE             = "";
        public uint m_nPort                 = 5000;
        public uint m_nDeviceID             = 0;
        public uint m_nModeSelect           = 1;
        public uint m_nLinkInterval         = 30;
        public uint m_nRetry                = 0;
        public uint m_nT3                   = 30;
        public uint m_nT5                   = 30;
        public uint m_nT6                   = 30;
        public uint m_nT7                   = 30;
        public uint m_nT8                   = 30;
        public uint m_nCTTime               = 0;    //ConversationTimeout
        public uint m_nCommReqeustTimeout   = 5;
        public uint m_nFormatTime           = 0;
        public uint m_EstablishTimeout      = 30;

        public string m_strModelName        = ""; //MDLN (모델)
        public string m_strSoftRev          = "1.1.20"; //버전
        public string m_EQName              = "";

        public class SVID {
            public const int CONTROL_STATE                  = 10002;    //U2    
            public const int EQUIPMENT_STATE                = 10003;    //U2    

            public const int USER_ID                        = 10031;    //A[20] 
            public const int PROCESS_USER_ID                = 10032;    //A[20] LOT 예약 작업자 사번

            public const int MODULE_ID                      = 10034;    //A[20] Module ID
            public const int MODULE_NAME                    = 10035;    //A     Module Name

            public const int LOT_ID                         = 10040;    //A     Lot ID
            public const int LOT_LIST                       = 10041;    //L     Lot List

            public const int PANEL_INDEX                    = 10053;    //U2    In,Out Panel Index
            public const int PANEL_ID                       = 10054;    //A[20] Panel ID

            public const int RECIPE_ID                      = 13001;    //A     RECIPE ID
            public const int PP_ERROR                       = 13002;    //A
            public const int PP_ERROR_DATA                  = 13003;    //L

            public const int RESERVE_PANEL_QTY              = 13044;    //U2    예약 수량
            public const int COMPLETE_PANEL_QTY             = 13045;    //U2    완료 수량

            public const int PARAMETER_LIST                 = 13061;    //L     추가입력 항목

            public const int EQP_CHANGE_REASON              = 13071;    //A[20] 설비호기저정변경 원인명
            public const int EQP_CHANGE_COMMENT             = 13072;    //A[20] 설비호기지정변경 사유
            public const int EQP_CHANGE_CODE                = 13073;    //U2    

            public const int LOT_TYPE                       = 13077;    //U2    1=초도, 2=본랏, 3=더미, 4=재초도, 5=재작업

            public const int START_TIME                     = 14001;    //A[16] 가동 Loss기간 시작 (TimeFormat = YYYYMMDDhhmmsscc)
            public const int END_TIME                       = 14002;    //A[16] 가동 Loss기간 종료 (TimeFormat = YYYYMMDDhhmmsscc)
            public const int OPERATION_LOSS_COMMENT         = 14004;    //A[30] 가동 Loss 특이사항
            public const int OPERATION_LOSS_CODE            = 14005;    //A     가동 Loss 코드

            public const int CUR_LEFT_BLADE                 = 15001;    //A     좌:교체 후 장착된 BLADE
            public const int OLD_LEFT_BLADE                 = 15002;    //A     좌:탈착된 BALDE

            public const int CUR_RIGHT_BLADE                = 15011;    //A     우:교체 후 장착된 BLADE
            public const int OLD_RIGHT_BLADE                = 15012;    //A     우:탈착된 BALDE
            public const int JIG                            = 15013;    //A     DICING 테이블

        }
        public class CEID {
            //state event
            public const int CONTROL_STATE_OFFLINE          = 111;  //Offline으로 변경
            public const int CONTROL_STATE_ONLINE_LOCAL     = 112;  //Online-Local로 변경
            public const int CONTROL_STATE_ONLINE_REMOTE    = 113;  //Online-Remote로 변경

            public const int EQUIPMENT_STATE_MANUAL         = 115;  //Manual로 변경 -> IDLE로 대체
            public const int EQUIPMENT_STATE_INIT           = 116;  //Init으로 변경 -> IDLE로 대체
            public const int EQUIPMENT_STATE_IDLE           = 117;  //Idle으로 변경
            public const int EQUIPMENT_STATE_SETUP          = 118;  //Setup으로 변경 -> IDLE로 대체
            public const int EQUIPMENT_STATE_READY          = 119;  //Ready으로 변경 -> IDLE로 대체
            public const int EQUIPMENT_STATE_RUN            = 120;  //Run으로 변경
            public const int EQUIPMENT_STATE_DOWN           = 121;  //Down으로 변경 -> IDLE로 대체
            public const int EQUIPMENT_STATE_PM             = 122;  //PM으로 변경 -> IDLE로 대체

            //tracking event
            public const int LOT_REQUEST                    = 601;  //LOT CARD READING 시 보고 (RECIPE VALIDATION)
            public const int LOT_PAUSE                      = 602;  //LOT PAUSE (AFTER LOT_PAUSE)
            public const int LOT_STARTED                    = 603;  //LOT STARTED (AFTER LOT_START)
            public const int LOT_RESUME                     = 604;  //LOT RESUME
            public const int LOT_CREATED                    = 605;
            public const int LOT_CREATED_FAIL               = 606;

            public const int LOT_LOADING                    = 301;  //LOT START 시 보고
            public const int LOT_COMPLETE                   = 302;  //LOT END 시 보고 
            public const int LOT_ABORT                      = 320;  //LOT ABORT 시 보고
            public const int LOT_CANCELED                   = 331;

            //equipment event
            public const int PANEL_LINE_IN                  = 306;  //Equipment에 대한 Event, 매 제품이 본체에 투입될 때
            public const int PANEL_LINE_OUT                 = 307;  //Equipment에 대한 Event, 매 제품이 본체에 배출될 때
            public const int PANEL_MODULE_IN                = 308;  //Equipment에 대한 Event, 매 제품이 Module에 투입될 때
            public const int PANEL_MODULE_OUT               = 309;  //Equipment에 대한 Event, 매 제품이 Module에서 배출될 때

            //
            public const int LOT_START_REQUEST              = 310;  //LOT START 요청
            public const int PARA_INPUT_COMPLETE            = 311;  //파라미터 입력 완료 할때
            public const int LOT_EQP_CHANGE_COMPLETE        = 312;  //설비 호기 변경 완료
            public const int LOT_MODIFIED                   = 313;  //대기열 수량 변경
            public const int PP_SELECTED                    = 314;  //PPSelect 완료
            public const int LOT_EQP_LOSS_COMPLETE          = 315;  //가동 LOSS 입력
            public const int LOT_LIST_MODIFIED              = 316;  //대기열 LOT 순서 변경

            //Dry Film Change
            public const int UPPER_DF_KITTING               = 501;  //상단 BLADE 장착
            public const int LOWER_DF_KITTING               = 502;  //하단 BLADE 장착

            public const int POP_CONDITION_COMPLETE         = 332;  //Pop Condition 확인 완료
        }
        public class ECID{
            public const int ECID_PORT_ID                   = 50001;
            public const int ECID_DEVICEID                  = 50002;
            public const int ECID_T3                        = 50003;
            public const int ECID_T5                        = 50004;
            public const int ECID_T6                        = 50005;
            public const int ECID_T7                        = 50006;
            public const int ECID_T8                        = 50007;
            public const int ECID_LINKTEST                  = 50008;
            public const int ECID_RETRY                     = 50009;

            public const int ECID_ESTABLISH_TIMEOUT         = 50011;
            public const int ECID_TIME_FORMAT               = 50012;
            public const int ECID_EQUIPMENT_NAME            = 50013;
        }
        public class RCMD {
            public const string PPSELECT            = "PP-SELECT";
            //TRACKING
            public const string LOT_CREATE          = "LOT_CREATE";     //HOST에서 LOT ID 전송
            public const string POP_CONDITION       = "POP_CONDITION";
            public const string LOT_INFO            = "LOT_INFO";       //LOT 정보 전달
            public const string LOT_START           = "LOT_START";      //설비 START
            public const string LOT_CANCEL          = "LOT_CANCEL";     //대기열 LOT 삭제
            public const string LOT_PAUSE           = "LOT_PAUSE";      //설비 투입기 일시 정지
            public const string LOT_RESUME          = "LOT_RESUME";     //설비 투입기 동작

            public const string LOT_EQP_CHANGE      = "LOT_EQP_CHANGE"; //설비호기지정변경
            public const string LOT_EQP_LOSS        = "LOT_EQP_LOSS";   //가동 LOSS 입력
            public const string PARAMETER_INFO      = "PARAMETER_INFO"; //필수 입력 파라미터 입력
            
        }
        public class CPNAME {
            public const string PPID                = "PPID";
            public const string PORTID              = "PORTID";
            public const string CARRIERID           = "CARRIERID";
            public const string WAFERMAP            = "WAFERMAP";

            public const string LOSS_START_TIME     = "START_TIME";
            public const string LOSS_END_TIME       = "END_TIME";

            public const string LOTID               = "LOTID";                  //LOT ID
            public const string QTY                 = "QTY";                    //STRIP or QUAD 수량
            public const string LOTTYPE             = "LOTTYPE";                //1=초도,2=본낫,3=재초도,4=재작업
            public const string PRODUCTTYPE         = "PRODUCTTYPE";            //STRIP or QUAD
            public const string TOOLNO              = "TOOLNO";                 //TOOL NO
            public const string ITS                 = "ITS";                    //ITS 진행 여부 : 0 진행, 1 미진행
            public const string ITSLOTID_IN         = "ITSLOTID_IN";            //ITS LOTID 내부
            public const string ITSLOTID_CT         = "ITSLOTID_CT";            //ITS LOTID 고객
            public const string UNITSIZEX           = "UNITSIZEX";              //유닛 SIZE X
            public const string UNITSIZEY           = "UNITSIZEY";              //유닛 SIZE Y
            public const string THICK               = "THICK";                  //두께
            public const string ABFMATERIA          = "ABFMATERIA";             //ABF 자재

            public const string PROCCD              = "PROCCD";
            public const string PROCNAME            = "PROCNAME";
            public const string WORKCONDITION       = "WORKCONDITION";
            public const string PROCESSCONDITION_1 = /*"WORK INSTRUCTION#";//*/"LOT ID#공정명#이벤트명#메세지 타입#작업지침#팝업여부#FILE_ATTACH#생성자#생성자명#생성 일자#등록자#등록자명#수정일자#FILE_GRP_ID";
            public const string PROCESSCONDITION_2 = /*"CREATOR#";//*/"LOT ID#작업/액티비티 번호#공정명#시작/종료층#이벤트명#보류 코드#보류명#특이사항#생성자#생성자명#생성 일자";
            public const string PROCESSCONDITION_3 = /*"Hole To Hole X##Hole To Hole Y;//*/"공정순서#공정코드#공정명#Hole To Hole X#공차 -#공차 +#Hole To Hole Y#공차 -#공차 +";

            public const string LOT_EQP_LOSSREASON_ = "LOT_EQP_LOSSREASON_";    //LOSSREASON NAME (n개)
            public const string PARAMETER_          = "PARAMETER_";             //PARAMETER NAME (n개)
        }

        public SecsGEM() {
            InitializeComponent();
            m_listPPID = new List<string>();

            m_gem.OnEZGemEvent  += new ON_EZGEM_EVENT(OnEventReceived); // Gem내부 이벤트를 받음.
            m_gem.OnEZGemMsg    += new ON_EZGEM_MSG(OnMsgReceived);     // Host로 부터 받은 메세지를 전달.

            cbEquipmentState.SelectedIndex  = 1;
            ChkEQ_PM.Checked                = false;
        }

        public void AddGemLog(string sLOG) {
            try {
                if (InvokeRequired) {
                    this.Invoke((MethodInvoker)delegate () {
                        AddGemLog(sLOG);
                    });
                }
                else {
                    if (lstLog.Items.Count > 10000) lstLog.Items.Clear();
                    string strTime = DateTime.Now.ToString("MM/dd HH:mm:ss:fff");
                    string strWrite = "[" + strTime + "] " + sLOG;
                    lstLog.Items.Add(strWrite);
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                }
            }
            catch (Exception EX) {
                System.Diagnostics.Trace.WriteLine(EX.Message);
                //LogWR_.SaveLogException("[GEM] ADD MESSAGE FAIL!" + ETC.NewLine + sLOG, EX);
            }
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(    // GetIniValue 를 위해
            String section,
            String key,
            String def,
            StringBuilder retVal,
            int size,
            String filePath);
        public String GetIniValue(String Section, String Key, String iniPath) {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, iniPath);
            return temp.ToString();
        }
        public void INI2VAL(string ss, string s, ref uint val) {
            string strValue = "";
            strValue = GetIniValue(ss, s, DATA_.PROJECT + "GEM.INI");
            val = uint.Parse(strValue);
        }
        public void ReadGemIniFile() {
            INI2VAL("GEM", "PORT", ref m_nPort);
            INI2VAL("GEM", "DEVICEID", ref m_nDeviceID);
            INI2VAL("GEM", "PASSIVE", ref m_nModeSelect);
            INI2VAL("GEM", "LINKTEST", ref m_nLinkInterval);
            INI2VAL("GEM", "RETRY", ref m_nRetry);
            INI2VAL("GEM", "T3", ref m_nT3);
            INI2VAL("GEM", "T5", ref m_nT5);
            INI2VAL("GEM", "T6", ref m_nT6);
            INI2VAL("GEM", "T7", ref m_nT7);
            INI2VAL("GEM", "T8", ref m_nT8);
            INI2VAL("GEM", "CONVERSATIONTIMEOUT", ref m_nCTTime);
            INI2VAL("GEM", "COMMREQUESTTIMEOUT", ref m_nCommReqeustTimeout);
            INI2VAL("GEM", "TIMEFORMAT", ref m_nFormatTime);

            m_strIP = GetIniValue("GEM", "IP", DATA_.PROJECT + "GEM.ini");
            m_strMODE = GetIniValue("GEM", "MODE", DATA_.PROJECT + "GEM.ini");
            m_EQName = GetIniValue("GEM", "EQUIP_NAME", DATA_.PROJECT + "GEM.ini");
        }
        public void GetDialogValue() {
            lbPort.Text         = m_nPort.ToString();
            lbDevice.Text       = m_nDeviceID.ToString();
            lbT3.Text           = m_nT3.ToString();
            lbT5.Text           = m_nT5.ToString();
            lbT6.Text           = m_nT6.ToString();
            lbT7.Text           = m_nT7.ToString();
            lbT8.Text           = m_nT8.ToString();
            lbLinkTest.Text     = m_nLinkInterval.ToString();
            lbCommRequest.Text  = m_nCommReqeustTimeout.ToString();

            if (m_gem.PassiveMode == 1) lbModeSelect.Text = "PASSIVE MODE";
            else                        lbModeSelect.Text = "ACTIVE MODE";
        }

        public void StartGem() {
            //------------ 속성(Attribute) Parameter
            timerGem.Stop();

            timer_SetClock.Interval = 1 * 1000;
            timer_SetClock.Enabled  = true;
            
            GetInfo();

            m_gem.DeviceID          = (short)m_nDeviceID;           // Default = 0
            m_gem.PassiveMode       = (short)m_nModeSelect;         // Default = 1 (true) = active mode
            m_gem.T3                = (short)m_nT3;                 // wait interval time for response
            m_gem.T5                = (short)m_nT5;                 // wait interval time for Reconnected( Active Mode )
            m_gem.T6                = (short)m_nT6;                 // wait interval time for Control message
            m_gem.T7                = (short)m_nT7;                 // wait interval time between connected and selected
            m_gem.T8                = (short)m_nT8;                 // multi message accepted time
            m_gem.Port              = (short)m_nPort;               // Default = 5000
            m_gem.RetryCount        = (short)m_nRetry;              // Default = 0 
            m_gem.LinkTestInterval  = (short)m_nLinkInterval;       // Default = 30 sec

            m_gem.CommRequest = (short)m_nCommReqeustTimeout; //  interval time between selected <-> S1F13w
            //------------- Method
            m_gem.SetIP(m_strIP);                               // Default = "127.0.0.1" (localhost)
            m_gem.SetLogFile(DATA_.LogMES + "GEM.LOG");
            m_gem.SetLogRetention(30);

            //m_gem.SetFormatFile(string.Format("FORMAT_U4.SML")); // S9계열 error메세지를 거르기 위하여 미리 포멧을 정의
            m_gem.SetFormatFile(DATA_.PROJECT + "FORMAT.SML");	 //SML파일경로 지정할것
            m_gem.SetFormatCheck(true);

            m_gem.SetReportFilePath(DATA_.PROJECT + "EZGEM.RPT");

            //각 ID는 미리 등록을 해두어야 사용이 가능하다.
            AddSVID(); // SVID등록하기
            AddCEID(); // CEID등록하기
            AddALID(); // ALAMR등록하기
            AddECID(); // ECID등록하기

            //각 ID별로 포멧을 정의함. I1, I2 , I4 , U1 , U2 , U4 가능.
            m_gem.SetFormatCode("SVID", "U4");
            m_gem.SetFormatCode("CEID", "U4");
            m_gem.SetFormatCode("ALID", "U4");
            m_gem.SetFormatCode("TRID", "U4");
            m_gem.SetFormatCode("RPTID", "U4");
            m_gem.SetFormatCode("DATAID", "U4");

            //m_gem.EnableSpooling();
            m_gem.EnableSpoolOverwrite();
            m_gem.SetMaxSpoolCount(1000);
            m_gem.SetMaxSpoolTransmitCount(100);

            m_gem.DisableSpooling();

            m_gem.SetModelName(m_strModelName);         //MODEL_NAME
            m_gem.SetSoftRev(m_strSoftRev);             //SOFTREV

            //DisableAutoReply 해당 메세지를 OnMsgRequested로 받오록한다.
            //user msg를 사용 해서 OnMsgRequest 이벤트로 보내는 경우
            m_gem.DisableAutoReply(6, 12); // CEID 응답

            //FILE
            m_gem.DisableAutoReply(7, 3);
            m_gem.DisableAutoReply(7, 5);

            m_gem.DisableAutoReply(7, 17);
            m_gem.DisableAutoReply(7, 19);  //RECIPE LIST 요청 (TC->EQ)
            
            m_gem.DisableAutoReply(7, 23);  //RMS 요청 (TC->EQ) 현재 사용안함!
            m_gem.DisableAutoReply(7, 24);  //RMS 응답 (TC->EQ)
            m_gem.DisableAutoReply(7, 25);  //RECIPE UPLOAD 요청

            m_gem.DisableAutoReply(10, 3);  //TERMINAL DISPLAY
            m_gem.DisableAutoReply(10, 5);

            ///////////////////////////////////////////////////////////////////////
            //				Remote Command 를 미리 추가 해 주어야 함
            /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
            m_gem.AddRemoteCommand(RCMD.PPSELECT);
            
            m_gem.AddRemoteCommand(RCMD.LOT_CREATE);
            m_gem.AddRemoteCommand(RCMD.POP_CONDITION);
            m_gem.AddRemoteCommand(RCMD.LOT_INFO);
            m_gem.AddRemoteCommand(RCMD.LOT_START);
            m_gem.AddRemoteCommand(RCMD.LOT_CANCEL);
            m_gem.AddRemoteCommand(RCMD.LOT_PAUSE);
            m_gem.AddRemoteCommand(RCMD.LOT_RESUME);

            m_gem.AddRemoteCommand(RCMD.LOT_EQP_CHANGE);
            m_gem.AddRemoteCommand(RCMD.LOT_EQP_LOSS);
            m_gem.AddRemoteCommand(RCMD.PARAMETER_INFO);
            /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */

            if (m_gem.Start() == 0){
                m_gem.GoOnlineRemote(); // S1F13을 통해 EstablishCommuncation이 되었을 때 진행할 Online함수
                                        // OnlineLocal을 원할 경우 GoOnlineLocal(); 함수를 호출.

                //----Gem 시작시 중요 설정값을 화면에 표시 ---------------
                if (m_gem.PassiveMode == 1) AddGemLog("PASSIVE MODE");
                else                        AddGemLog("ACTIVE MODE");
                AddGemLog(string.Format("PORT={0}", m_nPort));
                AddGemLog(string.Format("DEVICE={0}", m_nDeviceID));
                AddGemLog("EZGEM DLL (+) STARTED");

                btnStart.Enabled    = false;
                btnStop.Enabled     = true;
                //---------------------------------------------------------
            }

            //임시
            lblProcessWorkingCondition_Value1.Text = "";
            lblProcessWorkingCondition_Value2.Text = "";
            lblProcessWorkingCondition_Value3.Text = "";
            lblProcessWorkingCondition_Value4.Text = "";
        }

        public void AddSVID() {
            int nCount = 0;
            string filePath = string.Format("{0}SVID.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)){
                m_gem.WriteUserLog(string.Format("SVID.TXT is not Exist. Check file."));
                return;
            }
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string strId = "";
                        string strName = "";
                        string strType = "";
                        string strFormat = "";

                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length > 2){
                            strId = SplitNum[0];
                            strName = SplitNum[2];
                            strType = SplitNum[1];
                            strFormat = "";

                            m_gem.AddSVID(int.Parse(strId), strName, strType, ""); // ALID, ptr, strALCD
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }

            filePath = string.Format("{0}SVID1.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)) {
                m_gem.WriteUserLog(string.Format("SVID1.TXT is not Exist. Check file."));
                return;
            }
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string strId = "";
                        string strName = "";
                        string strType = "";
                        
                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length > 2){
                            strId       = SplitNum[0];
                            strName     = SplitNum[2];
                            strType     = SplitNum[1];
                            
                            m_gem.AddSVID(int.Parse(strId), strName, strType, ""); // ALID, ptr, strALCD
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }

            filePath = string.Format("{0}SVID2.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)){
                m_gem.WriteUserLog(string.Format("SVID2.TXT is not Exist. Check file."));
                return;
            }
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string strId = "";
                        string strName = "";
                        string strType = "";

                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length > 2){
                            strId = SplitNum[0];
                            strName = SplitNum[2];
                            strType = SplitNum[1];

                            m_gem.AddSVID(int.Parse(strId), strName, strType, ""); // ALID, ptr, strALCD
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }
            AddGemLog(string.Format("SVID ADD {0} COUNT", nCount));
        } //SVID 등록하기
        public void AddCEID() { 
            string filePath = string.Format("{0}CEID.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)) {
                m_gem.WriteUserLog(string.Format("CEID.TXT is not Exist. Check file."));
                return;
            }

            int nCount = 0;
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length == 2){
                            string strId = SplitNum[0];
                            string strName = SplitNum[1];

                            m_gem.AddCEID(int.Parse(strId), strName, "");
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }
            AddGemLog(string.Format("CEID ADD {0} COUNT", nCount));
        } //CEID 등록하기
        public void AddALID() {
            string filePath = string.Format("{0}ALARM.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)) {
                m_gem.WriteUserLog(string.Format("ALARM.TXT is not Exist. Check file."));
                return;
            }

            int nCount = 0;
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)) {
                    while (sr.Peek() > -1){
                        string input        = sr.ReadLine();
                        char[] Separators   = new char[] { '\t' };
                        string[] SplitNum   = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length == 3){
                            //번호  NAME ALCD(Alarm Code 1:Warning Alarm, 6:Stop Alarm )
                            m_gem.AddALID(int.Parse(SplitNum[0]), SplitNum[2], SplitNum[1]); // ALID, ptr, strALCD                                                                                            
                            nCount++;
                        }
                        else{
                            //AddGemLog(input);
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }
            AddGemLog(string.Format("ALARM ADD {0} COUNT", nCount));
        } // ALARM 등록하기
        public void AddECID(){
            string filePath = string.Format("{0}ECID.txt", /*fileinfo.Directory.FullName*/DATA_.PROJECT);
            if (!File.Exists(filePath)){
                m_gem.WriteUserLog(string.Format("ECID.TXT is not Exist. Check file."));
                return;
            }

            int nCount = 0;
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length == 5){
                            //번호  NAME ALCD(Alarm Code 1:Warning Alarm, 6:Stop Alarm )
                            m_gem.AddECID(int.Parse(SplitNum[0]), SplitNum[2], "", SplitNum[1]); // ALID, ptr, strALCD      
                            m_gem.SetECRange(int.Parse(SplitNum[0]), SplitNum[3], SplitNum[4]);
                            nCount++;
                        }
                        else{
                            //AddGemLog(input);
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            } 

            //
            m_gem.SetECValue(ECID.ECID_PORT_ID, m_gem.Port.ToString());
            m_gem.SetECValue(ECID.ECID_DEVICEID, m_gem.DeviceID.ToString());
            m_gem.SetECValue(ECID.ECID_T3, m_gem.T3.ToString());
            m_gem.SetECValue(ECID.ECID_T5, m_gem.T5.ToString());
            m_gem.SetECValue(ECID.ECID_T6, m_gem.T6.ToString());
            m_gem.SetECValue(ECID.ECID_T7, m_gem.T7.ToString());
            m_gem.SetECValue(ECID.ECID_T8, m_gem.T8.ToString());
            m_gem.SetECValue(ECID.ECID_LINKTEST, m_gem.LinkTestInterval.ToString());
            m_gem.SetECValue(ECID.ECID_RETRY, m_gem.RetryCount.ToString());

            m_gem.SetECValue(ECID.ECID_ESTABLISH_TIMEOUT, m_EstablishTimeout.ToString());
            m_gem.SetECValue(ECID.ECID_TIME_FORMAT, m_nFormatTime.ToString());
            m_gem.SetECValue(ECID.ECID_EQUIPMENT_NAME, m_EQName);

            AddGemLog(string.Format("ECID ADD {0} COUNT", nCount));
        } // ECID등록하기

        void GetInfo(){
#if _GEM
            ReadGemIniFile();
#endif
            GetDialogValue();
        }
        private void SecsGEM_Load(object sender, EventArgs e){
            this.Size = new Size(500, 940); //500, 277 -> 500, 940
            this.Text = DATA_.Version;
            m_strExePath = fileinfo.Directory.FullName;
            GetInfo();

            timerGem.Interval           = 5 * 1000;
            timerGem.Start();

            timer_SetClock.Interval     = 1 * 1000;
            timer_SetClock.Enabled      = true;

            btnStart.Enabled            = true;
            btnStop.Enabled             = false;

            //---------- 버튼,라벨의 초기화 --------------
            DATA_.sCommState            = "NOT COMMUNICATING";
            DATA_.cCommState            = Color.Red;
            lblCommState.Text           = DATA_.sCommState;
            lblCommState.BackColor      = DATA_.cCommState;

            DATA_.sControlState         = "OFFLINE";
            DATA_.cConnectState         = System.Drawing.Color.Red;
            lblControlState.Text        = DATA_.sControlState;
            lblControlState.BackColor   = DATA_.cConnectState;

            lblConnectState.Text        = Connection.DISCONNECTION_STRING;
            lblConnectState.BackColor   = Color.Red;

            btnRemote.Enabled           = false;
            btnLocal.Enabled            = false;
            btnOffline.Enabled          = false;

            //--------------------------------------------------
            //lstLog.DrawMode            = DrawMode.OwnerDrawFixed;
            //--------------------------------------------------

            LBL_EQP_CODE.Text = DATA_.EQPCode;
        }

        private void SecsGEM_FormClosing(object sender, FormClosingEventArgs e){
            //e.Cancel , true = 종료 취소, false = 종료( default : false )
            if (m_bCancel){
                e.Cancel = true;
            }
            else{
                e.Cancel = false;
            }
        }

        private void OnEventReceived(IntPtr lpParam, short nEventId, int lParam){
            ///////////////////////////////////////////////////
            switch (nEventId){
                case 1: // tcp connect
                    OnConnected();
                    break;
                case 2: // tcp disconnect
                    OnDisconnected();
                    break;

                case 401: // Msg In시 호출되는 함수.
                    OnMsgIn(lParam);
                    break;
                case 402: // Msg Out시 호출되는 함수.
                    OnMsgOut(lParam);
                    break;

                case 1001: // S1F15
                    OnOffline();
                    break;
                case 1002: // S1F17
                    OnOnlineLocal();
                    break;
                case 1003: // S1F17
                    OnOnlineRemote();
                    break;

                case 1010:
                    OnCummunicating();
                    break;
                case 1030: // S2F41w
                    OnRemoteCommand(lParam);
                    break;
                case 1015: // S2F15w
                    OnNewHOST_ECID(lParam);
                    break;
                case 1050: // S10F3, S10F5
                    OnTerminalMsg(lParam);
                    break;
                case 1060: // S10F3, S10F5
                    //OnTerminalMsg(lParam);
                    AddGemLog(string.Format("TRASACTIONID={0}", lParam));
                    break;
                case 501:       // s2f37에 응답을 보낸 경우 호출
                    OnOtherEvent(nEventId, lParam);
                    break;
                case 303:       // S9F9가 나가서 메시지를 페기하는 경우
                    AddGemLog("case 303");
                    AddGemLog(string.Format("nEventId={0}, lParam ={1}", nEventId, lParam));
                    break;
                default:
                    AddGemLog("default");
                    AddGemLog(string.Format("nEventId={0}, lParam ={1}", nEventId, lParam));
                    break;
            }
        }

        public void OnConnected(){
            this.Invoke(new MethodInvoker(delegate ()
            {
                DATA_.m_bConnected          = Connection.bCONNECTION;
                DATA_.sConnectState         = "CONNECTED";
                DATA_.cConnectState         = Color.Lime;

                lblConnectState.Text        = DATA_.sConnectState;
                lblConnectState.BackColor   = DATA_.cConnectState;

            }));
            
            if (DATA_.m_nControlState == ControlValue.CONTROL_EQ_OFFLINE){
                DATA_.sControlState         = "OFFLINE";
                DATA_.cConnectState         = System.Drawing.SystemColors.Control;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;
            }
            else if (DATA_.m_nControlState == ControlValue.CONTROL_LOCAL){
                DATA_.sControlState         = "LOCAL";
                DATA_.cControlState         = System.Drawing.Color.LightSkyBlue;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;
            }
            else if (DATA_.m_nControlState == ControlValue.CONTROL_REMOTE) {
                DATA_.sControlState         = "REMOTE";
                DATA_.cControlState         = System.Drawing.Color.LightSkyBlue;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;
            }
        }

        public void OnDisconnected(){
            this.Invoke(new MethodInvoker(delegate (){
                DATA_.m_bConnected          = Connection.bDISCONNECTION;
                DATA_.sConnectState         = "DISCONNECTED";
                DATA_.cConnectState         = Color.Red;

                lblConnectState.Text        = DATA_.sConnectState;
                lblConnectState.BackColor   = DATA_.cConnectState;

                btnRemote.Enabled           = false;
                btnLocal.Enabled            = false;
                btnOffline.Enabled          = false;

                if (DATA_.m_nControlState == ControlValue.CONTROL_LOCAL || DATA_.m_nControlState == ControlValue.CONTROL_REMOTE) DATA_.m_nControlState = ControlValue.CONTROL_EQ_OFFLINE;
                m_gem.GoOffline();
                SetControlState(CEID.CONTROL_STATE_OFFLINE);

                DATA_.sControlState         = "OFFLINE";
                DATA_.cConnectState         = System.Drawing.SystemColors.Control;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;

                btnOffline.Enabled          = false;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = true;
            }));
        }

        public void OnMsgIn(int lParam){
            int nStream = 0, nFunction = 0;

            nStream     = (int)(lParam / 1000);
            nFunction   = lParam % 1000;

            AddGemLog(string.Format("(H->E) S{0},F{1}", nStream, nFunction));
        }

        public void OnMsgOut(int lParam){
            int nStream = 0, nFunction = 0;

            nStream     = (int)(lParam / 1000);
            nFunction   = lParam % 1000;

            AddGemLog(string.Format("(E->H) S{0},F{1}", nStream, nFunction));
        }

        public void OnOffline(){
            this.Invoke(new MethodInvoker(delegate ()
            {
                DATA_.m_nPrevControlState   = DATA_.m_nControlState;
                DATA_.m_nControlState       = ControlValue.CONTROL_HOST_OFFLINE;
                SetControlState(CEID.CONTROL_STATE_OFFLINE);

                DATA_.sControlState         = "OFFLINE";
                DATA_.cConnectState         = System.Drawing.SystemColors.Control;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;
                
                btnOffline.Enabled          = false;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = true;

                btnOffline.BackColor        = Color.Red;
                btnLocal.BackColor          = System.Drawing.SystemColors.Control;
                btnRemote.BackColor         = System.Drawing.SystemColors.Control;
            }));
        }

        public void OnOnlineLocal(){
            this.Invoke(new MethodInvoker(delegate ()
            {
                DATA_.m_nPrevControlState   = DATA_.m_nControlState;
                DATA_.m_nControlState       = ControlValue.CONTROL_LOCAL;
                SetControlState(CEID.CONTROL_STATE_ONLINE_LOCAL);

                DATA_.sControlState         = "LOCAL";
                DATA_.cControlState         = System.Drawing.Color.LightSkyBlue;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;

                btnOffline.Enabled          = true;
                btnLocal.Enabled            = false;
                btnRemote.Enabled           = true;

                btnOffline.BackColor        = System.Drawing.SystemColors.Control;
                btnLocal.BackColor          = Color.Yellow;
                btnRemote.BackColor         = System.Drawing.SystemColors.Control;
            }));
        }

        public void OnOnlineRemote(){
            this.Invoke(new MethodInvoker(delegate ()
            {
                DATA_.m_nPrevControlState   = DATA_.m_nControlState;
                DATA_.m_nControlState       = ControlValue.CONTROL_REMOTE;
                SetControlState(CEID.CONTROL_STATE_ONLINE_REMOTE);

                DATA_.sControlState         = "REMOTE";
                DATA_.cControlState         = System.Drawing.Color.LightSkyBlue;
                lblControlState.Text        = DATA_.sControlState;
                lblControlState.BackColor   = DATA_.cConnectState;

                btnOffline.Enabled          = true;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = false;

                btnOffline.BackColor        = System.Drawing.SystemColors.Control;
                btnLocal.BackColor          = System.Drawing.SystemColors.Control;
                btnRemote.BackColor         = Color.Green;
            }));

        }

        public void OnCummunicating(){
            this.Invoke(new MethodInvoker(delegate ()
            {
                DATA_.sCommState        = "COMMUNICATING";
                DATA_.cCommState        = Color.Green;
                lblCommState.Text       = DATA_.sCommState;
                lblCommState.BackColor  = DATA_.cCommState;

                AddGemLog("COMMUNICATING");
            }));
        }

        public void OnRemoteCommand(int lMsgId){
            string strCommand = "", strCPName = "", strCPValue = "";
            int nParamCount = 0, nFormat = 0, nValueCount = 0;
            /*short*/byte nHCACK = 0;
           
            //---------- 받아서 처리하는 변수 선언 ----------------

            #region "테스트"
            //nParamCount = m_gem.GetRemoteCommand(lMsgId, ref strCommand);
            //string strLOTID = "", strPPID = "";
            //for (short i = 0; i < (short)nParamCount; i++){
            //    m_gem.GetRemoteCommandParam(lMsgId, i, ref strCPName, ref strCPValue, ref nFormat);
            //    strCPName.ToUpper();
            //    if (strCPName == CPNAME.LOTID){
            //        strLOTID = strCPValue;
            //    }
            //    else if (strCPName == CPNAME.PPID){
            //        strPPID = strCPValue;
            //    }
            //    else{ //해당되는 CPNAME이 존재하지 않는 경우. NACK{
            //        nHCACK = 3;
            //    }
            //}
            //if (nHCACK != 0){
            //    // 허용되지 않는 CPNAME이 존재함.
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //    return;
            //}
            //else{    
            //}

            //if (strCommand == RCMD.LOT_CREATE){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID)              DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.QTY)                DATA_.GET_LOT.QTY = int.Parse(strCPValue);
            //    }
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //}
            //if (strCommand == RCMD.LOT_EQP_LOSS){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID)              DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.LOTTYPE)            DATA_.GET_LOT.LotType = int.Parse(strCPValue);
            //        if (strCPName == CPNAME.LOSS_START_TIME)    DATA_.LotLossStartTime = strCPValue;
            //        if (strCPName == CPNAME.LOSS_END_TIME)      DATA_.LotLossEndTime = strCPValue;
            //        //
            //    }
            //    DATA_.bLotLoss = true;
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //}
            //if (strCommand == RCMD.LOT_EQP_CHANGE){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID)      DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.LOTTYPE)    DATA_.GET_LOT.LotType = int.Parse(strCPValue);
            //        //
            //    }
            //    DATA_.bEqpChange = true;
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //}
            //if (strCommand == RCMD.PARAMETER_INFO){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID)          DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.LOTTYPE)        DATA_.GET_LOT.LotType = int.Parse(strCPValue);
            //        //
            //    }
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //}
            //if (strCommand == RCMD.POP_CONDITION){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID)          DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.TOOLNO)         DATA_.GET_LOT.ToolNo = strCPValue;
            //        if (strCPName == CPNAME.PROCCD)         DATA_.GET_LOT.ProcCD = strCPValue;
            //        if (strCPName == CPNAME.PROCNAME)       DATA_.GET_LOT.ProcName = strCPValue;
            //        if (strCPName == CPNAME.WORKCONDITION)  DATA_.GET_LOT.WorkCondition = strCPValue;
            //        //
            //    }
            //
            //    DATA_.bLotLoss = false;
            //    DATA_.bEqpChange = false;
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //
            //    System.Threading.Thread.Sleep(100);
            //    SetPopCondition();
            //}
            //if (strCommand == RCMD.LOT_CANCEL){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID) DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.LOTTYPE) DATA_.GET_LOT.LotType = int.Parse(strCPValue);
            //
            //    }
            //    DATA_.bLotLoss = false;
            //    DATA_.bEqpChange = false;
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //}
            //if (strCommand == RCMD.LOT_INFO){
            //    nHCACK = 0;
            //    for (int n = 0; n < nParamCount; n++){
            //        m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
            //        if (strCPName == CPNAME.LOTID) DATA_.GET_LOT.LotID = strCPValue;
            //        if (strCPName == CPNAME.LOTTYPE) DATA_.GET_LOT.LotType = int.Parse(strCPValue);
            //        if (strCPName == CPNAME.QTY) DATA_.GET_LOT.QTY = int.Parse(strCPValue);
            //        if (strCPName == CPNAME.PRODUCTTYPE) DATA_.GET_LOT.ProductType = strCPValue;
            //
            //
            //    }
            //    DATA_.bLotLoss = false;
            //    DATA_.bEqpChange = false;
            //    m_gem.ReplyRemoteCommand(lMsgId, nHCACK);
            //
            //    System.Threading.Thread.Sleep(100);
            //    SetPPSelect();
            //}
            #endregion "테스트"

            m_gem.GetListItemOpen(lMsgId);
            m_gem.GetAsciiItem(lMsgId, ref strCommand);
            
            if (strCommand == RCMD.LOT_CREATE){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)      DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.QTY)        DATA_.GET_LOT.QTY = int.Parse(strCPValue);
                    if (strCPName == CPNAME.LOTTYPE)    DATA_.GET_LOT.LotType = int.Parse(strCPValue);
                    
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                nHCACK = 0;
            }          //Tracking 1
            else if (strCommand == RCMD.LOT_EQP_LOSS){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            DATA_.GET_LOT.LotType = int.Parse(strCPValue);
                    if (strCPName == CPNAME.LOSS_START_TIME)    DATA_.LotLossStartTime = strCPValue;
                    if (strCPName == CPNAME.LOSS_END_TIME)      DATA_.LotLossEndTime = strCPValue;
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                DATA_.bLotLoss = true;
                nHCACK = 0;
            }   //가동LOSS입력 1
            else if (strCommand == RCMD.LOT_EQP_CHANGE){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            DATA_.GET_LOT.LotType = int.Parse(strCPValue);

                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                DATA_.bEqpChange = true;
                nHCACK = 0;
            } //설비호기변경 1
            else if (strCommand == RCMD.PARAMETER_INFO){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            DATA_.GET_LOT.LotType = int.Parse(strCPValue);

                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                nHCACK = 0;
            } //파라미터입력 1
            else if (strCommand == RCMD.POP_CONDITION){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetListItemOpen(lMsgId);
                    m_gem.GetAsciiItem(lMsgId, ref strCPName);
                    if (strCPName == CPNAME.LOTID){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.LotID = strCPValue;
                    }
                    if (strCPName == CPNAME.TOOLNO){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.ToolNo = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCCD){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.ProcCD = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCNAME){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.ProcName = strCPValue;
                    }
                    if (strCPName == CPNAME.WORKCONDITION){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.WorkCondition = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_1){
                        lblProcessWorkingCondition_1.Text = strCPName;
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.ProcCondition_1 = strCPValue;
                        lblProcessWorkingCondition_Value1.Text = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_2){
                        lblProcessWorkingCondition_2.Text = strCPName;
                        nValueCount = m_gem.GetListItemOpen(lMsgId);
                        for (int j = 0; j < nValueCount; j++){
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                            if (j == 0) DATA_.GET_LOT.ProcCondition_2 = strCPValue;
                            if (j == 1) DATA_.GET_LOT.ProcCondition_3 = strCPValue;
                        }
                        m_gem.GetListItemClose(lMsgId);
                        lblProcessWorkingCondition_Value2.Text = DATA_.GET_LOT.ProcCondition_2;
                        lblProcessWorkingCondition_Value3.Text = DATA_.GET_LOT.ProcCondition_3;
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_3){
                        lblProcessWorkingCondition_3.Text = strCPName;
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        DATA_.GET_LOT.ProcCondition_4 = strCPValue;
                        lblProcessWorkingCondition_Value4.Text = strCPValue;
                    }

                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                    m_gem.GetListItemClose(lMsgId);
                }
                m_gem.GetListItemClose(lMsgId);
                m_gem.GetListItemClose(lMsgId);
                nHCACK = 0;
            }  //공정작업조건 1
            else if (strCommand == RCMD.LOT_CANCEL){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)      DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)    DATA_.GET_LOT.LotType = int.Parse(strCPValue);
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                DATA_.bLotLoss      = false;
                DATA_.bEqpChange    = false;
                nHCACK = 0;
            }     //Tracking 4
            else if (strCommand == RCMD.LOT_INFO){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)          DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)        DATA_.GET_LOT.LotType = int.Parse(strCPValue);
                    if (strCPName == CPNAME.QTY)            DATA_.GET_LOT.QTY = int.Parse(strCPValue);
                    if (strCPName == CPNAME.PRODUCTTYPE)    DATA_.GET_LOT.ProductType = strCPValue;
                    
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                    m_gem.GetListItemClose(lMsgId);
                }
                m_gem.GetListItemClose(lMsgId);
                DATA_.bLotLoss      = false;
                DATA_.bEqpChange    = false;
                nHCACK = 0;
            }       //Tracking 2
            else if (strCommand == RCMD.LOT_START){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID) DATA_.GET_LOT.LotID = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE) DATA_.GET_LOT.LotType = int.Parse(strCPValue);
                    
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                    m_gem.GetListItemClose(lMsgId);
                }
                m_gem.GetListItemClose(lMsgId);
                nHCACK = 0;
            }
            else{
                //------------- 존재하지 않는 CPNAME ----------
                nHCACK = 3;
            }

            m_gem.ReplyRemoteCommand(lMsgId, nHCACK);

            //if (strCommand == RCMD.PPSELECT){
            //    //레시피 변경이 가능한지 확인 
            //    if (strPortId != "1" && strPortId != "2"){
            //        nHCACK = 3; // 파라미터가 정의와 맞지 않음.
            //    }
            //    if (nCntOfSubSt < 1){
            //        nHCACK = 3;
            //    }
            //    // 현재 레시피를 변경 가능한 상태이면 0, 이미 작업중이라던지 변경이 불가능한 경우 2
            //    SendS2F42(lMsgId, nHCACK);
            //}
            //else{
            //    nHCACK = 1; // RCMD가 존재하지 않음.
            //    SendS2F42(lMsgId, nHCACK);
            //}

            if (strCommand == RCMD.POP_CONDITION){
                SetPopCondition();
            }
            else if (strCommand == RCMD.LOT_INFO){
                SetPPSelect();
            }
            else if (strCommand == RCMD.LOT_START){
                SetLotStarted();
            }
        }

        public void OnNewHOST_ECID(int lMsgId){
            int nECCount = 0;
            int nECID = 0;
            string strNewValue = "";

            nECCount = 0;
            while (nECCount > -1){
                nECCount = m_gem.GetHostSetECID(lMsgId, ref nECID, ref strNewValue); // HOST에서 전송한 ECID, ECVALUE를 가져옴.
                if (nECCount < 0) break;

                AddGemLog(string.Format("ECID={0},VALUE={1}", nECID, strNewValue));
                m_gem.SetECValue(nECID, strNewValue); //해당 값을 ECID에 저장.
            }
            m_gem.ReplyHostSetECID(lMsgId, 0); // ECID에 대한 응답을 보냄.
        }

        public void OnTerminalMsg(int lMsgId){
            // S10F3, S10F5를 받은 경우. 장비화면이나 팝업화면에 표시 
            int nTID = 0;
            int nCount = 0;
            string strMessage = "";
            string strMessage2 = "";

            nCount = 0;
            while (nCount > -1){
                nCount = m_gem.GetTerminalMsg(lMsgId, ref nTID, ref strMessage);
                AddGemLog(string.Format("TID={0},MESSAGE={1}", nTID, strMessage));

                strMessage2 += strMessage + "\r\n";
                // nCount는 Message를 처리하고 난 후 남은 갯 수를 리턴함. 0인 경우 더이상 받아 올 수 없음.
                if (nCount <= 0) break;
            }
            AddGemLog(string.Format("{0}", strMessage2));
            //터미널 메세지의 응답은 불필요.
        }

        public void OnOtherEvent(short nEventId, int lParam){
            short nEnable = 0;
            int nAck = 0;
            AddGemLog(string.Format("EVENT={0},lPARAM={1}", nEventId, lParam));

            if (nEventId == 501)
            { //S2F37
                if (lParam >= 10){
                    nAck = lParam - 10;     // 1의 자리는 응답코드, 0:OK, 1,2,3,.. = NACK
                    nEnable = 1;           // enable
                }
                else{
                    nAck = lParam;          // 1의 자리는 응답코드, 0:OK, 1,2,3,.. = NACK
                    nEnable = 0;           // disable
                }

                if (nEnable == 1 && nAck == 0){ // 응답코드가 0이고, event를 enable시킴
                    if (DATA_.m_nControlState == ControlValue.CONTROL_REMOTE){
                        SetControlState(CEID.CONTROL_STATE_ONLINE_REMOTE);
                    }
                    else if (DATA_.m_nControlState == ControlValue.CONTROL_LOCAL){
                        SetControlState(CEID.CONTROL_STATE_ONLINE_LOCAL);
                    }
                }
            }
        }

        private void OnMsgReceived(IntPtr lpParam, int lMsgId){
            //////////////////////////////////////////////////
            short nStream = 0, nFunction = 0, nWbit = 0;
            int nLength = 0;
            m_gem.GetMsgInfo(lMsgId, ref nStream, ref nFunction, ref nWbit, ref nLength);

            if (DATA_.m_nControlState == ControlValue.CONTROL_EQ_OFFLINE){
                m_gem.AbortMsg(lMsgId);
                return;
            }

            if (nStream == 7 && nFunction == 3)         OnS7F3(lMsgId);
            else if (nStream == 7 && nFunction == 5)    OnS7F5(lMsgId);

            else if (nStream == 7 && nFunction == 17)   OnS7F17(lMsgId);            
            else if (nStream == 7 && nFunction == 19)   OnS7F19(lMsgId);

            else if (nStream == 10 && nFunction == 3)   OnS10F3(lMsgId);
            else if (nStream == 10 && nFunction == 5)   OnS10F5(lMsgId);
            
            else if (nStream == 6 && nFunction == 12)   OnS6F12(lMsgId);

            else if (nStream == 2 && nFunction == 41)   OnS2F41(lMsgId);
            else if (nStream == 2 && nFunction == 49)   OnS2F49(lMsgId);
            else if (nStream == 6 && nFunction == 14)   m_gem.CloseMsg(lMsgId);
        }

        public void OnS7F3(int lMsgId){
            ///////////  Process Program Send(PPS)
            string strPPID      = "";
            byte nACKC7         = 0x03;
            string strFilename  = "";

            m_gem.GetListItemOpen(lMsgId);
            //GetAsciiItem((long)lMsgId, strPPID);	//Device Name을 strPPID에 담아 줌

            m_gem.GetAsciiItem(lMsgId, ref strPPID);

            strFilename = strPPID + ".rcp";
            m_gem.GetFileBinaryItem(lMsgId, strFilename);

            m_gem.GetListItemClose(lMsgId);

            AddGemLog(strPPID);

            int rMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.AddBinaryItem(lMsgId, nACKC7);

            ///////// ACKC7의 응답값 /////////////////
            // 0 : Accepted
            // 1 : Permission not granted
            // 2 : Length error
            // 3 : Matrix overflow
            // 4 : PPID not found
            // 5 : Mode unsupported
            // 6 : Command will be performed

            m_gem.SendMsg(rMsgId);
        }

        public void OnS7F5(int lMsgId){
            string strPPID = "";
            m_gem.GetAsciiItem(lMsgId, ref strPPID);

            //strPPID에 들어 있는 Device Name으로 해당 Device에 대한 파일 생성하여 그 Path Name을 반한하는 함수를 만들어서 사용
            string strPathName = strPPID + ".rcp";

            //S7F6을 생성하는 부분
            int rMsgId = m_gem.CreateReplyMsg(lMsgId);

            m_gem.OpenListItem(rMsgId);
            if (strPathName != ""){       //해당 DeviceFile 이 존재 하면 추가 없다면 추가 하지 않음
                m_gem.AddAsciiItem(rMsgId, strPPID, strPPID.Length);
                m_gem.AddFileBinaryItem(rMsgId, strPathName);	//File PathName을 기반으로 파일을 추가 함
            }
            m_gem.CloseListItem(rMsgId);
            m_gem.SendMsg(rMsgId);			//Host로 S7F6 전송
        }

        public void OnS7F17(int lMsgId){
            byte nACK7 = 0x00;
            short nCount = 0;
            string strPPID = "";
            
            List<string> listPPID = new List<string>();

            ///////// ACKC7의 응답값 /////////////////
            // 0 : Accepted
            // 1 : Permission not granted
            // 2 : Length error
            // 3 : Matrix overflow
            // 4 : PPID not found
            // 5 : Mode unsupported
            // 6 : Command will be performed

            nACK7 = 0x00;
            nCount = m_gem.GetListItemOpen(lMsgId);
            ///////////////// 삭제할 레시피의 개수와 삭제가 가능한지 여부 파악 //////////////
            if (nCount == 0){
                ////////////// 저장된 모든 레시피 삭제 
                nACK7 = 0x00;
            }
            else{
                ////////////// 삭제할 레시피 목록이 내려옴
                ////들어온 strPPID가 삭제가 가능한지, 존재하는 레시피인지 검색
                for (int i = 0; i < nCount; i++){
                    strPPID = "";
                    m_gem.GetAsciiItem(lMsgId, ref strPPID);

                    listPPID.Add(strPPID);
                }
                ////////// listPPID에 저장된 모든 레시피가 삭제가 가능한 경우 nACK7 == 0x00;
                ////////// 삭제가 불가능하거나 없는 경우 위의 ACKC7의 응답값 을 참조하여 설정 ///////////////////
            }
            m_gem.GetListItemClose(lMsgId);


            ////////////// 실제로 레시피를 삭제하는 부분 ///////////////////////////
            if (nACK7 == 0x00 && nCount > 0){
                /// strPPID에 저장된 레시피 삭제
                for (int j = 0; j < nCount; j++){
                    AddGemLog(listPPID[j]);
                }
            }
            else if (nACK7 == 0x00 && nCount == 0){
                //***********전체 레시피 삭제 ***************//
                //******************************************//
            }
            nACK7 = 0x04;

            /////// S7,F17의 응답인 S7,F18 Message를 만듬(rMsgId)
            int rMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.AddBinaryItem(lMsgId, nACK7);
            m_gem.SendMsg(rMsgId);
        }

        public void GetCurrentRecipeList(){
            m_listPPID.Clear();

            //== 실제 장비의 레시피 이름을 적음
            m_listPPID.Add("PPID_01");
            m_listPPID.Add("PPID_02");
            m_listPPID.Add("PPID_03");
            m_listPPID.Add("PPID_04");
        }
        public void OnS7F19(int lMsgId){
            //20100701
            //listPPID는 전역 변수 입니다. Host로 부터 Recipe ID List Request 가 왔으므로 해당 Recipe List를 전송해야 합니다.
            //이 함수를 타는 시점에 listPPID에 Recipe List를 Update 하는 부분을 추가 해 두면 됩니다.

            GetCurrentRecipeList();
            int nCount = 0;
            nCount = m_listPPID.Count;
            /// 현재 장비에 저장된 Recipe목록을 Host로 보내는 부분

            int rMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.OpenListItem(rMsgId);
            for (int i = 0; i < nCount; i++){
                string strPPID = "";
                strPPID = m_listPPID[i];
                m_gem.AddAsciiItem(rMsgId, strPPID, strPPID.Length);

                //AddGemLog(strPPID);
            }

            m_gem.CloseListItem(rMsgId);
            m_gem.SendMsg(rMsgId);
        }

        public void OnS2F41(int lMsgId){ // OnRemoteCommand
            //--------------------------------------------------------
            List<string> listWaferMap = new List<string>();

            byte nHcack = 0;
            string strRcmd = "", strCPName = "", strCPValue = "";

            short nCntOfCP = 0;
            short nCntOfSubSt = 0;

            short i = 0;

            m_gem.GetListItemOpen(lMsgId);
            m_gem.GetAsciiItem(lMsgId, ref strRcmd);
            byte nPort = 0;
            string strLotId = "", strCarrierId = "", strPortId = "", strPPID = "";
            //---------------------------------------------------------------------------------------------------------------
            nCntOfCP = m_gem.GetListItemOpen(lMsgId);
            for (i = 0; i < nCntOfCP; i++){
                m_gem.GetListItemOpen(lMsgId);
                m_gem.GetAsciiItem(lMsgId, ref strCPName);

                if (strCPName == CPNAME.LOTID){
                    m_gem.GetAsciiItem(lMsgId, ref strCPValue);     // A
                    strLotId = strCPValue;
                }
                else if (strCPName == CPNAME.CARRIERID){
                    m_gem.GetAsciiItem(lMsgId, ref strCPValue);     // A
                    strCarrierId = strCPValue;
                }
                else if (strCPName == CPNAME.PORTID){
                    m_gem.GetU1Item(lMsgId, ref nPort);         // U1
                    strPortId = nPort.ToString();
                }
                else if (strCPName == CPNAME.PPID){
                    m_gem.GetAsciiItem(lMsgId, ref strCPValue);         // U1
                    strPPID = strCPValue;
                }
                else if (strCPName == CPNAME.WAFERMAP){
                    AddGemLog("-----" + strCarrierId + "'s Wafer Map--------");
                    nCntOfSubSt = m_gem.GetListItemOpen(lMsgId);    // <L
                    for (short j = 0; j < nCntOfSubSt; j++){
                        string strSubst = "";
                        m_gem.GetAsciiItem(lMsgId, ref strSubst);   //      <A
                        listWaferMap.Add(strSubst);
                        AddGemLog(strSubst);
                    }
                    m_gem.GetListItemClose(lMsgId);                 // >
                    AddGemLog("---------------------------");
                }
                else{
                    //------------- 존재하지 않는 CPNAME ----------
                    nHcack = 3;
                }
                m_gem.GetListItemClose(lMsgId);
            }
            m_gem.GetListItemClose(lMsgId);
            //---------------------------------------------------------------------------------------------------------------

            m_gem.GetListItemClose(lMsgId);
            if (strRcmd == RCMD.PPSELECT){
                //레시피 변경이 가능한지 확인 
                if (strPortId != "1" && strPortId != "2"){
                    nHcack = 3; // 파라미터가 정의와 맞지 않음.
                }

                if (nCntOfSubSt < 1){
                    nHcack = 3;
                }
                // 현재 레시피를 변경 가능한 상태이면 0, 이미 작업중이라던지 변경이 불가능한 경우 2
                SendS2F42(lMsgId, nHcack);
            }
            else{
                nHcack = 1; // RCMD가 존재하지 않음.
                SendS2F42(lMsgId, nHcack);
            }
        }

        public void SendS2F42(int lMsgId, byte nHCACK){
            int rMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.OpenListItem(rMsgId);
            //m_gem.AddAsciiItem(rMsgId, strRcmd, strRcmd.Length);
            m_gem.AddBinaryItem(rMsgId, nHCACK);
            m_gem.OpenListItem(rMsgId);

            m_gem.CloseListItem(rMsgId);
            m_gem.CloseListItem(rMsgId);
            m_gem.SendMsg(rMsgId);
        }

        public void OnS2F49(int lMsgId){
            //
            lMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.SendMsg(lMsgId);
        }

        public void OnS10F3(int lMsgId){
            byte nTid = 0, nACKC10 = 0;
            string strMessage = "";
            m_gem.GetListItemOpen(lMsgId);
            {
                m_gem.GetBinaryItem(lMsgId, ref nTid);
                m_gem.GetAsciiItem(lMsgId, ref strMessage);
            }
            m_gem.GetListItemClose(lMsgId);

            AddGemLog(string.Format("MSG:{0}", strMessage));
            // 응답인 S10F4를 만드는 부분
            lMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.AddBinaryItem(lMsgId, nACKC10);
            m_gem.SendMsg(lMsgId); // S10F4를 전송함.

            lblTerminalMsg.Text = strMessage;
        }

        public void OnS10F5(int lMsgId){
            byte nTid = 0, nACKC10 = 0;
            int nCnt = 0;
            string strMessage = "", strTotalMessage = "";
            m_gem.GetListItemOpen(lMsgId);
            {
                m_gem.GetBinaryItem(lMsgId, ref nTid);
                nCnt = m_gem.GetListItemOpen(lMsgId);
                for (int i = 0; i < nCnt; i++){
                    m_gem.GetAsciiItem(lMsgId, ref strMessage);

                    if (i == 0) strTotalMessage = strMessage;
                    else strTotalMessage = string.Format("{0}\r\n{1}", strTotalMessage, strMessage);
                }
                AddGemLog(string.Format("MSG:{0}", strTotalMessage));
            }
            m_gem.GetListItemClose(lMsgId);

            // 응답인 S10F6를 만드는 부분
            lMsgId = m_gem.CreateReplyMsg(lMsgId);
            m_gem.AddBinaryItem(lMsgId, nACKC10);
            m_gem.SendMsg(lMsgId); // S10F6를 전송함.
        }

        public void OnS6F12(int lMsgId){
            byte nAckc6 = 0;
            //AddGemLog(m_gem.GetSysByteEx(CEID.LOT_COMPLETE).ToString());
            //AddGemLog(string.Format("m_nLotStart_System:{0}", m_nLotStart_System));
            //AddGemLog(string.Format("m_nLotEnd_System:{0}", m_nLotEnd_System));

            AddGemLog(m_gem.GetSysByteEx(CEID.PP_SELECTED).ToString());
            AddGemLog(string.Format("m_nPPSelected:{0}", m_nPPSelected));

            if (m_gem.GetSysByte(lMsgId) == m_nLotEnd_System){
                m_gem.GetBinaryItem(lMsgId, ref nAckc6);
                if (nAckc6 == 0){
                    AddGemLog("S6F12-LOT_END-OK ");
                }
                else{
                    AddGemLog("S6F12-LOT_END-NG ");
                }
            }
            else if (m_gem.GetSysByte(lMsgId) == m_nLotStart_System){
                m_gem.GetBinaryItem(lMsgId, ref nAckc6);
                if (nAckc6 == 0){
                    AddGemLog("S6F12-LOT_START-OK ");
                }
                else{
                    AddGemLog("S6F12-LOT_START-NG ");
                }
            }
            else if (m_gem.GetSysByte(lMsgId) == m_nPPSelected){
                m_gem.GetBinaryItem(lMsgId, ref nAckc6);
                if (nAckc6 == 0){
                    AddGemLog("S6F12-PPSelected-OK ");
                    SetLotStartRequest();
                }
                else{
                    AddGemLog("S6F12-PPSelected-NG ");
                }
            }
            m_gem.CloseMsg(lMsgId);
        }

        private void timerGem_Tick(object sender, EventArgs e){
            timerGem.Stop();
            StartGem();
        }

        private void btnStart_Click(object sender, EventArgs e){
            timerGem.Stop();
            StartGem();
        }
        private void btnStop_Click(object sender, EventArgs e){
            if (m_gem.Stop() == 0) AddGemLog("EZGEM(+) STOPPED");
            m_gem.GoOffline();

            timer_SetClock.Interval     = 1 * 1000;
            timer_SetClock.Enabled      = false;

            DATA_.m_nControlState       = ControlValue.CONTROL_EQ_OFFLINE;

            //---------button disalbed ----------------
            DATA_.sControlState         = "OFFLINE";
            DATA_.cConnectState         = System.Drawing.SystemColors.Control;
            lblControlState.Text        = DATA_.sControlState;
            lblControlState.BackColor   = DATA_.cConnectState;

            btnOffline.Enabled          = false;
            btnLocal.Enabled            = false;
            btnRemote.Enabled           = false;

            DATA_.sConnectState         = "DISCONNECT";
            DATA_.cConnectState         = Color.Red;
            lblConnectState.Text        = DATA_.sConnectState;
            lblConnectState.BackColor   = DATA_.cConnectState;

            DATA_.sCommState            = "NOT COMMUNICATION";
            DATA_.cCommState            = Color.Red;
            lblCommState.Text           = DATA_.sCommState;
            lblCommState.BackColor      = DATA_.cCommState;

            btnStart.Enabled            = true;
            btnStop.Enabled             = false;
            //------------------------------------------
        }
        private void btnOffline_Click(object sender, EventArgs e){
            if (DATA_.m_nControlState == ControlValue.CONTROL_LOCAL || DATA_.m_nControlState == ControlValue.CONTROL_REMOTE){
                DATA_.m_nPrevControlState   = DATA_.m_nControlState;
                DATA_.m_nControlState       = ControlValue.CONTROL_EQ_OFFLINE;

                m_gem.SetSVIDValue(SVID.CONTROL_STATE, DATA_.m_nControlState.ToString());
                //m_gem.SetSVIDValue(SVID.PREV_CONTROL_STATE, DATA_.m_nPrevControlState.ToString());
                //m_gem.SendEventReport(CEID.CONTROL_STATE_CHANGE);
            }

            m_gem.GoOffline();

            DATA_.sControlState         = "OFFLINE";
            DATA_.cConnectState         = System.Drawing.SystemColors.Control;
            lblControlState.Text        = DATA_.sControlState;
            lblControlState.BackColor   = DATA_.cConnectState;

            btnOffline.Enabled          = false;
            btnLocal.Enabled            = true;
            btnRemote.Enabled           = true;

            btnOffline.BackColor        = Color.Red;
            btnLocal.BackColor          = System.Drawing.SystemColors.Control; //Color.Gray;
            btnRemote.BackColor         = System.Drawing.SystemColors.Control; //Color.Gray;
        }
        private void btnRemote_Click(object sender, EventArgs e){
            m_gem.GoOnlineRemote();
        }
        private void btnLocal_Click(object sender, EventArgs e){
            m_gem.GoOnlineLocal();
        }

        public void SetControlState(int nCEID){
            m_gem.SetSVIDValue(SVID.EQUIPMENT_STATE, DATA_.m_nEqpState.ToString());
            m_gem.SetSVIDValue(SVID.CONTROL_STATE, DATA_.m_nControlState.ToString());
            m_gem.SetSVIDValue(SVID.USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(nCEID);
        } //GEM 상태 보고
        public void SetPrecessState(int nProcessState){
            DATA_.m_nPrevEqpState = DATA_.m_nEqpState;
            DATA_.m_nEqpState = nProcessState;
            m_gem.SetSVIDValue(SVID.EQUIPMENT_STATE, DATA_.m_nEqpState.ToString());
            m_gem.SetSVIDValue(SVID.CONTROL_STATE, DATA_.m_nControlState.ToString());
            
            int nCEID = CEID.EQUIPMENT_STATE_IDLE;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_INIT)        nCEID = CEID.EQUIPMENT_STATE_INIT;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_SETUP)       nCEID = CEID.EQUIPMENT_STATE_SETUP;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_READY)       nCEID = CEID.EQUIPMENT_STATE_READY;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_RUN)         nCEID = CEID.EQUIPMENT_STATE_RUN;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_DOWN)        nCEID = CEID.EQUIPMENT_STATE_DOWN;
            if (nProcessState == (int)EquipmentValue.EQUIPMENT_MANUAL)      nCEID = CEID.EQUIPMENT_STATE_MANUAL;
            m_gem.SendEventReport(nCEID);
        } // 설비 상태 보고
        public void SetEquipmentStatePM(){
            m_gem.SetSVIDValue(SVID.EQUIPMENT_STATE, DATA_.m_nPrevControlState.ToString());
            m_gem.SetSVIDValue(SVID.CONTROL_STATE, DATA_.m_nControlState.ToString());
            m_gem.SendEventReport(CEID.EQUIPMENT_STATE_IDLE/*CEID.EQUIPMENT_STATE_PM*/);
        } // 설비 PM 모드 보고
        public void OnAlarmSet(int nALID){
            short nALCD = 0x80;
            m_gem.SendAlarmReport(nALID, nALCD);
            //SetAlarm(nALID, 1);
            //OnS6F11W(CEID_ALARM.ToString() , "500", DATA_.WORK_LOT[0].LotID);
        } //설비 알람 
        public void OnAlarmClear(int nALID){
            short nALCD = 0x00;
            m_gem.SendAlarmReport(nALID, nALCD);
            //SetAlarm(nALID, 0);
            //OnS6F11W(CEID_ALARM.ToString(), "500", DATA_.WORK_LOT[0].LotID);
        } //설비 알람 리셋
        
        public void SetLotRequest(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.RESERVE_PANEL_QTY, DATA_.CurLotCnt.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_REQUEST);
        } // LOT CARD READING 시 보고 RECIPE VALIDATION 보고
        public void SetPopCondition(){
            //DATA_.GET_LOT.LotID = txtLotID.Text;
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.POP_CONDITION_COMPLETE);
        } // Pop Condition 확인 완료 보고
        public void SetLotCanceled(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_CANCELED);
        }
        public void SetLotLoss(string StartTime, string EndTime, string LossCode, string LossMessae){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.START_TIME, StartTime);
            m_gem.SetSVIDValue(SVID.END_TIME, EndTime);
            m_gem.SetSVIDValue(SVID.OPERATION_LOSS_COMMENT, LossMessae);
            m_gem.SetSVIDValue(SVID.OPERATION_LOSS_CODE, LossCode);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_EQP_LOSS_COMPLETE);
        }
        public void SetLotEqpChangeComplete(int nCode, string sComment){
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SetSVIDValue(SVID.EQP_CHANGE_COMMENT, sComment);
            m_gem.SetSVIDValue(SVID.EQP_CHANGE_CODE, nCode.ToString());
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SendEventReport(CEID.LOT_EQP_CHANGE_COMPLETE);
        }
        public void SetPPSelect(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            //m_gem.SendEventReport(CEID.PP_SELECTED);

            m_gem.SendEventReportEx(CEID.PP_SELECTED, CEID.PP_SELECTED);
            m_nPPSelected = m_gem.GetSysByteEx(CEID.PP_SELECTED);
            AddGemLog(string.Format("m_nPPSelected:{0}", m_nPPSelected));
        }
        public void SetUpperDFKitting(string OldBladeBarcode, string NewBaldeBarcode){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.CUR_LEFT_BLADE, NewBaldeBarcode);
            m_gem.SetSVIDValue(SVID.OLD_LEFT_BLADE, OldBladeBarcode);
            m_gem.SendEventReport(CEID.UPPER_DF_KITTING);
        }
        public void SetLowerDFKitting(string OldBladeBarcode, string NewBaldeBarcode){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.CUR_RIGHT_BLADE, NewBaldeBarcode);
            m_gem.SetSVIDValue(SVID.OLD_RIGHT_BLADE, OldBladeBarcode);
            m_gem.SendEventReport(CEID.LOWER_DF_KITTING);
        }
        public void SetLotStartRequest(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_START_REQUEST);
        }
        public void SetLotStarted(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_STARTED);

            //임시
            BTN_RUN.Enabled                 = true;
            BTN_LOT_LOADING.Enabled         = true;
            BTN_PANEL_LINE_IN.Enabled       = true;
            BTN_PANEL_MODULE_IN.Enabled     = true;
            BTN_PANEL_MODULE_OUT.Enabled    = true;
            BTN_PANEL_LINE_OUT.Enabled      = true;
            BTN_LOT_COMPLETE.Enabled        = true;
        }
        public void SetLotLoading(){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.RESERVE_PANEL_QTY, DATA_.CurLotCnt.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_LOADING);
        }
        public void SetPanelLineIn(int nPanelCnt, string sStripBarcode){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_ID, sStripBarcode);
            m_gem.SendEventReport(CEID.PANEL_LINE_IN);
        }
        public void SetPanelModuleIn(int nPanelCnt, string sStripBarcode, int sModuleID){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.MODULE_ID, sModuleID.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_ID, sStripBarcode);
            m_gem.SendEventReport(CEID.PANEL_MODULE_IN);
        }
        public void SetPanelModuleOut(int nPanelCnt, string sStripBarcode, int sModuleID){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.MODULE_ID, sModuleID.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_ID, sStripBarcode);
            m_gem.SendEventReport(CEID.PANEL_MODULE_OUT);
        }
        public void SetPanelLineOut(int nPanelCnt, string sStripBarcode){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.PANEL_ID, sStripBarcode);
            m_gem.SendEventReport(CEID.PANEL_LINE_OUT);
        }
        public void SetLotComplete(int nPanelCnt){
            m_gem.SetSVIDValue(SVID.LOT_ID, DATA_.GET_LOT.LotID);
            m_gem.SetSVIDValue(SVID.RECIPE_ID, DATA_.CurRecipe);
            m_gem.SetSVIDValue(SVID.LOT_TYPE, DATA_.CurLotType.ToString());
            m_gem.SetSVIDValue(SVID.RESERVE_PANEL_QTY, DATA_.CurLotCnt.ToString());
            m_gem.SetSVIDValue(SVID.COMPLETE_PANEL_QTY, nPanelCnt.ToString());
            m_gem.SetSVIDValue(SVID.PROCESS_USER_ID, DATA_.CurUserID);
            m_gem.SendEventReport(CEID.LOT_COMPLETE);
        }
        
        private void SetLotID_Click(object sender, EventArgs e){
            if (txtLotID.Text == ""){
                MessageBox.Show("LOT ID 입력 하셔야 합니다 !");
                return;
            }
            if (txtLotCnt.Text == ""){
                MessageBox.Show("LOT 수량 입력 하셔야 합니다 !");
                return;
            }
            DATA_.GET_LOT.LotID = txtLotID.Text;
            DATA_.CurLotCnt = int.Parse(txtLotCnt.Text);
        }
        private void SetUserID_Click(object sender, EventArgs e){
            if (txtUserID.Text == ""){
                MessageBox.Show("USER ID 입력 하셔야 합니다 !");
                return;
            }
            DATA_.CurUserID = txtUserID.Text;
        }
        private void SetRecipe_Click(object sender, EventArgs e){
            if (txtGroup.Text == "" || txtRecipe.Text == ""){
                MessageBox.Show("현재 DEVICE 그룹명 또는 레스피명이 입력 하셔야 합니다 !");
                return;
            }
            DATA_.CurRecipe = txtGroup.Text + txtRecipe.Text;
        }
        private void bEQSTATE_Click(object sender, EventArgs e){
            string sSelect = cbEquipmentState.Text;
            string[] sRslt = sSelect.Split(',');
            int nEQState = int.Parse(sRslt[0]);
            if (nEQState == (int)EquipmentValue.EQUIPMENT_MANUAL || nEQState == (int)EquipmentValue.EQUIPMENT_INIT || nEQState == (int)EquipmentValue.EQUIPMENT_SETUP || nEQState == (int)EquipmentValue.EQUIPMENT_READY || nEQState == (int)EquipmentValue.EQUIPMENT_DOWN){
                nEQState = (int)EquipmentValue.EQUIPMENT_IDLE;
            }
            SetPrecessState(nEQState);
        }
        private void ChkEQ_PM_CheckedChanged(object sender, EventArgs e){
            if (ChkEQ_PM.Checked){
                SetEquipmentStatePM();
            }
        }
        private void BTN_ALARM_SET_Click(object sender, EventArgs e){
            if (TXT_ALRAM_NUM.Text == ""){
                MessageBox.Show("알람 번호 입력 하셔야 합니다 !");
                return;
            }
            int nAlarm = int.Parse(TXT_ALRAM_NUM.Text);
            OnAlarmSet(nAlarm);
        }
        private void BTN_ALARM_RESET_Click(object sender, EventArgs e){
            if (TXT_ALRAM_NUM.Text == ""){
                MessageBox.Show("알람 번호 입력 하셔야 합니다 !");
                return;
            }
            int nAlarm = int.Parse(TXT_ALRAM_NUM.Text);
            OnAlarmClear(nAlarm);
        }      
        private void BTN_LOT_REQUEST_Click(object sender, EventArgs e){
            if (RBT_LOT_TYPE_1.Checked) DATA_.CurLotType = 1;
            else if (RBT_LOT_TYPE_2.Checked) DATA_.CurLotType = 2;
            else if (RBT_LOT_TYPE_3.Checked) DATA_.CurLotType = 3;
            else if (RBT_LOT_TYPE_4.Checked) DATA_.CurLotType = 4;
            else if (RBT_LOT_TYPE_5.Checked) DATA_.CurLotType = 5;
            else{
                MessageBox.Show("LOT 종류 선택 하셔야 합니다 !");
                return;
            }
            if (txtLotID.Text == ""){
                MessageBox.Show("LOT ID 입력 하셔야 합니다 !");
                return;
            }
            DATA_.GET_LOT.LotID = txtLotID.Text;
            SetLotRequest();
        }
        private void BTN_LOT_CANCELED_Click(object sender, EventArgs e){
            SetLotCanceled();
        }
        private void BTN_LOT_LOSS_Click(object sender, EventArgs e){
            if (txtLossStartTime.Text == "" || txtLossEndTime.Text == "") {
                MessageBox.Show("LOSS 시간 입력 하셔야 합니다 !");
                return;
            }
            if (!RDB_LOSS_1.Checked && !RDB_LOSS_2.Checked && !RDB_LOSS_3.Checked && !RDB_LOSS_4.Checked && !RDB_LOSS_5.Checked && !RDB_LOSS_6.Checked){
                MessageBox.Show("LOSS CODE 체크 하셔야 합니다 !");
                return;
            }
            string sStartTime   = txtLossStartTime.Text;
            string sEndTime     = txtLossEndTime.Text;

            string sLossCode = "";
            if (RDB_LOSS_1.Checked) sLossCode = RDB_LOSS_1.Text;
            if (RDB_LOSS_2.Checked) sLossCode = RDB_LOSS_2.Text;
            if (RDB_LOSS_3.Checked) sLossCode = RDB_LOSS_3.Text;
            if (RDB_LOSS_4.Checked) sLossCode = RDB_LOSS_4.Text;
            if (RDB_LOSS_5.Checked) sLossCode = RDB_LOSS_5.Text;
            if (RDB_LOSS_6.Checked) sLossCode = RDB_LOSS_6.Text;
            string sLossMemo = TXT_LOSS_MEMO.Text;

            SetLotLoss(sStartTime, sEndTime, sLossCode, sLossMemo);
        }
        private void BTN_EQP_CHANGE_COMPLETE_Click(object sender, EventArgs e){
            if (CBX_EQP_CHANGE_CODE.Text == "" || CBX_EQP_CHANGE_CODE.SelectedIndex < 0){
                MessageBox.Show("변경 원인 선택 하셔야 합니다 !");
                return;
            }
            string sSelect = cbEquipmentState.Text;
            string[] sRslt = sSelect.Split(',');
            int nCODE = int.Parse(sRslt[0]);
            string sEQP_CHANGE_COMMENT = TXT_EQP_CHANGE_COMMENT.Text;
            SetLotEqpChangeComplete(nCODE, sEQP_CHANGE_COMMENT);
        }
        private void BTN_UPPER_DF_KITTING_Click(object sender, EventArgs e){
            if (txtOldUpperBladeBarcode.Text == ""){
                MessageBox.Show("OLD BLADE BARCODE 입력 하셔야 합니다 !");
                return;
            }
            if (txtNewUpperBladeBarcode.Text == ""){
                MessageBox.Show("NEW BLADE BARCODE 입력 하셔야 합니다 !");
                return;
            }
            SetUpperDFKitting(txtOldUpperBladeBarcode.Text, txtNewUpperBladeBarcode.Text);
        }
        private void BTN_LOWER_DF_KITTING_Click(object sender, EventArgs e){
            if (txtOldLowerBladeBarcode.Text == ""){
                MessageBox.Show("OLD BLADE BARCODE 입력 하셔야 합니다 !");
                return;
            }
            if (txtNewLowerBladeBarcode.Text == ""){
                MessageBox.Show("NEW BLADE BARCODE 입력 하셔야 합니다 !");
                return;
            }
            SetLowerDFKitting(txtOldLowerBladeBarcode.Text, txtNewLowerBladeBarcode.Text);
        }
        private void BTN_LOT_LOADING_Click(object sender, EventArgs e){
            SetLotLoading();
        }
        private void BTN_PANEL_LINE_IN_Click(object sender, EventArgs e){
            int nCnt = int.Parse(txtPanelInCount.Text);
            string nStripBarcode = txtStripBarcode.Text;
            SetPanelLineIn(nCnt, nStripBarcode);
        }
        private void BTN_PANEL_MODULE_IN_Click(object sender, EventArgs e){
            int nCnt = int.Parse(txtPanelInCount.Text);
            string sSelect = cbx_ModuleName.Text;
            string[] sRslt = sSelect.Split(',');
            int nModuleID = int.Parse(sRslt[0]);
            string nStripBarcode = txtStripBarcode.Text;
            SetPanelModuleIn(nCnt, nStripBarcode, nModuleID);
        }
        private void BTN_PANEL_MODULE_OUT_Click(object sender, EventArgs e){
            int nCnt = int.Parse(txtPanelInCount.Text);
            string sSelect = cbx_ModuleName.Text;
            string[] sRslt = sSelect.Split(',');
            int nModuleID = int.Parse(sRslt[0]);
            string nStripBarcode = txtStripBarcode.Text;
            SetPanelModuleOut(nCnt, nStripBarcode, nModuleID);
        }
        private void BTN_PANEL_LINE_OUT_Click(object sender, EventArgs e){
            int nCnt = int.Parse(txtPanelInCount.Text);
            string nStripBarcode = txtStripBarcode.Text;
            SetPanelLineOut(nCnt, nStripBarcode);
        }
        private void BTN_LOT_COMPLETE_Click(object sender, EventArgs e){
            int nCnt = int.Parse(txtPanelInCount.Text);
            SetLotComplete(nCnt);
        }
        private void BTN_EXIT_Click(object sender, EventArgs e){ Close(); }

        private void timer1_Tick(object sender, EventArgs e){
            LBL_EQP_CODE_1.Text = DATA_.EQPCode;
            lblUserID.Text      = DATA_.CurUserID;

            if (DATA_.bLotLoss)     GBX_LOT_LOSS.Enabled = true;
            else                    GBX_LOT_LOSS.Enabled = false;
            
            if (DATA_.bEqpChange)   GBX_EQP_CHANGE.Enabled = true;
            else                    GBX_EQP_CHANGE.Enabled = false;
            

        }

        private void BTN_RUN_Click(object sender, EventArgs e){
            int nEQState = (int)EquipmentValue.EQUIPMENT_RUN;
            SetPrecessState(nEQState);
        }
    }
}
