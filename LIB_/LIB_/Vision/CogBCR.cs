using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Cognex_
{
    public class CogBCR
    {
        public PictureBox CogImage = null;
        public void SetImage(PictureBox image) { CogImage = image; }

        public DataManSystem _system = null;
        public ResultCollector _results;
        public ISystemConnector _connector = null;
        public object _currentResultInfoSyncLock = new object();
        public object _listAddItemLock = new object();

        private Thread tDATE;               //Current Date Time Thread
        private AutoResetEvent tEVENT;      //Current Date Time Event
        private string tCurDate = "";   //Current Date Time

        public List<string> sListLog = null; //
        public List<string> GetLog() { return sListLog; }

        public SynchronizationContext _syncContext = null;
        public EthSystemDiscoverer _ethSystemDiscoverer = null;
        public EthSystemDiscoverer.SystemInfo _GigeEInfo;  //GigaE 정보

        public bool bOpen = false;
        public bool bClosing = false;
        public bool bAutoConnect = false;
        public bool bSysConnected = false;
        public bool bRunKeepAlive = false;
        public string ReadResult = "";


        public void UpdateStatus() { bSysConnected = _system != null && _system.State == ConnectionState.Connected; }
        public void UpdateDate(){
            try{
                while (true){
                    tCurDate = "[" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "]";
                    tEVENT.WaitOne(500, false);
                }
            }
            catch (Exception ex) { AddLog("UpdateDate : " + ex.ToString()); }
        }

        public void AddLog(object log){
            lock (_listAddItemLock){
                try{
                    string sTIME = "[" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") + "]";
                    LogWR_.DEBUG_PRINT("[" + tCurDate/*sTIME*/ + "] cogBCR -> AddLog : " + log.ToString());
                }
                catch (Exception ex){
                    LogWR_.SaveLogException("cogBCR->AddLog Fail", ex);
                    return;
                }
            }
        }

        public CogBCR(){
            //DATE THREAD CREATE AND START
            tEVENT = new AutoResetEvent(false);
            tEVENT.Reset();

            tDATE = new Thread(UpdateDate);
            tDATE.IsBackground = true;
            tDATE.Name = "cogDATA THREAD";
            tDATE.Start();

            // The SDK may fire events from arbitrary thread context. Therefore if you want to change
            // the state of controls or windows from any of the SDK' events, you have to use this
            // synchronization context to execute the event handler code on the main GUI thread.
            _syncContext = WindowsFormsSynchronizationContext.Current;
            _ethSystemDiscoverer = new EthSystemDiscoverer(); // Create discoverers to discover ethernet systems.
            _ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);  // Subscribe to the system discoved event.
            _ethSystemDiscoverer.Discover(); // Ask the discoverers to start discovering systems.

            sListLog = new List<string>();
            sListLog.Clear();
            UpdateStatus();
        }
        ~CogBCR()
        {
            //DATA THREAD STOP ADN TERMINATE
            tEVENT.Set();
            tDATE.Join();
            tDATE = null;

            bClosing = true;
            bAutoConnect = false;
            if (null != _system && _system.State == ConnectionState.Connected) _system.Disconnect();
            _system = null;
        }
        public void OnEthSystemDiscovered(EthSystemDiscoverer.SystemInfo systemInfo){
            _syncContext.Post(
                new SendOrPostCallback(
                    delegate
                    {
                        _GigeEInfo = systemInfo;
                    }),
                    null);
        }

        public void CleanConnection(){
            if (_system != null)
            {

            }

        }

        public bool IsConnect(){
            if (_system == null) return false;
            return _system.State == ConnectionState.Connected ? true : false;
        }
        public bool Connect(){
            if (_GigeEInfo == null) goto OpenFail;

            try{
                EthSystemDiscoverer.SystemInfo eth_system_info = _GigeEInfo as EthSystemDiscoverer.SystemInfo;
                EthSystemConnector conn = new EthSystemConnector(eth_system_info.IPAddress, eth_system_info.Port);
                conn.UserName = "admin";
                conn.Password = "";
                _connector = conn;

                _system = new DataManSystem(_connector);
                _system.DefaultTimeout = 5000;

                // Subscribe to events that are signalled when the system is connected / disconnected.
                _system.SystemConnected += new SystemConnectedHandler(OnConnected);
                _system.SystemDisconnected += new SystemDisconnectedHandler(OnDisconnected);
                _system.SystemWentOnline += new SystemWentOnlineHandler(OnWentOnline);
                _system.SystemWentOffline += new SystemWentOfflineHandler(OnWentOffline);
                _system.KeepAliveResponseMissed += new KeepAliveResponseMissedHandler(OnKeepAliveResponseMissed);

                // Subscribe to events that are signalled when the deveice sends auto-responses.
                ResultTypes rlt = ResultTypes.ReadXml | ResultTypes.ReadString | ResultTypes.Image | ResultTypes.ImageGraphics;
                _results = new ResultCollector(_system, rlt);
                _results.ComplexResultCompleted += COMPLEX_RESULT_COMPLETED;
                _results.SimpleResultDropped/*PartialResultDropped*/ += REPORT_SIMPLE_RESULT_DROPPED;
                
                _system.SetKeepAliveOptions(bRunKeepAlive, 3000, 1000);
                _system.Connect();
            }
            catch (Exception ex){
                LogWR_.SaveLogException("", ex);
            }
        OpenFail:
            bOpen = false;
            return true;
        }

        public void OnConnected(object sender, EventArgs args){
            _syncContext.Post(
               delegate
               {
                   System.Diagnostics.Trace.WriteLine("System connected");
               },
               null);
        }
        public void OnDisconnected(object sender, EventArgs args){
            _syncContext.Post(
               delegate
               {
                   System.Diagnostics.Trace.WriteLine("System disconnected");
                   //if (!_closing && _autoconnect && bAutoReconect){
                   //    SUBFRM_.CogBarcoderReconnecting frm = new CogBarcoderReconnecting(this, _system);
                   //    if (frm.ShowDialog() == DialogResult.Cancel){
                   //    
                   //    }
                   //}
                   //else{
                   //
                   //}
                   //if (m_DM_SYSTEM == null || m_DM_SYSTEM.State != ConnectionState.Connected) return false;
                   //bAutoConnect = false;
                   //m_DM_SYSTEM.Disconnect();
                   //CleanConnection();
                   //m_Results.ClearCachedResults();
                   //m_Results = null;
                   //m_DM_SYSTEM = null;

                   bOpen = false;
               },
               null);
        }
        private void OnWentOnline(object sender, EventArgs args){
            _syncContext.Post(
                delegate
                {
                    System.Diagnostics.Trace.WriteLine("System went online");
                },
                null);
        }
        private void OnWentOffline(object sender, EventArgs args){
            _syncContext.Post(
                delegate
                {
                    System.Diagnostics.Trace.WriteLine("System went offline");
                },
                null);
        }
        private void OnKeepAliveResponseMissed(object sender, EventArgs args){
            _syncContext.Post(
                delegate
                {
                    System.Diagnostics.Trace.WriteLine("Keep-alive response missed");
                },
                null);
        }

        public void COMPLEX_RESULT_COMPLETED(object sender, ComplexResult Result){
            _syncContext.Post(
                 delegate
                 {
                     SHOW_RESULT(Result);
                 },
                 null);
            SHOW_RESULT(Result);
        }

        public void SHOW_RESULT(ComplexResult Result){
            List<Image> imgList = new List<Image>();
            List<string> img_graphics = new List<string>();
            string rdRESULT = string.Empty;
            int idRESULT = -1;
            ResultTypes Results = ResultTypes.None;

            // Take a reference or copy values from the locked result info object. This is done
            // so that the lock is used only for a short period of time.
            lock (_currentResultInfoSyncLock)
            {
                foreach (var simple_result in Result.SimpleResults)
                {
                    Results |= simple_result.Id.Type;
                    switch (simple_result.Id.Type)
                    {
                        case ResultTypes.Image:
                            Image img = ImageArrivedEventArgs.GetImageFromImageBytes(simple_result.Data);
                            if (imgList != null) imgList.Add(img);
                            break;
                        case ResultTypes.ImageGraphics:
                            img_graphics.Add(simple_result.GetDataAsString());
                            break;
                        case ResultTypes.ReadXml:
                            rdRESULT = GetReadStringFormResultXml(simple_result.GetDataAsString());
                            idRESULT = simple_result.Id.Id;

                            break;
                        case ResultTypes.ReadString:
                            rdRESULT = simple_result.GetDataAsString();
                            idRESULT = simple_result.Id.Id;
                            break;
                    }
                }
            }

            if (rdRESULT != null) { ReadResult = rdRESULT; }
            AddLog(string.Format("Complex result arrived: resultId = {0}, read result = {1}", idRESULT, rdRESULT));

            if (CogImage != null){
                if (imgList.Count > 0){
                    Image fIMAGE        = imgList[0];
                    Size ImageSize      = Gui.FitImageInControl(fIMAGE.Size, CogImage.Size);
                    Image FittedImage   = Gui.ResizeImageToBitmap(fIMAGE, ImageSize);
                    if (img_graphics.Count > 0){
                        using (Graphics g = Graphics.FromImage(FittedImage))
                        {
                            foreach (var graphics in img_graphics)
                            {
                                ResultGraphics rg = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, ImageSize.Width, ImageSize.Height));
                                ResultGraphicsRenderer.PaintResults(g, rg);
                            }
                        }
                    }
                    if (CogImage.Image != null){
                        var image       = CogImage.Image;
                        CogImage.Image  = null;
                        image.Dispose();
                    }
                    CogImage.Image = FittedImage;
                    CogImage.Invalidate();
                }
            }
        }

        public string GetReadStringFormResultXml(string Result){
            try{
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Result);
                XmlNode FullStringNode = doc.SelectSingleNode("result/general/full_string");
                if (FullStringNode != null && _system != null && _system.State == ConnectionState.Connected){
                    XmlAttribute encoding = FullStringNode.Attributes["encoding"];
                    if (encoding != null && encoding.InnerText == "base64"){
                        if (!string.IsNullOrEmpty(FullStringNode.InnerText)){
                            byte[] code = Convert.FromBase64String(FullStringNode.InnerText);
                            return _system.Encoding.GetString(code, 0, code.Length);
                        }
                        else return "";
                    }
                }
            }
            catch (Exception e){
                AddLog("GetReadStringFormResultXml Fail : " + e.Message);
            }
            return "";
        }

        void REPORT_SIMPLE_RESULT_DROPPED(object sender, SimpleResult e){
            _syncContext.Post(
                delegate{
                    REPORT_DROPPED_RESULT(e);
                },
                null);
        }
        void REPORT_DROPPED_RESULT(SimpleResult e){
            List<string> dropped = new List<string>();
            AddLog(String.Format("Partial result dropped : {0}, id = {1}", e.Id.Type.ToString(), e.Id.Id));
            System.Diagnostics.Trace.WriteLine("Partial results dropped: " + String.Join(", ", dropped.ToArray()));
        }
    }
}