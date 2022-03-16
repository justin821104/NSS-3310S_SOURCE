using LIB_.DateType;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LIB_.SubFROMLib
{
    public partial class SecsGem : Form
    {
        public EZGemPlusCS.CEZGemPlusLib m_gem = new EZGemPlusCS.CEZGemPlusLib(); // dll 참조
        public List<string> m_listPPID; // 테스트를 위하여 가지고 있는 레시피 리스트

        public LOTINFO_[] LotInfo = new LOTINFO_[3];
        
        public string[] strPPID1 = new string[1000];
        public string[] strPARA;

        public bool bIni                = false;    // gem 초기화 진행 여부
        public string strContiReadCstId = "";  //TCP/IP를 통해 계속들어옴
        public bool isReceivedTrackIn   = false; //상위로부터 START 호스트커맨드를 받았다.
        public bool isLotStartEnable    = false; //Lot Start 해도 되는 조건성립

        public void AddGemLog(string sLOG){
            try{
                if (InvokeRequired){
                    this.Invoke((MethodInvoker)delegate (){
                        AddGemLog(sLOG);
                    });
                }
                else{
                    if (lstLog.Items.Count > 1000) lstLog.Items.Clear();
                    string strTime  = DateTime.Now.ToString("MM/dd HH:mm:ss:fff");
                    string strWrite = "[" + strTime + "] " + sLOG;
                    lstLog.Items.Add(strWrite);
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                }
            }
            catch (Exception EX){
                LogWR_.SaveLogException("[GEM] ADD MESSAGE FAIL!" + ETC.NewLine + sLOG, EX);
            }
        }

        public SecsGem(){
            InitializeComponent();
            m_listPPID          = new List<string>();
            m_gem.OnEZGemEvent  += new EZGemPlusCS.ON_EZGEM_EVENT(OnEventReceived); // Gem내부 이벤트를 받음.
            m_gem.OnEZGemMsg    += new EZGemPlusCS.ON_EZGEM_MSG(OnMsgReceived);     // Host로 부터 받은 메세지를 전달.

            //StartGem(); // SEM 초기화!

            lblTerminalMsg.Text = "";
        }
        private void SecsGem_FormClosed(object sender, FormClosedEventArgs e) { 
            m_gem.Stop();
        }
        private void SecsGem_Load(object sender, EventArgs e){
            this.SetBounds(0, 0, this.Width, this.Height);

            INI_GEM();

            //---------- 버튼,라벨의 초기화 --------------
            CMES.sCommState             = "NOT COMMUNICATING";
            CMES.cCommState             = Color.Red;
            lblCommState.Text           = CMES.sCommState;
            lblCommState.BackColor      = CMES.cCommState;

            CMES.sControlState          = "OFFLINE";
            CMES.cConnectState          = Color.Red;
            lblControlState.Text        = CMES.sControlState;
            lblControlState.BackColor   = CMES.cConnectState;

            lblConnectState.Text        = CMES.Connection.DISCONNECTION_STRING;
            lblConnectState.BackColor   = Color.Red;

            btnRemote.Enabled           = false;
            btnLocal.Enabled            = false;
            btnOffline.Enabled          = false;

            //--------------------------------------------------
            //lstLog.DrawMode            = DrawMode.OwnerDrawFixed;
            //--------------------------------------------------

            StartGem(); // SEM 초기화!
            timer1.Enabled = true;
        }

        #region GEM 초기화
        public void INI_GEM(){
            CMES.m_nControlState        = CMES.ControlValue.CONTROL_EQ_OFFLINE;  //0 : OFFLINE
            CMES.m_nPrevControlState    = CMES.ControlValue.CONTROL_EQ_OFFLINE;  //0 : OFFLINE

            CMES.m_nEqpState            = CMES.EquipmentValue.EQUIPMENT_IDLE;
            CMES.m_nPrevEqpState        = CMES.EquipmentValue.EQUIPMENT_IDLE;
#if _GEM
            ReadGemIniFile();
#endif
            GetDialogValue();
        }
        public void ReadGemIniFile(){
            CMES.INI2VAL("GEM", "PORT", ref CMES.m_nPort);
            CMES.INI2VAL("GEM", "DEVICEID", ref CMES.m_nDeviceID);
            CMES.INI2VAL("GEM", "PASSIVE", ref CMES.m_nModeSelect);
            CMES.INI2VAL("GEM", "LINKTEST", ref CMES.m_nLinkInterval);
            CMES.INI2VAL("GEM", "RETRY", ref CMES.m_nRetry);
            CMES.INI2VAL("GEM", "T3", ref CMES.m_nT3);
            CMES.INI2VAL("GEM", "T5", ref CMES.m_nT5);
            CMES.INI2VAL("GEM", "T6", ref CMES.m_nT6);
            CMES.INI2VAL("GEM", "T7", ref CMES.m_nT7);
            CMES.INI2VAL("GEM", "T8", ref CMES.m_nT8);
            CMES.INI2VAL("GEM", "CONVERSATIONTIMEOUT", ref CMES.m_nCTTime);
            CMES.INI2VAL("GEM", "COMMREQUESTTIMEOUT", ref CMES.m_nCommReqeustTimeout);
            CMES.INI2VAL("GEM", "TIMEFORMAT", ref CMES.m_nFormatTime);

            CMES.m_strIP        = CMES.GetIniValue("GEM", "IP", PATH_.PROJECT + "GEM.ini");
            CMES.m_strMODE      = CMES.GetIniValue("GEM", "MODE", PATH_.PROJECT + "GEM.ini");
            CMES.m_strModelName = CMES.GetIniValue("GEM", "EQUIP_NAME", PATH_.PROJECT + "GEM.ini");
            CMES.m_strSoftRev   = CMES.GetIniValue("GEM", "SOFTREV", PATH_.PROJECT + "GEM.ini");
        }
        public void GetDialogValue(){
            lbPort.Text         = CMES.m_nPort.ToString();
            lbDevice.Text       = CMES.m_nDeviceID.ToString();
            lbT3.Text           = CMES.m_nT3.ToString();
            lbT5.Text           = CMES.m_nT5.ToString();
            lbT6.Text           = CMES.m_nT6.ToString();
            lbT7.Text           = CMES.m_nT7.ToString();
            lbT8.Text           = CMES.m_nT8.ToString();
            lbLinkTest.Text     = CMES.m_nLinkInterval.ToString();
            lbCommRequest.Text  = CMES.m_nCommReqeustTimeout.ToString();

            if (m_gem.PassiveMode == 1) lbModeSelect.Text = "PASSIVE MODE";
            else                        lbModeSelect.Text = "ACTIVE MODE";
        }
        #endregion GEM 초기화

        public void StartGem(){
            INI_GEM();

            m_gem.DeviceID          = (short)CMES.m_nDeviceID;           // Default = 0
            m_gem.PassiveMode       = (short)CMES.m_nModeSelect;         // Default = 1 (true) = active mode
            m_gem.T3                = (short)CMES.m_nT3;                 // wait interval time for response
            m_gem.T5                = (short)CMES.m_nT5;                 // wait interval time for Reconnected( Active Mode )
            m_gem.T6                = (short)CMES.m_nT6;                 // wait interval time for Control message
            m_gem.T7                = (short)CMES.m_nT7;                 // wait interval time between connected and selected
            m_gem.T8                = (short)CMES.m_nT8;                 // multi message accepted time
            m_gem.Port              = (short)CMES.m_nPort;               // Default = 5000
            m_gem.RetryCount        = (short)CMES.m_nRetry;              // Default = 0 
            m_gem.LinkTestInterval  = (short)CMES.m_nLinkInterval;       // Default = 30 sec

            m_gem.CommRequest       = (short)CMES.m_nCommReqeustTimeout; //  interval time between selected <-> S1F13w

            //------------- Method
            m_gem.SetIP(CMES.m_strIP);                               // Default = "127.0.0.1" (localhost)
            m_gem.SetLogFile(PATH_.LogMES + "GEM.LOG");
            m_gem.SetLogRetention(30);

            //m_gem.SetFormatFile(string.Format("FORMAT_U4.SML")); // S9계열 error메세지를 거르기 위하여 미리 포멧을 정의
            m_gem.SetFormatFile(PATH_.PROJECT + "FORMAT.SML");	 //SML파일경로 지정할것
            m_gem.SetFormatCheck(true);

            m_gem.SetReportFilePath(PATH_.PROJECT + "EZGEM.RPT");

            //각 ID는 미리 등록을 해두어야 사용이 가능하다.
            AddSVID();    // SVID등록하기
            AddCEID();    // CEID등록하기
            AddALID();     // ALAMR등록하기
            AddECID();    // ECID등록하기

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

            m_gem.SetModelName(CMES.m_strModelName);         //MODEL_NAME
            m_gem.SetSoftRev(CMES.m_strSoftRev);             //SOFTREV

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
                AddGemLog(string.Format("PORT={0}", CMES.m_nPort));
                AddGemLog(string.Format("DEVICE={0}", CMES.m_nDeviceID));
                AddGemLog("EZGEM DLL (+) STARTED");

                btnStart.Enabled = false;
                btnStop.Enabled = true;
                //---------------------------------------------------------
            }
            bIni = true;
        }

        public void AddSVID(){
            int nCount = 0;
            string filePath = string.Format("{0}SVID.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("SVID.TXT is not Exist. Check file."));
                return;
            }
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string strId = "";
                        string strName = "";
                        string strType = "";
                        //string strFormat = "";

                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                        if (SplitNum.Length > 2){
                            strId = SplitNum[0];
                            strName = SplitNum[2];
                            strType = SplitNum[1];
                            //strFormat = "";

                            m_gem.AddSVID(int.Parse(strId), strName, strType, ""); // ALID, ptr, strALCD
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }

            filePath = string.Format("{0}SVID1.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("SVID1.TXT is not Exist. Check file."));
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

            filePath = string.Format("{0}SVID2.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("SVID2.TXT is not Exist. Check file."));
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

                            SUBFRM_.gSecsGem.m_gem.AddSVID(int.Parse(strId), strName, strType, ""); // ALID, ptr, strALCD
                            nCount++;
                        }
                    }
                    sr.Close();
                }
                fsIn.Close();
            }
            AddGemLog(string.Format("SVID ADD {0} COUNT", nCount));
        }
        public void AddCEID(){
            string filePath = string.Format("{0}CEID.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("CEID.TXT is not Exist. Check file."));
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
        } //CEID (COLLECTIVE EVENT ID) 등록하기
        public void AddALID(){
            string filePath = string.Format("{0}ALARM.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("ALARM.TXT is not Exist. Check file."));
                return;
            }

            int nCount = 0;
            using (FileStream fsIn = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                using (StreamReader sr = new StreamReader(fsIn, Encoding.Default)){
                    while (sr.Peek() > -1){
                        string input = sr.ReadLine();
                        char[] Separators = new char[] { '\t' };
                        string[] SplitNum = input.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

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
        } // ALARM ID 등록하기
        public void AddECID(){
            string filePath = string.Format("{0}ECID.txt", PATH_.PROJECT);
            if (!File.Exists(filePath)){
                AddGemLog(string.Format("ECID.TXT is not Exist. Check file."));
                StandardECID();
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
            m_gem.SetECValue(CECID.ECID_PORT_ID, CMES.m_nPort.ToString());
            m_gem.SetECValue(CECID.ECID_DEVICEID, CMES.m_nDeviceID.ToString());
            m_gem.SetECValue(CECID.ECID_T3, CMES.m_nT3.ToString());
            m_gem.SetECValue(CECID.ECID_T5, CMES.m_nT5.ToString());
            m_gem.SetECValue(CECID.ECID_T6, CMES.m_nT6.ToString());
            m_gem.SetECValue(CECID.ECID_T7, CMES.m_nT7.ToString());
            m_gem.SetECValue(CECID.ECID_T8, CMES.m_nT8.ToString());
            m_gem.SetECValue(CECID.ECID_LINKTEST, CMES.m_nLinkInterval.ToString());
            m_gem.SetECValue(CECID.ECID_RETRY, CMES.m_nRetry.ToString());

            m_gem.SetECValue(CECID.ECID_ESTABLISH_TIMEOUT, CMES.m_EstablishTimeout.ToString());
            m_gem.SetECValue(CECID.ECID_TIME_FORMAT, CMES.m_nFormatTime.ToString());
            m_gem.SetECValue(CECID.ECID_EQUIPMENT_NAME, CMES.m_strModelName);

            AddGemLog(string.Format("ECID ADD {0} COUNT", nCount));
        } // ECID (EQUIPMENT CONSTANT ID) 등록하기
        public void StandardECID(){
            m_gem.AddECID(CECID.ECID_ESTABLISH_TIMEOUT, "EstablishCommTimeOut", "", "U2");
            m_gem.SetECRange(CECID.ECID_ESTABLISH_TIMEOUT, "0", "60");
            m_gem.SetECValue(CECID.ECID_ESTABLISH_TIMEOUT, "30");

            m_gem.AddECID(CECID.ECID_T3, "T3 Timeout", "second", "U1");
            m_gem.SetECRange(CECID.ECID_T3, "1", "120");
            m_gem.SetECValue(CECID.ECID_T3, CMES.m_nT3.ToString());

            m_gem.AddECID(CECID.ECID_T5, "T5 Timeout", "second", "U1");
            m_gem.SetECRange(CECID.ECID_T5, "1", "240");
            m_gem.SetECValue(CECID.ECID_T5, CMES.m_nT5.ToString());

            m_gem.AddECID(CECID.ECID_T6, "T6 Timeout", "second", "U1");
            m_gem.SetECRange(CECID.ECID_T6, "1", "240");
            m_gem.SetECValue(CECID.ECID_T6, CMES.m_nT6.ToString());

            m_gem.AddECID(CECID.ECID_T7, "T7 Timeout", "second", "U1");
            m_gem.SetECRange(CECID.ECID_T7, "1", "240");
            m_gem.SetECValue(CECID.ECID_T7, CMES.m_nT7.ToString());

            m_gem.AddECID(CECID.ECID_T8, "T8 Timeout", "second", "U1");
            m_gem.SetECRange(CECID.ECID_T8, "1", "120");
            m_gem.SetECValue(CECID.ECID_T8, CMES.m_nT8.ToString());

            m_gem.AddECID(CECID.ECID_LINKTEST, "LinkTestInterVal", "second", "U1");
            m_gem.SetECRange(CECID.ECID_LINKTEST, "0", "240");
            m_gem.SetECValue(CECID.ECID_LINKTEST, CMES.m_nLinkInterval.ToString());

            m_gem.AddECID(CECID.ECID_RETRY, "RetryLimit", "Count", "U1");
            m_gem.SetECRange(CECID.ECID_RETRY, "0", "5");
            m_gem.SetECValue(CECID.ECID_RETRY, CMES.m_nRetry.ToString());

            m_gem.AddECID(CECID.ECID_TIME_FORMAT, "TimeFormat", "", "U1");
            m_gem.SetECRange(CECID.ECID_TIME_FORMAT, "0", "1");
            m_gem.SetECValue(CECID.ECID_TIME_FORMAT, "0");

            m_gem.AddECID(CECID.ECID_PORT_ID, "PortNumber", "", "A");
            m_gem.SetECRange(CECID.ECID_PORT_ID, "5000", "10000");
            m_gem.SetECValue(CECID.ECID_PORT_ID, CMES.m_nPort.ToString());

            m_gem.AddECID(CECID.ECID_DEVICEID, "DeviceID", "", "A");
            m_gem.SetECRange(CECID.ECID_DEVICEID, "0", "1024");
            m_gem.SetECValue(CECID.ECID_DEVICEID, CMES.m_nDeviceID.ToString());

            m_gem.AddECID(CECID.ECID_EQUIPMENT_NAME, "EquipmentName", "", "A");
            m_gem.SetECRange(CECID.ECID_EQUIPMENT_NAME, "5000", "10000");
            m_gem.SetECValue(CECID.ECID_EQUIPMENT_NAME, PATH_.MACHINE_NAME);
        }

        #region SWITCH EVENT
        private void GEM_Start_Click(object sender, EventArgs e)    { StartGem(); }
        private void GEM_Stop_Click(object sender, EventArgs e){
            if (m_gem.Stop() == 0) AddGemLog("EZGEM(+) STOPPED");
            m_gem.GoOffline();

            CMES.m_nPrevControlState    = CMES.m_nControlState;
            CMES.m_nControlState        = CMES.ControlValue.CONTROL_EQ_OFFLINE;

            //---------button disalbed ----------------
            CMES.sControlState          = "OFFLINE";
            CMES.cConnectState          = SystemColors.Control;
            lblControlState.Text        = CMES.sControlState;
            lblControlState.BackColor   = CMES.cConnectState;

            btnOffline.Enabled          = false;
            btnLocal.Enabled            = false;
            btnRemote.Enabled           = false;

            CMES.sConnectState          = "DISCONNECT";
            CMES.cConnectState          = Color.Red;
            lblConnectState.Text        = CMES.sConnectState;
            lblConnectState.BackColor   = CMES.cConnectState;

            CMES.sCommState             = "NOT COMMUNICATION";
            CMES.cCommState             = Color.Red;
            lblCommState.Text           = CMES.sCommState;
            lblCommState.BackColor      = CMES.cCommState;

            btnStart.Enabled            = true;
            btnStop.Enabled             = false;
            //------------------------------------------
        }
        private void GEM_Offline_Click(object sender, EventArgs e){
            if (CMES.m_nControlState == CMES.ControlValue.CONTROL_LOCAL || CMES.m_nControlState == CMES.ControlValue.CONTROL_REMOTE){
                CMES.m_nPrevControlState    = CMES.m_nControlState;
                CMES.m_nControlState        = CMES.ControlValue.CONTROL_EQ_OFFLINE;

                m_gem.SetSVIDValue(CSVID.CONTROL_STATE, CMES.m_nControlState.ToString());
                //m_gem.SetSVIDValue(CSVID.PREV_CONTROL_STATE, DATA_.m_nPrevControlState.ToString());
                //m_gem.SendEventReport(CEID.CONTROL_STATE_CHANGE);
            }
            m_gem.GoOffline();

            CMES.sControlState          = "OFFLINE";
            CMES.cConnectState          = SystemColors.Control;
            lblControlState.Text        = CMES.sControlState;
            lblControlState.BackColor   = CMES.cConnectState;

            btnOffline.Enabled          = false;
            btnLocal.Enabled            = true;
            btnRemote.Enabled           = true;

            btnOffline.BackColor        = Color.Red;
            btnLocal.BackColor          = SystemColors.Control; //Color.Gray;
            btnRemote.BackColor         = SystemColors.Control; //Color.Gray;
        }
        private void GEM_Remote_Click(object sender, EventArgs e)   { m_gem.GoOnlineRemote(); }
        private void GEM_Local_Click(object sender, EventArgs e)    { m_gem.GoOnlineLocal(); }
        #endregion SWITCH EVENT

        #region Gem내부 이벤트를 받음.
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
        } // Gem내부 이벤트를 받음.

        #region 호스트 연결 이벤트
        public void OnConnected(){
            this.Invoke(new MethodInvoker(delegate (){
                CMES.m_bConnected           = CMES.Connection.bCONNECTION;
                CMES.sConnectState          = "CONNECTED";
                CMES.cConnectState          = Color.Lime;

                lblConnectState.Text        = CMES.sConnectState;
                lblConnectState.BackColor   = CMES.cConnectState;
            }));

            if (CMES.m_nControlState == CMES.ControlValue.CONTROL_EQ_OFFLINE){
                CMES.sControlState          = "OFFLINE";
                CMES.cConnectState          = SystemColors.Control;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;
            }
            else if (CMES.m_nControlState == CMES.ControlValue.CONTROL_LOCAL){
                CMES.sControlState          = "LOCAL";
                CMES.cControlState          = Color.LightSkyBlue;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;
            }
            else if (CMES.m_nControlState == CMES.ControlValue.CONTROL_REMOTE){
                CMES.sControlState          = "REMOTE";
                CMES.cControlState          = Color.LightSkyBlue;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;
            }
        }
        public void OnDisconnected(){
            this.Invoke(new MethodInvoker(delegate () {
                CMES.m_bConnected           = CMES.Connection.bDISCONNECTION;
                CMES.sConnectState          = "DISCONNECTED";
                CMES.cConnectState          = Color.Red;

                lblConnectState.Text        = CMES.sConnectState;
                lblConnectState.BackColor   = CMES.cConnectState;

                btnRemote.Enabled           = false;
                btnLocal.Enabled            = false;
                btnOffline.Enabled          = false;

                if (CMES.m_nControlState == CMES.ControlValue.CONTROL_LOCAL || CMES.m_nControlState == CMES.ControlValue.CONTROL_REMOTE){
                    CMES.m_nPrevControlState    = CMES.m_nControlState;
                    CMES.m_nControlState        = CMES.ControlValue.CONTROL_EQ_OFFLINE;
                }
                m_gem.GoOffline();
                SetControlState(CCEID.CONTROL_STATE_OFFLINE);

                CMES.sControlState          = "OFFLINE";
                CMES.cConnectState          = SystemColors.Control;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;

                btnOffline.Enabled          = false;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = true;
            }));
        }
        #endregion 호스트 연결 이벤트
        #region SECS MESSAGE 송/수신 발생 이벤트 로그
        public void OnMsgIn(int lParam){
            int nStream, nFunction;
            nStream     = (int)(lParam / 1000);
            nFunction   = lParam % 1000;
            AddGemLog(string.Format("(H->E) S{0},F{1}", nStream, nFunction));
        }

        public void OnMsgOut(int lParam){
            int nStream, nFunction;
            nStream     = (int)(lParam / 1000);
            nFunction   = lParam % 1000;
            AddGemLog(string.Format("(E->H) S{0},F{1}", nStream, nFunction));
        }
        #endregion SECS MESSAGE 송/수신 발생 이벤트 로그

        public void OnOffline(){
            this.Invoke(new MethodInvoker(delegate (){
                CMES.m_nPrevControlState    = CMES.m_nControlState;
                CMES.m_nControlState        = CMES.ControlValue.CONTROL_HOST_OFFLINE;
                SetControlState(CCEID.CONTROL_STATE_OFFLINE);

                CMES.sControlState          = "OFFLINE";
                CMES.cConnectState          = SystemColors.Control;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;

                btnOffline.Enabled          = false;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = true;

                btnOffline.BackColor        = Color.Red;
                btnLocal.BackColor          = SystemColors.Control;
                btnRemote.BackColor         = SystemColors.Control;
            }));
        }

        public void OnOnlineLocal(){
            this.Invoke(new MethodInvoker(delegate (){
                CMES.m_nPrevControlState    = CMES.m_nControlState;
                CMES.m_nControlState        = CMES.ControlValue.CONTROL_LOCAL;
                SetControlState(CCEID.CONTROL_STATE_ONLINE_LOCAL);

                CMES.sControlState          = "LOCAL";
                CMES.cControlState          = Color.LightSkyBlue;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;

                btnOffline.Enabled          = true;
                btnLocal.Enabled            = false;
                btnRemote.Enabled           = true;

                btnOffline.BackColor        = SystemColors.Control;
                btnLocal.BackColor          = Color.Yellow;
                btnRemote.BackColor         = SystemColors.Control;
            }));
        }

        public void OnOnlineRemote(){
            this.Invoke(new MethodInvoker(delegate (){
                CMES.m_nPrevControlState    = CMES.m_nControlState;
                CMES.m_nControlState        = CMES.ControlValue.CONTROL_REMOTE;
                SetControlState(CCEID.CONTROL_STATE_ONLINE_REMOTE);

                CMES.sControlState          = "REMOTE";
                CMES.cControlState          = Color.LightSkyBlue;
                lblControlState.Text        = CMES.sControlState;
                lblControlState.BackColor   = CMES.cConnectState;

                btnOffline.Enabled          = true;
                btnLocal.Enabled            = true;
                btnRemote.Enabled           = false;

                btnOffline.BackColor        = SystemColors.Control;
                btnLocal.BackColor          = SystemColors.Control;
                btnRemote.BackColor         = Color.Green;
            }));
        }

        public void OnCummunicating(){
            this.Invoke(new MethodInvoker(delegate (){
                CMES.sCommState             = "COMMUNICATING";
                CMES.cCommState             = Color.Green;
                lblCommState.Text           = CMES.sCommState;
                lblCommState.BackColor      = CMES.cCommState;

                AddGemLog("COMMUNICATING");
            }));
        }

        public void OnRemoteCommand(int lMsgId){
            string strCommand = "", strCPName = "", strCPValue = "";
            int nParamCount = 0, nFormat = 0, nValueCount = 0;
            /*short*/
            byte nHCACK = 0;

            m_gem.GetListItemOpen(lMsgId);
            m_gem.GetAsciiItem(lMsgId, ref strCommand);

            if (strCommand == RCMD.LOT_CREATE){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.QTY)                CLOT.GET_LOT.Qty        = int.Parse(strCPValue);
                    if (strCPName == CPNAME.LOTTYPE)            CLOT.GET_LOT.LotType    = int.Parse(strCPValue);
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
                    if (strCPName == CPNAME.LOTID)              CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            CLOT.GET_LOT.LotType    = int.Parse(strCPValue);
                    if (strCPName == CPNAME.LOSS_START_TIME)    CLOT.LotLossStartTime   = strCPValue;
                    if (strCPName == CPNAME.LOSS_END_TIME)      CLOT.LotLossEndTime     = strCPValue;

                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                CLOT.bLotLoss = true;
                nHCACK = 0;
            }   //가동LOSS입력 1
            else if (strCommand == RCMD.LOT_EQP_CHANGE){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            CLOT.GET_LOT.LotType    = int.Parse(strCPValue);

                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                CLOT.bEqpChange = true;
                nHCACK = 0;
            } //설비호기변경 1
            else if (strCommand == RCMD.PARAMETER_INFO){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)              CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)            CLOT.GET_LOT.LotType    = int.Parse(strCPValue);

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
                        CLOT.GET_LOT.LotID = strCPValue;
                    }
                    if (strCPName == CPNAME.TOOLNO){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.ToolNo = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCCD){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.ProcCD = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCNAME){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.ProcName = strCPValue;
                    }
                    if (strCPName == CPNAME.WORKCONDITION){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.WorkCondition = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_1){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.ProcCondition_1 = strCPValue;
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_2){
                        nValueCount = m_gem.GetListItemOpen(lMsgId);
                        for (int j = 0; j < nValueCount; j++){
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                            if (j == 0) CLOT.GET_LOT.ProcCondition_2 = strCPValue;
                            if (j == 1) CLOT.GET_LOT.ProcCondition_3 = strCPValue;
                        }
                        m_gem.GetListItemClose(lMsgId);
                    }
                    if (strCPName == CPNAME.PROCESSCONDITION_3){
                        m_gem.GetListItemOpen(lMsgId);
                        {
                            m_gem.GetAsciiItem(lMsgId, ref strCPValue);
                        }
                        m_gem.GetListItemClose(lMsgId);
                        CLOT.GET_LOT.ProcCondition_4 = strCPValue;
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
                    if (strCPName == CPNAME.LOTID)          CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)        CLOT.GET_LOT.LotType    = int.Parse(strCPValue);
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                }
                m_gem.GetListItemClose(lMsgId);
                CLOT.bLotLoss = false;
                CLOT.bEqpChange = false;

                //CANCEL 사유 없음 ?? TC 요청하야 함 !
                SUBFRM_.gGemMessage.sMESSAGE_1 = "LOT " + CLOT.GET_LOT.LotID + " 등록 실패 상위단 확인 필요 !!" + ETC.NewLine + "[임시 실행 하려면 EES OFF 후 LOT 등록 후 실행하셔야 합니다!] ";
                SUBFRM_.gGemMessage.INI();
                CLOT.bLotCanceled = true;
                nHCACK = 0;
            }     //Tracking 4
            else if (strCommand == RCMD.LOT_INFO){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)          CLOT.GET_LOT.LotID          = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)        CLOT.GET_LOT.LotType        = int.Parse(strCPValue);
                    if (strCPName == CPNAME.QTY)            CLOT.GET_LOT.Qty            = int.Parse(strCPValue);
                    if (strCPName == CPNAME.PRODUCTTYPE)    CLOT.GET_LOT.ProductType    = strCPValue;
                    if (strCPName == CPNAME.TOOLNO)         CLOT.GET_LOT.ToolNo         = strCPValue;
                    if (strCPName == CPNAME.ITS)            CLOT.GET_LOT.ITS            = int.Parse(strCPValue);
                    if (strCPName == CPNAME.ITSLOTID_IN)    CLOT.GET_LOT.ITS_LotID_IN   = strCPValue;
                    if (strCPName == CPNAME.ITSLOTID_CT)    CLOT.GET_LOT.ITS_LotID_CT   = strCPValue;
                    if (strCPName == CPNAME.UNITSIZEX)      CLOT.GET_LOT.UnitSizeX      = double.Parse(strCPValue);
                    if (strCPName == CPNAME.UNITSIZEY)      CLOT.GET_LOT.UnitSizeY      = double.Parse(strCPValue);
                    if (strCPName == CPNAME.UNITSIZE_UPPER) CLOT.GET_LOT.UnitSize_USL   = double.Parse(strCPValue);
                    if (strCPName == CPNAME.UNITSIZE_LOWER) CLOT.GET_LOT.UnitSize_LSL   = double.Parse(strCPValue);
                    if (strCPName == CPNAME.ABFMATERIAL)    CLOT.GET_LOT.ABFMATERIAL    = strCPValue;
                    if (strCPName == CPNAME.LANDPKGX)       CLOT.GET_LOT.LANDPKGX       = double.Parse(strCPValue);
                    if (strCPName == CPNAME.LANDPKGX_UPPER) CLOT.GET_LOT.LANDPKGX_UPPER = double.Parse(strCPValue);
                    if (strCPName == CPNAME.LANDPKGX_LOWER) CLOT.GET_LOT.LANDPKGX_LOWER = double.Parse(strCPValue);
                    if (strCPName == CPNAME.LANDPKGY)       CLOT.GET_LOT.LANDPKGY       = double.Parse(strCPValue);
                    if (strCPName == CPNAME.LANDPKGY_UPPER) CLOT.GET_LOT.LANDPKGY_UPPER = double.Parse(strCPValue);
                    if (strCPName == CPNAME.LANDPKGY_LOWER) CLOT.GET_LOT.LANDPKGY_LOWER = double.Parse(strCPValue);
                    AddGemLog(string.Format("CPNAME:{0}", strCPName));
                    AddGemLog(string.Format("CPVALUE:{0}", strCPValue));
                    m_gem.GetListItemClose(lMsgId);
                }
                m_gem.GetListItemClose(lMsgId);
                CLOT.bLotLoss = false;
                CLOT.bEqpChange = false;
                nHCACK = 0;
            }       //Tracking 2
            else if (strCommand == RCMD.LOT_START){
                AddGemLog(string.Format("RCMD:{0}", strCommand));
                nParamCount = m_gem.GetListItemOpen(lMsgId);
                for (int n = 0; n < nParamCount; n++){
                    m_gem.GetRemoteCommandParam(lMsgId, n, ref strCPName, ref strCPValue, ref nFormat);
                    if (strCPName == CPNAME.LOTID)          CLOT.GET_LOT.LotID      = strCPValue;
                    if (strCPName == CPNAME.LOTTYPE)        CLOT.GET_LOT.LotType    = int.Parse(strCPValue);
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
            SUBFRM_.gGemMessage.sMESSAGE = strMessage2;
            SUBFRM_.gGemMessage.sMESSAGE_1 = "";
            SUBFRM_.gGemMessage.INI();
            //터미널 메세지의 응답은 불필요.
        }

        public void OnOtherEvent(short nEventId, int lParam){
            short nEnable = 0;
            int nAck = 0;
            AddGemLog(string.Format("EVENT={0},lPARAM={1}", nEventId, lParam));

            if (nEventId == 501){ //S2F37
                if (lParam >= 10){
                    nAck = lParam - 10;     // 1의 자리는 응답코드, 0:OK, 1,2,3,.. = NACK
                    nEnable = 1;           // enable
                }
                else{
                    nAck = lParam;          // 1의 자리는 응답코드, 0:OK, 1,2,3,.. = NACK
                    nEnable = 0;           // disable
                }

                if (nEnable == 1 && nAck == 0){ // 응답코드가 0이고, event를 enable시킴
                    if (CMES.m_nControlState == CMES.ControlValue.CONTROL_REMOTE){
                        SetControlState(CCEID.CONTROL_STATE_ONLINE_REMOTE);
                    }
                    else if (CMES.m_nControlState == CMES.ControlValue.CONTROL_LOCAL){
                        SetControlState(CCEID.CONTROL_STATE_ONLINE_LOCAL);
                    }
                }
            }
        }
        #endregion Gem내부 이벤트를 받음.

        #region Host로 부터 받은 메세지를 전달.
        private void OnMsgReceived(IntPtr lpParam, int lMsgId){
            //////////////////////////////////////////////////
            short nStream = 0, nFunction = 0, nWbit = 0;
            int nLength = 0;
            m_gem.GetMsgInfo(lMsgId, ref nStream, ref nFunction, ref nWbit, ref nLength);

            if (CMES.m_nControlState == CMES.ControlValue.CONTROL_EQ_OFFLINE){
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
        } // Host로 부터 받은 메세지를 전달.

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
            SUBFRM_.gLotID.MESMessage(strMessage);
            SUBFRM_.gGemMessage.sMESSAGE    = strMessage;
            SUBFRM_.gGemMessage.sMESSAGE_1  = "";
            SUBFRM_.gGemMessage.INI();
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
            if (m_gem.GetSysByte(lMsgId) == CCEID.m_nLotEnd_System) { 
                //AddGemLog(m_gem.GetSysByteEx(CEID.LOT_COMPLETE).ToString());
                AddGemLog(string.Format("m_nLotEnd_System:{0}", CCEID.m_nLotEnd_System));
                m_gem.GetBinaryItem(lMsgId, ref nAckc6);
                if (nAckc6 == 0){
                    AddGemLog("S6F12-LOT_END-OK ");
                }
                else{
                    AddGemLog("S6F12-LOT_END-NG ");
                }
            }
            else if (m_gem.GetSysByte(lMsgId) == CCEID.m_nLotStart_System){

                AddGemLog(string.Format("m_nLotStart_System:{0}", CCEID.m_nLotStart_System));
                m_gem.GetBinaryItem(lMsgId, ref nAckc6);
                if (nAckc6 == 0){
                    AddGemLog("S6F12-LOT_START-OK ");
                }
                else{
                    AddGemLog("S6F12-LOT_START-NG ");
                }
            }
            else if (m_gem.GetSysByte(lMsgId) == CCEID.m_nPPSelected){
                AddGemLog(m_gem.GetSysByteEx(CCEID.PP_SELECTED).ToString());
                AddGemLog(string.Format("m_nPPSelected:{0}", CCEID.m_nPPSelected));

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
        #endregion Host로 부터 받은 메세지를 전달.

        #region CEID 이벤트 처리
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

        public void SetControlState(int nCEID){
            m_gem.SetSVIDValue(CSVID.EQUIPMENT_STATE, CMES.m_nEqpState.ToString());
            m_gem.SetSVIDValue(CSVID.CONTROL_STATE, CMES.m_nControlState.ToString());
            m_gem.SetSVIDValue(CSVID.USER_ID, CUSER.Current.ID);
            SendEvent(nCEID);
        } //GEM 상태 보고
        public void SetPrecessState(int nProcessState){
            int nCEID = nProcessState;
            if (nProcessState == CCEID.EQUIPMENT_STATE_DOWN)    CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_DOWN;
            if (nProcessState == CCEID.EQUIPMENT_STATE_INIT)    CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_INIT;
            if (nProcessState == CCEID.EQUIPMENT_STATE_SETUP)   CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_SETUP;
            if (nProcessState == CCEID.EQUIPMENT_STATE_READY)   CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_READY;
            if (nProcessState == CCEID.EQUIPMENT_STATE_RUN)     CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_RUN;
            if (nProcessState == CCEID.EQUIPMENT_STATE_IDLE)    CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_IDLE;
            if (nProcessState == CCEID.EQUIPMENT_STATE_MANUAL)  CMES.m_nEqpState = CMES.EquipmentValue.EQUIPMENT_MANUAL;

            CMES.m_nPrevEqpState    = CMES.m_nEqpState;
            m_gem.SetSVIDValue(CSVID.EQUIPMENT_STATE, CMES.m_nEqpState.ToString());
            m_gem.SetSVIDValue(CSVID.CONTROL_STATE, CMES.m_nControlState.ToString());
            SendEvent(nCEID);
        } // 설비 상태 보고
        public void SetEquipmentStatePM(){
            m_gem.SetSVIDValue(CSVID.EQUIPMENT_STATE, CMES.m_nEqpState.ToString());
            m_gem.SetSVIDValue(CSVID.CONTROL_STATE, CMES.m_nControlState.ToString());
            m_gem.SendEventReport(CCEID.EQUIPMENT_STATE_IDLE/*CEID.EQUIPMENT_STATE_PM*/);
            SendEvent(CCEID.EQUIPMENT_STATE_IDLE); // << [변경 됨]CCEID.EQUIPMENT_STATE_PM
        } // 설비 PM 모드 보고

        public void SetLotRequest(string sLotID, int nLotType, int nLotCnt){
            m_gem.SetSVIDValue(CSVID.LOT_ID, sLotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName); //필요 없음? lot validation 완료 후 recipe 변경 
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, nLotType.ToString());
            m_gem.SetSVIDValue(CSVID.RESERVE_PANEL_QTY, nLotCnt.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_REQUEST);
        } // LOT CARD READING 시 보고 RECIPE VALIDATION 보고
        public void SetPopCondition(){
            //DATA_.GET_LOT.LotID = txtLotID.Text;
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.POP_CONDITION_COMPLETE);
        } // Pop Condition 확인 완료 보고
        public void SetLotCanceled(string sLotID, int nLotType){
            m_gem.SetSVIDValue(CSVID.LOT_ID, sLotID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, nLotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_CANCELED);
        }
        public void SetLotLoss(string LossCode, string LossMessae){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.START_TIME, CLOT.LotLossStartTime);
            m_gem.SetSVIDValue(CSVID.END_TIME, CLOT.LotLossEndTime);
            m_gem.SetSVIDValue(CSVID.OPERATION_LOSS_COMMENT, LossMessae);
            m_gem.SetSVIDValue(CSVID.OPERATION_LOSS_CODE, LossCode);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_EQP_LOSS_COMPLETE);
        }
        public void SetLotEqpChangeComplete(int nCode, string sComment){
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            m_gem.SetSVIDValue(CSVID.EQP_CHANGE_COMMENT, sComment);
            m_gem.SetSVIDValue(CSVID.EQP_CHANGE_CODE, nCode.ToString());
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            SendEvent(CCEID.LOT_EQP_CHANGE_COMPLETE);
        }
        public void SetPPSelect(){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            //m_gem.SendEventReport(CEID.PP_SELECTED);

            m_gem.SendEventReportEx(CCEID.PP_SELECTED, CCEID.PP_SELECTED);
            CCEID.m_nPPSelected = m_gem.GetSysByteEx(CCEID.PP_SELECTED);
            AddGemLog(string.Format("m_nPPSelected:{0}", CCEID.m_nPPSelected));
        }
        
        public void SetUpperDFKitting(string OldBladeBarcode, string NewBaldeBarcode){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.CUR_LEFT_BLADE, NewBaldeBarcode);
            m_gem.SetSVIDValue(CSVID.OLD_LEFT_BLADE, OldBladeBarcode);
            SendEvent(CCEID.UPPER_DF_KITTING);
        }
        public void SetLowerDFKitting(string OldBladeBarcode, string NewBaldeBarcode){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.CUR_RIGHT_BLADE, NewBaldeBarcode);
            m_gem.SetSVIDValue(CSVID.OLD_RIGHT_BLADE, OldBladeBarcode);
            SendEvent(CCEID.LOWER_DF_KITTING);
        }
        
        public void SetLotStartRequest(){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_START_REQUEST);
        }
        public void SetLotStarted(){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_STARTED);

            //lot validation 완료 -> 설비 run 진행 !
            CLOT.bLotValidationSusses = true;
        }
        public void SetLotLoading(){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.RESERVE_PANEL_QTY, CLOT.GET_LOT.Qty.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_LOADING);
        }
        
        public void SetPanelLineIn(int nPanelCnt, string sStripBarcode){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_ID, sStripBarcode);
            SendEvent(CCEID.PANEL_LINE_IN);
        }
        public void SetPanelModuleIn(int nPanelCnt, string sStripBarcode, int sModuleID){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.MODULE_ID, sModuleID.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_ID, sStripBarcode);
            SendEvent(CCEID.PANEL_MODULE_IN);
        }
        public void SetPanelModuleOut(int nPanelCnt, string sStripBarcode, int sModuleID){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.MODULE_ID, sModuleID.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_ID, sStripBarcode);
            SendEvent(CCEID.PANEL_MODULE_OUT);
        }
        public void SetPanelLineOut(int nPanelCnt, string sStripBarcode){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.PANEL_INDEX, nPanelCnt.ToString());
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.PANEL_ID, sStripBarcode);
            SendEvent(CCEID.PANEL_LINE_OUT);
        }
        
        public void SetLotComplete(int nPanelCnt){
            m_gem.SetSVIDValue(CSVID.LOT_ID, CLOT.GET_LOT.LotID);
            m_gem.SetSVIDValue(CSVID.RECIPE_ID, DATA_.sJobName);
            m_gem.SetSVIDValue(CSVID.LOT_TYPE, CLOT.GET_LOT.LotType.ToString());
            m_gem.SetSVIDValue(CSVID.RESERVE_PANEL_QTY, CLOT.GET_LOT.Qty.ToString());
            m_gem.SetSVIDValue(CSVID.COMPLETE_PANEL_QTY, nPanelCnt.ToString());
            m_gem.SetSVIDValue(CSVID.PROCESS_USER_ID, CUSER.Current.ID);
            SendEvent(CCEID.LOT_COMPLETE);
        }

        #endregion CEID 이벤트 처리

        public int SetSVID(int nSVID, string strValue)  {
            if (!bIni) return -1; // gem 초기화 진행 안하고 event 보내면 log 폴더 안에 폴더들 삭제 함 (엔비아 버그) 추후 엔비아 수정 시 까지 임시 적용!
            return m_gem.SetSVIDValue(nSVID, strValue); 
        }
        public int SendEvent(int nCEID)                 {
            if (!bIni) return -1; // gem 초기화 진행 안하고 event 보내면 log 폴더 안에 폴더들 삭제 함 (엔비아 버그) 추후 엔비아 수정 시 까지 임시 적용!
            return m_gem.SendEventReport(nCEID); 
        }
        public void RefreshSVID(){
            //m_gem.SetSVIDValue(SVID_USE_HD1, DATA_.prMODEL[37].ToString());

        }//SVID 값 갱신 

        private void timer1_Tick(object sender, EventArgs e){

        }
    }
}